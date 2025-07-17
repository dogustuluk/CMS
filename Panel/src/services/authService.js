import axios from '../lib/axios';

export const login = async (email, password) => {
    const response = await axios.post(
        'https://localhost:7095/api/Auth/login',
        { email, password },
        { withCredentials: true }
    );
    return response.data;
};
