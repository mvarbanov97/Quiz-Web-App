# Quiz Wep App

Quizaldo is ASP.NET Core web application where registered users can paticipate in quizzes on different topics.

Quiz Web Application is a project assigned by Telerik Progress as one of the stages during the hiring process.

**Admin account**: </br>
 *email*: test@abv.bg <br>  *password*: 123456
## Getting Started

###### To run the application you need:
- .NET Core 3.1 

- If you don't have *Sql server* on your machine you should replace the configuration in *QuizWebApp.Web/appsettings.json* with this code:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\mssqllocaldb;Database=Quizaldo;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
- In your *package manger console* type: 

```
update-database
```

## 🛠 Built with:
* .Net Core MVC 3.1
* Entity Framework Core 3.1.13
* Bootstrap
* HTML
* CSS
* JavaScript
* Fluent API
* xUnit
* Moq


![alt text](https://i.imgur.com/ZcwjPdC.png)
