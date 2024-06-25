FROM nginx

# copy app to nginx public folder
COPY /content /usr/share/nginx/html

# copy custom nginx config
COPY nginx.conf /etc/nginx/

# set working directory
WORKDIR /app

# add app
COPY . ./

# open port 3051 for trafic
EXPOSE 3051

# start app
CMD ["nginx", "-g", "daemon off;"]