import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserCredential } from '../models/user-credential';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  private readonly baseURL: string = 'https://localhost:7136/api/auth/login';

  login(userCredential: UserCredential): Observable<UserCredential> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    return this.http.post<UserCredential>(`${this.baseURL}`, userCredential, { headers : headers })
      .pipe(
        catchError(error => {
          console.error('Error with request', error);
          return throwError(() => new Error('Error with request'));
        })
      );
  }
}
