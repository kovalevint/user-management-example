<template>
  <v-container>
    <v-list>
      <v-list-item v-for="user in users" :key="user.email" @click="viewUser(user.email)">
        <v-list-item-content>
          <v-list-item-title>{{ user.firstName }} {{ user.lastName }}</v-list-item-title>
          <v-list-item-subtitle>{{ user.email }}</v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-list>
  </v-container>
</template>

<script>
import apiClient from '../api/axios';

export default {
  data() {
    return {
      users: [],
    };
  },
  async created() {
    try {
      const response = await apiClient.get('/users-management/users', {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      this.users = response.data;
    } catch (error) {
      alert('Failed to load users');
    }
  },
  methods: {
    viewUser(email) {
      this.$router.push(`/users/${email}`);
    },
  },
};
</script>