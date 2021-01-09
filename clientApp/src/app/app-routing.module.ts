import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: '/dashboard',
    },
    {
        path: 'dashboard',
        loadChildren: () =>
            import('modules/dashboard/dashboard-routing.module').then(
                m => m.DashboardRoutingModule
            ),
    },
    {
        path: 'auth',
        loadChildren: () =>
            import('modules/auth/auth-routing.module').then(m => m.AuthRoutingModule),
    },
    {
        path: 'daily-beats',
        loadChildren: () =>
            import('modules/daily-beats/daily-beat-routing.module').then(m => m.DailyBeatRoutingModule),
    },
    {
        path: 'vehicle',
        loadChildren: () =>
            import('modules/vehicle/vehicle-routing.module').then(m => m.VehicleRoutingModule),
    },
    {
        path: 'route',
        loadChildren: () =>
            import('modules/route/route-routing.module').then(m => m.RouteRoutingModule),
    },
    {
        path: 'admin',
        loadChildren: () =>
            import('modules/auth/auth-routing.module').then(m => m.AuthRoutingModule),
    },
    {
        path: 'product-category',
        loadChildren: () =>
            import('modules/product-category/product-category-routing.module').then(m => m.ProductCategoryRoutingModule),
    },
    {
        path: 'product-sub-category',
        loadChildren: () =>
            import('modules/product-sub-category/product-sub-category-routing.module').then(m => m.ProductSubCategoryRoutingModule),
    },
    {
        path: 'product',
        loadChildren: () =>
            import('modules/product/product-routing.module').then(m => m.ProductRoutingModule),
    },
    {
        path: 'supplier',
        loadChildren: () =>
            import('modules/supplier/supplier-routing.module').then(m => m.SupplierRoutingModule),
    },
    {
        path: 'customer',
        loadChildren: () =>
            import('modules/customer/customer-routing.module').then(m => m.CustomerRoutingModule),
    },
    {
        path: 'order',
        loadChildren: () =>
            import('modules/order/order-routing.module').then(m => m.OrderRoutingModule),
    },
    {
        path: 'purchase-orders',
        loadChildren: () =>
            import('modules/purchase-order/purchase-order-routing.module').then(m => m.PurchaseOrderRoutingModule),
    },
    {
        path: 'error',
        loadChildren: () =>
            import('modules/error/error-routing.module').then(m => m.ErrorRoutingModule),
    },

    {
        path: '**',
        pathMatch: 'full',
        loadChildren: () =>
            import('modules/error/error-routing.module').then(m => m.ErrorRoutingModule),
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
    exports: [RouterModule],
})
export class AppRoutingModule {}
