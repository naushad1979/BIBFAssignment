# BIBFAssignment

# Steps to run the services
 1. Change the SQL connection string located in appsettings.json under ProductAPI as per your local database settings. The connection  
  "ConnectionString": "Server=xxx;Initial Catalog=Productdb;User Id=xxx;Password=xxx;Encrypt=False" where xxx needs to be replaced with your local SQL value.
 
 2. Run ProductAPI and IdentityServer simultaneously. You can change the project setting as per below
 <img width="578" alt="image" src="https://user-images.githubusercontent.com/58332656/230296163-85dd2d07-7c11-4aaa-b063-6e62a3e00585.png">
 
 3. Get the JWT secured token with URL http://localhost:5002/Identity/GetToken?userName=admin this token will be needed for ProductAPI secured communication.
 
 4. The endpoint for creating a new product is http://localhost:7050/Product/Create and to access this endpoint, do the following steps
   4.1 Install the Postman client 
   4.2 Create New collection named as ProductAPI
   4.3 Create New request named as CreateProduct with type as POST, URL http://localhost:7050/Product/Create 
   4.4 under Body tab place the payload to create new Product. Select Raw from Radio button and select Json. The sample paylod is. 
          { 
            "name": "Cricket Bat",
            "description": "Most Popular Bat globally",
           "price": 2345.65
          }
      Refer The screen below
    <img width="640" alt="image" src="https://user-images.githubusercontent.com/58332656/230302107-d5a2d14d-ec8f-4838-8f27-c932eaade8f5.png">

   4.5 Under Authorization tab, select Bearer Token and paste the token received from the point 3

  
