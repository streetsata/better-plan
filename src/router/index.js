import Vue from 'vue';
import VueRouter from 'vue-router';
import FacebookWorkspace from '../views/FacebookWorkspace';
import Moodboard from '../views/Moodboard';
import StartLearning from '../views/StartLearning';

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'FacebookWorkspace',
    component: FacebookWorkspace
  },
  {
    path: '/moodboard',
    name: 'Moodboard',
    component: Moodboard
  },
  {
    path: '/learning',
    name: 'StartLearning',
    component: StartLearning
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
