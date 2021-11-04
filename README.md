# air-table-proxy
Simple Log Proxy Web API which forwards log messages to AirTable API. It is implemented using .NET Core 5, with basic authentication. Unit tests are written using XUnit.

Two endpoints are implemented:
  * **GET /messages** - retrieves log messages from the AirTable API. It has one optional parameter called "maxCount", which specifies how many messages should be retrieved. If not specified, retrieves all the messages.
  * **POST /messages** - adds a new log message. The message has to have "title" and "text" values specified. Returns the inserted message with its metadata.

The app is packed in a Docker container. To run it, open a terminal, and run
  ```
  docker run -it --rm -p 8080:80 matusmrazik/airtableproxywebapi
  ```
To access the endpoints from a web browser, go to http://localhost:8080/swagger/index.html. In order to use the endpoints, you have to log in. There is only one user called **test** with password **test**. When accessing from terminal using curl, type:
  ```
  curl -X GET "http://localhost:8080/messages" -H "accept: application/json" -u "test:test"
  ```
  ```
  curl -X POST "http://localhost:8080/messages" -H "accept: application/json" -u "test:test" -H "Content-Type: application/json" -d "{\"title\":\"MESSAGE_TITLE\",\"text\":\"MESSAGE_TEXT\"}"
  ```
