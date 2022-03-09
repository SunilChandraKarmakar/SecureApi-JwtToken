import { Component, OnInit } from '@angular/core';
import { UpsertUserViewModel } from '../models/user/upsert-user-view-model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  providers: [UserService]
})

export class RegisterComponent implements OnInit {

  model: UpsertUserViewModel = new UpsertUserViewModel();
  constructor(private _userService: UserService) { }

  ngOnInit() {
  }

  onSubmitRegisterForm(): void {
    if(this.model.fullName == '' || this.model.fullName == null || this.model.fullName == undefined) {
      alert("Please, provied Full Name.");
    } 
    else if(this.model.email == '' || this.model.email == null || this.model.email == undefined) {
      alert("Please, provied valid Email");
    }
    else if(this.model.password == '' || this.model.password == null || this.model.password == undefined) {

    }
    else {
      this._userService.registerUser(this.model).subscribe((res) => {
        alert("Registration Successfull");
      },
      (error) => {
        alert(error.error);
      });
    }
  }
}