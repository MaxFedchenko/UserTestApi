<template>
  <div id="login-form" class="row">
    <div class="col s12 offset-m3 m6 offset-l4 l4">
      <h1>Login</h1>
      <br />
      <br />
      <p>
        <input
          placeholder="User Name"
          v-model="userName"
          :disabled="processing"
        />
      </p>
      <h6 class="red-text" v-show="loginFailed">Failed to login</h6>
      <br />
      <button class="btn-large" v-on:click="onSubmit" :disabled="processing">
        Submit
      </button>
    </div>
  </div>
</template>

<script>
import WebApi from '../services/WebApiService'

export default {
  data: function () {
    return {
      userName: 'User1',
      processing: false,
      loginFailed: false,
    }
  },
  methods: {
    onSubmit: function () {
      if (!this.userName?.trim()) {
        console.error('Empty input on submit')
        return
      }

      this.processing = true

      WebApi.loginUser(this.userName)
        .then((token) => {
          console.log('Login completed', this.userName)
          this.loginFailed = false
          this.$store.commit('setToken', token)
          this.$router.push('/tests')
        })
        .catch(() => {
          console.error('Login failed')
          this.loginFailed = true
        })
        .finally(() => (this.processing = false))
    },
  },
}
</script>

<style scoped>
#login-form {
  position: fixed;
  top: 50%;
  width: 100%;
  transform: translate(0, -60%);
}
</style>
