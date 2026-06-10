# 🏫 School_66 — School Forms System

**School_66** is a web application built with **ASP.NET Core 9** for backend and **HTML + Bootstrap** for frontend.  
The application allows users to fill out and manage forms with questions related to the school.

---

## 🚀 Features

- 📝 Fill out various forms for students, teachers, or classes  
- 📂 Store and manage form submissions  
- 🖥 Frontend pages built with static HTML + Bootstrap  
- Easy navigation and responsive design  

---

## 🛠 Technology Stack

### Backend
- ASP.NET Core 9  
- Entity Framework Core + SQLite  
- C#

### Frontend
- HTML + Bootstrap  
- JavaScript (optional for interactivity)  

---

## 📦 Installation

1. Clone or download the repository  
2. Open the project in **Rider** or **Visual Studio**  
3. Run database migrations (if needed)  
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```  
4. Run the application  
    ```bash
    dotnet run
    ```  
5. Open your browser and go to:  
    👉 `http://localhost:5000/`

---

## 📂 Project Structure

- `Entities/` — domain models (Form, Question, Submission, etc.)  
- `DataBase/` — EF Core DbContext (`AppDbContext`)  
- `wwwroot/` — static frontend files (`index.html`, styles, images)  
- `Program.cs` — application startup  

---

## 📄 License

This project is protected by copyright.  
See the [LICENSE](LICENSE) file for details.