<template>
    <div class="post">
        <!-- <div @click="$emit('delete',postId)" class="delete-button">X</div> -->

        <!-- <div class="post-title">
        <img @click="$emit('edit',postId,text)" class="edit" src="/images/edit.svg" alt="edit"> 

        </div> -->
        <img 
        v-if="imgs"
        class="post-image" 
        :src="imgs[0]"
        alt="img">
        <div v-else class="picture"></div>
        <div class="post-text">
            {{text}}
        </div>
        
    </div>
</template>

<script>
export default {
    props:['name','text','avatar','imgs','postId']
}
</script>

<style scoped>
.edit{
    cursor: pointer;
}
.post{
    margin-bottom: 15px;
    width: 648px;
    height: 166px;
    border-radius: 21px;
    background-color: white;
    align-items: center;
    display: flex;
}
.picture{
    width: 130px;
    height: 130px;
    border-radius: 1px;
    margin-left: 30px;
}
/* .post-title{
    padding: 12px;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}
.user-data{
    display: flex;
    flex-direction: row;
    align-items: center;
}
.user-data img{
    width: 55px;
    height: 55px;
    border-radius: 50%;
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
} */

/* .user-data .info .date{
    font-family: Lato;
    font-style: normal;
    font-weight: normal;
    font-size: 14px;
    line-height: 20px;
    color: #999999;
} */

.post .post-text{
    min-height: 130px;
    font-style: normal;
    font-weight: 300;
    font-size: 12px;
    line-height: 20px;
    padding: 12px;
    word-wrap: break-word;
    padding: 0px 49px 0px 16px;
}
.post .post-image{
    width: 130px;
    height: 130px;
    border-radius: 1px;
    margin-left: 30px;
    /* margin-bottom: 20px; */
}
/* .delete-button{
    background: red;
    display: flex;
    justify-content: center;
    align-items: center;
    color: white;
    width: 20px;
    height: 20px;
    border-radius: 50%;
    cursor: pointer;
} */
</style>