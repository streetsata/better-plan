<template>
    <div class="actions-container">
        <div class="control-container">
            <div v-on:click="$emit('create')" class="create-post"></div>
            <div class="edit">
                
            </div>
            <div class="show"></div>
            <div class="delete"></div>
            <div class="play"></div>
            
        </div>
    </div>
</template>

<style scoped>
.actions-container{
    margin-left: 14px;
    display: flex;
    flex-direction: column;
}
.control-container{
    width: 47px;
    height: 264px;
    border-radius: 30px;
    display: flex;
    justify-content: space-around;
    align-items: center;
    flex-direction: column;
    border: #86A296 solid 1px;
}

.control-container div{
    width: 30px;
    height: 30px;
    border-radius: 50%;
    cursor: pointer;
}
.control-container div svg{
    background-color: black;
}

.control-container .create-post{
    border: #F3A592 1px dashed;
    background: url('/images/plus.svg') no-repeat center center;
}

.edit{
    background: url('/images/edit.svg') no-repeat center center;

}
.show{
    background: url('/images/show.svg') no-repeat center center;
}
.delete{
    background: url('/images/delete.svg') no-repeat center center;
}
.play{
    background: url('/images/play.svg') no-repeat center center;
}
</style>