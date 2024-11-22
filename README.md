# Library Web Management System

## Final Project for Assal Technology Training

This project is a **Library Management System** built using **.NET Core**, **C#**, and **RESTful APIs**. The system focuses on efficiently managing library resources and user interactions. It showcases modern software development practices, including modular code design using **3-Tier architecture** and the **Repository pattern**.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [API Documentation](#api-documentation)
- [Authentication](#authentication)
- [Roles and Permissions](#roles-and-permissions)
- [Error Handling](#error-handling)

## Introduction

The Library Web Management System is designed to handle various functionalities required in a library, including account management, book borrowing, returns, and transactions. This system is structured around a 3-Tier architecture, separating concerns between the **UI**, **Business Logic**, and **Data Access** layers. The Repository pattern ensures that data access logic is abstracted away from the core business logic, allowing for better maintainability and testability.

The project also implements role-based access control (RBAC), ensuring different levels of access for **Accounters**, **Librarians**, and **Borrowers**.

## Features

- **Account Management:** User registration, login, and JWT token-based authentication.
- **Book Management:** Librarians can manage book records, add new books, and view existing books.
- **Book Borrowing and Returns:** Borrowers can request to borrow and return books. Librarians can approve or reject these requests.
- **Role-Based Access Control:** Different roles (Accounter, Librarian, Borrower) have access to specific features of the system.
- **Transaction Management:** Accounters can manage bills, while librarians approve borrowing and return transactions.
- **3-Tier Architecture:** The system uses a clear separation of concerns, with separate layers for UI, business logic, and data access.
- **Repository Pattern:** Abstracts data access logic and ensures the system is easily extensible and maintainable.

## Requirements

- **.NET Core 5.0** or later
- **ASP.NET Core Web API**
- **Entity Framework Core** (for database operations)
- **AutoMapper** (for DTO mappings)
- **JWT Authentication** (for secure user access)
- **CORS** (for cross-origin requests)
- **Microsoft.Extensions.DependencyInjection**

## Installation

Follow these steps to get the project up and running on your local machine.

### 1. Clone the repository:

```bash 
git clone https://github.com/yourusername/LibraryWebApi.git
```

###  2. Navigate into the project folder:

```bash
cd LibraryWebApi
```
### 3. Restore the dependencies:

```bash
dotnet restore
```
### 4. Build the project:

```bash
dotnet build
```
### 5. Run the project:

```bash
dotnet run
```
The API will be available at http://localhost:5000 (or the configured port).

---

## API Documentation

### Authentication

- **POST /api/auth/register**  
  Registers a new user and returns a JWT token.

- **POST /api/auth/login**  
  Authenticates a user with the provided credentials (username & password) and returns a JWT token.

### AccounterController (`/api/accounter`)

- **GET /api/accounter/GetAccounters**  
  Retrieves a list of all accounters.

- **GET /api/accounter/GetAccounter/{id}**  
  Retrieves an accounter by ID.

- **POST /api/accounter/AllowBill**  
  Allows a bill based on the provided bill ID.

### BorrowerController (`/api/borrower`)

- **GET /api/borrower/GetBorrowers**  
  Retrieves a list of all borrowers.

- **GET /api/borrower/GetBorrower/{id}**  
  Retrieves a borrower by ID.

- **POST /api/borrower/RequestToBorrow**  
  Requests to borrow a book based on the book ID.

- **POST /api/borrower/RequestToReturn**  
  Requests to return a borrowed book.

### LibrarianController (`/api/librarian`)

- **GET /api/librarian/GetBooks**  
  Retrieves all available books.

- **POST /api/librarian/AllowBorrow**  
  Allows a borrow transaction based on the transaction ID.

- **POST /api/librarian/AllowReturn**  
  Allows a return transaction based on the transaction ID.

- **POST /api/librarian/CreateBookWithAuthor**  
  Creates a new book along with its author.

---

## Authentication

The API uses **JWT (JSON Web Token)** for authentication. After registering or logging in, users receive a JWT token. This token must be included in the `Authorization` header for protected API endpoints.

Example:  
```http
Authorization: Bearer {your-token-here}
```
## Roles and Permissions

The system supports **Role-Based Access Control (RBAC)** with the following roles, each of which has specific access to different parts of the API.

### Roles

1. **Accounter**  
   - **Responsibilities**: Manages bills and handles payment transactions.  
   - **Access**: Can view and manage financial records and approve payments.

2. **Librarian**  
   - **Responsibilities**: Manages books and handles borrowing and return requests.  
   - **Access**: Can create, update, and delete book records, approve borrowing/return transactions, and view borrow/return history.

3. **Borrower**  
   - **Responsibilities**: Requests to borrow and return books.  
   - **Access**: Can view available books, request to borrow or return books, and check their borrowing history.

### Permissions

Each role is granted specific permissions to access different API controllers and actions.

- **AccounterPolicy**: Grants access to the `AccounterController`. Only users with the **Accounter** role can access endpoints related to bill management and financial transactions.
- **LibrarianPolicy**: Grants access to the `LibrarianController`. Only users with the **Librarian** role can access endpoints related to book management, and borrow/return transactions.
- **BorrowerPolicy**: Grants access to the `BorrowerController`. Only users with the **Borrower** role can access endpoints related to borrowing and returning books.

### Summary of Role-Based Permissions

| **Role**     | **Can Access**                                          |
|--------------|---------------------------------------------------------|
| **Accounter**| Bill management, transaction approval                   |
| **Librarian**| Book management, borrowing/return transaction approval  |
| **Borrower** | View available books, request borrow and return actions |

---

### Example: How Roles and Permissions Work

- An **Accounter** can access the `GET /api/accounter/GetAccounters` endpoint to manage bills but cannot interact with books or borrow/return requests.
- A **Librarian** can manage book records using endpoints like `POST /api/librarian/CreateBookWithAuthor` and approve borrow/return actions but does not have access to financial operations.
- A **Borrower** can request books to borrow or return but cannot manage books or approve transactions.

Each role has a specific set of responsibilities, ensuring that users can only perform actions relevant to their role in the system.


---

## Error Handling

The API returns standard HTTP status codes in responses to indicate the result of API requests. Below are the commonly used status codes:

- **200 OK**: The request was successful.  
- **400 Bad Request**: The request contained invalid data or missing parameters.  
- **401 Unauthorized**: The JWT token is missing or invalid.  
- **404 Not Found**: The requested resource does not exist.  
- **500 Internal Server Error**: An unexpected error occurred on the server.

### Example Error Response

```json
{
  "status": 400,
  "message": "Invalid request"
}
```

##  License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.



