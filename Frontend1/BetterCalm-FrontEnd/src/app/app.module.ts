import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';


//Import alerts
import { AlertModule } from 'ngx-alerts';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthInterceptor } from './components/auth-interceptor/auth-interceptor.component';
import { CommonModule } from '@angular/common'; 
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { PlayerComponent } from './components/player/player.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ShowPlaylistComponent } from './components/show-playlist/show-playlist.component';
import { BookpsychologistComponent } from './components/bookpsychologist/bookpsychologist.component';
import { LogInComponent } from './components/log-in/log-in.component';
import { MedicalconditionComponent } from './components/medicalcondition/medicalcondition.component';
import { AdministratorComponent } from './components/administrator/administrator.component';
import { PsychologistComponent } from './components/psychologist/psychologist.component';
import { DiscountComponent } from './components/discount/discount.component';
import { AdminTrackComponent } from './components/admin-track/admin-track.component';



@NgModule({
  declarations: [
    AppComponent,
    LogInComponent,
    AuthInterceptor,
    PlayerComponent,
    NavbarComponent,
    ShowPlaylistComponent,
    BookpsychologistComponent,
    MedicalconditionComponent,
    AdministratorComponent,
    PsychologistComponent,
    DiscountComponent,
    AdminTrackComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AlertModule.forRoot({maxMessages: 5, timeout: 6000, positionX: 'right', positionY: 'top'}),
    NgbModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      {path: 'session', component: LogInComponent},
      {path: 'player', component: PlayerComponent},
      {path: 'infoplaylist', component: ShowPlaylistComponent},
      {path: 'bookpsychologist', component: BookpsychologistComponent},
      {path: 'medicalcondition', component: MedicalconditionComponent},
      {path: 'adminManage', component: AdministratorComponent},
      {path: 'psychologist', component: PsychologistComponent},
      {path: 'registerdiscount', component: DiscountComponent},
      {path: 'track', component: AdminTrackComponent},
    ])
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
