<template>
  <div>
    <v-tour name="myTour" :steps="steps" :callbacks="myCallbacks" :options="myOptions">
    </v-tour>
  </div>
</template>

<script>
  export default {
    name: 'Learning',
    props:['notifyVisibilitie'],
    data () {
      return {
          myOptions: {
          labels: {
            buttonSkip: 'Пропустить',
            buttonNext: 'Далее',
            buttonStop: 'Окей'
          },
          startTimeout: 1000
        },
        steps: [
          {
            target: '.header__avatar',  
            content: `<p>Личный кабинет</p>Изменяйте и настраивайте основную информацию о себе во вкладке “Найстройки профиля”.`,
            params: {
              highlight: true,
            }
          },
          {
            target: '.header__notifications',
            content: `<p>Уведомления</p>Обо всех изменениях в проекте сигнализирует колокольчик. Чтобы просмотреть уведомления - кликните на колокольчик.`,
            params: {
              highlight: true,
            }
          },
          {
            target: '.notifications',
            content: `<p>Уведомления</p>Все уведомления сортируются на “Новые” и уже просмотренные ранее. Здесь вы найдете информацию о дете и времени и проекте.`,
            params: {
              highlight: true,
              placement: 'right'
            }
          },
          {
            target: '.notifications__bottom',
            content: `<p>Уведомления</p>Вы можете отфильтровать все уведомления чтобы видеть только те, что касаются одного проекта, с которым работаете сейчас.`,
            params: {
              highlight: true,
              placement: 'right'
            }
          },
          {
            target: '.draft-tab',
            content: `<p>Проекты</p>В строке проектов вы можете переключаться между активными проектами. Если проект редактируют вы увидите индикатор.`,
            params: {
              highlight: true,
            }
          },
          {
            target: '.draft-tab',
            content: `<p>Строка проектов</p>Во временем, проектов может стать больше, и чтобы просмотреть все проекты нажмите на стрелочку вниз.`,
            params: {
              highlight: true,
            }
          },
          {
            target: '.create-project',
            content: `<p>Новый проект</p>Для того чтобы добавить новый проект перейдите во вкладку создания проекта нажав на плюсик.`,
            params: {
              highlight: true,
            }
          },
          // {
          //   target: '.v-btn--icon',
          //   content: `<p>Создание проекта</p>Во вкладке создания проекта мы можете добавлять всю детальную информацию и производить настройки.`,
          //   params: {
          //     highlight: true,
          //   }
          // },
          {
            target: '.main__menu',
            content: `<p>Функции проекта</p>Все функции которые вы подключили к проекту во вкладке создания проекта отображаются здесь.`,
            params: {
              highlight: true,
              placement: 'left'
            }
          },
          {
            target: '.settings__icon',
            content: `<p>Настройка проекта</p>Чтобы изменить набор функций или редактировать информацию о проекте перейдите в настройки проекта.`,
            params: {
              highlight: true,
              placement: 'left'
            }
          },
          {
            target: '.dark__mode',
            content: `<p>Dark mode</p>Вы можете настраивать цветовую схему Better Plan - с легкостью переходите в ночной режим.`,
            params: {
              highlight: true,
              placement: 'left'
            }
          },
          {
            target: '.main__menu',
            content: `<p>Рабочая область</p>Вы можете расширить рабочую область проекта свернув блок с функциями проекта и в любой момент вызвать его обратно.`,
            params: {
              highlight: true,
              placement: 'left'
            }
          },
          {
            target: '.center',
            content: `<p>Рабочая область</p>Вы прошли первый блок обучения Better Plan, продолжите обучение из вкладки “Настройки профиля“ и оставляйте обратную связь.`,
            params: {
              highlight: true,
            }
          }
        ],
        myCallbacks:{
          onNextStep: this.myCustomNextStepCallback
        }
      }
    },
    methods: {
      myCustomNextStepCallback (currentStep) { 
        if (currentStep === 1) {
          document.querySelector('.menu-icon').click();
        }
        
        if(currentStep === 3){
          document.querySelector('.menu-icon').click();
          // document.querySelector('.create-project').click();
        }
        if(currentStep === 4){
          document.querySelector('.tab-title').click();
        }
        if(currentStep === 5){
          document.querySelector('.tab-title').click();
        }
        if(currentStep === 6){
          let sideMenu = document.querySelector('.main__menu');
          sideMenu.style.cssText="width: 258px;  overflow: visible;"
          document.querySelector('.nav__icon').click();
        }
        if(currentStep === 9){
          let sideMenu = document.querySelector('.main__menu');
          sideMenu.style.cssText="width: 100px;  overflow: hidden;"
          document.querySelector('.nav__icon').click();
        }

      }
    },
    mounted: function () {
      this.$tours['myTour'].start()
    }
  }
</script>

<style>
.v-step {
    width: 340px;
    height: 193px;
    color: #2B2B2B !important;
    background: #F8F8F8 !important;
    border-radius: 37px 0px 37px 37px !important;
    -webkit-filter: drop-shadow(0 0 2px rgba(0,0,0,.2));
    filter: drop-shadow(0 0 2px rgba(0,0,0,.5));
    padding: 1rem  !important;
    text-align: center;
    z-index: 10000;
}

.v-step[x-placement^=bottom]{
  margin-top: 50px  !important;
}

.v-step[x-placement^=left]{
  margin-right: 50px  !important;
}

.v-step[x-placement^=right]{
  margin-left: 50px  !important;
}

.v-step__button{
    color: #F8F8F8 !important;
    background: #F4896F !important;
    border-radius: 14px !important;
    padding: 0px 30px !important;
    font-weight: bold;
}

.v-step__button:hover{
    background: #FFAE9A !important;
}

.v-step__button-previous{
    display: none !important;
}

.v-step__button-skip{
    background: #F8F8F8 !important;
    color: #999999 !important;
    font-size: 10px !important;
    text-decoration-line: underline !important;
    display: block !important;
    margin: 0 auto !important;
}

.v-step__button-skip:hover{
    background: #F8F8F8 !important;
    font-weight: bold !important;
}

.v-step__button-stop{
    margin-top: .7rem !important;
}

.v-tour__target--highlighted{
    box-shadow: 0 0 0 99999px rgba(0,0,0,.4) !important;
}

.v-step .v-step__arrow{
  margin: .8rem !important;
}

.v-step[x-placement^=bottom] .v-step__arrow{
  border-width: 1px !important;
  height: 50px !important;
  top: -50px !important;
  /* left: calc(50% - 1rem); */
  margin-top: 0 !important;
  margin-bottom: 0 !important;
}

.v-step[x-placement^=left] .v-step__arrow{
  border-width: 1px !important;
  width: 60px !important;
  right: -50px !important;
  margin-right: 0 !important;
  margin-left: 0 !important;
}

.v-step[x-placement^=right] .v-step__arrow{
  border-width: 1px !important;
  width: 60px !important;
  left: -50px !important;
  margin-right: 0 !important;
  margin-left: 0 !important;
}

.v-step__arrow{
    /* border-color: #62716C !important; */
    border-color: #f8f8f8 !important;
    /* border-left-color: transparent !important;
    border-right-color: transparent !important;
    border-top-color: transparent !important; */
}

.v-step__content{
    margin: 0 !important;
    font-size: 14px;
    color: #2B2B2B;
}

.v-step__content p{
    margin: 0 0 0.2rem 0;
    font-size: 20px;
    color: #F4896F;
}
</style>
