import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { Category } from '../category/category.module';
import { Track } from '../track/track.module';

export class CategoryTrack{
    constructor( 
        public idCategory: number,
        public category: Category, 
        public idTrack: number,
        public track: Track
        ){}
}


@Injectable({
  providedIn: 'root'
})

export class CategoryTrackAdapter implements Adapter<CategoryTrack> {
  adapt(item: any): CategoryTrack {
      return new CategoryTrack (
          item.idCategory,
          item.category,
          item.idTrack,
          item.track,
      );
  }
}