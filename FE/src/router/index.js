import { createRouter, createWebHistory } from 'vue-router';
import jwt_decode from 'jwt-decode';
import store from '../store'
import DashboardLayout from "@/pages/Layout/DashboardLayout.vue";

import Dashboard from "@/pages/Dashboard.vue";
import UserProfile from "@/pages/UserProfile.vue";
import TableList from "@/pages/TableList.vue";
import Typography from "@/pages/Typography.vue";
import Icons from "@/pages/Icons.vue";
import Maps from "@/pages/Maps.vue";
import Notifications from "@/pages/Notifications.vue";
import UpgradeToPRO from "@/pages/UpgradeToPRO.vue";

const Home = () => import('@/views/Home.vue');
const About = () => import('@/views/About.vue');
const Header = () => import('@/components/Header.vue');
const Footer = () => import('@/components/Footer.vue');
const Profile = () => import('@/views/Profile.vue'); 
const ChangePassword = () => import('@/components/ChangePassword.vue')

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
    },
    children: [
      {
      path: 'changePassword',
      component: ChangePassword
    }
    ]
  },
  {
    path: "/",
    component: DashboardLayout,
    redirect: "/dashboard",
    children: [
      {
        path: "dashboard",
        name: "Dashboard",
        component: Dashboard,
      },
      {
        path: "user",
        name: "User Profile",
        component: UserProfile,
      },
      {
        path: "table",
        name: "Table List",
        component: TableList,
      },
      {
        path: "typography",
        name: "Typography",
        component: Typography,
      },
      {
        path: "icons",
        name: "Icons",
        component: Icons,
      },
      {
        path: "maps",
        name: "Maps",
        meta: {
          hideFooter: true,
        },
        component: Maps,
      },
      {
        path: "notifications",
        name: "Notifications",
        component: Notifications,
      },
      {
        path: "upgrade",
        name: "Upgrade to PRO",
        component: UpgradeToPRO,
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
  linkExactActiveClass: 'text-yellow-500',
});

router.beforeEach(() => {
  if (store.state.auth.status.loggedIn) {
    let user = JSON.parse(localStorage.getItem('user'));
    if (user && user.token) {
      const decoded = jwt_decode(user.token);
      const expire = decoded.exp;
      if(expire - Date.now()/1000 < 0){
        localStorage.removeItem('user');
      }
  }
}})

export default router;
