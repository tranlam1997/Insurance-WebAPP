<template>
  <!-- The Modal -->
  <div
    class="modal-login flex flex-row justify-center align-center"
    :class="{ hidden: !loginState }"
  >
    <div
      class="text-white text-center font-bold px-8 py-2 text-l fixed top-0 rounded-3xl z-10 flex justify-center align-center"
      v-if="login_show_alert"
      :class="login_alert_variant"
    >
      {{ login_alert_msg }}
    </div>

    <!-- Modal Content -->
    <vee-form
      class="modal-content animate flex flex-col p-8 max-w-max"
      :validation-schema="loginSchema"
      @submit="login"
    >
      <div class="header-login m-auto">
        <img
          src="../../public/assets/img/logo-brand.png"
          class="container"
          alt="logo brand"
        />
      </div>
      <div class="container flex flex-col">
        <div>
          <label for="email" class="inline-block mb-2"
            ><b>Email Address</b></label
          >
          <vee-field
            type="email"
            placeholder="Enter Email Address"
            name="email"
            size="50"
            required
            v-model="email"
            class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
          />
          <ErrorMessage class="text-red-600" name="email" />
        </div>

        <div>
          <div class="flex flex-row justify-between">
            <label for="password" class="inline-block mb-2"
              ><b>Password</b></label
            >
            <span class="self-center text-sm">
              <a href="#" class="text-blue-500 hover:text-blue-600 font-medium"
                >Forgot password?</a
              ></span
            >
          </div>
          <vee-field
            type="password"
            placeholder="Enter Password"
            name="password"
            size="50"
            required
            v-model="password"
            class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
          />
          <ErrorMessage class="text-red-600" name="password" />
        </div>

        <button
          type="submit"
          :disabled="!enableSubmit()"
          :class="{
            'opacity-50': !enableSubmit(),
            'cursor-not-allowed': !enableSubmit(),
          }"
          class="mt-4 block w-full bg-blue-600 text-white py-1.5 px-3 rounded transition hover:bg-blue-700"
        >
          Login
        </button>
        <label>
          <input type="checkbox" checked="checked" name="remember" /> Remember
          me
        </label>
      </div>

      <div class="footerLogin container flex flex-row mt-2 justify-between">
        <button
          type="button"
          onclick="document.getElementById('id01').style.display='none'"
          class="cancelbtn block rounded text-white py-2 px-3"
          @click.prevent="toggleLoginModal"
        >
          Cancel
        </button>
        <span class="psw self-center text-sm"
          >New to Psawn Insurance?
          <a
            href="#"
            class="text-blue-500 hover:text-blue-600 font-medium underline"
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

export default {
  name: "LoginForm",
  computed: mapState({
    loginState: (state) => state.loginModalShow,
  }),
  data() {
    return {
      userToken: "",
      email: "",
      password: "",
      loginSchema: {
        email: "required|email",
        password: "required",
      },
      login_in_submission: false,
      login_show_alert: false,
      login_alert_variant: "bg-blue-500",
      login_alert_msg: "Please wait! We are logging you in.",
    };
  },
  methods: {
    ...mapMutations([
      "toggleLoginModal",
      "toggleRegisterModal",
      "toggleBetweenLoginAndRegisterModal",
      "toggleUserInterface",
    ]),
    async login() {
      try {
        this.login_in_submission = true;
        this.login_show_alert = true;
        this.login_alert_variant = "bg-blue-500";
        this.login_alert_msg = "Please wait! We are logging you in.";
        const response = await this.axios({
          method: "post",
          url: `https://localhost:44312/api/User/login`,
          data: {
            email: this.email,
            password: this.password,
          },
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Content-type": "application/json",
          },
        });
        this.userToken = response.data.token;
        this.login_alert_variant = "bg-green-500";
        this.login_alert_msg = "Success! You are now logged in.";
        setTimeout(() => {
          this.turnOffLoginModal();
        }, 500);
        this.$store.dispatch("toggleUserInterface");
      } catch (error) {
        if (error.response) {
          this.login_alert_msg = error.response.data?.title
            ? error.response.data.title
            : error.response.data.message;
          this.login_alert_variant = "bg-red-500";
          this.login_show_alert = true;
          setTimeout(() => {
            this.login_show_alert = false;
            this.login_alert_variant = "bg-blue-500";
          }, 2000);
        } else if (error.request) {
          console.log(error.request);
        } else {
          console.log("Error", error.message);
        }
      }
    },
    turnOffLoginModal() {
      this.login_show_alert = false;
      this.toggleLoginModal();
    },
    enableSubmit() {
      return this.password && this.email;
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
