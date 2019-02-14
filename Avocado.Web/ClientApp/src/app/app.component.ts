import { Component } from "@angular/core";

@Component({
  selector: "av-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  isSideNavOpen = false;

  handleMenuClick(): void {
    this.isSideNavOpen = !this.isSideNavOpen;
  }

  handleClose(): void {
    this.isSideNavOpen = false;
  }
}
