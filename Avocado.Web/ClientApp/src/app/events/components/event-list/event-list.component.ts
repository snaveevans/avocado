import { Component, OnInit } from "@angular/core";

@Component({
  selector: "av-event-list",
  templateUrl: "./event-list.component.html",
  styleUrls: ["./event-list.component.scss"]
})
export class EventListComponent implements OnInit {
  cards = [1, 2, 3, 4, 5, 6];

  constructor() {}

  ngOnInit() {}
}
