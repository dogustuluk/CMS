import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const PrivateRoute = () => {
    const { user, loading } = useAuth();

    if (loading) {
        // Kullan�c� kontrol� yap�lana kadar bekler
        return <div>Y�kleniyor...</div>;
    }

    if (!user) {
        // Kullan�c� yoksa login'e y�nlendir
        return <Navigate to="/application/login" />;
    }

    return <Outlet />;
};

export default PrivateRoute;
