<template>
  <div class="vehicle-insurance tw-gap-5" v-if="!detailsShow">
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
        <div class="mt-2 mb-6 mx-4" v-html="vehicleInsuranceContent.content"></div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9">
          <p class="tw-font-bold">Person claim:</p>
          <p>{{ vehicleInsuranceContent.personClaim }}</p>  
        </div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9">
          <p class="tw-font-bold">Vehicle claim:</p>
          <p>{{ vehicleInsuranceContent.vehicleClaim }}</p>
        </div>
        <div class="tw-flex tw-flex-row tw-gap-5 col-9">
          <p class="tw-font-bold">Amount paid:</p>
          <p>{{ vehicleInsuranceContent.amountPaid }}</p>
        </div>
        <div>
          <router-link :to="{ name: 'Details', params: { id: vehicleInsuranceContent.id } }" ><button class="btn btn-primary" >View more</button></router-link>
        </div>
      </div>
    </template>
  </div>
      <router-view></router-view>
</template>

<script>
import PublicService from "../services/public.service.js";

export default {
  name: "VehicleInsurance",
  data() {
    return {
      vehicleInsuranceContents: [{
        id: '1234abc',
        type: "VehicleInsurance",
        content: "he Working Group I contribution to the Sixth Assessment Report addresses the most up-to-date physical understanding of the climate system and climate change, bringing together the latest advances in climate science, and combining multiple lines of evidence from paleoclimate, observations, process understanding, and global and regional climate simulations.",
        personClaim: "Person claim1",
        vehicleClaim: "Vehicle claim1",
        amountPaid: "Amount paid1",
      },
        {
        id: '1235abc',
        type: "VehicleInsurance",
        content: "he Working Group I contribution to the Sixth Assessment Report addresses the most up-to-date physical understanding of the climate system and climate change, bringing together the latest advances in climate science, and combining multiple lines of evidence from paleoclimate, observations, process understanding, and global and regional climate simulations.",
        personClaim: "Person claim1",
        vehicleClaim: "Vehicle claim1",
        amountPaid: "Amount paid1",
      },
              {
        id: '1235abc',
        type: "VehicleInsurance",
        content: "he Working Group I contribution to the Sixth Assessment Report addresses the most up-to-date physical understanding of the climate system and climate change, bringing together the latest advances in climate science, and combining multiple lines of evidence from paleoclimate, observations, process understanding, and global and regional climate simulations.",
        personClaim: "Person claim1",
        vehicleClaim: "Vehicle claim1",
        amountPaid: "Amount paid1",
      },
              {
        id: '1235abc',
        type: "VehicleInsurance",
        content: "he Working Group I contribution to the Sixth Assessment Report addresses the most up-to-date physical understanding of the climate system and climate change, bringing together the latest advances in climate science, and combining multiple lines of evidence from paleoclimate, observations, process understanding, and global and regional climate simulations.",
        personClaim: "Person claim1",
        vehicleClaim: "Vehicle claim1",
        amountPaid: "Amount paid1",
      },
              {
        id: '1235abc',
        type: "VehicleInsurance",
        content: "he Working Group I contribution to the Sixth Assessment Report addresses the most up-to-date physical understanding of the climate system and climate change, bringing together the latest advances in climate science, and combining multiple lines of evidence from paleoclimate, observations, process understanding, and global and regional climate simulations.",
        personClaim: "Person claim1",
        vehicleClaim: "Vehicle claim1",
        amountPaid: "Amount paid1",
      },
              {
        id: '1235abc',
        type: "VehicleInsurance",
        content: "he Working Group I contribution to the Sixth Assessment Report addresses the most up-to-date physical understanding of the climate system and climate change, bringing together the latest advances in climate science, and combining multiple lines of evidence from paleoclimate, observations, process understanding, and global and regional climate simulations.",
        personClaim: "Person claim1",
        vehicleClaim: "Vehicle claim1",
        amountPaid: "Amount paid1",
      },
              {
        id: '1235abc',
        type: "VehicleInsurance",
        content: "he Working Group I contribution to the Sixth Assessment Report addresses the most up-to-date physical understanding of the climate system and climate change, bringing together the latest advances in climate science, and combining multiple lines of evidence from paleoclimate, observations, process understanding, and global and regional climate simulations.",
        personClaim: "Person claim1",
        vehicleClaim: "Vehicle claim1",
        amountPaid: "Amount paid1",
      },
              {
        id: '1235abc',
        type: "VehicleInsurance",
        content: "he Working Group I contribution to the Sixth Assessment Report addresses the most up-to-date physical understanding of the climate system and climate change, bringing together the latest advances in climate science, and combining multiple lines of evidence from paleoclimate, observations, process understanding, and global and regional climate simulations.",
        personClaim: "Person claim1",
        vehicleClaim: "Vehicle claim1",
        amountPaid: "Amount paid1",
      },        {
        id: '1235abc',
        type: "VehicleInsurance",
        content: "he Working Group I contribution to the Sixth Assessment Report addresses the most up-to-date physical understanding of the climate system and climate change, bringing together the latest advances in climate science, and combining multiple lines of evidence from paleoclimate, observations, process understanding, and global and regional climate simulations.",
        personClaim: "Person claim1",
        vehicleClaim: "Vehicle claim1",
        amountPaid: "Amount paid1",
      }
      ],
      detailsShow: false,
    };
  },
  methods: {
    showVehicleDetails() {
      this.detailsShow = !this.detailsShow;
    },
  },
  created() {
    PublicService.getAllVehicleInsuranceContent().then((response) => {
      this.vehicleInsuranceContents = response.data.data;
    });
  },
  updated() {
      this.detailsShow = !this.detailsShow;
  }
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
