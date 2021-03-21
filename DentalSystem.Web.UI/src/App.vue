<template>
  <v-app>
    <Appbar />

    <v-main>
      <HelloWorld/>
    </v-main>
  </v-app>
</template>

<script>
import Appbar from './components/Appbar';
import HelloWorld from './components/HelloWorld';

export default {
  name: 'App',

  components: {
    Appbar,
    HelloWorld
  },

  data: () => ({
    //
  }),
};
</script>
