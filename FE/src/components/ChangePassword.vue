<template>
  <div class="change-password">
    <div class="fixed top-1/4 left-1/3 bg-gray-200">
      <vee-form
        class="modal-content animate flex flex-col p-8 max-w-max"
        @submit="authenPass"
        ref="form"
      >
        <div class="container flex flex-col">
          <div>
            <label for="email" class="inline-block mb-2"
              ><b>Current Password</b></label
            >
            <vee-field
              type="password"
              placeholder="Enter Current Password"
              name="currentPassword"
              size="50"
              required
              v-model="password"
              class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
            />
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

        <div class="footerLogin container flex flex-row mt-2 justify-between">
          <button
            type="button"
            onclick="document.getElementById('id01').style.display='none'"
            class="cancelbtn block rounded text-white py-2 px-3 border bg-blue-500 hover:bg-blue-600"
            @click.prevent="turnOffModal"
          >
            Cancel
          </button>
        </div>
      </vee-form>
    </div>
  </div>
</template>

<script>
import {mapState, mapMutations} from 'vuex';
export default {
  name: "ChangePassword",
    changePasswordState: (state) => state.toggle.changePasswordModalShow,
  computed: mapState({}),
  props: ["user"],
  data() {
    return {
      password: "",
    };
  },
  methods: {
      ...mapMutations(["toggle/toggleEditModalShow", "toggle/toggleChangePasswordModalShow"]),
    toggleChangePasswordModalShow() {
        this['toggle/toggleChangePasswordModalShow']();
    },
    authenPass() {
      if (this.user.password === this.password) {
        return true;
      }
    },
    enableSubmit() {
      return this.user.password;
    },
    turnOffModal() {
        this.toggleChangePasswordModalShow();
      this.$router.push('/profile');
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
</style>
