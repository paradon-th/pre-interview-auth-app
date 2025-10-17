import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { authGuard } from './guards/auth.guard';
import { LayoutComponent } from './layout/layout.component';
import { RegisterComponent } from './pages/register/register.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent},
    {
        path: 'dashboard',
        component: LayoutComponent,
        canActivate: [authGuard],
        children: [
            {path: '', component: DashboardComponent}
        ]
    },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
];
