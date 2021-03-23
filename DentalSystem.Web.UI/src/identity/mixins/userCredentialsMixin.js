import { required, email, minLength } from "vuelidate/lib/validators";

const userCredentialsMixin = {
  data: () => ({
    userCredentialsData: {
      username: '',
      password: '',
    },
    showPassword: false,
    errorMessage: null
  }),
  validations: {
    userCredentialsData: {
      username: {
        required,
        email
      },
      password: {
        required,
        minLength: minLength(6),
      }
    },
  },
  computed: {
    usernameErrors () {
      const errors = []
      if (!this.$v.userCredentialsData.username.$dirty) return errors
      !this.$v.userCredentialsData.username.email && errors.push('Must be valid e-mail')
      !this.$v.userCredentialsData.username.required && errors.push('User name is required')
      return errors
    },
    passwordErrors () {
      const errors = []
      if (!this.$v.userCredentialsData.password.$dirty) return errors
      !this.$v.userCredentialsData.password.required && errors.push('Password is required')
      !this.$v.userCredentialsData.password.minLength && errors.push('Password must be at least 6 characters')
      return errors
    },
  },
  methods: {
    clearUserCredentials () {
      this.userCredentialsData.username = ''
      this.userCredentialsData.password = ''
      this.showPassword = false
      this.errorMessage = null
    },
  }
};

export default userCredentialsMixin;