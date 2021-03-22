<template>
  <div class="home">
    <v-row class="text-center">
    <v-col class="mb-4">
      <h1 class="display-2 font-weight-bold mb-3">
        Welcome to Dental System
      </h1>

      <p class="subheading font-weight-regular">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
      </p>
    </v-col>
    </v-row>
    <Carousel :slides="allTreatments.map(t => t.Name)" />
  </div>
</template>

<script>
// @ is an alias to /src
import Carousel from '@/common/components/Carousel.vue'
import { mapGetters, mapActions } from "vuex";

export default {
  name: 'Home',
  components: {
    Carousel
  },
  computed: {
    ...mapGetters([
      'allTreatments',
    ]),
  },
  methods: {
    ...mapActions(["getTreatments"])
  },
  created() {
    this.getTreatments();
  }
}
</script>
