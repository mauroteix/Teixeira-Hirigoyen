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
    showSelect: boolean = false;
    showDelete: boolean = false;
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
    this.showSelect = false;
    this.showDelete = false;
  }

  showUpdateAdmin(){
    this.showAdmin = true;
    this.nameFunction = "Update";
    this.showSelect = true;
    this.showDelete = false;
  }

  showDeleteAdmin(){
    this.showAdmin = false;
    this.nameFunction = "Delete";
    this.showDelete = true;
    this.showSelect = true;
  }

  cleanForm(){
    this.adminForm.patchValue({
      name: '',
      email: '',
      password: '',
      administrator:''
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

  validateSelect(): boolean{
    if(this.adminForm.value.administrator == "") return false;
    return true;
  }
  functionAdd(){
    const admin = new AdministratorToAdd(
      this.adminForm.value.name,
      this.adminForm.value.email,
      this.adminForm.value.password,
      );
      this.adminService.post(admin)
      .subscribe( resp => {
      this.alertService.success(resp);
      this.ngOnInit();
      }, (err) => {
      this.alertService.danger(err.error);
      });
      this.cleanForm();
  }

  functionUpdate(){
    const admin = new AdministratorToAdd(
      this.adminForm.value.name,
      this.adminForm.value.email,
      this.adminForm.value.password,
       );
      this.adminService.put(admin,this.adminForm.value.administrator) 
      .subscribe( resp => {
      this.alertService.success(resp);
      this.cleanForm();
      this.ngOnInit();
      }, (err) => {
      this.alertService.danger(err.error);
      });
      
  }

  functionDelete(){
    console.log(this.adminForm.value.administrator);
    this.adminService.delete(this.adminForm.value.administrator) 
    .subscribe( resp => {
    this.alertService.success(resp);
    this.ngOnInit();
    }, (err) => {
    this.alertService.danger(err.error);
    });
    
  }

  functionAdmin(){
    if(this.showAdmin && this.validateAdmin()){
      if(this.nameFunction == "Add"){
          this.functionAdd();
      }
      if(this.nameFunction == "Update"){
        if(this.validateSelect()){
          this.functionUpdate();
        }
        else this.alertService.info("You need to select a administrator");
      }
    }
      if(this.nameFunction == "Delete"){
        if(this.validateSelect()){
          this.functionDelete();
        }
        else this.alertService.info("You need to select a administrator");
      }
    
 
  }
}
