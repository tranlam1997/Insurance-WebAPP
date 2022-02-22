<template>
<div class="jumbotron jumbotron-fluid">
  <div class="container">
    <h1 class="display-4">Psawn Insurance</h1>
    <p>Our service will bring happiness and satisfaction to all of you</p>
  </div>
</div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';


export default defineComponent({
  name: 'Home',
  components: {},
});
</script>

<style scoped lang="scss">
.jumbotron {
  background-image: url("https://www.thebalance.com/thmb/yo-4SVblURSgc4VdxtNhF0hOfqI=/3865x2576/filters:fill(auto,1)/Understanding-home-insurance-options-57ae4c723df78cd39cab3aa6.jpg");
  background-repeat: no-repeat;
  background-size: cover;
  height: 90vh;
}
</style>
