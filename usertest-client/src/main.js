import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

import './app.scss'

const app = createApp(App)

app.provide('WebApi', process.env.VUE_APP_WEB_API ?? 'api/')

app.use(router)
app.use(store)
app.mount('#app')
