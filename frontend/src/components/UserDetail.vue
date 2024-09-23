<template>
  <v-container>
    <v-card v-if="user">
      <v-card-title>{{ user.firstName }} {{ user.lastName }}</v-card-title>
      <v-card-subtitle>{{ user.email }}</v-card-subtitle>
    </v-card>
  </v-container>
</template>

<script>
import apiClient from '../api/axios';

export default {
  data() {
    return {
      user: null,
    };
  },
  async created() {
    const email = this.$route.params.email;
    try {
      const response = await apiClient.get(`/users-management/users/${email}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      this.user = response.data;
    } catch (error) {
      alert('Failed to load user');
    }
  },
};
</script>