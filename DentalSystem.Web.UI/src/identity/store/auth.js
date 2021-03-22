import axios from "axios";

const state = {
  username: localStorage.getItem('username') || null,
  authToken: localStorage.getItem('authToken') || null
};

const getters = {
  isAuthenticated: (state) => !!state.username,
  username: (state) => state.username,
  authToken: (state) => state.authToken,
  authTokenData: (state) => state.authToken
    ? JSON.parse(atob(state.authToken.split('.')[1]))
    : {}
};

const actions = {

  async logIn({commit}, userCredentials) {
    var response = await axios.post("api/Auth/login", userCredentials);
    console.log(response);
    await commit("setUser", userCredentials.username);
    await commit("setAuthToken", response.data.accessToken);
  },

  async logOut({ commit }) {
    commit("logout");
  },
};

const mutations = {
  setUser(state, username) {
    localStorage.setItem('username', username);
    state.username = username;
  },

  setAuthToken(state, authToken) {
    localStorage.setItem('authToken', authToken);
    state.authToken = authToken;
  },

  logout(state) {
    localStorage.removeItem('username');
    localStorage.removeItem('authToken');
    state.username = null;
    state.authToken = null;
  },
};

export default {
  state,
  getters,
  actions,
  mutations,
};