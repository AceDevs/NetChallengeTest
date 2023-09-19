# NetChallengeTest
Project for .Net Api Challenge provided by SoliSystems


## Instrucciones para ejecutar

1. Realizar un pull de la master branch, de preferencia ejecutar en Visual Studio 2022 ya que este fue el IDE que se utilizo.
2. Tener instalado y funcionando [Docker](https://docker.com) `El proyecto ya esta configurado para ejecutarse con dockers directamente.`
3. Abrir el proyecto en VS2022
4. Seleccionar Docker-Compose como Startup Project
5. En la terminal ejecutar el comando `docker-compose up` para levantar el servidor de postgreSQL
6. Ir al directorio de la API `cd NetChallengeTestAPI`.
7. Y correr las migraciones en ef con el comando `dotnet ef database update`
8. En la terminal ejecutar el comando `docker-compose down` y posteriormente `docker-compose up` para reiniciar el servicio, o ejecutarlo directamente en el depurador.
9. Ya podremos utilizar el servicio!



## Endpoints disponibles
### Products
- GET => https://localhost/products?page=1&pageSize=60&sortBy=ProductName ASC
- GET => https://localhost/products/id/{id}
- GET => https://localhost/products/name/{productName}
- GET => https://localhost/products/productcode/{productCode}
- POST => https://localhost/products
- PUT => https://localhost/products
- DELETE => https://localhost/products

### Categories
- GET => https://localhost/categories?page=1&pageSize=60&sortBy=CategoryName DESC
- GET => https://localhost/categories/id/{id}
- GET => https://localhost/categories/name/{categoryName}
- POST => https://localhost/categories
- PUT => https://localhost/categories
- DELETE => https://localhost/categories

### Import Data
- GET => https://localhost/importData
  - Este endpoint acepta archivos .csv el cual intentara importar y cargar las categorias y productos a la base de datos.

