import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { AuthService } from "@avocado/auth/services/auth.service";
import { AppRoute } from "@avocado/core/routes/app-route";
import {
  allRoutes,
  getRoutes,
  menuRoutes,
  RouteName
} from "@avocado/core/routes/app-routes";
import { ScrollService } from "@avocado/core/services/scroll.service";
import { Observable } from "rxjs";
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
  horizontalScrollPosition = 0;

  menuRoutes$: Observable<AppRoute[]>;

  constructor(authService: AuthService, private scrollService: ScrollService) {
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

  ngOnInit() {
    this.scrollService.horizontalListener$.subscribe(
      (position: number) => (this.horizontalScrollPosition = position)
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

  handleScroll(event: UIEvent): void {
    this.scrollService.registerScrollEvent(event);
  }
}
