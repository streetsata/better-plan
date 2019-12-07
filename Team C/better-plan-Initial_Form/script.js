new Vue({
    el: '#app',
    
    data() {
      return {
        selectedFile: null,
        text: '',
        msg: 'App'
      }
    },
    methods: {
      selectFile(event) {
        this.selectedFile = event.target.files[0]; 
      },
      uploadFiles() {
        const data = new FormData();
        data.append('image', this.selectedFile, this.selectedFile.name)
        data.append('text', this.text)
        axios.post('http://127.0.0.1:8080', data, {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          })
          .then(res => {
            console.log(res)
            
          })
          .catch(error => {
            console.log(error.res)
          })
      }
    }
  })