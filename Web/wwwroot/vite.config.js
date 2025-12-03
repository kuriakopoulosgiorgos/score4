import { defineConfig } from 'vite'

export default defineConfig({
    root: 'src',
    server: {
        open: 'index.html',
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
