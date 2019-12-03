Vue.component('post-comp', {
    data: function(){
        return {
            postText: "",
            postPhoto: null,
        }
    },
    methods: {
        sendFormData: function(event){
            event.preventDefault();

            if(this.postText === ""){
                alert("Please add post text");
                return;
            }

            if(this.postPhoto === null){
                alert("Please select photo");
                return;
            }

            let formData = new FormData();
            formData.append('postText', this.postText);
            formData.append('postPhoto', this.postPhoto);

            fetch('http://localhost:6534/post', {
                method: 'POST',
                body: formData
            })
            .then(response => response.text())
            .then(responseText => alert(responseText))
            .catch(err => alert(err.message));
        },
        getPhoto(event){
            this.postPhoto = event.target.files[0];
        }
    },
    template: `<form id="formElem" v-on:submit="sendFormData">
                    <textarea v-model="postText"></textarea>
                    <input type="file" @change="getPhoto" accept="image/*">
                    <input type="submit">
               </form>`
})

var app = new Vue({
    el: '#app'
})