import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id: 'applications',
        title: 'Analytics',
        translate: 'NAV.APPLICATIONS',
        type: 'group',
        icon: 'apps',
        children: [
            {
                id: 'dashboards',
                title: 'Dashboards',
                translate: 'NAV.DASHBOARDS',
                type: 'collapsable',
                icon: 'dashboard',
                children: [
                    {
                        id: 'inventory-dashboard',
                        title: 'Inventory',
                        type: 'item',
                        url: '/dashbaord/inventory-dashboard/i-dashboard'
                    },
                    {
                        id: 'sales-dashboard',
                        title: 'Sales',
                        type: 'item',
                        url: '/dashbaord/sales-dashboard/s-dashboard'
                    },
                    {
                        id: 'vehicle-dashboard',
                        title: 'Vehicle',
                        type: 'item',
                        url: '/dashbaord/vehicle-dashboard/v-dashboard'
                    }
                ]
            },
        ]
    },
    {
        id: 'vehicle',
        title: 'Vehicle',
        type: 'group',
        icon: 'pages',
        children: [
            {
                id: 'daily-beats',
                title: 'Daily Beats',
                type: 'item',
                icon: 'alarm',
                url: '/vehicle-tracking/daily-beats/list'
            }
        ]
    },
    {
        id: 'sales',
        title: 'Sales',
        type: 'group',
        icon: 'pages',
        children: [
            {
                id: 'sale-order',
                title: 'Sales Order',
                type: 'item',
                icon: 'alarm',
                url: '/vehicle-tracking/daily-beats/list'
            }
        ]
    },
    {
        id: 'inventory',
        title: 'Inventory',
        type: 'group',
        icon: 'pages',
        children: [

            {
                id: 'inventory-detail',
                title: 'Product Inventory',
                type: 'item',
                icon: 'alarm',
                url: '/inventory/inventory-detail/list'
            },
            {
                id: 'purchase-order',
                title: 'Purchase Orders',
                type: 'item',
                icon: 'alarm',
                url: '/inventory/purchase-order/list'
            }
        ]
    },
    {
        id: 'admin',
        title: 'Admin',
        type: 'group',
        icon: 'pages',
        children: [
            {
                id: 'Vehicle',
                title: 'Vehicles',
                type: 'collapsable',
                icon: 'lock',
                children: [
                    {
                        id: 'vehicle',
                        title: 'Vehicle',
                        type: 'item',
                        url: '/admin/vehicle/list'
                    },
                    {
                        id: 'vehicle-types',
                        title: 'Vehicle Types',
                        type: 'item',
                        url: '/admin/vehicle-types/list'
                    },
                    {
                        id: 'route',
                        title: 'Routes',
                        type: 'item',
                        url: '/admin/routes/list'
                    },
                    {
                        id: 'code',
                        title: 'Master Data Codes',
                        type: 'item',
                        url: '/admin/code/list'
                    }

                ]
            },
            {
                id: 'product',
                title: 'Product',
                type: 'collapsable',
                icon: 'lock',
                children: [
                    {
                        id: 'product-category',
                        title: 'Product Category',
                        type: 'item',
                        url: '/admin/product-category/list'
                    },
                    {
                        id: 'product-sub-category',
                        title: 'Product Sub Category',
                        type: 'item',
                        url: '/admin/product-sub-category/list'
                    },
                    {
                        id: 'product',
                        title: 'Product',
                        type: 'item',
                        url: '/admin/product/list'
                    },

                ]
            },
            {
                id: 'Other',
                title: 'Other',
                type: 'collapsable',
                icon: 'lock',
                children: [
                    {
                        id: 'client',
                        title: 'Client',
                        type: 'item',
                        url: '/admin/client/list'
                    },
                    {
                        id: 'supplier',
                        title: 'Supplier',
                        type: 'item',
                        url: '/admin/supplier/list'
                    },
                    {
                        id: 'wharehouse',
                        title: 'Wharehouse',
                        type: 'item',
                        url: '/admin/wharehouse/list'
                    },
                    {
                        id: 'User',
                        title: 'Users',
                        type: 'item',
                        url: '/admin/user/list'
                    }

                ]
            }
        ]
    }
];
