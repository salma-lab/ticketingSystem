import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7046/api'; // Replace with your API URL

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/Auth/login`, { email, password });
  }

  // Function to check if user has admin role
  isAdmin(): boolean {
    const token = localStorage.getItem('token');
    return token ? this.decodeToken(token).role === 'Admin' : false;
  }
  

  // Function to check if user is worker/technician
  isWorkerOrTechnician(): boolean {
    const token = localStorage.getItem('token');
    const role = token ? this.decodeToken(token).role : null;
    return role === 'Worker' || role === 'Technician';
  }

  private decodeToken(token: string): any {
    // Decode token logic here
  }
}
