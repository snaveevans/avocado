import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ContactListComponent } from "./components/contact-list/contact-list.component";

const routes: Routes = [{ path: "", component: ContactListComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContactsRoutingModule {}
