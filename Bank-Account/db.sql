Create Database Bank

Use Bank

Create Table UserReg(userId int primary key identity(100,1),
FullName varchar(20),
dob date,
cnicNum varchar(20) UNIQUE,
phoneNum varchar(20) UNIQUE,
email varchar(50) UNIQUE,
AccountType varchar(20) default 'current',
userName varchar(20) , 
password varchar(150),
otpCode varchar(20),
otpExpiry DATETime , 
agreeTerm BIT DEFAULT 0,
)

ALTER TABLE userReg
ADD isVerified BIT DEFAULT 0,
    loginAttempts INT DEFAULT 0,
    lockTime DATETIME NULL;
    

--agreeTerm 0:not agree , 1 agree

INSERT INTO UserReg(FullName , dob , cnicNum , phoneNum , email , AccountType , userName , password , otpCode , otpExpiry , agreeTerm)VALUES
('Biya Hafeez' , '2005-08-30' , '42101-4378274812' ,'03151089348', 'biya123@gmail.com' , 'current' , 'Sehar' , 'Biya123' , 12345 , '2026-01-06 23:59:09', 1 )

SELECT * FROM UserReg
Truncate table UserReg
DROP TABLE UserReg



create table Users(
userid int primary key identity(001,1) ,
username varchar(80),
Login_password varchar(60),
email varchar(50),
phone_num int,
status varchar (10)default 'active'
);

create table Accounts(
account_id int primary key identity (001,1),
users_id int,
account_num varchar(50) unique not null,
balance decimal (15,2)default 0,
status varchar(40) default 'active',
foreign key(users_id)references Users(userid) 
);


CREATE TABLE Transactions(
    TransactionID INT PRIMARY KEY IDENTITY,
    FromAccount VARCHAR(20),
    ToAccount VARCHAR(20),
    Amount DECIMAL(12,2),
    TransactionDate DATETIME DEFAULT GETDATE(),
    TransactionPassword varchar(160)
);

select * from Transactions
insert into Transactions(FromAccount,ToAccount,Amount,TransactionDate,TransactionPassword)
values('Acc1001','Acc1010',400,'2004-02-20','abc123'),
('Acc1002','Acc1020',3400,'2019-04-24','ser234'),
('Acc1003','Acc1030',500,'2020-10-09','vfd12');


INSERT INTO Users(username,Login_password,email,phone_num,status)
VALUES 
('user01','user123','user01@gmail.com',12345,'active'),
('user02','user456','user02@gmail.com',16546,'active'),
('user03','user789','user03@gmail.com',5435,'active');

INSERT INTO Accounts (account_num, Balance,status)
VALUES 
(1001,150000,'active'),
(1002,189000,'active'),
(1003,120000,'active');
select * from Transactions
select * from accounts
select * from users

SELECT * FROM Accounts WHERE account_id= 2;


create table Contact(
Id int primary key identity(1,1),
Name varchar(250),
Email varchar(250),
Phone varchar(50),
Message text
)
select*from Contact


drop table Contact