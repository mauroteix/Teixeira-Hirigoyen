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
  admin!: boolean;
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
    if(localStorage.getItem("auth_token") != null){
      this.admin = true;
    }  
    else {
      this.admin = false;
      this.alertService.warning("Unauthorized! You must be logged");
    }
    this.userService.getUserByMeetingCount().subscribe(
      (resp: any) => {
        this.userList = resp;
      },
      err => {
        this.alertService.danger(err.error);
      }
    );
  }
  validateSelectUpdate(item: any): boolean{
    if(item == "") return false;
    return true;
  }
  functionDiscount(){
    if(this.validateSelectUpdate(this.userForm.value.user)){
      if(this.validateSelectUpdate(this.userForm.value.dis)){
      const newuser = new UserToUpdate(
        this.userForm.value.user,
        this.userForm.value.dis,

      );
      this.userService.updateByAdmin(newuser,this.userForm.value.user) .subscribe( resp => {
          this.alertService.success("Discount apply");
          this.ngOnInit()
          this.cleanForm();
        }, (err) => {
          this.alertService.danger(err.error);
          this.ngOnInit()
          this.cleanForm();
        });
      }
      else this.alertService.info("You need to select a discount");
    }
    else this.alertService.info("You need to select a user");
  }
  cleanForm(){
    this.userForm.patchValue({
      user:'',
      dis: '',
    });
  }

}
