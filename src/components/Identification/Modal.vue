<template>
  <transition name="modal">
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div id="id01" class="form-modal">
          <form
            class="modal-contenttt"
            @submit.prevent="register"
            ref="loginForm"
            action="/action_page.html"
          >
            <div class="imgcontainer">
              <img src="../../assets/img/modal-img.png" alt="modal image" class="modal-img" />
            </div>
            <div class="form-container">
              <a href="#" class="close-modal" @click="$emit('close')">
                <img src="../../assets/img/close_modal.png" alt="close modal" />
              </a>
              <div class="register">
                <div class="registration-heading">
                  <p>
                    Зарегистрироваться
                    <span class="green-text">
                      или
                      <a href="#" @click.prevent="$emit('open')">Войти</a>
                    </span>
                  </p>
                </div>
                <div class="input-container">
                  <div class="form-item" :class="{ 'errorInput': $v.email.$error }">
                    <input
                      class="input-field"
                      type="email"
                      v-model="email"
                      @change="$v.email.$touch()"
                      placeholder="E-mail"
                      :class="{ error: $v.email.$error }"
                      name="email"
                    />
                    <p class="error" v-if="!$v.email.required">This field is required.</p>
                    <p class="error" v-if="!$v.email.email">Needs to be a valid email.</p>
                  </div>

                  <div class="form-item" :class="{ 'errorInput': $v.password.$error }">
                    <label for="psw">Password</label>
                    <div class="psw-container">
                      <img
                        src="../../assets/img/ion_eye.png"
                        id="eye"
                        alt="hide password"
                        @click.prevent="toggleShowPassword"
                      />
                      <input
                        class="input-field"
                        type="password"
                        id="psw"
                        v-model="password"
                        @change="$v.password.$touch()"
                        :class="{ error: $v.password.$error }"
                        placeholder="Password"
                        name="psw"
                      />

                      <p class="error" v-if="!$v.password.required">This field is required.</p>
                      <p
                        class="error"
                        v-else-if="!$v.password.strongPassword"
                      >Password must have at least 6 characters, contain number and letters.</p>
                    </div>
                  </div>

                  <div class="form-item" :class="{ 'errorInput': $v.confirmedPassword.$error }">
                    <label for="psw-repeat">Password again</label>
                    <div class="psw-container">
                      <img
                        src="../../assets/img/ion_eye.png"
                        id="eye"
                        alt="hide password"
                        @click.prevent="toggleConfPassword"
                      />
                      <input
                        class="input-field"
                        type="password"
                        id="confpsw"
                        v-model="confirmedPassword"
                        @change="$v.confirmedPassword.$touch()"
                        :class="{ error: $v.confirmedPassword.$error }"
                        placeholder="Repeat password"
                        name="psw-repeat"
                      />

                      <p class="error" v-if="!$v.confirmedPassword.required">This field is required.</p>
                      <p
                        class="error"
                        v-else-if="!$v.confirmedPassword.sameAsPassword"
                      >Passwords must be identical.</p>
                    </div>
                  </div>

                  <input
                    type="checkbox"
                    class="checkbox"
                    v-model="checked"
                    @click.prevent="toggleCheckbox"
                  />
                  <label for="checkbox" @click.prevent="toggleCheckbox">Не выходить из аккаунта</label>

                  <button
                    type="submit"
                    class="arrow-link submit"
                    :disabled="submitStatus === 'PENDING'"
                  >
                    Далее
                    <i class="icono"></i>
                  </button>
                </div>

                <div class="modal-social">
                  <p>Регистрация через соцсети</p>
                  <div class="social-icons">
                    <a href="#" class="fb btn">
                      <img src="../../assets/img/facebook.png" />
                    </a>
                    <a href="#" class="google btn">
                      <img src="../../assets/img/google.png" />
                    </a>
                    <a href="#" class="inst btn">
                      <img src="../../assets/img/instagram.png" />
                    </a>
                  </div>
                </div>

                <div class="buttons-list buttons-list--info">
                  <p class="typo__p" v-if="submitStatus === 'OK'">Thanks for your submission!</p>
                  <p class="error" v-if="submitStatus === 'ERROR'">Please fill the form correctly.</p>
                  <p class="typo__p" v-if="submitStatus === 'PENDING'">Sending...</p>
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
import { required, email, sameAs } from "vuelidate/lib/validators";

export default {
  props: ["showModalLogIn"],
  name: "registration",
  data() {
    return {
      email: null,
      password: null,
      confirmedPassword: null,

      checked: false,
      showPassword: false,
      submitStatus: null
    };
  },
  validations: {
    email: {
      required,
      email
    },
    password: {
      required,
      strongPassword(password) {
        return (
          /[a-z]/.test(password) &&
          /[0-9]/.test(password) &&
          password.length >= 6
        );
      }
    },
    confirmedPassword: {
      required,
      sameAsPassword: sameAs("password")
    }
  },
  methods: {
    register: function() {
      this.$v.$touch();
      if (this.$v.$invalid) {
        this.submitStatus = "ERROR";
      } else {
        let data = {
          email: this.email,
          password: this.password
        };
        this.submitStatus = "PENDING";
        this.$store
          .dispatch(AUTH_REGISTER, data)
          .then(() => (this.submitStatus = "OK"))
          .then(() => this.$router.push("/learning"))
          .catch(err => console.log(err));
      }
    },

    toggleShowPassword: function() {
      const show = document.getElementById("psw");
      if (!this.showPassword) {
        this.showPassword = true;
        show.type = "text";
      } else {
        this.showPassword = false;
        show.type = "password";
      }
    },
    toggleConfPassword: function() {
      const show = document.getElementById("confpsw");
      if (!this.showPassword) {
        this.showPassword = true;
        show.type = "text";
      } else {
        this.showPassword = false;
        show.type = "password";
      }
    },
    toggleCheckbox: function() {
      this.checked = this.checked ? false : true;
    }
  }
};
</script>

<style scoped>
*,
*::after,
*::before {
  box-sizing: border-box;
  font-family: "Lato", sans-serif;
}

p.error {
  color: red;
  font-size: 8px;
  position: absolute;
  margin-left: 85px;
  padding-top: 3px;
  text-transform: uppercase;
}
.form-item .error {
  display: none;
}
.errorInput .error {
  display: block;
}

input.error {
  border-color: red;
  /* animation: shake .3s; */
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
  width: 700px;
  height: 400px;
  margin: 0 auto 0;
  box-sizing: border-box;
  font-size: 14px;
  color: #2b2b2b;
}

.modal-contenttt {
  height: 100%;
  display: flex;
  border-radius: 37px;
  background: #b7cdc1;
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
  background: #f8f8f8;
  border: 0px solid #f8f8f8;
  border-radius: 0 37px 37px 0;
}

.register {
  position: relative;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
  width: 100%;
  height: 100%;
  padding-bottom: 0;
}

.close-modal {
  width: 32px;
  height: 32px;
  align-self: flex-end;
  text-decoration: none;
  opacity: 0.7;
  margin: 14px 14px 0 0;
}

.close-modal:hover {
  opacity: 1;
}

.registration-heading {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100%;
  height: 10%;
  font-size: 20px;
  color: #f4896f;
}

.registration-heading p {
  margin: 0 auto;
}

.registration-heading a,
.green-text {
  color: #2c473e;
}

.registration-heading a {
  text-decoration: underline;
}

.registration-heading a:hover {
  color: #2c473e;
  cursor: pointer;
  text-decoration: none;
}

.input-container {
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  width: 70%;
  height: 75%;
  padding: 0;
  max-height: 300px;
}

.input-container label {
    margin-bottom: 0;
}

input {
  width: 100%;
  height: 33px;
  padding: 12px;
  margin: 0 auto;
  border: 1px solid #86a296;
  box-sizing: border-box;
  border-radius: 8px;
  color: #2b2b2b;
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
  text-align: start;
  font-size: 12px;
  margin-bottom: 0;
  width: 100%;
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

.checkbox + label:before {
  content: "";
  position: absolute;
  top: -1px;
  left: 0;
  width: 20px;
  height: 20px;
  border: 1px solid #86a296;
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
  left: 7px;
  top: 2px;
  width: 7px;
  height: 12px;
  border: solid #2c473e;
  border-width: 0 3px 3px 0;
  -webkit-transform: rotate(45deg);
  -ms-transform: rotate(45deg);
  transform: rotate(45deg);
}

/* Show the checkmark when checked */
.checkbox:checked + label:after {
  display: block;
}

.icono {
  position: relative;
  display: inline-block;
  vertical-align: middle;
  color: #2c473e;
  box-sizing: border-box;
  width: 0;
  height: 0;
  border-width: 6px;
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

/*delete later .arrow styles*/
.arrow-link {
  display: flex;
  align-items: center;
  justify-content: flex-end;
  align-items: baseline;
  font-size: 16px;
  font-weight: 600;
  background-color: inherit;
  line-height: 1.5;
}

.submit {
  color: #2c473e;
  border: 5px solid transparent;
  cursor: pointer;
  align-self: flex-end;
  margin-right: -15px;
}

.submit:hover {
  border: 5px solid #dfdfdf;
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
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 70%;
  height: 15%;
  margin: 0 auto;
  font-size: 12px;
}

.modal-social p {
  margin-top: 1em;
  margin-bottom: 1em;
}

.fb,
.google,
.inst {
  width: 20px;
  height: 20px;
  padding: 0;
  font-size: 1em;
  border: none;
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
  color: #2c473e;
}
</style>