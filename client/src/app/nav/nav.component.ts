import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  //initializing class model property model object 
  model: any = {}

  loggedIn: boolean = true;

  constructor(private accountservice: AccountService) { }

  ngOnInit(): void {
  }

  //method login
  login() {
    this.accountservice.login(this.model).subscribe(Response => {
      console.log(Response);
      this.loggedIn = true;
    }, error => {   
      console.log(error);
    })

  }

  logout() {
    this.accountservice.logout();
    this.loggedIn = false;

  }
  getCurrentUser() {
    this.accountservice.currentUser$.subscribe(user => {
      this.loggedIn = !!user;
    }, error => {
      console.error();
    }

    )
  }

}
