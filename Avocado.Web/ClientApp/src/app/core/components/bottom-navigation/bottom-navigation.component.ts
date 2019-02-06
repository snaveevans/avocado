import { Component } from "@angular/core";
import { AuthService } from "@avocado/auth/services/auth.service";
import { bottomRoutes, getRoutes } from "@avocado/core/routes/app-routes";
import { Observable } from "rxjs";

@Component({
  selector: "av-bottom-navigation",
  templateUrl: "./bottom-navigation.component.html",
  styleUrls: ["./bottom-navigation.component.scss"]
})
export class BottomNavigationComponent {
  routes = getRoutes(bottomRoutes);
  isAuthenticated$: Observable<boolean>;

  constructor(authService: AuthService) {
    this.isAuthenticated$ = authService.isAuthenticated$;
  }
}
