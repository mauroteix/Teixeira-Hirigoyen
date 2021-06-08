import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { PlaylistCategory } from '../playlistcategory/playlistcategory.module';

export class Playlist{
    constructor( 
        public id: number,
        public name: string,
        public description: string,
        public image: string,
        public playlistCategory: PlaylistCategory[],
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
          item.playlistCategory
      );
  }
}