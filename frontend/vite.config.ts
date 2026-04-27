import vue from '@vitejs/plugin-vue'
import vuetify from 'vite-plugin-vuetify'
import { defineConfig } from 'vitest/config'

export default defineConfig({
  plugins: [vue(),vuetify({autoImport: true})],
  resolve: {alias: { '@': '/src'}},

  test: {
    globals: true, 
    environment: 'jsdom',
    setupFiles: ['./src/tests/setup.ts'],
    coverage: {
      provider: 'v8',
      reporter: ['text','lcov'],
      include: ['src/**/*.{ts,vue}'], 
      exclude: ['src/tests/**','src.main/ts'], 
      thresholds: {lines:70},
    }
  }
})