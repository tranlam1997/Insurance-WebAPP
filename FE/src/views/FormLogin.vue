<template>
  <nav-bar @sign-in-toggle="showSignInMethod" @sign-up-toggle="showSignUpMethod" v-if="false"></nav-bar>
  <sign-in  v-if="showSignIn"></sign-in>
  <sign-up  v-if="showSignUp"></sign-up>
</template>

<script lang="ts">
import NavBar from '@/components/NavBar.vue';
import { defineComponent } from 'vue';
import SignIn from '../components/SignIn.vue'
import SignUp from '../components/SignUp.vue'

export default defineComponent({
  name: 'FormLogin',
  components: {SignIn, SignUp, NavBar},
  methods: {
    showSignInMethod() {
      this.showSignIn = !this.showSignIn;
    },
    showSignUpMethod() {
      this.showSignUp = !this.showSignUp;
    }
  },
 data() {
        return {
        showSignIn: false,
        showSignUp: false,
        showForgotPassword: false,
        }
    },
});
</script>

<style scoped lang="scss">
  .background-login {
      background: red;
    background-image: url("../assets/login-background.jpg");
    background-repeat: no-repeat;
    background-size: cover;
    height: 100vh;
  }
</style>
