import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { JwtData } from "@avocado/auth/models/JwtData";
import * as firebase from "firebase/app";
import "firebase/auth";
import { FirebaseConfig } from "@avocado/auth/models/FirebaseConfig";

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
    this.isInitialized = true;
    this.token = token;
    this.jwtData$.next(new JwtData(token));
    localStorage.setItem("token", token);
  }

  getToken(): string {
    this.initializeIfNeeded();
    return this.token;
  }

  login = () => this.authenticate("login");
  register = () => this.authenticate("register");

  private authenticate(mode: "login" | "register"): Observable<boolean> {
    const provider = new firebase.auth.GoogleAuthProvider();
    provider.addScope("https://www.googleapis.com/auth/contacts.readonly");
    const firebaseAuth = firebase.auth();
    if (!firebaseAuth) {
      return of(false);
    }

    firebaseAuth.useDeviceLanguage();

    const signIn = from(firebaseAuth.signInWithPopup(provider));

    return signIn.pipe(
      switchMap((result: firebase.auth.UserCredential) => {
        if (!firebaseAuth.currentUser) {
          return of(false);
        }

        const getToken = from(firebaseAuth.currentUser.getIdToken(false));
        return getToken.pipe(
          switchMap((accessToken: string) => {
            const url = mode === "login" ? `api/auth/` : `api/account/register`;
            // TODO: refactor for registering
            const body = {
              provider: "GOOGLE",
              accessToken
            };
            return this.http.post<TokenResult>(url, body).pipe(
              map(({ token }) => {
                this.setToken(token);
                return true;
              })
            );
          })
        );
      })
    );
  }

  logout(): void {
    this.token = null;
    this.jwtData$.next(null);
    localStorage.setItem("token", "");
  }
}

interface TokenResult {
  token: string;
}
