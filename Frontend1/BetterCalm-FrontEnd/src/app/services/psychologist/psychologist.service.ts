import { Injectable } from '@angular/core';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';
import { Psychologist, PsychologistAdapter, PsychologistToAdd } from 'src/app/models/psychologist/psychologist.module';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PsychologistService {

  constructor(private http: HttpClient, private adapter: PsychologistAdapter) { }

  getAll(): Observable<Psychologist> {
    return this.http.get<Psychologist>(`${environment.apiUrl}/psychologist`);
  }

  post(psychologist: PsychologistToAdd){
    return this.http.post(`${environment.apiUrl}/psychologist`, psychologist, {responseType: 'text'})
    .pipe(
      map( resp => {
        return resp;
      })
    );
  }

  get(id: number): Observable<Psychologist>{
    return this.http.get(`${environment.apiUrl}/psychologist/${id}`).pipe(
      map(item => this.adapter.adapt(item))
    );
  }
  put(psy: PsychologistToAdd, id: number){
    return this.http.put(`${environment.apiUrl}/psychologist/${id}`, psy, {responseType: 'text'})
    .pipe(
    map( resp => {
      return resp;
    })
  );
}
}



