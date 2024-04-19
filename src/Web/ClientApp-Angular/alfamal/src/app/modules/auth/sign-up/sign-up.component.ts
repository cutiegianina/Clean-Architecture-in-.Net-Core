import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { fromEvent } from 'rxjs';
import { User } from '../../../core/models/user';
import { AuthService } from '../../../core/services/auth.service';
import { CommonModule } from '@angular/common';
import { FormService } from '../../../core/services/form.service';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, ReactiveFormsModule, CommonModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent {


  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    public formService: FormService) {
  }
  
  signUpForm = this.fb.group({
    firstName: [''],
    lastName: [''],
    username: [''],
    password: [''],
    email: ['']
  });

  onSubmit() {
    if (this.signUpForm.invalid) {
      this.signUpForm.markAllAsTouched();
      return;
    }
    const userCredential = this.signUpForm.value as User;
    this.authService.registerUser(userCredential)
      .subscribe(res => {
        console.log(res);
      })
  }

  ngOnInit() {
  }
  
  ngAfterViewInit() {
    this.formService.handleInputLabelAnimation();
  }
}
