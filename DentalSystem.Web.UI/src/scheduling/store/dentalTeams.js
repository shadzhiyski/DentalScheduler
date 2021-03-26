import axios from "../../common/plugins/axios";

const state = {
  dentalTeams: []
};

const getters = {
  allDentalTeams: (state) => state.dentalTeams,
};

const actions = {
  async getDentalTeams({commit}) {
    let response = await axios.get('odata/DentalTeam');

    await commit("setDentalTeams", response.data.value);
  }
};

const mutations = {
  setDentalTeams(state, dentalTeams) {
    state.dentalTeams = dentalTeams;
  }
};

export default {
  state,
  getters,
  actions,
  mutations,
};