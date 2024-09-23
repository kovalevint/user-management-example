import axios from 'axios';

const API_URL = 'http://localhost:5000';  // Your backend URL

const apiClient = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

export default apiClient;