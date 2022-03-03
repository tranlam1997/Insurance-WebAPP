import { createStore } from 'vuex';
import { auth } from './auth.module';
import {toggle} from './toggle.module';


export default createStore({
  modules: {
    auth,
    toggle
  }
});
