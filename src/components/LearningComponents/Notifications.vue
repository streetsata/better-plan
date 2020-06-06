<template>
  <transition name="fade">
    <div class="notifications">
      <div class="notifications__main">
        <div class="notifications__new">Новые</div>

        <div class="grid">
          <div class="mark">
            <div></div>
          </div>
          <div class="time">
            <p>11:00</p>
          </div>
          <div class="comment">
            <p>masterskaya2.58af</p>
            <p>
              <b>Джон Броуди</b> прокомментировал “Это фото нравится, оставляем, но убираем все
              надписи”
            </p>
          </div>
        </div>

        <div class="grid with-photo">
          <div class="mark">
            <div></div>
          </div>
          <div class="time">
            <p>19:00</p>
            <p class="time-date">Вчера</p>
          </div>
          <div class="comment">
            <p>masterskaya2.58af</p>
            <p>
              <b>Джоанна Беккер</b> добавила текст к фото
            </p>
          </div>
          <div class="comment-photo"></div>
        </div>

        <div class="notifications__new read">Просмотренные</div>

        <div class="grid">
          <div class="mark">
            <div></div>
          </div>
          <div class="time">
            <p>11:00</p>
            <p class="time-date">Вчера</p>
          </div>
          <div class="comment">
            <p>masterskaya2.58af</p>
            <p>
              <b>Джон Броуди</b> прокомментировал “Это фото нравится, оставляем, но убираем все
              надписи”
            </p>
          </div>
        </div>

        <div class="grid with-photo">
          <div class="mark">
            <div></div>
          </div>
          <div class="time">
            <p>11:00</p>
            <p class="time-date">18.01</p>
          </div>
          <div class="comment">
            <p>masterskaya2.58af</p>
            <p>
              <b>Джоанна Беккер</b> заменила фото
            </p>
          </div>
          <div class="comment-photo"></div>
        </div>

        <div class="grid">
          <div class="mark">
            <div></div>
          </div>
          <div class="time">
            <p>11:00</p>
            <p class="time-date">01.01</p>
          </div>
          <div class="comment">
            <p>masterskaya2.58af</p>
            <p>
              <b>Джон Броуди</b> прокомментировал “Это фото нравится, оставляем, но убираем все
              надписи”
            </p>
          </div>
        </div>

        <div class="grid with-photo">
          <div class="mark">
            <div></div>
          </div>
          <div class="time">
            <p>11:00</p>
            <p class="time-date">20.12</p>
          </div>
          <div class="comment">
            <p>masterskaya2.58af</p>
            <p>
              <b>Джоанна Беккер</b> добавила фото
            </p>
          </div>
          <div class="comment-photo"></div>
        </div>
        <div class="notifications__bottom">
          <input type="checkbox" class="checkbox checkbox-in-learning" id="checkbox" />
          <label for="checkbox">Уведомления только по этому проекту</label>
        </div>
      </div>
    </div>
  </transition>
</template>

<style scoped>
.notifications {
  background-color: #fff;
  flex: 2 1 auto;
  width: 165px;
  padding: 20px 0;
  height: 100%;
  transition: all 300ms ease-in-out;
  font-size: 12px;
  line-height: 17px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
}

.notifications__main {
  display: flex;
  flex-direction: column;
  align-items: center;
  box-sizing: border-box;
}

.notifications__new {
  width: 68px;
  height: 21px;
  background: #f4896f;
  border-radius: 80px;
  text-align: center;
  font-weight: bold;
  color: #ffffff;
  line-height: 19px;
}

.read {
  width: 120px;
  background: #b7cdc1;
}

.grid {
  margin-top: 15px;
  display: grid;
  grid-template-columns: 21px 50px 1fr;
  gap: 0;
  padding-right: 15px;
  align-items: start;
}

.with-photo {
  grid-template-columns: 21px 50px 1fr 40px;
}

.grid p {
  margin: 0;
}

.mark {
  box-sizing: border-box;
  width: 21px;
  height: 21px;
  background: transparent;
  position: relative;
}

.mark div {
  position: absolute;
  top: 8px;
  left: 8px;
  width: 5px;
  height: 5px;
  background: #f4896f;
  border-radius: 50%;
}

.time {
  color: #999999;
}

.time-date {
  font-size: 10px;
}

.comment p:first-child {
  color: #86a296;
}

.comment-photo {
  width: 40px;
  height: 40px;
  background: #c4c4c4;
}

.notifications__bottom {
  margin-top: 20px;
}
</style>