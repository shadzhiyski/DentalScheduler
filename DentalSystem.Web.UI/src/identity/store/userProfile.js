import axios from "../../common/plugins/axios";
import FormData from 'form-data';

const state = {
  userProfile: {
    firstName: null,
    lastName: null,
  },
};

const getters = {
  userProfile: (state) => {
    return { ...state.userProfile };
  },
}

const actions = {
  async getUserProfile({commit}) {
    const userProfileData = await axios.get("api/User/profile");
    await commit("setUserProfile", userProfileData.data);
  },
  async updateUserProfile({commit}, userProfileData) {
    var form = new FormData();
    form.append('FirstName', userProfileData.firstName);
    form.append('LastName', userProfileData.lastName);
    await axios.post("api/User/profile", form, { headers: { "Content-Type": "multipart/form-data" } });
    await commit("setUserProfile", userProfileData);
  },
};

const mutations = {
  setUserProfile(state, userProfileData) {
    state.userProfile = { ...userProfileData };
  },
};

export default {
  state,
  getters,
  actions,
  mutations,
};