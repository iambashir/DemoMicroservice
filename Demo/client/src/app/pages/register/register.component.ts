import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  loading = false;
  message = '';
  error = '';

  form = this.fb.group({
    fullName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    mobile: ['', Validators.required],
    userName: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(8)]],
    confirmPassword: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {}

  submit(): void {
    this.message = '';
    this.error = '';

    if (this.form.invalid || this.form.value.password !== this.form.value.confirmPassword) {
      this.form.markAllAsTouched();
      this.error = 'সব field সঠিকভাবে পূরণ করুন। Password এবং Confirm Password একই হতে হবে।';
      return;
    }

    this.loading = true;
    this.authService.register(this.form.getRawValue() as any).subscribe({
      next: response => {
        this.loading = false;
        this.message = response.message;
        setTimeout(() => this.router.navigate(['/login']), 600);
      },
      error: err => {
        this.loading = false;
        this.error = err.error?.message ?? 'Registration failed.';
      }
    });
  }
}
