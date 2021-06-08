import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';


export class MedicalCondition{
    constructor( 
        public id: number,
        public name: string,
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
      );
  }
}