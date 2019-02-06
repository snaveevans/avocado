import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { EventsRoutingModule } from "@avocado/events/events-routing.module";
import { EventListComponent } from "@avocado/events/components/event-list/event-list.component";
import { EventFormComponent } from "@avocado/events/components/event-form/event-form.component";

@NgModule({
  declarations: [EventListComponent, EventFormComponent],
  imports: [CommonModule, EventsRoutingModule]
})
export class EventsModule {}
