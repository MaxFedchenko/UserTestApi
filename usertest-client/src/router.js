import { createRouter, createWebHistory } from 'vue-router'
import Login from './components/Login.vue'
import Tests from './components/Tests.vue'
import TestForm from './components/TestForm.vue'
import store from './store'

const routes = [
  { path: '/login', component: Login },
  { path: '/tests', component: Tests },
  { path: '/test/:id', component: TestForm },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

router.beforeEach((to, from) => {
  if (!store.state.token && to.fullPath != '/login') {
    return '/login'
  }
})

export default router
