import { Component, OnInit } from "@angular/core";
import { AuthService } from "@avocado/auth/services/auth.service";
import { ActivatedRoute, Router, ParamMap } from "@angular/router";
import { faGoogle } from "@fortawesome/free-brands-svg-icons";
import { Observable } from "rxjs";
import { map, take } from "rxjs/operators";
import { FormBuilder, Validators } from "@angular/forms";
import { AuthResult } from "@avocado/auth/models/AuthResult";

@Component({
  selector: "av-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {
  isLoggingIn = false;
  googleIcon = faGoogle;
  isLoggedOut = false;
  showForm$: Observable<boolean>;
  registrationForm = this.formBuilder.group({
    name: ["", [Validators.required]],
    userName: ["", [Validators.required]]
  });

  private isLoggingOut$: Observable<boolean>;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.isLoggingOut$ = this.route.queryParamMap.pipe(
      map((paramMap: ParamMap) => paramMap.get("logout") === "true")
    );
    this.showForm$ = this.route.queryParamMap.pipe(
      map((paramMap: ParamMap) => paramMap.get("register") === "true")
    );
  }

  ngOnInit() {
    this.isLoggingOut$.subscribe((isLoggingOut: boolean) => {
      if (isLoggingOut) {
        this.authService.logout();
        this.isLoggedOut = true;
      }
    });
  }

  login(): void {
    this.isLoggingIn = true;
    this.authService.login().subscribe(this.postAuthentication);
  }

  startRegistration(): void {
    const queryParams: any = {
      register: true
    };
    const redirectUrl = this.route.snapshot.queryParamMap.get("redirectUrl");
    if (redirectUrl && redirectUrl.length > 0) {
      queryParams.redirectUrl = redirectUrl;
    }
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams
    });
  }

  register(): void {
    if (!this.registrationForm.valid) {
      return;
    }
    this.isLoggingIn = true;
    const name = this.registrationForm.get("name").value;
    const userName = this.registrationForm.get("name").value;
    this.authService
      .register(name, userName)
      .subscribe(this.postAuthentication);
  }

  cancelRegistration(): void {
    const queryParams: any = {};
    const redirectUrl = this.route.snapshot.queryParamMap.get("redirectUrl");
    if (redirectUrl && redirectUrl.length > 0) {
      queryParams.redirectUrl = redirectUrl;
    }
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams
    });
  }

  private postAuthentication = (result: AuthResult): void => {
    this.isLoggingIn = false;
    if (result !== AuthResult.Success) {
      console.log(`AuthResult ${result}`);
      // TODO: display some sort of error
      return;
    }

    // navigate away
    const redirectUrl = this.route.snapshot.queryParamMap.get("redirectUrl");
    if (redirectUrl && redirectUrl.length > 0) {
      const decoded = decodeURIComponent(redirectUrl);
      this.router.navigate([decoded]);
    } else {
      this.router.navigate([""]);
    }
  };
}
