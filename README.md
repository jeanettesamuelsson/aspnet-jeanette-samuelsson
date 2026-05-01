# 🏋️‍♂️ Core Fitness
**CoreFitness** is a web application and a gym administration system designed to streamline the management of training sessions, bookings, and memberships. This project was developed as a school project for an **ASP.NET** course

---

## Developed using these technologies:

* **C# .NET 10** (ASP.NET Core MVC)
* **Entity Framework Core (EF Core):** Utilized as the primary ORM for data mapping and managing relationships.
* **Databases:**
  * **SQLite:** Used for local development.
  * **Microsoft SQL Server:** Supported for production environments to ensure scalability.
* **ASP.NET Core Identity:** Handles authentication and role-based authorization (Admin/Member).

---

## 🏁 Getting Started

Follow these steps to set up the project on your local machine.

### 1. Database Setup

1. By default, the project is configured to use **SQLite** for development, requiring no local server installation.
2. If you wish to use **SQL Server**, update the `ConnectionString` in `appsettings.json` and ensure the database provider is set to SQL Server in `Program.cs`.
3. Open a terminal in the project root and run the following command to create the database schema:

---

### 2. Seeding & Admin Access

Upon the first launch, an automatic **Data Initializer** will seed the database with:

* **Admin Account:** `admin@corefitness.se` (Password: `ChangeMe123!`)
* **Initial Data:** Membership types and a few scheduled gym classes for demonstration.

### 3. Start the Application

1. Open the solution in Visual Studio and press **F5**, or run the following command in the project folder: "dotnet run"
2. Once the server is running, your browser will open automatically. If it doesn't, navigate manually to the URL provided in the terminal (usually `https://localhost:XXXX`).
3. Log in with the Admin credentials to access the management dashboard.

---

## 🚀 Future Improvements 

While the core architecture and basic administration and user profile are in place, the following features are planned for future development:

* **Admin Management Suite:** Fully implement the UI and backend logic to create, edit, and delete gym classes directly from the dashboard.
* **Booking Management:** Enable administrators to manually manage member reservations (add/remove participants).
* **Improved User Profiles:** Allow members to view and book gym classes.
