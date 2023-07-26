import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  // Send build static files to our server to serve.
  build: {
    outDir: "../ReactRoastDotnet.API/wwwroot",
    emptyOutDir: true,
  },
})
