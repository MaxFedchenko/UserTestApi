import axios from 'axios'

export default {
  baseURL: process.env.VUE_APP_WEB_API ?? '/api/',

  getRequest: async function (url) {
    try {
      const result = await axios.get(url)
      console.log('GET ' + url, result.data)
      return result.data
    } catch (ex) {
      console.error('GET ' + url, e)
      throw ex
    }
  },
  postRequest: async function (url, body) {
    try {
      const result = await axios.post(url, body)
      console.log('POST ' + url, result.data)
      return result.data
    } catch (ex) {
      console.error('POST ' + url, e)
      throw ex
    }
  },

  loginUser: async function (user) {
    const url = this.baseURL + 'account'
    const body = {
      userName: user,
    }

    await this.postRequest(url, body)
  },
  getTests: async function (user) {
    const url = this.baseURL + 'test?user=' + user

    return await this.getRequest(url)
  },
  getTest: async function (id) {
    const url = this.baseURL + 'test/' + id

    return await this.getRequest(url)
  },
  postAnswers: async function (testId, userName, answers) {
    const url = this.baseURL + 'test/complete'

    const body = {
      testId,
      userName,
      answers,
    }

    return await this.postRequest(url, body)
  },
}
