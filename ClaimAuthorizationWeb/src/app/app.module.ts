import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './register/register.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { UserManagementComponent } from './user-management/user-management.component';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [			
    AppComponent,
    LoginComponent,
    RegisterComponent,
    UserManagementComponent
   ],
  
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule  
  ],

  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }
