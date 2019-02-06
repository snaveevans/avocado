import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { EventListComponent } from "@avocado/events/components/event-list/event-list.component";
import { EventFormComponent } from "@avocado/events/components/event-form/event-form.component";

const routes: Routes = [
  { path: "", component: EventListComponent },
  { path: "new", component: EventFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventsRoutingModule {}
