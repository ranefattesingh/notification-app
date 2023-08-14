import { defineConfig, loadEnv } from 'vite'
import vue from '@vitejs/plugin-vue'

export default ({ mode }) => {
  // Extends 'process.env.*' with VITE_*-variables from '.env.(mode=production|development)'
  process.env = {...process.env, ...loadEnv(mode, process.cwd())};
  return defineConfig({
      server: {
        port: 8080,
      },
      plugins: [vue()],
  }); 
};