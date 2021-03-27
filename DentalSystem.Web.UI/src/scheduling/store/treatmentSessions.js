import axios from "../../common/plugins/axios";

const state = {
  treatmentSession: {},
  treatmentSessions: []
};

const getters = {
  treatmentSession: (state) => state.treatmentSession,
  allTreatmentSessions: (state) => state.treatmentSessions,
};

const actions = {
  async getTreatmentSession({commit}, referenceId) {
    let response = await axios.get(
      'odata/TreatmentSession',
      {
        params: {
          $expand: 'Treatment,DentalTeam',
          $filter: `ReferenceId eq ${referenceId}`
        }
      }
    );

    const responseData = response.data.value[0];
    const treatmentSessionData = {
      referenceId: responseData.ReferenceId,
      dentalTeamReferenceId: responseData.DentalTeam.ReferenceId,
      patientReferenceId: responseData.PatientReferenceId,
      treatmentReferenceId: responseData.Treatment.ReferenceId,
      start: responseData.Start,
      startDate: responseData.Start.substr(0, 10),
      startTime: responseData.Start.substr(11, 5),
      end: responseData.End,
      status: responseData.Status
    }

    await commit("setTreatmentSession", treatmentSessionData);
  },
  async updateTreatmentSession({commit}, treatmentSessionData) {
    await axios.put('odata/TreatmentSession',treatmentSessionData);
    await commit("setTreatmentSession", treatmentSessionData);
  },
  async createTreatmentSession({commit}, treatmentSessionData) {
    await axios.post('odata/TreatmentSession',treatmentSessionData);
    await commit("setTreatmentSession", treatmentSessionData);
  },
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
  setTreatmentSession(state, treatmentSession) {
    state.treatmentSession = treatmentSession;
  }
};

export default {
  state,
  getters,
  actions,
  mutations,
};