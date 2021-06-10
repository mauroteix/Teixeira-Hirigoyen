import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { Expertise } from '../expertise/expertise/expertise.module';


export class MedicalCondition{
    constructor( 
        public id: number,
        public name: string,
        public expertise: Expertise[]
        ){}
}


@Injectable({
  providedIn: 'root'
})

export class MedicalConditionAdapter implements Adapter<MedicalCondition> {
  adapt(item: any): MedicalCondition {
      return new MedicalCondition (
          item.id,
          item.name,
          item.expertise
      );
  }
}