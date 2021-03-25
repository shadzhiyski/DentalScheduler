"use strict";

import axios from "axios";
import router from '../router'
import store from '../store'

// Full config:  https://github.com/axios/axios#request-config
axios.defaults.baseURL = 'https://localhost:5555/';
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;
// axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

let config = {
  baseURL: 'https://localhost:5555/',
  // timeout: 60 * 1000, // Timeout
  // withCredentials: true, // Check cross-site Access-Control
};

const _axios = axios.create(config);

_axios.interceptors.request.use(
  function(config) {
    // Do something before request is sent
    var authToken = store.getters.authToken;
    console.log(authToken);
    if (authToken) {
      config.headers = { ...config.headers, Authorization: `Bearer ${authToken}` };
    }

    return config;
  },
  function(error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

// Add a response interceptor
_axios.interceptors.response.use(
  function(response) {
    // Do something with response data
    return response;
  },
  function(error) {
    // Do something with response error
    if (!error.response || error.response.status == 401) {
      store.dispatch('logOut');
      router.push({ path: '/' });
    }

    return Promise.reject(error);
  }
);

export default _axios;
