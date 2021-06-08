import { Component, OnInit } from '@angular/core';
import { Router} from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { Category } from 'src/app/models/category/category.module';
import { CategoryTrack } from 'src/app/models/categorytrack/categorytrack.module';
import { Playlist } from 'src/app/models/playlist/playlist.module';
import { PlaylistCategory } from 'src/app/models/playlistcategory/playlistcategory.module';
import { CategoryService } from 'src/app/services/category/category.service';
import { CategoryVideo } from 'src/app/models/categoryvideo/categoryvideo.module';
import { isNull } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {

  categories!: Category[];
  category!: Category;
  playlistCategory! : PlaylistCategory[];
  listPlaylist!: Playlist[];
  categoryTrack!: CategoryTrack[];
  categoryVideo!: CategoryVideo[];
  isPlaylist!: boolean;
  isTrack!: boolean;
  isVideo!: boolean;
  image!: string ;
  


  constructor(private categoryService: CategoryService,private  router:Router,
    private alertService: AlertService)
    { 
    
  }

  ngOnInit(): void {
    this.categoryService.getAll().subscribe(
      (res:any) => {
        this.categories = res;
      },
      err => this.alertService.warning(err.error())
    );
    /*if(localStorage.getItem("auth_token") != null) this.admin = true;  
    else this.admin = false;
    this.regions = ["","CentroSur","CorredorPajarosPintados","Este","LitoralNorte","Metropolitana"];*/
  }
  
  searchCategory(id: number){
    this.categoryService.get(id).subscribe(
      (resp: any) => {
        
        this.category = resp
        this.playlistCategory = this.category.playlistCategory
        this.categoryTrack = this.category.categoryTrack
        this.categoryVideo = this.category.categoryVideo
        
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
    this.setFalseAll();

  }
  showPlaylist(){
    if(this.playlistCategory.length == 0) {
      this.alertService.warning("Not exist playlist for that category");
    }
    else{
    this.isVideo = false;
    this.isTrack = false;
    this.isPlaylist = true;
  }
  }

  showTrack(){
    if(this.categoryTrack.length == 0) {
      this.alertService.warning("Not exist track for that category");
    }
    else{
    this.isVideo = false;
    this.isPlaylist = false;
    this.isTrack = true;
    }
  }

  
  showVideo(){
    if(this.categoryVideo.length == 0) {
      this.alertService.warning("Not exist video for that category");
    }
    else{
    this.isPlaylist = false;
    this.isTrack = false;
    this.isVideo = true;
    }
  }

  setFalseAll(){
    this.isPlaylist = false;
    this.isTrack = false;
    this.isVideo = false;
  }
  
  navegar(playlist:PlaylistCategory){
    const params = {
      id: playlist.idPlaylist
    };
    this.router.navigate( ['infoplaylist'], { queryParams: params });
  }
}
