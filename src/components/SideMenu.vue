 <template>
  <v-navigation-drawer class="side-menu" style="top:64px" expand-on-hover permanent>
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
</template>

<style scoped>
.side-menu{
  z-index: 10;
}
</style>