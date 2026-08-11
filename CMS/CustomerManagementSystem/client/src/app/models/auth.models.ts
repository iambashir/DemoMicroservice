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

export interface ChangePasswordRequest {
  userName: string;
  oldPassword: string;
  newPassword: string;
  confirmPassword: string;
}

export interface ApiMessageResponse {
  success: boolean;
  message: string;
}

export interface LoginResponse {
  success: boolean;
  accessToken: string;
  refreshToken: string;
  userName: string;
  fullName: string;
  expiresIn: number;
}
