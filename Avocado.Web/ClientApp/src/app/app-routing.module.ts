import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "./auth/services/auth.guard";

const authenticatedRoutes: Routes = [
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

const routes: Routes = [
  {
    path: "",
    canActivate: [AuthGuard],
    children: authenticatedRoutes
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
