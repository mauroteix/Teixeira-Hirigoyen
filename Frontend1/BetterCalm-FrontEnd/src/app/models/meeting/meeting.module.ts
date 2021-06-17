import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { Psychologist } from '../psychologist/psychologist.module';
import { User } from '../user/user.module';


export class Meeting{
    constructor( 
        public id: number,
        public idUser: number,
        public user: User,
        public idPsychologist: number,
        public psychologist: Psychologist,
        public date: Date,
        public adressMeeting: string,
        public meetingDuration: number,
        public totalPrice: number,
        public meetingDiscount: number,
        ){}
}


@Injectable({
  providedIn: 'root'
})

export class MeetingAdapter implements Adapter<Meeting> {
  adapt(item: any): Meeting {
      return new Meeting (
          item.id,
          item.idUser,
          item.user,
          item.idPsychologist,
          item.psychologist,
          item.date,
          item.adressMeeting,
          item.meetingDuration,
          item.totalPrice,
          item.meetingDiscount,

      );
  }
}