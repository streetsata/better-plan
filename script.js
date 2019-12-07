new Vue({
  el: '#app',
  
  data() {
    return {
      text: '',
      url: '',
      msg: 'App'
    }
  },
   methods: {
    uploadFiles() {
      let obj =JSON.stringify({"post_img_url":this.url,"post_text": this.text});
      axios.post('http://127.0.0.1:8080', obj,{
      contentType: 'application/json'})
        .then(res => {
          console.log(res)
        })
        .catch(error => {
          console.log(error.res)
        })
    }
  }
})