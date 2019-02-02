import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

const routes: Routes = [
  { path: "", redirectTo: "/events", pathMatch: "full" },
  {
    path: "events",
    loadChildren: "@avocado/events/events.module#EventsModule"
  },
  {
    path: "contacts",
    loadChildren: "@avocado/contacts/contacts.module#ContactsModule"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
