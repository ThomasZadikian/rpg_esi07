import * as authApi from '@/api/auth'
import { useAuthStore } from '@/stores/auth'
import { createPinia, setActivePinia } from 'pinia'
import { beforeEach, describe, expect, it, vi } from 'vitest'

vi.mock('@/api/auth', () => ({
    authApi: {
        login: vi.fn(), 
        register: vi.fn(), 
        mfa: vi.fn(),
    },
    default: {interceptors: {request: {use: vi.fn()}, response: {use: vi.fn()}}}
}))

vi.mock('@/router', () => ({
    default: {push: vi.fn()}
}))

function makeJwt(payload: object): string 
{
    const encoded= btoa(JSON.stringify(payload))
    return `header.${encoded}.signature`
}

describe('useAuthStore', () => {
    beforeEach(async () => {
        vi.resetModules()
        localStorage.clear()
        vi.clearAllMocks()
        setActivePinia(createPinia())
    })
    it('isAuthenticated est false par defaut', () => {
        const store = useAuthStore()
        expect(store.isAuthenticated).toBe(false)
    })

    it('isAuthenticated est true si un token est dans le localStorage', () => {
        localStorage.setItem('token', 'fake-token')
        const store =  useAuthStore()
        expect(store.isAuthenticated).toBe(true)
    })
    it('login sans MFA stocke le token et retourne requiresMfa false', async () => {
        const jwt = makeJwt({
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': 'Player',
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': 'testuser',
    })
    vi.mocked(authApi.authApi.login).mockResolvedValue({
        data: { token: jwt, requiresMfa: false, userId: 1 }
    } as any)
        const store = useAuthStore()
        const result = await store.login({ username: 'testuser', password: 'pass' })
        expect(result.requiresMfa).toBe(false)
        expect(store.isAuthenticated).toBe(true)
        expect(localStorage.getItem('token')).toBe(jwt)
    })
        it('login avec MFA requis retourne requiresMfa true', async () => {
            vi.mocked(authApi.authApi.login).mockResolvedValue({
            data: { token: '', requiresMfa: true, userId: 42 }
        } as any)
            const store = useAuthStore()
            const result = await store.login({ username: 'user', password: 'pass' })
            expect(result.requiresMfa).toBe(true)
            // Le pendingUserId doit être stocké pour l'étape MFA
            expect(localStorage.getItem('pendingUserId')).toBe('42')
        })
            it('logout vide le store et le localStorage', () => {
                localStorage.setItem('token', 'fake')
                localStorage.setItem('userId', '1')
                const store = useAuthStore()
                store.logout()
                expect(store.isAuthenticated).toBe(false)
                expect(localStorage.getItem('token')).toBeNull()
            })
                it('isAdmin est true si le rôle est Admin', () => {
                    localStorage.setItem('role', 'Admin')
                    const store = useAuthStore()
                    expect(store.isAdmin).toBe(true)
                })
                it('isAdmin est false si le rôle est Player', () => {
                localStorage.setItem('role', 'Player')
                const store = useAuthStore()
                expect(store.isAdmin).toBe(false)
            })
        })
            