
export const toggle =  {
  namespaced: true,
  state: {
    loginModalShow: false,
    registerModalShow: false,
    userModalShow: false,
    editModalShow: false,
    changePasswordModalShow: false,
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
    },
    toggleEditModalShow(state) {
      state.editModalShow = !state.editModalShow;
    },
    toggleChangePasswordModalShow(state) {
      state.changePasswordModalShow = !state.changePasswordModalShow;
    }
  }
};
