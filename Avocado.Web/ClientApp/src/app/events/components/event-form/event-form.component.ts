import { Component, OnInit } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";

@Component({
  selector: "av-event-form",
  templateUrl: "./event-form.component.html",
  styleUrls: ["./event-form.component.scss"]
})
export class EventFormComponent implements OnInit {
  eventForm = this.formBuilder.group({
    name: ["", Validators.required],
    description: ["", Validators.required]
  });

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {}

  handleSubmit(): void {
    // save the form
    console.log(`submitted!  isValid: ${this.eventForm.valid}`);
  }
}
