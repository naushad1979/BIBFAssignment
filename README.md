# BIBFAssignment

#Steps to run the services
 1. Change the SQL connection string located in appsettings.json under ProductAPI as per your local database settings. The connection  
  "ConnectionString": "Server=xxx;Initial Catalog=Productdb;User Id=xxx;Password=xxx;Encrypt=False" where xxx needs to be replaced with your local SQL value.
 
 2. Run ProductAPI and IdentityServer simultaneously. You can change the project setting as per below
 <img width="578" alt="image" src="https://user-images.githubusercontent.com/58332656/230296163-85dd2d07-7c11-4aaa-b063-6e62a3e00585.png">
 
 3. Get the JWT secured token with URL http://localhost:5002/Identity/GetToken?userName=admin this token will be needed for ProductAPI secured communication.
 4.  

  
