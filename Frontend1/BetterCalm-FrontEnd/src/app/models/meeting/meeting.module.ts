import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { User } from '../user/user.module';


export class Meeting{
    constructor( 
        public id: number,
        public idUser: number,
        public user: User,

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

      );
  }
}