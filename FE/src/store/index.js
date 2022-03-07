import { createStore } from 'vuex';
import { auth } from './auth.module';
import {toggle} from './toggle.module';
import {user} from './user.module';
import {admin} from './admin.module';


export default createStore({
  modules: {
    auth,
    toggle,
    user,
    admin,
  }
});
