import { Injectable } from "@angular/core";


@Injectable()
export class AuthService {

  checkIfLoggedIn() {
    return localStorage.getItem('user');
  }

  checkIfIsAdmin() {
    return localStorage.getItem('IsAdmin');
  }
}
