import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import "./assets/global.css";
import vuetify from "./plugins/vuetify";
import api from "./api/api";
import Vuelidate from 'vuelidate';

Vue.config.productionTip = false;

Vue.prototype.$api = api;

Vue.use(Vuelidate)

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
