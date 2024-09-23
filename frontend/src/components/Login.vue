<template>
  <v-container>
    <v-form @submit.prevent="login">
      <v-text-field label="Email" v-model="email" required></v-text-field>
      <v-text-field label="Password" type="password" v-model="password" required></v-text-field>
      <v-btn type="submit" color="primary">Login</v-btn>
    </v-form>

    <!-- Add the link to the registration page -->
    <div class="register-link">
      <span>Don't have an account?</span>
      <router-link to="/register">Register here</router-link>
    </div>
  </v-container>
</template>

<script>
import apiClient from '../api/axios';

export default {
  data() {
    return {
      email: '',
      password: '',
    };
  },
  methods: {
    async login() {
      try {
        const response = await apiClient.post('/login', {
          email: this.email,
          password: this.password,
        });

        localStorage.setItem('token', response.data.token);
        this.$router.push('/users'); // Redirect to the user list after login
      } catch (error) {
        alert('Invalid login credentials');
      }
    },
  },
};
</script>

<style>
/* Optional styles for the registration link */
.register-link {
  margin-top: 20px;
  text-align: center;
}

.register-link span {
  margin-right: 5px;
}
</style>