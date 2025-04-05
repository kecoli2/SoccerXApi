## Docker Image Iceriği Tükçe
FROM postgres:latest

# Generate a your language pack between operating system languages. We use: tr_TR.UTF-8
RUN apt-get update; apt-get install -y --no-install-recommends locales; rm -rf /var/lib/apt/lists/*; \
	localedef -i tr_TR -c -f UTF-8 -A /usr/share/locale/locale.alias tr_TR.UTF-8

# If you want to change the default language of the container, you can also add the following code. [optional]
# ENV LANG tr_TR.utf8


###### CMD den Calistiracagim Dizin 
c:\Container>docker build --no-cache -t custom-postgresql -f container.dockerfile .

### Dockerdan Calistiracagim Alan
docker run -d --name postgresql -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=kecoli2 -e LANG=tr_TR.UTF-8 -e LC_ALL=tr_TR.UTF-8 -e TZ=Europe/Istanbul -p 5432:5432 custom-postgresql
