# Treeloc Backend

Frontend aplikace se nachází zde [Treeloc Backend](https://github.com/prixladi/treeloc-backend).

# Spuštění projektu

## Docker (docker-compose)

Spuštění projektu pomocí `docker-compose up`

Vytvoření nových imagů a spuštění projektu pomocí `docker-compose up --build`

---

### Api service

Služba poběží na portu *4545*.

---
### Loader service

Služba poběží na portu *4546*.

---

### Mongo

Kontejner s databází se spustí na portu *27017*.

---

Mapování portů pro všechny služby se dá změnit v souboru **/docker-compose.yml**.

[Dokumentace docker-compose.](https://docs.docker.com/compose/)

---

## Yarn

Inicializace projektu  `yarn`

Spuštění projektu `yarn start`

# Proměnné prostředí
Proměnné prostředí se dají změnit v souboru **/docker-compose.yml**.

## Api service

|Název|Povinná|Popis|
|---|---|---|
|MONGO_URL|ano|Adresa Mongo databáze|
|MONGO_DATABASE_NAME|ano|Název Mongo databáze|

## Loader service

|Název|Povinná|Popis|
|---|---|---|
|MONGO_URL|ano|Adresa Mongo databáze|
|MONGO_DATABASE_NAME|ano|Název Mongo databáze|
|LOADER_INTERVAL|ano|Interval stahovaní datasetů ve vteřinách|
|DISCOVERY_INTERVAL|ano|Interval volání DISCOVERY_URL ve vteřinách|
|DISCOVERY_URL|ano|Adresa která vrací adresy datasetů|