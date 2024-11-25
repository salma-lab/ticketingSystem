import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-worker-login',
  templateUrl: './worker-login.component.html'
})
export class WorkerLoginComponent {
  email = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.email, this.password).subscribe(
      (response) => {
        if (this.authService.isWorkerOrTechnician()) {
          this.router.navigate(['/worker/dashboard']);
        } else {
          alert('Access Denied: Workers or Technicians only.');
        }
      },
      (error) => {
        alert('Invalid credentials');
      }
    );
  }
}
