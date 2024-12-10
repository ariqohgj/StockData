# How To Use 
- Step 1 : Clone This Project
- Step 2 : Download SSMS / SQL Server Management Studio, [Download SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16&redirectedfrom=MSDN)
- Step 3 : Open And Connect, The setting will be like this
  >![image](https://github.com/user-attachments/assets/f341bb3a-c997-4a6d-b46a-c3978e8d0f5f)

- Step 4 : Open cloned project and open appsetings.json
- Step 5 : Change this with your server name
  >"Data Source=YOUR SERVER NAME;Initial Catalog=blackshark;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  > ![image](https://github.com/user-attachments/assets/9a34bd8f-881c-40db-9a9a-81d062e8822a)
- Step 6 : Open terminal and run this code step by step
  - > cd api
  - > dotnet tool install --global dotnet-ef
  - > dotnet add package Microsoft.EntityFrameworkCore.Design
  - > dotnet ef database update
- Step 7 : When all done run this on terminal
  - > dotnet watch run

 DONE

