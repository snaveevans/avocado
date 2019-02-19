import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { JwtData } from "@avocado/auth/models/JwtData";
import * as firebase from "firebase/app";
import "firebase/auth";
import { BehaviorSubject, from, Observable, of, Subject } from "rxjs";
import { map, shareReplay, switchMap, tap, catchError } from "rxjs/operators";
import { FirebaseConfig } from "@avocado/auth/models/FirebaseConfig";
import { RegisterModel } from "@avocado/auth/models/RegisterModel";
import { LoginModel } from "@avocado/auth/models/LoginModel";
import { TokenResult } from "@avocado/auth/models/TokenResult";
import { AccessTokenResult } from "@avocado/auth/models/AccessTokenResult";
import { IdentityError } from "@avocado/auth/models/IdentityError";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  private isInitialized = false;
  private token?: string;
  private jwtData$ = new BehaviorSubject<JwtData>(null);
  private fetchFirebaseConfig = new Subject();

  isAuthenticated$ = this.jwtData$.pipe(
    tap(_ => setTimeout(this.initializeIfNeeded.bind(this), 1)),
    map((jwtData?: JwtData) => {
      return Boolean(jwtData) && !jwtData.isExpired();
    }),
    shareReplay(1)
  );

  constructor(private http: HttpClient) {
    this.fetchFirebaseConfig.subscribe(_ =>
      this.http
        .get<FirebaseConfig>("api/auth/firebase-config")
        .subscribe((config: FirebaseConfig) => {
          firebase.initializeApp(config);
          this.fetchFirebaseConfig.complete();
        })
    );
  }

  private initializeIfNeeded(): void {
    if (this.isInitialized) {
      return;
    }
    this.isInitialized = true;

    const token = localStorage.getItem("token");
    if (token && token.length > 0) {
      this.setToken(token);
    }

    this.fetchFirebaseConfig.next();
  }

  private setToken(token: string): void {
    // TODO: set account
    this.isInitialized = true;
    this.token = token;
    this.jwtData$.next(new JwtData(token));
    localStorage.setItem("token", token);
  }

  getToken(): string {
    this.initializeIfNeeded();
    return this.token;
  }

  login = (): Observable<IdentityError[]> =>
    from(this.getProviderToken()).pipe(
      switchMap(
        (tokenResult: AccessTokenResult): Observable<IdentityError[]> => {
          if (tokenResult.errors.length) {
            return of(tokenResult.errors);
          }
          return this.fetchToken(
            new LoginModel("GOOGLE", tokenResult.accessToken)
          );
        }
      ),
      catchError(_ => of([IdentityError.providerTokenFailed()]))
    );

  register = (name: string, userName: string): Observable<IdentityError[]> =>
    from(this.getProviderToken()).pipe(
      switchMap(
        (tokenResult: AccessTokenResult): Observable<IdentityError[]> => {
          if (tokenResult.errors.length) {
            return of(tokenResult.errors);
          }
          const url = `api/account/register`;
          const registerModel = new RegisterModel(
            name,
            userName,
            "GOOGLE",
            tokenResult.accessToken
          );
          return this.http.post<Account>(url, registerModel).pipe(
            switchMap(
              (account: Account): Observable<IdentityError[]> => {
                if (!account) {
                  return of([IdentityError.registrationFailed()]);
                }

                return this.fetchToken(registerModel);
              }
            ),
            catchError(result => of(result.error as IdentityError[]))
          );
        }
      ),
      catchError(_ => of([IdentityError.providerTokenFailed()]))
    );

  private getProviderToken = async (): Promise<AccessTokenResult> => {
    const provider = new firebase.auth.GoogleAuthProvider();
    provider.addScope("https://www.googleapis.com/auth/contacts.readonly");
    const firebaseAuth = firebase.auth();
    if (!firebaseAuth) {
      return AccessTokenResult.failed([IdentityError.firebaseAuthError()]);
    }

    firebaseAuth.useDeviceLanguage();

    try {
      await firebaseAuth.signInWithPopup(provider);
      if (!firebaseAuth.currentUser) {
        return AccessTokenResult.failed([IdentityError.firebaseUserError()]);
      }
      const accessToken = await firebaseAuth.currentUser.getIdToken(false);
      return AccessTokenResult.succeeded(accessToken);
    } catch (error) {
      return AccessTokenResult.failed([IdentityError.firebaseCancel()]);
    }
  };

  private fetchToken = (model: LoginModel): Observable<IdentityError[]> =>
    !model.accessToken.length
      ? of([IdentityError.invalidAccessToken()])
      : this.http.post<TokenResult>("api/auth", model).pipe(
          map(({ token }) => {
            this.setToken(token);
            return [];
          }),
          catchError(result => of(result.error as IdentityError[]))
        );

  logout(): void {
    this.token = null;
    this.jwtData$.next(null);
    localStorage.setItem("token", "");
  }
}
