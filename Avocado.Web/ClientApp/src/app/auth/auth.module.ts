import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AuthRoutingModule } from "@avocado/auth/auth-routing.module";
import { LoginComponent } from "@avocado/auth/components/login/login.component";
import { LogoutComponent } from "@avocado/auth/components/logout/logout.component";
import { RegisterFormComponent } from "@avocado/auth/components/register-form/register-form.component";
import { MaterialModule } from "@avocado/material/material.module";

@NgModule({
  declarations: [LoginComponent, LogoutComponent, RegisterFormComponent],
  imports: [
    CommonModule,
    AuthRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class AuthModule {}
