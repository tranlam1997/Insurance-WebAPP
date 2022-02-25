import { createStore } from 'vuex';

export default createStore({
  state: {
    loginModalShow: false,
    registerModalShow: false,
  },
  mutations: {
    toggleLoginModal: (state) => {
      state.loginModalShow = !state.loginModalShow;
    },
    toggleRegisterModal(state) {
      state.registerModalShow = !state.registerModalShow;
    },
  },
});
