import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { CategoryVideo, CategoryVideoToAdd } from '../categoryvideo/categoryvideo.module';
import { PlaylistVideo, PlaylistVideoToAdd } from '../playlistvideo/playlistvideo.module';

export class Video{
    constructor( 
        public id: number,
        public name: string,
        public author: string,
        public hour: number,
        public minseconds: number,
        public linkVideo: string,
        public categoryVideo: CategoryVideo[],
        public playlistVideo: PlaylistVideo[],
        ){}
}

export class VideoToAdd{
  constructor( 
      public name: string,
      public author: string,
      public hour: number,
      public minSeconds:number,
      public linkVideo: string,
      public categoryVideo: CategoryVideoToAdd[],
      public playlistVideo: PlaylistVideoToAdd[],

      ){}
}

@Injectable({
  providedIn: 'root'
})

export class VideoAdapter implements Adapter<Video> {
  adapt(item: any): Video {
      return new Video (
          item.id,
          item.name,
          item.author,
          item.hour,
          item.minseconds,
          item.linkVideo,
          item.categoryVideo,
          item.playlistVideo,
      );
  }
}