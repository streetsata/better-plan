import axios from "axios";

// const baseUrl = "https://localhost:5001/api/v1/";
const baseUrl = "https://better-plan.hillel.it/api/v1";

const api = {
  get(url, data, options) {
    return new Promise((resolve, reject) => {
      axios
        .get(baseUrl + url, data, options)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
  },
  put(url, data, options) {
    return new Promise((resolve, reject) => {
      axios
        .put(baseUrl + url, data, options)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
  },
  delete(url, data, options) {
    return new Promise((resolve, reject) => {
      axios
        .delete(baseUrl + url, data, options)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
  },
  post(url, data, options) {
    return new Promise((resolve, reject) => {
      axios
        .post(baseUrl + url, data, options)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
  }
};

export default api;
