import { Component, OnInit, Inject } from '@angular/core';
import { RegisterModel } from '../models/register';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  model: RegisterModel;
  baseUrl: string;
  router: Router;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrls: string, router: Router) {
    this.model = new RegisterModel("", "", "", "", "", false, false);
    this.baseUrl = baseUrls;
    this.router = router;
  }

  ngOnInit() {
  }

  submit() {
    console.log(this.model);

    return this.http.post(this.baseUrl + 'api/User/Register', this.model)
      .subscribe((result: RegisterModel) => {
        localStorage.setItem('user', result.firstName);
        this.router.navigateByUrl('');
      }, error => console.error(error));
  }
}
