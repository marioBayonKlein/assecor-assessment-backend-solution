# Assecor-assessment-backend-solution
This Project contains my personal solution for the corresponding test from: https://github.com/Assecor-GmbH/assecor-assessment-backend

The solution is programmed in .NET Core 3.1 version and it has 3 different projects:

### App Project ###
This Project is an ASP.NET Core Web API Project and it will be the main Project from the Solution. 
This Project contains the corresponding logic for: 

* Register all the implementations in the dependency injection container, so the Framework will resolve automatically the dependencies when these are called by the different constructors of the classes.
* Build the entry point for the User, using a basic Controller which will handle the exceptions (because it's the highest level of the Application) and it will delegate the responsability to retrieve and insert the information about the persons to a Manager class.
* Read the configuration in order to decide which implementation should be resolved for the Repository.

### Src Project ###
This is the Source Project and it is a Net Standard 2.0 Class Library. This project contains all the necessary logic to manipulate the different sources of the Data and these will be:

* The provided csv file from the original test definition, which has a specific format and my solution is to implement through the Repository Pattern an implementation in Memory, because one of the criteria acceptance was that the csv must not be modified in any moment. So the Memory Repository will retrieve all the data from the csv file in Memory, with an IDictionary structure.

* A SQL Server DataBase. There is another implementation of the Repository, which will handle the information defined in the DataBase. This implementation is done with Entity Framework Core and for testing it I have used a Microsoft SQL Server on Linux image from the Docker Hub (https://hub.docker.com/_/microsoft-mssql-server). To configure the application to hit a Sql Server DataBase, the configuration of the 'DefaultConnection' in the settings must be changed for the corresponding server name, data base name, the user and the password. The corresponding table created for modeling the entity 'Person' for testing is seen bellow:

```sql
CREATE TABLE Persons (
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(50) NOT NULL,
    LastName varchar(50) NOT NULL,
    Color int NOT NULL CHECK (Color IN(1, 2, 3, 4, 5, 6, 7)),
	Direction varchar(255) NOT NULL
); 
```

The Person entity from the Source project is defined as the next DTO for the entire Application:
```C#
public class PersonFromSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Color Color { get; set; }
        public string Direction { get; set; }

    }
```  

### Test Project ###
This folder contains two different Unit testing projects, one for the Application and the other for the Source project. The implementations are tested using the XUnit Framework (https://xunit.net/) and the Moq Framework (https://www.nuget.org/packages/Moq/) for mocking inyected services.

### Implemented additional points  ###
* I have implemented the additional Post method for inserting new persons in the Dataset. The applications allows to the user insert on single Person per each Post call and the Id value must be omitted. For example:

```json
{
    "name": "Peter",
    "lastName": "Parker",
    "zipCode": "82631",
    "city": "New York",
    "color": "Red"
}
```  
* I have implemented one Repository with Entity Framework Core, so it's possible to add another data source like a SQL DataBase. It's possible two switch the Memory implementation with the DataBase implementation by changing the RepositoryType value in the settings configuration.

I did not implement the MSBuild because I didn't implement a MSBuild file for any Project in the past and I was not shure to try to implement it for this project and do it wrong.

For any questions or comments, you can contact me to my personal email mariobayon@outlook.es .
