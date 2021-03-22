import Vue from 'vue'
import Vuex from 'vuex'
import treatments from "../../scheduling/store/treatments";
import auth from "../../identity/store/auth";

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
    auth
  }
})
