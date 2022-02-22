import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/signup',
    name: 'signup',
    component: () => import('../components/SignUp.vue')
  },
  {
    path: '/login',
    name: 'login',
    component: () => import('../components/LogIn.vue')
  },
  {
    path: '/forgot-password',
    name: 'forgot-password',
    component: () => import('../components/ForgotPassword.vue')
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
