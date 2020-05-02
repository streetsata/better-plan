<template>
  <div id="app">
  <button id="show-modal" @click="showModal = true">Войти</button>
  <Modal v-if="showModal" @close="closeModal" @open='openModalLogin'></Modal>
  <ModalLogIn  v-if="showModalLogIn" @close="closeModalLogIn" @open='openModal' @forgotPsw='openForgotPsw'></ModalLogIn>
  <ForgotPassword v-if="showForgotPsw" @close="closeForgotPsw"></ForgotPassword>
  <router-view/>
  </div>
</template>

<script>
import Modal from './components/Identification/Modal';
import ModalLogIn from './components/Identification/ModalLogIn';
import ForgotPassword from './components/Identification/ForgotPassword';

export default {
  name: 'app',
  data: () => {
  return {
    showModal: false,
    showModalLogIn: false,
    showForgotPsw: false
  }},
  components: {
    Modal,
    ModalLogIn,
    ForgotPassword
  },
  methods: {
    closeModal: function(){
      this.showModal = false
    },
    closeModalLogIn: function(){
      this.showModalLogIn = false
    },
    closeForgotPsw: function(){
      this.showForgotPsw = false
    },
    openModal: function(){
      this.showModalLogIn= false,
      this.showModal = true
    },
    openModalLogin: function(){
      this.showModal= false,
      this.showModalLogIn = true
    },
    openForgotPsw: function(){
      this.showModalLogIn= false,
      this.showForgotPsw = true
    }
  }

}
</script>

<style>
</style>