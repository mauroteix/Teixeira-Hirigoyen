import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';

export class Video{
    constructor( 
        public id: number,
        public name: string,
        public author: string,
        public hour: number,
        public minseconds: number,
        public linkVideo: string,
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
          item.linkVideo
      );
  }
}