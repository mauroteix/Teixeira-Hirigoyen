import { Component, OnInit, SecurityContext } from '@angular/core';
import { Router} from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { Category } from 'src/app/models/category/category.module';
import { CategoryTrack } from 'src/app/models/categorytrack/categorytrack.module';
import { Playlist } from 'src/app/models/playlist/playlist.module';
import { PlaylistCategory } from 'src/app/models/playlistcategory/playlistcategory.module';
import { CategoryService } from 'src/app/services/category/category.service';
import { CategoryVideo } from 'src/app/models/categoryvideo/categoryvideo.module';
import { isNull } from '@angular/compiler/src/output/output_ast';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

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
  url!: SafeResourceUrl;
  


  constructor(private categoryService: CategoryService,private  router:Router,
    private alertService: AlertService, private sanitizer: DomSanitizer)
    { 
    
  }

  ngOnInit(): void {
    this.categoryService.getAll().subscribe(
      (res:any) => {
        this.categories = res;
      },
      err => this.alertService.warning(err.error())
    );
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
    console.log(this.categoryVideo);
    console.log(this.categoryVideo.length);
    if(this.categoryVideo.length == 0) {
      this.alertService.warning("Not exist video for that category");
    }
    else{
    this.isPlaylist = false;
    this.isTrack = false;
    this.isVideo = true;
    }
  }

  photoURL(categoryvideo: CategoryVideo){
    console.log("Esta entrando");
    console.log(categoryvideo);
    console.log(categoryvideo.video.linkVideo);
    this.url = categoryvideo.video.linkVideo;
    return this.sanitizer.bypassSecurityTrustResourceUrl(this.url+"");
   // return this.sanitizer.bypassSecurityTrustResourceUrl('categoryvideo.video.linkVideo');
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
