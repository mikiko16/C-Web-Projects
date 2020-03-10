import { Component, OnInit } from '@angular/core';

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
=======
>>>>>>> parent of 7330c2a... Added so much things !!!
  }

}
