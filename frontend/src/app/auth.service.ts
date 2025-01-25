import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://localhost:5001/api/Auth'; // Update this URL to match your backend API

  constructor(
    private http: HttpClient,
    private router: Router,
    private jwtHelper: JwtHelperService
  ) {}

  // Login method to authenticate the user
  login(credentials: { email: string; password: string }) {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, credentials);
  }

  // Logout method to clear the token and redirect to the login page
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  // Check if the user is authenticated
  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return token ? !this.jwtHelper.isTokenExpired(token) : false;
  }

  // Get the current user's role from the JWT token
  getRole(): string {
    const token = localStorage.getItem('token');
    if (token) {
      const payload = this.jwtHelper.decodeToken(token);
      return payload.role; // Assuming the role is stored in the token
    }
    return '';
  }
}
