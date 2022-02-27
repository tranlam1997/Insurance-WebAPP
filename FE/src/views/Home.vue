<template>
  <div class= "hero-image text-white">
  <div class="hero-text">
    <h1>With Psawn Insurance Your Future Protected</h1>
    <h2>No one can surely predict the future. But we can protect it.</h2>
    <div class="mt-12">
    <button type="button" disabled class="border p-2 hover:bg-white hover:text-black">Learn More</button>
  </div>
  </div>
  <div class="container ">
  </div>
</div>

</template>

<script>
// @ is an alias to /src

export default {
  name: 'Home'
}
</script>

<style scoped lang="scss">
  .hero-image {
    height: 80vh;
    background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url("../../public/assets/img/hero-background.jpg");
    background-position: center center;
    background-repeat: no-repeat;
    background-size: cover;
}

.hero-text {
  text-align: center;
  position: absolute;
  top: 40%;
  left: 50%;
  transform: translate(-50%, -50%);
  h1 {
    font-size: 3rem;  
  font-weight: 900;
  }
  h2 {
    font-size: 1.5rem;
    font-weight: 500;
  }
}

</style>
