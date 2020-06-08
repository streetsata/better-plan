import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import "./assets/global.css";
import vuetify from "./plugins/vuetify";
import api from "./api/api";
import VueTour from "vue-tour"
import BootstrapVue from 'bootstrap-vue';
import  'bootstrap/dist/css/bootstrap.css';
import  'bootstrap-vue/dist/bootstrap-vue.css';

Vue.config.productionTip = false;

Vue.use(BootstrapVue);

Vue.prototype.$api = api;

require('vue-tour/dist/vue-tour.css');

Vue.use(VueTour);

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
