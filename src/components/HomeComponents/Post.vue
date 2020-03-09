<template>
    <div class="post">
        <div class="post-title">
            <div class="user-data">
                <img src="/images/avatar.png" alt="avatar">
                <div class="info">
                    <span class="name">{{ name }}</span>
                    <span class="date">26 июля 2017 г.</span>
                </div>
            </div>
            <img class="edit" src="/images/edit.svg" alt="edit">
        </div>
        <div class="post-text">
            {{text}}
        </div>
    </div>
</template>

<script>
export default {
    props:['name','text']
}
</script>

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