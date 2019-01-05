import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { EventListComponent } from "@avocado/events/components/event-list/event-list.component";

const routes: Routes = [{ path: "", component: EventListComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventsRoutingModule {}
