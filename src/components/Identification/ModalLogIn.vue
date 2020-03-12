<template>
  <transition name="modal">
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div id="id02" class="form-modal login">
        <form class="modal-contenttt" @submit.prevent="login" action="/action_page.html">
            <div class="form-container cont-log">
                
                <div class="close">
                    <a href="#" class="close-cross">+</a>
                </div>

                <div class="register">
                    <div class="registration-heading">
                        <p>Войти <span class="green-text">или <router-link to='/register'>Зарегистрироваться</router-link></span></p>
                    </div>
                    <div class="input-container">
                        <input 
                          type="text" 
                          v-model="email"
                          class="input-field"
                          placeholder="E-mail или login" 
                          name="email" required>
                        <div>
                            <label for="psw">Password</label>
                            <div class="psw-container">
                                <img src="../../assets/img/ion_eye.png" id="eye" alt="hide password"
                                    onclick="showPassword(); changeImage();">
                                <input 
                                  type="password" 
                                  v-model="password"
                                  class="input-field"
                                  id="psw" 
                                  placeholder="Password" 
                                  name="psw" required>
                            </div>
                        </div>

                        <input type="checkbox" class="checkbox" id="checkbox" />
                        <label for="checkbox"  class="checkbox-lable">Не выходить из аккаунта</label>

                        <button type="submit" class="arrow-link submit">Далее<div class="arrow"></div></button>
                    </div>

                    <div class="modal-social soc-log">
                        <p>Вход через соцсети</p>
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

                    <div class="forgot-password">
                        <a  @click ="logout" >Забыли пароль?</a>
                    </div>
                </div>
            </div>

            <div class="imgcontainer">
                <img src="../../assets/img/login-img.png" alt="login image" class="login-img">
            </div>
        </form>
    </div>
      </div>
    </div>
  </transition>
</template>

<script>
import { AUTH_REQUEST } from "../../store/actions/auth";
import { AUTH_LOGOUT } from "../../store/actions/auth";

export default {
  name: "logIn",
  data() {
    return{
      email: '',
      password: ''
    }
  },
  methods: {
    login: function (){
      let name = this.email;
      let password = this.password;
      this.$store.dispatch(AUTH_REQUEST, { name, password }).then(() => {
      this.$router.push("/mainPage")
      .catch(err => console.log(err));
      });
    },
    logout: function () {
      this.$store.dispatch(AUTH_LOGOUT)
      .then(() => {
        this.$router.push('/login')
      })
    }
  }
};
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

.cont-log { /*for login.html*/
    border-radius: 37px 0 0 37px;
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
    /* position: relative; */
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

.soc-log { /*for login.html*/
    justify-content: space-evenly;
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

.login-img{
    height: 100%;
}

.forgot-password a {
    text-decoration: none;
    color: #2C473E;
}
</style>