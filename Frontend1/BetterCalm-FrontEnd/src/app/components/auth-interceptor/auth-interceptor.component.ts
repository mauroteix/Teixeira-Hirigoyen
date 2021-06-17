import { Component } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-auth-interceptor',
  templateUrl: './auth-interceptor.component.html',
  styleUrls: ['./auth-interceptor.component.css']
})
export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      const token = localStorage.getItem('auth_token');
  if (!token) {
        return next.handle(req);
      }
  const headers = req.clone({
        headers: req.headers.set('Authorization', token)
      });
  return next.handle(headers);
    }
  }
