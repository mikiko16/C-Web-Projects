import { Component, OnInit, Inject, InjectionToken } from '@angular/core';
import { RegisterModel } from '../models/RegisterModel';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})

export class RegistrationComponent implements OnInit {

  model: RegisterModel;
  private BASE_URL: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    this.model = new RegisterModel('', '', '', '', '', false, '', '');
    this.BASE_URL = baseUrl;
  }

  ngOnInit() {
    console.log('Miro is here !!!');
  }

  submit() {
    this.http.post(this.BASE_URL + 'api/User/Register', this.model).subscribe(result => {
    }, error => console.error(error));
  }

}
