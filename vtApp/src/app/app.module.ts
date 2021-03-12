import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { InMemoryWebApiModule } from 'angular-in-memory-web-api';
import { TranslateModule } from '@ngx-translate/core';
import 'hammerjs';

import { FuseModule } from '@fuse/fuse.module';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseProgressBarModule, FuseSidebarModule, FuseThemeOptionsModule } from '@fuse/components';

import { fuseConfig } from 'app/fuse-config';

import { FakeDbService } from 'app/fake-db/fake-db.service';
import { AppComponent } from 'app/app.component';
import { AppStoreModule } from 'app/store/store.module';
import { LayoutModule } from 'app/layout/layout.module';
import { AuthGuard } from './guard/auth.guard';
import { CustomAuthService } from './services/account/custom-auth.service';
import { AuthInterceptorService } from './services/account/auth-interceptor.service';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';

const appRoutes: Routes = [
    {
        path: '',
        redirectTo: 'admin',
        pathMatch: 'full'
    },
    {
        path: 'apps',
        loadChildren: () => import('./main/apps/apps.module').then(m => m.AppsModule),
        canActivate: [AuthGuard]
    },
    {
        path: 'admin',
        loadChildren: () => import('./main/admin/admin.module').then(m => m.AdminModule),
        canActivate: [AuthGuard]
    },
    {
        path: 'vehicle-tracking',
        loadChildren: () => import('./main/vehicle-tracking/vehicle-tracking.module').then(m => m.VehicleTrackingModule),
        canActivate: [AuthGuard]
    },
    {
        path: 'dashbaord',
        loadChildren: () => import('./main/dashbaord/dashbaord.module').then(m => m.DashbaordModule),
        canActivate: [AuthGuard]
    },
    {
        path: 'inventory',
        loadChildren: () => import('./main/inventory/inventory.module').then(m => m.InventoryModule),
        canActivate: [AuthGuard]
    },
    {
        path: 'order',
        loadChildren: () => import('./main/order/order.module').then(m => m.OrderModule),
        canActivate: [AuthGuard]
    },
    {
        path: 'pages',
        loadChildren: () => import('./main/pages/pages.module').then(m => m.PagesModule)
    }

];

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes, { relativeLinkResolution: 'legacy' }),

        TranslateModule.forRoot(),
        InMemoryWebApiModule.forRoot(FakeDbService, {
            delay: 0,
            passThruUnknownUrl: true
        }),

        // Material moment date module
        MatMomentDateModule,

        // Material
        MatButtonModule,
        MatIconModule,

        // Fuse modules
        FuseModule.forRoot(fuseConfig),
        FuseProgressBarModule,
        FuseSharedModule,
        FuseSidebarModule,
        FuseThemeOptionsModule,

        // App modules
        LayoutModule,
        AppStoreModule
    ],
    providers:
        [
            AuthGuard,
            CustomAuthService,
            { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true },
            { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
            {
                provide: LocationStrategy,
                useClass: HashLocationStrategy
            }
        ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule {
}
