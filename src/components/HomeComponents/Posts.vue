<template>
    <div v-if="!posts" class="posts-container">
        <Loader />
    </div>
    <div v-else class="posts-container">
        <Post @delete="deletePost" @edit="editPost" v-for="(post,index) in posts" :key="index" :text="post.post_text" :name="user.name" :avatar="user.picture" :imgs="post.imagesURLList" :postId="post.facebookPostId" />
    </div>
</template>

<script>
import Post from './Post'
import Loader from '../Loader'

export default {
    props:['user','posts'],
    components:{
        Post,
        Loader
    },
    mounted(){
        console.log(this.posts)
    },
    methods:{
        deletePost(postId){
            // console.log('POSTS','DELETE',postId)
            this.$emit('deletePost',postId)
        },
        editPost(postId,postText){
            this.$emit('editPost',postId,postText)
        }
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