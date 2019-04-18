import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AccessTokenResult } from "@avocado/auth/models/AccessTokenResult";
import { FirebaseConfig } from "@avocado/auth/models/FirebaseConfig";
import { IdentityError } from "@avocado/auth/models/IdentityError";
import { JwtData } from "@avocado/auth/models/JwtData";
import { LoginModel } from "@avocado/auth/models/LoginModel";
import { RegisterModel } from "@avocado/auth/models/RegisterModel";
import { TokenResult } from "@avocado/auth/models/TokenResult";
import * as firebase from "firebase/app";
import "firebase/auth";
import { BehaviorSubject, from, Observable, of, Subject } from "rxjs";
import {
  catchError,
  filter,
  map,
  shareReplay,
  switchMap,
  tap,
  retry
} from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  private firebaseConfigActual$ = new BehaviorSubject<FirebaseConfig>(null);
  private firebaseConfig$: Observable<
    FirebaseConfig
  > = this.firebaseConfigActual$.pipe(
    tap((config: FirebaseConfig) => {
      if (config === null) {
        this.http
          .get<FirebaseConfig>("api/auth/firebase-config")
          .pipe(retry(3))
          .subscribe(c => this.firebaseConfigActual$.next(c));
      } else {
        firebase.initializeApp(config);
      }
    }),
    filter((config: FirebaseConfig) => config !== null),
    shareReplay(1)
  );

  private tokenActual$ = new BehaviorSubject<string>("init");
  token$: Observable<string> = this.tokenActual$.pipe(
    tap((token: string) => {
      if (token !== null && token !== "init") {
        localStorage.setItem("token", token);
      } else if (token === "init") {
        // token hasn't been in
        this.tokenActual$.next(localStorage.getItem("token") || "");
      }
    }),
    filter((token: string) => token !== "init"),
    shareReplay(1)
  );

  isAuthenticated$: Observable<boolean> = this.token$.pipe(
    map((token: string) => (token ? new JwtData(token) : null)),
    map((jwtData?: JwtData) => Boolean(jwtData) && !jwtData.isExpired()),
    shareReplay(1)
  );

  constructor(private http: HttpClient) {}

  login = (): Observable<IdentityError[]> =>
    this.firebaseConfig$.pipe(
      switchMap(_ => from(this.getProviderToken())),
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
    this.firebaseConfig$.pipe(
      switchMap(_ => from(this.getProviderToken())),
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
            this.tokenActual$.next(token);
            return [];
          }),
          catchError(result => of(result.error as IdentityError[]))
        );

  logout(): void {
    this.tokenActual$.next("");
  }
}
