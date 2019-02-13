import { Directive, Output, EventEmitter, HostListener } from "@angular/core";

@Directive({
  selector: "[avScroll]"
})
export class ScrollDirective {
  @Output()
  avScroll = new EventEmitter<UIEvent>();

  private ticking = false;

  constructor() {
    console.log("constructor");
  }

  @HostListener("scroll", ["$event"])
  onListenerTriggered(event: UIEvent): void {
    if (!this.ticking) {
      window.requestAnimationFrame(_ => {
        this.avScroll.emit(event);
        this.ticking = false;
      });
      this.ticking = true;
    }
  }
}
