import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { getRoutes, menuRoutes } from "@avocado/core/routes/app-routes";

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

  menuRoutes = getRoutes(menuRoutes);

  constructor() {}

  ngOnInit() {}

  handleChange(isOpen: boolean): void {
    if (!isOpen) {
      this.close.emit();
    }
  }
}
