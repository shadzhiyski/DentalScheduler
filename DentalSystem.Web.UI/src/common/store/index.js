import Vue from 'vue'
import Vuex from 'vuex'
import treatments from "../../scheduling/store/treatments";
import treatmentSessions from "../../scheduling/store/treatmentSessions";
import auth from "../../identity/store/auth";
import userProfile from "../../identity/store/userProfile";

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    treatments,
    treatmentSessions,
    auth,
    userProfile
  }
})
