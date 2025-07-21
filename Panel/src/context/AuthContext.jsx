import React, { createContext, useContext, useState, useEffect } from 'react';
import { jwtDecode } from 'jwt-decode';
import axiosInstance from '../lib/axios';
import { register as registerService, fetchMe } from '../services/authService';


const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);


  

    useEffect(() => {
        const checkAuth = async () => {
            try {
                const res = await axiosInstance.get('/Auth/me');
                setUser(res.data);
            } catch {
                setUser(null);
            } finally {
                setLoading(false);
            }
        };
        checkAuth();
    }, []);

    const loginUser = async (email, password) => {
        try {
            await axiosInstance.post('/Auth/login', { email, password });
            const res = await axiosInstance.get('/Auth/me');
            setUser(res.data);
        } catch (err) {
            setUser(null);
            throw err;
        }
    };


    const logoutUser = async () => {
        try {
            await axiosInstance.post('/Auth/logout', {}, { withCredentials: true });
            setUser(null);
        } catch (error) {
            console.error("Logout failed:", error);
        }
    };


    const registerUser = async (username, email, password, role, tenantName, domain) => {
        try {
            await registerService(username, email, password, role, tenantName, domain);
            const me = await fetchMe();
            setUser(me);
        } catch (error) {
            console.error("Register failed:", error);
            setUser(null);
            throw error;
        }
    };

    return (
        <AuthContext.Provider value={{ user, loginUser, logoutUser, registerUser, loading }}>
            {children}
        </AuthContext.Provider>
    );

};

export const useAuth = () => useContext(AuthContext);
