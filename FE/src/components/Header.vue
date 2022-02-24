<template>
  <!-- Header -->
  <header id="header" class="bg-gray-700">
    <nav class="container m-auto flex justify-between py-5 px-4">
      <!-- App Name -->
      <router-link class="text-white font-bold uppercase text-2xl mr-4"
        :to="{ name: 'Home' }" exact-active-class="no-active">
        Psawn Insurance
      </router-link>

      <div class="flex items-center text-base">
        <!-- Primary Navigation -->
        <ul class="flex flex-row mt-1">
          <!-- Navigation Links -->
          <li>
            <router-link class="px-2 text-white" :to="{ name: 'About' }">
              About
            </router-link>
          </li>
          <li>
            <a class="px-2 text-white" href="#" @click.prevent="toggleAuthModal">
              Login / Register
            </a>
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
    ...mapMutations(['toggleAuthModal']),

    // toggleAuthModal() {
    //   this.$store.commit('toggleAuthModal');
    // },
  },
};
</script>
