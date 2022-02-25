<template>
  <!-- Header -->
  <header id="header" class="bg-blue-800 ">
    <nav class="container m-auto flex justify-between py-3 px-4">
      <!-- App Name -->
      <router-link class="text-white font-bold uppercase text-2xl mr-4 self-center"
        :to="{ name: 'Home' }" exact-active-class="no-active">
        Psawn Insurance
      </router-link>

      <div class="flex items-center text-base">
        <!-- Primary Navigation -->
        <ul class="flex flex-row mt-1">
          <!-- Navigation Links -->
          <li class="self-center">
            <router-link class="px-2 text-white" :to="{ name: 'About' }">
              About
            </router-link>
          </li>
          <li class="self-center">
            <a class="px-2 text-white" href="#" @click.prevent="toggleLoginModal">
              Login
            </a>
          </li>
          <li>
            <button type="button" class="buttonRegister p-2 rounded text-white bg-orange-700 ml-1" href="#" @click.prevent="toggleRegisterModal">
              Register
            </button>
          </li>
        </ul>
      </div>
    </nav>
  </header>
</template>

<script>
import { mapMutations } from 'vuex';

export default {
  name: 'Header',
  methods: {
    ...mapMutations(['toggleLoginModal', 'toggleRegisterModal']),
  },
};
</script>

<style scoped lang="scss">
.buttonRegister {
  background-color:  #FF7F24;
  &:hover {
    background-color:  #fc6d07;
  }
}
</style>
