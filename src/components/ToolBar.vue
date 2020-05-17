<template>
  <v-app-bar app flat="true">
    <v-toolbar-title>
      <router-link to="/"><logo-betterplan id="logo"></logo-betterplan> </router-link>
    </v-toolbar-title>
    <v-spacer></v-spacer>
    <v-list-item-avatar class="user_ava">
      <v-img src="https://randomuser.me/api/portraits/women/85.jpg"></v-img>
      <!-- <v-img src="https://graph.facebook.com/895127244222164/picture"></v-img>  avatar -->
    </v-list-item-avatar>
    <v-btn icon href="#">
      <v-icon>add_alert</v-icon>
    </v-btn>
  </v-app-bar>
</template>


<script>
import LogoBetterPlan from "../components/LogoBetterPlan";

export default {
  components: {
    "logo-betterplan": LogoBetterPlan
  }
}
</script>

<style>
.user_ava {
  border-radius: 50px;
  border: 3px solid rgb(61, 117, 61);
}
</style>
