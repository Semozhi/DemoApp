import { Component, OnInit } from '@angular/core';
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

  constructor(private accountservice: AccountService) { }

  ngOnInit(): void {

    this.currentUser$ = this.accountservice.currentUser$;

  }

  //method login
  login() {
    this.accountservice.login(this.model).subscribe(Response => {
      console.log(Response);

    }, error => {
      console.log(error);
    })

  }

  logout() {
    this.accountservice.logout();


  }


}
