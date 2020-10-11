# Microservices.App
https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/ tanfolyam által készített alkalmazás

MongoDb telepítése a Projekthez
	NuGet Package Manager segítségével telepítsük fel a következő csomagokat:
		- MongoDB.Driver
		- MongoDB.Bson

MongoDb telepítése Dockerben:
	- docker pull mongo

MongoDb indítása Dockerben:
	- docker run -d -p 27017:27017 --name aspnetrun-mongo mongo

MongoDb elérése Dockeren belül:
	- docker exec -it aspnetrun-mongo /bin/bash
	- mongo
		- adatbázisok lekérése:
			show dbs
		- adatbázis létrehozása:
			use CatalogDb
		- Collection létrehozása a CatalogDb-ben:
			db.createCollection('Products')

Swagger telepítése a Projekthez
	NuGet Package Manager segítségével telepítsük fel a következő csomagot:
		- Swashbuckle.AspNetCore

Docker-compose futtatása (Generálása: jobb klikk a projekt-en, és menüből kiválasztani)
	docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d


	
