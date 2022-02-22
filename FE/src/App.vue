<template>
  <div class="d-flex flex-column">
    <!-- Navigation -->
      <div class="container-fluid bg-white p-2 h6 d-flex flex-row justify-content-between">
        <img src="./assets/brand-logo.png" alt="" class="img-fluid" style="max-width: 120px">
        <ul class="nav flex-row justify-content-end align-self-center">
          <li class="nav-item">
            <router-link class="nav-link pr-3 text-dark" to="/login">Sign in</router-link>
          </li>
          <li class="nav-item">
            <router-link class="btn btn-outline-dark" to="/signup">Sign up</router-link>
          </li>
        </ul>
      </div>
    <!-- Main -->
      <div class="vertical-center">
        <div class="inner-block h6">
          <router-view />
        </div>
    </div>
  </div>
</template>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
