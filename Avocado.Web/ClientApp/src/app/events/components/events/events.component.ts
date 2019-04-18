import { Component, OnInit } from "@angular/core";
import { EventModel } from "@avocado/events/models/EventModel";
import { EventService } from "@avocado/events/services/event.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "av-events",
  templateUrl: "./events.component.html",
  styleUrls: ["./events.component.scss"]
})
export class EventsComponent implements OnInit {
  events: EventModel[] = [];

  constructor(private eventService: EventService, route: ActivatedRoute) {
    route.data.subscribe(console.log);
  }

  ngOnInit() {
    this.eventService
      .getEvents()
      .subscribe((events: EventModel[]) => (this.events = events));
  }
}
