-----CLASS EXERCISE & ASSIGNMENT-----

---Stored Procedure
-- Create stored procedure:
CREATE PROC uspDisplayMessage
AS
BEGIN
PRINT 'Welcome to .Net React'
END

-- Example for creating stored procedure:
CREATE PROC uspProductList
AS
BEGIN
select Product_name,list_price from production.products
order by product_name
END

EXEC uspProductList
-- To know about the uspProductList(stored procedure):
sp_help uspProductList

USE BikeStores
GO
-- To know about the what are all database have in this Machine:
EXEC sp_databases

-- Alter Procedure:
alter proc uspProductList
as
begin
select Product_name, model_year, liSt_price from
production.products order by list_price desc
end 
uspProductList

-- Stored procedure have two paramters are: input and output parameter
---Input parameters:
CREATE PROC uspFindProducts(@modelyear as int)
AS
BEGIN
SELECT * FROM production.products where model_year=@modelyear
END
uspFindProducts 2019

---Example 4 with multiple parameter:
CREATE PROC uspFindProductsbyRange(@minPrice decimal,@maxPrice decimal)
AS
BEGIN
SELECT * FROM production.products WHERE
list_price>=@minPrice  AND list_price<=@maxPrice
END;
uspFindProductsbyRange 100,3000

--using named parameter:
uspFindProductsbyRange
@maxPrice=12000,
@minPrice=5000

--Optional parameter:
CREATE PROC uspFindProductsByName(@minPrice as decimal=2000,@maxPrice as decimal,@name as varchar(max))
AS
BEGIN
SELECT * FROM production.products WHERE list_price>=@minPrice and list_price<=@maxPrice
and
product_name like '%'+@name+'%'
END;
uspFindProductsByName 100,1000,'Sun'

uspFindProductsByName @maxPrice=3000,@name='Trek'

-- Output parameter:
CREATE PROC uspFindProductCountByModelYear
(@modelyear int,@productCount int OUTPUT)
AS
BEGIN
SELECT Product_name,list_price from production.products
WHERE model_year =@modelyear

SELECT @productCount=@@ROWCOUNT
END

--DECLARE:
DECLARE @count int;

EXEC uspFindProductCountByModelYear @modelyear=2016,@productCount=@count OUT;;

SELECT @count as 'Number of Products Found';


-- Creating the 2 stored procedure and display it in single:
CREATE PROC usp_GetAllCustomers
AS
BEGIN
select * from sales.customers
END
usp_GetAllCustomers

CREATE PROC usp_GetcusotmerOrders
@customerId INT
AS
BEGIN
SELECT * FROM sales.orders WHERE customer_id=@customerId
END
usp_GetcusotmerOrders 1

CREATE PROC usp_GetCustomerData
(@customerId INT)
AS
BEGIN
EXEC usp_GetAllCustomers;
EXEC usp_GetcusotmerOrders @customerId;
END
exec usp_GetCustomerData 1

----------------------------------ASSIGNMENTS------------------------------------------------------------------------------------

---ASSIGNMENT 1:-
/*You need to create a stored procedure that retrieves a list of all customers who have purchased a specific product.
consider below tables Customers, Orders,Order items and Products Create a stored procedure,it should return a list of all customers who have purchased the specified product,
including customer details like CustomerlD, CustomerName, and PurchaseDate. The procedure should take a ProductlD as an input parameter.*/
CREATE PROCEDURE usp_GetCustomersByProduct
@ProductID INT
AS
BEGIN
SELECT c.customer_id AS CustomerID,c.first_name + ' ' + c.last_name AS CustomerName,o.order_date AS PurchaseDate
FROM sales.customers c
INNER JOIN sales.orders o ON c.customer_id = o.customer_id
INNER JOIN sales.order_items oi ON o.order_id = oi.order_id
INNER JOIN production.products p ON oi.product_id = p.product_id
WHERE p.product_id = @ProductID;
END

EXEC usp_GetCustomersByProduct @ProductID = 6


--ASSIGNMENT 2:-
create table Department (
ID int identity primary key,
Name nvarchar(100) not null)

-- inserting test data
insert into Department (Name)
values ('Human Resources'),('Finance'),('IT'),('Sales')


create table Employee (
ID int identity primary key,
Name nvarchar(100) not null,
Gender char(1) check (Gender in ('M', 'F')) not null,
DOB date not null,
DeptID int foreign key references Department(ID))

-- inserting test dataa
insert into Employee (Name, Gender, DOB, DeptID)
values 
('Rajesh', 'M', '2003-06-15', 1),
('Priya', 'F', '2000-08-22', 2),
('Anil', 'M', '2002-02-11', 3),
('Sita', 'F', '1999-10-10', 4),
('Amit', 'M', '2001-12-05', 2);



--a) Creating a procedure to update the Employee details based on Employee ID:
create proc UpdateEmployeeDetails
(@EmployeeID int,@Name nvarchar(100),@Gender char(1),@DOB date,@DeptID int)
as
begin
update Employee set Name = @Name,Gender = @Gender,DOB = @DOB,DeptID = @DeptID 
where ID = @EmployeeID;
end
UpdateEmployeeDetails @EmployeeID = 1, @Name = 'Rajesh Kumar', @Gender = 'M', @DOB = '1999-06-15', @DeptID = 1
select * from Employee

--b) Creating a procedure to get employee information by passing the employee gender and department ID:
create procedure GetEmployeeByGenderAndDept
(@Gender char,@DeptID int)
as
begin
select ID, Name, Gender, DOB, DeptID
from Employee
where Gender = @Gender and DeptID = @DeptID;
end
GetEmployeeByGenderAndDept @Gender = 'F', @DeptID = 2
select * from Employee

--c) Creating a procedure to get the count of employees based on Gender:
create procedure GetEmployeeCountByGender
(@Gender char)
as
begin
select count(*) as EmployeeCount
from Employee
where Gender = @Gender;
end
GetEmployeeCountByGender @Gender = 'F'

----------or(using output parameter)----------
create procedure usp_GetEmployeeCountByGender
(@Gender char(1),@EmployeeCount int output)
as
begin
select @EmployeeCount = count(*)
from Employee
where Gender = @Gender;
end


declare @Count int;

exec usp_GetEmployeeCountByGender @Gender = 'F', @EmployeeCount = @Count output;

select @Count as EmployeeCount;



---USER DEFINED(USD) Function
--Scalar valued function
Create Function GetAllProducts()
RETURNS INT
AS
BEGIN
RETURN (SELECT COUNT(*) from production. products)
END
PRINT dbo.GetAllProducts()


---Table Valued Function
--Inline Table valued Function ===> It's contain only single select statemnet
Create function GetProductById(@productId int)
Returns Table
as
RETURN (Select * from production. products where product_id=@productId)

select * from GetProductById(4)

--Multi-Statement Table Valued Function
CREATE TABLE Departments(
ID INT PRIMARY KEY,
DepartmentName VARCHAR(50))
GO
--Populating the department table with test data
INSERT INTO Departments VALUES(1,'IT')
INSERT INTO Departments VALUES(2,'HR')
INSERT INTO Departments VALUES(3,'SALES')

CREATE FUNCTION ILTVF_GetEmployee()
RETURNS TABLE
AS 
RETURN(SELECT ID,Name,Cast(DOB as Date) as DOB FROM Employee)

CREATE FUNCTION MSTVF_GetEmployee()
RETURNS @TempTable Table (ID int, Name varchar(50),DOB Date)
BEGIN
INSERT INTO @TempTable
SELECT ID, Name, Cast(DOB as Date) FROM Employee
Return
END

select * from MSTVF_GetEmployee()
select * from ILTVF_GetEmployee()

update ILTVF_GetEmployee() set name ='Riya' where ID=2
update MSTVF_GetEmployee() set name ='Tina' where ID=2 -- Cannot update because it is taking the data from Temp Table


--ASSIGNMENT 3:-
/*Create a user Defined function to calculate the TotalPrice based on 
productid and Quantity Products Table*/

create table Products (
ProductID int identity primary key,
ProductName nvarchar(100) not null,
Price decimal(10, 2) not null
)

-- Inserting test data
insert into Products (ProductName, Price)
values('Laptop', 50000.00),
('Mobile Phone', 15000.00),
('Headphones', 2000.00),
('Smartwatch', 5000.00),
('Tablet', 25000.00)


create table Customers (
CustomerID int identity primary key,
CustomerName nvarchar(100) not null,
Email nvarchar(100) not null unique,
Phone nvarchar(15) not null,
Address nvarchar(255) not null
)

-- Inserting test data
insert into Customers (CustomerName, Email, Phone, Address)
values
('Rahul', 'rahul@gmail.com', '9876543210', '123, MG Road, Chennai'),
('Sita', 'sita@gmail.com', '1234567890', '321, Indiranagar, Bangalore, Karnataka'),
('Kiran', 'kiran@gmail.com', '2345678901', '654, Koramangala, Mumbai')

create table Orders (
OrderID int identity primary key,
CustomerID int not null,
OrderDate date not null,
foreign key (CustomerID) references Customers(CustomerID)
)
insert into Orders (CustomerID, OrderDate)
values
(1, '2024-10-01'),  
(2, '2024-10-02'),  
(1, '2024-10-03');  


create table Order_Items (
OrderItemID int identity primary key,
OrderID int not null,
ProductID int not null,
Quantity int not null,
foreign key (OrderID) references Orders(OrderID),
foreign key (ProductID) references Products(ProductID)
);

insert into Order_Items (OrderID, ProductID, Quantity)
values
(1, 1, 1),  
(1, 2, 2),  
(2, 3, 1),  
(3, 4, 1);  



---3. User-Defined Function to Calculate Total Price:
create function calculate_total_price
(@productid int,@quantity int)
returns decimal(10, 2)
as
begin
return (select price * @quantity from products where productid = @productid);
end;

select dbo.calculate_total_price(1, 2); 
select * from Products;

-- ASSIGNMENT 4:-
/*Create a function that returns all orders for a specific customer, including details such as OrderlD, OrderDate,
and the total amount of each order.*/

---4.User-Defined Function to Return All Orders for a Specific Customer:
create function get_orders_by_customer
(@customerid int)
returns table
as
return
(select o.orderid, o.orderdate, sum(oi.quantity * p.price) as total_amount
from orders o
join order_items oi on o.orderid = oi.orderid
join products p on oi.productid = p.productid
where o.customerid = @customerid
group by o.orderid, o.orderdate
)

select * from dbo.get_orders_by_customer(1); 

--ASSIGNMENT 5:-
/*Create a Multistatement table valued function that calculates the total sales for each product,
considering quantity and price.*/

---5. Multi-Statement Table-Valued Function to Calculate Total Sales for Each Product:
create function CalculateTotalSales()
returns @SalesTable table (ProductID int,ProductName nvarchar(100),TotalSales decimal(10, 2))
as
begin
insert into @SalesTable (ProductID, ProductName, TotalSales)
select p.ProductID,p.ProductName,sum(oi.Quantity * p.Price) as TotalSales
from Products p
left join Order_Items oi on p.ProductID = oi.ProductID
group by p.ProductID, p.ProductName;
return;
end;

select * from CalculateTotalSales();


--ASSIGNMENT 6:-
/*Create a multi-statement table-valued function that lists all customers along with the
total amount they have spent on orders.*/

---6. Multi-Statement Table-Valued Function to List All Customers with Total Amount Spent:
create function GetCustomerSpending()
returns @CustomerSpendingTable table (CustomerID int,CustomerName nvarchar(100),TotalSpent decimal(10, 2))
as
begin
insert into @CustomerSpendingTable (CustomerID, CustomerName, TotalSpent)
select c.CustomerID,c.CustomerName,isnull(sum(oi.Quantity * p.Price), 0) as TotalSpent
from Customers c
left join Orders o on c.CustomerID = o.CustomerID
left join Order_Items oi on o.OrderID = oi.OrderID
left join Products p on oi.ProductID = p.ProductID
group by c.CustomerID, c.CustomerName;
return;
end;

select * from GetCustomerSpending();

select * from Order_Items
select * from Orders
select * from Products