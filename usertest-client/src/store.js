import { createStore } from 'vuex'

export default createStore({
  state() {
    return {
      userName: null,
    }
  },
  mutations: {
    login(state, userName) {
      state.userName = userName
    },
  },
})
