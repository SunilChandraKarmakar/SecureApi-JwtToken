import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UserViewModel } from '../models/user/user-view-model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss']
})

export class UserManagementComponent implements OnInit {

  dataSource: UserViewModel[] = [];
  constructor(private _userService: UserService, private _spinnerService: NgxSpinnerService, private _toastr: ToastrService) { }

  ngOnInit() {
    this.getAllUsers();
  }

  private getAllUsers(): void {
    this._spinnerService.show();
    this._userService.getUsers().subscribe((res) => {
      this.dataSource = res;
      this._spinnerService.hide();
      return;
    },
    (error) => {
      this._spinnerService.hide();
      this._toastr.error(error, 'Error');
      return
    })
  }
}