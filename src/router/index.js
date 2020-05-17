import Vue from 'vue'
import VueRouter from 'vue-router'
import FacebookWorkspace from '../views/FacebookWorkspace'
import Moodboard from '../views/Moodboard'

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
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
