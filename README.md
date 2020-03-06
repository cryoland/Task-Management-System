# Task management system for small developer team (like “Kanban”).

The main application window is a list of tasks (tickets). Each task has the following fields: Name, Description, Status, Priority, Reporter, Assignee. It is possible to open each task in a separate window to edit the fields.
Application users have two different roles: Manager and Developer. A user with the Manager role can add new tasks and edit all task's fields. A user with the Developer role can change the task Status and the Assignee field.
Implementation Note. 

## Technologies

* Language: C#. 
* Platform: ASP.NET Core 3
* Database: MS SQL Server 
* Additional: ORM Entity Framework Core 3 and xUnit.

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/cryoland/task-management-system/blob/onion-architecture/LICENSE) file for details.