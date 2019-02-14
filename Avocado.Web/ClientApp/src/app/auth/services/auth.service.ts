import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { JwtData } from "@avocado/auth/models/JwtData";
import * as firebase from "firebase/app";
import "firebase/auth";
import { BehaviorSubject, from, Observable, of } from "rxjs";
import { map, shareReplay, switchMap, tap } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  private isInitialized = false;
  private token?: string;
  private jwtData$ = new BehaviorSubject<JwtData>(null);

  isAuthenticated$ = this.jwtData$.pipe(
    tap(_ => setTimeout(this.initializeIfNeeded.bind(this), 1)),
    map((jwtData?: JwtData) => {
      return Boolean(jwtData) && !jwtData.isExpired();
    }),
    shareReplay(1)
  );

  constructor(private http: HttpClient) {
    const config = {
      apiKey: "AIzaSyA8ywmoMF2iSp0TX4Z1D9IIYbCPkP-Ho30",
      authDomain: "avocado-208414.firebaseapp.com",
      databaseURL: "https://avocado-208414.firebaseio.com",
      projectId: "avocado-208414"
    };
    firebase.initializeApp(config);
  }

  private initializeIfNeeded(): void {
    if (this.isInitialized) {
      return;
    }
    this.isInitialized = true;

    const token = localStorage.getItem("token");
    if (!token || token.length <= 0) {
      return;
    }

    this.setToken(token);
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
          switchMap((idToken: string) => {
            const url = `api/token/${mode}`;
            const options = {
              method: "GET",
              mode: "cors",
              cache: "no-cache",
              headers: {
                "Id-Token": idToken
              }
            };
            return this.http.get<TokenResult>(url, options).pipe(
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
