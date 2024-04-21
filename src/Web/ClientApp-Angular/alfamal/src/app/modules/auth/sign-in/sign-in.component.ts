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
    private router: Router) { }

  signInStatus: boolean = true;
  loading: boolean = false;

  signInForm = this.fb.group({
    username: ['', this.defaultValidators()],
    password: ['', this.defaultValidators([Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).+$')])]
  });

  defaultValidators(validator?: ValidatorFn[]): ValidatorFn[] {
    const validators: ValidatorFn[] = [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(32)
    ];
    validator?.forEach(v => validators.push(v));
    return validators;
  }

  getSignInStatus = (res: any): void => {
    this.signInStatus = res['userSignInStatus'] == 1;
    this.loading = false;
    if (this.signInStatus) {
      this.router.navigate(['/home']);
    }
  };

  onSubmit(): void {
    if (this.signInForm.invalid) {
      this.signInForm.markAllAsTouched();
      return;
    }
    this.loading = true;
    const userCredential = this.signInForm.value as UserCredential;
    this.authService.login(userCredential)
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