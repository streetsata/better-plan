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
              <ProfileInfo :user="users[active]" />
              <Posts :user="users[active]" :posts="posts" @deleteost="deletePost" @editPost="showEditModalFunc" />
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
import EditModal from "../components/HomeComponents/EditModal"


export default {
  components: {
    SideMenu,
    ToolBar,
    UserTabs,
    Posts,
    ProfileInfo,
    CreatePost,
    Modal,
    Loader,
    EditModal
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
          this.posts = res.data;
          console.log(res);
        })
        .catch(error => {
          console.log(error.res);
        });
    },

    publishPost(text,edit) {
      this.closeModal(edit)
      console.log(text)
      this.posts = null
      // console.log(this.postText)
      let obj = JSON.stringify({ post_text: text, "isPosting": true, "isWaiting": false });
      this.$api
        .post(`USER/${this.users[this.active].id}/POST`, obj, {
          headers: {
            "Content-Type": "application/json"
          }
        })
        .then(res => {
          console.log(res);
          this.select(this.active);
        })
        .catch(error => {
          console.log(error.res);
        });
    },

    editPost(postText) {
      console.log(this.postEditId, postText);
      // this.showEditModal = true
      this.closeModal(true)
      this.posts = null

      // this.dialog = false;
      let obj = JSON.stringify({ FacebookPostId: this.postEditId, edit_text: postText });
      this.$api
        .put(`USER/${this.users[this.active].id}/EDIT`, obj, {
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
    },

    deletePost(postId) {
      // this.dialog = false;
      console.log('delete',postId)
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