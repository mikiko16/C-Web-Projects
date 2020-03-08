export class RegisterModel {
  constructor(
    public password: string,
    public confirmPassword: string,
    public firstName: string,
    public lastName: string,
    public email: string,
    public isActive: boolean,
    public companyname: string,
    public username: string
  ) { }
}
