import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Video, VideoAdapter, VideoToAdd } from 'src/app/models/video/video.module';
import { environment } from 'src/environments/environment';
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class VideoService {

  constructor(private http: HttpClient, private adapter: VideoAdapter) { }

  getAll(): Observable<Video> {
    return this.http.get<Video>(`${environment.apiUrl}/video`);
  }

  get(id: number): Observable<Video>{
    return this.http.get(`${environment.apiUrl}/video/${id}`).pipe(
      map(item => this.adapter.adapt(item))
    );
  }

  post(video: VideoToAdd){
    return this.http.post(`${environment.apiUrl}/video`, video, {responseType: 'text'})
    .pipe(
      map( resp => {
        return resp;
      })
    );
  }

put(video: VideoToAdd, id: number){
  return this.http.put(`${environment.apiUrl}/video/${id}`, video, {responseType: 'text'})
  .pipe(
    map( resp => {
      return resp;
    })
  );
}

delete( videoId: number ){
  return this.http.delete(`${ environment.apiUrl }/video/${videoId}`,  {responseType: 'text'})
  .pipe(
    map( resp => {
      return resp;
    })
  );
}

}
