import { defineConfig } from 'vite'

export default defineConfig({
    server: {
        open: 'src/index.html',
        proxy: {
            '/api': {
                target: 'http://localhost:5278',
                changeOrigin: true,
                secure: false,
            },
            '/gameHub': {
                target: 'http://localhost:5278',
                ws: true,
                changeOrigin: true,
                secure: false,
            },
        },
    },
})
