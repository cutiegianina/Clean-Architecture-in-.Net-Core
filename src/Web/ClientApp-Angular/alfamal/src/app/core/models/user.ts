export class User {
    firstName: string = '';
    lastName: string = '';
    username: string = '';
    password: string = '';
    email: string = '';
    address: string = '';
    roleId: number = 0;
    genderId: number = 0;
    userStatusId?: number;
    dateOfBirth: Date = new Date();
}