import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { Playlist } from '../playlist/playlist.module';
import { Video } from '../video/video.module';


export class PlaylistVideo{
    constructor( 
        public idVideo: number,
        public video: Video, 
        public idPlaylist: number,
        public playlist: Playlist
        ){}
}


@Injectable({
  providedIn: 'root'
})

export class PlaylistVideoAdapter implements Adapter<PlaylistVideo> {
  adapt(item: any): PlaylistVideo {
      return new PlaylistVideo (
          item.idVideo,
          item.video,
          item.idPlaylist,
          item.playlist,
      );
  }
}