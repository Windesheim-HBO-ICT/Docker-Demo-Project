#student-start
FROM nginx

# copy app to nginx public folder
COPY /content /usr/share/nginx/html

# copy custom nginx config
COPY nginx.conf /etc/nginx/

# set working directory
WORKDIR /app

# add app
COPY . ./

# open port 4080 for trafic
EXPOSE 4080

# start app
CMD ["nginx", "-g", "daemon off;"]
#student-end