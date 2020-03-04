<template>
    <div class="post">
        <div class="post-title">
            <div class="user-data">
                <img src="/images/avatar.png" alt="avatar">
                <div class="info">
                    <span class="name">Natalie Chelovek</span>
                    <span class="date">26 июля 2017 г.</span>
                </div>
            </div>
            <img class="edit" src="/images/edit.svg" alt="edit">
        </div>
        <div class="post-text">
                Ну, что посмотрел и я это гуано от «журналистов-расследователей».<br><br>Вернее, не гуано, а обычную жалкую халтуру.<br><br>Кроме манипулятивно скомпилированных нарезок из старых и новых интервью, подлавливании на словах, езды к другу другу под заупокойную музыку и унылых световых эффектов - ничего.…Ещё
        </div>
    </div>
</template>

<style scoped>
.edit{
    cursor: pointer;
}
.post{
    margin-bottom:20px;
    width: 545px;
    height: 300px;
    border-radius: 4px;
    background-color: white;
}
.post-title{
    padding: 12px;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}
.user-data{
    display: flex;
    flex-direction: row;
    /* justify-content: center; */
    align-items: center;
}
.user-data img{
    width: 55px;
    height: 55px;
    /* margin: 12px; */
}

.info{
    margin-left: 6px;
    display: flex;
    flex-direction: column;
}
.user-data .info .name{
    font-family: Lato;
    font-style: normal;
    font-weight: bold;
    font-size: 16px;
    line-height: 20px;
    color: #4A6FB7;
}

.user-data .info .date{
    font-family: Lato;
    font-style: normal;
    font-weight: normal;
    font-size: 14px;
    line-height: 20px;
    color: #999999;
}

.post .post-text{
    /* font-family: Lato; */
    font-style: normal;
    font-weight: normal;
    font-size: 14px;
    line-height: 20px;
    color: #2B2B2B;
    font-weight: 600;
    padding: 12px;
}
</style>