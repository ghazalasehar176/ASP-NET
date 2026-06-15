Create Database BookStore

use BookStore

Create Table Books(
bookId int primary key identity (100 , 1),
title varchar (20),
author varchar(20), 
price int,
category varchar(20) , 
stock int , 
imageUrl varchar(20)
)
 


INSERT INTO Books (title, author, price, category, stock, imageUrl)
VALUES 

 
Create Table users (
userId int primary key identity(200 , 1),
name varchar(20),
email varchar(100),
passwords varchar(50),
)

select email , passwords from users

ALTER TABLE users ADD CONSTRAINT UQ_email UNIQUE(email);
alter table users drop column role
Alter table users alter column passwords varchar(255)
ALTER TABLE users ADD role VARCHAR(20) DEFAULT 'User'

Update users set role = 'Admin' Where userId  = 200

Create table Orders (
OrderId int Primary key identity(400,1),
names varchar(100),
Email NVARCHAR(100),
TotalAmount int,
OrderDate DateTime DEFAULT GetDate()
);
Alter table Orders Add names varchar(255)  

Drop Table Orders
Drop Table orderItems
 

select * from Books


select * from users


select * from Orders
select *  from orderItems

 

Truncate table users
Truncate table orderItems


Create table orderItems (
id int primary key identity(300,1),
OrderId int,
Bookid int , 
Quantity int , 
Price int
)