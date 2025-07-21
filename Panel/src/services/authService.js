import axios from '../lib/axios';

export const login = async (email, password) => {
    const response = await axios.post(
        'https://localhost:7095/api/Auth/login',
        { email, password },
        { withCredentials: true }
    );
    return response.data;
};

export const register = async (username, email, password, role, tenantName, domain) => {
    const response = await axios.post(
        'https://localhost:7095/api/Auth/register',
        { username, email, password, role, tenantName, domain },
        { withCredentials: true }
    );

    return response.data;
}


export const logout = async () => {
    return axios.post('/Auth/logout');
};

export const fetchMe = async () => {
    return axios.get('/Auth/me');

    //const response = await axios.get('/Auth/me');
    //return response.data;

};
