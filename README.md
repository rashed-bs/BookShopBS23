<span style="font-size:30px;font-weight:bold;">Book Shop BS23</span>
This application is built using ASP.NET with C#. It has three main pages: Home, Books, and Authors. The user is automatically redirected to the Books page when they hit the Home page. The Books page displays a list of all books with options to create a new book and edit, delete, or view details for each book. Similarly, the Authors page displays a list of all authors with options to edit, delete, or view details for each author.

<span style="font-size:30px;font-weight:bold;">APIs</span>

<span style="font-size:30px;font-weight:bold;">Home Page</span>
  The Home page is the starting point of the application. It automatically redirects the user to the Books page.

<span style="font-size:30px;font-weight:bold;">Books Page</span>
  Get all books: `GET /api/book` - Retrieves a list of all books in the system.
  Create a new book: `POST /api/book` - Creates a new book in the system.
  Edit a book: PUT `/api/book/{id}` - Updates the book with the specified ID.
  Delete a book: DELETE `/api/book/{id}` - Deletes the book with the specified ID.
  View book details: `GET /api/book/{id}` - Retrieves details for the book with the specified ID.
  
<span style="font-size:30px;font-weight:bold;">Authors Page</span>
  Get all authors: `GET /api/authors` - Retrieves a list of all authors in the system.
  Edit an author: `PUT /api/authors/{id}` - Updates the author with the specified ID.
  Delete an author: `DELETE /api/authors/{id}` - Deletes the author with the specified ID.
  View author details: `GET /api/authors/{id}` - Retrieves details for the author with the specified ID.
