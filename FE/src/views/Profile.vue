<template>
  <!------ Include the above in your HEAD tag ---------->
  <div
    class="text-white text-center font-bold px-8 py-2 text-l fixed top-1/6 rounded-3xl z-10 flex justify-center align-center"
    v-if="edit_show_alert"
    :class="edit_alert_variant"
    style="left: 40%"
  >
    {{ edit_alert_msg }}
  </div>
  <div class="container profile m-auto my-24 border p-8">
    <div class="flex flex-row">
      <div class="w-96">
        <div class="profile-img flex-1 w-64">
          <img
            src="../../public/assets/img/default-profile.jpg"
            alt=""
            class=""
          />
          <div class="file top-14 text-white">
            Change Photo
            <input type="file" class="cursor-pointer" name="file" />
          </div>
        </div>
      </div>
      <div class="self-start flex flex-col gap-8 w-full">
        <div class="flex flex-row gap-10 justify-between">
          <div class="profile-head">
            <h5>{{ user.data.firtsName + " " + user.data.lastName }}</h5>
            <h6>Web Developer and Designer</h6>
            <ul class="flex flex-row gap-2 pt-8" id="myTab" role="tablist">
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
              class="border bg-gray-300 p-2 2xl:text-base xl:text-base lg:text-sm md:text-sm sm:text-xs text-xs"
              name="btnAddMore"
              @click.prevent="toggleEditModalShow"
            >
              Edit Profile
            </button>
          </div>
        </div>
        <vee-form
          :validation-schema="editSchema"
          class="modal-content animate flex flex-col p-8"
          @submit="handleEdit"
          ref="form"
        >
          <div class="flex flex-col gap-3">
            <div class="flex flex-row" :class="{ hidden: editState }">
              <p class="w-48 font-bold">ID:</p>
              <p class="">{{ user.data.id }}</p>
            </div>
            <div class="flex flex-row" :class="{ hidden: editState }">
              <p class="w-48 font-bold">Name:</p>
              <p class="flex-1">
                {{ user.data.firtsName + " " + user.data.lastName }}
              </p>
            </div>
            <div class="flex flex-row" :class="{ hidden: !editState }">
              <label class="w-48 font-bold self-start">First name:</label>
              <div class="flex flex-col">
                <vee-field
                  type="text"
                  placeholder="Enter First Name"
                  name="firstName"
                  size="50"
                  required
                  class="flex-1 block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
                  v-model="userData.firstName"
                />
                <ErrorMessage class="text-red-600" name="firstName" />
              </div>
            </div>
            <div class="flex flex-row" :class="{ hidden: !editState }">
              <label class="w-48 font-bold self-start">Last name:</label>
              <div class="flex flex-col">
                <vee-field
                  type="text"
                  placeholder="Enter Last Name"
                  name="lastName"
                  size="50"
                  required
                  :class="{ hidden: !editState }"
                  class="flex-1 block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
                  v-model="userData.lastName"
                />
                <ErrorMessage class="text-red-600" name="lastName" />
              </div>
            </div>
            <div class="flex flex-row">
              <label class="w-48 font-bold self-start">Date Of Birth:</label>
              <p class="flex-1" :class="{ hidden: editState }">
                {{ user.data.dateOfBirth }}
              </p>
              <div class="flex flex-col">
                <vee-field
                  type="date"
                  placeholder="DD-MM-YYYY"
                  name="dateOfBirth"
                  size="50"
                  required
                  :class="{ hidden: !editState }"
                  class="flex-1 block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
                  v-model="userData.dateOfBirth"
                />
                <ErrorMessage class="text-red-600" name="dateOfBirth" />
              </div>
            </div>
            <div class="flex flex-row" :class="{ hidden: editState }">
              <p class="w-48 font-bold">Email:</p>
              <p class="flex-1">{{ user.data.email }}</p>
            </div>
            <div class="flex flex-row">
              <label class="w-48 font-bold self-start">Phone:</label>
              <p class="flex-1" :class="{ hidden: editState }">
                {{ user.data.phoneNumber }}
              </p>
              <div class="flex flex-col">
                <vee-field
                  type="text"
                  placeholder="Enter Phone Number"
                  name="phoneNumber"
                  size="50"
                  required
                  :class="{ hidden: !editState }"
                  class="flex-1 block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
                  v-model="userData.phoneNumber"
                />
                <ErrorMessage class="text-red-600" name="phoneNumber" />
              </div>
            </div>
            <div class="flex flex-row">
              <label class="w-48 font-bold self-start">Address:</label>
              <p class="flex-1" :class="{ hidden: editState }">
                {{ user.data.address }}
              </p>
              <div class="flex flex-col">
                <vee-field
                  type="text"
                  placeholder="Enter Address"
                  name="address"
                  size="50"
                  required
                  :class="{ hidden: !editState }"
                  class="flex-1 block w-full py-2 px-3 text-gray-800 border border-gray-300 transition duration-500 focus:outline-none focus:border-black rounded"
                  v-model="userData.address"
                />
                <ErrorMessage class="text-red-600" name="address" />
              </div>
            </div>
            <div class="flex flex-row" :class="{ hidden: editState }">
              <p class="w-48 font-bold">Role:</p>
              <p class="flex-1">{{ user.data.role }}</p>
            </div>
            <div class="flex flex-row" :class="{ hidden: editState }">
              <p class="w-48 font-bold">Password:</p>
              <div>
                <p class="flex-1 self-center mt-1">***********</p>
                <button
                  type="button"
                  class="border border-black p-1 mr-96 hover:bg-gray-300"
                >
                  Change password
                </button>
              </div>
            </div>
            <button
              type="submit"
              :class="{ hidden: !editState }"
              class="border border-black p-1 w-36 ml-56 hover:bg-gray-300"
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
  computed: mapState({
    editState: (state) => state.toggle.editModalShow,
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
  }),
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
    ...mapMutations(["toggle/toggleEditModalShow"]),
    toggleEditModalShow() {
      this.$refs.form.resetForm();
      this["toggle/toggleEditModalShow"]();
    },
    async handleEdit() {
      (this.edit_show_alert = true),
        (this.edit_alert_variant = "bg-blue-500"),
        (this.edit_alert_msg = "Please wait!"),
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
  },
  created() {
    UserService.getUserInfo().then((response) => {
      this.user = response.data;
    });
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
