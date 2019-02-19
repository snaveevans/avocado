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
import { AuthResult } from "@avocado/auth/models/AuthResult";
import { AccessTokenResult } from "@avocado/auth/models/AccessTokenResult";

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

  login = (): Observable<AuthResult> =>
    from(this.getProviderToken()).pipe(
      switchMap(
        (tokenResult: AccessTokenResult): Observable<AuthResult> => {
          if (tokenResult.authResult !== AuthResult.Success) {
            return of(tokenResult.authResult);
          }
          return this.fetchToken(
            new LoginModel("GOOGLE", tokenResult.accessToken)
          );
        }
      ),
      catchError(() => of(AuthResult.Unknown))
    );

  register = (name: string, userName: string): Observable<AuthResult> =>
    from(this.getProviderToken()).pipe(
      switchMap(
        (tokenResult: AccessTokenResult): Observable<AuthResult> => {
          if (tokenResult.authResult !== AuthResult.Success) {
            return of(tokenResult.authResult);
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
              (account: Account): Observable<AuthResult> => {
                if (!account) {
                  return of(AuthResult.RegistrationFailed);
                }

                return this.fetchToken(registerModel);
              }
            ),
            catchError(() => of(AuthResult.RegistrationFailed))
          );
        }
      )
    );

  private getProviderToken = async () => {
    const provider = new firebase.auth.GoogleAuthProvider();
    provider.addScope("https://www.googleapis.com/auth/contacts.readonly");
    const firebaseAuth = firebase.auth();
    if (!firebaseAuth) {
      return new AccessTokenResult(AuthResult.FirebaseAuthError);
    }

    firebaseAuth.useDeviceLanguage();

    try {
      await firebaseAuth.signInWithPopup(provider);
      if (!firebaseAuth.currentUser) {
        return new AccessTokenResult(AuthResult.FirebaseUserError);
      }
      const accessToken = await firebaseAuth.currentUser.getIdToken(false);
      return new AccessTokenResult(AuthResult.Success, accessToken);
    } catch (error) {
      return new AccessTokenResult(AuthResult.FirebaseCancel);
    }
  };

  private fetchToken = (model: LoginModel): Observable<AuthResult> =>
    !model.accessToken.length
      ? of(AuthResult.InvalidAccessToken)
      : this.http.post<TokenResult>("api/auth", model).pipe(
          map(({ token }) => {
            this.setToken(token);
            return AuthResult.Success;
          }),
          catchError(() => of(AuthResult.Unknown))
        );

  logout(): void {
    this.token = null;
    this.jwtData$.next(null);
    localStorage.setItem("token", "");
  }
}
