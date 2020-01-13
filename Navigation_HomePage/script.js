Vue.component('header-nav', {
    template: `<ul class='header__list'>
        <li class='header__item'><router-link to='/posibilities' class='header__link'>Возможности</router-link></li>
        <li class='header__item'><router-link to='/learning' class='header__link'>Обучение</router-link></li>
        <li class='header__item'><router-link to='/help' class='header__link'>Помощь</router-link></li>
        <li class='header__item'><router-link to='/login' class='header__link'>Войти</router-link></li>
    </ul>`
});

Vue.component('logo-betterplan', {
    template: `<a href="#" class="logo">
        <img src="./img/logo.svg" alt="Better Plan" class="logo__img">
        <img src="./img/BETTERPLAN.svg" alt="Better Plan" class="logo__text">
    </a>`
});
const Posibilities = {
    template: '<h2>Posibilities..</h2>'
};
const Learning = {
    template: '<h2>Learning...</h2>'
};
const Help = {
    template: '<h2>Help...</h2>'
};
const LogIn = {
    template: '<h2>Log In...</h2>'
};
const NotFound = {
    template: '<h2>Page Not Found</h2>'
};

const routes = [{
        path: '/posibilities',
        component: Posibilities
    },
    {
        path: '/learning',
        component: Learning
    },
    {
        path: '/help',
        component: Help
    },
    {
        path: '/login',
        component: LogIn
    }
];

const router = new VueRouter({
    mode: 'history',
    routes: routes
});

new Vue({
    el: '#app',
    router: router
});