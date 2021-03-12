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
                        id: 'analytics',
                        title: 'Analytics',
                        type: 'item',
                        url: '/apps/dashboards/analytics'
                    },
                    {
                        id: 'project',
                        title: 'Project',
                        type: 'item',
                        url: '/apps/dashboards/project'
                    }
                ]
            },
        ]
    },
    {
        id: 'vehicles',
        title: 'Vehicles Tracking',
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
                        url: '/apps/vehicle-types/list'
                    },
                    {
                        id: 'vehicle',
                        title: 'Vehicle',
                        type: 'item',
                        url: '/apps/vehicle/list'
                    },
                    {
                        id: 'route',
                        title: 'Vehicle Routes',
                        type: 'item',
                        url: '/apps/routes/list'
                    },
                    {
                        id: 'engine-oil',
                        title: 'Engine Oil',
                        type: 'item',
                        url: '/apps/engine-oil/list'
                    },
                    {
                        id: 'gear-box-oil',
                        title: 'Gear Box Oil',
                        type: 'item',
                        url: '/apps/gear-box-oil/list'
                    },
                    {
                        id: 'break-oil',
                        title: 'Break Oil',
                        type: 'item',
                        url: '/apps/break-oil/list'
                    },
                    {
                        id: 'differential-oil',
                        title: 'Differential Oil',
                        type: 'item',
                        url: '/apps/differential-oil/list'
                    },
                    {
                        id: 'engine-coolant',
                        title: 'Engine Coolant',
                        type: 'item',
                        url: '/apps/engine-coolant/list'
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
                        url: '/apps/product-category/list'
                    },
                    {
                        id: 'product-sub-category',
                        title: 'Product Sub Category',
                        type: 'item',
                        url: '/apps/product-sub-category/list'
                    },
                    {
                        id: 'product',
                        title: 'Product',
                        type: 'item',
                        url: '/apps/product/list'
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
                        url: '/apps/client/list'
                    },
                    {
                        id: 'supplier',
                        title: 'Supplier',
                        type: 'item',
                        url: '/apps/supplier/list'
                    },
                    {
                        id: 'wharehouse',
                        title: 'Wharehouse',
                        type: 'item',
                        url: '/apps/wharehouse/list'
                    }

                ]
            }
        ]
    }
];
