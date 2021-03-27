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
import { mapGetters } from "vuex";

export default {
  name: 'App',
  computed: {
    navigationItems() {
      let routes = this.$router.options.routes;
      if (this.isAuthenticated()) {
        routes = routes.filter(r =>  r.showOn == 'Authorized' || r.showOn == 'Always');
      } else {
        routes = routes.filter(r =>  r.showOn == 'NonAuthorized' || r.showOn == 'Always');
      }

      return routes;
    }
  },
  components: {
    Appbar
  },
  methods: {
    ...mapGetters(['isAuthenticated'])
  },
  data: () => ({
    //
  }),
};
</script>
