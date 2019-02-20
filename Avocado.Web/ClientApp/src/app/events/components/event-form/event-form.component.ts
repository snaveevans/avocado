import { Component, OnInit } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { EventService } from "@avocado/events/services/event.service";
import { EventForm } from "@avocado/events/models/EventForm";
import { Router } from "@angular/router";
import { allRoutes, RouteName } from "@avocado/core/routes/app-routes";

@Component({
  selector: "av-event-form",
  templateUrl: "./event-form.component.html",
  styleUrls: ["./event-form.component.scss"]
})
export class EventFormComponent implements OnInit {
  eventForm = this.formBuilder.group({
    title: ["", Validators.required],
    description: ["", Validators.required]
  });

  constructor(
    private formBuilder: FormBuilder,
    private eventService: EventService,
    private router: Router
  ) {}

  ngOnInit() {}

  handleSubmit(): void {
    if (!this.eventForm.valid) {
      return;
    }
    const title = this.eventForm.get("title").value;
    const description = this.eventForm.get("description").value;
    const form = new EventForm(title, description);
    this.eventService
      .create(form)
      .subscribe(_ => this.router.navigate([allRoutes[RouteName.MyEvents]]));
  }
}
