import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

// onInit is a lifecycle hook that gets called after data bound properties are created
export class AppComponent implements OnInit {
  title = 'Dating App';
  users: any;

  // constructor with HttpClient obj
  constructor(private http: HttpClient){}

  // this is required method for OnInit
  ngOnInit() {
    this.getUsers();
  }

  // this is an async fucntion. This gets your "observable" which you then have to "subscirbe"
  // to get the response.
  getUsers() {
    this.http.get("https://localhost:5001/api/users").subscribe(response=>{
      this.users = response;  // you then override the properties
    }, error => {
      console.log(error);
    })
  }
}
