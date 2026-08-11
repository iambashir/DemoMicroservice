import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent {
  loading = false;
  message = '';
  error = '';

  form = this.fb.group({
    userName: [this.authService.getUserName(), Validators.required],
    oldPassword: ['', Validators.required],
    newPassword: ['', [Validators.required, Validators.minLength(8)]],
    confirmPassword: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private authService: AuthService) {}

  submit(): void {
    this.message = '';
    this.error = '';

    if (this.form.invalid || this.form.value.newPassword !== this.form.value.confirmPassword) {
      this.form.markAllAsTouched();
      this.error = 'সব field পূরণ করুন এবং new password confirm করুন।';
      return;
    }

    this.loading = true;
    this.authService.changePassword(this.form.getRawValue() as any).subscribe({
      next: response => {
        this.loading = false;
        this.message = response.message;
        this.form.patchValue({ oldPassword: '', newPassword: '', confirmPassword: '' });
      },
      error: err => {
        this.loading = false;
        this.error = err.error?.message ?? 'Password change failed.';
      }
    });
  }
}
