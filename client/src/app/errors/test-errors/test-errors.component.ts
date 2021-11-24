import { HttpClient, HttpSentEvent } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {

  baseurl = 'https://localhost:5001/api/';
  valdationErrors: string[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404error() {
    this.http.get(this.baseurl + 'buggy/not-found').subscribe(Response => {
      console.log(Response);
    }, error => {
      console.log(error);
    })
  }
  get400error() {
    this.http.get(this.baseurl + 'buggy/bad-request').subscribe(Response => {
      console.log(Response);
    }, error => {
      console.log(error);
    })
  }
  get500error() {
    this.http.get(this.baseurl + 'buggy/server-error').subscribe(Response => {
      console.log(Response);
    }, error => {
      console.log(error);
    })
  }
  get401error() {
    this.http.get(this.baseurl + 'buggy/auth').subscribe(Response => {
      console.log(Response);
    }, error => {
      console.log(error);
    })
  }
  get400ValidationError() {
    this.http.post(this.baseurl + 'buggy/account/register', {}).subscribe(Response => {
      console.log(Response);
    }, error => {
      console.log(error);
      this.valdationErrors=error;
    })
  }


}
