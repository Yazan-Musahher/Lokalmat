# Stage 1: Build the React application
FROM node:latest as build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Stage 2: Serve the app with Caddy
FROM caddy:2-alpine
COPY --from=build /app/build /usr/share/caddy
COPY Caddyfile /etc/caddy/Caddyfile

# Create the caddy user and group if it doesn't exist (fallback)
RUN addgroup -S caddy 2>/dev/null || true
RUN adduser -S caddy -G caddy 2>/dev/null || true

# Set ownership and permissions
RUN chown -R caddy:caddy /usr/share/caddy && \
    find /usr/share/caddy -type d -exec chmod 755 {} \; && \
    find /usr/share/caddy -type f -exec chmod 644 {} \;

EXPOSE 80
CMD ["caddy", "run", "--config", "/etc/caddy/Caddyfile", "--adapter", "caddyfile"]

