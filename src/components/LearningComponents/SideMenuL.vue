<template>
  <transition name="fade">
    <nav class="main__menu">
      <ul class="ul__menu">
        <div class="nav__icons">
          <li>
            <a class="main__menu-link" href="#">
              <div class="nav__icon">
                <div class="nav__icon-bcg"></div>
              </div>
              <div class="nav__text">Instagram</div>
            </a>
          </li>

          <li>
            <a class="main__menu-link" href="#">
              <div class="nav__icon">
                <img src="../../assets/img/sidebar/facebook.png" alt="facebook icon" />
              </div>
              <div class="nav__text">Facebook</div>
            </a>
          </li>

          <li>
            <a class="main__menu-link" href="#">
              <div class="nav__icon">
                <img src="../../assets/img/sidebar/stories.png" alt="stories icon" />
              </div>
              <div class="nav__text">Stories</div>
            </a>
          </li>

          <li>
            <a class="main__menu-link" href="#">
              <div class="nav__icon">
                <img src="../../assets/img/sidebar/dashboard.png" alt="moodboard icon" />
              </div>
              <div class="nav__text">Moodboard</div>
            </a>
          </li>

          <li>
            <a class="main__menu-link" href="#">
              <div class="nav__icon">
                <img src="../../assets/img/sidebar/send.png" alt="report icon" />
              </div>
              <div class="nav__text">Отчётность</div>
            </a>
          </li>

          <li>
            <a class="main__menu-link" href="#">
              <div class="nav__icon">
                <img src="../../assets/img/sidebar/trending.png" alt="strategy icon" />
              </div>
              <div class="nav__text">Стратегия</div>
            </a>
          </li>

          <li>
            <a class="main__menu-link" href="#">
              <div class="nav__icon">
                <img src="../../assets/img/sidebar/solid.png" alt="content-plan icon" />
              </div>
              <div class="nav__text">Контент-план</div>
            </a>
          </li>

          <li>
            <a class="main__menu-link" href="#">
              <div class="nav__icon">
                <img src="../../assets/img/sidebar/baseline.png" alt="posting icon" />
              </div>
              <div class="nav__text">Автопостинг</div>
            </a>
          </li>

          <li>
            <a class="main__menu-link" href="#">
              <div class="nav__icon">
                <img src="../../assets/img/sidebar/clipboard.png" alt="PK icon" />
              </div>
              <div class="nav__text">PK</div>
              </a>
          </li>

          <li class ="settings__icon">
            <a class="main__menu-link" href="#">
              <div class="nav__icon">
                <img src="../../assets/img/sidebar/settings.png" alt="settings icon" />
              </div>
            </a>
          </li>

          <li class ="dark__mode">
            <div class="toggle__button-wrapper">
              <div class="toggle__button" id="button-1">
                <input type="checkbox" class="checkbox" />
                <div class="knobs"></div>
                <div class="layer"></div>
              </div>
            </div>
          </li>
          
        </div>
      </ul>
    </nav>
  </transition>
</template>

<style scoped>
.main__menu {
  background: #F8F8F8;
  height: 100%;
  position: absolute;
  width: 100px;
  overflow: hidden;
  -webkit-transition: width .05s linear;
  transition: width .05s linear;
  z-index: 2;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
  display: -webkit-box;
  display: -ms-flexbox;
  /* display: flex; */
  /* flex: 2 1 auto; */
  position: absolute;
  -webkit-box-orient: vertical;
  -webkit-box-direction: normal;
      -ms-flex-direction: column;
          flex-direction: column;
  -webkit-box-pack: justify;
      -ms-flex-pack: justify;
          justify-content: space-between;
}

.main__menu ul {
  height: 100%;
  outline: 0;
  padding: 0;
  margin: 7px 0;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-orient: vertical;
  -webkit-box-direction: normal;
      -ms-flex-direction: column;
          flex-direction: column;
  -webkit-box-pack: justify;
      -ms-flex-pack: justify;
          justify-content: space-between;
}

.main__menu li {
  display: block;
  width: 258px;
  outline: 0;
  margin: 0;
  padding: 0;
  height: 55px;
}

.main__menu:hover {
  width: 258px;
  overflow: visible;
}

.main__menu-link {
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
      -ms-flex-align: center;
          align-items: center;
  height: 70px;
  font-size: 16px;
  font-weight: 400;
  color: #999999;
  text-decoration: none;
  -webkit-transition: all .1s linear;
  transition: all .1s linear;
}

.main__menu-link:focus {
  font-weight: 700;
}

.main__menu-link > .nav__icon {
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-pack: center;
      -ms-flex-pack: center;
          justify-content: center;
  -webkit-box-align: center;
      -ms-flex-align: center;
          align-items: center;
  width: 100px;
}

.main__menu-link > .nav__text {
  width: 158px;
}

.nav__icon-bcg {
  position: relative;
  width: 42px;
  height: 42px;
  background: #86A296;
  border-radius: 21px 0px 21px 21px;
}

.nav__icon-bcg:after {
  content: url(../../assets/img/sidebar/instagram.png);
  position: absolute;
  left: 8.5px;
  top: 8px;
}

.toggle__button-wrapper {
  width: 100px;
  height: 70px;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-pack: center;
      -ms-flex-pack: center;
          justify-content: center;
  -webkit-box-align: center;
      -ms-flex-align: center;
          align-items: center;
}

.toggle__button {
  position: relative;
  width: 72px;
  height: 37px;
  border-radius: 18.5px;
  overflow: hidden;
}

.toggle__button .checkbox {
  position: relative;
  width: 100%;
  height: 100%;
  margin: 0;
  opacity: 0;
  cursor: pointer;
  z-index: 3;
}

.toggle__button .knobs, .toggle__button .layer {
  z-index: 2;
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
}

.toggle__button .layer {
  width: 100%;
  height: 100%;
  background-color: #F2F2F2;
  z-index: 1;
}

.toggle__button .knobs:before {
  content: 'day';
  position: absolute;
  top: 2px;
  left: 2px;
  width: 33px;
  height: 33px;
  color: #86A296;
  font-size: 13px;
  font-weight: 700;
  line-height: 2.3;
  text-align: center;
  background-color: #F8F8F8;
  border-radius: 50%;
  -webkit-transition: 0.3s cubic-bezier(0.18, 0.89, 0.35, 1.15) all;
  transition: 0.3s cubic-bezier(0.18, 0.89, 0.35, 1.15) all;
}

.toggle__button .checkbox:checked + .knobs:before {
  content: 'night';
  left: 36px;
  color: #F8F8F8;
  background-color: #86A296;
}

.main__menu li:hover > a {
  color: #fff;
  background-color: #C6C6C6;
}

.main__menu li:hover .nav__icon > img {
  background-color: #fff;
}
</style>