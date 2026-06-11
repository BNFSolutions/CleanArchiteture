import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// Proxy /api para a API local.
// Padrao: http://localhost:5094
// HTTPS: defina VITE_API_PROXY=https://localhost:7164
const apiProxyTarget = process.env.VITE_API_PROXY ?? "http://localhost:5094";

export default defineConfig({
  base: "./",
  plugins: [react()],
  server: {
    port: 5173,
    host: true,
    proxy: {
      "/api": {
        target: apiProxyTarget,
        changeOrigin: true,
        secure: false
      }
    }
  }
});
