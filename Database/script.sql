USE [master]
GO
/****** Object:  Database [Municipality_Db]    Script Date: 5/26/2022 7:37:35 AM ******/
CREATE DATABASE [Municipality_Db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Municipality_Db', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Municipality_Db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Municipality_Db_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Municipality_Db_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Municipality_Db] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Municipality_Db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Municipality_Db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Municipality_Db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Municipality_Db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Municipality_Db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Municipality_Db] SET ARITHABORT OFF 
GO
ALTER DATABASE [Municipality_Db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Municipality_Db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Municipality_Db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Municipality_Db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Municipality_Db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Municipality_Db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Municipality_Db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Municipality_Db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Municipality_Db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Municipality_Db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Municipality_Db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Municipality_Db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Municipality_Db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Municipality_Db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Municipality_Db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Municipality_Db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Municipality_Db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Municipality_Db] SET RECOVERY FULL 
GO
ALTER DATABASE [Municipality_Db] SET  MULTI_USER 
GO
ALTER DATABASE [Municipality_Db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Municipality_Db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Municipality_Db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Municipality_Db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Municipality_Db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Municipality_Db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Municipality_Db', N'ON'
GO
ALTER DATABASE [Municipality_Db] SET QUERY_STORE = OFF
GO
USE [Municipality_Db]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 5/26/2022 7:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillsPayment]    Script Date: 5/26/2022 7:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillsPayment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BillId] [bigint] NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[Price] [float] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_BillsPayment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 5/26/2022 7:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[NationalCode] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[ExpireDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Customer]
GO
ALTER TABLE [dbo].[BillsPayment]  WITH CHECK ADD  CONSTRAINT [FK_BillsPayment_Bill] FOREIGN KEY([BillId])
REFERENCES [dbo].[Bill] ([Id])
GO
ALTER TABLE [dbo].[BillsPayment] CHECK CONSTRAINT [FK_BillsPayment_Bill]
GO
/****** Object:  StoredProcedure [dbo].[showminmaxpayment]    Script Date: 5/26/2022 7:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[showminmaxpayment]
(
@cid bigint
)
as
begin
select * from(Select s.ispaid, 
 max(s.Price) OVER (PARTITION by s.ispaid) as maxpaid,
 min(s.Price) OVER (PARTITION by s.ispaid) as minpaid
From Bill b
JOIN BillsPayment s on s.BillId =  b.id
where b.CustomerId = @cid)svg
group by svg.ispaid,svg.maxpaid,svg.minpaid
having COUNT(*) >1
end
GO
/****** Object:  StoredProcedure [dbo].[SP_changeExpireDate]    Script Date: 5/26/2022 7:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_changeExpireDate]
(

 @id bigint
 , @newdate datetime 
)
as
begin 
declare @date datetime , @crid bigint 
declare cr cursor
for (
select [Id] , [ExpireDate] from customer 
)
for update of  [ExpireDate] 
open cr
fetch next from cr into @crid , @date
	while(@@FETCH_STATUS = 0)
		begin		
			update Customer 
			set ExpireDate = @newdate
			where id = @id and @crid = @id
			fetch next from cr into @crid , @date
		end
close cr
deallocate cr
end
GO
/****** Object:  StoredProcedure [dbo].[SP_TFpivot]    Script Date: 5/26/2022 7:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_TFpivot]
(
	@cid int
)
as
begin
select * from (
Select s.ispaid , b.id 
From Bill b
JOIN BillsPayment s on s.BillId =  b.id
where b.CustomerId = @cid
) pvt

pivot (
count(id)
for ispaid in ([1] , [0])
)as pt
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_TotalPrice]    Script Date: 5/26/2022 7:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Sp_TotalPrice]
as
declare @sum float , @num float
declare cr cursor for
(
select price from bill
)
set @sum = 0
open cr 
fetch next from cr into @num
while(@@FETCH_STATUS = 0)
begin
set @sum = (@num + @sum)
fetch next from cr into @num
end
close cr
deallocate cr
print (@sum)
GO
USE [master]
GO
ALTER DATABASE [Municipality_Db] SET  READ_WRITE 
GO
