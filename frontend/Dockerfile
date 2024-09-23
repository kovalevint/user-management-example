# Frontend Dockerfile (for Vue.js)
FROM node:18 AS build
WORKDIR /app

# Copy package.json and install dependencies
COPY ["package.json", "package-lock.json*", "./"]
RUN npm install

# Copy the rest of the frontend source code
COPY . .

# Build the Vue.js project
RUN npm run build

# Serve the production build with Nginx
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]