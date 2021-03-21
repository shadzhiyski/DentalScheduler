import Vue from 'vue'
import App from './App.vue'
import router from './common/router'
import vuetify from './common/plugins/vuetify';

Vue.config.productionTip = false

new Vue({
  router,
  vuetify,
  render: h => h(App)
}).$mount('#app')
