# Company Portal API

This is a backend API for a company management portal built using **.NET Core Web API**. It includes registration, authentication, profile management, and logo uploads for companies.

## 🔧 Technologies Used

- **.NET Core Web API**
- **Entity Framework Core**
- **JWT Authentication**
- **FluentValidation** – for robust server-side validation
- **Repository Pattern** – for data access abstraction
- **Unit of Work** – for transactional consistency
- **DTOs (Data Transfer Objects)** – for clean API communication
- **Result Pattern** – for clear and consistent service responses
- **AutoMapper** – for mapping between entities and DTOs
- **File Service** – for managing image/logo uploads

---

## 📁 Project Structure

- `Controllers/` – API endpoints
- `Services/` – Business logic (clean services)
- `Repositories/` – Encapsulates EF Core operations
- `DTOs/` – Request/Response shaping
- `Shared/Result.cs` – Implements the Result pattern
- `Services/FileService.cs` – Handles saving, deleting, and accessing image/logo files

---

## 🔐 Authentication

- JWT tokens are issued on login and are required for protected endpoints.
- Tokens include the user role and email for secure access control.

---

## 🏢 Company Registration Flow

1. A company can register by submitting its:
   - Arabic & English names
   - Email, Phone number, Website (optional)
   - Logo file (optional)

2. The system:
   - Validates input with **FluentValidation**
   - Stores the data using clean services and repositories
   - Uploads the logo using the FileService
   - Returns a success message and redirects to OTP verification

---

## 📦 Features

- Company signup and login
- Profile retrieval and update
- JWT-based authentication
- Image upload and preview
- Arabic and English name handling
- Secure, scalable architecture

---

## ✅ Getting Started

1. Clone the repository.
2. Configure your PostgreSQL connection string in `appsettings.json`:
3. Run migrations and update the database.
4. Start the API using Visual Studio or `dotnet run`.

---

## 🛡️ Notes

- Ensure to secure sensitive endpoints using `[Authorize]`.
- Consider using Swagger for API testing.

---

## ✉️ Contributions

Feel free to fork this repo and submit a PR for any improvements or fixes.

---

## 📄 License

This project is open-source and available under the [MIT License](LICENSE).

