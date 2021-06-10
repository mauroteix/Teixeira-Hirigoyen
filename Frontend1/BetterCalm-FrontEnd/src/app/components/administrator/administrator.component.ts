import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { Administrator, AdministratorToAdd } from 'src/app/models/admin/admin.module';
import { AdminService } from 'src/app/services/admin/admin.service';

@Component({
  selector: 'app-administrator',
  templateUrl: './administrator.component.html',
  styleUrls: ['./administrator.component.css']
})
export class AdministratorComponent implements OnInit {

  admin!: boolean;

  constructor(private route: ActivatedRoute,
    private router: Router,private alertService: AlertService, private adminService: AdminService) { }

    showAdmin: boolean = false;
    showUpdate: boolean = false;
    nameFunction!: string;
    adminList!: Administrator[];
    

    adminForm= new FormGroup({ 
      name:new FormControl(''),
      email :new FormControl(''),
      password: new FormControl(''),
      administrator: new FormControl(''),
    })

  ngOnInit(): void {
    if(localStorage.getItem("auth_token") != null){
      this.admin = true;
    }  
    else {
      this.admin = false;
      this.alertService.warning("Unauthorized! You must be logged");
    }
    this.adminService.getAll().subscribe(
      (resp: any) => {
        this.adminList = resp
      },
      err => {
        this.alertService.danger(err.error);
      }
    );

  }

  showAddAdmin(){
    this.showAdmin = true;
    this.nameFunction = "Add";
    this.showUpdate = false;
  }

  showUpdateAdmin(){
    this.showAdmin = true;
    this.nameFunction = "Update";
    this.showUpdate = true;
  }

  showDeleteAdmin(){
    this.showAdmin = false;
    this.nameFunction = "Delete";
  }

  cleanForm(){
    this.adminForm.patchValue({
      name: '',
      email: '',
      password: ''
   });
  }

  validateAdmin() : boolean{
    if(this.adminForm.value.name == "") {
      this.alertService.info("The name can not be empty");
      return false;
    }
    else if(this.adminForm.value.email == ""){ 
       this.alertService.info("The email can not be empty");
       return false;
      }
    else if(this.adminForm.value.password == ""){
        this.alertService.info("The password can not be empty");
        return false;
    }
    return true;
  }

  functionAdmin(){
    if(this.validateAdmin()){
      if(this.nameFunction == "Add"){
      const admin = new AdministratorToAdd(
      this.adminForm.value.name,
      this.adminForm.value.email,
      this.adminForm.value.password,
      );
      this.adminService.post(admin)
      .subscribe( resp => {
      this.alertService.success(resp)
      }, (err) => {
      this.alertService.danger(err.error);
      });
      this.cleanForm();
    }
      if(this.nameFunction == "Update"){
      const admin = new AdministratorToAdd(
      this.adminForm.value.name,
      this.adminForm.value.email,
      this.adminForm.value.password,
      );
      this.adminService.put(admin,6) //harckodeadoo!!!! FALTA TRAER EL ID DE UN ADMIN
      .subscribe( resp => {
      this.alertService.success(resp)
      }, (err) => {
      this.alertService.danger(err.error);
      });
      this.cleanForm();
    }
    
  }
  }
}
