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
        id: 'products',
        title: 'Products',
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
                        id: 'vehicle_types',
                        title: 'Vehicle Types',
                        type: 'item',
                        url: '/apps/vehicle-types/list'
                    },
                    {
                        id: 'route',
                        title: 'Routes',
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
    }
];
