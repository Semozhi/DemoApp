import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  //initializing class model property model object 
  model: any = {}
  currentUser$: Observable<User>;

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {

    this.currentUser$ = this.accountService.currentUser$;

  }

  //method login
  login() {
    this.accountService.login(this.model).subscribe(Response => {
      this.router.navigateByUrl('/members');
      //console.log(Response);

    })

  }

  logout() {
    this.accountService.logout();
    //home page 
    this.router.navigateByUrl('/')



  }


}
