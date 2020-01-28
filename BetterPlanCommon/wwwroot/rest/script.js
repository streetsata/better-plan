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
    users: [],
    posts: [],
    dialog: false,
    loading: false,
    active: 0,
    items: [
      'Content for tab 1!',
      'Content for tab 2!',
      'Content for tab 3!'
    ],
    tab: null,
    text: '',
    postText: '',
    // indicateProgressbar: 'ma-2'
  },
  computed: {
    content() {
      return this.items[this.active]
    }
  },
  methods: {
    activate(index) {
      this.active = index;
      axios.get(`https://localhost:5001/api/v1/USER/${this.users[index].id}/POSTS`)
        .then(res => {
          this.posts = res.data
          console.log(res)
          console.log(this.posts)
        })
        .catch(error => {
          console.log(error.res)
        });
    },
    
    publishPost() {
      this.dialog = false;
      let obj = JSON.stringify({ "post_text": this.text });
      axios.post(`https://localhost:5001/api/v1/USER/${this.users[this.active].id}/POST`, obj, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
        .then(res => {
          console.log(res)
          this.activate(this.active)
        })
        .catch(error => {
          console.log(error.res)
        });
    },

    editPost(postId) {
      console.log(postId);
      this.dialog = false;
      let obj = JSON.stringify({"post_id":postId, "edit_text": this.text });
      axios.put(`https://localhost:5001/api/v1/USER/${this.users[this.active].id}/EDIT`, obj, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
        .then(res => {
          console.log(res)
        })
        .catch(error => {
          console.log(error.res)
        });
        
    },

    deletePost() {
      // this.dialog = false;
      let obj = JSON.stringify({ "post_id": postId });
      axios.delete(`https://localhost:5001/api/v1/USER/${this.users[this.active].id}/DELETE`, obj, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
        .then(res => {
          console.log(res)
        })
        .catch(error => {
          console.log(error.res)
        });
    },

    // indicateProgressbar(){
    //   debugger
    //   this.loading !== false ? "ma-2":"d-none"
    // }
  },
  watch: {
    length(val) {
      this.tab = val - 1
    },
  },

  mounted() {
    axios
      .get('https://localhost:5001/api/v1/USERS')
      .then(response => {
        this.users = response.data
        this.loading = true
        this.activate(0)
      })
      .catch(error => console.log(error));
  },
});
