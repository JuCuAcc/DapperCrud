Use master 
CREATE Database DapperDB
GO 

Use DapperDB
GO

CREATE Table Hero(
	Id int primary key identity,
	Name varchar(100) not null,
	FirstName varchar(100) not null,
	LastName varchar(100) not null,
	Place varchar(100) not null,
);
GO

INSERT INTO  Hero Values('Jashim Uddin', 'Jashim', 'Uddin', 'CTG');
GO

SELECT * FROM Hero
GO