import { required, maxLength } from "vuelidate/lib/validators";

const userProfileMixin = {
  data: () => ({
    userProfileData: {
      firstName: '',
      lastName: ''
    },
  }),
  validations: {
    userProfileData: {
      firstName: {
        required,
        maxLength: maxLength(32),
      },
      lastName: {
        required,
        maxLength: maxLength(32),
      }
    },
  },
  computed: {
    firstNameErrors () {
      const errors = []
      if (!this.$v.userProfileData.firstName.$dirty) return errors
      !this.$v.userProfileData.firstName.required && errors.push('First name is required')
      !this.$v.userProfileData.firstName.maxLength && errors.push('First name must less than 32 characters')
      return errors
    },
    lastNameErrors () {
      const errors = []
      if (!this.$v.userProfileData.lastName.$dirty) return errors
      !this.$v.userProfileData.lastName.required && errors.push('Last name is required')
      !this.$v.userProfileData.lastName.maxLength && errors.push('Last name must less than 32 characters')
      return errors
    },
  },
  methods: {
    clearUserProfile() {
      this.userProfileData.firstName = ''
      this.userProfileData.lastName = ''
    },
  }
};

export default userProfileMixin;