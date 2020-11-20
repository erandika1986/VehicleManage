import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';

const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
            { path: '', redirectTo: 'vehicle', pathMatch: 'prefix' },
            { path: 'vehicle', loadChildren: () => import('./vehicle/vehicle.module').then(m => m.VehicleModule) },
            { path: 'routes', loadChildren: () => import('./route/route.module').then(m => m.RouteModule) },
            { path: 'users', loadChildren: () => import('./user/user.module').then(m => m.UserModule) },
            { path: 'report', loadChildren: () => import('./report/report.module').then(m => m.ReportModule) },
            { path: 'daily-beats', loadChildren: () => import('./daily-beats/daily-beat.module').then(m => m.DailyBeatModule) },

        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LayoutRoutingModule { }
