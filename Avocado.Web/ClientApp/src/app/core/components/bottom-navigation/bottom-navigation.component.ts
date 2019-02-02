import { Component, OnInit } from "@angular/core";
import { getRoutes, bottomRoutes } from "@avocado/core/routes/app-routes";

@Component({
  selector: "av-bottom-navigation",
  templateUrl: "./bottom-navigation.component.html",
  styleUrls: ["./bottom-navigation.component.scss"]
})
export class BottomNavigationComponent implements OnInit {
  routes = getRoutes(bottomRoutes);

  constructor() {}

  ngOnInit() {}
}
