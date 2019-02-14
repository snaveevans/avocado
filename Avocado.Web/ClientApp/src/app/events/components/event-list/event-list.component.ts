import { Component, OnInit } from "@angular/core";
import { ScrollService } from "@avocado/core/services/scroll.service";
import { debounceTime, map } from "rxjs/operators";
import { Subject } from "rxjs";

@Component({
  selector: "av-event-list",
  templateUrl: "./event-list.component.html",
  styleUrls: ["./event-list.component.scss"]
})
export class EventListComponent implements OnInit {
  cards = [1, 2, 3, 4, 5, 6];
  selectedIndex = 0;

  private cardPositions: number[] = [];

  constructor(private scrollService: ScrollService) {}

  ngOnInit() {
    const selectedIndex$ = new Subject();
    selectedIndex$
      .pipe(debounceTime(500))
      .subscribe(_ => this.focusCard(this.selectedIndex));
    this.calculateCardPositions();
    this.scrollService.horizontalScroll$
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
    // see flex basis
    // .75 for the 75vw
    // 32 for padding (16px on left and right sides)
    const cardWidth = window.outerWidth * 0.75 + 32;
    // see at padding-left/right calc
    // .125 for the 12.5vw
    // 15 for the -15px
    const edgePadding = window.outerWidth * 0.125 - 15;
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
    this.scrollService.scrollToHorizontal(left);
  }
}
