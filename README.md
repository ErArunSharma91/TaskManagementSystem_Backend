# Task Management System - Backend Documentation

## Objective

The **Task Management System** backend provides functionality for user registration, authentication, and task management. Users can create, update, delete, and track tasks, while ensuring that only authenticated users can perform these operations.

---

## Technologies Used

- **.NET Core**: For building the backend API.
- **Entity Framework Core**: For database interaction.
- **JWT (JSON Web Token)**: For securing API endpoints.
- **MySQL or SQL Server**: As the database system to store user and task data.

---

## Features

### 1. **User Authentication**

- **Registration**: Users can register with a unique username, email, and password.
- **Login**: Users can log in using their username or email and password.
- **JWT Token**: Upon successful login, a JWT token is generated, which must be used to authenticate all subsequent API requests.

### 2. **Task Management**

- **Create a Task**: Authenticated users can create tasks with a title, description, due date, and status (e.g., Pending, In Progress, Completed).
- **Read Tasks**: Retrieve a list of tasks that belong to the authenticated user.
- **Update a Task**: Modify details of an existing task.
- **Delete a Task**: Delete a task from the user's task list.

---

## API Endpoints

### **Authentication Endpoints**

1. **POST** `/api/user/register`

   - Registers a new user.
   - **Request Body**:
     ```json
     {
       "username": "user1",
       "email": "user1@example.com",
       "password": "Password@123"
     }
     ```
   - **Response**: `200 OK` on successful registration.

2. **POST** `/api/user/login`
   - Authenticates the user and returns a JWT token.
   - **Request Body**:
     ```json
     {
       "username": "user1",
       "password": "Password@123"
     }
     ```
   - **Response**:
     ```json
     {
       "token": "your-jwt-token"
     }
     ```

### **Task Management Endpoints**

1. **GET** `/api/tasks`

   - Retrieves a list of tasks for the authenticated user.
   - **Response**:
     ```json
     [
       {
         "id": 1,
         "title": "Task 1",
         "description": "Description for task 1",
         "dueDate": "2024-09-20",
         "status": "Pending"
       },
       {
         "id": 2,
         "title": "Task 2",
         "description": "Description for task 2",
         "dueDate": "2024-09-21",
         "status": "In Progress"
       }
     ]
     ```

2. **POST** `/api/tasks`

   - Creates a new task.
   - **Request Body**:
     ```json
     {
       "title": "Task 1",
       "description": "This is the first task",
       "dueDate": "2024-09-20",
       "status": "Pending"
     }
     ```
   - **Response**: `201 Created`

3. **PUT** `/api/tasks/{id}`

   - Updates an existing task.
   - **Request Body**:
     ```json
     {
       "title": "Updated Task 1",
       "description": "Updated description",
       "dueDate": "2024-09-22",
       "status": "In Progress"
     }
     ```
   - **Response**: `200 OK`

4. **DELETE** `/api/tasks/{id}`
   - Deletes a task.
   - **Response**: `204 No Content`

---

## Database Design

The system uses a relational database to store information about users and tasks. The database structure is as follows:

### **Users Table**

| Field          | Type   | Description                        |
| -------------- | ------ | ---------------------------------- |
| `Id`           | int    | Primary key                        |
| `Username`     | string | Unique username                    |
| `Email`        | string | User's email                       |
| `PasswordHash` | string | Secure hash of the user's password |

### **Tasks Table**

| Field         | Type   | Description                                   |
| ------------- | ------ | --------------------------------------------- |
| `Id`          | int    | Primary key                                   |
| `Title`       | string | Title of the task                             |
| `Description` | string | Detailed description of the task              |
| `DueDate`     | Date   | Due date for task completion                  |
| `Status`      | string | Task status (Pending, In Progress, Completed) |
| `UserId`      | int    | Foreign key referencing the user              |

---

## Security

- **JWT Authentication**: All task-related API endpoints are protected and require a valid JWT token. The JWT is generated upon login and must be included in the `Authorization` header for subsequent API requests.

Example Authorization header:

```
Authorization: Bearer your-jwt-token
```

---

## Error Handling

- **400 Bad Request**: For invalid inputs.
- **401 Unauthorized**: When requests are made without a valid JWT token.
- **404 Not Found**: When trying to access a non-existent task.
- **500 Internal Server Error**: For unexpected server errors.

---

## Setup Instructions

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/ErArunSharma91/TaskManagementSystem_Backend.git
   ```

2. **Navigate to the Project Directory**:

   ```bash
   cd TaskManagementSystem_Backend
   ```

3. **Restore Dependencies**:

   ```bash
   dotnet restore
   ```

4. **Set Up the Database**:

   - Ensure the correct connection string is provided in `appsettings.json`.
   - Run database migrations:
     ```bash
     dotnet ef database update
     ```

5. **Run the Application**:
   ```bash
   dotnet run
   ```

---

## Contribution Guidelines

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Submit a pull request once the feature is complete.
4. Ensure all changes are tested before submission.

---

## License

This project is licensed under the MIT License.
