import { createApp } from 'vue'
import './style.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap'
import App from './App.vue'
import axios from 'axios'

const axiosClient = axios.create({
	baseURL: 'http://localhost:5078/api/',
});

const app = createApp(App)
app.provide('axios-client', axiosClient);
app.mount('#app')
