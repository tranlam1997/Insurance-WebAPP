import { createRouter, createWebHistory } from 'vue-router';

const Home = () => import('@/views/Home.vue');
const About = () => import('@/views/About.vue');

const routes = [
  {
    name: 'Home',
    path: '/', // example.com/
    component: Home,
  },
  {
    name: 'About',
    path: '/about',
    component: About,
  },
  {
    path: '/:catchAll(.*)*',
    redirect: { name: 'home' },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
  linkExactActiveClass: 'text-yellow-500',
});


export default router;
