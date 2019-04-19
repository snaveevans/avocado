import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { RegisterForm } from "@avocado/auth/models/RegisterForm";

@Component({
  selector: "av-register-form",
  templateUrl: "./register-form.component.html",
  styleUrls: ["./register-form.component.scss"]
})
export class RegisterFormComponent implements OnInit {
  @Output()
  register = new EventEmitter<RegisterForm>();
  @Output()
  cancel = new EventEmitter();
  registrationForm = this.formBuilder.group({
    name: ["", [Validators.required]],
    userName: ["", [Validators.required]]
  });

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {}

  handleSubmit(): void {
    if (!this.registrationForm.valid) {
      return;
    }
    const name = this.registrationForm.get("name").value;
    const userName = this.registrationForm.get("userName").value;
    const registerForm: RegisterForm = {
      name,
      userName
    };
    this.register.emit(registerForm);
  }
}
