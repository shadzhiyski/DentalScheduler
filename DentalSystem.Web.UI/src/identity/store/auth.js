import axios from "../../common/plugins/axios";

const state = {
  username: localStorage.getItem('username') || null,
  authToken: localStorage.getItem('authToken') || null
};

const getters = {
  isAuthenticated: (state) => !!state.username,
  username: (state) => state.username,
  authToken: (state) => state.authToken,
  authTokenData: (state) => {
    var rawData = state.authToken
    ? JSON.parse(atob(state.authToken.split('.')[1]))
    : {};

    var resultData = {
      username: rawData['unique_name'],
      role: rawData['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],

    };

    if (resultData.role == 'Patient') {
      resultData.patientReferenceId = rawData['PatientReferenceId']
    } else {
      resultData.dentalTeamReferenceId = rawData['DentalTeamReferenceId']
    }

    return resultData;
  }
};

const actions = {

  async logIn({commit}, userCredentials) {
    var response = await axios.post("api/Auth/login", userCredentials);

    await commit("setUser", userCredentials.username);
    await commit("setAuthToken", response.data.accessToken);
  },

  async register({commit}, userInputData) {
    var response = await axios.post("api/Auth/register", userInputData);

    await commit("setUser", userInputData.username);
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