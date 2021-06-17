import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AlertService } from 'ngx-alerts';
import { Category } from 'src/app/models/category/category.module';
import { CategoryTrackToAdd } from 'src/app/models/categorytrack/categorytrack.module';
import { Playlist, PlaylistToAdd } from 'src/app/models/playlist/playlist.module';
import { PlaylistCategoryToAdd } from 'src/app/models/playlistcategory/playlistcategory.module';
import { PlaylistTrackToAdd } from 'src/app/models/playlisttrack/playlisttrack.module';
import { PlaylistVideoToAdd } from 'src/app/models/playlistvideo/playlistvideo.module';
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
  showDelete: boolean = false;
  nameFunction!: string;
  playlistList!: Playlist[];
  categoryList!: Category[];
  videoList!: Video[];
  trackList!: Track[];
  categoryId!: PlaylistCategoryToAdd;
  trackId!: PlaylistTrackToAdd;
  videoId!: PlaylistVideoToAdd;
  playlistCategory: PlaylistCategoryToAdd[] = [];
  playlistTrack: PlaylistTrackToAdd[] = [];
  playlistVideo: PlaylistVideoToAdd[] = [];

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
    this.categoryService.getAll().subscribe(
      (resp: any) => {
        this.categoryList = resp;
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
    this.trackService.getAll().subscribe(
      (resp: any) => {
        this.trackList = resp;
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
    this.videoService.getAll().subscribe(
      (resp: any) => {
        this.videoList = resp;
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
    playlist : new FormControl(''),
    playlistDelete : new FormControl(''),
  })

  cleanForm(){
    this.playlistCategory = [];
    this.playlistTrack = [];
    this.playlistVideo = [];
    this.playlistForm.patchValue({
      name: '',
      description: '',
      image: '',
      category: '',
      video: '',
      track: '',
      playlist: '',
      playlistDelete: '',
   });
  }

  validatePlaylist() : boolean{
    if(this.playlistForm.value.name == "") {
      this.alertService.info("The name can not be empty");
      return false;
    }
    else if(this.playlistForm.value.description == ""){ 
       this.alertService.info("The description can not be empty");
       return false;
      }
    else if(this.playlistForm.value.description.length > 150){
        this.alertService.info("The description can not be more than 150 characters");
        return false;
    }
    else if(this.playlistForm.value.image == ""){
      this.alertService.info("The image can not be empty");
      return false;
    }
    else if(this.playlistForm.value.category == ""){
      this.alertService.info("You need to select one or more categories");
      return false;
    }
    return true;
  }

  validateSelect(playlist: any): boolean{
    if(playlist == "") return false;
    return true;
  }

  showAddPlaylist(){
    this.showDelete = false;
    this.showSelect = false;
    this.showPlaylist = true;
    this.nameFunction = "Add";
  }

  showUpdatePlaylist(){
    this.showDelete = false;
    this.showSelect = true;
    this.showPlaylist = true;
    this.nameFunction = "Update";
  }

  showDeletePlaylist(){
    this.showPlaylist = false;
    this.showDelete = true;
    this.nameFunction = "Delete";
  }

  functionPlaylist(){
    if(this.showPlaylist && this.validatePlaylist()){
      this.playlistForm.value.category.forEach((e:number) => {
      this.categoryId = new PlaylistCategoryToAdd(e);
      this.playlistCategory.push(this.categoryId);
      });
      if(this.playlistForm.value.video.length != 0){
        this.playlistForm.value.video.forEach((e:number) => {
        this.videoId = new PlaylistVideoToAdd(0,e);
         this.playlistVideo.push(this.videoId);
      });
      }
      if(this.playlistForm.value.track.length != 0){
        this.playlistForm.value.track.forEach((e:number) => {
        this.trackId = new PlaylistTrackToAdd(0,e);
         this.playlistTrack.push(this.trackId);
      });
      }
    const playlist = new PlaylistToAdd(
    this.playlistForm.value.name,
    this.playlistForm.value.description,
    this.playlistForm.value.image,
    this.playlistCategory,
    this.playlistTrack,
    this.playlistVideo,
    );
    if(this.nameFunction == "Add"){
    console.log(playlist);
    this.functionAdd(playlist);
    }
    if(this.nameFunction == "Update"){
      if(this.validateSelect(this.playlistForm.value.playlist)){
        this.functionUpdate(playlist);
      }
      else this.alertService.info("You need to select a playlist");
    }
  }
  if(this.nameFunction == "Delete"){
    if(this.validateSelect(this.playlistForm.value.playlistDelete)){
      this.functionDelete();
    }
    else this.alertService.info("You need to select a playlist");
  }
  }

  functionAdd(playlist: any){
    this.playlistService.post(playlist)
    .subscribe( resp => {
    this.alertService.success(resp);
    this.ngOnInit();
    }, (err) => {
    this.alertService.danger(err.error);
    });
    this.cleanForm();
}

functionUpdate(playlist: any){
  this.playlistService.put(playlist,this.playlistForm.value.playlist) 
    .subscribe( resp => {
    this.alertService.success(resp);
    this.cleanForm();
    this.ngOnInit();
    }, (err) => {
    this.alertService.danger(err.error);
    this.cleanForm();
    });
    
}

functionDelete(){
  this.playlistService.delete(this.playlistForm.value.playlistDelete) 
  .subscribe( resp => {
  this.alertService.success(resp);
  this.ngOnInit();
  this.cleanForm();
  }, (err) => {
  this.alertService.danger(err.error);
  });
}


}
