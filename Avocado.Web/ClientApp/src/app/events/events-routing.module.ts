import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { EventFormComponent } from "@avocado/events/components/event-form/event-form.component";
import { EventListComponent } from "@avocado/events/components/event-list/event-list.component";

const routes: Routes = [
  { path: "", component: EventListComponent },
  { path: "new", component: EventFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventsRoutingModule {}
