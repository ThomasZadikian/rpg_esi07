import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL, 
    headers: {'Content-Type':'application/json'},
})

export default api
export interface LoginRequest {username: string; password:string}
export interface LoginResponse{token: string; requiresMfa: boolean; userId: number}
export interface RegisterRequest{username: string; email: string; password: string}
export interface MfaRequest {userId: number; code: string}

export const authApi = {
    login: (data: LoginRequest) => api.post<LoginResponse>('/auth/login', data), 
    register: (data: RegisterRequest) => api.post('/auth/register', data), 
    mfa: (data: MfaRequest) => api.post<LoginResponse>('/auth/mfa', data),
}

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token')
    if(token) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

api.interceptors.response.use(
    (response) => response, 
    (error) => {
        if(error.response?.status === 401)
        {
            localStorage.clear()
            window.location.href = '/login'
        }
        return Promise.reject(error)
    }
)