<template>
  <div class="home-insurance tw-gap-5">
    <template
      class=""
      v-for="homeInsuranceContent in homeInsuranceContents"
      :key="homeInsuranceContent.homePolicyId"
    >
      <div
        class="
          home-content
          tw-flex tw-flex-col
          gap-1
          d-flex
          align-items-center
        "
      >
        <div>
          <h1 class="text-info tw-font-black tw-my-8">
            {{ homeInsuranceContent.type }}
          </h1>
        </div>
        <div class="" v-html="homeInsuranceContent.content"></div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9 tw-mt-8">
          <p class="tw-font-bold">Person claim:</p>
          <p>{{ homeInsuranceContent.personClaim }}</p>
        </div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9">
          <p class="tw-font-bold">Home claim:</p>
          <p>{{ homeInsuranceContent.homeClaim }}</p>
        </div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9">
          <p class="tw-font-bold">Amount paid:</p>
          <p>{{ homeInsuranceContent.amountPaid }}</p>
        </div>
        <button type="button" class="btn btn-info col-1 tw-ml-2">Buy</button>
      </div>
    </template>
  </div>
</template>

<script>
import PublicService from "../services/public.service.js";

export default {
  name: "HomeInsurance",
  data() {
    return {
      homeInsuranceContents: [],
    };
  },
  methods: {
    fetchhomeInsuranceContent() {},
  },
  created() {
    PublicService.getAllHomeInsuranceContent().then((response) => {
      this.homeInsuranceContents = response.data.data;
    });
  },
};
</script>

<style scoped lang="scss">
.home-insurance {
  background-color: #4682b4;
  font-size: 1rem;
  padding: 1rem;
  margin: 4rem;
  color: black;

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.home-content {
  background-color: #f6f6f2;
  width: 100%;
  height: 100%;
}
</style>
