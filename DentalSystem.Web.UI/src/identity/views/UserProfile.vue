<template>
  <v-card
      elevation="5"
      max-width="768"
      class="mx-auto pa-4 pa-sm-4 pa-md-8 my-12"
    >
      <form @submit.prevent="submit">
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
          save
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
import { validationMixin } from 'vuelidate'
import userProfileMixin from '../mixins/userProfileMixin'

export default {
  name: "UserProfile",
  mixins: [validationMixin, userProfileMixin],
  data: () => ({
    errorMessage: null,
  }),
  methods: {
    ...mapGetters([
      "userProfile"
    ]),
    ...mapActions([
      "getUserProfile",
      "updateUserProfile"
    ]),
    async submit () {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        try {
          console.log(this.userProfileData);
          await this.updateUserProfile(this.userProfileData);
          this.$router.push("/");
        } catch (error) {
          console.log(error);
          var propertiesErrors = error
            .response
            ?.data
            ?.map(propertyErrors => `${propertyErrors.propertyName}: ${propertyErrors.errors.join(' ')}`) ?? [];
          this.errorMessage = propertiesErrors.join(' ');
        }
      }
    },
    clear () {
      this.$v.$reset();
      this.clearUserProfile();
      this.errorMessage = null;
    },
  },
  async created() {
    await this.getUserProfile();
    this.userProfileData = await this.userProfile();
    console.log(this.userProfileData);
  }
}
</script>


<style scoped src="../../common/styles/formStyle.css"></style>