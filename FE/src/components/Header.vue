<template>
  <!-- Header -->
  <header id="header" class="bg-blue-800 text-base">
    <nav class="m-auto flex justify-between py-3 px-4">
      <!-- App Name -->
      <router-link class="text-white font-bold uppercase text-2xl mr-4 self-center"
        :to="{ name: 'Home' }" exact-active-class="no-active">
        Psawn Insurance
      </router-link>

      <div class="text-base text-white">
        <!-- Primary Navigation -->
        <ul class="flex flex-row mt-1 gap-4">
          <li class="self-center">
            <router-link class="" :to="{ name: 'Home'}">
              Home
            </router-link>
          </li>
          <li class="dropdown-menu self-center">
            <a href="
            ">
            Products & Services <i class="fa-solid fa-angle-down"></i>
            </a>
            <ul class="dropdown-content flex-col absolute top-19 bg-blue-800 border-b hidden">
              <li class="p-3 mt-3">
                <a href="">Health Insurance</a>
              </li>
              <li class="p-3">
                <a href="">Life Insurance</a>
              </li>
              <li class="p-3">
                <a href="">Body Insurance</a>
              </li>
              <li class="p-3">
                <a href="">Property Insurance</a>
              </li>
            </ul>
          </li>
          <!-- Navigation Links -->
          <li class="self-center">
            <router-link class="" :to="{ name: 'About' }">
              About
            </router-link>
          </li>
          <li class="self-center">
            <a class="" href="#" @click.prevent="toggleLoginModal">
              Login
            </a>
          </li>
          <li class="self-center">
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

.dropdown-menu:hover .dropdown-content {
      display: flex;
}
</style>
