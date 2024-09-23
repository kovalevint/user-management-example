import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '../views/LoginPage.vue';
import RegisterPage from '../views/RegisterPage.vue';
import UserDetailPage from '../views/UserDetailPage.vue';
import UserListPage from '../views/UserListPage.vue';

const routes = [
  { path: '/login', component: LoginPage },
  { path: '/register', component: RegisterPage },
  { path: '/users/:email', component: UserDetailPage },
  { path: '/users', component: UserListPage },
  { path: '/', redirect: '/login' }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;