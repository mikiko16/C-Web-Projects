import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth/authservice';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from '../models/Login';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{
  isExpanded = false;
  username: String;
  routing: Router; 

  constructor(router: Router, private authService: AuthService, private http: HttpClient)
  {
    this.routing = router;

  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  ngOnInit() {
    this.username = localStorage.getItem('user');
  }

  logout() {
    localStorage.clear();
    this.routing.navigateByUrl('/login');
  }
}
