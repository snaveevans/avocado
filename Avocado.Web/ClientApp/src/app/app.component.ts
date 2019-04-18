import { Component } from "@angular/core";
import { AuthService } from "@avocado/auth/services/auth.service";
import { Observable } from "rxjs";

@Component({
  selector: "av-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  isSideNavOpen = false;
  isAuthenticated$: Observable<boolean>;

  constructor(authService: AuthService) {
    this.isAuthenticated$ = authService.isAuthenticated$;
  }

  handleMenuClick(): void {
    this.isSideNavOpen = !this.isSideNavOpen;
  }

  handleClose(): void {
    this.isSideNavOpen = false;
  }
}
