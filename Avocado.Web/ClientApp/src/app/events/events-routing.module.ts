import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { EventFormComponent } from "@avocado/events/components/event-form/event-form.component";
import { EventsComponent } from "@avocado/events/components/events/events.component";

const routes: Routes = [
  { path: "", component: EventsComponent, data: { events: "all" } },
  { path: "mine", component: EventsComponent, data: { events: "mine" } },
  {
    path: "upcoming",
    component: EventsComponent,
    data: { events: "upcoming" }
  },
  { path: "new", component: EventFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventsRoutingModule {}
