import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from '../models/Login';
import { HttpClient } from '@angular/common/http';
import { RegisterModel } from '../models/register';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: LoginModel;
  baseUrl: string;
  router: Router;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrls: string, router: Router) {
    this.model = new LoginModel("", "");
    this.baseUrl = baseUrls;
    this.router = router;
  }

  ngOnInit() {
  }

  login() {
    return this.http.post(this.baseUrl + 'api/User/Login', this.model)
      .subscribe((result: RegisterModel) => {
        console.log(result);
        localStorage.setItem('user', result.firstName);
        localStorage.setItem('company', result.companyname);
        if (result.isAdmin == true) {
          localStorage.setItem('IsAdmin', 'true');
        }
        this.router.navigateByUrl('');
      }, error => console.error(error.error));
  }
}
