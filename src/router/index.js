import Vue from 'vue';
import VueRouter from 'vue-router';
// import Login from '../components/Identification/Login.vue';
import Modal from '../components/Identification/Modal';
import ModalLogIn from '../components/Identification/ModalLogIn'
// import Posibilities from '../components/Posibilities.vue';
// import Help from '../components/Help.vue';
// import Learning from '../components/Learning.vue';
// import store from '../store';

Vue.use(VueRouter)

// const ifNotAuthenticated = (to, from, next) => {
//   if (!store.getters.isAuthenticated) {
//     next();
//     return;
//   }
//   next("/");
// };

// const ifAuthenticated = (to, from, next) => {
//   if (store.getters.isAuthenticated) {
//     next();
//     return;
//   }
//   next("/");
// };

const routes = [
  {
    path: '/registration',
    name: 'Modal',
    component: Modal,
    // beforeEnter: ifNotAuthenticated
  },
  {
    path: '/login',
    name: 'ModalLogIn',
    component: ModalLogIn,
    // beforeEnter: ifNotAuthenticated
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
