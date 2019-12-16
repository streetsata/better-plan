const postUrl = 'http://localhost:8080';
const testUrl = 'https://jsonplaceholder.typicode.com/users';

new Vue({
  el: '#app',
  data(){
    return{ 
      postText: '',
      selectedFile: null,
    }
  },
  methods: {
    onFileSelected(e){
      this.selectedFile = e.target.files[0];
      // console.log(this.selectedFile.name);
    },
    submit(){
      const formData = new FormData();
      formData.append("pictureName", JSON.stringify(this.selectedFile.name), this.selectedFile)
      formData.append("postText", JSON.stringify(this.postText))
      axios.post(postUrl, formData, {
               onUploadProgress: ProgressEvent => {
                   console.log(ProgressEvent.loaded / ProgressEvent.total);
               }
            })
            .then(res => {
              console.log(res)
            })
    }
  }
})