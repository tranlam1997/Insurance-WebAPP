<template>
  <div class="vehicle-insurance tw-gap-5">
    <template
      class=""
      v-for="vehicleInsuranceContent in vehicleInsuranceContents"
      :key="vehicleInsuranceContent.vehiclePolicyId"
    >
      <div
        class="
          vehicle-content
          tw-flex tw-flex-col
          gap-1
          d-flex
          align-items-center
        "
      >
          <h1 class="text-info tw-font-black tw-my-8">
            {{ vehicleInsuranceContent.type }}
          </h1>
        <div class="" v-html="vehicleInsuranceContent.content"></div>
        <button type="button" class="btn btn-info col-1 tw-ml-2" @click.prevent="showVehicleDetails(vehicleInsuranceContent.id)">See More</button>
      </div>
      <router-view :vehicle-id="vehicleInsuranceContent.id"></router-view>
    </template>
  </div>
</template>

<script>
// import PublicService from "../services/public.service.js";

export default {
  name: "VehicleInsurance",
  data() {
    return {
      vehicleInsuranceContents: [
          {id: 'abcd', type: 'title 1', content: '<p>content 1</p>'},
      ],
    };
  },
  methods: {
    showVehicleDetails(vehicleId) {
        this.$router.push({
            path: "/vehicle-insurance/details",
            query: {
            id: vehicleId,
            },
        });
        },
    },
  created() {
    // PublicService.getAllVehicleInsuranceContent().then((response) => {
    //   this.vehicleInsuranceContents = response.data.data;
    // });
  },
};
</script>

<style scoped lang="scss">
.vehicle-insurance {
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

.vehicle-content {
  background-color: #f6f6f2;
  width: 100%;
  height: 100%;
}
</style>
