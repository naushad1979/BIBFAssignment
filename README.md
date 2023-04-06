# BIBFAssignment

# 1. Steps to run the services
 1. Change the SQL connection string located in appsettings.json under ProductAPI as per your local database settings. The connection  
  "ConnectionString": "Server=xxx;Initial Catalog=Productdb;User Id=xxx;Password=xxx;Encrypt=False" where xxx needs to be replaced with your local SQL value.
 
 2. Run ProductAPI and IdentityServer simultaneously. You can change the project setting as per below
 <img width="578" alt="image" src="https://user-images.githubusercontent.com/58332656/230296163-85dd2d07-7c11-4aaa-b063-6e62a3e00585.png">
 
 3. Get the JWT secured token with URL http://localhost:5002/Identity/GetToken?userName=admin this token will be needed for ProductAPI secured communication.
 
 4. The endpoint for creating a new product is http://localhost:7050/Product/Create and to access this endpoint, do the following steps
 
   4.1 Install the Postman client.   
   4.2 Create New collection named as ProductAPI.   
   4.3 Create New request named as CreateProduct with The Method type as POST, URL http://localhost:7050/Product/Create 
   4.4 under Body tab place the payload to create new Product. Select Raw from Radio button and select Json. The sample paylod is. 
          { 
            "name": "Cricket Bat",
            "description": "Most Popular Bat globally",
           "price": 2345.65
          }
      Refer The screen below
    <img width="640" alt="image" src="https://user-images.githubusercontent.com/58332656/230302107-d5a2d14d-ec8f-4838-8f27-c932eaade8f5.png">

   4.5 Under Authorization tab, select Bearer Token and paste the token received from the point 3
   
   5. To delete the product, the endpoint will be http://localhost:7050/Product/Delete/1 where 1 the id that you wish to delete. It may be different in your case.        The token settings will be as per point 4.5 above. The Method type will Delete.
   
   6. To update the Product, the ednpoint will be http://localhost:7050/Product/Update and other settings will be as per 4.4 and 4.5. The Method type will PUT. The         payload will be 
     {
       "id": 1,
       "name": "Modern Bat",
       "description": "New Modern Bat",
       "price": 23456.65
     }
   
   7. To get the product by Id, the endpoint will be http://localhost:7050/Product/GetById?id=2 where 2 will be changed as per your need. The Token setting will be         as 4.5 and The Method type will be GET
   8. To get all products, the endpoint will be http://localhost:7050/Product/GetAll and The Token setting will be as 4.5 and The Method type will be GET

  # 2. Solution details with folder wise 
     
       # ProductAPI: 
       The ProductAPI is the core service to manage the product CRUD operations. These are secured method that can only be accessed using secured JWT token                  provided by IdentityServer. The ProductAPI deatils are as follows
       
       Controllers: ProductController.cs -  This the interface to external client for the required communication related to Product.
       
       Exceptions: Contains various types of exception the global exception middleware uses.
       
       Infrastructure:  Infrastructure folder contains all persistence logic (database) for the application such Entity, EntityConfiguration, Repository and base            classes.
        
       Middlewares:  GlobalErrorHandlingMiddleware.cs will be responsible for handling the application exception globally. ApplicationBuilderExtensions.cs resgiters                      the GlobalErrorHandlingMiddleware as and extension method.
       
       Migrations : This folder consists of the DB migration related to table create, update or other related details.
       
       Models : Consists of Models and Validators to validate the model properties. FluentValidation is used for the validation.
       
       Profile : Consists of MappingProfile.cs that is reponsible for Mapping the entity. It uses AutoMapper for the mapping.
       
       Response : The response that API will return to the client.
       
       Services : The Business Service which act as a business layer.
       
       Program.cs  -  This file setup the all the service and middlewares in the startup.
       
       ServicesRegistration.cs - Sets the additional service though dependancy injection. This is used by Program.cs
       
       
   
    
    
     
   
