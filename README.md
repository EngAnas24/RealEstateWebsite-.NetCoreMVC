# 🏠 Real Estate Management System

## 📌 Overview

The **Real Estate Management System** is a web-based platform that allows users to publish, search, and manage real estate listings such as properties for **sale, rent, or purchase requests**.

The platform provides a **clean and user-friendly interface** for browsing properties and a **powerful management system** for both **users and administrators**.

The system is designed using the **N-Tier Architecture** to ensure **scalability, maintainability, and separation of concerns**.

---

# ✨ Features

## 🏡 Home Page

The home page displays the **latest property listings** published on the platform.

Users can easily search for properties using multiple filters such as:

🔎 **Address / Location**
💰 **Operation Type** (Sale • Rent • Buy)
🏢 **Property Type** (Apartment • House • Land • etc.)

This makes it easy for users to quickly find properties that match their needs.

---

## 📄 Property Details

When a user opens a property listing they can:

👀 View **complete property details**
📍 See **location, price, and description**
🖼️ View property images
⭐ **Save the property to favorites**
📞 **Contact the property owner directly**

---

## 👤 User Dashboard

Registered users have access to their **personal account dashboard** where they can:

✏️ Update **username**
📱 Update **phone number**
💬 Send **messages to the support team**
📊 Manage their **published properties**

Users can also:

➕ **Create new property listings**
✏️ **Edit their own listings**
🗑️ **Delete their listings**

⚠️ Only **authenticated users** can publish properties on the platform.

---

## ⭐ Favorites System

Users can mark any property as **favorite**.

📌 Favorite properties are stored in the user's account, allowing them to quickly access them later.

---

## 🔍 Advanced Search & Filtering

The platform provides a **powerful search system** that allows users to filter properties based on:

📍 Address / Location
🏠 Property Type
💰 Operation Type (Sale • Rent • Buy)

This helps users find the **most relevant properties quickly**.

---

## 🛠️ Admin Control Panel

Administrators have access to a **complete system management dashboard**.

The admin panel allows administrators to:

👥 Manage all users
🏠 Manage property listings
📨 Review support messages
⚙️ Monitor and control the entire platform

---

# 💻 Technologies Used

## 🔧 Backend

* **ASP.NET Core MVC**
* **C#**
* **N-Tier Architecture**
* **Entity Framework Core**
* **Microsoft SQL Server**

## 🎨 Frontend

* **HTML**
* **CSS**
* **Bootstrap** (UI components and icons)
* **AJAX** (used in delete actions and dynamic interactions)

---

# 🏗️ System Architecture

The system follows the **N-Tier Architecture** which separates the application into multiple layers for better organization.

### 🎨 Presentation Layer

Handles the **user interface and user interactions**.

Technologies:

* ASP.NET Core MVC
* HTML
* CSS
* Bootstrap
* JavaScript / AJAX

---

### ⚙️ Business Logic Layer

Responsible for **application rules and core logic**.

Responsibilities include:

✔ Data validation
✔ Business operations
✔ Processing application workflows

---

### 🗄️ Data Access Layer

Responsible for communication with the database.

Technologies used:

* **Entity Framework Core**
* **SQL Server**

---

# 🔐 Authentication & Authorization

The system implements full **Authentication and Authorization** to secure the application.

Implemented features include:

🔑 User **Registration and Login**
🛡️ **Role-Based Access Control**
🔒 Protected routes and actions
👤 User identity management
👑 Admin-only privileges

Only authenticated users can:

✔ Publish property listings
✔ Manage their listings
✔ Save favorite properties

---

# 🗃️ Database

The system uses **Microsoft SQL Server** as the main database.

The database stores the following data:

👤 Users
🏠 Property Listings
🏷️ Property Types
⭐ Favorites
💬 Messages
⚙️ Administrative data

---

# 🚀 Key Functionalities

✔ Publish real estate listings
✔ Edit and delete user listings
✔ Advanced property search and filtering
✔ Property favorites system
✔ Direct communication with property owners
✔ User account management
✔ Admin management panel
✔ Secure authentication and authorization

---

# 🎯 Project Goals

The main goals of this project are:

🏗️ Build a **real-world real estate platform**
🧠 Apply **N-Tier architecture principles**
🔐 Implement **secure authentication and authorization**
📈 Create a **scalable and maintainable system**

---

# 🔮 Future Improvements

Possible future enhancements:

🖼️ Add **property image uploads**
💬 Implement **real-time chat between users**
🗺️ Integrate **Google Maps for property locations**
🔍 Improve **advanced filtering system**
🔔 Add **notification system**

---

# 👨‍💻 Author

**Anas Algahma**

🎓 Computer Engineer
💻 Back-End Developer (.NET Core)
