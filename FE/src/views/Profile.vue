<template>
  <!------ Include the above in your HEAD tag ---------->
  <div
    class="tw-text-white tw-text-center tw-font-bold tw-px-8 tw-py-2 text-l tw-fixed tw-top-1/6 tw-rounded-3xl tw-z-10 tw-flex tw-justify-center align-center"
    v-if="edit_show_alert"
    :class="edit_alert_variant"
    style="left: 40%"
  >
    {{ edit_alert_msg }}
  </div>
  <div class="container profile tw-m-auto tw-my-24 tw-border tw-p-8">
    <div class="tw-flex tw-flex-row">
      <div class="tw-w-96">
        <div class="profile-img tw-flex-1 tw-w-64">
          <img
            src="../../public/assets/img/default-profile.jpg"
            alt=""
            class=""
          />
          <div class="file tw-top-14 tw-text-white">
            Change Photo
            <input type="file" class="tw-cursor-pointer" name="file" />
          </div>
        </div>
      </div>
      <div class="tw-self-start tw-flex tw-flex-col tw-gap-8 tw-w-full">
        <div class="tw-flex tw-flex-row tw-gap-10 tw-justify-between">
          <div class="profile-head">
            <h5>{{ user.data.firtsName + " " + user.data.lastName }}</h5>
            <h6>Web Developer and Designer</h6>
            <ul class="tw-flex tw-flex-row tw-gap-2 tw-pt-8" id="myTab" role="tablist">
              <li class="nav-item">
                <a
                  class="nav-link active"
                  id="home-tab"
                  data-toggle="tab"
                  href="#home"
                  role="tab"
                  aria-controls="home"
                  aria-selected="true"
                  >Profile</a
                >
              </li>
              /
              <li class="nav-item">
                <a
                  class="nav-link"
                  id="profile-tab"
                  data-toggle="tab"
                  href="#profile"
                  role="tab"
                  aria-controls="profile"
                  aria-selected="false"
                  >Insurance Collection</a
                >
              </li>
            </ul>
          </div>
          <div class="">
            <button
              type="button"
              class="tw-border tw-bg-gray-300 tw-p-2 2xl:tw-text-base xl:tw-text-base lg:tw-text-sm md:tw-text-sm sm:tw-text-xs tw-text-xs"
              name="btnAddMore"
              @click.prevent="toggleEditModalShow"
            >
              Edit Profile
            </button>
          </div>
        </div>
        <vee-form
          :validation-schema="editSchema"
          class="modal-content animate tw-flex tw-flex-col tw-p-8"
          @submit="handleEdit"
          ref="form"
        >
          <div class="tw-flex tw-flex-col tw-gap-3">
            <div class="tw-flex tw-flex-row" :class="{ 'tw-hidden': editState }">
              <p class="tw-w-48 tw-font-bold">ID:</p>
              <p class="">{{ user.data.id }}</p>
            </div>
            <div class="tw-flex tw-flex-row" :class="{ 'tw-hidden': editState }">
              <p class="tw-w-48 tw-font-bold">Name:</p>
              <p class="tw-flex-1">
                {{ user.data.firtsName + " " + user.data.lastName }}
              </p>
            </div>
            <div class="tw-flex tw-flex-row" :class="{ 'tw-hidden': !editState }">
              <label class="tw-w-48 tw-font-bold tw-self-start">First name:</label>
              <div class="tw-flex tw-flex-col">
                <vee-field
                  type="text"
                  placeholder="Enter First Name"
                  name="firstName"
                  size="50"
                  required
                  class="tw-flex-1 tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
                  v-model="userData.firstName"
                />
                <ErrorMessage class="tw-text-red-600" name="firstName" />
              </div>
            </div>
            <div class="tw-flex tw-flex-row" :class="{ 'tw-hidden': !editState }">
              <label class="tw-w-48 tw-font-bold tw-self-start">Last name:</label>
              <div class="tw-flex tw-flex-col">
                <vee-field
                  type="text"
                  placeholder="Enter Last Name"
                  name="lastName"
                  size="50"
                  required
                  :class="{ 'tw-hidden': !editState }"
                  class="tw-flex-1 tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
                  v-model="userData.lastName"
                />
                <ErrorMessage class="tw-text-red-600" name="lastName" />
              </div>
            </div>
            <div class="tw-flex tw-flex-row">
              <label class="tw-w-48 tw-font-bold tw-self-start">Date Of Birth:</label>
              <p class="tw-flex-1" :class="{ 'tw-hidden': editState }">
                {{ user.data.dateOfBirth }}
              </p>
              <div class="tw-flex tw-flex-col">
                <vee-field
                  type="date"
                  placeholder="DD-MM-YYYY"
                  name="dateOfBirth"
                  size="50"
                  required
                  :class="{ 'tw-hidden': !editState }"
                  class="tw-flex-1 tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
                  v-model="userData.dateOfBirth"
                />
                <ErrorMessage class="tw-text-red-600" name="dateOfBirth" />
              </div>
            </div>
            <div class="tw-flex tw-flex-row" :class="{ 'tw-hidden': editState }">
              <p class="tw-w-48 tw-font-bold">Email:</p>
              <p class="tw-flex-1">{{ user.data.email }}</p>
            </div>
            <div class="tw-flex tw-flex-row">
              <label class="tw-w-48 tw-font-bold tw-self-start">Phone:</label>
              <p class="tw-flex-1" :class="{ 'tw-hidden': editState }">
                {{ user.data.phoneNumber }}
              </p>
              <div class="tw-flex tw-flex-col">
                <vee-field
                  type="text"
                  placeholder="Enter Phone Number"
                  name="phoneNumber"
                  size="50"
                  required
                  :class="{ 'tw-hidden': !editState }"
                  class="tw-flex-1 tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
                  v-model="userData.phoneNumber"
                />
                <ErrorMessage class="tw-text-red-600" name="phoneNumber" />
              </div>
            </div>
            <div class="tw-flex tw-flex-row">
              <label class="tw-w-48 tw-font-bold tw-self-start">Address:</label>
              <p class="tw-flex-1" :class="{ 'tw-hidden': editState }">
                {{ user.data.address }}
              </p>
              <div class="tw-flex tw-flex-col">
                <vee-field
                  type="text"
                  placeholder="Enter Address"
                  name="address"
                  size="50"
                  required
                  :class="{ 'tw-hidden': !editState }"
                  class="tw-flex-1 tw-block tw-w-full tw-py-2 tw-px-3 tw-text-gray-800 tw-border tw-border-gray-300 tw-transition tw-duration-500 focus:tw-outline-none focus:tw-border-black tw-rounded"
                  v-model="userData.address"
                />
                <ErrorMessage class="tw-text-red-600" name="address" />
              </div>
            </div>
            <div class="tw-flex tw-flex-row" :class="{ 'tw-hidden': editState }">
              <p class="tw-w-48 tw-font-bold">Role:</p>
              <p class="tw-flex-1">{{ user.data.role }}</p>
            </div>
            <div class="tw-flex tw-flex-row" :class="{ 'tw-hidden': editState }">
              <p class="tw-w-48 tw-font-bold">Password:</p>
              <div>
                <p class="tw-flex-1 tw-self-center tw-mt-1">***********</p>
                <button
                  type="button"
                  class="tw-border tw-border-black tw-p-1 tw-mr-96 hover:tw-bg-gray-300"
                  @click.prevent="renderChangePassword"
                  :class="{hidden: changePasswordState}"
                >
                  Change password
                </button>
                <router-view :user-password="userPassword"></router-view>
              </div>
            </div>
            <button
              type="submit"
              :class="{ 'tw-hidden': !editState }"
              class="tw-border tw-border-black tw-p-1 tw-w-36 tw-ml-56 hover:tw-bg-gray-300"
            >
              Save changes
            </button>
          </div>
        </vee-form>
      </div>
    </div>
  </div>
</template>

<script>
import UserService from "../services/user.service.js";
import { mapState, mapMutations } from "vuex";
import User from "../models/user";

export default {
  name: "Profile",
  computed: {...mapState({
    editState: (state) => state.toggle.editModalShow,
    changePasswordState: (state) => state.toggle.changePasswordModalShow,
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
  }),
    userPassword() {
      return JSON.parse(localStorage.getItem("user")).password
    }
  },
  data() {
    return {
      editSchema: {
        firstName: "min:3|max:100|",
        lastName: "min:3|max:100|",
        address: "min:3|max:100",
      },
      userData: new User("", "", "", "", "", "", "", "", ""),
      user: null,
      edit_show_alert: false,
      edit_alert_variant: "bg-blue-500",
      edit_alert_msg: "Please wait!",
    };
  },
  methods: {
    ...mapMutations(["toggle/toggleEditModalShow", "toggle/toggleChangePasswordModalShow"]),
    toggleEditModalShow() {
      this.$refs.form.resetForm();
      this["toggle/toggleEditModalShow"]();
    },
    async handleEdit() {
      this.edit_show_alert = true,
        this.edit_alert_variant = "bg-blue-500",
        this.edit_alert_msg = "Please wait!",
        UserService.editUserInfo(this.userData)
          .then((response) => {
            this.edit_alert_variant = "bg-green-500";
            this.edit_alert_msg = "Successfully updated!";
            setTimeout(() => {
              this.edit_show_alert = false;
            }, 1500);
            UserService.getUserInfo().then((response) => {
              this.user = response.data;
            });
            this.toggleEditModalShow();
            return response;
          })
          .catch((error) => {
            (this.edit_alert_variant = "bg-red-500"),
              (this.edit_alert_msg = error),
              setTimeout(() => {
                this.edit_show_alert = false;
              }, 1500);
            console.log(error);
          });
    },
    renderChangePassword() {
      this.$router.push('/profile/changePassword');
      this['toggle/toggleChangePasswordModalShow']();
    },
  },
  created() {
    UserService.getUserInfo().then((response) => {
      this.user = response.data;
    });
  },
  mounted() {
    if (this.editState) {
      this.toggleEditModalShow();
    }
  },
};
</script>

<style scoped lang="scss">
.profile {
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
}
.emp-profile {
  padding: 3%;
  margin-top: 3%;
  margin-bottom: 3%;
  border-radius: 0.5rem;
  background: #fff;
}
.profile-img {
  text-align: center;
}
.profile-img img {
  width: 70%;
  height: 100%;
}
.profile-img .file {
  position: relative;
  overflow: hidden;
  margin-top: -20%;
  width: 70%;
  border: none;
  border-radius: 0;
  font-size: 15px;
  background: #212529b8;
}
.profile-img .file input {
  position: absolute;
  opacity: 0;
  right: 0;
  top: 0;
}
.profile-head h5 {
  color: #333;
}
.profile-head h6 {
  color: #0062cc;
}
.profile-edit-btn {
  border: none;
  border-radius: 1.5rem;
  width: 70%;
  padding: 2%;
  font-weight: 600;
  color: #6c757d;
  cursor: pointer;
}
.proile-rating {
  font-size: 12px;
  color: #818182;
  margin-top: 5%;
}
.proile-rating span {
  color: #495057;
  font-size: 15px;
  font-weight: 600;
}
.profile-head .nav-tabs {
  margin-bottom: 5%;
}
.profile-head .nav-tabs .nav-link {
  font-weight: 600;
  border: none;
}
.profile-head .nav-tabs .nav-link.active {
  border: none;
  border-bottom: 2px solid #0062cc;
}
.profile-work {
  padding: 14%;
  margin-top: -15%;
}
.profile-work p {
  font-size: 12px;
  color: #818182;
  font-weight: 600;
  margin-top: 10%;
}
.profile-work a {
  text-decoration: none;
  color: #495057;
  font-weight: 600;
  font-size: 14px;
}
.profile-work ul {
  list-style: none;
}
.profile-tab label {
  font-weight: 600;
}
.profile-tab p {
  font-weight: 600;
  color: #0062cc;
}
</style>
