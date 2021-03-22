import Vue from 'vue'
import App from './App.vue'
import router from './common/router'
import vuetify from './common/plugins/vuetify';
import store from './common/store'
import './common/plugins/axios'

Vue.config.productionTip = false

new Vue({
  router,
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app')
