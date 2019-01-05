import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { EventsRoutingModule } from "@avocado/events/events-routing.module";
import { EventListComponent } from "@avocado/events/components/event-list/event-list.component";

@NgModule({
  declarations: [EventListComponent],
  imports: [CommonModule, EventsRoutingModule]
})
export class EventsModule {}
