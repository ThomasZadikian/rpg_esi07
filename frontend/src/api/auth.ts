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