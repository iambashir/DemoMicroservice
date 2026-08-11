import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  loading = false;
  error = '';

  form = this.fb.group({
    userName: ['johnsmith', Validators.required],
    password: ['Password@123', Validators.required]
  });

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {}

  submit(): void {
    this.error = '';

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.error = 'User name এবং password দিন।';
      return;
    }

    this.loading = true;
    this.authService.login(this.form.getRawValue() as any).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/customers']);
      },
      error: err => {
        this.loading = false;
        this.error = err.error?.message ?? 'Invalid username or password.';
      }
    });
  }
}
