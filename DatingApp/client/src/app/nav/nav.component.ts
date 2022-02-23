import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}; // this will be updated from the form

  // this will be used in the nav.component.html using the directive *ngIf,
  // e.g., we can use this to determine wheather to show some elements only when user is logged in
  loggedIn: boolean;
  // inject service into this component - dependency injection
  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  // this is called from ngForm
  login() {
    //console.log(this.model);
    this.accountService.login(this.model).subscribe(
      response => {
        console.log(response);
        this.loggedIn = true;
      },
      error => {
        console.log(error);
      }
    );
  }

  logout() {
    this.loggedIn = false;
  }
}
