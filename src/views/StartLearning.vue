<template>
  <div class="home-container">
    <ToolBarL @show="showNotification" />
    <div class="content-wrapper">
      <div class="toolswrap">
        <Learning @show="showNotification" :notifyVisibilitie="notifyVisibilitie" />
        <SideMenuL />
        <div class="tabs-container">
            <UserTabsL />
        </div>
        <div class="center"></div>
        <Notifications v-if="notifyVisibilitie" />
      </div>
    </div>
  </div>
</template>

<script>
import SideMenuL from "../components/LearningComponents/SideMenuL";
import UserTabsL from "../components/LearningComponents/UserTabsL";
import ToolBarL from "../components/LearningComponents/ToolBarL"
import Notifications from "../components/LearningComponents/Notifications";
import Learning from "../components/LearningComponents/Learning";

export default {
  components: {
    SideMenuL,
    ToolBarL,
    UserTabsL,
    Notifications,
    Learning
  },
    data: () => {
    return {
      notifyVisibilitie: false
    };
  },
  methods: {
    showNotification: function(){
      this.notifyVisibilitie = !this.notifyVisibilitie;
    }
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
}

.content-wrapper{
  padding-left: 0 !important;
}
.toolswrap{
  height: 100%;
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
}

.center{
  top: 50%;
  left: 50%;
  justify-content: center;
  width: 15px;
  height: 10px;
  z-index: 76;
  opacity: 0;
  position: absolute;
}

.home-container {
  height: 100%;
  width: 100%;
}

.content-wrapper {
  height: 100%;
  width: 100%;
  padding-top: 0 !important;
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
  min-width: 70vw;
  flex: 1 2 auto;
  margin-left: 120px;
}
</style>    