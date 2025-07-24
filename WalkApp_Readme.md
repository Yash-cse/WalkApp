# WalkApp

### Steps
#### Step 1 :- Domain/models
=> Creating Models and Domain to assign relationship properties like DataType & Nullable.

#### Step 2 :- Framework
=> EF.Core.design
=> EF.Core.tool
=> EF.Core.sql

#### Step 3 :- Data/DbContext
=> Creating DbContext file to create bridge between Domain models and database. Adding DbSet properties of models.

#### Step 4 :- Api/Connection String
=> Creating Connection string for Database connection. 

#### Step 5 :- Api/Dependency Injection (DI)
=> Adding DI to main program file.

#### Step 6 :- DAL/Migration
=> Migrating the data using ef core to create tables in DB.
=> Code: Add-Migration "Name of migration(eg: Initial)" -Project "Your target project(API)" -StartupProject "Your Startup project(DAL)"

#### Step 7 :- API/Controllers
=> Creat CRUD Operations in the api controller (Region Controller)
=> GET | PUT | POST | DELETE

#### Step 8 :- Domain/Data Transfer Objects(DTO)
=> Used to transfer data between diffrent layers so that the domain layer is not exposed to the database directly.

### Now we will make the project more advance, fast and secure.

#### Step 9 :- project/Async Programing
=> Using Async and Await keyword so that all the requests can run simultaneously(so that it can handel more request) and the other request dont have to wait for the prevouse one to complete. 

#### Step 10 :- DAL/Repository Pattern
=> We create the repository so that we can pass DbContext through it so that Database will not expose directly to the Controllers, this will adds security to the data and hide code from controller as well.
=> Adding Repository services to the main program file.
=> Simple Diagram :
api request = -->
							DbContext
									|
				 					V
Database <-- 	Repository 	<--		Controller
					
#### Step 11 :- Domain/Auto mapper 
=> This will help to Map data (for eg: mapping domain data to the data transfer objects) this will help to simplify code and makes the code shorter as manual mapping is not requeried. Injecting automapper profile to the main program file.
=> CreateMap<Source, Destination>().ReverseMap();
=> _mapper.map<destination of data flow>(Source of data flow);

#### Step 12 :- API/Walks Controller 
=> Seeding Data using EF core and adding crud operations to the Walks Controller. 






