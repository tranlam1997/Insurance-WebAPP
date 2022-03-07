<template>
  <!-- Header -->
  <header id="header" class="tw-bg-blue-800 tw-text-base">
    <nav class="tw-m-auto tw-flex tw-justify-between tw-py-1 tw-px-4">
      <!-- App Name -->
      <router-link
        class="
          tw-text-white
          tw-font-bold
          tw-uppercase
          tw-text-2xl
          tw-mr-4
          tw-self-center
        "
        :to="{ name: 'Home' }"
        exact-active-class="no-active"
      >
        Psawn Insurance
      </router-link>

      <div class="tw-text-base tw-text-white tw-self-center tw-m-0 tw-p-0">
        <!-- Primary Navigation -->
        <ul class="tw-flex tw-flex-row tw-mt-4 tw-gap-4 tw-self-center">
          <li class="tw-self-center tw-text-white">
            <router-link class="tw-text-white" :to="{ name: 'Home' }">
              Home
            </router-link>
          </li>
          <li class="dropdown-menu-tw tw-self-center">
            <a class="tw-text-white" href="">
              Products & Services <i class="fa-solid fa-angle-down"></i>
            </a>
            <ul
              class="
                dropdown-content-tw
                tw-flex-col
                tw-absolute
                tw-top-19
                tw-bg-blue-800
                tw-border-b
                tw-hidden
              "
            >
              <li class="tw-p-3 tw-mt-3">
                <a class="tw-text-white" href="/">Home Insurance</a>
              </li>
              <li class="tw-p-3">
                <a class="tw-text-white" href="/">Life Insurance</a>
              </li>
              <li class="tw-p-3">
                <a class="tw-text-white" href="/">Medical Insurance</a>
              </li>
              <li class="tw-p-3">
                <a class="tw-text-white" href="/">Vehicle Insurance</a>
              </li>
            </ul>
          </li>
          <!-- Navigation Links -->
          <li class="tw-self-center">
            <router-link class="tw-text-white" :to="{ name: 'About' }">
              About
            </router-link>
          </li>
          <li class="tw-self-center" :class="{ 'tw-hidden': loggedIn }">
            <div class="tw-flex tw-flex-row tw-gap-2">
              <a
                class="tw-self-center tw-text-white"
                href="#"
                @click.prevent="toggleLoginModal"
              >
                Login
              </a>
              <button
                type="button"
                class="
                  buttonRegister
                  tw-p-2 tw-rounded tw-text-white tw-bg-orange-700 tw-ml-1
                "
                href="#"
                @click.prevent="toggleRegisterModal"
              >
                Register
              </button>
            </div>
          </li>
          <li
            class="tw-self-center"
            :class="{ 'tw-hidden': !loggedIn }"
            v-click-outside="clickOutSide"
          >
            <a @click.prevent="toggleUserModalShow"
              ><i class="fa-solid fa-user text-l tw-mx-4 tw-cursor-pointer"></i
            ></a>
            <div
              class="
                dropdown-content
                tw-flex-col tw-absolute tw-top-19 tw-right-5 tw-bg-blue-900
              "
              v-if="userModalShow"
            >
              <li class="tw-px-6 tw-py-3 tw-mt-3" v-if="email">
                {{ email }}
              </li>
              <li class="tw-px-6 tw-py-3 tw-pb-4">
                <router-link to="/profile"
                  ><a href="/" class="tw-text-white">Profile</a></router-link
                >
              </li>
              <li class="tw-px-6 tw-pt-3 tw-pb-4">
                <a class="tw-text-white" href="/">Settings</a>
              </li>
              <li
                class="tw-px-6 tw-pb-2 tw-pt-3 tw-border-t"
                @click.prevent="logOut"
              >
                <a href="/" class="tw-text-white">Logout</a>
              </li>
            </div>
          </li>
        </ul>
      </div>
    </nav>
  </header>
</template>

<script>
import { mapMutations, mapState } from "vuex";

export default {
  name: "Header",
  data() {
    return {
      email: "",
    };
  },
  computed: mapState({
    userModalShow: (state) => state.toggle.userModalShow,
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
  }),
  watch: {
    loggedIn: {
      handler(newValue) {
        if (newValue) {
          this.email = JSON.parse(localStorage.getItem("user") ? localStorage.getItem("user"): localStorage.getItem("admin")).email;
        }
      },
      immediate: true,
    },
  },
  methods: {
    ...mapMutations([
      "toggle/toggleLoginModal",
      "toggle/toggleRegisterModal",
      "toggle/toggleUserModalShow",
    ]),
    toggleLoginModal() {
      this["toggle/toggleLoginModal"]();
    },
    toggleRegisterModal() {
      this["toggle/toggleRegisterModal"]();
    },
    toggleUserModalShow() {
      this["toggle/toggleUserModalShow"]();
    },
    logOut() {
      this.$store.dispatch("auth/logout");
      this.$router.push("/");
    },
    clickOutSide() {
      if (this.userModalShow) {
        this.toggleUserModalShow();
      }
    },
  },
};
</script>

<style scoped lang="scss">
.buttonRegister {
  background-color: #ff7f24;
  &:hover {
    background-color: #fc6d07;
  }
}

.dropdown-menu-tw:hover .dropdown-content-tw {
  display: flex;
}
</style>
