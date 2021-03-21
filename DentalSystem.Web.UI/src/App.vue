<template>
  <v-app>
    <Appbar />

    <v-main>
      <HelloWorld />
    </v-main>
  </v-app>
</template>

<script>
import Appbar from './common/components/Appbar';
import HelloWorld from './common/components/HelloWorld';

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
