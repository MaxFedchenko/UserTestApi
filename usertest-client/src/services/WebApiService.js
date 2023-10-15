import axios from 'axios'
import store from '@/store'

export default {
  baseURL: process.env.VUE_APP_WEB_API ?? '/api/',

  makeRequest: async function (method, url, body = null) {
    try {
      const result = await axios({
        method,
        url,
        data: body,
        headers: {
          Authorization: store.state.token
            ? 'Bearer ' + store.state.token
            : undefined,
          'Content-Type': 'application/json',
        },
      })
      console.log(`${method} ${url}`, result.data)
      return result.data
    } catch (ex) {
      console.error(`${method} ${url}`, e)
      throw ex
    }
  },

  loginUser: async function (user) {
    const url = this.baseURL + 'account'
    const body = {
      userName: user,
    }

    return await this.makeRequest('POST', url, body)
  },
  getTests: async function () {
    const url = this.baseURL + 'test'

    return await this.makeRequest('GET', url)
  },
  getTest: async function (id) {
    const url = this.baseURL + 'test/' + id

    return await this.makeRequest('GET', url)
  },
  postAnswers: async function (testId, answers) {
    const url = this.baseURL + 'test/complete'

    const body = {
      testId,
      answers,
    }

    return await this.makeRequest('POST', url, body)
  },
}
