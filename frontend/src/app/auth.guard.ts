import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const isAuthenticated = this.authService.isAuthenticated();
    const userRole = this.authService.getRole();
    const requiredRoles = route.data['roles'] as Array<string>;

    if (!isAuthenticated) {
      // Redirect to login page if not authenticated
      this.router.navigate(['/login']);
      return false;
    }

    if (requiredRoles && !requiredRoles.includes(userRole)) {
      // Redirect if the user role is not authorized
      alert('Access denied: You do not have the required permissions.');
      this.router.navigate(['/login']);
      return false;
    }

    return true; // Grant access if authenticated and role matches
  }
}
