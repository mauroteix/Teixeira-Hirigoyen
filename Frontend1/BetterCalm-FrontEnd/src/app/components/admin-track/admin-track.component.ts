import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AlertService } from 'ngx-alerts';
import { Category } from 'src/app/models/category/category.module';
import { CategoryService } from 'src/app/services/category/category.service';

@Component({
  selector: 'app-admin-track',
  templateUrl: './admin-track.component.html',
  styleUrls: ['./admin-track.component.css']
})
export class AdminTrackComponent implements OnInit {

  constructor(private alertService: AlertService, private categoryService : CategoryService) { }

  admin!: boolean;
  nameFunction!: string;
  categoryList!: Category[];

  ngOnInit(): void {
    if(localStorage.getItem("auth_token") != null){
      this.admin = true;
    }  
    else {
      this.admin = false;
      this.alertService.warning("Unauthorized! You must be logged");
    }
    this.categoryService.getAll().subscribe(
      (resp: any) => {
        this.categoryList = resp;
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
  }

  trackForm= new FormGroup({ 
    name:new FormControl(''),
    author :new FormControl(''),
    image: new FormControl(''),
    sound: new FormControl(''),
    hour: new FormControl(''),
    minSeconds: new FormControl(''),
    administrator: new FormControl(''),
  })

  showAddTrack(){
    this.nameFunction = "Add";
  }

  showUpdateTrack(){
    
  }

  showDeleteTrack(){
    
  }

  functionTrack(){

  }

}
