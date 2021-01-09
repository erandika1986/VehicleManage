import { SideNavItems, SideNavSection } from '@modules/navigation/models';

export const sideNavSections: SideNavSection[] = [
    {
        text: 'CORE',
        items: ['dashboard'],
    },
    {
        text: 'Business',
        items: ['vehicleTypes', 'vehicle','route','dailyBeats'],
    },
    {
        text: 'Admin',
        items: ['users', 'suppliers','clients','productCategory','productSubCategory','product'],
    },
];

export const sideNavItems: SideNavItems = {
    dashboard: {
        icon: 'tachometer-alt',
        text: 'Dashboard',
        link: '/dashboard',
    },
/*     layouts: {
        icon: 'columns',
        text: 'Layouts',
        submenu: [
            {
                text: 'Static Navigation',
                link: '/dashboard/static',
            },
            {
                text: 'Light Sidenav',
                link: '/dashboard/light',
            },
        ],
    },
    pages: {
        icon: 'book-open',
        text: 'Pages',
        submenu: [
            {
                text: 'Authentication',
                submenu: [
                    {
                        text: 'Login',
                        link: '/auth/login',
                    },
                    {
                        text: 'Register',
                        link: '/auth/register',
                    },
                    {
                        text: 'Forgot Password',
                        link: '/auth/forgot-password',
                    },
                ],
            },
            {
                text: 'Error',
                submenu: [
                    {
                        text: '401 Page',
                        link: '/error/401',
                    },
                    {
                        text: '404 Page',
                        link: '/error/404',
                    },
                    {
                        text: '500 Page',
                        link: '/error/500',
                    },
                ],
            },
        ],
    }, */

    vehicleTypes: {
        icon: 'chart-area',
        text: 'Vehicle Types',
        link: '/charts',
    },

    vehicle: {
        icon: 'chart-area',
        text: 'Vehicle',
        link: '/charts',
    },

    route: {
        icon: 'chart-area',
        text: 'Route',
        link: '/charts',
    },

    dailyBeats: {
        icon: 'chart-area',
        text: 'Daily Beats',
        link: '/charts',
    },

    users: {
        icon: 'chart-area',
        text: 'Users',
        link: '/charts',
    },
    suppliers: {
        icon: 'table',
        text: 'Suppliers',
        link: '/tables',
    },
    clients: {
        icon: 'table',
        text: 'Clients',
        link: '/tables',
    },
    productCategory: {
        icon: 'table',
        text: 'Product Category',
        link: '/tables',
    },
    productSubCategory: {
        icon: 'table',
        text: 'Product Sub Category',
        link: '/tables',
    }
    ,
    product: {
        icon: 'table',
        text: 'Product',
        link: '/tables',
    }
};
