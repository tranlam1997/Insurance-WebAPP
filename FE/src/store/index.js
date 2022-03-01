import { createStore } from 'vuex';

export default createStore({
  state: {
    loginModalShow: false,
    registerModalShow: false,
    userLoginState: false,
    userModalShow: false,
  },
  mutations: {
    toggleLoginModal(state) {
      state.loginModalShow = !state.loginModalShow;
    },
    toggleRegisterModal(state) {
      state.registerModalShow = !state.registerModalShow;
    },
    toggleBetweenLoginAndRegisterModal(state) {
      state.loginModalShow = !state.loginModalShow;
      state.registerModalShow = !state.registerModalShow;
    },
    toggleUserInterface(state) {
      state.userLoginState = !state.userLoginState;
    },
    toggleUserModalShow(state) {
      state.userModalShow = !state.userModalShow;
    }
  },
  actions: {
    toggleUserInterface(context) {
      context.commit('toggleUserInterface')
    }
  }
  
});
