# Financial Management System (FMS)

## **Project Overview**

Financial Management System (FMS) is an MVC-based web application built with **ASP.NET Core**, **Entity Framework Core**. The system is designed for managing employees, financial accounts, journal entries, and generating reports.  

**Key Features:**

- **User Management**: Employees with authentication and role-based authorization.
- **Chart of Accounts Management**: Create, Read, Update, Delete (CRUD) operations for accounts.
- **Reporting**: Generate **PDF reports** for accounts and financial summaries.
- **Pagination & Filtering**: Efficient handling of large datasets on the UI with JavaScript.
- **JWT Authentication**: Secure endpoints with token-based authentication.
- **Database**: SQL Server 2022 with EF Core for database access and migrations.

---

## **Architecture Explanation**

**Architecture Pattern:** Clean Architecture + CQRS  

The project separates concerns across multiple layers:

### **1. MVC Layer (FMS.UI / Controllers)**
- Handles HTTP requests and serves Razor Views.  
- Coordinates with the **Application Layer** using CQRS commands/queries.  

### **2. Application Layer (FMS.Application)**
- Implements **CQRS commands** and **queries**.  
- Handles validations with **FluentValidation** and errors with **ErrorOr**.  
- Maps entities to DTOs using **AutoMapper**.  

### **3. Core Layer (FMS.Core)**
- Contains entities (`User`, `ChartOfAccount`, `JournalEntry`), DTOs, and repository/service interfaces.  

### **4. Infrastructure Layer (FMS.Infrastructure)**
- Implements **AppDbContext** and repositories for database access.  
- Handles external services if needed (e.g., caching, file storage).  

**Layer Interaction:**

       +-------------------+
       |      MVC Layer    |  <-- Controllers + Razor Views
       +--------+----------+
                |
                v
       +-------------------+
       |   Application     |  <-- Commands, Queries, Validation
       +--------+----------+
                |
                v
       +-------------------+
       |       Core        |  <-- Entities, DTOs, Interfaces
       +--------+----------+
                ^
                |
       +-------------------+
       |   Infrastructure  |  <-- Repositories, AppDbContext
       +-------------------+


