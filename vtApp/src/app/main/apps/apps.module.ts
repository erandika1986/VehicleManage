import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FuseSharedModule } from '@fuse/shared.module';

const routes = [
    {
        path: '',
        redirectTo: 'vehicle-types',
        pathMatch: 'full'
    },
    {
        path: 'vehicle-types',
        loadChildren: () => import('./vehicle-types/vehicle-types.module').then(m => m.VehicleTypesModule)
    },
    {
        path: 'vehicle',
        loadChildren: () => import('./vehicle-types/vehicle-types.module').then(m => m.VehicleTypesModule)
    },
];

@NgModule({
    imports: [
        RouterModule.forChild(routes),
        FuseSharedModule
    ]
})
export class AppsModule {
}
