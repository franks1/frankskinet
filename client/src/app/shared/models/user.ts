export interface IUser {
  email: string,
  displayname: string,
  token: string
}


export class User implements IUser {
  displayname: string = '';
  email: string = '';
  token: string = '';
}
