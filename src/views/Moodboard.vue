<template>
  <div class="home-container">
        <ToolBar />
        <SideMenu />
        <div class="content-wrapper">
            <UserTabs :users="users" :active="active" @select="select" />
            <div class="content">
              
            </div>
        </div>
  </div>
</template>

<script>
import SideMenu from "../components/SideMenu"
import ToolBar from "../components/ToolBar"
import UserTabs from "../components/HomeComponents/UserTabs"
// import ProfileInfo from "../components/HomeComponents/ProfileInfo"
// import CreatePost from "../components/HomeComponents/CreatePost"
// import axios from "axios";

export default {
  components: {
    SideMenu,
    ToolBar,
    UserTabs
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