import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserCredential } from '../models/user-credential';
import { Observable, catchError, throwError } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  private readonly baseURL: string = 'https://localhost:7136/api/auth';

  private readonly userURL: string = 'https://localhost:44311/api/user';

  registerUser(user: User) : Observable<User> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    return this.http.post<User>(`${this.userURL}/register`, user, { headers: headers})
      .pipe(
        catchError(error => {
          console.error('Registration failed!', error);
          return throwError(() => new Error('Registration failed!'))
        })
      )
  }

  login(userCredential: UserCredential): Observable<UserCredential> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    return this.http.post<UserCredential>(`${this.baseURL}/login`, userCredential, { headers: headers })
      .pipe(
        catchError(error => {
          console.error('Error with request', error);
          return throwError(() => new Error('Error with request'));
        })
      );
  }
}
