import {
  AUTH_REQUEST,
  AUTH_ERROR,
  AUTH_SUCCESS,
  AUTH_LOGOUT,
  AUTH_REGISTER
} from "../actions/auth";
import axios from 'axios';

const state = {
  token: localStorage.getItem("token") || "",
  status: "",
  hasLoadedOnce: false
};

const getters = {
  isAuthenticated: state => !!state.token,
  authStatus: state => state.status
};

const actions = {
  [AUTH_REQUEST]: ({ commit }, user) => {  //login
    return new Promise((resolve, reject) => {
      // setTimeout(()=>{},750)
      commit(AUTH_REQUEST);
      axios.post('https://localhost:5001/api/Authentication/token', user, {
        headers: {
          'Content-Type': 'application/json'
        }
        })
      .then(resp => {
        console.log(resp)
        const token = resp.data.access_jwtToken
        const user = resp.data.user;
        localStorage.setItem('token', token);
        axios.defaults.headers.common['Authorization']=token
        commit(AUTH_SUCCESS, token, user)
        resolve(resp)
      })
      .catch(err => {
        commit(AUTH_ERROR)
        localStorage.removeItem('token')
        reject(err)
      })
    });
  },

[AUTH_REGISTER]: ({ commit }, user) => {
  return new Promise((resolve, reject) => {
    commit(AUTH_REGISTER)
    // this.$api.post('Authentication/registration', user)
    axios({url: 'https://localhost:5001/api/Authentication/registration', data: user, method: 'POST' })
    .then(resp => {
      console.log(resp)
      const token = resp.data.token
      const user = resp.data.user
      localStorage.setItem('token', token)
      axios.defaults.headers.common['Authorization'] = token
      commit('AUTH_SUCCESS', token, user)
      resolve(resp)
    })
    .catch(err => {
      commit('AUTH_ERROR', err)
      localStorage.removeItem('token')
      reject(err)
    })
  })
},

  [AUTH_LOGOUT]: ({ commit }) => {
    return new Promise(resolve => {
      commit(AUTH_LOGOUT);
      // axios({url: 'https://localhost:44364/api/Authentication/LogOut', data: userName, method: 'POST' })
    // .then(()=>{
      localStorage.removeItem("token");
      delete axios.defaults.headers.common['Authorization']
      resolve();     
    });
  }
};

const mutations = {
  [AUTH_REQUEST]: state => {
    state.status = "loading";
  },
  [AUTH_SUCCESS]: (state, resp) => {
    state.status = "success";
    state.token = resp;
    state.hasLoadedOnce = true;
  },
  [AUTH_ERROR]: state => {
    state.status = "error";
    state.hasLoadedOnce = true;
  },
  [AUTH_REGISTER]: state => {
    state.status = "loading";
  },
  [AUTH_LOGOUT]: state => {
    state.token = "";
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
