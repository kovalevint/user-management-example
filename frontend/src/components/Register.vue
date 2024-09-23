<template>
  <v-container>
    <v-form @submit.prevent="register">
      <v-text-field label="First Name" v-model="firstName" required></v-text-field>
      <v-text-field label="Last Name" v-model="lastName" required></v-text-field>
      <v-text-field label="Email" v-model="email" required></v-text-field>
      <v-text-field label="Password" type="password" v-model="password" required></v-text-field>
      <v-btn type="submit" color="primary">Register</v-btn>
    </v-form>
  </v-container>
</template>

<script>
import apiClient from '../api/axios';

export default {
  data() {
    return {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
    };
  },
  methods: {
    async register() {
      try {
        await apiClient.post('/users-management/users', {
          firstName: this.firstName,
          lastName: this.lastName,
          email: this.email,
          password: this.password,
        });
        this.$router.push('/login');
      } catch (error) {
        alert('Failed to register user');
      }
    },
  },
};
</script>