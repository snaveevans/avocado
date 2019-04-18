import { Component, Input } from "@angular/core";
import { EventModel } from "@avocado/events/models/EventModel";
import {
  faCalendarCheck,
  faMapMarkerAlt,
  faUser,
  faCalendarPlus
} from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: "av-event-card",
  templateUrl: "./event-card.component.html",
  styleUrls: ["./event-card.component.scss"]
})
export class EventCardComponent {
  userIcon = faUser;
  mapMarkerIcon = faMapMarkerAlt;
  times = [1, 2, 3];
  get calendarIcon() {
    return this.isPoll ? faCalendarPlus : faCalendarCheck;
  }

  @Input()
  event: EventModel;
  @Input()
  i: number;
  get isPoll(): boolean {
    return this.i % 2 === 0;
  }
  get isAdminMember(): boolean {
    return this.i > 1;
  }
}
