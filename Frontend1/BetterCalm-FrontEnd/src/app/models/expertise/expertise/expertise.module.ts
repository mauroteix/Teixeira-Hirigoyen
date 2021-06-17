import { Injectable } from '@angular/core';
import { Adapter } from '../../adapter/adapter.module';
import { MedicalCondition } from '../../medicalcondition/medicalcondition.module';
import { Psychologist } from '../../psychologist/psychologist.module';


export class Expertise{
    constructor( 
        public idMedicalCondition: number,
        public medicalCondition: MedicalCondition, 
        public idPsychologist: number,
        public psychologist: Psychologist
        ){}
}
export class ExpertiseToAdd{
  constructor( 
      public idMedicalCondition: number,
      ){}
}


@Injectable({
  providedIn: 'root'
})

export class ExpertiseAdapter implements Adapter<Expertise> {
  adapt(item: any): Expertise {
      return new Expertise (
          item.idMedicalCondition,
          item.medicalCondition,
          item.idPsychologist,
          item.psychologist,
      );
  }
}