<template>
  <div class="about tw-flex tw-flex-col tw-mb-36">
    <div class="introduction tw-text-5xl tw-text-white tw-font-thin tw-p-10 tw-mt-5 tw-antialiased">
      <h1 class="tw-text-white">
        Decisions made <span class="tw-italic tw-font-semibold">easier.</span>
      </h1>
      <h1 class="tw-text-white">Lives made <span class="tw-italic tw-font-semibold">better.</span></h1>
    </div>
    <div class="achievement tw-flex tw-flex-row tw-gap-10 tw-justify-between tw-mt-24 tw-mx-10">
      <div class="business tw-p-8 tw-flex-1">
        <i class="fa-solid fa-building-columns tw-text-5xl"></i>
        <h2 class="tw-mt-5 tw-mb-2 tw-font-bold">Our Business</h2>
        <p class="tw-font-thin">Psawn provides financial advice, insurance, as well as wealth and asset management solutions to help people make their decisions easier and lives better, and to ensure they are well-protected.</p>
      </div>
      <div class="history tw-p-8 tw-flex-1">
        <i class="fa-solid fa-book-open tw-text-5xl"></i>
        <h2 class="tw-mt-5 tw-mb-2 tw-font-bold">Our History</h2>
        <p class="tw-font-thin">Psawn was founded in 2022 by a group of professionals who had a passion for providing financial advice and wealth management solutions to people. Today, Manulife is the largest financial services company in the world.</p>
      </div>
      <div class="award tw-p-8 tw-flex-1">
        <i class="fa-solid fa-medal tw-text-5xl"></i>
        <h2 class="tw-mt-5 tw-mb-2 tw-font-bold">Our Awards</h2>
        <p class="tw-font-thin">Psawn has been awarded the “Best Financial Services Company” by the Financial Services Association of America (FSA) for its services and products.</p>
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

.business, .history, .award {
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
}
</style>
