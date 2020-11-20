import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VehiclesComponent } from './vehicles/vehicles.component';
import { VehicleDetailComponent } from './vehicle-detail/vehicle-detail.component';
import { VehicleTypesComponent } from './vehicle-types/vehicle-types.component';

const routes: Routes = [
    {
        path: 'vehicles',
        component: VehiclesComponent,
    },
    {
        path: 'vehicle-detail/:id',
        component: VehicleDetailComponent,
    },
    {
        path: 'vehicle-type',
        component: VehicleTypesComponent,
    },
    {
        path: '',
        redirectTo: 'vehicles',
        pathMatch: 'full',
    }

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class VehicleRoutingModule { }
