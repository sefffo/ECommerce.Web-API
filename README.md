<div align="center">

# 🛍️ E-Commerce Web API

### Enterprise-Grade E-Commerce Platform Built with ASP.NET Core 9.0

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13.0-239120?style=flat-square&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)](LICENSE)
[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-blue?style=flat-square)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)

**[Documentation](#-api-documentation) • [Installation](#-quick-start) • [Features](#-features)**

---

A production-ready, scalable e-commerce API demonstrating **Clean Architecture**, **SOLID principles**, and modern .NET development practices. Features dual authentication systems, comprehensive admin dashboard, payment processing, and high-performance caching.

</div>

---

## 📋 Table of Contents

- [✨ Features](#-features)
- [🏗️ Architecture](#️-architecture)
- [🚀 Quick Start](#-quick-start)
- [📚 API Documentation](#-api-documentation)
- [🔐 Authentication](#-authentication)
- [🛠️ Tech Stack](#️-tech-stack)
- [📊 Design Patterns](#-design-patterns)
- [🤝 Contributing](#-contributing)

---

## ✨ Features

<table>
<tr>
<td width="50%">

### 🔐 Advanced Authentication
- **JWT Token Authentication** for API clients
- **Google OAuth 2.0** with One-Tap sign-in
- **Cookie-based sessions** for admin panel
- **Role-based authorization** (Admin, User, SuperAdmin)
- **ASP.NET Core Identity** integration
- Account lockout & password hashing

### 📦 E-Commerce Core
- **Product Catalog** with categories & brands
- **Shopping Cart** with Redis caching
- **Order Management** system
- **Stripe Payment** integration
- **Delivery Methods** management
- **Product Reviews** & ratings

</td>
<td width="50%">

### 🎨 Admin Dashboard
- **Modern MVC interface** with responsive design
- **Google OAuth login** for administrators
- **Complete CRUD operations** for all entities
- **User & Role management**
- **Product management** with image upload
- **Real-time data** synchronization

### ⚡ Technical Excellence
- **Clean Architecture** implementation
- **Repository & Unit of Work** patterns
- **Specification Pattern** for queries
- **Redis Caching** for performance
- **Global exception handling**
- **API tested using Postman collections**

</td>
</tr>
</table>

---

## 🏗️ Architecture

This project implements **Clean Architecture** with clear separation of concerns across multiple layers:

```text
┌─────────────────────────────────────────────────────────────┐
│                     Presentation Layer                       │
│  ┌──────────────────────┐    ┌──────────────────────┐      │
│  │   ECommerce.Web      │    │   AdminDashboard     │      │
│  │   (REST API)         │    │   (MVC + OAuth)      │      │
│  └──────────────────────┘    └──────────────────────┘      │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                      Business Layer                          │
│  ┌─────────────────────────────────────────────────────┐   │
│  │           Ecommerce.Service                          │   │
│  │  • AuthService     • ProductService                  │   │
│  │  • CartService     • OrderService                    │   │
│  │  • PaymentService  • CacheService                    │   │
│  └─────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                       Domain Layer                           │
│  ┌─────────────────────────────────────────────────────┐   │
│  │           ECommerce.Core                             │   │
│  │  • Domain Models    • Repository Interfaces          │   │
│  │  • Business Logic   • Service Abstractions           │   │
│  │  • Specifications   • Domain Exceptions              │   │
│  └─────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                  Infrastructure Layer                        │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐        │
│  │ SQL Server  │  │    Redis    │  │   Stripe    │        │
│  │  (EF Core)  │  │   (Cache)   │  │  (Payment)  │        │
│  └─────────────┘  └─────────────┘  └─────────────┘        │
└─────────────────────────────────────────────────────────────┘
```

### 📁 Project Structure

```text
ECommerce.Web-API/
│
├── ECommerce.Core/              # Domain layer
│   ├── Domain/
│   │   ├── Entities/            # Business entities
│   │   ├── Exceptions/          # Domain exceptions
│   │   └── BaseEntity.cs
│   ├── Abstraction/             # Service interfaces
│   └── Contracts/               # Repository interfaces
│
├── Ecommerce.Service/           # Business logic layer
│   ├── Services/                # Service implementations
│   ├── MappingProfiles/         # AutoMapper profiles
│   ├── Specifications/          # Query specifications
│   └── Helpers/                 # Utility classes
│
├── Ecommerce.Shared/            # Shared layer
│   ├── DTOs/                    # Data transfer objects
│   ├── ErrorModels/             # Error responses
│   ├── Common/                  # Shared constants & enums
│   └── Pagination/              # Pagination utilities
│
├── ECommerce.Web/               # API layer
│   ├── Controllers/             # API endpoints
│   ├── Middlewares/             # Custom middleware
│   ├── Extensions/              # Service extensions
│   └── Program.cs               # Application entry point
│
└── AdminDashboard/              # Admin UI layer
    ├── Controllers/             # MVC controllers
    ├── Views/                   # Razor views
    └── wwwroot/                 # Static files
```

---

## 🚀 Quick Start

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB/Express/Full)
- [Redis](https://redis.io/download) (Windows: [Redis on Windows](https://github.com/microsoftarchive/redis/releases))
- [Stripe Account](https://stripe.com) (for payments)
- [Google Cloud Console](https://console.cloud.google.com/) (for OAuth)
- [Postman](https://www.postman.com/downloads/) (for testing the API)

### Installation

1️⃣ **Clone the repository**
```bash
git clone https://github.com/sefffo/ECommerce.Web-API.git
cd ECommerce.Web-API
```

2️⃣ **Configure application settings**

Update `appsettings.json` in **ECommerce.Web**:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ECommerceDB;Trusted_Connection=true;"
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  },
  "Stripe": {
    "PublishableKey": "pk_test_your_key",
    "SecretKey": "sk_test_your_key",
    "WebhookSecret": "whsec_your_secret"
  },
  "JwtSettings": {
    "Key": "YourSuperSecretKeyHere_MinimumLengthRequired",
    "Issuer": "ECommerceAPI",
    "Audience": "ECommerceClients",
    "ExpiryInMinutes": 60
  }
}
```

Update `appsettings.json` in **AdminDashboard**:
```json
{
  "Authentication": {
    "Google": {
      "ClientId": "your-client-id.apps.googleusercontent.com",
      "ClientSecret": "your-client-secret"
    }
  }
}
```

3️⃣ **Setup database**
```bash
cd ECommerce.Web
dotnet ef database update
```

4️⃣ **Start Redis server**
```bash
redis-server
```

5️⃣ **Run the applications**
```bash
# Terminal 1 - API
cd ECommerce.Web
dotnet run

# Terminal 2 - Admin Dashboard
cd AdminDashboard
dotnet run
```

6️⃣ **Test the API using Postman**
- Import the Postman collection (JSON file) for this project
- Configure the base URL (e.g. `https://localhost:5001`)
- Use the provided routes and example bodies below

7️⃣ **Access the Admin Dashboard**
- URL: `https://localhost:7001`

---

## 📚 API Documentation

> All endpoints are tested and documented using **Postman collections**. Below are some key examples.

### 🔑 Authentication

#### Register User
```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "displayName": "John Doe",
  "password": "SecurePass123!",
  "phoneNumber": "+1234567890"
}
```

**Response:**
```json
{
  "email": "user@example.com",
  "displayName": "John Doe",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "SecurePass123!"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "user@example.com",
  "displayName": "John Doe",
  "expiresAt": "2025-12-18T15:00:00Z"
}
```

---

### 🛍️ Products

#### Get All Products (Paginated)
```http
GET /api/products?pageIndex=1&pageSize=10&sort=name
Authorization: Bearer {token}
```

**Response:**
```json
{
  "pageIndex": 1,
  "pageSize": 10,
  "count": 150,
  "data": [
    {
      "id": 1,
      "name": "Running Shoes",
      "description": "High-performance running shoes",
      "price": 89.99,
      "pictureUrl": "/images/products/shoe1.jpg",
      "productBrand": "Nike",
      "productType": "Footwear"
    }
  ]
}
```

#### Get Product by ID
```http
GET /api/products/1
Authorization: Bearer {token}
```

#### Create Product (Admin)
```http
POST /api/products
Authorization: Bearer {admin-token}
Content-Type: multipart/form-data

{
  "name": "New Product",
  "description": "Product description",
  "price": 49.99,
  "productBrandId": 1,
  "productTypeId": 1,
  "file": [image file]
}
```

#### Update Product (Admin)
```http
PUT /api/products/1
Authorization: Bearer {admin-token}
Content-Type: application/json

{
  "name": "Updated Product Name",
  "price": 59.99
}
```

#### Delete Product (Admin)
```http
DELETE /api/products/1
Authorization: Bearer {admin-token}
```

---

### 🛒 Shopping Cart

#### Get Cart
```http
GET /api/cart/{cartId}
```

#### Update Cart
```http
POST /api/cart
Content-Type: application/json

{
  "id": "cart-abc123",
  "items": [
    {
      "id": 1,
      "productName": "Running Shoes",
      "price": 89.99,
      "quantity": 2,
      "pictureUrl": "/images/products/shoe1.jpg",
      "brand": "Nike",
      "type": "Footwear"
    }
  ]
}
```

#### Delete Cart
```http
DELETE /api/cart/{cartId}
```

---

### 📦 Orders

#### Create Order
```http
POST /api/orders
Authorization: Bearer {token}
Content-Type: application/json

{
  "cartId": "cart-abc123",
  "deliveryMethodId": 1,
  "shippingAddress": {
    "firstName": "John",
    "lastName": "Doe",
    "street": "123 Main Street",
    "city": "New York",
    "state": "NY",
    "zipCode": "10001"
  }
}
```

#### Get User Orders
```http
GET /api/orders
Authorization: Bearer {token}
```

#### Get Order by ID
```http
GET /api/orders/{orderId}
Authorization: Bearer {token}
```

---

### 💳 Payments

#### Create Payment Intent
```http
POST /api/payments/{cartId}
Authorization: Bearer {token}
```

**Response:**
```json
{
  "clientSecret": "pi_xxx_secret_yyy",
  "paymentIntentId": "pi_xxx"
}
```

---

## 🔐 Authentication

### JWT Authentication (API)
- Token-based stateless authentication
- Access tokens with configurable expiration
- Role claims embedded in JWT payload
- Secure token validation

### Google OAuth 2.0 (Admin Dashboard)
- One-Tap sign-in integration
- Automatic user registration
- Profile information retrieval
- Secure cookie-based sessions

### Role-Based Authorization
```csharp
[Authorize(Roles = "Admin")]
public async Task<ActionResult> AdminOnlyEndpoint()
{
    // Admin-only logic
}

[Authorize(Roles = "Admin,SuperAdmin")]
public async Task<ActionResult> PrivilegedEndpoint()
{
    // Logic for multiple roles
}
```

---

## 🛠️ Tech Stack

### Backend Framework
- **ASP.NET Core 9.0** - Modern web framework
- **Entity Framework Core** - ORM for database access
- **ASP.NET Core Identity** - User authentication & management

### Database & Caching
- **SQL Server** - Primary relational database
- **Redis** - In-memory cache for shopping carts

### Authentication & Security
- **JWT Bearer Tokens** - API authentication
- **Google OAuth 2.0** - Social login
- **PBKDF2** - Password hashing algorithm

### Payment Processing
- **Stripe API** - Payment gateway integration

### Development Tools
- **AutoMapper** - Object-to-object mapping
- **Postman** - API testing & documentation
- **ILogger** - Structured logging

### Design Patterns & Principles
- **Clean Architecture** - Layered architecture pattern
- **Repository Pattern** - Data access abstraction
- **Unit of Work** - Transaction management
- **Specification Pattern** - Query encapsulation
- **Dependency Injection** - Loose coupling
- **SOLID Principles** - Object-oriented design

---

## 📊 Design Patterns

| Pattern | Purpose | Location |
|---------|---------|----------|
| **Repository** | Abstract data access logic | `ECommerce.Core/Contracts` |
| **Unit of Work** | Manage transactions across repositories | `ECommerce.Core/Contracts` |
| **Specification** | Encapsulate query logic | `Ecommerce.Service/Specifications` |
| **Dependency Injection** | Promote loose coupling | Throughout application |
| **Factory** | Complex object creation | Service layer |
| **Strategy** | Payment processing flexibility | `PaymentService` |
| **Middleware** | Request pipeline processing | `ECommerce.Web/Middlewares` |

---

## 🎯 Key Highlights

### 🏆 Clean Architecture Benefits
- **Testability**: Easy unit and integration testing
- **Maintainability**: Clear separation of concerns
- **Scalability**: Independent layer scaling
- **Flexibility**: Easy technology swapping

### ⚡ Performance Optimizations
- **Redis caching** for shopping cart operations
- **Async/await** throughout for non-blocking I/O
- **Database indexing** on frequently queried columns
- **Pagination** to handle large datasets efficiently

### 🔒 Security Features
- JWT token authentication with secure key storage
- Password hashing using ASP.NET Core Identity
- SQL injection protection via EF Core
- CORS configuration for API security
- HTTPS enforcement in production
- Rate limiting on authentication endpoints
- Account lockout after failed attempts

### 📈 Scalability Features
- Stateless API design for horizontal scaling
- Redis for distributed caching
- Repository pattern for data layer flexibility
- Modular architecture for microservices migration

---

## 🎨 Admin Dashboard Screenshots

### Login Page with Google OAuth
```text
┌─────────────────────────────────────────┐
│  E-Commerce Admin Panel                 │
│                                          │
│  ┌────────────────────────────────┐    │
│  │  Email: ___________________    │    │
│  │  Password: _______________     │    │
│  │  [  Sign In  ]                 │    │
│  │                                 │    │
│  │  ────────── OR ──────────      │    │
│  │                                 │    │
│  │  [🔵 Sign in with Google]     │    │
│  └────────────────────────────────┘    │
└─────────────────────────────────────────┘
```

### Product Management Dashboard
- Create, edit, and delete products
- Upload product images
- Manage categories and brands
- Real-time inventory updates

---

## 📖 Usage Examples

### Using the API with JavaScript (Fetch)

```javascript
// Register a new user
const register = async () => {
  const response = await fetch('https://localhost:5001/api/auth/register', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      email: 'user@example.com',
      displa