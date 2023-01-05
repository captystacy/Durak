#STAGE 1
FROM node:19.3-alpine AS build
WORKDIR /usr/src/app
COPY Durak.AngularUI/package.json Durak.AngularUI/package-lock.json ./
RUN npm install
COPY Durak.AngularUI/ ./
RUN npm run build

#STAGE 2
FROM nginx:1.23.3-alpine
COPY Durak.AngularUI/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/durak.angular-ui /usr/share/nginx/html
