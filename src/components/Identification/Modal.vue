<template>
  <transition name="modal">
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div id="id01" class="form-modal">
        <form class="modal-contenttt" @submit.prevent="register" ref="loginForm" action="/action_page.html">
            <div class="imgcontainer">
                <img src="../../assets/img/modal-img.png" alt="modal image" class="modal-img">
            </div>
            <div class="form-container">
              <div class="close" >
                        <a href="#" class="close-cross">+</a>
                    </div>
                <div class="register">
                    <div class="registration-heading">
                        <p>Зарегистрироваться <span class="green-text">или <router-link to='/login'>Войти</router-link></span></p>
                    </div>
                    <div class="input-container">
                        <input 
                          class="input-field"
                          type="text" 
                          v-model='email'
                          placeholder="E-mail" 
                          name="email" 
                          :rules="emailRules"
                          required />

                        <label for="psw">Password</label>
                        <div class="psw-container">
                            <img src="../../assets/img/ion_eye.png" id="eye" alt="hide password"
                                onclick="showPassword(); changeImage();">
                            <input 
                              class="input-field"
                              type="password" 
                              id="psw"
                              v-model='password'
                              :rules="[rules.required, rules.min]"
                              placeholder="Password" 
                              name="psw" 
                              required />
                        </div>
                        <label for="psw-repeat">Password again</label>
                        <div class="psw-container">
                            <img src="../../assets/img/ion_eye.png" id="eye" alt="hide password"
                                onclick="showConfirmPassword(); changeImage();">
                            <input 
                              class="input-field"
                              type="password" 
                              id="confpsw"
                              v-model="confirmedPassword" 
                              placeholder="Repeat password" 
                              name="psw-repeat" 
                              required />
                        </div>

                        <input type="checkbox" class="checkbox" />
                        <label for="checkbox" class="checkbox-lable">Не выходить из аккаунта</label>
                    </div>
                    <button type="submit" class="arrow-link submit" >Далее<div class="arrow"></div></button>
                </div>

                <div class="modal-social">
                    <p>Регистрация через соцсети</p>
                    <div class="social-icons">
                        <a href="#" class="fb btn">
                            <img src="../../assets/img/facebook.png">
                        </a>
                        <a href="#" class="google btn">
                            <img src="../../assets/img/google.png">
                        </a>
                        <a href="#" class="inst btn">
                            <img src="../../assets/img/instagram.png">
                        </a>
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
import { AUTH_REGISTER } from "../../store/actions/auth";

export default {
  name: 'registration'
,

// <button class="modal-default-button" @click="$emit('close')">
//                 OK
//               </button>

data() {
    return{
      email: '',
      password: '',
      confirmedPassword: '',

      dialog: true,
      checked: false,
      valid: true,

     emailRules: [
        v => !!v || "Required",
        v => /.+@.+\..+/.test(v) || "E-mail не корректный"
      ],

      show1: false,
      rules: {
        required: value => !!value || "Required.",
        min: v => (v && v.length >= 6) || "Меньше 6 символов"
      }
  }
  },
  
  methods: {
    register: function () {
      let data = {
        email: this.email,
        password: this.password
      }
      
      this.$store.dispatch(AUTH_REGISTER, data)
      .then(() => this.$router.push('/learning'))
      .catch(err => console.log(err))
    },
    // reset() {
    //   this.$refs.form.reset();
    // },
    // resetValidation() {
    //   this.$refs.form.resetValidation();
    // }
  }
}
</script>

<style>
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, .5);
  display: table;
  transition: opacity .3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}
.form-modal {
    width: 700px;
    height: 400px;
    margin: 0 auto 0;
    box-sizing: border-box;
    font-family: 'Lato', sans-serif;
    font-size: 14px;
    color: #2B2B2B;
}

.modal-contenttt {
    height: 100%;
    display: flex;
}

.imgcontainer {
    max-width: 320px;
}

.form-container {
    width: 55%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: center;
    background: #F8F8F8;
    border: 0px solid #F8F8F8;
    border-radius: 0 37px 37px 0;
}

.register {
    position: relative;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-between;
    width: 100%;
    height: 80%;    
    padding-bottom: 10px;
}

.close {
    position: relative;
    width: 32px;
    height: 32px;
    background: #FFAE9A;
    opacity: 0.35;
    border-radius: 0px 21px 21px 21px;
    align-self: flex-end;
    margin: 14px 14px 0 0;
}

.close-cross {
    position: absolute;
    font-size: 40px;
    font-weight: 900;
    text-decoration: none;
    top: -9px;
    left: 3px;
    color: red;
    opacity: 0.5;
    transform: rotate(-45deg);
}

.close-cross:hover {
    opacity: 1;
}

.registration-heading {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 30px;
    font-size: 20px;
    color: #F4896F;
}

.registration-heading p {
    margin: 0;
}

.registration-heading a, .green-text {
    color: #2C473E;
}

.input-container {
    display: flex;
    flex-direction: column;
    justify-content: space-evenly;
    width: 70%;
    height: 70%;
    max-height: 240px;
    padding: 25px 0;
}

.input-field{
  border: 1px solid #86A296;
    box-sizing: border-box;
    border-radius: 8px;
    height: 33px;
    padding: 12px;
}

input {
    width: 100%;
    height: 33px;
    padding: 12px;
    margin: 0 auto;
    border: 1px solid #86A296;
    box-sizing: border-box;
    border-radius: 8px;
    color: #2B2B2B;
}

.psw-container {
    position: relative;
    margin-top: 5px;
}

.psw-container img {
    position: absolute;
    top: 10px;
    right: 10px;
    cursor: pointer;
}

label {
    width: 70%;
    text-align: start;
    font-size: 12px;
}

/*CHECKBOX*/
.checkbox {
    position: absolute;
    z-index: -1;
    opacity: 0;
    margin: 5% 0 0 0;
}

.checkbox + label {
	position: relative;
    padding: 0 0 0 26px;
	cursor: pointer;
}

.checkbox-lable{
  margin-top: 20px;
}
.checkbox + label:before {
	content: '';
	position: absolute;
	top: -4px;
	left: 0;
	width: 20px;
    height: 20px;
    border: 1px solid #86A296;
	border-radius: 6px;
}

/* On mouse-over, add a background color */
.checkbox + label:hover:before {
    background-color: #dfdfdf;
}

.checkbox:checked + label:before {
	background: #dfdfdf;
}

/* Create the checkmark/indicator (hidden when not checked) */
.checkbox + label:after {
    content: "";
    position: absolute;
    display: none;
    left: 8px;
    top: 0;
    width: 5px;
    height: 10px;
    border: solid #2C473E;
    border-width: 0 3px 3px 0;
    -webkit-transform: rotate(45deg);
    -ms-transform: rotate(45deg);
    transform: rotate(45deg);
}

/* Show the checkmark when checked */
.checkbox:checked + label:after {
    display: block;
}

/*delete later .arrow styles*/
.arrow-link {
    display: flex;
    align-items: center;
    justify-content: flex-end;
    font-size: 16px;
    font-weight: 600;
    line-height: 22px;
    background-color: inherit;
}

.arrow-link a {
    display: flex;
    align-items: center;
    text-decoration: none;
    color: #F4896F;
}

.arrow {
    position: relative;
    margin-top: 3px;
    margin-left: 20px;
    width: 15px;
    height: 4px;
    background: #F4896F;
}

.arrow:after {
    content: "";
    display: block;
    position: absolute;
    width: 0;
    height: 0;
    border: 5px solid transparent;
    top: -3px;
    right: -10px;
    border-left-color: #F4896F;
    border-width: 5px 0 5px 10px;
}

.submit {
    color: #2C473E;
    /* padding: 14px 20px;
    margin: 8px 0; */
    border: 1px solid transparent;
    cursor: pointer;
    align-self: flex-end;
    margin-right: 45px;
    /* margin-right: -15px; */
}

.submit:hover {
    border: 1px solid #dfdfdf;
    border-radius: 5px;
    background: #dfdfdf;
}

.submit .arrow {
    background: #2c473e;
}

.submit .arrow:after {
    border-left-color: #2c473e;
}

.modal-social {
    height: 20%;
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 70%;
    margin: 0 auto;
    font-size: 12px;
}

.btn img {
    width: 20px;
    height: 20px;
}

/*delete later*/
.social-icons {
    display: flex;
    opacity: 0.8;
    width: 50%;
    max-width: 96px;
    justify-content: space-between;
    align-items: center;
}

.forgot-password a {
    text-decoration: none;
    color: #2C473E;
}
</style>