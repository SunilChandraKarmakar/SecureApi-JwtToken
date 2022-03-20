import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ResponseStatusModel } from '../models/response-model/response-status-model';
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
  constructor(private _formBuilder: FormBuilder, private _userService: UserService, private _toastr: ToastrService) { }

  registerForm: FormGroup = this._formBuilder.group({
    fullName: ['', Validators.required],
    email: ['', [Validators.email, Validators.required]],
    password: ['', Validators.required]
  });

  ngOnInit() {
  }

  onFormSubmit(): void {
    this.model = this.registerForm.value;

    if(this.model.fullName == '' || this.model.fullName == undefined || this.model.fullName == null) {
      this._toastr.warning('Please, provied full name', 'Full Name');
      return;
    }
    else if(this.model.email == '' || this.model.email == undefined || this.model == null) {
      this._toastr.warning('Please, provied valid email', 'Valid Email');
      return;
    }
    else if(this.model.password == '' || this.model.password == undefined || this.model.password == null) {
      this._toastr.warning('Please, provied pasword', 'Password');
      return;
    }
    else {
      this._userService.registerUser(this.model).subscribe((res) => {
        this._toastr.success('Register Successfull', 'Successfull');
        return;
      },
      (error) => {
        this._toastr.error(error.error.dataSet, 'Error');
        return;
      });
    }
  }
}