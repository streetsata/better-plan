<template>
  <div class="photos-wrapper">
    <Photo v-for="(post,index) in posts" :key="index" :imgs="post.imagesURLList" :text="post.post_text" />
    
  </div>
</template>

<script>
import Photo from './Photo'

export default {
  props:['posts'],
  components:{
    Photo
  }
}
</script>

<style scoped>
/* 184 */
.photos-wrapper{
  padding-right: 5px;
  width: 590px;
  overflow-y: auto;
  display: flex;
  justify-content: space-between;
  flex-wrap: wrap;
  align-items: flex-start;
  height: max-content;
  max-height: 100%;
  box-sizing: border-box;
}
.photo{
  width: 184px;
  height: 184px;
  border-radius: 1px;
  margin-bottom: 20px;
}
.stub{
  background: #E7E7E7;
  padding: 14px;
  color: #2B2B2B;
  font-size: 16px;
}

.photos-wrapper::-webkit-scrollbar {
    /* background: #E0E0E0; */
    width: 6px;
    border-radius: 21px;
    
}

.photos-wrapper::-webkit-scrollbar-track {
    /* background: #E0E0E0; */
    /* border-radius: 21px; */
    background-image: url(/images/track.png);
    background-position: center;
    background-repeat: repeat-y;

}
 
.photos-wrapper::-webkit-scrollbar-thumb {
    background: #84A295;
    opacity: 0.5;
    border-radius: 21px;

}

</style>