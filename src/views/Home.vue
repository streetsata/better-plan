<template>
  <div class="home-container">
      <!-- <tool-bar></tool-bar> -->
        <ToolBar />
        <SideMenu />

        <div class="content-wrapper">
            <UserTabs />
            <div class="content">
              <ProfileInfo />
              <Posts />
              <CreatePost />
            </div>
        </div>
  </div>
</template>

<script>
import SideMenu from "../components/SideMenu.vue";
import ToolBar from "../components/ToolBar.vue";
import UserTabs from "../components/HomeComponents/UserTabs"
import Posts from "../components/HomeComponents/Posts"
import ProfileInfo from "../components/HomeComponents/ProfileInfo"
import CreatePost from "../components/HomeComponents/CreatePost"

export default {
  components: {
    SideMenu,
    ToolBar,
    UserTabs,
    Posts,
    ProfileInfo,
    CreatePost
  },
  data: () => {
    return {
      users: [],
      posts: [],
      dialog: false,
      loading: false,
      active: 0,
      items: ["Content for tab 1!", "Content for tab 2!", "Content for tab 3!"],
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
        this.users = response.data;
        this.activate(0);
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