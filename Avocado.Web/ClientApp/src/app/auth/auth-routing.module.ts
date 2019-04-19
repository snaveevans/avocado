import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LogoutComponent } from "@avocado/auth/components/logout/logout.component";
import { RegisterFormComponent } from "@avocado/auth/components/register-form/register-form.component";
import { LoginComponent } from "@avocado/auth/components/login/login.component";

const routes: Routes = [
  {
    path: "login",
    component: LoginComponent
  },
  {
    path: "logout",
    component: LogoutComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {}
