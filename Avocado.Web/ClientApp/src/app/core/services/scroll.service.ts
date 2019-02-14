import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { map, shareReplay, startWith } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class ScrollService {
  private scroll$ = new BehaviorSubject<UIEvent>(null);
  private scrollListener$ = new BehaviorSubject<number>(0);

  horizontalScroll$ = this.scroll$.pipe(
    map((event: UIEvent) => (event ? event.srcElement.scrollLeft : 0)),
    shareReplay(1)
  );

  horizontalListener$ = this.scrollListener$.pipe();

  constructor() {}

  registerScrollEvent(event: UIEvent): void {
    this.scroll$.next(event);
  }

  scrollToHorizontal(position: number): void {
    this.scrollListener$.next(position);
  }
}
