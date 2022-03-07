import { createRouter, createWebHistory } from 'vue-router';
import jwt_decode from 'jwt-decode';
import store from '../store'
import Dashboard from "@/views/DashboardFrame.vue";
import Tables from "@/views/Tables.vue";
import Billing from "@/views/Billing.vue";
import VirtualReality from "@/views/VirtualReality.vue";
import ProfileAdmin from "@/views/ProfileAdmin.vue";
import Rtl from "@/views/Rtl.vue";
import SignIn from "@/views/SignIn.vue";
import SignUp from "@/views/SignUp.vue";
import AdminBoard from "@/views/AdminBoard.vue";

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
    name: 'Admin',
    path: '/admin',
    components: {
      Admin: AdminBoard,
    },
    children: [
      {
        path: "dashboard",
        name: "Dashboard",
        component: Dashboard,
      },
      {
        path: "tables",
        name: "Tables",
        component: Tables,
      },
      {
        path: "billing",
        name: "Billing",
        component: Billing,
      },
      {
        path: "virtual-reality",
        name: "Virtual Reality",
        component: VirtualReality,
      },
      {
        path: "profile",
        name: "Profile",
        component: ProfileAdmin,
      },
      {
        path: "rtl-page",
        name: "Rtl",
        component: Rtl,
      },
      {
        path: "sign-in",
        name: "Sign In",
        component: SignIn,
      },
      {
        path: "sign-up",
        name: "Sign Up",
        component: SignUp,
      },
    ]
}
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
