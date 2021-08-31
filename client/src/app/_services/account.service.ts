import { HttpClient } from '@angular/common/http';
import { assertNotNull } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseurl = "https://localhost:5001/api/";
  //observable . to store value , to display the value last used 
  private currentusersource = new ReplaySubject<User>(1);
  currentUser$ = this.currentusersource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseurl + 'account/login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentusersource.next(user)
        }

      })
    )

  }
  setCurrentUser(user: User) {
    this.currentusersource.next(user);

  }
  logout() {
    localStorage.removeItem('user');
    this.currentusersource.next();

  }

}
