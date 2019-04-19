import { Component } from "@angular/core";
import { ActivatedRoute, ParamMap, Router } from "@angular/router";
import { IdentityError } from "@avocado/auth/models/IdentityError";
import { RegisterForm } from "@avocado/auth/models/RegisterForm";
import { AuthService } from "@avocado/auth/services/auth.service";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Component({
  selector: "av-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent {
  errors: IdentityError[] = [];
  isRegistering$: Observable<boolean>;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.isRegistering$ = this.route.queryParamMap.pipe(
      map((paramMap: ParamMap) => paramMap.get("register") === "true")
    );
  }

  login(): void {
    this.errors = [];
    this.authService.login().subscribe(this.postAuthentication);
  }

  showForm(): void {
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

  register(registerForm: RegisterForm): void {
    this.authService.register(registerForm).subscribe(this.postAuthentication);
  }

  cancelRegistration(): void {
    this.errors = [];
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

  private postAuthentication = (errors: IdentityError[]): void => {
    if (errors.length) {
      this.errors = errors;
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
