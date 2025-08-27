# 🏫 School_66 — School Management System

This is a fullstack school management system built with **ASP.NET Core** as the backend and a simple **HTML/Bootstrap frontend**.  
The application allows users to manage students, teachers, subjects, classes, schedules, grades, and attendance.  

## 🚀 Features

- 👩‍🏫 Manage teachers and assign them to classes and subjects  
- 🧑‍🎓 Manage students and track their attendance  
- 📚 Manage subjects and connect them to teachers and classes  
- 🗓 Manage class schedules  
- 📝 Add and view homework for students  
- 🎯 Manage grades for students in different subjects  
- 🖥 Frontend home page with navigation (built using static HTML + Bootstrap)  

## 🛠 Tech Stack

### Backend
- ASP.NET Core 9
- Entity Framework Core with SQLite
- C#

### Frontend
- HTML + Bootstrap (static pages in `wwwroot`)
- JavaScript (optional for interactivity)

## 📦 Installation

> 1. Clone or download the repository  
> 2. Open the project in **Rider** or **Visual Studio**  
> 3. Run database migrations (if needed)  
>    ```bash
>    dotnet ef migrations add InitialCreate
>    dotnet ef database update
>    ```
> 4. Run the application  
>    ```bash
>    dotnet run
>    ```
> 5. Open your browser and go to:  
>    👉 `http://localhost:5000/` to see the main page  

## 📂 Project Structure

- `Entities/` — domain models (Student, Teacher, Subject, Class, Schedule, Grades, HomeWork, Attendance, etc.)  
- `DataBase/` — EF Core DbContext (`AppDbContext`)  
- `wwwroot/` — static frontend files (`index.html`, styles, images)  
- `Program.cs` — application startup  

---

✨ With this project you can manage a whole school system in one place!  
