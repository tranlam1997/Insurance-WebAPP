import { createApp } from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import "bootstrap/dist/css/bootstrap.css"
import '@/assets/css/index.css'
import NavBar from './components/NavBar.vue'

const vm = createApp(App).use(store).use(router);

vm.component("NavBar", NavBar);

vm.mount('#app');
