 HEAD
--------Day-2:CLASS EXERCISE-----------

------------------------------INDEXES:-------------------------------------
create database Training;
use Training;

CREATE TABLE Employee1
(Id INT,
Name VARCHAR(50),
Salary INT,
Gender VARCHAR(10),
City VARCHAR(50),
Dept VARCHAR(50)
)
select * from Employee1
INSERT INTO Employee1 VALUES (3, 'Pranaya',4500,'Male', 'New York', 'IT' ),
(1, 'anu',4500,'female', 'New York', 'IT' ),
(4, 'priya',3000,'female', 'New York', 'HR' ),
(5, 'sambit',43400,'Male', 'chennai', 'HR' ),
(7, 'preety',30500,'female', 'China', 'IT' ),
(6, 'tarun',4500,'Male', 'New York', 'IT' ),
(2, 'hina',2000,'female', 'mumbai', 'HR' ),
(8, 'john',400,'Male', 'delhi', 'IT' ),
(10, 'pam',4500,'Male', 'US', 'HR' ),
(9, 'sara',450,'female', 'New York', 'IT' );

SELECT * FROM Employee1 WHERE Id=8

CREATE INDEX IX_EMPLOYEE_ID
ON EMPLOYEE1(ID ASC)

CREATE CLUSTERED INDEX IX_EMPLOYEE_ID1
ON EMPLOYEE1(ID ASC)

DROP TABLE Employee1

CREATE TABLE Employee
(Id INT primary key,
Name VARCHAR(50),
Salary INT,
Gender VARCHAR(10),
City VARCHAR(50),
Dept VARCHAR(50)
)
INSERT INTO Employee VALUES (3, 'Pranaya',4500,'Male', 'Sydney', 'IT' ),
(1, 'anu',4500,'female', 'New York', 'IT' ),
(4, 'priya',3000,'female', 'London', 'HR' ),
(5, 'sambit',43400,'Male', 'Tokiyo', 'HR' ),
(7, 'preety',30500,'female', 'China', 'IT' ),
(6, 'tarun',4500,'Male', 'New York', 'IT' ),
(2, 'hina',2000,'female', 'mumbai', 'HR' ),
(8, 'john',400,'Male', 'delhi', 'IT' ),
(10, 'pam',4500,'Male', 'US', 'HR' ),
(9, 'sara',450,'female', 'New York', 'IT' );

truncate table Employee
select * from Employee

use BikeStores;
SELECT * FROM sales.customers
-- Example for Unique Index:
CREATE UNIQUE INDEX Idx_unique_email
ON sales.customers(email)

-- Clustered Index:
--We can create only one clustered index per table
--if we have primary key in a table automatically it will create clustered index for that table
--suppose when table is not having primary key then only we can create clustered index. while creating clustered index
--if we can du licate and null values exists it will sort and store the data
use Training
CREATE CLUSTERED INDEX IX_EMPLOYEE_ID1
ON EMPLOYEE(ID ASC)

-- Non-Clustered Index:
-- we can create upto 999 non clustered index per table
use BikeStores
CREATE NONCLUSTERED INDEX IDX_NAME
ON sales.customers(first_name,last_name)
(or)
CREATE INDEX IDX_NAME1
ON sales.customers(first_name,last_name)

use Training
CREATE TABLE Department(
Id int, Name Varchar(100))

Insert into Department values(1,'HR'),(1,'Admin')

select* from Department;

create Clustered Index Idx_dept_id
ON Department(Id)

Insert into Department values(2,'IT'),(3,'Transport'),(2,'Information Tech')

Insert Into Department(Name) values('Insurance')


--SQL Script to create tblEmployee table:
CREATE TABLE tblEmployee
(
Id int primary key,
Name nvarchar(30),
Salary int,
Gender nvarchar(10),
DepartmentId int
)

--SQL Script to create tb1Department table:
CREATE TABLE tblDepartment(
DepartID INT PRIMARY KEY,
DeptName nvarchar(20)
)

--lnsert data into tb1Department table:
Insert into tblDepartment values (1,'IT')
Insert into tblDepartment values (2,'Payroll')
Insert into tblDepartment values (3,'HR')
Insert into tblDepartment values (4,'Admin')

--lnsert data into tb1Employee table:
Insert into tblEmployee values (1, 'John',5000,'male',3)
Insert into tblEmployee values (2, 'mike',5000,'male',2)
Insert into tblEmployee values (3, 'Pam',5000,'male',1)
Insert into tblEmployee values (4, 'Todd',5000,'male',4)
Insert into tblEmployee values (5, 'Sara',5000,'male',1)
Insert into tblEmployee values (6, 'Ben',5000,'male',3)

SELECT Id,Name,Salary, Gender,DeptName
from tblEmployee
join tblDepartment
on tblEmployee.DepartmentId=tblDepartment.DepartID;


--------------------------------------VIEW:------------------------------------------------------

--Now let's create a view, using the JOINS query, we have just written.
CREATE VIEW vWEmployeesByDepartment
AS
SELECT Id, Name,Salary,Gender,DeptName
from tblEmployee
join tblDepartment
on tblEmployee.DepartmentId=tblDepartment.DepartID

select * from vWEmployeesByDepartment
select * from tblEmployee
update vWEmployeesByDepartment set Name='Geetha' where Id=3

insert into vWEmployeesByDepartment values(7,'Ron',9000,'Male','IT')

--View that returns only IT department employees:
CREATE VIEW vWITDepartment_Employees
AS
SELECT Id, Name,Salary,Gender,DeptName
from tblEmployee
join tblDepartment
on tblEmployee.DepartmentId=tblDepartment.DepartID
where tblDepartment.DeptName='IT'

SELECT * FROM vWITDepartment_Employees

--View that returns all columns except Salary column:
CREATE VIEW vWEmployeesNonConfidentialData
as
Select Id, Name,Gender,DeptName
from tblEmployee
join tblDepartment
on tblEmployee.DepartmentId=tblDepartment.DepartID

select * from vWEmployeesNonConfidentialData

Create View vWEmployeesCountByDepartment
as
Select DeptName, COUNT(Id) as TotalEmployees
from tblEmployee
join tblDepartment
on tblEmployee.DepartmentId=tblDepartment.DepartID
Group by DeptName

select * from vWEmployeesCountByDepartment
sp_helptext vWEmployeesCountByDepartment

-----------------------------------------------TRIGGER:-----------------------------------------------
Create table orders(
order_id INT PRIMARY KEY,
customer_id INT,
order_date DATE
);

Create table order_audit(
audit_id INT IDENTITY PRIMARY KEY,
order_id INT,
customer_id INT,
order_date DATE,
audit_date DATETIME DEFAULT GETDATE()
);

ALTER TABLE order_Audit ADD audit_information varchar(max)

--Example for After or For Trigger with insert:
select * from orders;

select * from order_audit;

CREATE TRIGGER trgAfterInsertOrder
ON Orders
AFTER INSERT
AS
BEGIN
DECLARE  @auditInfo nvarchar(1000)
SET @auditInfo='Data Inserted'
INSERT INTO order_audit (order_id, customer_id,order_date,audit_information)
SELECT order_id,customer_id,order_date,@auditInfo
FROM inserted
END

INSERT INTO orders values(1001,31,'8-10-2024')
INSERT INTO orders values(1002,41,'8-8-2024')
update orders set customer_id=32 where order_id=1
update orders set customer_id=31 where order_id=1001

--Examp1e for After or For Trigger with update:
CREATE TRIGGER trgAfterUpdateOrder
ON Orders
FOR UPDATE
AS
BEGIN
DECLARE @auditInfo nvarchar(1000)
SET @auditInfo='Data changed'
INSERT INTO order_audit(order_id, customer_id,order_date,audit_information)
SELECT order_id,customer_id,order_date,@auditInfo
FROM inserted
END

UPDATE orders SET customer_id=33, order_date='10-10-2020'
WHERE order_id=1001;

UPDATE orders SET customer_id=32, order_date='10-10-2024'
WHERE order_id=1001;

INSERT INTO orders values(1003,40,'03-01-2024')

-- Example for Instead of Trigger:
CREATE VIEW vwEmployeeDetails
AS
SELECT Id,Name,Gender,DeptName from tblEmployee e
join tblDepartment d
on e.DepartmentId=d.DepartID

select * from vwEmployeeDetails

INSERT vwEmployeeDetails VALUES(7,'Tina','Female','HR')

CREATE TRIGGER tr_vmEmployeeDetails_InsteadofInsert
ON vwEmployeeDetails
INSTEAD OF INSERT
AS BEGIN
DECLARE @deptId int
SELECT @deptId=DepartID from tblDepartment
JOIN inserted
on inserted.DeptName=tblDepartment.DeptName

if(@deptId is null)
BEGIN
Raiserror('Invalid Department Name. Statement Terminated',16,1)
RETURN
END
INSERT INTO tblEmployee(Id,Name,Gender,DepartmentId)
SELECT Id,Name,Gender,@deptId
FROM inserted
END

INSERT INTO vwEmployeeDetails VALUES(7,'Tina','Female','HR')

INSERT INTO vwEmployeeDetails VALUES(8,'Yash','Male','Banking')


----------------------------------------------TRANSACTION-----------------------------------------------------
USE BikeStores

BEGIN TRANSACTION
INSERT INTO sales.orders(customer_id,order_status,order_date,required_date,shipped_date,store_id,staff_id)
VALUES (49,4,'20170228','20170301','20170302',2,6);
INSERT INTO sales.order_items(order_id,item_id,product_id,quantity,list_price,discount)
VALUES(1001,10,8,2,269.99,0.07);
IF @@ERROR=0
BEGIN
COMMIT TRANSACTION
PRINT 'Insertion Sucessful!...'
END
ELSE
BEGIN
ROLLBACK TRANSACTION
PRINT 'Something went wrong while insertion!!!'
END

SELECT * FROM production.products WHERE product_id=8



--------Day-2:CLASS EXERCISE-----------

------------------------------INDEXES:-------------------------------------
create database Training;
use Training;

CREATE TABLE Employee1
(Id INT,
Name VARCHAR(50),
Salary INT,
Gender VARCHAR(10),
City VARCHAR(50),
Dept VARCHAR(50)
)
select * from Employee1
INSERT INTO Employee1 VALUES (3, 'Pranaya',4500,'Male', 'New York', 'IT' ),
(1, 'anu',4500,'female', 'New York', 'IT' ),
(4, 'priya',3000,'female', 'New York', 'HR' ),
(5, 'sambit',43400,'Male', 'chennai', 'HR' ),
(7, 'preety',30500,'female', 'China', 'IT' ),
(6, 'tarun',4500,'Male', 'New York', 'IT' ),
(2, 'hina',2000,'female', 'mumbai', 'HR' ),
(8, 'john',400,'Male', 'delhi', 'IT' ),
(10, 'pam',4500,'Male', 'US', 'HR' ),
(9, 'sara',450,'female', 'New York', 'IT' );

SELECT * FROM Employee1 WHERE Id=8

CREATE INDEX IX_EMPLOYEE_ID
ON EMPLOYEE1(ID ASC)

CREATE CLUSTERED INDEX IX_EMPLOYEE_ID1
ON EMPLOYEE1(ID ASC)

DROP TABLE Employee1

CREATE TABLE Employee
(Id INT primary key,
Name VARCHAR(50),
Salary INT,
Gender VARCHAR(10),
City VARCHAR(50),
Dept VARCHAR(50)
)
INSERT INTO Employee VALUES (3, 'Pranaya',4500,'Male', 'Sydney', 'IT' ),
(1, 'anu',4500,'female', 'New York', 'IT' ),
(4, 'priya',3000,'female', 'London', 'HR' ),
(5, 'sambit',43400,'Male', 'Tokiyo', 'HR' ),
(7, 'preety',30500,'female', 'China', 'IT' ),
(6, 'tarun',4500,'Male', 'New York', 'IT' ),
(2, 'hina',2000,'female', 'mumbai', 'HR' ),
(8, 'john',400,'Male', 'delhi', 'IT' ),
(10, 'pam',4500,'Male', 'US', 'HR' ),
(9, 'sara',450,'female', 'New York', 'IT' );

truncate table Employee
select * from Employee

use BikeStores;
SELECT * FROM sales.customers
-- Example for Unique Index:
CREATE UNIQUE INDEX Idx_unique_email
ON sales.customers(email)

-- Clustered Index:
--We can create only one clustered index per table
--if we have primary key in a table automatically it will create clustered index for that table
--suppose when table is not having primary key then only we can create clustered index. while creating clustered index
--if we have duplicate and null values exists it will sort and store the data
use Training
CREATE CLUSTERED INDEX IX_EMPLOYEE_ID1
ON EMPLOYEE(ID ASC)

-- Non-Clustered Index:
-- we can create upto 999 non clustered index per table
use BikeStores
CREATE NONCLUSTERED INDEX IDX_NAME
ON sales.customers(first_name,last_name)
(or)
CREATE INDEX IDX_NAME1
ON sales.customers(first_name,last_name)

use Training
CREATE TABLE Department(
Id int, Name Varchar(100))

Insert into Department values(1,'HR'),(1,'Admin')

select* from Department;

create Clustered Index Idx_dept_id
ON Department(Id)

Insert into Department values(2,'IT'),(3,'Transport'),(2,'Information Tech')

Insert Into Department(Name) values('Insurance')


--SQL Script to create tblEmployee table:
CREATE TABLE tblEmployee
(
Id int primary key,
Name nvarchar(30),
Salary int,
Gender nvarchar(10),
DepartmentId int
)

--SQL Script to create tb1Department table:
CREATE TABLE tblDepartment(
DepartID INT PRIMARY KEY,
DeptName nvarchar(20)
)

--lnsert data into tb1Department table:
Insert into tblDepartment values (1,'IT')
Insert into tblDepartment values (2,'Payroll')
Insert into tblDepartment values (3,'HR')
Insert into tblDepartment values (4,'Admin')

--lnsert data into tb1Employee table:
Insert into tblEmployee values (1, 'John',5000,'male',3)
Insert into tblEmployee values (2, 'mike',5000,'male',2)
Insert into tblEmployee values (3, 'Pam',5000,'male',1)
Insert into tblEmployee values (4, 'Todd',5000,'male',4)
Insert into tblEmployee values (5, 'Sara',5000,'male',1)
Insert into tblEmployee values (6, 'Ben',5000,'male',3)

SELECT Id,Name,Salary, Gender,DeptName
from tblEmployee
join tblDepartment
on tblEmployee.DepartmentId=tblDepartment.DepartID;


--------------------------------------VIEW:------------------------------------------------------

--Now let's create a view, using the JOINS query, we have just written.
CREATE VIEW vWEmployeesByDepartment
AS
SELECT Id, Name,Salary,Gender,DeptName
from tblEmployee
join tblDepartment
on tblEmployee.DepartmentId=tblDepartment.DepartID

select * from vWEmployeesByDepartment
select * from tblEmployee
update vWEmployeesByDepartment set Name='Geetha' where Id=3

insert into vWEmployeesByDepartment values(7,'Ron',9000,'Male','IT')

--View that returns only IT department employees:
CREATE VIEW vWITDepartment_Employees
AS
SELECT Id, Name,Salary,Gender,DeptName
from tblEmployee
join tblDepartment
on tblEmployee.DepartmentId=tblDepartment.DepartID
where tblDepartment.DeptName='IT'

SELECT * FROM vWITDepartment_Employees

--View that returns all columns except Salary column:
CREATE VIEW vWEmployeesNonConfidentialData
as
Select Id, Name,Gender,DeptName
from tblEmployee
join tblDepartment
on tblEmployee.DepartmentId=tblDepartment.DepartID

select * from vWEmployeesNonConfidentialData

Create View vWEmployeesCountByDepartment
as
Select DeptName, COUNT(Id) as TotalEmployees
from tblEmployee
join tblDepartment
on tblEmployee.DepartmentId=tblDepartment.DepartID
Group by DeptName

select * from vWEmployeesCountByDepartment
sp_helptext vWEmployeesCountByDepartment

-----------------------------------------------TRIGGER:-----------------------------------------------
Create table orders(
order_id INT PRIMARY KEY,
customer_id INT,
order_date DATE
);

Create table order_audit(
audit_id INT IDENTITY PRIMARY KEY,
order_id INT,
customer_id INT,
order_date DATE,
audit_date DATETIME DEFAULT GETDATE()
);

ALTER TABLE order_Audit ADD audit_information varchar(max)

--Example for After or For Trigger with insert:
select * from orders;

select * from order_audit;

CREATE TRIGGER trgAfterInsertOrder
ON Orders
AFTER INSERT
AS
BEGIN
DECLARE  @auditInfo nvarchar(1000)
SET @auditInfo='Data Inserted'
INSERT INTO order_audit (order_id, customer_id,order_date,audit_information)
SELECT order_id,customer_id,order_date,@auditInfo
FROM inserted
END

INSERT INTO orders values(1001,31,'8-10-2024')
INSERT INTO orders values(1002,41,'8-8-2024')
update orders set customer_id=32 where order_id=1
update orders set customer_id=31 where order_id=1001

--Examp1e for After or For Trigger with update:
CREATE TRIGGER trgAfterUpdateOrder
ON Orders
FOR UPDATE
AS
BEGIN
DECLARE @auditInfo nvarchar(1000)
SET @auditInfo='Data changed'
INSERT INTO order_audit(order_id, customer_id,order_date,audit_information)
SELECT order_id,customer_id,order_date,@auditInfo
FROM inserted
END

UPDATE orders SET customer_id=33, order_date='10-10-2020'
WHERE order_id=1001;

UPDATE orders SET customer_id=32, order_date='10-10-2024'
WHERE order_id=1001;

INSERT INTO orders values(1003,40,'03-01-2024')

-- Example for Instead of Trigger:
CREATE VIEW vwEmployeeDetails
AS
SELECT Id,Name,Gender,DeptName from tblEmployee e
join tblDepartment d
on e.DepartmentId=d.DepartID

select * from vwEmployeeDetails

INSERT vwEmployeeDetails VALUES(7,'Tina','Female','HR')

CREATE TRIGGER tr_vmEmployeeDetails_InsteadofInsert
ON vwEmployeeDetails
INSTEAD OF INSERT
AS BEGIN
DECLARE @deptId int
SELECT @deptId=DepartID from tblDepartment
JOIN inserted
on inserted.DeptName=tblDepartment.DeptName

if(@deptId is null)
BEGIN
Raiserror('Invalid Department Name. Statement Terminated',16,1)
RETURN
END
INSERT INTO tblEmployee(Id,Name,Gender,DepartmentId)
SELECT Id,Name,Gender,@deptId
FROM inserted
END

INSERT INTO vwEmployeeDetails VALUES(7,'Tina','Female','HR')

INSERT INTO vwEmployeeDetails VALUES(8,'Yash','Male','Banking')


----------------------------------------------TRANSACTION-----------------------------------------------------
USE BikeStores

BEGIN TRANSACTION
INSERT INTO sales.orders(customer_id,order_status,order_date,required_date,shipped_date,store_id,staff_id)
VALUES (49,4,'20170228','20170301','20170302',2,6);
INSERT INTO sales.order_items(order_id,item_id,product_id,quantity,list_price,discount)
VALUES(1001,10,8,2,269.99,0.07);
IF @@ERROR=0
BEGIN
COMMIT TRANSACTION
PRINT 'Insertion Sucessful!...'
END
ELSE
BEGIN
ROLLBACK TRANSACTION
PRINT 'Something went wrong while insertion!!!'
END

SELECT * FROM production.products WHERE product_id=8


 a5f5607 (first commit)
