## User Management Service

This project provides a user management service with a frontend built using Vue.js and a backend powered by ASP.NET Core Minimal API. The backend integrates with Keycloak as the central identity provider, handling all user authentication and authorization needs. Keycloak acts as the main source for storing and managing user data.

The service is containerized using Docker, making it easy to deploy both locally and in cloud environments such as Azure. The project is organized as a mono-repo with both the frontend and backend services combined in one repository, allowing for seamless development and deployment.

# General Architecture

## Frontend (Vue.js)

The Vue.js application serves as the user interface, allowing users to interact with the platform. It communicates with the backend API to perform various actions related to user management, such as user registration, login, and profile management.

Keycloak is integrated on the frontend via the Keycloak JavaScript Adapter, which handles user authentication. The adapter ensures that users are authenticated before accessing protected routes in the application. It communicates with Keycloak to fetch authentication tokens, which are then passed to the backend.

## Backend (ASP.NET Core Minimal API)

The backend is built using ASP.NET Core Minimal API (.NET 8) and is responsible for providing RESTful services to the frontend. It communicates with Keycloak using JWT Bearer tokens to validate the user’s identity and manage user-related data. The backend does not store user data directly but relies on Keycloak for user authentication and authorization.

The backend exposes various API endpoints for:

	•	User management (e.g., creating, updating user profiles)
	•	Secure data retrieval that requires authentication
	•	Integration with other services as needed for user-related operations

## Keycloak

Keycloak is the central identity provider and handles all aspects of user authentication and authorization. It stores user information, manages roles, and issues JWT tokens that are used by both the frontend and backend to verify users. Keycloak also supports Single Sign-On (SSO), making it easy to scale user authentication across multiple services.

## Communication between Frontend and Backend

	•	The Vue.js frontend interacts with the ASP.NET Core Minimal API backend through RESTful API calls.
	•	Upon successful authentication in Keycloak, the frontend retrieves a JWT token, which is included in the HTTP headers (Authorization: Bearer token) for each request sent to the backend.
	•	The backend verifies the JWT token by communicating with Keycloak and ensures that the request is coming from an authenticated user.

## Backend and Keycloak Interaction

	•	The backend uses JWT Bearer Authentication to validate each incoming request by contacting Keycloak’s authentication server.
	•	Keycloak is responsible for user authentication, managing roles, and issuing tokens.
	•	All user-related actions (like role assignments and user creation) are managed through Keycloak.

## Dockerized Setup

The project uses Docker to containerize both the frontend and backend services for easy local development and deployment to cloud platforms like Azure.

## Docker Setup

	•	Frontend: The Vue.js application is containerized using an NGINX server for production builds.
	•	Backend: The ASP.NET Core Minimal API is containerized using the official .NET 8 runtime.
	•	Docker Compose: A docker-compose.yml file is included at the root of the project to orchestrate both services, ensuring they can be easily spun up together with one command.

## Local Development

To run the project locally using Docker, run the following command from the root of the mono-repo:

`docker-compose up --build`

## Deployment to Azure

The project is designed to be easily deployed to Azure. The frontend can be deployed as a Static Web App, while the backend can be deployed as an Azure App Service running a containerized ASP.NET Core API.

This description provides a comprehensive overview of the architecture, communication flow, and deployment setup. You can modify it as necessary for your project!