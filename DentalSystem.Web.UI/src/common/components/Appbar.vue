<template>
  <div>
    <v-app-bar
      app
      color="primary"
      dark
    >
      <v-app-bar-nav-icon @click="toggleNavigation"></v-app-bar-nav-icon>
      <v-toolbar-title>Dental Scheduler UI</v-toolbar-title>

      <v-spacer></v-spacer>

      <v-btn v-if="isAuthenticated"
          text
          @click="logOutClicked">
        <v-icon>
          mdi-logout
        </v-icon>
        <span class="mr-2">Logout</span>
      </v-btn>
      <div class="d-flex align-center">
        <v-img
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          src="../assets/logo.svg"
          transition="scale-transition"
          width="40"
        />
      </div>

    </v-app-bar>
    <v-navigation-drawer v-model="showDrawer" absolute temporary>
      <v-list nav dense>
        <v-list-item-group
          active-class="primary--text text--accent-4"
        >
          <v-list-item v-for="action in navigationItems" v-bind:key="action.path">
            <v-list-item-icon>
              <v-icon>{{action.icon}}</v-icon>
            </v-list-item-icon>
            <v-list-item-title><router-link :to="action.path">{{action.name}}</router-link></v-list-item-title>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: 'Appbar',
  methods: {
    toggleNavigation() {
      this.showDrawer = !this.showDrawer;
    },
    ...mapActions(["logOut"]),
    logOutClicked() {
      this.logOut();
      this.$router.push('/');
    }
  },
  computed: {
    ...mapGetters([
      'isAuthenticated',
    ]),
  },
  props: {
    navigationItems: {
      type: Array,
      required: true,
    }
  },
  data: () => ({
    showDrawer: false,
  })
};
</script>