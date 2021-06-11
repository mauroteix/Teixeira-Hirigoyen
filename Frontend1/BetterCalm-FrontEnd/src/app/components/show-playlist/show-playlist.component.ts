import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { CategoryTrack } from 'src/app/models/categorytrack/categorytrack.module';
import { Playlist } from 'src/app/models/playlist/playlist.module';
import { PlaylistCategory } from 'src/app/models/playlistcategory/playlistcategory.module';
import { PlaylistTrack } from 'src/app/models/playlisttrack/playlisttrack.module';
import { PlaylistVideo } from 'src/app/models/playlistvideo/playlistvideo.module';
import { Track } from 'src/app/models/track/track.module';
import { PlaylistService } from 'src/app/services/playlist/playlist.service';

@Component({
  selector: 'app-showplaylist',
  templateUrl: './show-playlist.component.html',
  styleUrls: ['./show-playlist.component.css']
})
export class ShowPlaylistComponent implements OnInit {
  
  public play!:any;
  playlist!: Playlist;
  playlistTrack!: PlaylistTrack[];
  playlistVideo!: PlaylistVideo[];
  isTrack: boolean = false;
  isVideo:boolean = false;
  playSong: boolean = false;
  songTrack!: Track;
  duration!: string;
  url!: SafeResourceUrl;


 constructor(private route: ActivatedRoute,
  private router: Router, private playlistService: PlaylistService, private alertService: AlertService,  private sanitizer: DomSanitizer) { 
  this.route.queryParams.subscribe( params => {
  this.play = +params['id'];
  });
}

  onPlay(track: Track){
    this.playSong = true;
    this.songTrack = track;
    if(track.hour == 0) this.duration = "" +track.minSeconds + " min";
    else if(track.minSeconds == 0) this.duration = ""+track.hour+ " h";
    else this.duration = ""+track.hour+ "h" +  " " +track.minSeconds + "min";
  }


  onFav(){
    this.alertService.info("We are working on..");
  }

  ngOnInit(): void {
    this.playlistService.get(this.play).subscribe(
      (resp: any) => {
        this.playlist = resp
        this.playlistTrack = this.playlist.playlistTrack
        this.playlistVideo = this.playlist.playlistVideo
        if(this.playlist.playlistTrack.length == 0) this.isTrack = true;
        if(this.playlist.playlistVideo.length == 0) this.isVideo = true;
        if(this.playlist.playlistTrack.length == 0 && this.playlist.playlistVideo.length == 0) this.alertService.info("Not exist track or video for this playlist");
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
  }

  
  photoURL(playlistvideo: PlaylistVideo){
    this.url = playlistvideo.video.linkVideo;
    return this.sanitizer.bypassSecurityTrustResourceUrl(this.url+"");
  }

 
}
