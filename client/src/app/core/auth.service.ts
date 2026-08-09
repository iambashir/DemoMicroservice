import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';

export interface RegisterRequest {
  fullName: string;
  email: string;
  mobile: string;
  userName: string;
  password: string;
  confirmPassword: string;
}

export interface LoginRequest {
  userName: string;
  password: string;
}

export interface LoginResponse {
  success: boolean;
  accessToken: string;
  refreshToken: string;
  userName: string;
  fullName: string;
  expiresIn: number;
}

export interface ChangePasswordRequest {
  userName: string;
  oldPassword: string;
  newPassword: string;
  confirmPassword: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly tokenKey = 'accessToken';
  private readonly refreshTokenKey = 'refreshToken';
  private readonly userNameKey = 'userName';
  private readonly fullNameKey = 'fullName';

  constructor(private http: HttpClient) {}

  register(request: RegisterRequest): Observable<{ success: boolean; message: string }> {
    return this.http.post<{ success: boolean; message: string }>(`${environment.userApiUrl}/api/UserRegistration`, request);
  }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.userApiUrl}/api/Login`, request).pipe(
      tap(response => {
        localStorage.setItem(this.tokenKey, response.accessToken);
        localStorage.setItem(this.refreshTokenKey, response.refreshToken);
        localStorage.setItem(this.userNameKey, response.userName);
        localStorage.setItem(this.fullNameKey, response.fullName);
      })
    );
  }

  changePassword(request: ChangePasswordRequest): Observable<{ success: boolean; message: string }> {
    return this.http.put<{ success: boolean; message: string }>(`${environment.userApiUrl}/api/changePassword`, request);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getUserName(): string {
    return localStorage.getItem(this.userNameKey) ?? '';
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.refreshTokenKey);
    localStorage.removeItem(this.userNameKey);
    localStorage.removeItem(this.fullNameKey);
  }
}
