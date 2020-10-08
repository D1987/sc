import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/login',
        pathMatch: 'full'
    },
    { 
        path: 'login', 
        loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule)
    },
    { 
        path: 'hosts', 
        loadChildren: () => import('./host/host.module').then(m => m.HostModule),
        canActivate: [AuthGuard]
    },
    { 
        path: 'vms', 
        loadChildren: () => import('./virtualmachine/vm.module').then(m => m.VMModule),
        canActivate: [AuthGuard]
    },
    { 
        path: 'apps', 
        loadChildren: () => import('./application/app.module').then(m => m.AppModule),
        canActivate: [AuthGuard]
    },
    {
        path: '**',
        redirectTo: ''
    }
];

export const appRouterProviders = RouterModule.forRoot(routes, { useHash: true });
