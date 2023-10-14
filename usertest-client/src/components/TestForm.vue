<template>
  <div>
    <div class="container">
      <h3>{{ testName }}</h3>

      <div class="section" v-for="q in questions">
        <h5>{{ q.number }}) {{ q.description }}</h5>
        <div class="divider"></div>
        <p v-for="o in q.options">
          <label>
            <input
              type="radio"
              class="with-gap"
              v-bind:value="o.number"
              v-model="q.answer"
            />
            <span>{{ o.name }}</span>
          </label>
        </p>
      </div>
      <br />
      <button
        class="btn-large"
        v-on:click="completeTest"
        :disabled="processing"
      >
        Complete
      </button>
    </div>
    <div v-show="resultPopupVisible">
      <div id="blur-screen"></div>
      <div id="result-form">
        <h2>Score: {{ points }}</h2>
        <br />
        <button class="btn" v-on:click="cancelTest">Go back</button>
      </div>
    </div>
  </div>
</template>

<script>
import WebApi from '../services/WebApiService'

export default {
  setup() {},
  data: function () {
    return {
      testId: null,
      testName: null,
      questions: null,
      resultPopupVisible: false,
      points: null,
      processing: false,
    }
  },
  beforeRouteEnter(to, from, next) {
    console.log('Before route enter test form')
    next((c) => c.loadTest())
  },
  beforeRouteUpdate(to, from) {
    console.log('Before route update test form')
    this.loadTest()
  },
  methods: {
    loadTest: function () {
      WebApi.getTest(this.$route.params.id)
        .then((test) => {
          this.testId = test.id
          this.testName = test.name
          this.questions = test.questions
        })
        .catch(() => console.error('Failed to load test'))
    },
    completeTest: function () {
      const answers = this.questions.reduce((obj, q) => {
        obj[q.number] = q.answer
        return obj
      }, {})

      this.processing = true

      WebApi.postAnswers(this.testId, this.$store.state.userName, answers)
        .then((points) => {
          console.log('Test completed with the score', points)
          this.points = points
          this.resultPopupVisible = true
        })
        .catch(() => console.error('Failed to complete test'))
        .finally(() => {
          this.processing = false
        })
    },
    cancelTest: function () {
      this.resultPopupVisible = false
      this.$router.push('/tests')
    },
  },
}
</script>

<style scoped>
#blur-screen {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: white;
  opacity: 0.7;
}
#result-form {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -60%);
  background: white;
  padding: 45px;
}
</style>
