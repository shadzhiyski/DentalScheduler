<template>
  <v-app>
    <Appbar :navigationItems="navigationItems"/>

    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
import Appbar from './common/components/Appbar';

export default {
  name: 'App',
  computed: {
    navigationItems() {
      return this.$router.options.routes;
    }
  },
  components: {
    Appbar
  },

  data: () => ({
    //
  }),
};
</script>
