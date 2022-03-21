import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { UserViewModel } from '../../models/user/user-view-model';
import { UserService } from '../../services/user.service';

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

  deleteUser(id: string): void {
    if(id == null || id == undefined || id == '') {
      this._toastr.warning('User id can not found! Try again.', 'Not Found');
    }
    else {
      this._spinnerService.show();
      this._userService.delete(id).subscribe((res) => {
        this._spinnerService.hide();
        this._toastr.success('Selected user delete successfull.', 'Successfull');
        this.ngOnInit();
      },
      (error) => {
        this._spinnerService.hide();
        this._toastr.error(error, 'Error');
      })
    }
  }
}