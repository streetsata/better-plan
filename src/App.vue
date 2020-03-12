<template>
  <div id="app">
  <button id="show-modal" @click="showModal = true">Войти</button>
  <Modal v-if="showModal" @close="showModal = false">
    <h3 slot="header">custom header</h3>
  </Modal>
  <router-view/>
  </div>
</template>

<script>
import Modal from './components/Identification/Modal'

export default {
  name: 'app',
  data: () => ({
    showModal: false
  }),
  components: {
    Modal
  }
}
</script>

<style>

h1 {
  color:blueviolet;
}
</style>