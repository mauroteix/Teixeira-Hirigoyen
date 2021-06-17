import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';
import { Playlist, PlaylistAdapter, PlaylistToAdd } from 'src/app/models/playlist/playlist.module';

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {

  constructor(private http: HttpClient, private adapter: PlaylistAdapter) { }

  getAll(): Observable<Playlist> {
    return this.http.get<Playlist>(`${environment.apiUrl}/playlist`);
  }

  get(id: number): Observable<Playlist>{
    return this.http.get(`${environment.apiUrl}/playlist/${id}`).pipe(
      map(item => this.adapter.adapt(item))
    );
  }

  post(playlist: PlaylistToAdd){
    return this.http.post(`${environment.apiUrl}/playlist`, playlist, {responseType: 'text'})
    .pipe(
      map( resp => {
        return resp;
      })
    );
  }

put(playlist: PlaylistToAdd, id: number){
  return this.http.put(`${environment.apiUrl}/playlist/${id}`, playlist, {responseType: 'text'})
  .pipe(
    map( resp => {
      return resp;
    })
  );
}

delete( playlistId: number ){
  return this.http.delete(`${ environment.apiUrl }/playlist/${playlistId}`,  {responseType: 'text'})
  .pipe(
    map( resp => {
      return resp;
    })
  );
}

  
}