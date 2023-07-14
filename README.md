# Travel agency desktop application

## Tech Stack
- C# .NET WPF
- Entity Framework

## Description

This project is a desktop travel agency application. It is designed to support the work of travel agents and allow clients to browse and book travel arrangements for various destinations.

There are two types of users in the system:

1. Registered Users (Clients/Travelers): They can view available travel arrangements and make reservations.

2. Agents: They can add new travel arrangements, update existing ones, add and update accommodations, restaurants, and tourist attractions.

The application provides the following functionalities:

1. Booking and purchasing travel arrangements.
2. Viewing purchased and reserved travel arrangements.
3. Browsing all available travel arrangements.
4. Viewing travel arrangements on a map.
5. Adding, updating, and deleting travel arrangements.
6. Adding, updating, and deleting tourist attractions.
7. Adding, updating, and deleting accommodations and restaurants.
8. Viewing sold travel arrangements within a specific month.
9. Viewing the number of bookings for a particular travel arrangement.

## How to Run

1. Clone the repository:
```
git clone https://github.com/VukRadmilovic/travel-agency-desktop.git
```
2. Open the project in Visual Studio.

3. Set up the database connection in the configuration file (`Utils/Context.cs`).

4. Build the solution.

5. Run the migration commands to create the database schema:
```
Add-Migration InitialMigration
Update-Database
```
6. Add an initial user if you wish to log in as an agent via the SQL Server Object Explorer.

7. Run the application.

Note: Additional configuration steps may be required depending on your environment.
