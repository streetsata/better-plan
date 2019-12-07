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
      const data = new FormData();
      data.append("post_img_url", this.url)
      data.append("post_text", this.text)
      let obj ={};
      data.forEach((value, key)=> obj[key]=value);
      axios.post('http://127.0.0.1:8080', JSON.stringify(obj))
        .then(res => {
          console.log(res)
        })
        .catch(error => {
          console.log(error.res)
        })
    }
  }
})