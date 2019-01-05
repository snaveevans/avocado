import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

const routes: Routes = [
  { path: "", redirectTo: "/events", pathMatch: "full" },
  { path: "events", loadChildren: "@avocado/events/events.module#EventsModule" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
