###Freelancer Application###

To access the front-end(hosted) can directly go to this link:
https://freelanceui-e297d6c9997f.herokuapp.com/

To access the backend-api swagger documentation directly go to this link:
https://cdn-91il.onrender.com/swagger/index.html

To run locally, kindly run these steps:

Steps:
1. Install .NET Core SDK
2. Install Postgres
3. If you want to use server-hosted db, skip this part, if you want to use fresh local db:
    - Create a db named "cdn_main"
    - update .env variable to point to local db
    - run db update: dotnet ef database update
4. cd to FreelanceApp folder
    - cd FreelanceApp
5. Build FreelanceApp project
    - dotnet build
6. Run local development command
    - run dotnet watch run
7. Feel free to play around the API!
