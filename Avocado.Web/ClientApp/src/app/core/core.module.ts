import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { HttpClientModule } from "@angular/common/http";

import { MaterialModule } from "@avocado/material/material.module";
import { ToolbarComponent } from "@avocado/core/components/toolbar/toolbar.component";
import { BottomNavigationComponent } from "@avocado/core/components/bottom-navigation/bottom-navigation.component";
import { SideNavigationComponent } from "@avocado/core/components/side-navigation/side-navigation.component";

const COMPONENTS = [
  ToolbarComponent,
  BottomNavigationComponent,
  SideNavigationComponent
];

@NgModule({
  imports: [CommonModule, RouterModule, MaterialModule, HttpClientModule],
  declarations: COMPONENTS,
  exports: COMPONENTS
})
export class CoreModule {}
