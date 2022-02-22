<template>
<div class="d-flex flex-column">
<div class="container-fluid bg-white p-2 h6 d-flex flex-row justify-content-between">
        <router-link to="/"><img src="../assets/brand-logo.png" alt="" class="img-fluid" style="max-width: 120px"></router-link>
        <ul class="nav flex-row justify-content-end align-self-center">
          <li class="nav-item">
            <router-link class="nav-link pr-3 text-dark" to="/signin">Sign in</router-link>
          </li>
          <li class="nav-item">
            <router-link class="btn btn-outline-dark" to="/signup">Sign up</router-link>
          </li>
        </ul>
      </div>
      <router-view></router-view>
</div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'NavBar',
  data() {
    return {}
  },
});
</script>

<style scoped lang="scss">
    img {
        cursor: pointer;
    }
</style>
