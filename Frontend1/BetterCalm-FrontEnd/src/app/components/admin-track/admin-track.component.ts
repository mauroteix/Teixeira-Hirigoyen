import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NgbPaginationNumber } from '@ng-bootstrap/ng-bootstrap';
import { AlertService } from 'ngx-alerts';
import { Category } from 'src/app/models/category/category.module';
import { CategoryTrackToAdd } from 'src/app/models/categorytrack/categorytrack.module';
import { Playlist } from 'src/app/models/playlist/playlist.module';
import { PlaylistTrackToAdd } from 'src/app/models/playlisttrack/playlisttrack.module';
import { TrackToAdd } from 'src/app/models/track/track.module';
import { CategoryService } from 'src/app/services/category/category.service';
import { PlaylistService } from 'src/app/services/playlist/playlist.service';
import { TrackService } from 'src/app/services/track/track.service';

@Component({
  selector: 'app-admin-track',
  templateUrl: './admin-track.component.html',
  styleUrls: ['./admin-track.component.css']
})
export class AdminTrackComponent implements OnInit {

  constructor(private alertService: AlertService, private categoryService : CategoryService, private playlistService: PlaylistService, private trackService: TrackService) { }

  admin!: boolean;
  nameFunction!: string;
  categoryList!: Category[];
  playlistList!: Playlist[];
  showTrack: boolean = false;
  categoryId!: CategoryTrackToAdd;
  playlistId!: PlaylistTrackToAdd;
  categoryTrack: CategoryTrackToAdd[] = [];
  playlistTrack: PlaylistTrackToAdd[] = [];
  nombre : any = [];
  

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
   
  }

  trackForm= new FormGroup({ 
    name:new FormControl(''),
    author :new FormControl(''),
    image: new FormControl(''),
    sound: new FormControl(''),
    hour: new FormControl(''),
    minSeconds: new FormControl(''),
    category: new FormControl(''),
    playlist: new FormControl(''),
  })

  showAddTrack(){
    this.showTrack = true;
    this.nameFunction = "Add";
  }

  showUpdateTrack(){
    
  }

  showDeleteTrack(){
    
  }

  functionTrack(){
    if(this.nameFunction == "Add"){
      this.functionAdd();
  }
  }

  functionAdd(){
    //console.log(this.trackForm.value.category);
    this.nombre = this.trackForm.value.category; 
    console.log(this.nombre);
    this.nombre.forEach((e:number) => {
        console.log(e);
        this.categoryId = new CategoryTrackToAdd(e);
        console.log(this.categoryId);
        this.categoryTrack.push(this.categoryId);
    });
    this.trackForm.value.playlist.forEach((e:number) => {
      this.playlistId = new PlaylistTrackToAdd(e);
      this.playlistTrack.push(this.playlistId);
  });
   // this.expertisetoadd1 = new ExpertiseToAdd(this.psyForm.value.medical3);
   // this.list.push(this.expertisetoadd1);
    //this.categoryTrack = this.trackForm.value.category;
   // this.playlistTrack = this.trackForm.value.playlist;
    console.log(this.categoryTrack);
    console.log(this.playlistTrack);
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
      this.trackService.post(track)
      .subscribe( resp => {
      this.alertService.success(resp);
      this.ngOnInit();
      }, (err) => {
      this.alertService.danger(err.error);
      });
      //this.cleanForm();
  }

}
