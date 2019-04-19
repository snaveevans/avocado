import { Component, OnInit } from "@angular/core";
import { AuthService } from "@avocado/auth/services/auth.service";

@Component({
  selector: "av-logout",
  templateUrl: "./logout.component.html",
  styleUrls: ["./logout.component.scss"]
})
export class LogoutComponent implements OnInit {
  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.logout();
  }
}
