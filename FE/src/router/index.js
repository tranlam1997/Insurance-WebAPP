import { createRouter, createWebHistory } from 'vue-router';

const Home = () => import('@/views/Home.vue');
const About = () => import('@/views/About.vue');
const Header = () => import('@/components/Header.vue');

const routes = [
  {
    path: '/',
    name: 'Home', // example.com/
    components: {
      default: Home,
      newHeader: Header,
    }
  },
  {
    name: 'About',
    path: '/about',
    component: About,
  },
  {
    name: 'User',
    path: '/user/:id',
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
  linkExactActiveClass: 'text-yellow-500',
});


export default router;
