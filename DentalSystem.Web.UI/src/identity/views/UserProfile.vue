<template>
  <v-card
      elevation="5"
      max-width="768"
      class="mx-auto pa-4 pa-sm-4 pa-md-8 my-12"
    >
      <form @submit.prevent="submit">
        <v-text-field
          v-model="formData.firstName"
          :error-messages="firstNameErrors"
          label="First Name"
          @input="$v.formData.firstName.$touch()"
          @blur="$v.formData.firstName.$touch()"
        ></v-text-field>

        <v-text-field
          v-model="formData.lastName"
          :error-messages="lastNameErrors"
          label="Last Name"
          @input="$v.formData.lastName.$touch()"
          @blur="$v.formData.lastName.$touch()"
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
import { maxLength } from "vuelidate/lib/validators";

export default {
  name: "UserProfile",
  mixins: [validationMixin],
  data: () => ({
    formData: {
      firstName: '',
      lastName: ''
    },
    errorMessage: null,
  }),
  validations: {
    formData: {
      firstName: {
        maxLength: maxLength(32),
      },
      lastName: {
        maxLength: maxLength(32),
      }
    },
  },
  computed: {
    firstNameErrors () {
      const errors = []
      if (!this.$v.formData.firstName.$dirty) return errors
      !this.$v.formData.firstName.maxLength && errors.push('Last name must less than 32 characters')
      return errors
    },
    lastNameErrors () {
      const errors = []
      if (!this.$v.formData.lastName.$dirty) return errors
      !this.$v.formData.lastName.maxLength && errors.push('Last name must less than 32 characters')
      return errors
    },
  },
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
          console.log(this.formData);
          await this.updateUserProfile(this.formData);
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
      this.formData.firstName = ''
      this.formData.lastName = ''
      this.errorMessage = null;
    },
  },
  async created() {
    await this.getUserProfile();
    this.formData = await this.userProfile();
    console.log(this.formData);
  }
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