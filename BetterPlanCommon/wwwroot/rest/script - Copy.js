Vue.component('logo-betterplan', {
  template: `<a href="#" class="logo">
        <img src="rest/img/logo.svg" alt="Better Plan" class="logo__img">
        <img src="rest/img/BETTERPLAN.svg" alt="Better Plan" class="logo__text">
    </a>`
});

Vue.component('side-menue', {
  template: `<div></div>`
});

Vue.component('add-page', {
  template: `<a href="#" class="addPage" @click="length++">
        <img src="rest/img/addProject.svg" alt="Better Plan" class="add__img">
    </a>`
});

// Vue.component('add-post', {
//   template: `<a href="#" class="add_post_link">
//   <div class="add_post_content">
//       <img src="rest/img/plus.svg" alt="Better Plan" class="plus">
//       <p class="add_post_text">Добавить пост</p>
//       </div>
//   </a>`
// });
Vue.component('add-post', {
  template: `<a href="#" class="add_post_link">
  <div class="add_post_content">
  <v-btn large class="my-2" rounded>
   
  </a>
  `
});

const router = new VueRouter({
  mode: 'history'
  // routes: routes
});

new Vue({
  el: '#app',
  router: router,
  vuetify: new Vuetify(),

  data: {
    dialog: false,
    active: 0,
    items: [
      'Content for tab 1!',
      'Content for tab 2!',
      'Content for tab 3!'
    ],
    isModalVisible: false,
  },
  computed: {
    content() {
      return this.items[this.active]
    }
  },
  methods: {
    activate(index) {
      this.active = index;
      axios.get('https://api.coindesk.com/v1/bpi/currentprice.json')
        .then(res => {
          console.log(res)
        })
        .catch(error => {
          console.log(error.res)
        });
      //   FB.getLoginStatus(function(response) {
      //     statusChangeCallback(response);
      // });
    },
    showModal() {
      this.isModalVisible = true;
    },
  },
  watch: {
    length(val) {
      this.tab = val - 1
    },
  },
});