import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { CategoryTrack } from '../categorytrack/categorytrack.module';
import { CategoryVideo } from '../categoryvideo/categoryvideo.module';
import { PlaylistCategory } from '../playlistcategory/playlistcategory.module';

export class Category{
    constructor( 
        public id: number,
        public name: string,
        public categoryTrack : CategoryTrack[],
        public playlistCategory: PlaylistCategory[],
        public categoryVideo : CategoryVideo[], 
        public image: string,
        ){}
}


@Injectable({
  providedIn: 'root'
})

export class CategoryAdapter implements Adapter<Category> {
  adapt(item: any): Category {
      return new Category (
          item.id,
          item.name,
          item.categoryTrack,
          item.playlistCategory,
          item.categoryVideo,
          item.image
      );
  }
}