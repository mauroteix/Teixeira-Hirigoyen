import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { MedicalCondition } from '../medicalcondition/medicalcondition.module';
import { Meeting } from '../meeting/meeting.module';

export class User{
    constructor( 
        public id: number,
        public name: string,
        public surname: string,
        public birthday: Date,
        public email: string,
        public cellphone: string,
        public meeting:Meeting[],
        public medicalCondition: MedicalCondition,
        public meetingduration:number,
        public discount:number,
        public meetingcount:number
        ){}
}
export class UserToAdd{
  constructor( 
      public name: string,
      public surname: string,
      public birthday: Date,
      public email: string,
      public cellphone: string,
      public medicalCondition: MedicalCondition,
      public meetingduration:number,
      public discount:number,
      public meetingcount:number,
      public meeting:Meeting[],
      ){}
}

@Injectable({
  providedIn: 'root'
})

export class UserAdapter implements Adapter<User> {
  adapt(item: any): User {
      return new User (
          item.id,
          item.name,
          item.surname,
          item.birthday,
          item.email,
          item.cellphone,
          item.meeting,
          item.medicalCondition,
          item.meetingduration,
          item.discount,
          item.meetingcount,
      );
  }
}