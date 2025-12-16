# 🛒 E-Commerce Web API  
A modular, scalable, and production-ready **E-Commerce Platform** built with **ASP.NET Core**, following **Clean Architecture**, with **JWT Authentication**, **Google OAuth**, **Redis caching**, **Admin Dashboard**, and comprehensive **API documentation**.

**Version:** 1.0.0 — First Stable Release  
🚀 More features coming soon!

---

## ✨ Key Highlights

### 🔐 **Dual Authentication System**
- **API:** JWT token-based authentication for mobile/web clients
- **Admin Dashboard:** Cookie-based authentication with Google OAuth integration
- **Google One-Tap OAuth:** Seamless admin login with Google accounts
- **Role-based Authorization:** Granular access control (Admin, SuperAdmin, User)

### 🎨 **Admin Dashboard (ASP.NET MVC)**
- **Modern UI:** Beautiful purple gradient theme with responsive design
- **Google OAuth:** One-click admin authentication
- **Full CRUD:** Products, Categories, Brands, Types, Users, Roles
- **User Management:** Assign/remove roles, manage permissions
- **Product Management:** Upload images, manage inventory, pricing
- **Authentication:** Email/Password + Google Sign-In

### 📦 **Core Business Features**
- **Products & Categories:** Full catalog management with images
- **Shopping Cart:** Redis-backed cart with real-time updates
- **Orders & Payments:** Complete order processing with Stripe
- **Delivery Methods:** Multiple shipping options
- **Reviews & Ratings:** Customer feedback system
- **User Management:** Profile management and order history

### ⚙️ **Technical Excellence**
- **Clean Architecture:** Domain-driven design with clear separation
- **EF Core + SQL Server:** Robust data persistence
- **Redis Caching:** High-performance in-memory caching
- **Global Exception Handling:** Centralized error management
- **AutoMapper:** Seamless DTO mapping
- **Repository + UOW Pattern:** Maintainable data access
- **Structured Logging:** ILogger\<T\> for debugging
- **API Documentation:** Swagger + Postman collections

---

## 🏗️ Architecture Overview

**Built with:** .NET 9, C# 13, Entity Framework Core, ASP.NET Core Identity, JWT, Google OAuth 2.0, AutoMapper, Stripe, Redis

### 📁 Project Structure

#### **🎯 ECommerce.Core** (Domain Layer)
├── Abstraction/ # Service interfaces
│ ├── IProductService
│ ├── ICartService
│ ├── IOrderService
│ ├── IAuthService
│ └── ICacheService
├── Domain/ # Domain entities
│ ├── Models/
│ │ ├── Products/
│ │ ├── Orders/
│ │ ├── Cart/
│ │ └── Identity/
│ ├── Exceptions/
│ └── BaseEntity
└── Contracts/ # Repository interfaces
├── IGenericRepository
├── IUnitOfWork
└── ISpecification


#### **⚙️ Ecommerce.Service** (Business Layer)
├── Services/ # Business logic implementation
│ ├── AuthService # JWT + Google OAuth
│ ├── ProductService
│ ├── CartService # Redis integration
│ ├── OrderService
│ ├── PaymentService # Stripe integration
│ └── CacheService
├── MappingProfiles/ # AutoMapper configurations
├── Specifications/ # Query specifications
└── Helpers/ # Utility classes


#### **📤 Ecommerce.Shared** (Shared Layer)
├── DTOs/ # Data Transfer Objects
│ ├── ProductDto
│ ├── CartDto
│ ├── OrderDto
│ └── IdentityDto
├── ErrorModels/ # Standardized error responses
├── Common/ # Enums, constants
└── Pagination/ # Pagination utilities


#### **🌐 ECommerce.Web** (Presentation Layer - API)
├── Controllers/ # API endpoints
├── Middlewares/ # Exception handling
├── Extensions/ # Service configurations
├── Program.cs # Startup & DI container
└── appsettings.json # Configuration

#### **🎨 AdminDashboard** (Presentation Layer - MVC)
├── Controllers/
│ ├── AdminController # Auth + Google OAuth
│ ├── ProductsController
│ ├── BrandsController
│ ├── TypesController
│ ├── UsersController
│ └── RolesController
├── Views/
│ ├── Admin/
│ │ └── Login.cshtml # Google OAuth UI
│ ├── Products/
│ ├── Users/
│ └── Shared/
└── wwwroot/
├── css/
│ └── auth.css # Beautiful UI styles
└── js/


---

## 🔑 Key Features Breakdown

### 🔐 **Authentication Architecture**

#### **API Authentication (JWT)**
- Token-based stateless authentication
- Refresh token support
- Role claims in JWT payload
- Secure password hashing (ASP.NET Core Identity)

#### **Admin Dashboard Authentication**
- **Email/Password:** Traditional login with remember me
- **Google OAuth:** One-click sign-in for admin accounts
- **Cookie-based sessions:** Persistent authentication
- **Role validation:** Only Admin/SuperAdmin can access
- **Account lockout:** Protection against brute-force

### 📊 **Design Patterns Applied**
- ✅ **Repository Pattern:** Data access abstraction
- ✅ **Unit of Work:** Transaction management
- ✅ **Specification Pattern:** Reusable query logic
- ✅ **Dependency Injection:** Loose coupling
- ✅ **Factory Pattern:** Object creation
- ✅ **Strategy Pattern:** Payment processing
- ✅ **Middleware Pattern:** Request pipeline

### 🎯 **Clean Architecture Layers**
┌─────────────────────────────────────┐
│ Presentation (API + MVC) │
│ - Controllers │
│ - Views (Admin Dashboard) │
├─────────────────────────────────────┤
│ Service Layer │
│ - Business Logic │
│ - Specifications │
├─────────────────────────────────────┤
│ Domain Layer │
│ - Entities │
│ - Interfaces │
├─────────────────────────────────────┤
│ Infrastructure (Future) │
│ - EF Core Implementation │
│ - External Services │
└─────────────────────────────────────┘

---

## 🚀 Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server
- Redis Server
- Google Cloud Console Account (for OAuth)
- Stripe Account (for payments)
