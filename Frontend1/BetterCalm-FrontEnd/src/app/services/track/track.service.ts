import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Track, TrackAdapter, TrackToAdd } from 'src/app/models/track/track.module';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class TrackService {

  constructor(private http: HttpClient, private adapter: TrackAdapter) { }

  getAll(): Observable<Track> {
    return this.http.get<Track>(`${environment.apiUrl}/track`);
  }

  get(id: number): Observable<Track>{
    return this.http.get(`${environment.apiUrl}/track/${id}`).pipe(
      map(item => this.adapter.adapt(item))
    );
  }

  post(track: TrackToAdd){
    return this.http.post(`${environment.apiUrl}/track`, track, {responseType: 'text'})
    .pipe(
      map( resp => {
        return resp;
      })
    );
  }

put(track: TrackToAdd, id: number){
  return this.http.put(`${environment.apiUrl}/track/${id}`, track, {responseType: 'text'})
  .pipe(
    map( resp => {
      return resp;
    })
  );
}

delete( trackId: number ){
  return this.http.delete(`${ environment.apiUrl }/track/${trackId}`,  {responseType: 'text'})
  .pipe(
    map( resp => {
      return resp;
    })
  );
}

}
