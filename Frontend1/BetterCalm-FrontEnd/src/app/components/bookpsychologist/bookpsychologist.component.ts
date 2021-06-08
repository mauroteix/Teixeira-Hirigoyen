import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserToAdd } from 'src/app/models/user/user.module';

@Component({
  selector: 'app-bookpsychologist',
  templateUrl: './bookpsychologist.component.html',
  styleUrls: ['./bookpsychologist.component.css']
})
export class BookpsychologistComponent implements OnInit {
  userForm= new FormGroup({ 
    name:new FormControl(''),
    surname :new FormControl(''),
  })
  constructor() { }

  ngOnInit(): void {
  }

  createMeeting() {
   /* const user = new UserToAdd(
      this.userForm.value.name,
      this.userForm.value.surname,
      this.userForm.value.birthday,
      this.userForm.value.email,
      this.userForm.value.cellphone,

    );*/
  }

}

