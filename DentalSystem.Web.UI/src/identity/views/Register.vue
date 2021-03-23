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
import { maxLength } from "vuelidate/lib/validators";
import userCredentialsMixin from '../mixins/userCredentialsMixin';

export default {
  name: "Login",
  mixins: [validationMixin, userCredentialsMixin],
  data: () => ({
    formData: {
      firstName: '',
      lastName: ''
    }
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
    ...mapActions(["register"]),
    async submit () {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        try {
          await this.register(this.formData);
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
      this.formData.firstName = ''
      this.formData.lastName = ''
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