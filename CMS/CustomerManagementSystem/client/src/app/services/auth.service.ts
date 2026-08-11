import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { ApiMessageResponse, ChangePasswordRequest, LoginRequest, LoginResponse, RegisterRequest } from '../models/auth.models';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly tokenKey = 'accessToken';
  private readonly refreshTokenKey = 'refreshToken';
  private readonly userNameKey = 'userName';
  private readonly fullNameKey = 'fullName';
  private readonly loggedInSubject = new BehaviorSubject<boolean>(this.isLoggedIn());

  readonly loggedIn$ = this.loggedInSubject.asObservable();

  constructor(private readonly http: HttpClient) {}

  register(request: RegisterRequest): Observable<ApiMessageResponse> {
    return this.http.post<ApiMessageResponse>(`${environment.userApiUrl}/api/UserRegistration`, request);
  }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.userApiUrl}/api/Login`, request).pipe(
      tap(response => this.saveSession(response))
    );
  }

  changePassword(request: ChangePasswordRequest): Observable<ApiMessageResponse> {
    return this.http.put<ApiMessageResponse>(`${environment.userApiUrl}/api/changePassword`, request);
  }

  saveSession(response: LoginResponse): void {
    localStorage.setItem(this.tokenKey, response.accessToken);
    localStorage.setItem(this.refreshTokenKey, response.refreshToken);
    localStorage.setItem(this.userNameKey, response.userName);
    localStorage.setItem(this.fullNameKey, response.fullName);
    this.loggedInSubject.next(true);
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.refreshTokenKey);
    localStorage.removeItem(this.userNameKey);
    localStorage.removeItem(this.fullNameKey);
    this.loggedInSubject.next(false);
  }

  getAccessToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getUserName(): string {
    return localStorage.getItem(this.userNameKey) ?? '';
  }

  getFullName(): string {
    return localStorage.getItem(this.fullNameKey) ?? '';
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }
}
