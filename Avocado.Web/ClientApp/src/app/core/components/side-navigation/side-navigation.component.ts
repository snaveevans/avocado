import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import {
  getRoutes,
  menuRoutes,
  allRoutes,
  RouteName
} from "@avocado/core/routes/app-routes";
import { AuthService } from "@avocado/auth/services/auth.service";
import { Observable } from "rxjs";
import { AppRoute } from "@avocado/core/routes/app-route";
import { map } from "rxjs/operators";

@Component({
  selector: "av-side-navigation",
  templateUrl: "./side-navigation.component.html",
  styleUrls: ["./side-navigation.component.scss"]
})
export class SideNavigationComponent implements OnInit {
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

  ngOnInit() {}

  handleChange(isOpen: boolean): void {
    if (!isOpen) {
      this.close.emit();
    }
  }

  handleLinkClick(): void {
    this.isOpen = false;
  }
}
