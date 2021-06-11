import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { Expertise, ExpertiseToAdd } from '../expertise/expertise/expertise.module';
import { Meeting } from '../meeting/meeting.module';


export class Psychologist{
    constructor( 
        public id: number,
        public name: string,
        public meetingType: number,
        public meetingPrice: number,
        public adressMeeting: string,
        public expertise: Expertise[],
        public meeting: Meeting[],
      
        ){}
}

export class PsychologistToAdd{
  constructor( 
      public name: string,
      public meetingType: number,
      public meetingPrice: number,
      public adressMeeting: string,
      public expertise: ExpertiseToAdd[],
      public meeting: Meeting[],   
      ){}
}


@Injectable({
  providedIn: 'root'
})

export class PsychologistAdapter implements Adapter<Psychologist> {
  adapt(item: any): Psychologist{
      return new Psychologist (
          item.id,
          item.name,
          item.description,
          item.image,
          item.playlistCategory,
          item.playlistTrack,
          item.playlistVideo,
      );
  }
}
