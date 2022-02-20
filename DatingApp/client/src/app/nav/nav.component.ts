import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}; // this will be updated from the form
  loggedIn: boolean;
  // inject service into this component
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
}
