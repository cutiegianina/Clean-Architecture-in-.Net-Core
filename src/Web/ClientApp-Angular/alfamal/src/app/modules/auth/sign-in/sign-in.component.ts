import { Component, OnInit, AfterViewInit } from '@angular/core';
import { fromEvent } from 'rxjs';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClientService } from '../../../core/services/http-client.service';
import { UserCredential } from '../../../core/models/user-credential';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, ReactiveFormsModule],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss'
})
export class SignInComponent implements OnInit, AfterViewInit {

  eyeIcon: string = 'fa-solid fa-eye';
  passwordType: string = 'password';

  constructor(
    private fb: FormBuilder,
    private httpService: HttpClientService,
    private authService: AuthService) { }

  signInForm = this.fb.group({
    username: [''],
    password: ['']
  });

  onSubmit() {
    let userCredential: UserCredential = {
      username: this.signInForm.get<string>('username')?.value,
      password: this.signInForm.get<string>('password')?.value
    };

    this.authService.login(userCredential)
      .subscribe(res => {
        console.log(res);
      })

    // const res = this.httpService.getData('api/product')
    //   .subscribe(data => {
    //     console.log(data);
    //   })

    // const res = this.httpService.getDataById('api/product', '9874dca7-992a-414c-9a70-e4ed8c0f610f')
    //   .subscribe(data => {
    //     console.log(data);
    //   })
  }

  ngOnInit() {
  }
  
  ngAfterViewInit() {
    this.handleInputLabelAnimation();
  }


  handleInputLabelAnimation(): void {
    const inputElement = document.querySelectorAll('.form-input');
    inputElement.forEach(element => {
      const inputElementKeyPress = fromEvent(element, 'keyup');
      const elementLabel = document.getElementById(`input-label ${element.id}`)!;

      const getElement = element as HTMLInputElement;

      inputElementKeyPress.subscribe(() => {
        if (getElement.value.length > 0)
          elementLabel.style.display = 'block';
        else
          elementLabel.style.display = 'none';
      });
    });
  }

  toggleEyeIcon(event: Event): void {
    const iconElement = event.currentTarget as HTMLElement;
    if (iconElement.classList.contains('fa-eye')) {
      iconElement.className = 'fa-solid fa-eye-slash eye-icon';
      this.passwordType = 'text';
      iconElement.setAttribute('type', 'text');
    } else {
      iconElement.className = 'fa-solid fa-eye eye-icon';
      this.passwordType = 'password';
    }
  }
}