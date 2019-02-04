import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { MaterialModule } from "@avocado/material/material.module";
import { AuthRoutingModule } from "./auth-routing.module";
import { LoginComponent } from "./components/login/login.component";

@NgModule({
  declarations: [LoginComponent],
  imports: [CommonModule, AuthRoutingModule, MaterialModule]
})
export class AuthModule {}
