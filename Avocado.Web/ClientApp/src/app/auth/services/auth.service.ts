import { Injectable } from "@angular/core";
import * as firebase from "firebase/app";
import "firebase/auth";
import { Observable, of, from } from "rxjs";
import { switchMap, map } from "rxjs/operators";
import { HttpClient } from "@angular/common/http";
import { JwtData } from "@avocado/auth/models/JwtData";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  private isInitialized = false;
  private token?: string;
  private jwtData?: JwtData;

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
    this.jwtData = new JwtData(token);
    localStorage.setItem("token", token);
  }

  getToken(): string {
    this.initializeIfNeeded();
    return this.token;
  }

  isAuthenticated(): boolean {
    this.initializeIfNeeded();
    return this.jwtData && !this.jwtData.isExpired();
  }

  authenticate(mode: "login" | "register"): Observable<boolean> {
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

  repudiate(): void {
    this.token = undefined;
    this.jwtData = undefined;
  }
}

interface TokenResult {
  token: string;
}
