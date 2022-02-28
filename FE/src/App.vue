<template>
  <app-header></app-header>
  <router-view v-slot="{ Component }">
    <transition name="fade" mode="out-in">
      <component :is="Component"></component>
    </transition>
  </router-view>
  <log-in-form></log-in-form>
  <register-form></register-form>
</template>

<script>
import AppHeader from './components/Header.vue'
import LoginForm from './components/LoginForm.vue'
import RegisterForm from './components/RegisterForm.vue'
import LogInForm from './components/LoginForm.vue'

export default {
  name: 'App',
  components: {
    AppHeader,
    LoginForm,
    RegisterForm,
    LogInForm
}
}
</script>

<style>
.fade-enter-from {
  opacity: 0;
}

.fade-enter-active {
  transition: all 0.3s linear;
}

.fade-leave-to {
  transition: all 0.3s linear;
  opacity: 0;
}
</style>