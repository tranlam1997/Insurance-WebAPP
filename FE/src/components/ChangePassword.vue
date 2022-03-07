<template>
  <div class="change-password tw-justify-center align-center">
    <div
      class="error-message tw-text-white tw-text-center tw-font-bold tw-px-8 tw-py-2 text-l tw-fixed tw-top-0 tw-rounded-3xl tw-z-10 tw-flex tw-justify-center align-center"
      v-if="change_pass_show_alert"
      :class="change_pass_alert_variant"
    >
      {{ change_pass_alert_msg }}
    </div>
    <div
      class="modal-content-v1 2xl:tw-w-1/3 xl:tw-w-1/3 lg:tw-w-2/4 md:tw-w-2/4 sm:tw-w-3/4 tw-w-3/4 tw-fixed tw-top-60 2xl:tw-left-1/3 xl:tw-left-1/3 lg:tw-left-1/4 md:tw-left-1/4 sm:tw-left-24 tw-left-16 tw-bg-gray-200"
    >
      <vee-form class="animate tw-flex tw-flex-col tw-p-8" @submit="authenPass">
        <div class="container tw-flex tw-flex-col">
          <div>
            <label for="currentPassword" class="tw-inline-block tw-mb-2"
              ><b>Current Password</b></label
            >
            <div class="tw-flex tw-flex-row">
              <vee-field
                :type="showType"
                placeholder="Enter Current Password"
                name="currentPassword"
                size="50"
                required
                v-model="currentPassword"
                class="tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
              />
              <div
                class="control tw-self-center tw-border tw-border-gray-300 tw-bg-white tw-py-2 tw-px-3 tw-rounded"
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

            <ErrorMessage class="tw-text-red-600" name="currentPassword" />
          </div>

          <button
            type="submit"
            :disabled="!enableSubmit()"
            :class="{
              'tw-opacity-50': !enableSubmit(),
              'tw-cursor-not-allowed': !enableSubmit(),
            }"
            class="tw-mt-4 tw-block tw-w-full tw-bg-blue-600 tw-text-white tw-py-1.5 tw-px-3 tw-rounded tw-transition hover:tw-bg-blue-700"
          >
            Next
          </button>
        </div>
      </vee-form>
      <div class="footerLogin container tw-flex tw-flex-row tw-mt-2 tw-justify-between">
        <button
          type="button"
          onclick="document.getElementById('id01').style.display='none'"
          class="cancelbtn tw-block tw-rounded tw-text-white tw-py-2 tw-px-3 tw-border tw-bg-blue-500 hover:tw-bg-blue-600 tw-ml-8 tw-mb-4 tw--mt-4"
          @click.prevent="turnOffModal"
        >
          Cancel
        </button>
      </div>
    </div>

    <div
      class="modal-content-v2 2xl:tw-w-1/3 xl:tw-w-1/3 lg:tw-w-2/4 md:tw-w-2/4 sm:tw-w-3/4 tw-w-3/4 tw-fixed tw-top-60 2xl:tw-left-1/3 xl:tw-left-1/3 lg:tw-left-1/4 md:tw-left-1/4 sm:tw-left-24 tw-left-16 tw-bg-gray-200"
      v-if="nextChangePassword"
    >
      <vee-form
        :validation-schema="changePassSchema"
        class="animate tw-flex tw-flex-col tw-p-8 tw-gap-2"
        @submit="handleSubmit"
        ref="form"
      >
        <div class="container tw-flex tw-flex-col tw-gap-2">
          <div>
            <label for="email" class="tw-inline-block tw-mb-2"
              ><b>New Password</b></label
            >
            <div class="tw-flex tw-flex-row">
              <vee-field
                :type="showType"
                placeholder="Enter New Password"
                name="password"
                size="50"
                required
                v-model="newPassword"
                class="tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
              />
              <div
                class="control tw-self-center tw-border tw-border-gray-300 tw-bg-white tw-py-2 tw-px-3 tw-rounded"
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

          <div>
            <label for="email" class="tw-inline-block tw-mb-2"
              ><b>Confirm Password</b></label
            >
            <vee-field
              type="password"
              placeholder="Confirm New Password"
              name="confirm_password"
              size="50"
              required
              v-model="confirmNewPassword"
              class="tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
            />
            <ErrorMessage class="tw-text-red-600" name="confirm_password" />
          </div>

          <button
            type="submit"
            :disabled="!enableSubmitV2()"
            :class="{
              'tw-opacity-50': !enableSubmitV2(),
              'tw-cursor-not-allowed': !enableSubmitV2(),
            }"
            class="tw-mt-4 tw-block tw-w-full tw-bg-blue-600 tw-text-white tw-py-1.5 tw-px-3 tw-rounded tw-transition hover:tw-bg-blue-700"
          >
            Submit
          </button>
        </div>
      </vee-form>
      <button
        type="button"
        onclick="document.getElementById('id01').style.display='none'"
        class="cancelbtn tw-block tw-rounded tw-text-white tw-py-2 tw-px-3 tw-border tw-bg-blue-500 hover:tw-bg-blue-600 tw-ml-8 tw-mb-4 tw--mt-4"
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
      change_pass_alert_variant: "tw-bg-red-600",
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
          (this.change_pass_alert_variant = "tw-bg-green-600"),
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
        (this.change_pass_alert_variant = "tw-bg-red-600"),
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
      this.$router.push("/user-profile");
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

label { 
  font-size: 1rem;
}
</style>
