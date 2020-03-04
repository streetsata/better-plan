<template>
    <!-- <v-tabs background-color="gray accent-4" left>
    <v-tabs-slider></v-tabs-slider>
    <v-tab
        v-for="(user, index) in users"
        :key="index"
        @click="$emit('select-user',index)"
    >
    {{user.name}}
    </v-tab>

    </v-tabs> -->
    <div>
        <div class="tabs-container">
            <div class="tabs-wrapper">
                <div class="tab">Masterskaya @2.58 af</div>
                <div class="tab selected">Masterskaya @2.58 af</div>
            </div>
            <img class="add-button" src="../../assets/img/addProject.svg" alt="add" />
        </div>
        
    </div>
    
</template>

<script>
export default {
    props:['users']
}
</script>

<style>
.tabs-container{
    display: flex;
    flex-direction: row;
    background: #FFFFFF;
    border-radius: 80px;
    min-height: 30px;
    margin-top: 15px;
    margin-left: 11px; 
    margin-right: 11px; 
}

.tabs-wrapper{
    display: flex;
    flex-direction: row;
    width: 100%;
}

.add-button{
    cursor: pointer;
    margin-right: 5px;
}

.tab{
    cursor: pointer;
    /* border: solid black 1px; */
    min-width: 200px;
    
    border: 1px solid #FFFFFF;

    box-sizing: border-box;
    border-radius: 80px;
    margin: 6px;
    padding: 4px 22px;
    /* font-family: sans-serif; */
    font-style: normal;
    font-weight: normal;
    font-size: 16px;
    line-height: 33px;
    /* identical to box height, or 206% */

    text-align: center;

    color: #999999;
    transition: .3s
}
.tab:hover{
    border: 1px solid #86A296;
}
.tab.selected{
    border: 1px solid #86A296;
    color: #86A296;
}
</style>