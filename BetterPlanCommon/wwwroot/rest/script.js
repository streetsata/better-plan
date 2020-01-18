Vue.component('logo-betterplan', {
    template: `<a href="#" class="logo">
        <img src="rest/img/logo.svg" alt="Better Plan" class="logo__img">
        <img src="rest/img/BETTERPLAN.svg" alt="Better Plan" class="logo__text">
    </a>`
});

Vue.component('side-menue', {
    template: `<div></div>`
});

Vue.component('add-project', {
    template: `<a href="#" class="addProject">
        <img src="rest/img/addProject.svg" alt="Better Plan" class="add__img">
    </a>`
});

const router = new VueRouter({
    mode: 'history'
    // routes: routes
});

new Vue({
    el: '#app',
    router: router,

    data: {
        active: 0,
        items: [
          'Content for tab 1!',
          'Content for tab 2!',
          'Content for tab 3!',
          'Content for tab 4!',
          'Content for tab 5!'
        ]
      },
      computed: {
        content () {
          return this.items[this.active]
        }
      },
      methods: {
        activate (index) {
          this.active = index
        }
    }
});