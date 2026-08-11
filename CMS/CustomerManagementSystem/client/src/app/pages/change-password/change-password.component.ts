import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, ValidationErrors, Validators } from '@angular/forms';
import { finalize } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss'
})
export class ChangePasswordComponent {
  loading = false;
  successMessage = '';
  errorMessage = '';

  form = this.fb.group({
    userName: [{ value: this.authService.getUserName(), disabled: false }, Validators.required],
    oldPassword: ['', Validators.required],
    newPassword: ['', [Validators.required, Validators.minLength(8)]],
    confirmPassword: ['', [Validators.required, Validators.minLength(8)]]
  }, { validators: this.passwordMatchValidator });

  constructor(private readonly fb: FormBuilder, private readonly authService: AuthService) {}

  submit(): void {
    this.form.markAllAsTouched();
    this.successMessage = '';
    this.errorMessage = '';
    if (this.form.invalid || this.loading) {
      return;
    }

    this.loading = true;
    this.authService.changePassword(this.form.getRawValue() as any)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: response => {
          this.successMessage = response.message;
          this.form.patchValue({ oldPassword: '', newPassword: '', confirmPassword: '' });
        },
        error: error => this.errorMessage = error.error?.message ?? 'Password change failed.'
      });
  }

  private passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    return control.get('newPassword')?.value === control.get('confirmPassword')?.value ? null : { passwordMismatch: true };
  }
}
