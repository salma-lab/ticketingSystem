import { Routes } from '@angular/router';
import { AdminLoginComponent } from './components/admin-login/admin-login.component';
import { WorkerLoginComponent } from './components/worker-login/worker-login.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { WorkerDashboardComponent } from './components/worker-dashboard/worker-dashboard.component';

export const routes: Routes = [
  { path: 'login/admin', component: AdminLoginComponent },
  { path: 'login/worker', component: WorkerLoginComponent },
  { path: 'admin/dashboard', component: AdminDashboardComponent },
  { path: 'worker/dashboard', component: WorkerDashboardComponent },
  { path: '', redirectTo: '/login/worker', pathMatch: 'full' },
];
