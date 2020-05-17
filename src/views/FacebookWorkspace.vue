<template>
  <div class="home-container">
      <!-- <tool-bar></tool-bar> -->
        <Modal @publish="publishPost" @close="closeModal" :show="showModal" :user="users[active]" />
        <EditModal :text="postEditText" @edit="editPost" @close="closeModal" :show="showEditModal" :user="users[active]" />

        <ToolBar />
        <SideMenu />
        <div v-if="active === null" class="content-wrapper">
            <Loader />
        </div>
        <div v-else class="content-wrapper">
            <UserTabs :users="users" :active="active" @select="select" />
            <div class="content">
              <Posts :user="users[active]" :posts="posts" @deletePost="deletePost" @editPost="showEditModalFunc" />
              <Moodboard />
              <Photos :posts="posts" />
              <CreatePost @create="createPost" />
            </div>
        </div>
  </div>
</template>

<script>
import SideMenu from "../components/SideMenu"
import Loader from "../components/Loader"
import ToolBar from "../components/ToolBar"
import UserTabs from "../components/HomeComponents/UserTabs"
import Posts from "../components/HomeComponents/Posts"
import CreatePost from "../components/HomeComponents/CreatePost"
import Modal from "../components/HomeComponents/Modal"
import EditModal from "../components/HomeComponents/EditModal"
import Moodboard from "../components/HomeComponents/Moodboard"
import Photos from "../components/HomeComponents/Photos"

export default {
  components: {
    SideMenu,
    ToolBar,
    UserTabs,
    Posts,
    // ProfileInfo,
    Moodboard,
    CreatePost,
    Modal,
    Loader,
    EditModal,
    Photos
  },
  data: () => {
    return {
      showModal:false,
      showEditModal:false,
      users: [],
      active: null,
      posts: null,

      postEditText:'',
      postEditId:'',

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
      this.active = index;
      this.posts = null
      this.$api
        .get(`USER/${this.users[index].id}/POSTS`)
        .then(res => {
          this.posts = res.data
          console.log(res.data)
        })
        .catch(error => {
          console.log(error.res)
        });
    },
    createPost(){
      this.showModal = true;
    },
    showEditModalFunc(postId,postText){
      this.showEditModal = true
      this.postEditText = postText 
      this.postEditId = postId
    },
    closeModal(edit){
      if(!edit) this.showModal = false;
      else this.showEditModal = false
    },
    activate(index) {
      console.log(index)
      this.active = index;
      this.$api
        .get(`USER/${this.users[index].id}/POSTS`)
        .then(res => {
          console.log(res.data)
          this.posts = res.data;
        })
        .catch(error => {
          console.log(error.res);
        });
    },

    publishPost(text,edit) {
      this.closeModal(edit)
      console.log(text)
      this.posts = null
      let obj = JSON.stringify({ post_text: text, "isPosting": true, "isWaiting": false });
      this.$api
        .post(`USER/${this.users[this.active].id}/POST`, obj, {
          headers: {
            "Content-Type": "application/json"
          }
        })
        .then(() => {
          this.select(this.active);
        })
        .catch(error => {
          console.log(error.res);
        });
    },

    editPost(postText) {
      this.closeModal(true)
      this.posts = null

      let obj = JSON.stringify({ FacebookPostId: this.postEditId, edit_text: postText });
      this.$api
        .put(`USER/${this.users[this.active].id}/EDIT`, obj, {
          headers: {
            "Content-Type": "application/json"
          }
        })
        .then(() => {
          this.select(this.active)
        })
        .catch(error => {
          console.log(error);
        });
    },

    deletePost(postId) {
      let obj = JSON.stringify({ FacebookPostId: postId });
      this.$api
        .delete(`USER/${this.users[this.active].id}/DELETE`, obj, {
          headers: {
            "Content-Type": "application/json"
          }
        })
        .then(res => {
          console.log(res);
          this.select(this.active)
        })
        .catch(error => {
          console.log(error);
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
        console.log('users',response.data)
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
    font-family: 'Lato', sans-serif;
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
    background: #f2f2f2;
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