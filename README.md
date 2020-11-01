# Treeloc Backend

[Project repository](https://github.com/prixladi/treeloc/).

# Launching the project

## Docker (docker-compose)

Start the project using `docker-compose up`

Create new images and start the project using `docker-compose up --build`

---

### Api

Api service should be running on port **4545**.

---
### Loader

Loader service should be running on port **4546**.

---

### MongoDb

MongoDb container should be running on port **27017**.

---

Port mapping for all services can be changed in the **./docker-compose.yml** file.

[docker-compose documentation.](https://docs.docker.com/compose/)

# Run tests and code coverage

## Docker (docker-compose)

Using `docker-compose -f ./tests/docker-compose.yml up --build`

UI with code coverage should be be accessible on port **80**.

Port mapping for all services can be changed in the **./tests/docker-compose.yml** file.

# Environment variables
Environment variables can be changed in the **./docker-compose.yml** file.

## Api service

|Name|Optional|Description|
|---|---|---|
|MONGO_URL|no|MongoDB address|
|MONGO_DATABASE_NAME|no|MongoDB Database name|

## Loader service

|Name|Optional|Description|
|---|---|---|
|MONGO_URL|no|MongoDB address|
|MONGO_DATABASE_NAME|no|MongoDB Database name|
|LOADER_INTERVAL|no|Dataset download interval \[s\]|
|DISCOVERY_INTERVAL|no|DISCOVERY_URL call interval \[s\]|
|DISCOVERY_URL|no|Address of discovery endpoint|
|REMOVE_OLD|no|Flag true/false whether to clear all data after start|
