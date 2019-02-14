import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { BottomNavigationComponent } from "@avocado/core/components/bottom-navigation/bottom-navigation.component";
import { SideNavigationComponent } from "@avocado/core/components/side-navigation/side-navigation.component";
import { ToolbarComponent } from "@avocado/core/components/toolbar/toolbar.component";
import { ScrollDirective } from "@avocado/core/directives/scroll.directive";
import { MaterialModule } from "@avocado/material/material.module";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";

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
