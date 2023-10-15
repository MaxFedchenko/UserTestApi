<template>
  <div class="container">
    <div v-for="test in tests">
      <div class="section">
        <div>
          <h4>{{ test.testName }}</h4>
          <template v-if="test.isCompleted">
            <h6>Completed {{ test.userPoints }}/{{ test.totalPoints }}</h6>
          </template>
        </div>

        <div v-if="!test.isCompleted">
          <button class="btn-small" v-on:click="startTest(test.testId)">
            Start
          </button>
        </div>
      </div>
      <div class="divider"></div>
    </div>
  </div>
</template>

<script>
import WebApi from '../services/WebApiService'

export default {
  setup: function () {
    console.log('Tests component setup')
  },
  beforeRouteEnter(to, from, next) {
    console.log('Before route rnter user tests')
    next((c) => c.getTests())
  },
  data: function () {
    return {
      tests: null,
    }
  },
  methods: {
    getTests: function () {
      WebApi.getTests()
        .then((tests) => {
          this.tests = tests
        })
        .catch(() => console.error('Failed to fetch tests'))
    },
    startTest: function (testId) {
      this.$router.push('/test/' + testId)
    },
  },
}
</script>

<style scoped>
.clearfix {
  clear: both;
}
.section {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: flex-end;
}
</style>
