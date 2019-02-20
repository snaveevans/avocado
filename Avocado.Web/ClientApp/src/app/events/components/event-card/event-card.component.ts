import { Component, OnInit, Input } from "@angular/core";
import { EventModel } from "@avocado/events/models/EventModel";

@Component({
  selector: "av-event-card",
  templateUrl: "./event-card.component.html",
  styleUrls: ["./event-card.component.scss"]
})
export class EventCardComponent {
  @Input()
  event: EventModel;
}
