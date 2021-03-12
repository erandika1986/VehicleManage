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
        id: 'core-business',
        title: 'Business',
        type: 'group',
        icon: 'pages',
        children: [
            {
                id: 'daily-beats',
                title: 'Daily Beats',
                type: 'item',
                icon: 'alarm',
                url: '/vehicle-tracking/daily-beats/list'
            },
            {
                id: 'sale-order',
                title: 'Sales Order',
                type: 'item',
                icon: 'alarm',
                url: '/vehicle-tracking/daily-beats/list'
            },
            {
                id: 'inventory',
                title: 'Product Inventory',
                type: 'item',
                icon: 'alarm',
                url: '/vehicle-tracking/daily-beats/list'
            }
        ]
    },

    {
        id: 'orders',
        title: 'Orders',
        type: 'group',
        icon: 'pages',
        children: [
            {
                id: 'authentication',
                title: 'Authentication',
                type: 'collapsable',
                icon: 'lock',
                badge: {
                    title: '10',
                    bg: '#525e8a',
                    fg: '#FFFFFF'
                },
                children: [
                    {
                        id: 'login',
                        title: 'Login',
                        type: 'item',
                        url: '/pages/auth/login'
                    },
                    {
                        id: 'login-v2',
                        title: 'Login v2',
                        type: 'item',
                        url: '/pages/auth/login-2'
                    }

                ]
            },
            {
                id: 'coming-soon',
                title: 'Coming Soon',
                type: 'item',
                icon: 'alarm',
                url: '/pages/coming-soon'
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
                id: 'authentication',
                title: 'Authentication',
                type: 'collapsable',
                icon: 'lock',
                badge: {
                    title: '10',
                    bg: '#525e8a',
                    fg: '#FFFFFF'
                },
                children: [
                    {
                        id: 'login',
                        title: 'Login',
                        type: 'item',
                        url: '/pages/auth/login'
                    },
                    {
                        id: 'login-v2',
                        title: 'Login v2',
                        type: 'item',
                        url: '/pages/auth/login-2'
                    }

                ]
            },
            {
                id: 'coming-soon',
                title: 'Coming Soon',
                type: 'item',
                icon: 'alarm',
                url: '/pages/coming-soon'
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
                        id: 'vehicle-types',
                        title: 'Vehicle Types',
                        type: 'item',
                        url: '/admin/vehicle-types/list'
                    },
                    {
                        id: 'vehicle',
                        title: 'Vehicle',
                        type: 'item',
                        url: '/admin/vehicle/list'
                    },
                    {
                        id: 'route',
                        title: 'Vehicle Routes',
                        type: 'item',
                        url: '/admin/routes/list'
                    },
                    {
                        id: 'engine-oil',
                        title: 'Engine Oil',
                        type: 'item',
                        url: '/admin/engine-oil/list'
                    },
                    {
                        id: 'gear-box-oil',
                        title: 'Gear Box Oil',
                        type: 'item',
                        url: '/admin/gear-box-oil/list'
                    },
                    {
                        id: 'break-oil',
                        title: 'Break Oil',
                        type: 'item',
                        url: '/admin/break-oil/list'
                    },
                    {
                        id: 'differential-oil',
                        title: 'Differential Oil',
                        type: 'item',
                        url: '/admin/differential-oil/list'
                    },
                    {
                        id: 'engine-coolant',
                        title: 'Engine Coolant',
                        type: 'item',
                        url: '/admin/engine-coolant/list'
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
                    }

                ]
            }
        ]
    }
];
