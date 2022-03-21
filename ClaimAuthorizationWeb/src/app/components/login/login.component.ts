import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { LoginViewModel } from '../../models/login/login-view-model';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  
  model: LoginViewModel = new LoginViewModel();
  constructor(private _formBuilder: FormBuilder,  private _userService: UserService, private _toastr: ToastrService, private _spinnerService: NgxSpinnerService, private _router: Router) { }

  loginForm: FormGroup = this._formBuilder.group({
    email: ['', [Validators.email, Validators.required]],
    password: ['', Validators.required]
  });

  ngOnInit() {
  }

  onFormSubmit(): void {
    this.model = this.loginForm.value;

    if(this.model.email == '' || this.model.email == undefined || this.model.email == null) {
      this._toastr.warning('Please, provied valid email', 'Email');
      return;
    }
    else if(this.model.password == '' || this.model.password == undefined || this.model.password == null) {
      this._toastr.warning('Please, provied password', 'Password');
      return;
    }
    else {
      this._spinnerService.show();
      this._userService.login(this.model).subscribe((res: any) => {
        this._spinnerService.hide();
        localStorage.setItem('loginUserInfo', JSON.stringify(res));
        this._toastr.success('Login Successfull', 'Successfull');
        return this._router.navigate(['/user-managemet']);
      },
      (error) => {
        this._spinnerService.hide();
        this._toastr.error(error.error.responseMessage, 'Error');
        return;
      });
    }
  }
}