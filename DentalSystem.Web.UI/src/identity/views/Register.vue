<template>
  <v-card
      elevation="5"
      max-width="768"
      class="mx-auto pa-4 pa-sm-4 pa-md-8 my-12"
    >
      <form @submit.prevent="submit">
        <v-text-field
          v-model="formData.username"
          :error-messages="usernameErrors"
          label="User Name"
          required
          @input="$v.formData.username.$touch()"
          @blur="$v.formData.username.$touch()"
        ></v-text-field>

        <v-text-field
          v-model="formData.password"
          :error-messages="passwordErrors"
          :type="showPassword ? 'text' : 'password'"
          @click:append="showPassword = !showPassword"
          :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
          label="Password"
          required
          @input="$v.formData.password.$touch()"
          @blur="$v.formData.password.$touch()"
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
import { required, email, minLength, maxLength } from "vuelidate/lib/validators";

export default {
  name: "Login",
  mixins: [validationMixin],
  data: () => ({
    formData: {
      username: '',
      password: '',
      firstName: '',
      lastName: ''
    },
    showPassword: false,
    errorMessage: null
  }),
  validations: {
    formData: {
      username: {
        required,
        email
      },
      password: {
        required,
        minLength: minLength(6),
      },
      firstName: {
        maxLength: maxLength(32),
      },
      lastName: {
        maxLength: maxLength(32),
      }
    },
  },
  computed: {
    usernameErrors () {
      const errors = []
      if (!this.$v.formData.username.$dirty) return errors
      !this.$v.formData.username.email && errors.push('Must be valid e-mail')
      !this.$v.formData.username.required && errors.push('User name is required')
      return errors
    },
    passwordErrors () {
      const errors = []
      if (!this.$v.formData.password.$dirty) return errors
      !this.$v.formData.password.required && errors.push('Password is required')
      !this.$v.formData.password.minLength && errors.push('Password must be at least 6 characters')
      return errors
    },
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
      this.formData.username = ''
      this.formData.password = ''
      this.formData.firstName = ''
      this.formData.lastName = ''
      this.showPassword = false
      this.errorMessage = null
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