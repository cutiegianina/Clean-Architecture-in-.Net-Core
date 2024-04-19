import { Component, OnInit, AfterViewInit } from '@angular/core';
import { fromEvent } from 'rxjs';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClientService } from '../../../core/services/http-client.service';
import { UserCredential } from '../../../core/models/user-credential';
import { AuthService } from '../../../core/services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, ReactiveFormsModule, CommonModule],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss'
})
export class SignInComponent implements OnInit, AfterViewInit {

  eyeIcon: string = 'fa-solid fa-eye';
  passwordType: string = 'password';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService) { }

  signInForm = this.fb.group({
    username: ['', [Validators.required, Validators.minLength(8), Validators.max(25)]],
    password: ['', [Validators.required, Validators.minLength(8), Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).+$')]]
  });

  onSubmit() {
    if (this.signInForm.invalid) {
      this.signInForm.markAllAsTouched();
      return;
    }
    const userCredential = this.signInForm.value as UserCredential;
    this.authService.login(userCredential)
      .subscribe(res => {
        console.log(res);
      })
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