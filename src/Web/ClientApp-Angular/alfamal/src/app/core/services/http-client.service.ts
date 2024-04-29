import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {

  constructor(private http: HttpClient) {}

  private readonly baseURL: string = 'https://localhost:44311';

  getData(url: string): Observable<any> {
    return this.http.get(`${this.baseURL}/${url}`)
      .pipe(
        catchError(error => {
          console.error('Error fetching data');
          return throwError(() => new Error('Error fetching data'));
        })
      );
  }

  getDataById(url: string, id: string): Observable<any> {
    // let headers = new HttpHeaders();
    // let token = localStorage.getItem('jwtToken');
    // headers = headers.append('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseURL}/${url}/${id}`)
      .pipe(
        catchError(error => {
          console.error('Error fetching data', error);
          return throwError(() => new Error('Error fetching data'));
        })
      );
  }
}