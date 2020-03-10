<<<<<<< HEAD
import { Component, OnInit } from '@angular/core';
=======
import { Component, OnInit, Inject, InjectionToken } from '@angular/core';
import { RegisterModel } from '../models/RegisterModel';
import { HttpClient } from '@angular/common/http';
>>>>>>> parent of a44f7aa... Register and Login Works!!!

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor() { }

  ngOnInit() {
<<<<<<< HEAD
    console.log('Miro is here !!!');
  }

  submit() {
    this.http.post(this.BASE_URL + 'api/User/Register', this.model)
      .subscribe(result => {
    }, error => console.error(error));
<<<<<<< HEAD
=======
>>>>>>> parent of 7330c2a... Added so much things !!!
=======
>>>>>>> parent of a44f7aa... Register and Login Works!!!
  }

}
