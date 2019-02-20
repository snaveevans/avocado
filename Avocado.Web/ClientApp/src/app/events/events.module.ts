import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { CoreModule } from "@avocado/core/core.module";
import { EventFormComponent } from "@avocado/events/components/event-form/event-form.component";
import { EventListComponent } from "@avocado/events/components/event-list/event-list.component";
import { EventsRoutingModule } from "@avocado/events/events-routing.module";
import { MaterialModule } from "@avocado/material/material.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { EventCardComponent } from './components/event-card/event-card.component';

@NgModule({
  declarations: [EventListComponent, EventFormComponent, EventCardComponent],
  imports: [
    CommonModule,
    EventsRoutingModule,
    MaterialModule,
    CoreModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class EventsModule {}
