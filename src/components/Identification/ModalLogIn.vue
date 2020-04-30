<template>
  <transition name="modal">
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div id="id02" class="form-modal login">
          <form class="modal-contenttt" @submit.prevent="login" action="/action_page.html">
            <div class="form-container cont-log">
              <a href="#" class="close-modal" @click="$emit('close')">
                <img src="../../assets/img/close_modal.png" alt="close modal" />
              </a>

              <div class="register">
                <div class="registration-heading">
                  <p>
                    Войти
                    <span class="green-text">
                      или
                      <a @click.prevent="$emit('open')">Зарегистрироваться</a>
                    </span>
                  </p>
                </div>
                <div class="input-container login-input">
                  <input
                    type="text"
                    v-model="email"
                    class="input-field"
                    placeholder="E-mail или login"
                    name="email"
                    required
                  />
                  <div class="form-item">
                    <label for="psw">Password</label>
                    <div class="psw-container">
                      <img
                        src="../../assets/img/ion_eye.png"
                        id="eye"
                        alt="hide password"
                        @click.prevent="toggleShowPassword"
                      />
                      <input
                        type="password"
                        v-model="password"
                        class="input-field"
                        id="psw"
                        placeholder="Password"
                        name="psw"
                        required
                      />
                    </div>
                  </div>

                  <input type="checkbox" class="checkbox" id="checkbox" />
                  <label for="checkbox" class="checkbox-lable">Не выходить из аккаунта</label>

                  <button type="submit" class="arrow-link submit">
                    Далее
                    <i class="icono"></i>
                  </button>
                </div>

                <div class="modal-social soc-log">
                  <p>Вход через соцсети</p>
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

                <div class="forgot-password">
                  <a @click="logout">Забыли пароль?</a>
                </div>
              </div>
            </div>

            <div class="imgcontainer">
              <img src="../../assets/img/login-img.png" alt="login image" class="login-img" />
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
    return {
      email: "",
      password: "",

      showPassword: false,
    };
  },
  methods: {
    login: function() {
      let name = this.email;
      let password = this.password;
      this.$store.dispatch(AUTH_REQUEST, { name, password })
      .then(() => {
        // location.replace("http://google.com/")
        this.$router.push("/mainPage")
        .catch(err => console.log(err));
      });
    },
    logout: function() {
      this.$store.dispatch(AUTH_LOGOUT)
      .then(() => {
        this.$router.push("/login");
      });
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
  }
};
</script>

<style scoped src='./Modal.css'>
</style>
