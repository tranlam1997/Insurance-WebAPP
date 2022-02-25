<template>

<!-- The Modal -->
    <div id="register" class="modal-register" :class="{hidden: !registerState}">
  <span onclick="document.getElementById('id01').style.display='none'"
class="close" title="Close Modal">&times;</span>

  <!-- Modal Content -->
  <form class="modal-content animate flex flex-col p-8 max-w-max" action="/action_page.php">

    <div class="container flex flex-col">
      <label for="name" class="inline-block mb-2"><b>Name</b></label>
      <input type="text" placeholder="Enter Name" name="name" size="50" required class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition
          duration-500 focus:outline-none focus:border-black rounded">

      <label for="email" class="inline-block mb-2"><b>Email</b></label>
      <input type="email" placeholder="Enter Email" name="email" size="50" required class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition
          duration-500 focus:outline-none focus:border-black rounded">

      <label for="age" class="inline-block mb-2"><b>Age</b></label>
      <input type="text" placeholder="Enter Age" name="age" size="50" required class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition
          duration-500 focus:outline-none focus:border-black rounded">

      <label for="phone" class="inline-block mb-2"><b>Phone Number</b></label>
      <input type="text" placeholder="Enter Phone Number" name="phone" size="50" required class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition
          duration-500 focus:outline-none focus:border-black rounded">

      <label for="password" class="inline-block mb-2"><b>Password</b></label>
      <input type="password" placeholder="Enter Password" name="password" size="50" required class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition
          duration-500 focus:outline-none focus:border-black rounded">

      <label for="confirm_password" class="inline-block mb-2"><b>Confirm Password</b></label>
      <input type="password" placeholder="Confirm Password" name="confirm_password" size="50" required class="block w-full py-2 px-3 text-gray-800 border border-gray-300 transition
          duration-500 focus:outline-none focus:border-black rounded">

      <div class="mt-3 pl-1 flex flex-row gap-1">
      <input type="checkbox" name="tos" value="1"
        class="w-4 h-4 rounded self-center" />
      <label class="" keypath="register.accept" tag="label">
        <a class="underline"><b>I agree on Terms and Condition</b></a>
      </label>
    </div>

      <button type="submit" class="mt-4 block w-full bg-blue-600 text-white py-1.5 px-3 rounded transition
        hover:bg-blue-700">Submit</button>
    </div>

    <div class="footerLogin container flex flex-row mt-8 justify-between">
      <button type="button" onclick="document.getElementById('id01').style.display='none'"  class="cancelbtn block rounded text-white py-2 px-3" @click.prevent="toggleLoginModal">Cancel</button>
      <span class="psw self-center">Already have an account ?<a href="#" class="text-blue-500 hover:text-blue-600 font-medium underline">Sign in</a></span>
    </div>
  </form>
</div>
</template>

<script>
import { mapState } from "vuex"
import { mapMutations } from "vuex"

export default {
    name: 'RegisterForm',
    computed: mapState({
      registerState: state => state.registerModalShow
    }),
      methods: {
    ...mapMutations(['toggleLoginModal', 'toggleRegisterModal']),
  },
}
</script>

<style scoped lang="scss">
  .modalRegister {
  position: fixed; /* Stay in place */
  z-index: 1; /* Sit on top */
  left: 50%;
  top: 50%;
  width: 100%;
  height: 100%;
  transform: translate(-50%, -50%);
  overflow: auto; /* Enable scroll if needed */
  background-color: rgba(0,0,0,0.7); /* Black w/ opacity */
}

/* Modal Content/Box */
.modal-content {
  position: relative;
  top: 10%;
  background-color: #fefefe;
  margin: 5% auto 15% auto; /* 5% from the top, 15% from the bottom and centered */
  border: 1px solid #888;
  width: 80%; /* Could be more or less, depending on screen size */
}

.cancelbtn {
  background-color: rgb(244 63 94);
  &:hover {
    background-color: rgb(238, 30, 65)
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
  &:hover, &:focus {
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
  animation: animatezoom 0.6s
}

@-webkit-keyframes animatezoom {
  from {-webkit-transform: scale(0)} 
  to {-webkit-transform: scale(1)}
}
  
@keyframes animatezoom {
  from {transform: scale(0)} 
  to {transform: scale(1)}
}
</style>

