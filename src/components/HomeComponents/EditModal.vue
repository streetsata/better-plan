<template>
    <transition v-if="show" name="fade">

    <div @click="$emit('close',true)" class="modal-container">
        <div class="modal-inner" @click.stop>
            <div class="post-modal-header">Изменить публикацию</div>
            
            <div class="modal-input">
                <div>
                <img :src="user.picture" alt="avatar">
                </div>
                <textarea v-model="value" :placeholder="`Что у вас нового, ${user.name}`" />
            </div>
            <div class="buttons"></div>
            <div class="post-button-wrapper"><button @click="$emit('edit',value)" >Изменить</button></div>
            
            
        </div>
    </div>

    </transition>

</template>

<script>
export default {
    props:['show','user','value']
}
</script>

<style>
.modal-container{
    position: fixed;
    width: 100%;
    height: 100%;
    /* background-color: gray; */
    background: rgba(153, 153, 153, 0.4);
    /* opacity: .6; */
    z-index: 20;
    display: flex;
    justify-content: center;
    align-items: center;
}
.modal-inner{
    /* opacity: 1; */
    /* margin: auto; */
    /* margin-top: auto; */
    width: 700px;
    height: 675px;
    background-color: white;
    border-radius: 21px;
    padding: 12px;
    z-index: 22;

}
.post-modal-header{
    padding-left: 4px;
    /* font-family: Lato; */
    font-style: normal;
    font-weight: normal;
    font-size: 18px;
    line-height: 24px;
    color: #2B2B2B;
}

.modal-input img{
    width: 60px;
    height: 60px;
    border-radius: 50%;
}

.modal-input{
    padding: 16px 0px;
    margin: 16px 0px;
    width: 100%;
    display: flex;
    border-top: 1px solid #999999;
    border-bottom: 1px solid #999999;
}

.modal-input textarea{
    padding: 10px 16px;
    /* padding-top: 10px; */
    width: 100%;
    height: 500px;
    resize: none;
    outline: none;
    font-size: 30px;
    line-height: 42px;
}
.modal-input textarea::placeholder{
    color: #999999;
}
.modal-input textarea::-webkit-scrollbar{
    background: #E0E0E0;
    width: 13px;
    border-radius: 21px;

}

.modal-input textarea::-webkit-scrollbar-track{
    background: #E0E0E0;

    border-radius: 21px;

}

.modal-input textarea::-webkit-scrollbar-thumb{
    background: #84A295;
    opacity: 0.5;
    border-radius: 21px;
}

.post-button-wrapper{
    display: flex;
    justify-content: center;
    align-items: center;
}

.post-button-wrapper button{
    color: #F8F8F8;
    font-size: 16px;
    line-height: 20px;
    background: #4A6FB7;
    border-radius: 98px;
    padding: 8px 14px;
    /* outline: one; */
}
.post-button-wrapper button:focus{
    outline: none;
}

.bg{
    background-color: red;
    width: 100%;
    height: 100%;
    position: fixed;
    z-index: 19;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity .5s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active до версии 2.1.8 */ {
  opacity: 0;
}

</style>