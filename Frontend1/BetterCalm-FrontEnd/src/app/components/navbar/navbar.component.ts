import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  admin!: boolean;
  username : any;
  constructor() { }

  ngOnInit(): void {
    if(localStorage.getItem("auth_token") != null){
      this.admin = true;
      this.username = localStorage.getItem("name"); 
    }  
    else this.admin = false;
  }

  cerrarSesion(){
    localStorage.removeItem("auth_token");
    location.reload();
}
}
