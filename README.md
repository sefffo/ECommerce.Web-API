🛒 E-Commerce Web API
A modular, scalable, and production-ready E-Commerce Platform built with ASP.NET Core, following Clean Architecture principles with JWT Authentication, Google OAuth, Redis caching, Admin Dashboard, and comprehensive API documentation.
Version: 1.0.0 — First Stable Release  
🚀 More features coming soon!
✨ Key Highlights
🔐 Dual Authentication System
•	API: JWT token-based authentication for mobile/web clients
•	Admin Dashboard: Cookie-based authentication with Google OAuth integration
•	Google One-Tap OAuth: Seamless admin login with Google accounts
•	Role-based Authorization: Granular access control (Admin, SuperAdmin, User)
🎨 Admin Dashboard (ASP.NET MVC)
•	Modern UI: Beautiful purple gradient theme with responsive design
•	Google OAuth: One-click admin authentication
•	Full CRUD Operations: Products, Categories, Brands, Types, Users, Roles
•	User Management: Assign/remove roles, manage permissions
•	Product Management: Upload images, manage inventory, pricing
•	Authentication Options: Email/Password + Google Sign-In
📦 Core Business Features
•	Products & Categories: Full catalog management with image support
•	Shopping Cart: Redis-backed cart with real-time updates
•	Orders & Payments: Complete order processing with Stripe integration
•	Delivery Methods: Multiple shipping options
•	Reviews & Ratings: Customer feedback system
•	User Management: Profile management and order history
⚙️ Technical Excellence
•	Clean Architecture: Domain-driven design with clear separation of concerns
•	EF Core + SQL Server: Robust data persistence layer
•	Redis Caching: High-performance in-memory caching
•	Global Exception Handling: Centralized error management
•	AutoMapper: Seamless DTO mapping
•	Repository + Unit of Work Pattern: Maintainable data access layer
•	Structured Logging: ILogger<T> for debugging and monitoring
•	API Documentation: Swagger UI + Postman collections
🏗️ Architecture Overview

Built with: .NET 9, C# 13, Entity Framework Core, ASP.NET Core Identity, JWT, Google OAuth 2.0, AutoMapper, Stripe, Redis

📦 E-Commerce Solution
│
├─ 🎯 ECommerce.Core (Domain Layer)
│  │
│  ├─ Abstraction/
│  │  ├─ IProductService
│  │  ├─ ICartService
│  │  ├─ IOrderService
│  │  ├─ IAuthService
│  │  └─ ICacheService
│  │
│  ├─ Domain/
│  │  ├─ Models/
│  │  │  ├─ Products/
│  │  │  ├─ Orders/
│  │  │  ├─ Cart/
│  │  │  └─ Identity/
│  │  ├─ Exceptions/
│  │  └─ BaseEntity
│  │
│  └─ Contracts/
│     ├─ IGenericRepository
│     ├─ IUnitOfWork
│     └─ ISpecification
│
├─ ⚙️ Ecommerce.Service (Business Layer)
│  │
│  ├─ Services/
│  │  ├─ AuthService (JWT + Google OAuth)
│  │  ├─ ProductService
│  │  ├─ CartService (Redis integration)
│  │  ├─ OrderService
│  │  ├─ PaymentService (Stripe integration)
│  │  └─ CacheService
│  │
│  ├─ MappingProfiles/ (AutoMapper configurations)
│  ├─ Specifications/ (Query specifications)
│  └─ Helpers/ (Utility classes)
│
├─ 📤 Ecommerce.Shared (Shared Layer)
│  │
│  ├─ DTOs/
│  │  ├─ ProductDto
│  │  ├─ CartDto
│  │  ├─ OrderDto
│  │  └─ IdentityDto
│  │
│  ├─ ErrorModels/ (Standardized error responses)
│  ├─ Common/ (Enums, constants)
│  └─ Pagination/ (Pagination utilities)
│
├─ 🌐 ECommerce.Web (Presentation Layer - API)
│  │
│  ├─ Controllers/ (API endpoints)
│  ├─ Middlewares/ (Exception handling)
│  ├─ Extensions/ (Service configurations)
│  ├─ Program.cs (Startup & DI container)
│  └─ appsettings.json (Configuration)
│
└─ 🎨 AdminDashboard (Presentation Layer - MVC)
   │
   ├─ Controllers/
   │  ├─ AdminController (Auth + Google OAuth)
   │  ├─ ProductsController
   │  ├─ BrandsController
   │  ├─ TypesController
   │  ├─ UsersController
   │  └─ RolesController
   │
   ├─ Views/
   │  ├─ Admin/
   │  │  └─ Login.cshtml (Google OAuth UI)
   │  ├─ Products/
   │  ├─ Users/
   │  └─ Shared/
   │
   └─ wwwroot/
      ├─ css/
      │  └─ auth.css (Beautiful UI styles)
      └─ js/

Layer Responsibilities

┌──────────────────────────────────────────────┐
│         Presentation Layer                   │
│  ┌────────────────┐  ┌──────────────────┐   │
│  │  Web API       │  │  Admin Dashboard │   │
│  │  (REST API)    │  │  (MVC + OAuth)   │   │
│  └────────────────┘  └──────────────────┘   │
└──────────────────────────────────────────────┘
                    ↓
┌──────────────────────────────────────────────┐
│           Service Layer                      │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐   │
│  │  Auth    │  │ Product  │  │  Order   │   │
│  │ Service  │  │ Service  │  │ Service  │   │
│  └──────────┘  └──────────┘  └──────────┘   │
└──────────────────────────────────────────────┘
                    ↓
┌──────────────────────────────────────────────┐
│           Domain Layer                       │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐   │
│  │ Entities │  │Interface │  │  Domain  │   │
│  │  (Core)  │  │  (IRepo) │  │  Logic   │   │
│  └──────────┘  └──────────┘  └──────────┘   │
└──────────────────────────────────────────────┘
                    ↓
┌──────────────────────────────────────────────┐
│      Data Access (EF Core + Redis)           │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐   │
│  │   SQL    │  │  Redis   │  │  Stripe  │   │
│  │  Server  │  │  Cache   │  │   API    │   │
│  └──────────┘  └──────────┘  └──────────┘   │
└──────────────────────────────────────────────┘

🔑 Authentication Architecture
API Authentication (JWT)
•	Token-based stateless authentication
•	Refresh token support
•	Role claims embedded in JWT payload
•	Secure password hashing via ASP.NET Core Identity
•	Token expiration and validation
Admin Dashboard Authentication
•	Email/Password Login: Traditional authentication with “Remember Me” functionality
•	Google OAuth: One-click sign-in for admin accounts
•	Cookie-based Sessions: Persistent authentication state
•	Role Validation: Restricted access to Admin/SuperAdmin roles only
•	Account Lockout: Built-in protection against brute-force attacks


📊 Design Patterns
This project implements several proven design patterns:
•	Repository Pattern: Abstracts data access logic from business logic
•	Unit of Work Pattern: Manages transactions across multiple repositories
•	Specification Pattern: Encapsulates reusable query logic
•	Dependency Injection: Promotes loose coupling and testability
•	Factory Pattern: Handles complex object creation
•	Strategy Pattern: Flexible payment processing implementations
•	Middleware Pattern: Custom request pipeline processing

🚀 Getting Started
Prerequisites
•	.NET 9 SDK
•	SQL Server (LocalDB or Express)
•	Redis Server
•	Google Cloud Console Account (for OAuth configuration)
•	Stripe Account (for payment processing)
