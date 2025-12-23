drop database loginBased

Create Database LoginBased

Use LoginBased

Create table Users(Id int primary key identity(1 , 1) , Name varchar(30) , Email varchar(20) , password varchar(20) , Role varchar(20) DEFAULT 'User' )


Insert into Users(Name,Email,password,Role)Values('ali' , 'ali123@gmail.com' , 'ABC@#123' , 'Admin')
Select * from Users
