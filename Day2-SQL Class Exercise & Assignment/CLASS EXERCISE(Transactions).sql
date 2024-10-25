 HEAD
Create database Transactions_db
use Transactions_db
Create table CUSTOMERS
(customer_id int primary key,
Name varchar(100),
active bit)


Create database Transactions_db
use Transactions_db
Create table CUSTOMERS
(customer_id int primary key,
Name varchar(100),
active bit)

 a5f5607 (first commit)
CREATE TABLE orders
(order_id int primary key,
customer_id int foreign key references Customers (customer_id), 
order_status varchar(100))

insert into CUSTOMERS values(1, 'Pam', 1),(2, 'Kim', 1)
insert into orders values (101,1, 'Pending'),(102,2, 'Pending')

select * from CUSTOMERS
select�* from�orders

--Transaction A
HEAD
BEGIN TRANSACTION
UPDATE CUSTOMERS SET Name='John'
WHERE customer_id=1

WAITFOR DELAY '00:00:05';
UPDATE orders SET order_status=' Processed'
WHERE order_id=101

COMMIT TRANSACTION


-- Transaction B
BEGIN TRANSACTION
UPDATE orders SET order_status= 'Shipped'
WHERE order_id=101

WAITFOR DELAY '00:00:05';
UPDATE CUSTOMERS SET Name='Geetha'
WHERE customer_id=1

COMMIT TRANSACTION

BEGIN TRANSACTION
UPDATE CUSTOMERS SET Name='John'
WHERE customer_id=1

WAITFOR DELAY '00:00:05';
UPDATE orders SET order_status=' Processed'
WHERE order_id=101

COMMIT TRANSACTION


-- Transaction B
BEGIN TRANSACTION
UPDATE orders SET order_status= 'Shipped'
WHERE order_id=101

WAITFOR DELAY '00:00:05';
UPDATE CUSTOMERS SET Name='Geetha'
WHERE customer_id=1

COMMIT TRANSACTION
 a5f5607 (first commit)
