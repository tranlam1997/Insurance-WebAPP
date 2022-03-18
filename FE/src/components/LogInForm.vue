<template>
  <!-- The Modal -->
  <div
    class="modal-login tw-flex tw-flex-row tw-justify-center align-center"
    :class="{ 'tw-hidden': !loginState }"
  >
    <div
      class="
        tw-text-white tw-text-center tw-font-bold tw-px-8 tw-py-2
        text-l
        tw-fixed tw-top-0 tw-rounded-3xl tw-z-10 tw-flex tw-justify-center
        align-center
      "
      v-if="login_show_alert"
      :class="login_alert_variant"
    >
      {{ login_alert_msg }}
    </div>

    <!-- Modal Content -->
    <vee-form
      class="modal-content animate tw-flex tw-flex-col tw-p-8 tw-max-w-max"
      :validation-schema="loginSchema"
      @submit="handleLogin"
      ref="form"
    >
      <div class="header-login tw-m-auto">
        <img
          src="../../public/assets/img/logo-brand.png"
          class="container"
          alt="logo brand"
        />
      </div>
      <div class="container tw-flex tw-flex-col">
        <div>
          <label for="email" class="tw-inline-block tw-mb-2"
            ><b>Email Address</b></label
          >
          <vee-field
            type="email"
            placeholder="Enter Email Address"
            name="email"
            size="50"
            required
            v-model="user.email"
            class="
              tw-block
              tw-w-full
              tw-py-2
              tw-px-3
              tw-text-gray-800
              tw-border
              tw-border-gray-300
              tw-transition
              tw-duration-500
              focus:tw-outline-none focus:tw-border-black
              tw-rounded
            "
          />
          <ErrorMessage class="tw-text-red-600" name="email" />
        </div>

        <div>
          <div class="tw-flex tw-flex-row tw-justify-between">
            <label for="password" class="tw-inline-block tw-mb-2"
              ><b>Password</b></label
            >
            <span class="tw-self-center tw-text-sm">
              <a
                href="#"
                class="tw-text-blue-500 hover:tw-text-blue-600 tw-font-medium"
                >Forgot password?</a
              ></span
            >
          </div>
          <div class="tw-flex tw-flex-row">
            <vee-field
              :type="showType"
              placeholder="Enter Password"
              name="password"
              size="20"
              required
              v-model="user.password"
              class="
                tw-w-full
                tw-py-2
                tw-px-3
                tw-text-gray-800
                tw-border
                tw-border-gray-300
                tw-transition
                tw-duration-500
                focus:tw-outline-none focus:tw-border-black
                tw-rounded
              "
            />
            <div
              class="
                control
                tw-self-center
                tw-border
                tw-border-gray-300
                tw-bg-white
                tw-py-2
                tw-px-3
                tw-rounded
              "
              @click="togglePassword"
            >
              <span :class="{ 'tw-hidden': showPassword }">
                <i class="fa-solid fa-eye"></i>
              </span>
              <span :class="{ 'tw-hidden': !showPassword }">
                <i class="fa-solid fa-eye-slash"></i>
              </span>
            </div>
          </div>

          <ErrorMessage class="tw-text-red-600" name="password" />
        </div>

        <button
          type="submit"
          :disabled="!enableSubmit()"
          :class="{
            'tw-opacity-50': !enableSubmit(),
            'tw-cursor-not-allowed': !enableSubmit(),
          }"
          class="
            tw-mt-4
            tw-block
            tw-w-full
            tw-bg-blue-600
            tw-text-white
            tw-py-1.5
            tw-px-3
            tw-rounded
            tw-transition
            hover:tw-bg-blue-700
          "
        >
          Login
        </button>
        <label>
          <input
            type="checkbox"
            checked="checked"
            v-model="rememberMe"
            name="remember"
          />
          Remember me
        </label>
      </div>

      <div
        class="
          footerLogin
          container
          tw-flex tw-flex-row tw-mt-2 tw-justify-between
        "
      >
        <button
          type="button"
          onclick="document.getElementById('id01').style.display='none'"
          class="cancelbtn tw-block tw-rounded tw-text-white tw-py-2 tw-px-3"
          @click.prevent="toggleLoginModal"
        >
          Cancel
        </button>
        <span class="psw tw-self-center tw-text-sm"
          >New to Psawn Insurance?
          <a
            href="#"
            class="
              tw-text-blue-500
              hover:tw-text-blue-600
              tw-font-medium tw-underline
            "
            @click.prevent="toggleBetweenLoginAndRegisterModal"
            >Sign up</a
          ></span
        >
      </div>
    </vee-form>
  </div>
</template>

<script>
import { mapState } from "vuex";
import { mapMutations } from "vuex";
import User from "../models/user";
import jwt_decode from "jwt-decode";

export default {
  name: "LoginForm",
  computed: mapState({
    loginState: (state) => state.toggle.loginModalShow,
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
  }),
  data() {
    return {
      rememberMe: false,
      showType: "password",
      showPassword: false,
      userToken: "",
      user: new User("", "", "", "", "", "", "", "", ""),
      loginSchema: {
        email: "required|email",
        password: "required",
      },
      login_in_submission: false,
      login_show_alert: false,
      login_alert_variant: "tw-bg-blue-500",
      login_alert_msg: "Please wait! We are logging you in.",
      time: null,
    };
  },
  created() {
    if (localStorage.getItem("rememberMe")) {
      this.rememberMe = true;
      this.user.email = localStorage.getItem("email");
    }
    if (this.loggedIn) {
      this.$router.push("/profile");
    }
  },
  methods: {
    ...mapMutations([
      "toggle/toggleLoginModal",
      "toggle/toggleRegisterModal",
      "toggle/toggleBetweenLoginAndRegisterModal",
    ]),
    togglePassword() {
      this.showPassword = !this.showPassword;
      this.showType = this.showType === "password" ? "text" : "password";
    },
    toggleLoginModal() {
      this.$refs.form.resetForm();
      this.login_show_alert = false;
      this["toggle/toggleLoginModal"]();
    },
    toggleRegisterModal() {
      this.$refs.form.resetForm();
      this.login_show_alert = false;
      this["toggle/toggleRegisterModal"]();
    },
    toggleBetweenLoginAndRegisterModal() {
      this.$refs.form.resetForm();
      this.login_show_alert = false;
      this["toggle/toggleBetweenLoginAndRegisterModal"]();
    },
    async handleLogin() {
      this.isRememberMe();
      try {
        this.login_in_submission = true;
        this.login_show_alert = true;
        this.login_alert_variant = "tw-bg-blue-500";
        this.login_alert_msg = "Please wait! We are logging you in.";
        this.time = setTimeout(this.requestTimeOut, 5000);
        const response = await this.$store.dispatch("auth/login", this.user);
        this.login_alert_variant = "tw-bg-green-500";
        this.login_alert_msg = "Success! You are now logged in.";
        if (this.isAdmin(response.token)) {
          setTimeout(() => {
            this.login_show_alert = false;
            this.$refs.form.resetForm();
          }, 1500);
          this.$router.push("/admin");
        } else {
          setTimeout(() => {
            this.login_show_alert = false;
            this.$refs.form.resetForm();
          }, 1500);
          const { id } = JSON.parse(localStorage.getItem("user"));
          this.$router.push({ name: "User", params: { id } });
        }
        setTimeout(() => {
          this.toggleLoginModal();
        }, 500);
      } catch (error) {
        if (error.response) {
          clearTimeout(this.time);
          this.login_alert_msg = error.response.data?.title
            ? error.response.data.title
            : error.response.data.message;
          this.login_alert_variant = "tw-bg-red-500";
          this.login_show_alert = true;
          setTimeout(() => {
            this.login_show_alert = false;
            this.login_alert_variant = "tw-bg-blue-500";
          }, 2000);
        } else if (error.request) {
          console.log(error.request);
        } else {
          console.log("Error", error.message);
        }
      }
    },
    requestTimeOut() {
      if (this.loginState || !this.loggedIn) {
        this.login_alert_variant = "tw-bg-red-500";
        this.login_show_alert = true;
        this.login_alert_msg = "Request timed out. Please try again.";
        setTimeout(() => {
          this.login_show_alert = false;
        }, 2000);
      }
    },
    enableSubmit() {
      return this.user.password && this.user.email;
    },
    emptyInputData() {
      this.user.email = "";
      this.user.password = "";
      this.rememberMe = false;
    },
    isRememberMe() {
      if (
        this.rememberMe &&
        this.user.email !== "" &&
        this.user.password !== ""
      ) {
        localStorage.setItem("email", this.user.email);
        localStorage.setItem("rememberMe", this.rememberMe);
      } else {
        localStorage.removeItem("email");
        localStorage.removeItem("rememberMe");
      }
    },
    isAdmin(token) {
      const decoded = jwt_decode(token);
      const role =
        decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      console.log(role);
      if (role === "User") {
        return false;
      }
      return true;
    },
  },
};
</script>

<style scoped lang="scss">
.modal-login {
  position: fixed; /* Stay in place */
  z-index: 1; /* Sit on top */
  left: 50%;
  top: 50%;
  width: 100%;
  height: 100%;
  transform: translate(-50%, -50%);
  overflow: auto; /* Enable scroll if needed */
  background-color: rgba(0, 0, 0, 0.7); /* Black w/ opacity */
}

/* Modal Content/Box */
.modal-content {
  position: fixed;
  top: 10%;
  background-color: #dcdcdc;
  margin: 5% auto 15% auto; /* 5% from the top, 15% from the bottom and centered */
  border: 1px solid #888;
  width: 80%; /* Could be more or less, depending on screen size */
}

.header-login {
  padding: 0;
  margin-top: -5rem;
  img {
    position: relative;
    border-radius: 50%;
    top: -5rem;
    height: auto;
    padding: 0;
    margin-bottom: -5rem;
  }
}

.cancelbtn {
  background-color: rgb(244 63 94);
  &:hover {
    background-color: rgb(238, 30, 65);
  }
}

label {
  margin: 1rem 0;
  font-size: 1rem;
}

/* Add Zoom Animation */
.animate {
  -webkit-animation: animatezoom 0.6s;
  animation: animatezoom 0.6s;
}

@-webkit-keyframes animatezoom {
  from {
    -webkit-transform: scale(0);
  }
  to {
    -webkit-transform: scale(1);
  }
}

@keyframes animatezoom {
  from {
    transform: scale(0);
  }
  to {
    transform: scale(1);
  }
}
</style>
