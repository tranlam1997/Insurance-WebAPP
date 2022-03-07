<template>
  <!-- The Modal -->
  <div
    id="register"
    class="modal-register tw-flex tw-flex-row tw-justify-center align-center"
    :class="{ 'tw-hidden': !registerState }"
  >
    <div
      class="tw-text-white tw-text-center tw-px-8 tw-py-2 tw-font-bold tw-h-14 text-l tw-sticky tw-top-0 tw-rounded-3xl tw-z-10 tw-flex tw-justify-center align-center"
      v-if="reg_show_alert"
      :class="reg_alert_variant"
    >
      {{ reg_alert_msg }}
    </div>
    <!-- Modal Content -->
    <vee-form
      :validation-schema="registerSchema"
      class="modal-content animate tw-flex tw-flex-col tw-p-8"
      @submit="submitRegister"
      ref="form"
    >
      <div class="header-register tw-text-black tw-text-3xl tw-font-black tw-text-center">
        Register
      </div>
      <div class="container tw-flex tw-flex-col">
        <div>
          <label for="firstName" class="tw-inline-block tw-mb-2"
            ><b>First Name</b></label
          >
          <vee-field
            type="text"
            placeholder="Enter First Name"
            name="firstName"
            size="50"
            required
            class="tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
            v-model="user.firstName"
          />
          <ErrorMessage class="tw-text-red-600" name="firstName" />
        </div>

        <div>
          <label for="lastName" class="tw-inline-block tw-mb-2"
            ><b>Last Name</b></label
          >
          <vee-field
            type="text"
            placeholder="Enter Last Name"
            name="lastName"
            size="50"
            required
            class="tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
            v-model="user.lastName"
          />
          <ErrorMessage class="tw-text-red-600" name="lastName" />
        </div>

        <div>
          <label for="dateOfBirth" class="tw-inline-block tw-mb-2"
            ><b>Date Of Birth</b></label
          >
          <vee-field
            type="date"
            placeholder="DD-MM-YYYY"
            name="dateOfBirth"
            size="50"
            required
            class="tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
            v-model="user.dateOfBirth"
          />
          <ErrorMessage class="tw-text-red-600" name="dateOfBirth" />
        </div>

        <div>
          <label for="email" class="tw-inline-block tw-mb-2"
            ><b>Email Address</b></label
          >
          <vee-field
            type="Email"
            placeholder="Enter Email Adress"
            name="email"
            size="50"
            required
            class="tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
            v-model="user.email"
          />
          <ErrorMessage class="tw-text-red-600" name="email" />
        </div>

        <div>
          <label for="address" class="tw-inline-block tw-mb-2"><b>Address</b></label>
          <vee-field
            type="text"
            placeholder="Enter Address"
            name="address"
            size="50"
            required
            class="tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
            v-model="user.address"
          />
          <ErrorMessage class="tw-text-red-600" name="address" />
        </div>

        <div>
          <label for="phoneNumber" class="tw-inline-block tw-mb-2"
            ><b>Phone Number</b></label
          >
          <vee-field
            type="text"
            placeholder="Enter Phone Number"
            name="phoneNumber"
            size="50"
            required
            class="tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
            v-model="user.phoneNumber"
          />
          <ErrorMessage class="tw-text-red-600" name="phoneNumber" />
        </div>

        <div class="">
          <label for="password" class="tw-inline-block tw-mb-2"
            ><b>Password</b></label
          >
          <div class="tw-flex tw-flex-row">
            <vee-field
              :type="showType"
              placeholder="Enter Password"
              name="password"
              size="10"
              required
              class="tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
              v-model="user.password"
            />
            <div
              class="control tw-self-center tw-border tw-border-gray-300 tw-bg-white tw-py-2 tw-px-3 tw-rounded"
              @click="togglePassword"
            >
              <span :class="{'tw-hidden': showPassword }">
                <i class="fa-solid fa-eye"></i>
              </span>
              <span :class="{'tw-hidden': !showPassword }">
                <i class="fa-solid fa-eye-slash"></i>
              </span>
            </div>
          </div>

          <ErrorMessage class="tw-text-red-600" name="password" />
        </div>

        <div>
          <label for="confirm_password" class="tw-inline-block tw-mb-2"
            ><b>Confirm Password</b></label
          >
          <vee-field
            type="password"
            placeholder="Confirm Password"
            name="confirm_password"
            size="50"
            required
            class="tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
            v-model="confirm_password"
          />
          <ErrorMessage class="tw-text-red-600" name="confirm_password" />
        </div>

        <div class="tw-mt-3 tw-pl-1 tw-flex tw-flex-row tw-gap-1">
          <vee-field
            type="checkbox"
            name="tos"
            value="1"
            class="tw-w-4 tw-h-4 tw-rounded tw-self-center"
            v-model="tos"
          />
          <label class="tos" keypath="register.accept" tag="label">
            <a
              class="tw-border-b tw-border-black"
              style="cursor: pointer"
              target="_blank"
              href="https://www.sec.gov/Archives/edgar/data/1222840/000122284008000014/exhibit10533insuranceriskmgm.htm"
              ><b>I agree on Terms and Condition</b></a
            >
          </label>
        </div>
        <ErrorMessage class="tw-text-red-600 tw-block" name="tos" />

        <button
          type="submit"
          :disabled="!enableRegisterButton()"
          :class="{
            'tw-opacity-50': !enableRegisterButton(),
            'tw-cursor-not-allowed': !enableRegisterButton(),
          }"
          class="tw-mt-4 tw-block tw-w-full tw-bg-blue-600 tw-text-white tw-py-1.5 tw-px-3 tw-rounded tw-transition hover:tw-bg-blue-700"
        >
          Submit
        </button>
      </div>

      <div class="footerLogin container tw-flex tw-flex-row tw-mt-8 tw-justify-between">
        <button
          type="button"
          onclick="document.getElementById('id01').style.display='none'"
          class="cancelbtn tw-block tw-rounded tw-text-white tw-py-2 tw-px-3"
          @click.prevent="toggleRegisterModal"
        >
          Cancel
        </button>
        <span class="psw tw-self-center"
          >Already have an account ?<a
            href="#"
            class="tw-text-blue-500 hover:tw-text-blue-600 tw-font-medium tw-underline"
            @click.prevent="toggleBetweenLoginAndRegisterModal"
            >Sign in</a
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

export default {
  name: "RegisterForm",
  computed: mapState({
    registerState: (state) => state.toggle.registerModalShow,
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
  }),
  mounted() {
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
    toggleLoginModal() {
      this.$refs.form.resetForm();
      this["toggle/toggleLoginModal"]();
    },
    toggleRegisterModal() {
      this.$refs.form.resetForm();
      this["toggle/toggleRegisterModal"]();
    },
    toggleBetweenLoginAndRegisterModal() {
      this.$refs.form.resetForm();
      this["toggle/toggleBetweenLoginAndRegisterModal"]();
    },
    async submitRegister() {
      try {
        this.reg_show_alert = true;
        this.reg_alert_variant = "bg-blue-500";
        this.reg_alert_msg = "Please wait! Your account is being created.";
        const response = await this.$store.dispatch("auth/register", this.user);
        this.reg_alert_variant = "bg-green-500";
        this.reg_alert_msg = response.message;
        setTimeout(() => {
          this.reg_show_alert = false;
          this.reg_alert_variant = "bg-green-500";
          this.reg_alert_msg = "Please wait! Your account is being created.";
          this.$refs.form.resetForm();
          this["toggle/toggleBetweenLoginAndRegisterModal"]();
        }, 1500);
      } catch (error) {
        if (error.response) {
          console.log(error.response);
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
    togglePassword() {
      this.showPassword = !this.showPassword;
      this.showType = this.showType === "password" ? "text" : "password";
    },
    enableRegisterButton() {
      return (
        this.user.firstName &&
        this.user.lastName &&
        this.user.dateOfBirth &&
        this.user.email &&
        this.user.address &&
        this.user.phoneNumber &&
        this.confirm_password &&
        this.tos
      );
    },
  },
  data() {
    return {
      user: new User("", "", "", "", "", "", "", ""),
      confirm_password: "",
      tos: false,
      registerSchema: {
        firstName: "required|min:3|max:100|name",
        lastName: "required|min:3|max:100|name",
        dateOfBirth: "required",
        email: "required|email",
        address: "required|min:3|max:100",
        phoneNumber: "required|phone",
        password: "required|password",
        confirm_password: "passwords_mismatch:@password",
        tos: "tos",
      },
      reg_show_alert: false,
      reg_alert_variant: "bg-blue-500",
      reg_alert_msg: "Please wait! Your account is being created.",
      showType: "password",
      showPassword: false,
    };
  },
};
</script>

<style scoped lang="scss">
.modal-register {
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
  top: -5%;
  background-color: #dcdcdc;
  margin: 5% auto 15% auto; /* 5% from the top, 15% from the bottom and centered */
  border: 1px solid #888;
  width: 30%; /* Could be more or less, depending on screen size */
}

.cancelbtn {
  background-color: rgb(244 63 94);
  &:hover {
    background-color: rgb(238, 30, 65);
  }
}

.tos {
  a {
    &:hover {
      color: black;
    }
  }
}

/* The Close Button (x) */
.close {
  position: absolute;
  right: 25px;
  top: 0;
  color: #000;
  font-size: 35px;
  font-weight: bold;
}

.close {
  &:hover,
  &:focus {
    color: red;
    cursor: pointer;
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
