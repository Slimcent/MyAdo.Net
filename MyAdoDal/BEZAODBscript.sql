
CREATE TABLE [dbo].[Users]
(
[Id] INT IDENTITY(1, 1) NOT NULL ,
[Name] NVARCHAR(50) NOT NULL ,
[Email] NVARCHAR(50) NOT NULL ,

PRIMARY KEY CLUSTERED ( [Id] ASC )
);

CREATE TABLE [dbo].[RollCheck] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    [Email] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Accounts]
(
[Id] INT IDENTITY(1, 1) NOT NULL ,
[UserId] INT  NOT NULL ,
[Account_Number] INT NOT NULL ,
[Balance] Decimal(38,2) NOT NULL ,

PRIMARY KEY CLUSTERED ( [Id] ASC ),
CONSTRAINT [FK_Accounts_Users] 
   FOREIGN KEY (UserId) REFERENCES [Users]([Id])
      ON DELETE CASCADE
);

CREATE TABLE [dbo].[Transactions]
(
[Id] INT IDENTITY(1, 1) NOT NULL ,
[UserId] INT  NOT NULL ,
[Mode] NVARCHAR(10) NOT NULL ,
[Amount] Decimal(38,2) NOT NULL ,
[Time] DATETIME NOT NULL,

PRIMARY KEY CLUSTERED ( [Id] ASC ),
CONSTRAINT [FK_Transactions_Users] 
   FOREIGN KEY (UserId) REFERENCES [Users]([Id])
      ON DELETE CASCADE
);

Go

CREATE PROCEDURE GetName
@Id int,
@name char(50) output
AS
SELECT @name = Name from Users where Id = @Id

Go

CREATE PROCEDURE [dbo].[InsertIntoAccounts]
	@userID int,
	@accountNumber Varchar(50),
	@balance Varchar(50)
AS
BEGIN
	INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (@userID, @accountNumber, @balance)
END

Go

CREATE PROCEDURE [dbo].[InsertIntoRollCheck]
	@name Varchar(50),
	@email Varchar(50)
AS
BEGIN
	INSERT INTO RollCheck (Name, Email) VALUES (@name, @email)
END

Go

CREATE PROCEDURE [dbo].[InsertIntoUsers]
	@name Varchar(50),
	@email Varchar(50)
AS
BEGIN
	INSERT INTO Users (Name, Email) VALUES (@name, @email)
END

Go

INSERT INTO Users (Name, Email) VALUES ('Tochukwu', 'tochukwu@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Shola', 'shola.nejo@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Chikky', 'chikky@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Dara', 'dara@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Alex', 'alex@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Uriel', 'Uriel@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Kachi', 'kachi@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Chinedu', 'chinedu@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Loveth', 'loveth@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Sunday', 'sunday@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Sammy', 'sammy@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Einstien', 'einstien@domain.com')
INSERT INTO Users (Name, Email) VALUES ('KCM', 'kcm@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Obinna', 'obi@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Gideon', 'Giddy@domain.com')
INSERT INTO Users (Name, Email) VALUES ('Francis', 'SorrySir@domain.com')
 

 
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (1,0293842983,1008900.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (2,0293842653,1008700.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (3,0293840983,1000900.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (4,0293847683,1008700.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (5,0293845683,1006700.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (6,0293844283,1670000.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (7,0293982983,1340000.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (8,0293112983,1002300.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (9,0293672983,1009800.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (10,0290942983,1067000.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (11,0293898983,1076000.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (12,0293887983,1087000.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (13,0293800983,1009000.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (14,0293812983,1087000.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (15,0293854983,1034000.99)
INSERT INTO Accounts (UserId, Account_Number, Balance) VALUES (16,0290954983,1034670.99)
