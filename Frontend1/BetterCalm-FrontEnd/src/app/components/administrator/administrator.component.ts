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
    showSelectDelete: boolean = false;
    nameFunction!: string;
    adminList!: Administrator[];
    username: any;
    adminLogged!: Administrator;
    adminDeleteList: Administrator[] = [];
    

    adminForm= new FormGroup({ 
      name:new FormControl(''),
      email :new FormControl(''),
      password: new FormControl(''),
      administrator: new FormControl(''),
      administratorDelete: new FormControl(''),
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
        this.adminList = resp;
        if(this.showDelete){
          this.adminDeleteList = [];
          this.adminList.forEach(a => {
            if(a.email != this.username) {
              this.adminDeleteList.push(a);
            }
          })
        }
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
    this.username = localStorage.getItem("name");
   
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
    this.cleanForm();
    this.ngOnInit();
    this.showDelete = true;
    this.showSelect = false;
    this.showAdmin = false;
    this.showSelectDelete = true;
    this.nameFunction = "Delete";
    
  }

  cleanForm(){
    this.adminList = [];
    this.adminDeleteList=[];
    this.adminForm.patchValue({
      name: '',
      email: '',
      password: '',
      administrator:'',
      administratorDelete:'',
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

  validateSelect(admin: any): boolean{
    if(admin == "") return false;
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
      //location.reload();
      }, (err) => {
      this.alertService.danger(err.error);
      });
      
  }

  functionDelete(){
    this.adminService.delete(this.adminForm.value.administratorDelete) 
    .subscribe( resp => {
    this.alertService.success(resp);
    this.ngOnInit();
    this.cleanForm();
    //location.reload();
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
        if(this.validateSelect(this.adminForm.value.administrator)){
          this.functionUpdate();
        }
        else this.alertService.info("You need to select a administrator");
      }
    }
      if(this.nameFunction == "Delete"){
        if(this.validateSelect(this.adminForm.value.administratorDelete)){
          this.functionDelete();
        }
        else this.alertService.info("You need to select a administrator");
      }
    
 
  }
}
