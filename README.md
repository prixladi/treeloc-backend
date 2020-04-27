# Treeloc Backend

Frontend aplikace se nachází zde [Treeloc Frontend](https://github.com/prixladi/treeloc-frontend).

# Spuštění projektu

## Docker (docker-compose)

Spuštění projektu pomocí `docker-compose up`

Vytvoření nových imagů a spuštění projektu pomocí `docker-compose up --build`

---

### Api 

Služba poběží na portu **4545**.

---
### Loader 

Služba poběží na portu **4546**.

---

### MongoDb

Kontejner s databází se spustí na portu **27017**.

---

Mapování portů pro všechny služby se dá změnit v souboru **./docker-compose.yml**.

[Dokumentace docker-compose.](https://docs.docker.com/compose/)

# Spuštění testů a zobrazení pokrytí kódu 

## Docker (docker-compose)

Pomocí `docker-compose -f ./tests/docker-compose.yml up --build`

UI s pokrytím kódu běží po doběhnutí testů na portu **80**.

Mapování portů pro všechny služby se dá změnit v souboru **./tests/docker-compose.yml**.

# Proměnné prostředí
Proměnné prostředí se dají změnit v souboru **./docker-compose.yml**.

## Api

|Název|Povinná|Popis|
|---|---|---|
|MONGO_URL|ano|Adresa Mongo databáze|
|MONGO_DATABASE_NAME|ano|Název Mongo databáze|

## Loader

|Název|Povinná|Popis|
|---|---|---|
|MONGO_URL|ano|Adresa Mongo databáze|
|MONGO_DATABASE_NAME|ano|Název Mongo databáze|
|LOADER_INTERVAL|ano|Interval stahovaní datasetů ve vteřinách|
|DISCOVERY_INTERVAL|ano|Interval volání DISCOVERY_URL ve vteřinách|
|DISCOVERY_URL|ano|Adresa která vrací adresy datasetů|