import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';
import { MedicalCondition, MedicalConditionAdapter } from 'src/app/models/medicalcondition/medicalcondition.module';

@Injectable({
  providedIn: 'root'
})
export class MedicalconditionService {

  constructor(private http: HttpClient, private adapter: MedicalConditionAdapter) { }

  getAll(): Observable<MedicalCondition> {
    return this.http.get<MedicalCondition>(`${environment.apiUrl}/medicalcondition`);
  }

  get(id: number): Observable<MedicalCondition>{
    return this.http.get(`${environment.apiUrl}/medicalcondition/${id}`).pipe(
      map(item => this.adapter.adapt(item))
    );
  }

  
}