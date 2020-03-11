import Vue from 'vue'
import VueRouter from 'vue-router'
import FacebookWorkspace from '../views/FacebookWorkspace.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'FacebookWorkspace',
    component: FacebookWorkspace
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
