<template>
    <div class="posts-container">
        <Post />
        <Post />
        <Post />
        <Post />
        <Post />
        <Post />
    </div>
</template>

<script>
import Post from './Post'

export default {
    components:{
        Post
    }
}
</script>

<style scoped>
.posts-container{
    overflow-y: scroll;
    margin-left: 30px;
    width: 590px;
    height: 100%;
    /* background: blue; */
}
.posts-container::-webkit-scrollbar {
    background: #E0E0E0;
    width: 10px;
    border-radius: 21px;
}
 
.posts-container::-webkit-scrollbar-track {
  /* box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3); */
    background: #E0E0E0;

    border-radius: 21px;

}
 
.posts-container::-webkit-scrollbar-thumb {
    /* background-color: darkgrey;
    outline: 1px solid slategrey; */
    background: #84A295;
    opacity: 0.5;
    border-radius: 21px;

}

</style>