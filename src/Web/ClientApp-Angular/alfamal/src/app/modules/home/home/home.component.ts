import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { HttpClientService } from '../../../core/services/http-client.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  constructor(
    private authService: AuthService,
    private router: Router,
    private httpClientService: HttpClientService) {
  }

  #productUrl: string = 'api/product';

  products: any = [];

  getProducts() {
    return this.httpClientService.getDataById(this.#productUrl, '9874DCA7-992A-414C-9A70-E4ED8C0F610F')
      .subscribe({
        next: res => {
          this.products.push(res);
        },
        error: err => console.log(err),
        complete: () => console.log('completed')
      });
  }

  logout():void {
    this.authService.logout();
    this.router.navigate(['/sign-in']);
  }

}
