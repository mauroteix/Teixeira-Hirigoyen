import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AlertService } from 'ngx-alerts';
import { Category } from 'src/app/models/category/category.module';
import { Playlist } from 'src/app/models/playlist/playlist.module';
import { Track } from 'src/app/models/track/track.module';
import { Video } from 'src/app/models/video/video.module';
import { CategoryService } from 'src/app/services/category/category.service';
import { PlaylistService } from 'src/app/services/playlist/playlist.service';
import { TrackService } from 'src/app/services/track/track.service';
import { VideoService } from 'src/app/services/video/video.service';

@Component({
  selector: 'app-admin-playlist',
  templateUrl: './admin-playlist.component.html',
  styleUrls: ['./admin-playlist.component.css']
})
export class AdminPlaylistComponent implements OnInit {

  constructor(private alertService: AlertService, private playlistService: PlaylistService,  private categoryService: CategoryService,
    private videoService: VideoService,  private trackService: TrackService
    ) { }

  admin!: boolean;
  showPlaylist: boolean = false;
  showSelect: boolean = false;
  nameFunction!: string;
  playlistList!: Playlist[];
  categoryList!: Category[];
  videoList!: Video[];
  trackList!: Track[];

  ngOnInit(): void {
    if(localStorage.getItem("auth_token") != null){
      this.admin = true;
    }  
    else {
      this.admin = false;
      this.alertService.warning("Unauthorized! You must be logged");
    }
    this.playlistService.getAll().subscribe(
      (resp: any) => {
        this.playlistList = resp;
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
  }

  playlistForm= new FormGroup({ 
    name:new FormControl(''),
    description :new FormControl(''),
    image: new FormControl(''),
    category: new FormControl(''),
    video: new FormControl(''),
    track: new FormControl(''),
  })

  showAddPlaylist(){
    this.showPlaylist = true;
  }

  showUpdatePlaylist(){
    this.showPlaylist = true;
  }

  showDeletePlaylist(){
    this.showPlaylist = false;
  }

  functionVideo(){

  }

}
