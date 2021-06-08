import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';

export class Track{
    constructor( 
        public id: number,
        public name: string,
        public author: string,
        public image: string,
        public hour: number,
        public minseconds: number,
        public sound: string,
        ){}
}


@Injectable({
  providedIn: 'root'
})

export class TrackAdapter implements Adapter<Track> {
  adapt(item: any): Track {
      return new Track (
          item.id,
          item.name,
          item.author,
          item.image,
          item.hour,
          item.minseconds,
          item.sound
      );
  }
}