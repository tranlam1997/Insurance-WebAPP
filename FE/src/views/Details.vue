<template>
    <div
      class="vehicle-details"
    >
      <div
        class="
          vehicle-content-details
          tw-flex tw-flex-col
          gap-1
          d-flex
          align-items-center
        "
      >
        <div>
          <h1 class="text-info tw-font-black tw-my-8">
            {{ vehicleDetail.type }}
          </h1>
        </div>
        <div class="" v-html="vehicleDetail.content"></div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9 tw-mt-8">
          <p class="tw-font-bold">Person claim:</p>
          <p>{{ vehicleDetail.personClaim }}</p>
        </div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9">
          <p class="tw-font-bold">Vehicle claim:</p>
          <p>{{ vehicleDetail.vehicleClaim }}</p>
        </div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9">
          <p class="tw-font-bold">Amount paid:</p>
          <p>{{ vehicleDetail.amountPaid }}</p>
        </div>
        <button type="button" class="btn btn-info col-1 tw-ml-2">Buy</button>
      </div>
    </div>
</template>

<script>
// import PublicService from "../services/public.service.js";

export default {
  name: "Details",
  data() {
    return {
      vehicleDetail: {
        type: 'type1',
        content: 'content1',
        personClaim: 'personClaim1',
        vehicleClaim: 'vehicleClaim1',
        amountPaid: 'amountPaid1',
      },
    };
  },
  // props: ['vehicleId'],
  // created() {
  //   console.log(this.vehicleId)
  //   PublicService.getVehicleInsuranceContentById(this.vehicleId).then((response) => {
  //     this.vehicleDetail = response.data.data;
  //   });
  // },
};
</script>

<style scoped lang="scss">
.vehicle-details {
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

.vehicle-content-details {
  background-color: #f6f6f2;
  width: 100%;
  height: 100%;
}
</style>
