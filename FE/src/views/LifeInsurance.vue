<template>
  <div class="life-insurance tw-gap-5">
    <template
      class=""
      v-for="lifeInsuranceContent in lifeInsuranceContents"
      :key="lifeInsuranceContent.lifePolicyId"
    >
      <div
        class="
          life-content
          tw-flex tw-flex-col
          gap-1
          d-flex
          align-items-center
        "
      >
        <div>
          <h1 class="text-info tw-font-black tw-my-8">
            {{ lifeInsuranceContent.type }}
          </h1>
        </div>
        <div class="" v-html="lifeInsuranceContent.content"></div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9 tw-mt-8">
          <p class="tw-font-bold">Person claim:</p>
          <p>{{ lifeInsuranceContent.personClaim }}</p>
        </div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9">
          <p class="tw-font-bold">Amount paid:</p>
          <p>{{ lifeInsuranceContent.amountPaid }}</p>
        </div>
      </div>
    </template>
  </div>
</template>

<script>
import PublicService from "../services/public.service.js";

export default {
  name: "LifeInsurance",
  data() {
    return {
      lifeInsuranceContents: [],
    };
  },
  methods: {
    showlifeDetails(lifeId) {
        this.$router.push({
            name: "lifeDetails",
            params: {
            id: lifeId,
            },
        });
        },
    },
  created() {
    PublicService.getAllLifeInsuranceContent().then((response) => {
      this.lifeInsuranceContents = response.data.data;
    });
  },
};
</script>

<style scoped lang="scss">
.life-insurance {
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

.life-content {
  background-color: #f6f6f2;
  width: 100%;
  height: 100%;
}
</style>
