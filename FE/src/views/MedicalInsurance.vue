<template>
  <div class="medical-insurance tw-gap-5">
    <template
      class=""
      v-for="medicalInsuranceContent in medicalInsuranceContents"
      :key="medicalInsuranceContent.medicalPolicyId"
    >
      <div
        class="
          medical-content
          tw-flex tw-flex-col
          gap-1
          d-flex
          align-items-center
        "
      >
        <div>
          <h1 class="text-info tw-font-black tw-my-8">
            {{ medicalInsuranceContent.type }}
          </h1>
        </div>
        <div class="" v-html="medicalInsuranceContent.content"></div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9">
          <p class="tw-font-bold">Amount paid:</p>
          <p>{{ medicalInsuranceContent.amountPaid }}</p>
        </div>
      </div>
    </template>
  </div>
</template>

<script>
import PublicService from "../services/public.service.js";

export default {
  name: "MedicalInsurance",
  data() {
    return {
      medicalInsuranceContents: [],
    };
  },
  methods: {
    fetchmedicalInsuranceContent() {},
  },
  created() {
    PublicService.getAllMedicalInsuranceContent().then((response) => {
      this.medicalInsuranceContents = response.data.data;
    });
  },
};
</script>

<style scoped lang="scss">
.medical-insurance {
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

.medical-content {
  background-color: #f6f6f2;
  width: 100%;
  height: 100%;
}
</style>
