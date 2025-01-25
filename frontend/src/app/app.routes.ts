import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'demandes',
    component: DemandeListComponent, // Replace with your component
    canActivate: [AuthGuard],
    data: { roles: ['user', 'admin'] },
  },
  {
    path: 'admin',
    component: AdminPanelComponent, // Replace with your component
    canActivate: [AuthGuard],
    data: { roles: ['admin'] },
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
