
# BookShopBS23

A book shop application where books are displayed along with their author. This application is a part of Software Engineer Trainee program orgranised by Brain Station 23. Dotnet(.NET), Razor pages, SQL server and MVC pattern are used in this application. 

## API Reference

#### Get all Books

```http
  GET /api/Book
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `none` | `string` | All **books** are fetched |

#### Get a book with details

```http
  GET /api/book/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of book to fetch |


#### Create a book

```http
  POST /api/book/
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `none`      | `string` | A book is created with **unique Id** |


#### Update a book

```http
  PUT /api/book/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of book to update |


#### Delete a book

```http
  DELETE /api/book/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of book to delete |

```http
  GET /api/Author
```

#### Get all the Authors

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `none` | `string` | All **authors** are fetched |

#### Get an author with details

```http
  GET /api/author/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of the author to fetch |


#### Create an author

```http
  POST /api/author/
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `none`      | `string` | An author is created with **unique Id** |


#### Update an Author

```http
  PUT /api/author/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of the author to update |


#### Delete an Author

```http
  DELETE /api/author/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of the author to delete. All the books are deleted written by the author. |

## Screenshots

**List of all books**
![App Screenshot](https://i.postimg.cc/nhny4qmj/Screenshot-2023-04-18-110416.png)

**Books Details**
![App Screenshot](https://i.postimg.cc/rFG6RfD2/Screenshot-2023-04-18-110459.png)

**List of all Authors**
![App Screenshot](https://i.postimg.cc/2STP23dH/Screenshot-2023-04-18-110620.png)

**Authors Details**
![App Screenshot](https://i.postimg.cc/xCNhfRw7/Screenshot-2023-04-18-110643.png)

**Author Deletion confirmation page**
![App Screenshot](https://i.postimg.cc/CKQ9ptQk/Screenshot-2023-04-18-110702.png)


**Updation of Book Details**
![App Screenshot](https://i.postimg.cc/ZYdz4bZR/Screenshot-2023-04-18-110534.png)

## Tech Stack

**Client:** Razor pages
**Server:** .Net, MVC pattern


## Run Locally

Clone the project

```bash
  https://github.com/rashed-bs/BookShopBS23.git
```

Go to the project directory

```bash
  cd BookShopBS23
```

Install dependencies

```bash
  Microsoft.EntityFrameworkCore(v-7.0.5)
  Microsoft.EntityFrameworkCOre.SqlServer(v-7.0.5)
  Microsoft.EntityFrameworkCore.Tools(v-7.0.5)
  Newtonsoft.json(v-13.0.1)
```

Database migration- Open package manager console

```
1. Add-migration <migration-name>
Example: Add-migration "Initial-migration"

2. Update-Database
```

Start the server

```bash
  dotnet run
```


## Acknowledgements

 - [Awesome Readme Templates](https://awesomeopensource.com/project/elangosundar/awesome-README-templates)
 - [Awesome README](https://github.com/matiassingers/awesome-readme)
## Authors

- [@mdrashed](https://github.com/)

