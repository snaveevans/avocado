import { Component, EventEmitter, Input, Output } from "@angular/core";
import { AuthService } from "@avocado/auth/services/auth.service";
import { AppRoute } from "@avocado/core/routes/app-route";
import {
  allRoutes,
  getRoutes,
  menuRoutes,
  RouteName
} from "@avocado/core/routes/app-routes";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Component({
  selector: "av-side-navigation",
  templateUrl: "./side-navigation.component.html",
  styleUrls: ["./side-navigation.component.scss"]
})
export class SideNavigationComponent {
  @Input()
  isOpen = false;
  @Output()
  close = new EventEmitter();
  menuRoutes$: Observable<AppRoute[]>;

  constructor(authService: AuthService) {
    this.menuRoutes$ = authService.isAuthenticated$.pipe(
      map((isAuthenticated: boolean) => {
        const loginRoute = allRoutes[RouteName.Login];
        if (isAuthenticated) {
          return getRoutes(menuRoutes).filter(
            (route: AppRoute) => route !== loginRoute
          );
        }
        return [loginRoute];
      })
    );
  }

  handleChange(isOpen: boolean): void {
    if (!isOpen) {
      this.close.emit();
    }
  }

  handleLinkClick(): void {
    this.isOpen = false;
  }
}
