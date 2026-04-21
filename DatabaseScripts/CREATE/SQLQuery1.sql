CREATE TABLE Employee
(
    Id        INT PRIMARY KEY IDENTITY(1,1),
    Name      NVARCHAR(100)  NOT NULL,
    IsEnabled BIT            NOT NULL DEFAULT 1,
    ManagerId INT            NULL REFERENCES Employee(Id)
);