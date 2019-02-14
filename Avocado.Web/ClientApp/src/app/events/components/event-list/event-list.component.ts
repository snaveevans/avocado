import { Component, OnInit } from "@angular/core";
import { BehaviorSubject, Subject } from "rxjs";
import { debounceTime, map } from "rxjs/operators";

@Component({
  selector: "av-event-list",
  templateUrl: "./event-list.component.html",
  styleUrls: ["./event-list.component.scss"]
})
export class EventListComponent implements OnInit {
  cards = [1, 2, 3, 4, 5, 6];
  selectedIndex = 0;
  horizontalScrollPosition = 0;

  private cardPositions: number[] = [];
  private horizontalScroll$ = new BehaviorSubject<number>(0);

  ngOnInit() {
    const selectedIndex$ = new Subject();

    // after scroll finishes center on card
    selectedIndex$
      .pipe(debounceTime(500))
      .subscribe(_ => this.focusCard(this.selectedIndex));

    // calculate card positions ahead of time so determining selected card is quick
    this.calculateCardPositions();

    // while scrolling determine selected card
    this.horizontalScroll$
      .pipe(
        debounceTime(10),
        map((leftScroll: number) => leftScroll + window.outerWidth / 2)
      )
      .subscribe((adjustedScroll: number) => {
        const index = this.cardPositions.findIndex(pos => pos > adjustedScroll);
        this.selectedIndex = index;
        selectedIndex$.next();
      });
  }

  calculateCardPositions(): void {
    const cardWidth = window.outerWidth * 0.8;
    const edgePadding = window.outerWidth * 0.1;
    this.cardPositions = this.cards.map(
      (_value: number, index: number, arr: number[]) => {
        const first = cardWidth + edgePadding;
        if (index === 0) {
          // first
          return first;
        }
        const middle = cardWidth * index;
        if (index === arr.length - 1) {
          // last
          return middle + first + edgePadding;
        }
        // middle
        return middle + first;
      }
    );
  }

  handleScroll(event: UIEvent): void {
    this.horizontalScroll$.next(event.srcElement.scrollLeft);
  }

  handleClick(index: number): void {
    if (this.selectedIndex !== index) {
      this.focusCard(index);
      return;
    }

    console.log("show card details");
  }

  focusCard(index: number): void {
    const previousPosition = index === 0 ? 0 : this.cardPositions[index - 1];
    const elementPosition = this.cardPositions[index];
    const center = (previousPosition + elementPosition) / 2;
    const left = center - window.outerWidth / 2;
    this.horizontalScrollPosition = left;
  }
}
