Create Database RaoVat
Use RaoVat
GO


Create Table Users
(
	IDUser NVARCHAR(20)Not Null Primary Key,
	UserName NVARCHAR(20) Not null,
	PassWord NVARCHAR(16) Not null,
	Type NVARCHAR(8),
	DateCreate DATETIME,
	FullName NVARCHAR(30),
	Gender NVARCHAR(5),
	Birth DATETIME,
	Avatar VARBINARY(MAX),
	Address NVARCHAR(50),
	Phone NVARCHAR(10),
	Email NVARCHAR(50) Not Null,
	IdentityCard NVARCHAR(12),
	Balance INT,
	EmailComfirm BIT
	
	
)
Alter Table Users
Alter Column PassWord NVARCHAR(50)
go
Create Table Category
(
	IDCategory NVARCHAR(20)Not Null Primary Key,
	Name NVARCHAR(30) Not Null,
	ImageCate VARBINARY(MAX) Not Null,
)
Alter Table Category
Add ImageCate VARBINARY(MAX)
go
Create Table SubCategory
(
	IDSubCategory NVARCHAR(20)Not Null Primary Key,
	IDCategory NVARCHAR(20) Not Null,
	Name NVARCHAR(30) Not Null,
	ImageSubCate VARBINARY(MAX) Not Null,
	
	FOREIGN KEY (IDCategory) REFERENCES Category(IDCategory)
)
go
Create Table Brand
(
	IDBrand NVARCHAR(20)Not Null Primary Key,
	IDSubCategory NVARCHAR(20) Not Null,
	Name NVARCHAR(30) Not Null,
	ImageBrand VARBINARY(MAX) Not Null,
	
	FOREIGN KEY (IDSubCategory) REFERENCES SubCategory(IDSubCategory)
)
go
Create Table News
(
	IDNews NVARCHAR(20)Not Null Primary Key,
	IDUser NVARCHAR(20) Not Null,
	IDBrand NVARCHAR(20) Not Null,
	Name NVARCHAR(100) Not Null,
	Price Float Not Null,
	Time Datetime Not Null,
	Description NVARCHAR(MAX) Not Null,
	Address NVARCHAR(100) Not Null,
	Type BIT Not Null,
	Status TinyINT Not Null
	
	FOREIGN KEY (IDUser) REFERENCES Users(IDUser),
	FOREIGN KEY (IDBrand) REFERENCES Brand(IDBrand)
)

go
Create Table ImgNews
(
	IDImg NVARCHAR(10)Not Null Primary Key,
	IDNews NVARCHAR(20) Not Null,
	ImgURL NVARCHAR(MAX) Not Null,
	NameImage NVARCHAR(MAX) Not Null,
	
	FOREIGN KEY (IDNews) REFERENCES News(IDNews)
)
Alter Table ImgNews
Add NameImage NVARCHAR(MAX)
Create Table BoxSave
(
	IDBoxSave NVARCHAR(10)Not Null Primary Key,
	IDUser NVARCHAR(20) Not Null,
	IDNews NVARCHAR(20) Not Null,
	
	FOREIGN KEY (IDNews) REFERENCES News(IDNews),
	FOREIGN KEY (IDUser) REFERENCES Users(IDUser)
)
go
Create Table Reports
(
	IDReports NVARCHAR(10)Not Null Primary Key,
	IDNews NVARCHAR(20) Not Null,
	Content NVARCHAR(20) Not Null,
	TypeOfReport NVARCHAR(20) Not Null,
	Email NVARCHAR(20) Not Null,
	Name NVARCHAR(20) Not Null,
	Status BIT Not Null
	
	FOREIGN KEY (IDNews) REFERENCES News(IDNews),
)
Create Table RoomChat
(
	IDRoom NVARCHAR(10) Not Null Primary Key,
	IDUserA NVARCHAR(20) Not Null,
	IDUserB NVARCHAR(20) Not Null,
	status INT Not Null,
	Time Datetime Not Null,
	
	FOREIGN KEY (IDUserA) REFERENCES Users(IDUser),
	FOREIGN KEY (IDUserB) REFERENCES Users(IDUser)
)
go
Create Table Servicess
(
	IDService NVARCHAR(5)Not Null Primary Key,
	Name NVARCHAR(20) Not Null,
	Price NVARCHAR(20) Not Null,
	TotalTime NVARCHAR(20) Not Null,

)
go
Create Table HistoryService
(
	IDHistoryService NVARCHAR(10)Not Null Primary Key,
	IDUser NVARCHAR(20) Not Null,
	IDService NVARCHAR(5) Not Null,
	Time DateTime Not Null,

	
	FOREIGN KEY (IDUser) REFERENCES Users(IDUser),
	FOREIGN KEY (IDService) REFERENCES Servicess(IDService),
)
go
Create Table Message_S
(
	IDMessage NVARCHAR(10)Not Null Primary Key,
	IDRoom NVARCHAR(10) Not Null,
	Type Bit Not Null,
	Content NVARCHAR(MAX),
	Img VARBINARY(MAX),
	Time DateTime Not Null,
	
	FOREIGN KEY (IDRoom) REFERENCES RoomChat(IDRoom),
)
GO
CREATE VIEW dbo.vw_Function_Base
AS
SELECT substring(replace(convert(varchar(100), NEWID()), '-', ''), 1, 20) AS Rand_Value
GO
CREATE VIEW dbo.vw_Function_BaseImg
AS
SELECT substring(replace(convert(varchar(100), NEWID()), '-', ''), 1, 10) AS Rand_Value
GO
CREATE FUNCTION dbo.fn_getRandom_Value()
RETURNS VARCHAR(20)
AS
BEGIN
  DECLARE @Rand_Value VARCHAR(20);
SELECT @Rand_Value = Rand_Value
FROM vw_Function_Base

  RETURN @Rand_Value;
END
GO
CREATE FUNCTION dbo.fn_getRandom_ValueImg()
RETURNS VARCHAR(10)
AS
BEGIN
  DECLARE @Rand_Value VARCHAR(10);
SELECT @Rand_Value = Rand_Value
FROM vw_Function_BaseImg

  RETURN @Rand_Value;
END
GO
Alter Proc USP_Login
@UserName NVARCHAR(20),@PassWord NVARCHAR(50)
AS
Begin
	declare @count INT;
	declare @result BIT;
	Select  @count=COUNT(*) FROM dbo.Users WHERE UserName = @UserName AND PassWord =@PassWord
	
	IF(@count=0)
	BEGIN
		SET @result=0
		select @result
	END
	ELSE
	BEGIN
		SET @result=1
		select @result
	END
End
GO
Select  COUNT(*) FROM dbo.Users WHERE UserName = 'trilxag1001' AND PassWord ='ZJGXDZmctpv3ul6iQx0K1w=='
Go
 USP_Login 'trilxag1001','ZJGXDZmctpv3ul6iQx0K1w=='
GO
Alter Proc USP_CheckAccout
@UserName NVARCHAR(20)
AS
Begin
	declare @count INT;
	declare @result BIT;
	Select  @count=COUNT(*) FROM dbo.Users WHERE UserName = @UserName
	
	IF(@count=0)
	BEGIN
		SET @result=0
		select @result
	END
	ELSE
	BEGIN
		SET @result=1
		select @result
	END
End
GO
Alter Proc USP_GmailAddress
@Email NVARCHAR(50)
AS
Begin
	declare @count INT;
	declare @result BIT;
	Select  @count=COUNT(*) FROM dbo.Users WHERE Email = @Email
	
	IF(@count=0)
	BEGIN
		SET @result=0
		select @result
	END
	ELSE
	BEGIN
		SET @result=1
		select @result
	END
End
GO
Alter Proc USP_Register
@UserName NVARCHAR(20),@PassWord NVARCHAR(16),@Email NVARCHAR(50)
AS
Begin
	INSERT INTO Users(IDUser,UserName,PassWord,Email,FullName,Type,DateCreate,Birth,EmailComfirm)
	VALUES(dbo.fn_getRandom_Value(),@UserName,@PassWord,@Email,@UserName,N'User',GETDATE(),GETDATE(),0)
End

