import { Component, OnInit } from '@angular/core';
import {Location} from '@angular/common';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  admin!: boolean;
  username : any;
  constructor( private _location: Location) { }

  ngOnInit(): void {
    if(localStorage.getItem("auth_token") != null){
      this.admin = true;
      this.username = localStorage.getItem("name"); 
    }  
    else this.admin = false;
  }

  cerrarSesion(){
    localStorage.removeItem("auth_token");
    localStorage.removeItem("name");
    this._location.go("https://localhost:4200");
    location.reload();
}
}
