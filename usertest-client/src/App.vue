<template>
  <div class="navbar-fixed">
    <nav>
      <div class="nav-wrapper container">
        <div class="brand-logo s right center">Tests</div>
        <ul class="left">
          <li><router-link to="/login">Login</router-link></li>
          <li><router-link to="/tests">Test List</router-link></li>
        </ul>
      </div>
    </nav>
  </div>
  <router-view></router-view>
</template>

<script>
import M from 'materialize-css'

export default {
  name: 'App',
  mounted() {
    M.AutoInit()
  },
}
</script>

<style>
.nav-wrapper a {
  font-size: 120%;
}
</style>
