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

        <v-text-field
          v-model="userProfileData.firstName"
          :error-messages="firstNameErrors"
          label="First Name"
          @input="$v.userProfileData.firstName.$touch()"
          @blur="$v.userProfileData.firstName.$touch()"
        ></v-text-field>

        <v-text-field
          v-model="userProfileData.lastName"
          :error-messages="lastNameErrors"
          label="Last Name"
          @input="$v.userProfileData.lastName.$touch()"
          @blur="$v.userProfileData.lastName.$touch()"
        ></v-text-field>

        <v-btn
          type="submit"
          color="success"
        >
          register
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
import userCredentialsMixin from '../mixins/userCredentialsMixin';
import userProfileMixin from '../mixins/userProfileMixin';

export default {
  name: "Register",
  mixins: [
    userCredentialsMixin,
    userProfileMixin,
    validationMixin,
  ],
  validations: {
    ...userCredentialsMixin.validations,
    ...userProfileMixin.validations,
  },
  methods: {
    ...mapActions(["register"]),
    async submit () {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        try {
          await this.register({
            ...this.userCredentialsData,
            ...this.userProfileData
          });

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
      this.clearUserProfile();
    },
  },
}
</script>


<style scoped src="../../common/styles/formStyle.css"></style>