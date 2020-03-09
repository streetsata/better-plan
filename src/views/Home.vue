<template>
  <div class="home-container">
      <!-- <tool-bar></tool-bar> -->
        <Modal @close="closeModal" :show="showModal" :user="users[active]" />

        <ToolBar />
        <SideMenu />
        <div v-if="active === null" class="content-wrapper">
            <Loader />
        </div>
        <div v-else class="content-wrapper">
            <UserTabs :users="users" :active="active" @select="select" />
            <div class="content">
              <ProfileInfo :user="users[active]" />
              <Posts :user="users[active]" :posts="posts" />
              <CreatePost @create="createPost" />
            </div>
        </div>
  </div>
</template>

<script>
import SideMenu from "../components/SideMenu.vue";
import Loader from "../components/Loader.vue";
import ToolBar from "../components/ToolBar.vue";
import UserTabs from "../components/HomeComponents/UserTabs"
import Posts from "../components/HomeComponents/Posts"
import ProfileInfo from "../components/HomeComponents/ProfileInfo"
import CreatePost from "../components/HomeComponents/CreatePost"
import Modal from "../components/HomeComponents/Modal"


export default {
  components: {
    SideMenu,
    ToolBar,
    UserTabs,
    Posts,
    ProfileInfo,
    CreatePost,
    Modal,
    Loader
  },
  data: () => {
    return {
      showModal:false,
      users: [],
      active: null,
      posts: null,
      
      dialog: false,
      loading: false,

      tab: null,
      text: "",
      postText: ""
    };
  },
  computed: {
    content() {
      return this.items[this.active];
    }
  },
  methods: {
    select(index){
      // console.log("XUI 2")
      this.active = index;
      this.posts = null
      this.$api
        .get(`USER/${this.users[index].id}/POSTS`)
        .then(res => {
          // this.posts = res.data;
          this.posts = res.data
          console.log(res)
        })
        .catch(error => {
          console.log(error.res)
        });
      // console.log(this.posts)
    },
    createPost(){
      // console.log('показать')
      // console.log(this.showModal)
      this.showModal = true;
    },
    closeModal(){
      this.showModal = false;
    },
    activate(index) {
      console.log(index)
      this.active = index;
      this.$api
        .get(`USER/${this.users[index].id}/POSTS`)
        .then(res => {
          this.posts = res.data;
          console.log(res);
        })
        .catch(error => {
          console.log(error.res);
        });
    },

    publishPost() {
      this.dialog = false;
      let obj = JSON.stringify({ post_text: this.text });
      this.$api
        .post(`USER/${this.users[this.active].id}/POST`, obj, {
          headers: {
            "Content-Type": "application/json"
          }
        })
        .then(res => {
          console.log(res);
          this.activate(this.active);
        })
        .catch(error => {
          console.log(error.res);
        });
    },

    editPost(postId) {
      console.log(postId);
      this.dialog = false;
      let obj = JSON.stringify({ post_id: postId, edit_text: this.text });
      this.$api
        .put(`USER/${this.users[this.active].id}/EDIT`, obj, {
          headers: {
            "Content-Type": "application/json"
          }
        })
        .then(res => {
          console.log(res);
          this.activate(this.active)
        })
        .catch(error => {
          console.log(error.res);
        });
    },

    deletePost(postId) {
      // this.dialog = false;
      let obj = JSON.stringify({ post_id: postId });
      this.$api
        .delete(`USER/${this.users[this.active].id}/DELETE`, obj, {
          headers: {
            "Content-Type": "application/json"
          }
        })
        .then(res => {
          console.log(res);
          this.activate(this.active)
        })
        .catch(error => {
          console.log(error.res);
        });
    }
  },
  watch: {
    length(val) {
      this.tab = val - 1;
    }
  },

  mounted() {
    this.$api
      .get("USERS")
      .then(response => {
        this.users = response.data
        this.select(0)

      })
      .catch(error => console.log(error));
  }
};
</script>

<style>

html,body,#app{
    width: 100%;
    height: 100%;
    font-family: Avenir, Helvetica, Arial, sans-serif;
    /* overflow: hidden; */
}

.home-container {
  height: 100%;
  width: 100%;
}

.content-wrapper{
    height: 100%;
    width: 100%;
    padding-top: 64px;
    padding-left: 56px;
}

.content{
  padding-top:10px;
  height: 90%;
  display: flex;
  flex-direction: row;
  /* justify-content:space-around; */
  /* background: green; */
}

.dialog-container {
  height: 50%;
}

/* .pos {
  position: absolute;
  z-index: 10;
} */

.side-menu{
    position: fixed;
    top: 64px;
}

.dashed-fild {
  width: 100%;
  margin-left: 15px;
  width: 98%;
  margin-right: 15px;
  border: 2px dashed lightgrey;
  background-color: rgb(241, 238, 238);
}
.tabs{
  display: flex;
  flex-direction: row;
}
.fixed-tab {
  position: absolute;
  right: 1px;
  top: 3px;
}
</style>    