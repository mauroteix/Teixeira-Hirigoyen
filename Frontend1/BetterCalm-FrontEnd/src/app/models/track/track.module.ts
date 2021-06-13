import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { CategoryTrack, CategoryTrackToAdd } from '../categorytrack/categorytrack.module';
import { PlaylistTrack, PlaylistTrackToAdd } from '../playlisttrack/playlisttrack.module';

export class Track{
    constructor( 
        public id: number,
        public name: string,
        public author: string,
        public image: string,
        public hour: number,
        public minSeconds:number,
        public sound: string,
        public categoryTrack: CategoryTrack[],
        public playlistTrack: PlaylistTrack[],

        ){}
}

export class TrackToAdd{
  constructor( 
      public name: string,
      public author: string,
      public image: string,
      public hour: number,
      public minSeconds:number,
      public sound: string,
      public categoryTrack: CategoryTrackToAdd[],
      public playlistTrack: PlaylistTrackToAdd[],

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
          item.minSeconds,
          item.sound,
          item.categoryTrack,
          item.playlistTrack
      );
  }
}