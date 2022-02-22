import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router'
import Home from '../views/Home.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/signup',
    name: 'signup',
    component: () => import('../components/SignUp.vue')
  },
  {
    path: '/signin',
    name: 'signin',
    component: () => import('../components/SignIn.vue')
  },
  {
    path: '/forgot-password',
    name: 'forgot-password',
    component: () => import('../components/ForgotPassword.vue')
  },
  {
    path: '/',
    name: 'home',
    component: () => Home
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
