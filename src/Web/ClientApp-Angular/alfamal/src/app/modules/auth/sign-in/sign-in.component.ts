import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { UserCredential } from '../../../core/models/user-credential';
import { AuthService } from '../../../core/services/auth.service';
import { CommonModule } from '@angular/common';
import { FormService } from '../../../core/services/form.service';
import { NgxLoadingModule } from 'ngx-loading';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, ReactiveFormsModule, CommonModule, NgxLoadingModule],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss'
})
export class SignInComponent implements OnInit, AfterViewInit {

  
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    public formService: FormService,
    private router: Router) {console.log('SignInComponent') }
  loading: boolean = false;

  signInForm = this.fb.group({
    username: ['', this.defaultValidators()],
    password: ['', this.defaultValidators()]
  });

  isLoggedIn: boolean = true;

  defaultValidators(validator?: ValidatorFn[]): ValidatorFn[] {
    const validators: ValidatorFn[] = [
      Validators.required,
      Validators.minLength(8)
    ];
    validator?.forEach(v => validators.push(v));
    return validators;
  }

  getSignInStatus = (res: any): void => {
    let loggedInSuccessful = res['userSignInStatus'] == 1;
    if (loggedInSuccessful) {
      this.authService.login();
    }
    this.isLoggedIn = loggedInSuccessful;
    this.loading = false;
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/admin']);
    }
  };

  onSubmit(): void {
    if (this.signInForm.invalid) {
      this.signInForm.markAllAsTouched();
      return;
    }
    this.loading = true;
    const userCredential = this.signInForm.value as UserCredential;
    this.authService.signIn(userCredential)
      .subscribe({
        next: (res: any) => this.getSignInStatus(res),
        error: (err) => console.error('Sign in failed!', err)
      })
  }

  ngOnInit() {
  }
  
  ngAfterViewInit() {
    this.formService.handleInputLabelAnimation();
  }

}