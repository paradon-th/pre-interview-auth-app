# Assignment: ระบบยืนยันตัวตนและจัดการผู้ใช้ (Full-Stack)



โปรเจกต์นี้เป็นระบบยืนยันตัวตนและจัดการผู้ใช้ ที่สร้างขึ้นสำหรับโจทย์ Pre-Interview Round 1 - Software Engineer ประกอบด้วย Backend API, Frontend Web App และ Database ที่ทำงานบน Docker



## ✨ คุณสมบัติหลัก (Features)

* **ระบบยืนยันตัวตน**: รองรับการสมัครสมาชิก (Register) และ ล็อกอิน (Login)
* **ความปลอดภัย**: ใช้ JWT Bearer Token ในการยืนยันตัวตนเพื่อเข้าถึง API
* **การจัดการผู้ใช้ (CRUD)**: หน้า Dashboard เพื่อดู, แก้ไข(Admin), และลบข้อมูลผู้ใช้(Admin)
* **การจัดการสิทธิ์ (Role-Based Access)**: แบ่งสิทธิ์ผู้ใช้อย่างชัดเจนระหว่าง `Admin` และ `User`


## 🛠️ เทคโนโลยีที่ใช้ (Tech Stack)

* **Backend**: .NET 8 Web API
* **Frontend**: Angular 19
* **UI Framework**: Angular Material & Tailwind CSS
* **Database**: PostgreSQL
* **Containerization**: Docker & Docker Compose

## 🚀 การติดตั้งและรันโปรเจกต์

### สิ่งที่ต้องมี
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (ต้องติดตั้งและเปิดโปรแกรมไว้)

### ขั้นตอนการรัน
1.  **Clone Repository:**
    ```bash
    git clone https://github.com/paradon-th/pre-interview-auth-app.git
    cd pre-interview-auth-app
    ```

2.  **รันโปรเจกต์ด้วย Docker Compose:**
    ```bash
    docker-compose up --build
    ```
    คำสั่งนี้จะทำการ build image ของ Backend และ Frontend จากนั้นจึงรัน service ทั้ง 3 ตัวขึ้นมา

3.  **การเข้าใช้งาน:**
    เมื่อทุกอย่างรันเสร็จเรียบร้อยแล้ว:
    * **หน้าบ้าน (Frontend)**: เปิด Browser ไปที่ `http://localhost:4200`
    * **หลังบ้าน (Backend API)**: ดูเอกสาร API ได้ที่ `http://localhost:8080/swagger`

## 👨‍💻 ข้อมูลเริ่มต้นสำหรับทดสอบ (Seed Data)
ฐานข้อมูลได้ทำการ Seed ข้อมูลผู้ใช้เริ่มต้นไว้ให้ 2 บัญชี เพื่อให้สามารถทดสอบระบบได้ทันที:

| สิทธิ์ | อีเมล                 | รหัสผ่าน       |
| :--- |:----------------------|:---------------|
| 👨‍💻 **ผู้ดูแลระบบ (Admin)** | `somying.j@system.io` | `password123!` |
| 👤 **ผู้ใช้ทั่วไป (User)** | `somchai.r@email.com` | `password123!` |