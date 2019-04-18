import { Component, Input } from "@angular/core";
import { EventModel } from "@avocado/events/models/EventModel";

@Component({
  selector: "av-event-list",
  templateUrl: "./event-list.component.html",
  styleUrls: ["./event-list.component.scss"]
})
export class EventListComponent {
  @Input()
  events: EventModel[] = [];

  handleClick(event: EventModel): void {
    console.log("show card details");
  }
}
