import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { Playlist } from '../playlist/playlist.module';
import { Track } from '../track/track.module';


export class PlaylistTrack{
    constructor( 
        public idTrack: number,
        public track: Track, 
        public idPlaylist: number,
        public playlist: Playlist
        ){}
}


@Injectable({
  providedIn: 'root'
})

export class PlaylistTrackAdapter implements Adapter<PlaylistTrack> {
  adapt(item: any): PlaylistTrack {
      return new PlaylistTrack (
          item.idTrack,
          item.track,
          item.idPlaylist,
          item.playlist,
      );
  }
}