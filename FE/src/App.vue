<template>
  <router-view name="Header"></router-view>
  <router-view>
  </router-view>
  <router-view name="Footer"></router-view>
  <log-in-form></log-in-form>
  <register-form></register-form>
  <router-view name="Admin"></router-view>
</template>

<script>
import RegisterForm from './components/RegisterForm.vue'
import LogInForm from './components/LoginForm.vue'

export default {
  name: 'App',
  components: {
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