<template>
  <div class="home-container">
    <ToolBar />
    <div class="content-wrapper">
      <SideMenu />
      <div class="testwrap">
        <div class="tabs-container">
            <div class="tabs-wrapper">
                <div v-for="(user,index) in users" :key="user.id" class="tab" :class="index == active ? ' selected': ''" @click="$emit('select',index)">{{ user.name }}</div>
            </div>
            <img class="add-button" src="../assets/img/addProject.svg" alt="add" />
        </div>
        <Notifications />
        <!-- <div class="content">
        </div>-->
      </div>
    </div>
  </div>
</template>

<script>
import SideMenu from "../components/SideMenu";
import ToolBar from "../components/ToolBar";
// import UserTabs from "../components/HomeComponents/UserTabs";
import Notifications from "../components/LearningComponents/Notifications";

export default {
  components: {
    SideMenu,
    ToolBar,
    // UserTabs,
    Notifications
  }
};
</script>

<style scoped>
html,
body,
#app {
  width: 100%;
  height: 100%;
  font-family: "Lato", sans-serif;
  /* overflow: hidden; */
}

.testwrap{
  display: flex;
}

.home-container {
  height: 100%;
  width: 100%;
}

.content-wrapper {
  height: 100%;
  width: 100%;
  padding-top: 64px;
  padding-left: 56px;
}

.content {
  padding-top: 10px;
  height: 90%;
  display: flex;
  flex-direction: row;
}

.dialog-container {
  height: 50%;
}

.side-menu {
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

.tabs-container{
  min-width: 75vw;
}
.tabs {
  display: flex;
  flex-direction: row;
}
.fixed-tab {
  position: absolute;
  right: 1px;
  top: 3px;
}
</style>    