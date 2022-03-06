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
import GlobalComponents from "./globalComponents";



const app = createApp(App);
app.use(GlobalComponents);
app.directive('click-outside', {
    mounted(el, binding) {
      el.clickOutsideEvent = function(event) {
        if (!(el === event.target || el.contains(event.target))) {
          binding.value(event, el);
        }
      };
      document.body.addEventListener('click', el.clickOutsideEvent);
    },
    unmounted(el) {
      document.body.removeEventListener('click', el.clickOutsideEvent);
    }
  });
app.use(store);
app.use(router);
app.use(VeeValidatePlugin);
app.use(VueAxios, axios);
app.component("font-awesome-icon", FontAwesomeIcon);

app.mount('#app');