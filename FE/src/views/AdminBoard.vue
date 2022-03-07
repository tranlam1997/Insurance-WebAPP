<template>
  <sidenav
    :custom_class="this.$store.state.admin.mcolor"
    :class="[
      this.$store.state.admin.isTransparent,
      this.$store.state.admin.isRTL ? 'fixed-end' : 'fixed-start',
    ]"
    v-if="this.$store.state.admin.showSidenav"
  />
  <main
    class="
      main-content
      position-relative
      max-height-vh-100
      h-100
      border-radius-lg
    "
    :style="this.$store.state.admin.isRTL ? 'overflow-x: hidden' : ''"
  >
    <!-- nav -->
    <navbar
      :class="[navClasses]"
      :textWhite="this.$store.state.admin.isAbsolute ? 'text-white opacity-8' : ''"
      :minNav="navbarMinimize"
      v-if="this.$store.state.admin.showNavbar"
    />
    <router-view />
    <app-footer v-show="this.$store.state.admin.showFooter" />
    <configurator
      :toggle="toggleConfigurator"
      :class="[
        this.$store.state.admin.showConfig ? 'show' : '',
        this.$store.state.admin.hideConfigButton ? 'd-none' : '',
      ]"
    />
  </main>
</template>

<script>
import Sidenav from "@/examples/Sidenav";
import Configurator from "@/examples/Configurator.vue";
import Navbar from "@/examples/Navbars/Navbar.vue";
import AppFooter from "@/examples/Footer.vue";
import { mapMutations } from "vuex";
export default {
  name: "AdminBoard",
  components: {
    Sidenav,
    Configurator,
    Navbar,
    AppFooter,
  },
  methods: {
    ...mapMutations(["toggleConfigurator", "navbarMinimize"]),
  },
  computed: {
    navClasses() {
      return {
        "position-sticky blur shadow-blur mt-4 left-auto top-1 z-index-sticky":
          this.$store.state.admin.isNavFixed,
        "position-absolute px-4 mx-0 w-100 z-index-2":
          this.$store.state.admin.isAbsolute,
        "px-0 mx-4 mt-4": !this.$store.state.admin.isAbsolute,
      };
    },
  },
  beforeMount() {
    this.$store.state.admin.isTransparent = "bg-transparent";
  },
};
</script>

<style></style>
