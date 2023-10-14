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
      <button class="btn-large" v-on:click="onSubmit">Submit</button>
    </div>
  </div>
</template>

<script>
import WebApi from '../services/WebApiService'

export default {
  emits: ['login'],
  data: function () {
    return {
      userName: 'user1',
      processing: false,
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
        .then(() => {
          console.log('Login completed', this.userName)
          this.$store.commit('login', this.userName)
          this.$router.push('/tests')
        })
        .catch(() => console.error('Login failed'))
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
