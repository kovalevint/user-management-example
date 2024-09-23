<template>
  <div id="app">
    <v-app>
      <v-container>
        <!-- This will render the current view based on the route -->
        <router-view />
      </v-container>
    </v-app>
  </div>
</template>

<script>
export default {
  name: 'App',
};
</script>

<style>
/* Add some global styles if needed */
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>