<template>
  <transition name="modal">
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div id="id01" class="form-modal forgot">
          <form class="modal-contentt" action="/action_page.html">
            <div class="form-container cont-forgot">
              <div class="close">
                <img src="../../assets/img/close_modal_bcg.png" alt="close modal window"/>
                <a href="#" class="close-cross" @click="$emit('close')">
                  <img src="../../assets/img/close_modal_cross.png" alt="cross"/>
                </a>
              </div>

              <div class="register">
                <div class="forgotpsw-container" v-if="showInput">
                <div class="forgot-heading">
                  <p>Восстановление пароля</p>
                </div>
                <div class="input-container input-forgot" >
                  <input
                    type="text"
                    v-model="email"
                    placeholder="E-mail"
                    name="email"
                    class="input-field"
                    required
                  />
                  <button type="submit" class="arrow-link submit" @click="showRecoveredText">
                    Далее
                    <i class="icono"></i>
                  </button>
                </div>
                </div>
                <div class="recovered-psw" v-else>
                  <div class="recovered-psw__text">
                  <p>Мы отправили на Вашу почту 
                    {{email}} ссылку для восстановления пароля</p>
                </div>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </transition>
</template>

<script>
export default {
  name: "forgotPsw",
  data() {
    return {
      email: "",
      showInput: true
    };
  },
  methods: {
    send: function() {
      let name = this.email;
      console.log(name);
    },
    showRecoveredText: function(){
      this.showInput=false;
    }
  },
};
</script>

<style>
*, *::after, *::before {
     box-sizing: border-box;
     font-family: "Lato", sans-serif;
 }
 
 .modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.form-modal {
  max-width: 386px;
  margin: 0 auto 0;
  font-size: 14px;
  color: #2b2b2b;
}

.modal-contentt{
  align-items: center;
  background: #f8f8f8;
  border: 0px solid #f8f8f8;
  border-radius: 37px 0 37px 37px;
}
.forgot-heading{
  display: flex;
  align-items: center;
  justify-content: center;
  padding-top: 45px;
  font-size: 20px;
  color: #f4896f;
  margin: 0 auto;
}

.recovered-psw__text{
  font-size: 20px;
  color: #2C473E;
  text-align: center;
  font-weight: 300;
  padding: 45px;
}

.input-forgot {
  display: flex;
  flex-direction: column;
  justify-content: center;
  width: 60%;
  margin: 0 auto;
  padding: 20px 0 10px 0;
}

.cont-forgot{
  position: relative;
}

.close img{
  position: absolute;
  right: 0;
  padding: 10px;
}

.input-field {
  margin: 0 auto;
  border: 1px solid #86a296;
  box-sizing: border-box;
  border-radius: 8px;
  width: 100%;
  height: 33px;
  padding: 12px;
}

.input-forgot>.arrow-link {
  padding: 25px 0;
  display: flex;
  justify-content: flex-end;
  font-size: 14px;
  font-weight: 600;
  line-height: 20px;
  color: #2c473e;
}

.input-forgot .icono {
  position: relative;
  display: inline-block;
  vertical-align: middle;
  box-sizing: border-box;
  width: 0;
  height: 0;
  top: 6px;
  border-width: 5px;
  border-style: solid;
  border-bottom-color: transparent;
  border-left-color: transparent;
  margin-left: 20px;
  transform: rotate(45deg);
}

.icono:before {
  content: "";
  box-sizing: border-box;
  right: 0;
  top: -3px;
  position: absolute;
  height: 4px;
  box-shadow: inset 0 0 0 32px;
  /* -webkit-transform: rotate(-45deg); */
  transform: rotate(-45deg);
  width: 15px;
  /* -webkit-transform-origin: right top; */
  transform-origin: right top;
}
</style>
