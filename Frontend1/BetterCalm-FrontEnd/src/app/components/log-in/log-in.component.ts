import { Component, OnInit } from '@angular/core';
import { LogInService } from 'src/app/services/login/log-in.service';
import { AlertConfig, AlertService } from 'ngx-alerts';
import {Location} from '@angular/common';
import { AdminLoginModel } from 'src/app/models/admin/adminLogic.module';


@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {
  username!: string;
  password!: string;

  constructor(
    private loginService: LogInService,
    private alertService: AlertService,
    private _location: Location
  ) { }

  ngOnInit(): void {
  }

  onLogin(): void {
    this.loginService.login(this.username, this.password).subscribe(
      (res) => {
        this._location.back();
        localStorage.setItem('auth_token', res);
        localStorage.setItem('name', this.username);
        this.alertService.success("Login successfully!");
        this.password = "";
        this.username = "";
        this._location.go("https://localhost:4200");
        location.reload();
      },
      (err) => {
        this.alertService.danger(err.error);
        this.password = "";
        this.username = "";
      }
    );
  }

  signUp(): void {
    this.alertService.warning('This feature will be available in the next version!');
  }

  forgotPassword(): void {
    this.alertService.warning('This feature will be available in the next version!');
  }
}
