import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UserEditViewModel } from 'src/app/models/user/user-edit-view-model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})

export class EditUserComponent implements OnInit {

  model: UserEditViewModel = new UserEditViewModel();
  private _existUserId: string | undefined;

  constructor(private _userService: UserService, private _route: ActivatedRoute, private _toastr: ToastrService, private _spinnerService: NgxSpinnerService, private _router: Router) { }

  ngOnInit() {
    this.checkExistUserId();
    this.getExistUser();    
  }

  private checkExistUserId(): void {
    this._spinnerService.show();
    this._route.params.subscribe(params => {
      this._existUserId = params['recordId']; 
      this._spinnerService.hide();
    });
  }

  private getExistUser(): void {
    this._spinnerService.show();
    this._userService.get(this._existUserId).subscribe((res: any) => {
      this._spinnerService.hide();
      this.model = res;
    },
    (error) => {
      this._spinnerService.hide();
      this._toastr.error(error, 'Error');
    })
  }

  editUser(): void {
    this._spinnerService.show();
    this._userService.put(this.model.id, this.model).subscribe((res) => {
      this._spinnerService.hide();
      this._toastr.success('User update successfull.', 'Successfull');
      return this._router.navigate(['user-managemet']);
    },
    (error) => {
      this._spinnerService.hide();
      this._toastr.error(error.error.dataSet == null ? error.error.responseMessage : error.error.dataSet, 'Error');
    })
  }
}