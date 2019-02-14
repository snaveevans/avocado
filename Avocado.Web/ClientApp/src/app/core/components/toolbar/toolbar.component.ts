import { Component, Output, EventEmitter } from "@angular/core";

@Component({
  selector: "av-toolbar",
  templateUrl: "./toolbar.component.html",
  styleUrls: ["./toolbar.component.scss"]
})
export class ToolbarComponent {
  @Output()
  menuClick: EventEmitter<void> = new EventEmitter();

  handleMenuClick(): void {
    this.menuClick.emit();
  }
}
