import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../auth/authservice';
import { HttpClient } from '@angular/common/http';
import { RegisterModel } from '../models/register';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  public username: String;
  users: Observable<RegisterModel[]>;
  @Input() model: Array<RegisterModel>; 

  constructor(private authService: AuthService, private http: HttpClient) {
  }

  ngOnInit() {
    this.username = localStorage.getItem('user');

    if (this.authService.checkIfIsAdmin) {
      this.users = this.getNotAvtiveUsers();
    }
  }

  getNotAvtiveUsers() {
    return this.http.get<RegisterModel[]>('https://localhost:5001/api/User/All');
  }

  approve(id) {
    console.log(`https://localhost:5001/api/User/${id}`);
    this.http.put(`https://localhost:5001/api/User/${id}`, id)
      .subscribe((result) => console.log(result));

    this.users = this.getNotAvtiveUsers();
  }
 }
