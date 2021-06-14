import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AlertService } from 'ngx-alerts';
import { Category } from 'src/app/models/category/category.module';
import { CategoryVideoToAdd } from 'src/app/models/categoryvideo/categoryvideo.module';
import { Playlist } from 'src/app/models/playlist/playlist.module';
import { PlaylistVideoToAdd } from 'src/app/models/playlistvideo/playlistvideo.module';
import { Video, VideoToAdd } from 'src/app/models/video/video.module';
import { CategoryService } from 'src/app/services/category/category.service';
import { PlaylistService } from 'src/app/services/playlist/playlist.service';
import { VideoService } from 'src/app/services/video/video.service';

@Component({
  selector: 'app-admin-video',
  templateUrl: './admin-video.component.html',
  styleUrls: ['./admin-video.component.css']
})
export class AdminVideoComponent implements OnInit {

  constructor(private alertService: AlertService ,private categoryService : CategoryService, private playlistService: PlaylistService,
    private videoService: VideoService) { }

  admin!: boolean;
  showVideo: boolean = false;
  showSelect: boolean = false;
  showDelete: boolean = false;
  nameFunction!: string;
  categoryList!: Category[];
  playlistList!: Playlist[];
  videoList!: Video[];
  categoryId!: CategoryVideoToAdd;
  playlistId!: PlaylistVideoToAdd;
  categoryVideo: CategoryVideoToAdd[] = [];
  playlistVideo: PlaylistVideoToAdd[] = [];


  ngOnInit(): void {
    if(localStorage.getItem("auth_token") != null){
      this.admin = true;
    }  
    else {
      this.admin = false;
      this.alertService.warning("Unauthorized! You must be logged");
    }
    this.categoryService.getAll().subscribe(
      (resp: any) => {
        this.categoryList = resp;
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
    this.playlistService.getAll().subscribe(
      (resp: any) => {
        this.playlistList = resp;
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

  videoForm= new FormGroup({ 
    name:new FormControl(''),
    author :new FormControl(''),
    linkVideo: new FormControl(''),
    hour: new FormControl('0'),
    minSeconds: new FormControl('0'),
    category: new FormControl(''),
    playlist: new FormControl(''),
    video: new FormControl(''),
    videoDelete: new FormControl(''),
  })

  cleanForm(){
    this.categoryVideo = [];
    this.playlistVideo = [];
    this.videoForm.patchValue({
      name: '',
      author: '',
      linkVideo: '',
      hour:'0',
      minSeconds: '0',
      category: '',
      playlist: '',
      video: '',
      videoDelete: '',
   });
  }

  validateSelect(video: any): boolean{
    if(video == "") return false;
    return true;
  }

  showAddVideo(){
      this.showDelete = false;
      this.showVideo = true;
      this.showSelect = false;
      this.nameFunction = "Add";
  }

  showUpdateVideo(){
    this.showDelete = false;
    this.showVideo = true;
    this.showSelect = true;
    this.nameFunction = "Update";

  }

  showDeleteVideo(){
    this.showVideo = false;
    this.showDelete = true;
    this.nameFunction = "Delete";
  }

  validateVideo() : boolean{
    if(this.videoForm.value.name == "") {
      this.alertService.info("The name can not be empty");
      return false;
    }
    else if(this.videoForm.value.author == ""){ 
       this.alertService.info("The author can not be empty");
       return false;
      }
    else if(this.videoForm.value.linkVideo == ""){
        this.alertService.info("The link video can not be empty");
        return false;
    }
    else if(this.videoForm.value.hour == "" && this.videoForm.value.minSeconds == ""){
      this.alertService.info("The hour and minSeconds both can not be empty, you must add one");
      return false;
    }
    else if(this.videoForm.value.hour == 0 && this.videoForm.value.minSeconds == 0 ){
      this.alertService.info("The hour and minSeconds both can not be 0");
      return false;
    }
    else if(this.videoForm.value.category == ""){
      this.alertService.info("You need to select one or more categories");
      return false;
    }
    return true;
  }

  functionVideo(){
    if(this.showVideo && this.validateVideo()){
      this.videoForm.value.category.forEach((e:number) => {
      this.categoryId = new CategoryVideoToAdd(e);
      this.categoryVideo.push(this.categoryId);
      });
      if(this.videoForm.value.playlist.length != 0){
        this.videoForm.value.playlist.forEach((e:number) => {
        this.playlistId = new PlaylistVideoToAdd(e);
         this.playlistVideo.push(this.playlistId);
      });
      }
    const video = new VideoToAdd(
    this.videoForm.value.name,
    this.videoForm.value.author,
    this.videoForm.value.hour,
    this.videoForm.value.minSeconds,
    this.videoForm.value.linkVideo,
    this.categoryVideo,
    this.playlistVideo,
    );
    if(this.nameFunction == "Add"){
    this.functionAdd(video);
    }
    if(this.nameFunction == "Update"){
      if(this.validateSelect(this.videoForm.value.video)){
        this.functionUpdate(video);
      }
      else this.alertService.info("You need to select a track");
    }
  }
  if(this.nameFunction == "Delete"){
    if(this.validateSelect(this.videoForm.value.videoDelete)){
      this.functionDelete();
    }
    else this.alertService.info("You need to select a administrator");
  }
  }

  functionAdd(video: any){
    console.log(video);
    this.videoService.post(video)
    .subscribe( resp => {
    this.alertService.success(resp);
    this.ngOnInit();
    }, (err) => {
      console.log(err);
    this.alertService.danger(err.error);
    });
    this.cleanForm();
  }

  functionUpdate(video: any){
    this.videoService.put(video,this.videoForm.value.video) 
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
    this.videoService.delete(this.videoForm.value.videoDelete) 
    .subscribe( resp => {
    this.alertService.success(resp);
    this.cleanForm();
    this.ngOnInit();
    }, (err) => {
    this.alertService.danger(err.error);
    });
  }

}
