<template>
  <!------ Include the above in your HEAD tag ---------->

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
            <h5>{{user.data.firtsName + ' ' + user.data.lastName}}</h5>
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
            >
              Edit Profile
            </button>
          </div>
        </div>
        <div class="flex flex-col gap-3">
          <div class="flex flex-row">
            <p class="w-48 font-bold">ID:</p>
            <p class="">{{ user.data.id }}</p>
          </div>
          <div class="flex flex-row">
            <p class="w-48 font-bold">Name:</p>
            <p class="flex-1">{{ user.data.firtsName + ' ' + user.data.lastName }}</p>
          </div>
          <div class="flex flex-row">
            <p class="w-48 font-bold">Date Of Birth:</p>
            <p class="flex-1">{{ user.data.dateOfBirth}}</p>
          </div>
          <div class="flex flex-row">
            <p class="w-48 font-bold">Email:</p>
            <p class="flex-1">{{ user.data.email }}</p>
          </div>
          <div class="flex flex-row">
            <p class="w-48 font-bold">Phone:</p>
            <p class="flex-1">{{user.data.phoneNumber}}</p>
          </div>
          <div class="flex flex-row">
            <p class="w-48 font-bold">Address:</p>
            <p class="flex-1">{{user.data.address}}</p>
          </div>
          <div class="flex flex-row">
            <p class="w-48 font-bold">Role:</p>
            <p class="flex-1">{{user.data.role}}</p>
          </div>
          <div class="flex flex-row">
            <p class="w-48 font-bold">Password:</p>
            <div>
              <p class="flex-1 self-center mt-1">***********</p>
              <button
                type="button"
                class="border border-black p-1 mr-96 hover:bg-gray-200"
              >
                Change password
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import UserService from '../services/user.service.js';

export default {
  name: "Profile",
  data() {
    return {
      user: null
    }
  },
  created() {
    console.log(this.user)
    UserService.getUserInfo().then((response) => {
        this.user = response.data;
      })
    console.log(this.user)
  }
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
