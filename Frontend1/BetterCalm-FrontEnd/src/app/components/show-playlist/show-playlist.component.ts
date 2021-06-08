import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryTrack } from 'src/app/models/categorytrack/categorytrack.module';
import { Playlist } from 'src/app/models/playlist/playlist.module';
import { PlaylistCategory } from 'src/app/models/playlistcategory/playlistcategory.module';
import { PlaylistTrack } from 'src/app/models/playlisttrack/playlisttrack.module';

@Component({
  selector: 'app-showplaylist',
  templateUrl: './show-playlist.component.html',
  styleUrls: ['./show-playlist.component.css']
})
export class ShowPlaylistComponent implements OnInit {
  
  public play!:any;

 // playList!:Playlist;
 // playlistTrack! : PlaylistTrack[];
 constructor(private route: ActivatedRoute,
  private router: Router) { 
  this.route.queryParams.subscribe( params => {
  this.play = +params['id'];
  });
}
  mostrarPlayList(){
    
   // this.playList = this.playlistcategory.playlist;
    //this.playlistTrack  = this.playList.playlistTrack;
  }

  ngOnInit(): void {
    
  }

}
