import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';
import { MedicalCondition, MedicalConditionAdapter } from 'src/app/models/medicalcondition/medicalcondition.module';
import { User, UserAdapter, UserToAdd, UserToUpdate } from 'src/app/models/user/user.module';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private adapter: UserAdapter) { }

  getUserByMeetingCount(): Observable<User> {
    return this.http.get<User>(`${environment.apiUrl}/user`);
  }

  post(user: UserToAdd){
    //const headers = new HttpHeaders()
    //  .set('Content-Type', 'application/json')
    return this.http.post(`${environment.apiUrl}/user`, user, {responseType: 'text'})
    .pipe(
      map( resp => {
        return resp;
      })
    );
  }

  updateByAdmin(user: UserToUpdate, id: number){
    return this.http.put(`${environment.apiUrl}/user/${id}`, user, {responseType: 'text'})
    .pipe(
    map( resp => {
      return resp;
      })
    );
  }

  

  
}