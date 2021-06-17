import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';
import { Category } from '../category/category.module';
import { Video } from '../video/video.module';

export class CategoryVideo{
    constructor( 
        public idCategory: number,
        public category: Category, 
        public idVideo: number,
        public video: Video
        ){}
}

export class CategoryVideoToAdd{
  constructor( 
      public idCategory: number,
      ){}
}


@Injectable({
  providedIn: 'root'
})

export class CategoryVideoAdapter implements Adapter<CategoryVideo> {
  adapt(item: any): CategoryVideo {
      return new CategoryVideo (
          item.idCategory,
          item.category,
          item.idVideo,
          item.video,
      );
  }
}