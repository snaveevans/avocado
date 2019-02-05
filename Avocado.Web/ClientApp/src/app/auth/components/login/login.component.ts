import { Component, OnInit } from "@angular/core";
import { AuthService } from "@avocado/auth/services/auth.service";
import { ActivatedRoute, Router } from "@angular/router";
import { faGoogle } from "@fortawesome/free-brands-svg-icons";

@Component({
  selector: "av-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {
  isLoggingIn = false;
  googleIcon = faGoogle;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {}

  handleLoginClick(): void {
    this.authenticate("login");
  }

  handleRegisterClick(): void {
    this.authenticate("register");
  }

  private authenticate(mode: "login" | "register"): void {
    this.isLoggingIn = true;
    this.authService.authenticate(mode).subscribe((isLoggedIn: boolean) => {
      this.isLoggingIn = false;
      if (!isLoggedIn) {
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
    });
  }
}
