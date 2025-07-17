import axios from 'axios';

const baseURL = 'https://localhost:7095/api'; // .env'den al

const axiosInstance = axios.create({
    baseURL,
    withCredentials: true,
    headers: {
        'Content-Type': 'application/json'
    }
});

export default axiosInstance;
