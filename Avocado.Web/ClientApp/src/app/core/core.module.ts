import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { HttpClientModule } from "@angular/common/http";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";

import { MaterialModule } from "@avocado/material/material.module";
import { ToolbarComponent } from "@avocado/core/components/toolbar/toolbar.component";
import { BottomNavigationComponent } from "@avocado/core/components/bottom-navigation/bottom-navigation.component";
import { SideNavigationComponent } from "@avocado/core/components/side-navigation/side-navigation.component";
import { ScrollDirective } from "@avocado/core/directives/scroll.directive";

const COMPONENTS = [
  ToolbarComponent,
  BottomNavigationComponent,
  SideNavigationComponent,
  ScrollDirective
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule,
    HttpClientModule,
    FontAwesomeModule
  ],
  declarations: COMPONENTS,
  exports: COMPONENTS
})
export class CoreModule {}
