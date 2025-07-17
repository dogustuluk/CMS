import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const PrivateRoute = () => {
    const { user, loading } = useAuth();

    if (loading) {
        // Kullanýcý kontrolü yapýlana kadar bekler
        return <div>Yükleniyor...</div>;
    }

    if (!user) {
        // Kullanýcý yoksa login'e yönlendir
        return <Navigate to="/application/login" />;
    }

    return <Outlet />;
};

export default PrivateRoute;
