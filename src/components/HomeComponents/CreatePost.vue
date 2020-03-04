<template>
    <div class="actions-container">
        <div class="add-post-container">
            <div class="create-post">
                <span>Добавить пост</span><img src="/images/plus.svg" alt="plus">
            </div>
            <div class="controls">
                <div class="date-picker"><img src="/images/calendar.svg" alt="calendar"></div>
                <div class="posts-view">
                    <img class="block" src="/images/block.svg" alt="block">
                    <img class="list" src="/images/list.svg" alt="list">
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.actions-container{
    margin-left: 14px;
    display: flex;
    flex-direction: column;
}
.add-post-container{
    width: 280px;
    height: 115px;
    background-color: white;
    border-radius: 30px;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
}

.add-post-container .create-post{
    cursor: pointer;
    color: #F3A592;
    border: #F3A592 1px dashed;
    border-radius: 98px;
    padding: 6px 50px;
}
.add-post-container .create-post img{
    margin-left: 12px;
}
.controls{
    width: 100%;
    padding-left: 31px;
    padding-right: 31px;
    padding-top: 17px;

    display: flex;
    flex-direction: row;
    justify-content: space-between;
}
.controls .block{
    padding-right: 24px;
}
.controls img{
    cursor: pointer;
}
</style>