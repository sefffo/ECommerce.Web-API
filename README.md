# 🛒 E-Commerce Web API  
A modular, scalable, and production-ready **E-Commerce Backend** built with **ASP.NET Core**, following **Clean Architecture**, with **JWT Authentication**, **Google One-Tap OAuth**, **Redis caching**, **MVC Admin Dashboard**, and full **Postman** testing/documentation.
**Version:** 1.0.0 — First Stable Release
Soon there will be other new Features 
---

## 🚀 Features

### 🔐 Authentication & Security
- JWT authentication & authorization  
- Google One-Tap OAuth (one-way login)  
- Role-based access control (Admin/User)

### 📦 Core Business Modules
- Products & Categories  
- Shopping Cart  
- Orders & Payments  
- Delivery Methods  
- Reviews  
- User Management (Admin side)

### ⚙️ Technical Features
- Clean Architecture  
- EF Core + SQL Server  
- Redis in-memory caching  
- Global Exception Handling  
- AutoMapper for DTOs  
- Repository + Service layers  
- ILogger\<T\> for structured logging  
- Swagger + Postman documentation  
- ASP.NET MVC Admin Dashboard  

---

# 🧱 Architecture Overview

Technologies: .NET 9, C#, Entity Framework Core, JWT Authentication, AutoMapper, Stripe, Redis

Project Structure Overview:

Core Project (ECommerce.Core)

Abstraction: Interfaces for Services (IProductService, ICartService, IOrderService, etc.)

Domain: Base entities, exceptions, models (Products, Orders, Cart, Identity)

Contracts & UOW: Generic repositories, Unit of Work, specifications, data seeding

Service Project (Ecommerce.Service)

Business Services: Implementation of authentication, caching, cart, orders, payment, and product services

Mapping Profiles: AutoMapper profiles for DTOs

Specifications: Business rules and query specifications

Shared Project (Ecommerce.Shared)

DTOs: Data Transfer Objects for Products, Cart, Orders, Identity

Common: Enums, sorting, and utility classes

Error Models: Standardized error responses

Pagination & Specifications Enhancements

Web Project (ECommerce.Web)

Configuration: appsettings.json, Program.cs, JWT Auth setup

Custom Middlewares: Exception handling middleware

Bin/Obj: Build output and dependency libraries

Other Files

Postman Collection: ecommerce-postman.json.txt

Project Structure Document: structure.txt

Key Features

Fully layered architecture using Clean Architecture principles

Unit of Work and Specification Pattern applied for database operations

JWT Authentication and Authorization implemented

Redis caching for cart and frequently accessed data

Stripe Payment Integration

DTOs & AutoMapper for mapping between entities and API responses

Exception handling using custom middleware



