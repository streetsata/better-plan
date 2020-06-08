import Vue from 'vue';
import VueRouter from 'vue-router';
import FacebookWorkspace from '../views/FacebookWorkspace';
import Moodboard from '../views/Moodboard';
import StartLearning from '../views/StartLearning';
import NotFound from '../components/error-pages/NotFound';
import StartPage from '../views/StartPage';


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
    },
    {
        path: '*',
        name: 'NotFound',
        component: NotFound
    },
    {
        path: '/slider',
        name: 'StartPage',
        component: StartPage
    }
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router
