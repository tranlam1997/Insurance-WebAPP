import { createRouter, createWebHistory } from 'vue-router';

const Home = () => import('@/views/Home.vue');
const About = () => import('@/views/About.vue');
const Header = () => import('@/components/Header.vue');
const Footer = () => import('@/components/Footer.vue');
const Profile = () => import('@/views/Profile.vue'); 

const routes = [
  {
    path: '/',
    name: 'Home', // example.com/
    components: {
      default: Home,
      Header,
      Footer,
    }
  },
  {
    name: 'About',
    path: '/about',
    components: {
      default: About,
      Header,
      Footer,
    }
  },
  {
    name: 'Profile',
    path: '/profile',
    components: {
      default: Profile,
      Header,
      Footer,
    }
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
