import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { ExpertiseToAdd } from 'src/app/models/expertise/expertise/expertise.module';
import { MedicalCondition } from 'src/app/models/medicalcondition/medicalcondition.module';
import { Meeting } from 'src/app/models/meeting/meeting.module';
import { Psychologist, PsychologistToAdd } from 'src/app/models/psychologist/psychologist.module';
import { MedicalconditionService } from 'src/app/services/medicalcondition/medicalcondition.service';
import { PsychologistService } from 'src/app/services/psychologist/psychologist.service';
import {Location} from '@angular/common';

@Component({
  selector: 'app-psychologist',
  templateUrl: './psychologist.component.html',
  styleUrls: ['./psychologist.component.css']
})
export class PsychologistComponent implements OnInit {
  admin!: boolean;
  add: boolean = false;
  select: boolean = false;
  nameFunction!: string;
  listMedicalCondition!:MedicalCondition[];
  list : ExpertiseToAdd[] = [];
  medicalcondition! : MedicalCondition;
  expertisetoadd1!: ExpertiseToAdd;
  listMeeting: Meeting[] = [];
  idMedical!: number;
  psychologist!: Psychologist;
  psyList!:Psychologist[];
  
 

  psyForm= new FormGroup({ 
    name:new FormControl(''),
    meetingtype :new FormControl(''),
    meetingprice: new FormControl(''),
    adressmeeting: new FormControl(''),
    medical1: new FormControl(''),
    medical2: new FormControl(''),
    medical3: new FormControl(''),
    psychologist: new FormControl(''),

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
      this.psyService.getAll().subscribe(
        (resp: any) => {
          this.psyList = resp
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
    this.select = false;
  }
  showUpdate(){
    this.add = true;
    this.nameFunction = "Update";
    this.select = true;
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
  validateSelectUpdate(): boolean{
    if(this.psyForm.value.psychologist == "") return false;
    return true;
  }
  functionPsychologist(){
    if(this.validatePsychologist()){
      this.createExpertise();
      if(this.nameFunction == "Add"){
        this.createPsychologist();  
      }
      if(this.nameFunction == "Update"){
        if(this.validateSelectUpdate()){
          this.updatePsychologist();
        }  
        
      }
    }
  
  }
  createExpertise(){
    if(this.psyForm.value.medical1 != ""){
      this.expertisetoadd1 = new ExpertiseToAdd(this.psyForm.value.medical1);
      this.list.push(this.expertisetoadd1);
    }
    if(this.psyForm.value.medical2 != ""){
      this.expertisetoadd1 = new ExpertiseToAdd(this.psyForm.value.medical2);
      this.list.push(this.expertisetoadd1);
    }
    if(this.psyForm.value.medical3 != ""){
      this.expertisetoadd1 = new ExpertiseToAdd(this.psyForm.value.medical3);
      this.list.push(this.expertisetoadd1);
    }
  }
  updatePsychologist(){
    const psy = new PsychologistToAdd(
      this.psyForm.value.name,
      this.psyForm.value.meetingtype,
      this.psyForm.value.meetingprice,
      this.psyForm.value.adressmeeting,
      this.list,
      this.listMeeting
    );
    this.psyService.put(psy,this.psyForm.value.psychologist) .subscribe( resp => {
        this.alertService.success(resp);
        this.ngOnInit()
        this.cleanForm();
      }, (err) => {
        this.alertService.danger(err.error);
        this.cleanForm();
      });
  }
  createPsychologist() {
    const psy = new PsychologistToAdd(
      this.psyForm.value.name,
      this.psyForm.value.meetingtype,
      this.psyForm.value.meetingprice,
      this.psyForm.value.adressmeeting,
      this.list,
      this.listMeeting
    );
    this.psyService.post(psy).subscribe( resp => {
      this.cleanForm();
      this.alertService.success(resp)
    },(err) => {
      this.cleanForm();
      this.alertService.danger(err.error);
    });
    
  }
  cleanForm(){
    this.list= [];
    this.psyList= [];
    this.listMedicalCondition= [];
    this.psyForm.patchValue({
      name:'',
      meetingtype: '',
      meetingprice:'',
      adressmeeting:'',
      medical1:'',
      medical2:'',
      medical3: '',
      psychologist: '',
    });

  }

}


