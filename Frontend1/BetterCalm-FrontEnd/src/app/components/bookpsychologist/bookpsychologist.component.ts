import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { MedicalCondition } from 'src/app/models/medicalcondition/medicalcondition.module';
import { Meeting } from 'src/app/models/meeting/meeting.module';
import { UserToAdd } from 'src/app/models/user/user.module';
import { MedicalconditionService } from 'src/app/services/medicalcondition/medicalcondition.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-bookpsychologist',
  templateUrl: './bookpsychologist.component.html',
  styleUrls: ['./bookpsychologist.component.css']
})
export class BookpsychologistComponent implements OnInit {
  userForm= new FormGroup({ 
    name:new FormControl(''),
    surname :new FormControl(''),
    birthday :new FormControl('2000-01-01'),
    email :new FormControl(''),
    cellphone :new FormControl(''),
    meetingduration :new FormControl(''),
  })
  public play!:any;
  medicalCondition!: MedicalCondition;
  discount!:number;
  meetingcount!:number;
  meeting:Meeting[] = [];

  
  constructor(private route: ActivatedRoute,
    private router: Router,private alertService: AlertService, private medicalService: MedicalconditionService, private userService: UserService) { 
    this.route.queryParams.subscribe( params => {
    this.play = +params['id'];
    });
  }
  ngOnInit(): void {
    this.medicalService.get(this.play).subscribe(
      (resp: any) => {
        this.medicalCondition = resp
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
  }

  validateUser() : boolean{
    if(this.userForm.value.name == "") {
      this.alertService.info("The name can not be empty");
      return false;
    }
    else if(this.userForm.value.surname == ""){ 
       this.alertService.info("The surname can not be empty");
       return false;
      }
    else if(this.userForm.value.email == ""){
        this.alertService.info("The email can not be empty");
    }
    else if(this.userForm.value.cellphone == "") {
      this.alertService.info("The cellphone can not be empty");
      return false;
    }
    else if(this.userForm.value.meetingduration == ""){
      this.alertService.info("The duration can not be empty");
      return false;
    } 
    return true;
  }
  createMeeting() {
    if(this.validateUser()){
      const user = new UserToAdd(
        this.userForm.value.name,
        this.userForm.value.surname,
        this.userForm.value.birthday,
        this.userForm.value.email,
        this.userForm.value.cellphone,
        this.medicalCondition,
        this.userForm.value.meetingduration,
        this.discount = 100,
        this.meetingcount= 0,
        this.meeting,
      );
      this.userService.post(user)
      .subscribe( resp => {   
       this.alertService.success(resp);
      }, (err) => {
        this.alertService.danger(err.error);
      });



    }
  
   
  }

}

