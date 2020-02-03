import axios from "axios";

const baseUrl = 'https://localhost:5001/api/v1/';

const api = {
    get(url, onSuccess, onReject) {
        axios.get(baseUrl + url)
        .then(response => onSuccess(response))
        .catch(error => onReject(error));
    }
}

Vue.prototype.$api = api;