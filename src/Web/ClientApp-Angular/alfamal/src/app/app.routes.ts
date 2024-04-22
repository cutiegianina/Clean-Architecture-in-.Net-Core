import { Routes } from '@angular/router';
import { HomeComponent } from './modules/home/home/home.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    { 
        path: 'admin',
        loadChildren: () => import('../app/modules/admin/admin/admin.module').then(m => m.AdminModule),
        canActivate: [authGuard]
    },
    { 
        path: 'sign-in',
        loadComponent: () => import('./modules/auth/sign-in/sign-in.component').then(c => c.SignInComponent)
    },
    {
        path: 'sign-up',
        loadComponent: () => import('./modules/auth/sign-up/sign-up.component').then(c => c.SignUpComponent)
    },
    { path: '', redirectTo: '/sign-in',  pathMatch: 'full' },
    { path: '**', redirectTo: '/sign-in' }
];

// TODO
// 1. lazy load every components