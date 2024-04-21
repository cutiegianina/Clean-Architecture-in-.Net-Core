import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
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

  roles = [
    { id: 1, name: 'Admin' },
    { id: 2, name: 'Merchant' },
    { id: 3, name: 'User' }
  ];

  genders = [
    { id: 1, name: 'Male', },
    { id: 2, name: 'Female' }
  ];
  
  signUpForm = this.fb.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    username: ['', [Validators.required]],
    password: ['', [Validators.required]],
    email: ['', [Validators.required]],
    address: ['', [Validators.required]],
    roleId: [this.roles[0].id],
    genderId: [this.genders[0].id],
    dateOfBirth: [new Date()]
  });

  onSubmit() {
    if (this.signUpForm.invalid) {
      this.signUpForm.markAllAsTouched();
      return;
    }
    const user = this.signUpForm.value as User;
    this.authService.registerUser(user)
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
