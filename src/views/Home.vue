<template>
  <v-app>
    <v-toolbar app>
      <v-toolbar-title>
        <!-- <img src="rest/img/logo.svg" alt="Better Plan" class="logo__img"> -->
        <logo-betterplan id="logo"></logo-betterplan>
      </v-toolbar-title>

      <v-spacer></v-spacer>
      <!-- <div class="user_ava"> -->
      <v-list-item-avatar class="user_ava">
        <v-img src="https://randomuser.me/api/portraits/women/85.jpg"></v-img>
        <!-- <v-img src="https://graph.facebook.com/895127244222164/picture"></v-img>  avatar -->
      </v-list-item-avatar>
      <!-- </div> -->
      <v-btn icon href="#">
        <v-icon>add_alert</v-icon>
      </v-btn>
    </v-toolbar>
    <v-content>
      <v-container>
        <v-row>
          <v-navigation-drawer class="pos" expand-on-hover permanent>
            <v-list nav dense>
              <v-list-item link>
                <v-list-item-icon>
                  <v-icon>stars</v-icon>
                </v-list-item-icon>
                <v-list-item-title>Instagram</v-list-item-title>
              </v-list-item>
              <v-list-item link>
                <v-list-item-icon>
                  <v-icon>poll</v-icon>
                </v-list-item-icon>
                <v-list-item-title>Facebook</v-list-item-title>
              </v-list-item>
              <v-list-item link>
                <v-list-item-icon>
                  <v-icon>dashboard</v-icon>
                </v-list-item-icon>
                <v-list-item-title>Moodboard</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-navigation-drawer>

          <!--END: navigation -->
          <v-row no-gutters class="justify-center">
            <v-col cols="10">
              <v-card>
                <v-card class="tab-navigation">
                  <v-tabs background-color="gray accent-4" left>
                    <v-tabs-slider></v-tabs-slider>
                    <v-tab
                      href="#tab-1"
                      v-for="(item, index) in users"
                      @click="activate(index)"
                      :key="index"
                    >{{ item.name }}</v-tab>
                    <!-- <v-tab href="#tab-2">
                    Masterskaya @2.58 af
                    {{index + 1}}
                  </v-tab>
                  <v-tab href="#tab-3">
                    Masterskaya @2.58 af
                    {{index + 1}}
                    </v-tab>-->
                  </v-tabs>
                  <v-tab class="fixed-tab">
                    <img src="../assets/img/addProject.svg" alt />
                  </v-tab>

                  <v-row>
                    <!-- Add Post modal window -->
                    <v-dialog v-model="dialog" persistent max-width="600px">
                      <template v-slot:activator="{ on }">
                        <v-col class="dashed-fild">
                          <!-- <v-btn v-on="on"> -->
                          <v-icon v-on="on">add</v-icon>
                          <!-- </v-btn>                 -->
                          Add
                        </v-col>
                      </template>

                      <v-card>
                        <v-card-title>
                          <span class="headline">User Post</span>
                        </v-card-title>
                        <v-card-text>
                          <v-container class="dialog-container">
                            <v-row>
                              <v-col cols="12">
                                <!-- ДОБАВЛЕНИЕ ФОТО
                                <v-file-input  :rules="rules" accept="image/png, image/jpeg, image/bmp"
                                    placeholder="Pick an avatar"
                                    prepend-icon="mdi-camera"   label="Avatar"
                                ></v-file-input>-->
                                <v-textarea
                                  v-model="text"
                                  label="Message"
                                  counter
                                  maxlength="120"
                                  full-width
                                  single-line
                                ></v-textarea>
                              </v-col>
                            </v-row>
                          </v-container>
                        </v-card-text>
                        <v-card-actions>
                          <v-spacer></v-spacer>
                          <v-btn color="blue darken-1" text @click="dialog = false">Close</v-btn>
                          <v-btn color="blue darken-1" text @click="publishPost">Save</v-btn>
                        </v-card-actions>
                      </v-card>
                    </v-dialog>
                    <!--END: Add Post modal window -->
                  </v-row>
                  <!-- Progress bar -->
                  <v-row justify="center" align="center">
                    <!-- class="d-none" Скрывает прогрессбар ma-2 показывает-->
                    <v-progress-circular
                      class="d-none"
                      :indeterminate="false"
                      rotate="0"
                      size="32"
                      value="0"
                      width="4"
                      color="blue"
                    ></v-progress-circular>
                  </v-row>
                  <!-- END: Progress bar -->
                  <!-- <v-tabs-item v-model="tab"> -->
                  <v-tabs-item v-for="i in posts" :key="i" :value="'tab-' + i" v-model="tab">
                    <v-tabs-item v-if="typeof i['text'] !== 'undefined'">
                      <v-col cols="6">
                        <v-card rounded>
                          <div class="justify-center">
                            <v-list-item>
                              <v-list-item-avatar>
                                <img
                                  src="https://randomuser.me/api/portraits/women/85.jpg"
                                  alt="ava"
                                />
                              </v-list-item-avatar>
                              <v-list-item-content class="mt-2">
                                <v-list-item-title
                                  class="questrial subtitle"
                                >{{ users[active].name }}</v-list-item-title>
                              </v-list-item-content>
                              <v-list-item-action>
                                <!-- Edit modal window -->
                                <v-row>
                                  <template>
                                    <v-dialog max-width="500px">
                                      <template v-slot:activator="{ on }">
                                        <v-btn text>
                                          <v-icon v-on="on">edit</v-icon>
                                        </v-btn>
                                      </template>

                                      <v-card>
                                        <v-card-title>
                                          <span class="headline">Edit Post</span>
                                        </v-card-title>
                                        <v-card-text>
                                          <v-container class="dialog-container">
                                            <v-row>
                                              <v-col cols="12">
                                                <v-textarea
                                                  v-model="text"
                                                  label="edit text"
                                                  counter
                                                  maxlength="120"
                                                  full-width
                                                  single-line
                                                >{{ i.text }}</v-textarea>
                                              </v-col>
                                            </v-row>
                                          </v-container>
                                        </v-card-text>
                                        <v-card-actions>
                                          <v-spacer></v-spacer>
                                          <v-btn
                                            color="blue darken-1"
                                            text
                                            @click="dialog = false"
                                          >Close</v-btn>
                                          <v-btn
                                            color="blue darken-1"
                                            text
                                            @click="editPost(i.post_id)"
                                          >Save</v-btn>
                                        </v-card-actions>
                                      </v-card>
                                    </v-dialog>
                                  </template>
                                  <!--END: Edit modal window -->
                                  <v-btn text>
                                    <v-icon v-on="on">delete</v-icon>
                                  </v-btn>
                                </v-row>
                              </v-list-item-action>
                            </v-list-item>
                          </div>
                          <v-img class="grey lighten-2" :src="i.img" aspect-ratio="2.7"></v-img>

                          <v-card-text>
                            <div class="questrial body1 mb-4">{{ i.text }}</div>
                            <v-divider></v-divider>

                            <v-layout class="py-4">
                              <v-flex class="text-right">
                                <v-layout wrap justify-end>
                                  <div class="questrial mr-3 font-weight-bold">
                                    4 Comments
                                    <v-icon small>keyboard_arrow_down</v-icon>
                                    <v-icon v-if="false" small>keyboard_arrow_up</v-icon>
                                  </div>
                                  <div class="questrial ml-2 font-weight-bold mr-2">1 Share</div>
                                </v-layout>
                              </v-flex>
                            </v-layout>
                          </v-card-text>
                        </v-card>
                      </v-col>
                    </v-tabs-item>
                  </v-tabs-item>
                </v-card>
              </v-card>
            </v-col>
          </v-row>
        </v-row>
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
import LogoBetterPlan from "../components/LogoBetterPlan.vue";

export default {
  components: {
    "logo-betterplan": LogoBetterPlan
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
      // indicateProgressbar: 'ma-2'
    };
  },
  computed: {
    content() {
      return this.items[this.active];
    }
  },
  methods: {
    activate(index) {
      this.active = index;
      this.$api
        .get(`USER/${this.users[index].id}/POSTS`)
        .then(res => {
          this.posts = res.data;
          console.log(res);
          console.log(this.posts);
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
        })
        .catch(error => {
          console.log(error.res);
        });
    }

    // indicateProgressbar(){
    //   debugger
    //   this.loading !== false ? "ma-2":"d-none"
    // }
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
        this.loading = true;
        this.activate(0);
      })
      .catch(error => console.log(error));
  }
};
</script>

<style>
.user_ava .v-image {
  border-radius: 50px;
  border: 3px solid rgb(61, 117, 61);
}
.container {
  height: 90vh;
  max-width: 100vw;
}
.dialog-container {
  height: 50%;
}

/* .tab-navigation {
   
  } */
.pos {
  position: absolute;
  z-index: 10;
}
.dashed-fild {
  /* background-color: teal; */
  width: 100%;
  margin-left: 15px;
  width: 98%;
  margin-right: 15px;
  border: 2px dashed lightgrey;
  background-color: rgb(241, 238, 238);
}
.fixed-tab {
  position: absolute;
  right: 1px;
  top: 3px;
}
</style>    