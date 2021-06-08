import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShowPlaylistComponent } from './components/show-playlist/show-playlist.component';

const routes: Routes = [
  { path: 'infoplaylist', component: ShowPlaylistComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
