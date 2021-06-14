import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { Category } from '../category/category.module';
import { Playlist } from '../playlist/playlist.module';


export class PlaylistCategory{
    constructor( 
        public idCategory: number,
        public category: Category, 
        public idPlaylist: number,
        public playlist: Playlist
        ){}
}

export class PlaylistCategoryToAdd{
  constructor( 
      public idCategory: number,
      ){}
}


@Injectable({
  providedIn: 'root'
})

export class PlaylistCategoryAdapter implements Adapter<PlaylistCategory> {
  adapt(item: any): PlaylistCategory {
      return new PlaylistCategory (
          item.idCategory,
          item.category,
          item.idPlaylist,
          item.playlist,
      );
  }
}