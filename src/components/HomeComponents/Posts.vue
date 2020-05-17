<template>
    <div v-if="!posts" class="posts-container">
        <Loader />
    </div>
    <div v-else class="posts-container">
        <Post @delete="deletePost" @edit="editPost" v-for="(post,index) in posts" :key="index" :text="post.post_text" :name="user.name" :avatar="user.picture" :imgs="post.imagesURLList" :postId="post.facebookPostId" />

    </div>
</template>

<script>
// import Post from './Post'
import PostNew from './PostNew'
import Loader from '../Loader'

export default {
    props:['user','posts'],
    components:{
        Post:PostNew,
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
    margin-left: 20px;
    width: 665px;
    height: 100%;
    /* background: blue; */
}
.posts-container::-webkit-scrollbar {
    /* background: #E0E0E0; */
    width: 6px;
    border-radius: 21px;
    
}
 
.posts-container::-webkit-scrollbar-track {
    /* background: #E0E0E0; */
    /* border-radius: 21px; */
    background-image: url(/images/track.png);
    background-position: center;
    background-repeat: repeat-y;

}
 
.posts-container::-webkit-scrollbar-thumb {
    background: #84A295;
    opacity: 0.5;
    border-radius: 21px;

}

</style>