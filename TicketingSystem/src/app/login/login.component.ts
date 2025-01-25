import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  template: `
    <div class="container">
      <h2>Login</h2>
      <form (submit)="login()">
        <div>
          <label for="email">Email</label>
          <input id="email" [(ngModel)]="email" name="email" type="email" />
        </div>
        <div>
          <label for="password">Password</label>
          <input id="password" [(ngModel)]="password" name="password" type="password" />
        </div>
        <button type="submit">Login</button>
      </form>
    </div>
  `
})
export class LoginComponent {
  email = '';
  password = '';

  login() {
    console.log('Email:', this.email, 'Password:', this.password);
  }
}
