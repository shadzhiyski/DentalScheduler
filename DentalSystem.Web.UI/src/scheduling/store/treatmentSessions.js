import axios from "axios";

const state = {
  treatmentSessions: []
};

const getters = {
  allTreatmentSessions: (state) => state.treatmentSessions,
};

const actions = {
  async getPatientTreatmentSessions({commit}, patientReferenceId) {
    let response = await axios.get(
      'odata/TreatmentSession',
      {
        params: {
          $expand: 'Treatment',
          $filter: `PatientReferenceId eq ${patientReferenceId}`
        }
      }
    );

    await commit("setTreatmentSessions", response.data.value);
  },
  async getDentalTeamTreatmentSessions({commit}, dentalTeamReferenceId) {
    let response = await axios.get(
      'odata/TreatmentSession',
      {
        params: {
          $expand: `Treatment,DentalTeam($filter=ReferenceId eq ${dentalTeamReferenceId})`
        }
      }
    );

    await commit("setTreatmentSessions", response.data.value);
  },
};

const mutations = {
  setTreatmentSessions(state, treatmentSessions) {
    state.treatmentSessions = treatmentSessions;
  },
};

export default {
  state,
  getters,
  actions,
  mutations,
};