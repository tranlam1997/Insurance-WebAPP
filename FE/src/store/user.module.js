export const user =  {
    namespaced: true,
    state: {
      loginModalShow: false,
      registerModalShow: false,
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
      toggleUserModalShow(state) {
        state.userModalShow = !state.userModalShow;
      }
    }, 
  };