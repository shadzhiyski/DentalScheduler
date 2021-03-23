<template>
  <v-card
      elevation="5"
      max-width="768"
      class="mx-auto pa-4 pa-sm-4 pa-md-8 my-12"
    >
      <form @submit.prevent="submit">
        <v-text-field
          v-model="formData.username"
          label="User Name"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.password"
          :type="showPassword ? 'text' : 'password'"
          @click:append="showPassword = !showPassword"
          :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
          label="Password"
          required
        ></v-text-field>

        <v-btn
          type="submit"
          color="success"
        >
          submit
        </v-btn>
        <v-btn @click="clear">
          clear
        </v-btn>
      </form>
      <v-alert
        v-if="errorMessage != null"
        border="left"
        dark
        dismissible
        type="error"
      > {{ errorMessage }}</v-alert>
  </v-card>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "Login",
  data: () => ({
    formData: {
      username: '',
      password: '',
    },
    showPassword: false,
    errorMessage: null
  }),
  computed: {
    ...mapGetters([
      'username',
      'authToken',
      'authTokenData'
    ])
  },
  methods: {
    ...mapActions(["logIn"]),
    async submit () {
      try {
        await this.logIn(this.formData);
        this.$router.push("/");
        this.showError = false
      } catch (error) {
        this.showError = true
      }
    },
    clear () {
      this.formData.username = ''
      this.formData.password = ''
      this.showPassword = false
      this.showError = false
    },
  },
}
</script>


<style scoped>
* {
  box-sizing: border-box;
}
label {
  padding: 12px 12px 12px 0;
  display: inline-block;
}
button[type="submit"] {
  background-color: #4caf50;
  color: white;
  padding: 12px 20px;
  cursor: pointer;
  border-radius: 30px;
}
button[type="submit"]:hover {
  background-color: #45a049;
}
input {
  margin: 5px;
  box-shadow: 0 0 15px 4px rgba(0, 0, 0, 0.06);
  padding: 10px;
  border-radius: 30px;
}
#error {
  color: red;
}
</style>