<template>
  <header>
    <div class="header__logo">
        <img src="../../assets/img/bp_logo.png" alt="project logo">
    </div>

    <div class="header__avatar-wrapper">
        <div class="header__avatar"></div>
        <div class="header__notifications">
            <input type="checkbox" id="menu-toggle" />
            <label for="menu-toggle" class="menu-icon" @click="$emit('show')"><img src="../../assets/img/bell.png" alt="notifications icon"></label>
        </div>
    </div>
  </header>
</template>

<style scoped>
header {
  border-bottom: 1px solid #EEEEEE;
  background: #F8F8F8;
  width: 100%;
  max-width: 1920px;
  height: 85px;
  margin: 0 auto;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-pack: justify;
      -ms-flex-pack: justify;
          justify-content: space-between;
  -webkit-box-align: center;
      -ms-flex-align: center;
          align-items: center;
  padding-left: 15px;
  padding-right: 27px;
}

.header__avatar-wrapper {
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
      -ms-flex-align: center;
          align-items: center;
}

.header__avatar-wrapper .header__avatar {
  width: 66px;
  height: 66px;
  margin-right: 21px;
  background: url(../../assets/img/social.png);
  border: 3px solid #84a295;
  border-radius: 50%;
}

#menu-toggle {
  display: none;
}
</style>