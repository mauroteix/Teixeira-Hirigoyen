import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { Expertise } from 'src/app/models/expertise/expertise/expertise.module';
import { MedicalCondition } from 'src/app/models/medicalcondition/medicalcondition.module';
import { Meeting } from 'src/app/models/meeting/meeting.module';
import { Psychologist, PsychologistToAdd } from 'src/app/models/psychologist/psychologist.module';
import { MedicalconditionService } from 'src/app/services/medicalcondition/medicalcondition.service';
import { PsychologistService } from 'src/app/services/psychologist/psychologist.service';

@Component({
  selector: 'app-psychologist',
  templateUrl: './psychologist.component.html',
  styleUrls: ['./psychologist.component.css']
})
export class PsychologistComponent implements OnInit {
  admin!: boolean;
  add: boolean = false;
  nameFunction!: string;
  listMedicalCondition!:MedicalCondition[];
  list : Expertise[] = [];
  medicalcondition! : MedicalCondition;
  expertise!: Expertise;
  listMeeting: Meeting[] = [];
  idMedical!: number;
  psychologist!: Psychologist;
 

  psyForm= new FormGroup({ 
    name:new FormControl(''),
    meetingtype :new FormControl(''),
    meetingprice: new FormControl(''),
    adressmeeting: new FormControl(''),
    medical1: new FormControl(''),
    medical2: new FormControl(''),
    medical3: new FormControl(''),

  })
 
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private alertService: AlertService,
    private medicalService: MedicalconditionService,
    private psyService: PsychologistService,
    ){
    }

  ngOnInit(): void {
    if(localStorage.getItem("auth_token") != null){
      this.admin = true;
      this.medicalService.getAll().subscribe(
        (resp: any) => {
          this.listMedicalCondition = resp;
          this.listMedicalCondition.pop();
        },
        err => {
          this.alertService.danger(err.error);
        }
      );
     
    }  
    else {
      this.admin = false;
      this.alertService.warning("Unauthorized! You must be logged");
    }
  }
  showAdd(){
    this.add = true;
    this.nameFunction = "Add";
  }
  validateMedicalCondition() : boolean{
    var valor1 = this.psyForm.value.medical1 == "";
    var valor2 = this.psyForm.value.medical2 == "";
    var valor3 = this.psyForm.value.medical3 == "";

    if(this.psyForm.value.medical1 == "" && this.psyForm.value.medical2 == "" && this.psyForm.value.medical3 == "" ){
      return false;
    }
    return true;
  }

  validatePsychologist() : boolean{
    if(this.psyForm.value.name == "") {
      this.alertService.info("The name can not be empty");
      return false;
    }
    else if(this.psyForm.value.meetingtype == ""){ 
       this.alertService.info("The meeting type can not be empty");
       return false;
      }
    else if(this.psyForm.value.meetingprice == ""){
        this.alertService.info("The meetin price can not be empty");
    }
    else if(this.psyForm.value.adressmeeting == "") {
      this.alertService.info("The adress meeting can not be empty");
      return false;
    }
    else if(!this.validateMedicalCondition()){
      console.log(this.validateMedicalCondition());
      this.alertService.info("There are one or more medical condition repeated");
      return false;
    } 
    return true;
  }
  getMedicalConditionFromId(id:number){
    this.medicalService.get(id).subscribe(
      (resp: any) => {
        this.medicalcondition = resp;
        this.expertise = new Expertise(
          this.medicalcondition.id,
          this.medicalcondition,
          0,
          this.psychologist
        )
        console.log(this.expertise);
        this.list.push(this.expertise);
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
  }
  createListExpertice():Expertise[]{
    if(this.psyForm.value.medical1 != ""){
      this.getMedicalConditionFromId(this.psyForm.value.medical1);
    }
    if(this.psyForm.value.medical2 != ""){
      this.getMedicalConditionFromId(this.psyForm.value.medical2);
    }
    if(this.psyForm.value.medical3 != ""){
      this.getMedicalConditionFromId(this.psyForm.value.medical3);
    }
    return this.list;
  }
  createPsychologist() {
    if(this.validatePsychologist()){
      this.createListExpertice();
      const psy = new PsychologistToAdd(
        this.psyForm.value.name,
        this.psyForm.value.meetingtype,
        this.psyForm.value.meetingprice,
        this.psyForm.value.adressmeeting,
        this.list,
        this.listMeeting
      );
      this.psyService.post(psy)
      .subscribe( resp => {
       this.alertService.success(resp)
      }, (err) => {
        this.alertService.danger(err.error);
      });
    }
  }

}
