import { identifierModuleUrl } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { MedicalCondition } from 'src/app/models/medicalcondition/medicalcondition.module';
import { Psychologist } from 'src/app/models/psychologist/psychologist.module';
import { MedicalconditionService } from 'src/app/services/medicalcondition/medicalcondition.service';
import { PsychologistService } from 'src/app/services/psychologist/psychologist.service';

@Component({
  selector: 'app-medicalcondition',
  templateUrl: './medicalcondition.component.html',
  styleUrls: ['./medicalcondition.component.css']
})
export class MedicalconditionComponent implements OnInit {

  medicalconditions!: MedicalCondition[];
  image!:string[]
  medical!: MedicalCondition;
  exist :boolean = false;
  listPsychologist!: Psychologist[];
  
  constructor(private medicalconditionService: MedicalconditionService,private  router:Router,
    private alertService: AlertService, private psychologistService : PsychologistService) { }

  ngOnInit(): void {
    this.medicalconditionService.getAll().subscribe(
      (res:any) => {
        this.medicalconditions = res;
      },
      err => this.alertService.warning(err.error())
    );
    this.psychologistService.getAll().subscribe(
      (resp: any) => {
        this.listPsychologist = resp;
        
        if(this.listPsychologist.length > 0){
           this.exist = true;
        }
        else this.alertService.warning("Not exist psychologist for all medical condition");
      
      },
      err => {
        this.alertService.danger(err.error);
      }
     
    );
  }

  public arePsychologistAvailable(idMedical:number){
    this.medicalconditionService.get(idMedical).subscribe(
      (resp: any) => {
        this.medical = resp;
        if(this.medical.expertise.length > 0 || (this.exist && this.medical.id ==8)){
            const params = {
             id: this.medical.id
            };
        this.router.navigate( ['bookpsychologist'], { queryParams: params });
        }
        else this.alertService.warning("Not exist psychologist for this medical condition");
      
      },
      err => {
        this.alertService.danger(err.error);
      }
     
    );
  

  }


}
