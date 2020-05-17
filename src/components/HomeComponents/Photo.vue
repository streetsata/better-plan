<template>
  <img v-if="imgs" :src="imgs[0]" alt="img" class="photo">
  <div v-else class="photo stub">
    {{text}}
  </div>
</template>

<script>
export default {
  props:['imgs','text']
}
</script>

<style scoped>

</style>