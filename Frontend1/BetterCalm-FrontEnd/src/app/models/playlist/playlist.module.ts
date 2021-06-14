import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { PlaylistCategory, PlaylistCategoryToAdd } from '../playlistcategory/playlistcategory.module';
import { PlaylistTrack, PlaylistTrackToAdd } from '../playlisttrack/playlisttrack.module';
import { PlaylistVideo, PlaylistVideoToAdd } from '../playlistvideo/playlistvideo.module';

export class Playlist{
    constructor( 
        public id: number,
        public name: string,
        public description: string,
        public image: string,
        public playlistCategory: PlaylistCategory[],
        public playlistTrack: PlaylistTrack[],
        public playlistVideo: PlaylistVideo[],
      
        ){}
}

export class PlaylistToAdd{
  constructor( 
      public name: string,
      public description: string,
      public image: string,
      public playlistCategory: PlaylistCategoryToAdd[],
      public playlistTrack: PlaylistTrackToAdd[],
      public playlistVideo: PlaylistVideoToAdd[],
    
      ){}
}


@Injectable({
  providedIn: 'root'
})

export class PlaylistAdapter implements Adapter<Playlist> {
  adapt(item: any): Playlist{
      return new Playlist (
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