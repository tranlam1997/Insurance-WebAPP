<template>
  <!-- The Modal -->
  <div
    id="register"
    class="modal-register flex flex-row justify-center align-center"
    :class="{ hidden: !registerState }"
  >
    <div
      class="text-white text-center font-bold px-10 py-3 h-14 text-xl sticky top-0 rounded-3xl z-10"
      v-if="reg_show_alert"
      :class="reg_alert_variant"
    >
      {{ reg_alert_msg }}
    </div>
    <!-- Modal Content -->
    <vee-form
      :validation-schema="registerSchema"
      class="modal-content animate flex flex-col p-8 max-w-max"
      @submit="register"
    >
      <div class="header-register text-black text-3xl font-black text-center">
        Register
      </div>
      <div class="container flex flex-col">
        <div>
          <label for="firstName" class="inline-block mb-2"
            ><b>First Name</b></label
          >
          <vee-field
            type="text"
            placeholder="Enter First Name"
            name="firstName"
            size="50"
            required
            class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
            v-model="firstName"
          />
          <ErrorMessage class="text-red-600" name="firstName" />
        </div>

        <div>
          <label for="lastName" class="inline-block mb-2"
            ><b>Last Name</b></label
          >
          <vee-field
            type="text"
            placeholder="Enter Last Name"
            name="lastName"
            size="50"
            required
            class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
            v-model="lastName"
          />
          <ErrorMessage class="text-red-600" name="lastName" />
        </div>

        <div>
          <label for="dateOfBirth" class="inline-block mb-2"
            ><b>Date Of Birth</b></label
          >
          <vee-field
            type="date"
            placeholder="DD-MM-YYYY"
            name="dateOfBirth"
            size="50"
            required
            class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
            v-model="dateOfBirth"
          />
          <ErrorMessage class="text-red-600" name="dateOfBirth" />
        </div>

        <div>
          <label for="email" class="inline-block mb-2"
            ><b>Email Address</b></label
          >
          <vee-field
            type="Email"
            placeholder="Enter Email Adress"
            name="email"
            size="50"
            required
            class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
            v-model="email"
          />
          <ErrorMessage class="text-red-600" name="email" />
        </div>

        <div>
          <label for="address" class="inline-block mb-2"><b>Address</b></label>
          <vee-field
            type="text"
            placeholder="Enter Address"
            name="address"
            size="50"
            required
            class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
            v-model="address"
          />
          <ErrorMessage class="text-red-600" name="address" />
        </div>

        <div>
          <label for="phoneNumber" class="inline-block mb-2"
            ><b>Phone Number</b></label
          >
          <vee-field
            type="text"
            placeholder="Enter Phone Number"
            name="phoneNumber"
            size="50"
            required
            class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
            v-model="phoneNumber"
          />
          <ErrorMessage class="text-red-600" name="phoneNumber" />
        </div>

        <div>
          <label for="password" class="inline-block mb-2"
            ><b>Password</b></label
          >
          <vee-field
            type="password"
            placeholder="Enter Password"
            name="password"
            size="50"
            required
            class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
            v-model="password"
          />
          <ErrorMessage class="text-red-600" name="password" />
        </div>

        <div>
          <label for="confirm_password" class="inline-block mb-2"
            ><b>Confirm Password</b></label
          >
          <vee-field
            type="password"
            placeholder="Confirm Password"
            name="confirm_password"
            size="50"
            required
            class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
            v-model="confirm_password"
          />
          <ErrorMessage class="text-red-600" name="confirm_password" />
        </div>

        <div class="mt-3 pl-1 flex flex-row gap-1">
          <vee-field
            type="checkbox"
            name="tos"
            value="1"
            class="w-4 h-4 rounded self-center"
            v-model="tos"
          />
          <label class="tos" keypath="register.accept" tag="label">
            <a class="border-b border-black" style="cursor: pointer"
              ><b>I agree on Terms and Condition</b></a
            >
          </label>
        </div>
        <ErrorMessage class="text-red-600 block" name="tos" />

        <button
          type="submit"
          :disabled="!enableRegisterButton()"
          :class="{
            'opacity-50': !enableRegisterButton(),
            'cursor-not-allowed': !enableRegisterButton(),
          }"
          class="mt-4 block w-full bg-blue-600 text-white py-1.5 px-3 rounded transition hover:bg-blue-700"
        >
          Submit
        </button>
      </div>

      <div class="footerLogin container flex flex-row mt-8 justify-between">
        <button
          type="button"
          onclick="document.getElementById('id01').style.display='none'"
          class="cancelbtn block rounded text-white py-2 px-3"
          @click.prevent="toggleRegisterModal"
        >
          Cancel
        </button>
        <span class="psw self-center"
          >Already have an account ?<a
            href="#"
            class="text-blue-500 hover:text-blue-600 font-medium underline"
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

export default {
  name: "RegisterForm",
  computed: mapState({
    registerState: (state) => state.registerModalShow,
  }),
  methods: {
    ...mapMutations([
      "toggleLoginModal",
      "toggleRegisterModal",
      "toggleBetweenLoginAndRegisterModal",
    ]),
    register() {
      this.reg_show_alert = true;
      this.reg_alert_variant = "bg-blue-500";
      this.reg_alert_msg = "Please wait! Your account is being created.";

      setTimeout(() => {
        this.reg_alert_variant = "bg-green-500";
        this.reg_alert_msg = "Success! Your account has been created.";
        window.location.reload();
      }, 2000);
    },
    enableRegisterButton() {
      return (
        this.firstName &&
        this.lastName &&
        this.dateOfBirth &&
        this.email &&
        this.address &&
        this.phoneNumber &&
        this.password &&
        this.confirm_password
      );
    },
  },
  data() {
    return {
      firstName: "",
      lastName: "",
      dateOfBirth: "",
      email: "",
      address: "",
      phoneNumber: "",
      password: "",
      confirm_password: "",
      registerSchema: {
        firstName: "required|min:3|max:100|alpha_spaces",
        lastName: "required|min:3|max:100|alpha_spaces",
        dateOfBirth: "required",
        email: "required|email",
        address: "required|min:3|max:100",
        phoneNumber: "required|phone",
        password: "required|min:3|max:100",
        confirm_password: "passwords_mismatch:@password",
        tos: "tos",
      },
      userData: {
        country: "USA",
      },
      reg_show_alert: false,
      reg_alert_variant: "bg-blue-500",
      reg_alert_msg: "Please wait! Your account is being created.",
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
  width: 80%; /* Could be more or less, depending on screen size */
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
