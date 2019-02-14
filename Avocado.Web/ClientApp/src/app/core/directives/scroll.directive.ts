import {
  Directive,
  ElementRef,
  EventEmitter,
  HostListener,
  Input,
  OnChanges,
  Output,
  SimpleChanges
} from "@angular/core";

@Directive({
  selector: "[avScroll]"
})
export class ScrollDirective implements OnChanges {
  @Output()
  avScroll = new EventEmitter<UIEvent>();
  @Input()
  avScrollPosition?: number;

  private ticking = false;
  private readonly element: HTMLElement;

  constructor(el: ElementRef) {
    this.element = el.nativeElement as HTMLElement;
  }

  ngOnChanges(changes: SimpleChanges): void {
    const positionChange = changes["avScrollPosition"];
    if (!positionChange) {
      return;
    }

    this.updateScrollPosition();
  }

  private updateScrollPosition(): void {
    this.element.scroll(this.avScrollPosition || 0, 0);
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
