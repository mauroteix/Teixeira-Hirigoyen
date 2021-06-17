import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category, CategoryAdapter } from 'src/app/models/category/category.module';
import { environment } from 'src/environments/environment';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient, private adapter: CategoryAdapter) { }

  getAll(): Observable<Category> {
    return this.http.get<Category>(`${environment.apiUrl}/category`);
  }

  get(id: number): Observable<Category>{
    return this.http.get(`${environment.apiUrl}/category/${id}`).pipe(
      map(item => this.adapter.adapt(item))
    );
  }

  
}
