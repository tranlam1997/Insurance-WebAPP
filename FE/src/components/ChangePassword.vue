<template>
  <div class="change-password justify-center align-center">
    <div
      class="error-message text-white text-center font-bold px-8 py-2 text-l fixed top-0 rounded-3xl z-10 flex justify-center align-center"
      v-if="change_pass_show_alert"
      :class="change_pass_alert_variant"
    >
      {{ change_pass_alert_msg }}
    </div>
    <div
      class="modal-content-v1 2xl:w-1/3 xl:w-1/3 lg:w-2/4 md:w-2/4 sm:w-3/4 w-3/4 fixed top-60 2xl:left-1/3 xl:left-1/3 lg:left-1/4 md:left-1/4 sm:left-24 left-16 bg-gray-200"
    >
      <vee-form class="animate flex flex-col p-8" @submit="authenPass">
        <div class="container flex flex-col">
          <div>
            <label for="email" class="inline-block mb-2"
              ><b>Current Password</b></label
            >
            <div class="flex flex-row">
              <vee-field
                :type="showType"
                placeholder="Enter Current Password"
                name="currentPassword"
                size="50"
                required
                v-model="currentPassword"
                class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
              />
              <div
                class="control self-center border border-gray-300 bg-white py-2 px-3 rounded"
                @click="togglePassword"
              >
                <span :class="{ hidden: showPassword }">
                  <i class="fa-solid fa-eye"></i>
                </span>
                <span :class="{ hidden: !showPassword }">
                  <i class="fa-solid fa-eye-slash"></i>
                </span>
              </div>
            </div>

            <ErrorMessage class="text-red-600" name="currentPassword" />
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
            Next
          </button>
        </div>
      </vee-form>
      <div class="footerLogin container flex flex-row mt-2 justify-between">
        <button
          type="button"
          onclick="document.getElementById('id01').style.display='none'"
          class="cancelbtn block rounded text-white py-2 px-3 border bg-blue-500 hover:bg-blue-600 ml-8 mb-4 -mt-4"
          @click.prevent="turnOffModal"
        >
          Cancel
        </button>
      </div>
    </div>

    <div
      class="modal-content-v2 2xl:w-1/3 xl:w-1/3 lg:w-2/4 md:w-2/4 sm:w-3/4 w-3/4 fixed top-60 2xl:left-1/3 xl:left-1/3 lg:left-1/4 md:left-1/4 sm:left-24 left-16 bg-gray-200"
      v-if="nextChangePassword"
    >
      <vee-form
        :validation-schema="changePassSchema"
        class="animate flex flex-col p-8 gap-2"
        @submit="handleSubmit"
        ref="form"
      >
        <div class="container flex flex-col gap-2">
          <div>
            <label for="email" class="inline-block mb-2"
              ><b>New Password</b></label
            >
            <div class="flex flex-row">
              <vee-field
                :type="showType"
                placeholder="Enter New Password"
                name="password"
                size="50"
                required
                v-model="newPassword"
                class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
              />
              <div
                class="control self-center border border-gray-300 bg-white py-2 px-3 rounded"
                @click="togglePassword"
              >
                <span :class="{ hidden: showPassword }">
                  <i class="fa-solid fa-eye"></i>
                </span>
                <span :class="{ hidden: !showPassword }">
                  <i class="fa-solid fa-eye-slash"></i>
                </span>
              </div>
            </div>

            <ErrorMessage class="text-red-600" name="password" />
          </div>

          <div>
            <label for="email" class="inline-block mb-2"
              ><b>Confirm Password</b></label
            >
            <vee-field
              type="password"
              placeholder="Confirm New Password"
              name="confirm_password"
              size="50"
              required
              v-model="confirmNewPassword"
              class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
            />
            <ErrorMessage class="text-red-600" name="confirm_password" />
          </div>

          <button
            type="submit"
            :disabled="!enableSubmitV2()"
            :class="{
              'opacity-50': !enableSubmitV2(),
              'cursor-not-allowed': !enableSubmitV2(),
            }"
            class="mt-4 block w-full bg-blue-600 text-white py-1.5 px-3 rounded transition hover:bg-blue-700"
          >
            Submit
          </button>
        </div>
      </vee-form>
      <button
        type="button"
        onclick="document.getElementById('id01').style.display='none'"
        class="cancelbtn block rounded text-white py-2 px-3 border bg-blue-500 hover:bg-blue-600 ml-8 mb-4 -mt-4"
        @click.prevent="turnOffModal"
      >
        Cancel
      </button>
    </div>
  </div>
</template>

<script>
import { mapState, mapMutations } from "vuex";
import UserService from "../services/user.service.js";
export default {
  name: "ChangePassword",
  changePasswordState: (state) => state.toggle.changePasswordModalShow,
  computed: mapState({}),
  props: ["userPassword"],
  data() {
    return {
      changePassSchema: {
        password: "password",
        confirm_password: "passwords_mismatch:@password",
      },
      nextChangePassword: false,
      change_pass_alert_msg: "Password not correct",
      change_pass_show_alert: false,
      change_pass_alert_variant: "bg-red-600",
      currentPassword: "",
      newPassword: "",
      confirmNewPassword: "",
      showType: "password",
      showPassword: false,
    };
  },
  methods: {
    ...mapMutations([
      "toggle/toggleEditModalShow",
      "toggle/toggleChangePasswordModalShow",
    ]),
    toggleChangePasswordModalShow() {
      this["toggle/toggleChangePasswordModalShow"]();
    },
    authenPass() {
      if (this.userPassword === this.currentPassword) {
        (this.showType = "password"),
          (this.showPassword = false),
          (this.nextChangePassword = true);
      } else {
        this.change_pass_show_alert = true;
        setTimeout(() => {
          this.change_pass_show_alert = false;
        }, 3000);
      }
    },
    async handleSubmit() {
      try {
        const response = await UserService.changeUserPassword({
          password: this.currentPassword,
          newPassword: this.newPassword,
        });
        this.change_pass_show_alert = true;
        (this.change_pass_alert_msg = response.data.message),
          (this.change_pass_alert_variant = "bg-green-600"),
          setTimeout(() => {
            this.change_pass_show_alert = false;
          }, 500);
        const user = JSON.parse(localStorage.getItem("user"));
        localStorage.setItem(
          "user",
          JSON.stringify({
            ...user,
            password: this.newPassword,
          })
        );
        window.location.reload();
      } catch (err) {
        this.change_pass_show_alert = true;
        (this.change_pass_alert_variant = "bg-red-600"),
          (this.change_pass_alert_msg = "Something went wrong"),
          setTimeout(() => {
            this.change_pass_show_alert = false;
          }, 2000);
      }
    },
    enableSubmit() {
      return this.currentPassword;
    },
    enableSubmitV2() {
      return this.newPassword && this.confirmNewPassword;
    },
    turnOffModal() {
      this.toggleChangePasswordModalShow();
      this.$router.push("/profile");
    },
    togglePassword() {
      this.showPassword = !this.showPassword;
      this.showType = this.showType === "password" ? "text" : "password";
    },
  },
  created() {},
};
</script>

<style scoped lang="scss">
.change-password {
  width: 100vw;
  height: 100vh;
  background-color: rgba(0, 0, 0, 0.5);
  position: fixed;
  top: 0;
  left: 0;
}

.error-message {
  left: 40%;
}
</style>
