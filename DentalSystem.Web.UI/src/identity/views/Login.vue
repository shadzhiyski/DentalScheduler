<template>
  <v-card
      elevation="5"
      max-width="768"
      class="mx-auto pa-4 pa-sm-4 pa-md-8 my-12"
    >
      <form @submit.prevent="submit">
        <v-text-field
          v-model="userCredentialsData.username"
          :error-messages="usernameErrors"
          label="User Name"
          required
          @input="$v.userCredentialsData.username.$touch()"
          @blur="$v.userCredentialsData.username.$touch()"
        ></v-text-field>

        <v-text-field
          v-model="userCredentialsData.password"
          :error-messages="passwordErrors"
          :type="showPassword ? 'text' : 'password'"
          @click:append="showPassword = !showPassword"
          :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
          label="Password"
          required
          @input="$v.userCredentialsData.password.$touch()"
          @blur="$v.userCredentialsData.password.$touch()"
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
import { mapActions } from "vuex";
import { validationMixin } from 'vuelidate'
import userCredentialsMixin from '../mixins/userCredentialsMixin'

export default {
  name: "Login",
  mixins: [validationMixin, userCredentialsMixin],
  methods: {
    ...mapActions(["logIn"]),
    async submit () {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        try {
          await this.logIn(this.userCredentialsData);
          this.$router.push("/");
        } catch (error) {
          var propertiesErrors = error
            .response
            .data
            .map(propertyErrors => `${propertyErrors.propertyName}: ${propertyErrors.errors.join(' ')}`);
          this.errorMessage = propertiesErrors.join(' ');
        }
      }
    },
    clear () {
      this.$v.$reset();
      this.clearUserCredentials();
    },
  },
}
</script>

<style scoped src="../../common/styles/formStyle.css"></style>