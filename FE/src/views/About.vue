<template>
  <div class="about flex flex-col mb-36">
    <div class="introduction text-5xl text-white font-thin p-10 mt-5 antialiased">
      <h1 class="">
        Decisions made <span class="italic font-semibold">easier.</span>
      </h1>
      <h1>Lives made <span class="italic font-semibold">better.</span></h1>
    </div>
    <div class="achievement flex flex-row gap-10 justify-between mt-24 mx-10">
      <div class="business">
        <i class="fa-solid fa-building-columns text-5xl"></i>
        <h2 class="mt-5 mb-2 font-bold">Our Business</h2>
        <p class="font-thin">Psawn provides financial advice, insurance, as well as wealth and asset management solutions to help people make their decisions easier and lives better, and to ensure they are well-protected.</p>
      </div>
      <div class="history">
        <i class="fa-solid fa-book-open text-5xl"></i>
        <h2 class="mt-5 mb-2 font-bold">Our History</h2>
        <p class="font-thin">Psawn was founded in 2022 by a group of professionals who had a passion for providing financial advice and wealth management solutions to people. Today, Manulife is the largest financial services company in the world.</p>
      </div>
      <div class="award">
        <i class="fa-solid fa-medal text-5xl"></i>
        <h2 class="mt-5 mb-2 font-bold">Our Awards</h2>
        <p class="font-thin">Psawn has been awarded the “Best Financial Services Company” by the Financial Services Association of America (FSA) for its services and products.</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "About",
};
</script>

<style scoped lang="scss">
.introduction {
  background-image: url("https://www.teahub.io/photos/full/364-3648118_orange-background-full-hd.png");
  background-position: center center;
  background-repeat: no-repeat;
  background-size: cover;
}
</style>
