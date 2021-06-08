import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class LogInService {

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<any> {
     return this.http.post(`${environment.apiUrl}/session`,  {
      email: email,
      password: password
    })
  }

  logout() {
    localStorage.removeItem('auth_token');
  }
}
