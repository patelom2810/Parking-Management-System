# Parking Management System (PMSF)

## 🚗 Offline Parking Management System

### 📌 Overview
The **Parking Management System (PMSF)** is an offline application designed to streamline and automate parking operations **without requiring an internet connection**. This system allows administrators to efficiently manage vehicle entries, exits, floor capacity, user access, and records, ensuring a smooth and reliable parking experience.

### 🔹 Key Features

- **🔐 Secure Login System** – Admin authentication for secure access.
- **🚗 Vehicle Entry & Exit Management** – Track vehicles in and out of the parking lot.
- **📊 Real-time Floor Management** – Monitor available slots per floor.
- **🔍 Vehicle Search & Records** – Retrieve vehicle history quickly.
- **📜 Receipt Generation** – Print receipts for vehicle entries.
- **👥 User Management** – Add and manage users within the system.
- **🔑 Password Recovery** – Securely reset forgotten passwords.
- **💾 Offline Functionality** – No internet required for operation.

---

## 🛠️ Project Structure

### 📂 Root Directory
```
PMSF
│── PMSF.sln                      # Visual Studio Solution File
│── PMSF.vbproj                   # Project File
│── PMSF.mdf                      # Database File
│── PMSF_log.LDF                  # Log File for Database
│── App.config                    # Application Configuration
│── README.md                     # Project Documentation
├── Application Files
│   ├── Application.Designer.vb
│   ├── Application.myapp
│   ├── AssemblyInfo.vb
│   ├── Resources.Designer.vb
│   ├── Resources.resx
│   ├── Settings.Designer.vb
│   ├── Settings.settings
├── Forms
│   ├── 1_Login                    # Login Form
│   │   ├── 1_Login.Designer.vb
│   │   ├── 1_Login.resx
│   │   ├── 1_Login.vb
│   ├── 2_Dashboard                 # Admin Dashboard
│   │   ├── 2_Dashboard.Designer.vb
│   │   ├── 2_Dashboard.resx
│   │   ├── 2_Dashboard.vb
│   ├── 3_Ventry                    # Vehicle Entry
│   │   ├── 3_Ventry.Designer.vb
│   │   ├── 3_Ventry.resx
│   │   ├── 3_Ventry.vb
│   ├── 4_Vexit                     # Vehicle Exit
│   │   ├── 4_Vexit.Designer.vb
│   │   ├── 4_Vexit.resx
│   │   ├── 4_Vexit.vb
│   ├── 5_Receipt                   # Receipt Generation
│   │   ├── 5_Receip.Designer.vb
│   │   ├── 5_Receip.resx
│   │   ├── 5_Receip.vb
│   ├── 6_VehicleSearch             # Vehicle Search
│   │   ├── 6_VehicleSearch.Designer.vb
│   │   ├── 6_VehicleSearch.resx
│   │   ├── 6_VehicleSearch.vb
│   ├── 7_RecordofVehicle           # Vehicle Record Management
│   │   ├── 7_RecordofVehicle.Designer.vb
│   │   ├── 7_RecordofVehicle.resx
│   │   ├── 7_RecordofVehicle.vb
│   ├── 8_FloorManage               # Floor Management
│   │   ├── 8_FloorManage.Designer.vb
│   │   ├── 8_FloorManage.resx
│   │   ├── 8_FloorManage.vb
│   ├── 9_ChangePass                # Change Password
│   │   ├── 9_ChangePass.Designer.vb
│   │   ├── 9_ChangePass.resx
│   │   ├── 9_ChangePass.vb
│   ├── 10_EntryToken               # Entry Token Generation
│   │   ├── 10_EntryToken.Designer.vb
│   │   ├── 10_EntryToken.resx
│   │   ├── 10_EntryToken.vb
│   ├── 11_AddUser                  # Add User
│   │   ├── 11_AddUser.Designer.vb
│   │   ├── 11_AddUser.resx
│   │   ├── 11_AddUser.vb
│   ├── 12_ForgotPassword           # Forgot Password
│   │   ├── 12_ForgotPassword.Designer.vb
│   │   ├── 12_ForgotPassword.resx
│   │   ├── 12_ForgotPassword.vb
```

---

## 🚀 Installation & Setup

### 🛠️ Prerequisites
- **Windows OS** (For running VB.NET applications)
- **Visual Studio** (With VB.NET support)
- **SQL Server Express** (For managing the database)

### 📥 Steps to Run
1. Clone the repository or download the source code.
2. Open the solution file `PMSF.sln` in **Visual Studio**.
3. Restore the database from `PMSF.mdf` using SQL Server.
4. Build and run the project from Visual Studio.

---

## 📌 Usage Guide

### 1️⃣ Login as Admin
- Use admin credentials to access the dashboard.

### 2️⃣ Vehicle Entry & Exit
- Register vehicle details upon entry and remove upon exit.

### 3️⃣ Search & Manage Vehicles
- Use the **Vehicle Search** module to find registered vehicles.

### 4️⃣ Manage Parking Floors
- Admin can assign total capacity and track available slots per floor.

### 5️⃣ User & Password Management
- Admin can add new users and change passwords when needed.

---

## 🤝 Contributing
If you’d like to contribute to **PMSF**, feel free to fork this repository and submit pull requests.

---

## 📝 License
This project is licensed under the **MIT License** – feel free to use and modify it!

---

## 📧 Contact
For any inquiries, reach out via **[Your Contact Email]** or connect with me on **LinkedIn**.

#ParkingSystem #OfflineManagement #VBNet #SQLServer #Automation #SmartParking 🚀

