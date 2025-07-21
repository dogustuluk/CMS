import React, { lazy } from 'react';

// project import
import MainLayout from 'layout/MainLayout';
import Loadable from 'component/Loadable';

import PrivateRoute from './PrivateRoutes';

const DashboardDefault = Loadable(lazy(() => import('views/Dashboard/Default')));
const UtilsTypography = Loadable(lazy(() => import('views/Utils/Typography')));
const SamplePage = Loadable(lazy(() => import('views/SamplePage')));
const RolePage = Loadable(lazy(() => import('views/Roles/index.jsx'))); //loadable lazy ile bir bileþeni dinamik olarak, ihtiyaç olunca (lazy loading) yüklememi saðlar.



// ==============================|| MAIN ROUTES ||============================== //

const MainRoutes = {
    path: '/',
    element: <PrivateRoute />,  // PrivateRoute ana element oldu
    children: [
        {
            path: '/',
            element: <MainLayout />,  // Burada MainLayout ile sarmalýyoruz
            children: [
                { path: '/', element: <DashboardDefault /> },
                { path: '/dashboard/default', element: <DashboardDefault /> },
                { path: '/utils/util-typography', element: <UtilsTypography /> },
                { path: '/sample-page', element: <SamplePage /> },
                { path: '/roles', element: <RolePage /> }
            ]
        }
    ]
};


export default MainRoutes;
