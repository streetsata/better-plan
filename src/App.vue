<template>
  <div id="app">
  <button id="show-modal" @click="showModal = true">Войти</button>
  <Modal v-if="showModal" @close="closeModal" @open='openModalLogin'></Modal>
  <ModalLogIn  v-if="showModalLogIn" @close="closeModalLogIn" @open='openModal'></ModalLogIn>
  <router-view/>
  </div>
</template>

<script>
import Modal from './components/Identification/Modal'
import ModalLogIn from './components/Identification/ModalLogIn'

export default {
  name: 'app',
  data: () => {
  return {
    showModal: false,
    showModalLogIn: false
  }},
  components: {
    Modal,
    ModalLogIn
  },
  methods: {
    closeModal: function(){
      this.showModal = false
    },
    closeModalLogIn: function(){
      this.showModalLogIn = false
    },
    openModal: function(){
      this.showModalLogIn= false,
      this.showModal = true
    },
    openModalLogin: function(){
      this.showModal= false,
      this.showModalLogIn = true
    }
  }

}
</script>

<style>
</style>