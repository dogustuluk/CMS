import axios from '../lib/axios';

export const login = async (email, password) => {
    const response = await axios.post(
        'https://localhost:7095/api/Auth/login',
        { email, password },
        { withCredentials: true }
    );
    return response.data;
};


export const logout = async () => {
    return axios.post('/Auth/logout');
};

export const fetchMe = async () => {
    return axios.get('/Auth/me');
};
