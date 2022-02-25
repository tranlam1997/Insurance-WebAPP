<template>
  <div class= "container hero-image text-white">
  <div class="hero-text">
    hello
  </div>
</div>
</template>

<script>
// @ is an alias to /src

export default {
  name: 'Home'
}
</script>

<style scoped>
  .hero-image {
height: 80vh;
background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url("../../public/assets/img/hero-background.jpg");
background-position: center center;
background-repeat: no-repeat;
background-size: cover;
background-attachment: scroll;

}

.hero-text {
  text-align: center;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
</style>
