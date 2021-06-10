import { Injectable } from '@angular/core';
import { Adapter } from '../adapter/adapter.module';


export class Administrator{
    constructor( 
        public id: number,
        public name: string, 
        public email: string,
        public password: string
        ){}
}

export class AdministratorToAdd{
      constructor( 
        public name: string, 
        public email: string,
        public password: string
        ){}
}

@Injectable({
  providedIn: 'root'
})

export class AdministratorAdapter implements Adapter<Administrator> {
  adapt(item: any): Administrator {
      return new Administrator (
          item.id,
          item.name,
          item.email,
          item.password
      );
  }
}