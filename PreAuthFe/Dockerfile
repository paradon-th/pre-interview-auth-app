# Stage 1: Build the application
FROM node:20-alpine AS build
WORKDIR /usr/src/app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Stage 2: Run the application
FROM node:20-alpine
WORKDIR /usr/src/app
# Copy built app from the build stage
COPY --from=build /usr/src/app/dist/pre-auth-fe ./dist/pre-auth-fe
# Expose the port the app runs on
EXPOSE 4000
# Start the node server
CMD ["node", "dist/pre-auth-fe/server/server.mjs"]
