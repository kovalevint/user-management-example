// main.js
import { createApp } from 'vue';
import App from './App.vue';
import router from './router';

// Import Vuetify and its styles
import 'vuetify/styles';  // Required for Vuetify styles
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';  // Import Vuetify components
import * as directives from 'vuetify/directives';  // Import Vuetify directives

const vuetify = createVuetify({
    components,
    directives,
});

createApp(App)
    .use(router)
    .use(vuetify)   // Use Vuetify in the app
    .mount('#app');