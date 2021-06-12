import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute , Router} from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { User, UserToUpdate } from 'src/app/models/user/user.module';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-discount',
  templateUrl: './discount.component.html',
  styleUrls: ['./discount.component.css']
})
export class DiscountComponent implements OnInit {

  userList!:User[];

  userForm= new FormGroup({ 
    user: new FormControl(''),
    dis: new FormControl('')
  })
  constructor( 
    private route: ActivatedRoute,
    private router: Router,
    private alertService: AlertService,
    private userService: UserService,
    ) 
  {}

  ngOnInit(): void {
    this.userService.getUserByMeetingCount().subscribe(
      (resp: any) => {
        this.userList = resp;
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
  }
  validateSelectUpdate(): boolean{
    if(this.userForm.value.user == "") return false;
    return true;
  }
  functionDiscount(){
    if(this.validateSelectUpdate()){
      const newuser = new UserToUpdate(
        this.userForm.value.user,
        this.userForm.value.dis,

      );
      this.userService.updateByAdmin(newuser,this.userForm.value.user) .subscribe( resp => {
          this.alertService.success(resp);
          this.ngOnInit()
          this.cleanForm();
        }, (err) => {
          this.alertService.danger(err.error);
          this.ngOnInit()
          this.cleanForm();
        });
    }
  }
  cleanForm(){
    this.userForm.patchValue({
      user:'',
      dis: '',
    });
  }

}
