import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';
import { Administrator, AdministratorAdapter, AdministratorToAdd } from 'src/app/models/admin/admin.module';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient, private adapter: AdministratorAdapter) { }

  getAll(): Observable<Administrator> {
    return this.http.get<Administrator>(`${environment.apiUrl}/administrator`);
  }

  post(admin: AdministratorToAdd){
    return this.http.post(`${environment.apiUrl}/administrator`, admin, {responseType: 'text'})
    .pipe(
      map( resp => {
        return resp;
      })
    );
  }

put(admin: AdministratorToAdd, id: number){
  return this.http.put(`${environment.apiUrl}/administrator/${id}`, admin, {responseType: 'text'})
  .pipe(
    map( resp => {
      return resp;
    })
  );
}

delete( adminId: number ){
  return this.http.delete(`${ environment.apiUrl }/administrator/${adminId}`,  {responseType: 'text'})
  .pipe(
    map( resp => {
      return resp;
    })
  );

}


}