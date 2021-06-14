import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AlertService } from 'ngx-alerts';
import { Category } from 'src/app/models/category/category.module';
import { CategoryTrackToAdd } from 'src/app/models/categorytrack/categorytrack.module';
import { Playlist } from 'src/app/models/playlist/playlist.module';
import { PlaylistTrackToAdd } from 'src/app/models/playlisttrack/playlisttrack.module';
import { Track, TrackToAdd } from 'src/app/models/track/track.module';
import { CategoryService } from 'src/app/services/category/category.service';
import { PlaylistService } from 'src/app/services/playlist/playlist.service';
import { TrackService } from 'src/app/services/track/track.service';

@Component({
  selector: 'app-admin-track',
  templateUrl: './admin-track.component.html',
  styleUrls: ['./admin-track.component.css']
})
export class AdminTrackComponent implements OnInit {

  constructor(private alertService: AlertService, private categoryService : CategoryService, private playlistService: PlaylistService,
     private trackService: TrackService) { }

  admin!: boolean;
  nameFunction!: string;
  categoryList!: Category[];
  playlistList!: Playlist[];
  showTrack: boolean = false;
  showSelect: boolean = false;
  showDelete: boolean = false;
  categoryId!: CategoryTrackToAdd;
  playlistId!: PlaylistTrackToAdd;
  categoryTrack: CategoryTrackToAdd[] = [];
  playlistTrack: PlaylistTrackToAdd[] = [];
  trackList!: Track[];

  

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
    this.trackService.getAll().subscribe(
      (resp: any) => {
        this.trackList = resp;
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
   
  }

  trackForm= new FormGroup({ 
    name:new FormControl(''),
    author :new FormControl(''),
    image: new FormControl(''),
    sound: new FormControl(''),
    hour: new FormControl('0'),
    minSeconds: new FormControl('0'),
    category: new FormControl(''),
    playlist: new FormControl(''),
    track: new FormControl(''),
    trackDelete: new FormControl(''),
  })

  cleanForm(){
    this.categoryTrack = [];
    this.playlistTrack = [];
    this.trackForm.patchValue({
      name: '',
      author: '',
      image: '',
      sound:'',
      hour:'0',
      minSeconds: '0',
      category: '',
      playlist: '',
      track: '',
      trackDelete: '',
   });
  }

  validateTrack() : boolean{
    if(this.trackForm.value.name == "") {
      this.alertService.info("The name can not be empty");
      return false;
    }
    else if(this.trackForm.value.author == ""){ 
       this.alertService.info("The author can not be empty");
       return false;
      }
    else if(this.trackForm.value.image == ""){
        this.alertService.info("The image can not be empty");
        return false;
    }
    else if(this.trackForm.value.sound == ""){
      this.alertService.info("The sound can not be empty");
      return false;
    }
    else if(this.trackForm.value.hour == "" && this.trackForm.value.minSeconds == ""){
      this.alertService.info("The hour and minSeconds both can not be empty, you must add one");
      return false;
    }
    else if(this.trackForm.value.hour == 0 && this.trackForm.value.minSeconds == 0 ){
      this.alertService.info("The hour and minSeconds both can not be 0");
      return false;
    }
    else if(this.trackForm.value.hour < 0 || this.trackForm.value.minSeconds < 0 ){
      this.alertService.info("The hour and minSeconds can not be negative");
      return false;
    }
    else if(this.trackForm.value.category == ""){
      this.alertService.info("You need to select one or more categories");
      return false;
    }
    return true;
  }

  validateSelect(track: any): boolean{
    if(track == "") return false;
    return true;
  }

  showAddTrack(){
    this.showDelete = false;
    this.showSelect = false;
    this.showTrack = true;
    this.nameFunction = "Add";
  }

  showUpdateTrack(){
    this.showDelete = false;
    this.showTrack = true;
    this.showSelect = true;
    this.nameFunction = "Update";
  }

  showDeleteTrack(){
    this.showDelete = true;
    this.showTrack = false;
    this.nameFunction = "Delete";
  }

  functionTrack(){
    if(this.showTrack && this.validateTrack()){
        this.trackForm.value.category.forEach((e:number) => {
        this.categoryId = new CategoryTrackToAdd(e);
        this.categoryTrack.push(this.categoryId);
        });
        if(this.trackForm.value.playlist.length != 0){
          this.trackForm.value.playlist.forEach((e:number) => {
          this.playlistId = new PlaylistTrackToAdd(e);
           this.playlistTrack.push(this.playlistId);
        });
        }
      const track = new TrackToAdd(
      this.trackForm.value.name,
      this.trackForm.value.author,
      this.trackForm.value.image,
      this.trackForm.value.hour,
      this.trackForm.value.minSeconds,
      this.trackForm.value.sound,
      this.categoryTrack,
      this.playlistTrack,
      );
      if(this.nameFunction == "Add"){
      this.functionAdd(track);
      }
      if(this.nameFunction == "Update"){
        if(this.validateSelect(this.trackForm.value.track)){
          this.functionUpdate(track);
        }
        else this.alertService.info("You need to select a track");
      }
    }
    if(this.nameFunction == "Delete"){
      if(this.validateSelect(this.trackForm.value.trackDelete)){
        this.functionDelete();
      }
      else this.alertService.info("You need to select a administrator");
    }
  }

  functionUpdate(track: any){
    this.trackService.put(track,this.trackForm.value.track) 
      .subscribe( resp => {
      this.alertService.success(resp);
      this.cleanForm();
      this.ngOnInit();
      }, (err) => {
      this.alertService.danger(err.error);
      this.cleanForm();
      });
      
  }

  functionAdd(track: any){
      this.trackService.post(track)
      .subscribe( resp => {
      this.alertService.success(resp);
      this.ngOnInit();
      }, (err) => {
      this.alertService.danger(err.error);
      });
      this.cleanForm();
  }

  functionDelete(){
    this.trackService.delete(this.trackForm.value.trackDelete) 
    .subscribe( resp => {
    this.alertService.success(resp);
    this.ngOnInit();
    this.cleanForm();
    }, (err) => {
    this.alertService.danger(err.error);
    });
  }

}
