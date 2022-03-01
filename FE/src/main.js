import { createApp } from 'vue';
import App from './App.vue';
import './registerServiceWorker';
import router from './router';
import store from './store';
import './assets/tailwind.css';
import './assets/main.css';
import VeeValidatePlugin from './includes/validation';
import { library } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { fas } from '@fortawesome/free-solid-svg-icons'
library.add(fas);
import { fab } from '@fortawesome/free-brands-svg-icons';
library.add(fab);
import { far } from '@fortawesome/free-regular-svg-icons';
library.add(far);
import { dom } from "@fortawesome/fontawesome-svg-core";
dom.watch();
import axios from 'axios';
import VueAxios from 'vue-axios';


const app = createApp(App);
app.use(store);
app.use(router);
app.use(VeeValidatePlugin);
app.use(VueAxios, axios);
app.component("font-awesome-icon", FontAwesomeIcon);

app.mount('#app');