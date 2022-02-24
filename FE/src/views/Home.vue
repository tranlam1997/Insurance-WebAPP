<template>
  <div class="container hero-image text-black p-96 m-auto bg-center bg-no-repeat bg-cover">
  <div class="hero-text">
    <h1>I am John Doe</h1>
    <p>And I'm a Photographer</p>
    <button>Hire me</button>
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
  background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url("https://www.thebalance.com/thmb/yo-4SVblURSgc4VdxtNhF0hOfqI=/3865x2576/filters:fill(auto,1)/Understanding-home-insurance-options-57ae4c723df78cd39cab3aa6.jpg");
  position: relative;
}

.hero-text {
  text-align: center;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
</style>
