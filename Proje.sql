USE [master]
GO
/****** Object:  Database [Kutuphane_db]    Script Date: 15.12.2019 22:17:17 ******/
CREATE DATABASE [Kutuphane_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Kutuphane_db', FILENAME = N'C:\Users\Mehmet Hanedar\Kutuphane_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Kutuphane_db_log', FILENAME = N'C:\Users\Mehmet Hanedar\Kutuphane_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Kutuphane_db] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Kutuphane_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Kutuphane_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Kutuphane_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Kutuphane_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Kutuphane_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Kutuphane_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [Kutuphane_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Kutuphane_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Kutuphane_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Kutuphane_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Kutuphane_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Kutuphane_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Kutuphane_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Kutuphane_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Kutuphane_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Kutuphane_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Kutuphane_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Kutuphane_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Kutuphane_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Kutuphane_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Kutuphane_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Kutuphane_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Kutuphane_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Kutuphane_db] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Kutuphane_db] SET  MULTI_USER 
GO
ALTER DATABASE [Kutuphane_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Kutuphane_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Kutuphane_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Kutuphane_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Kutuphane_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Kutuphane_db] SET QUERY_STORE = OFF
GO
USE [Kutuphane_db]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Kutuphane_db]
GO
/****** Object:  UserDefinedFunction [dbo].[buyukharf]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[buyukharf](@gelendeger varchar(50))
returns varchar(50)
as
begin
return upper(@gelendeger)
end
GO
/****** Object:  Table [dbo].[Uye]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Uye](
	[kullaniciID] [int] NOT NULL,
	[kayitTarihi] [date] NOT NULL,
	[okuduguKitapSayisi] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[kullaniciID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[KitapOkuyanlar]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[KitapOkuyanlar]()
returns table
as
return (SELECT * FROM Uye where okuduguKitapSayisi>0)
GO
/****** Object:  UserDefinedFunction [dbo].[KitapOkuyamayanlar]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[KitapOkuyamayanlar]()
returns table
as
return (SELECT * FROM Uye where okuduguKitapSayisi=0)
GO
/****** Object:  Table [dbo].[Kitap]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kitap](
	[kitapID] [int] IDENTITY(1,1) NOT NULL,
	[isbn] [varchar](50) NOT NULL,
	[kitapAdi] [varchar](50) NOT NULL,
	[yayinEviAdi] [varchar](50) NOT NULL,
	[yazarAdi] [varchar](50) NOT NULL,
	[stokSayisi] [int] NOT NULL,
	[basimTarihi] [date] NOT NULL,
	[ciltNo] [int] NOT NULL,
	[kategoriID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[kitapID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[DüsükStok]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[DüsükStok]()
returns table
as
return (SELECT * FROM Kitap where stokSayisi<5)
GO
/****** Object:  Table [dbo].[Bilgisayar]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bilgisayar](
	[BilgisayarID] [int] IDENTITY(1,1) NOT NULL,
	[marka] [varchar](50) NOT NULL,
	[model] [varchar](50) NOT NULL,
	[stokSayisi] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BilgisayarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[DüsükStokPc]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[DüsükStokPc]()
returns table
as
return (SELECT * FROM Bilgisayar where stokSayisi<5)
GO
/****** Object:  Table [dbo].[BilgisayarKayit]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BilgisayarKayit](
	[bilgisayarKayitID] [int] IDENTITY(1,1) NOT NULL,
	[bilgisayarID] [int] NOT NULL,
	[kullaniciID] [int] NOT NULL,
	[alisTarihi] [date] NOT NULL,
	[verisTarihi] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[bilgisayarKayitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Il]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Il](
	[ilID] [int] NOT NULL,
	[ilAdi] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ilID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ilce]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ilce](
	[ilceID] [int] NOT NULL,
	[ilceAdi] [varchar](20) NOT NULL,
	[ilID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ilceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IletisimBilgileri]    Script Date: 15.12.2019 22:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IletisimBilgileri](
	[iletisimID] [int] IDENTITY(1,1) NOT NULL,
	[telefon] [varchar](20) NOT NULL,
	[adres] [varchar](100) NOT NULL,
	[eposta] [varchar](50) NOT NULL,
	[ilID] [int] NOT NULL,
	[ilceID] [int] NULL,
	[okulID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[iletisimID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kategori]    Script Date: 15.12.2019 22:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategori](
	[kategoriID] [int] NOT NULL,
	[kategoriAdi] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[kategoriID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KitapKayit]    Script Date: 15.12.2019 22:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KitapKayit](
	[kitapKayitID] [int] IDENTITY(1,1) NOT NULL,
	[kullaniciID] [int] NOT NULL,
	[kitapID] [int] NOT NULL,
	[alisTarihi] [date] NOT NULL,
	[verisTarihi] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[kitapKayitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kullanici]    Script Date: 15.12.2019 22:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kullanici](
	[kullaniciID] [int] IDENTITY(1,1) NOT NULL,
	[kullaniciAdi] [varchar](20) NOT NULL,
	[sifre] [varchar](20) NOT NULL,
	[tcNo] [varchar](50) NOT NULL,
	[adSoyad] [varchar](50) NOT NULL,
	[cinsiyet] [varchar](50) NOT NULL,
	[dogumTarihi] [date] NOT NULL,
	[iletisimID] [int] NOT NULL,
	[kullaniciTur] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[kullaniciID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Okul]    Script Date: 15.12.2019 22:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Okul](
	[okulID] [int] NOT NULL,
	[okulAdi] [varchar](50) NOT NULL,
	[ilceID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[okulID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personel]    Script Date: 15.12.2019 22:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personel](
	[kullaniciID] [int] NOT NULL,
	[yetki] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Personel] PRIMARY KEY CLUSTERED 
(
	[kullaniciID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bilgisayar] ON 

INSERT [dbo].[Bilgisayar] ([BilgisayarID], [marka], [model], [stokSayisi]) VALUES (1, N'HP', N'Pavilion', 10)
INSERT [dbo].[Bilgisayar] ([BilgisayarID], [marka], [model], [stokSayisi]) VALUES (2, N'Dell', N'gtx1050', 4)
INSERT [dbo].[Bilgisayar] ([BilgisayarID], [marka], [model], [stokSayisi]) VALUES (1003, N'lenova', N'q150', 12)
SET IDENTITY_INSERT [dbo].[Bilgisayar] OFF
SET IDENTITY_INSERT [dbo].[BilgisayarKayit] ON 

INSERT [dbo].[BilgisayarKayit] ([bilgisayarKayitID], [bilgisayarID], [kullaniciID], [alisTarihi], [verisTarihi]) VALUES (2005, 1, 2018, CAST(N'2019-12-15' AS Date), CAST(N'2019-12-15' AS Date))
SET IDENTITY_INSERT [dbo].[BilgisayarKayit] OFF
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (1, N'Adana')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (2, N'Adiyaman')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (3, N'Afyonkarahisar')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (4, N'Agri')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (5, N'Amasya')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (6, N'Ankara')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (7, N'Antalya')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (8, N'Artvin')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (9, N'Aydin')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (10, N'Balikesir')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (11, N'Bilecik')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (12, N'Bingöl')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (13, N'Bitlis')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (34, N'Istanbul')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (53, N'Rize')
INSERT [dbo].[Il] ([ilID], [ilAdi]) VALUES (54, N'Sakarya')
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (1, N'Serdivan', 54)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (2, N'Arifiye', 54)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (3, N'Adapazari', 54)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (4, N'Kuzuluk', 54)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (5, N'Fatih', 34)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (6, N'Gaziosmanpasa', 34)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (7, N'Zeytinburnu', 34)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (8, N'Bebek', 34)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (9, N'Ortaköy', 34)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (10, N'Pazar', 53)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (11, N'Merkez', 53)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (12, N'Güneysu', 53)
INSERT [dbo].[Ilce] ([ilceID], [ilceAdi], [ilID]) VALUES (13, N'Gündogdu', 53)
SET IDENTITY_INSERT [dbo].[IletisimBilgileri] ON 

INSERT [dbo].[IletisimBilgileri] ([iletisimID], [telefon], [adres], [eposta], [ilID], [ilceID], [okulID]) VALUES (1, N'05368521453', N'Ali kusçu mah', N'mehmethanedar@hotmail.com', 34, NULL, NULL)
INSERT [dbo].[IletisimBilgileri] ([iletisimID], [telefon], [adres], [eposta], [ilID], [ilceID], [okulID]) VALUES (1014, N'05412584466', N'sakarya adresi', N'asdfasd@hotmail.com', 34, 6, 5)
INSERT [dbo].[IletisimBilgileri] ([iletisimID], [telefon], [adres], [eposta], [ilID], [ilceID], [okulID]) VALUES (1020, N'123', N'aderss', N'posta@homt.cm', 54, 1, 6)
INSERT [dbo].[IletisimBilgileri] ([iletisimID], [telefon], [adres], [eposta], [ilID], [ilceID], [okulID]) VALUES (2020, N'054131319653', N'SULTANBEYLI', N'ibrahim.crayz@gmail.com', 53, 12, NULL)
INSERT [dbo].[IletisimBilgileri] ([iletisimID], [telefon], [adres], [eposta], [ilID], [ilceID], [okulID]) VALUES (3020, N'0258', N'adres', N'eposta:)', 34, 9, 4)
INSERT [dbo].[IletisimBilgileri] ([iletisimID], [telefon], [adres], [eposta], [ilID], [ilceID], [okulID]) VALUES (4020, N'012', N'gurbet', N'eposta@gmailcom', 34, 9, NULL)
INSERT [dbo].[IletisimBilgileri] ([iletisimID], [telefon], [adres], [eposta], [ilID], [ilceID], [okulID]) VALUES (4021, N'987', N'ev', N'epostaadresi@gmail.com', 34, 7, NULL)
INSERT [dbo].[IletisimBilgileri] ([iletisimID], [telefon], [adres], [eposta], [ilID], [ilceID], [okulID]) VALUES (4022, N'025', N'istanbul no : 16', N'postaadersi', 34, 9, 4)
INSERT [dbo].[IletisimBilgileri] ([iletisimID], [telefon], [adres], [eposta], [ilID], [ilceID], [okulID]) VALUES (4023, N'0253425', N'istanbul no : 16', N'postaadersi', 34, 5, 1)
INSERT [dbo].[IletisimBilgileri] ([iletisimID], [telefon], [adres], [eposta], [ilID], [ilceID], [okulID]) VALUES (4024, N'4', N'sadf', N'dsf', 34, 7, 3)
SET IDENTITY_INSERT [dbo].[IletisimBilgileri] OFF
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAdi]) VALUES (1, N'Çocuk Kitaplari')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAdi]) VALUES (2, N'Edebiyat')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAdi]) VALUES (3, N'Egitim')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAdi]) VALUES (4, N'Ekonomi')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAdi]) VALUES (5, N'Felsefe')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAdi]) VALUES (6, N'Bilim - Mühendislik')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAdi]) VALUES (7, N'Gezi ve Rehber Kitaplari')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAdi]) VALUES (8, N'Hukuk')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAdi]) VALUES (9, N'Inanç Kitaplari')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAdi]) VALUES (10, N'Saglik')
SET IDENTITY_INSERT [dbo].[Kitap] ON 

INSERT [dbo].[Kitap] ([kitapID], [isbn], [kitapAdi], [yayinEviAdi], [yazarAdi], [stokSayisi], [basimTarihi], [ciltNo], [kategoriID]) VALUES (3, N'496415', N'Eylül', N'bilgievi', N'üsame', 18, CAST(N'2014-07-23' AS Date), 3, 9)
INSERT [dbo].[Kitap] ([kitapID], [isbn], [kitapAdi], [yayinEviAdi], [yazarAdi], [stokSayisi], [basimTarihi], [ciltNo], [kategoriID]) VALUES (4, N'21512', N'Çakallarin yurdu', N'kus yayin evi', N'yazar', 40, CAST(N'2009-10-20' AS Date), 12, 3)
INSERT [dbo].[Kitap] ([kitapID], [isbn], [kitapAdi], [yayinEviAdi], [yazarAdi], [stokSayisi], [basimTarihi], [ciltNo], [kategoriID]) VALUES (1003, N'654132', N'Dönüsüm', N'yayinevi', N'Franz Kafka', 150, CAST(N'2019-12-02' AS Date), 14, 5)
INSERT [dbo].[Kitap] ([kitapID], [isbn], [kitapAdi], [yayinEviAdi], [yazarAdi], [stokSayisi], [basimTarihi], [ciltNo], [kategoriID]) VALUES (1004, N'1215', N'Fareler ve Insanlar', N'gökkusagi', N'John Steinbeck', 100, CAST(N'2019-11-30' AS Date), 75, 3)
INSERT [dbo].[Kitap] ([kitapID], [isbn], [kitapAdi], [yayinEviAdi], [yazarAdi], [stokSayisi], [basimTarihi], [ciltNo], [kategoriID]) VALUES (1005, N'964', N'Bilinmeyen Adanin Öyküsü', N'yol', N'Jose Saramago', 200, CAST(N'2019-11-05' AS Date), 2, 9)
INSERT [dbo].[Kitap] ([kitapID], [isbn], [kitapAdi], [yayinEviAdi], [yazarAdi], [stokSayisi], [basimTarihi], [ciltNo], [kategoriID]) VALUES (1006, N'659', N'Küçük Prens', N'bilgievi', N'Antoine de Saint-Exupery', 49, CAST(N'2019-10-28' AS Date), 4, 5)
INSERT [dbo].[Kitap] ([kitapID], [isbn], [kitapAdi], [yayinEviAdi], [yazarAdi], [stokSayisi], [basimTarihi], [ciltNo], [kategoriID]) VALUES (1007, N'65921', N'Kürk Mantolu Madonna', N'yayinevi', N'sabahattin ali', 29, CAST(N'2019-10-01' AS Date), 5, 2)
SET IDENTITY_INSERT [dbo].[Kitap] OFF
SET IDENTITY_INSERT [dbo].[KitapKayit] ON 

INSERT [dbo].[KitapKayit] ([kitapKayitID], [kullaniciID], [kitapID], [alisTarihi], [verisTarihi]) VALUES (3002, 3018, 4, CAST(N'2019-12-15' AS Date), CAST(N'2019-12-15' AS Date))
INSERT [dbo].[KitapKayit] ([kitapKayitID], [kullaniciID], [kitapID], [alisTarihi], [verisTarihi]) VALUES (3003, 4020, 1007, CAST(N'2019-12-15' AS Date), CAST(N'2019-12-15' AS Date))
INSERT [dbo].[KitapKayit] ([kitapKayitID], [kullaniciID], [kitapID], [alisTarihi], [verisTarihi]) VALUES (3004, 1, 1006, CAST(N'2019-12-15' AS Date), CAST(N'2019-12-15' AS Date))
SET IDENTITY_INSERT [dbo].[KitapKayit] OFF
SET IDENTITY_INSERT [dbo].[Kullanici] ON 

INSERT [dbo].[Kullanici] ([kullaniciID], [kullaniciAdi], [sifre], [tcNo], [adSoyad], [cinsiyet], [dogumTarihi], [iletisimID], [kullaniciTur]) VALUES (1, N'hanedar', N'1453', N'28438914524', N'mehmet hanedar', N'erkek', CAST(N'1999-02-06' AS Date), 1, N'admin')
INSERT [dbo].[Kullanici] ([kullaniciID], [kullaniciAdi], [sifre], [tcNo], [adSoyad], [cinsiyet], [dogumTarihi], [iletisimID], [kullaniciTur]) VALUES (1011, N'okuyucu', N'1234', N'28435476214', N'okuyucusoyadi', N'Erkek', CAST(N'1995-06-02' AS Date), 1014, N'üye')
INSERT [dbo].[Kullanici] ([kullaniciID], [kullaniciAdi], [sifre], [tcNo], [adSoyad], [cinsiyet], [dogumTarihi], [iletisimID], [kullaniciTur]) VALUES (2018, N'HALIL', N'123', N'2486547120', N'HALILBO', N'Erkek', CAST(N'2019-12-02' AS Date), 2020, N'personel')
INSERT [dbo].[Kullanici] ([kullaniciID], [kullaniciAdi], [sifre], [tcNo], [adSoyad], [cinsiyet], [dogumTarihi], [iletisimID], [kullaniciTur]) VALUES (3018, N'ibo', N'1', N'654312', N'ibo', N'Bayan', CAST(N'1989-06-07' AS Date), 3020, N'üye')
INSERT [dbo].[Kullanici] ([kullaniciID], [kullaniciAdi], [sifre], [tcNo], [adSoyad], [cinsiyet], [dogumTarihi], [iletisimID], [kullaniciTur]) VALUES (4018, N'memo', N'123', N'284325', N'mehmet hanedarr', N'Erkek', CAST(N'1994-06-15' AS Date), 4020, N'personel')
INSERT [dbo].[Kullanici] ([kullaniciID], [kullaniciAdi], [sifre], [tcNo], [adSoyad], [cinsiyet], [dogumTarihi], [iletisimID], [kullaniciTur]) VALUES (4020, N'üye', N'123', N'984561', N'üye adi', N'Erkek', CAST(N'2004-06-09' AS Date), 4022, N'üye')
INSERT [dbo].[Kullanici] ([kullaniciID], [kullaniciAdi], [sifre], [tcNo], [adSoyad], [cinsiyet], [dogumTarihi], [iletisimID], [kullaniciTur]) VALUES (4022, N'asdf', N'22', N'234', N'sdfa', N'Erkek', CAST(N'2019-12-02' AS Date), 4024, N'üye')
SET IDENTITY_INSERT [dbo].[Kullanici] OFF
INSERT [dbo].[Okul] ([okulID], [okulAdi], [ilceID]) VALUES (1, N'Fatih kiz lisesi', 5)
INSERT [dbo].[Okul] ([okulID], [okulAdi], [ilceID]) VALUES (2, N'Fatih imamhatip lisesi', 5)
INSERT [dbo].[Okul] ([okulID], [okulAdi], [ilceID]) VALUES (3, N'Zeytinburnu anadolu lisesi', 7)
INSERT [dbo].[Okul] ([okulID], [okulAdi], [ilceID]) VALUES (4, N'Ortaköy lisesi', 9)
INSERT [dbo].[Okul] ([okulID], [okulAdi], [ilceID]) VALUES (5, N'Gaziosmanpasa anadolu lisesi', 6)
INSERT [dbo].[Okul] ([okulID], [okulAdi], [ilceID]) VALUES (6, N'Sakarya Üniversitesi', 1)
INSERT [dbo].[Okul] ([okulID], [okulAdi], [ilceID]) VALUES (7, N'Subü', 1)
INSERT [dbo].[Personel] ([kullaniciID], [yetki]) VALUES (1, N'admin')
INSERT [dbo].[Personel] ([kullaniciID], [yetki]) VALUES (2018, N'Personel')
INSERT [dbo].[Personel] ([kullaniciID], [yetki]) VALUES (4018, N'Personel')
INSERT [dbo].[Uye] ([kullaniciID], [kayitTarihi], [okuduguKitapSayisi]) VALUES (3018, CAST(N'2019-12-14' AS Date), 16)
INSERT [dbo].[Uye] ([kullaniciID], [kayitTarihi], [okuduguKitapSayisi]) VALUES (4020, CAST(N'2019-12-15' AS Date), 4)
INSERT [dbo].[Uye] ([kullaniciID], [kayitTarihi], [okuduguKitapSayisi]) VALUES (4022, CAST(N'2019-12-15' AS Date), 0)
ALTER TABLE [dbo].[BilgisayarKayit]  WITH CHECK ADD  CONSTRAINT [FK_BilgisayarKayit_Bilgisayar] FOREIGN KEY([bilgisayarID])
REFERENCES [dbo].[Bilgisayar] ([BilgisayarID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BilgisayarKayit] CHECK CONSTRAINT [FK_BilgisayarKayit_Bilgisayar]
GO
ALTER TABLE [dbo].[BilgisayarKayit]  WITH CHECK ADD  CONSTRAINT [FK_BilgisayarKayit_Kullanici] FOREIGN KEY([kullaniciID])
REFERENCES [dbo].[Kullanici] ([kullaniciID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BilgisayarKayit] CHECK CONSTRAINT [FK_BilgisayarKayit_Kullanici]
GO
ALTER TABLE [dbo].[Ilce]  WITH CHECK ADD  CONSTRAINT [FK_Ilce_Il] FOREIGN KEY([ilID])
REFERENCES [dbo].[Il] ([ilID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ilce] CHECK CONSTRAINT [FK_Ilce_Il]
GO
ALTER TABLE [dbo].[IletisimBilgileri]  WITH CHECK ADD  CONSTRAINT [FK_IletisimBilgileri_Il] FOREIGN KEY([ilID])
REFERENCES [dbo].[Il] ([ilID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IletisimBilgileri] CHECK CONSTRAINT [FK_IletisimBilgileri_Il]
GO
ALTER TABLE [dbo].[IletisimBilgileri]  WITH CHECK ADD  CONSTRAINT [FK_IletisimBilgileri_Ilce] FOREIGN KEY([ilceID])
REFERENCES [dbo].[Ilce] ([ilceID])
GO
ALTER TABLE [dbo].[IletisimBilgileri] CHECK CONSTRAINT [FK_IletisimBilgileri_Ilce]
GO
ALTER TABLE [dbo].[IletisimBilgileri]  WITH CHECK ADD  CONSTRAINT [FK_IletisimBilgileri_Okul] FOREIGN KEY([okulID])
REFERENCES [dbo].[Okul] ([okulID])
GO
ALTER TABLE [dbo].[IletisimBilgileri] CHECK CONSTRAINT [FK_IletisimBilgileri_Okul]
GO
ALTER TABLE [dbo].[Kitap]  WITH CHECK ADD  CONSTRAINT [FK_Kitap_Kategori] FOREIGN KEY([kategoriID])
REFERENCES [dbo].[Kategori] ([kategoriID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kitap] CHECK CONSTRAINT [FK_Kitap_Kategori]
GO
ALTER TABLE [dbo].[KitapKayit]  WITH CHECK ADD  CONSTRAINT [FK_KitapKayit_Kitap] FOREIGN KEY([kitapID])
REFERENCES [dbo].[Kitap] ([kitapID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KitapKayit] CHECK CONSTRAINT [FK_KitapKayit_Kitap]
GO
ALTER TABLE [dbo].[KitapKayit]  WITH CHECK ADD  CONSTRAINT [FK_KitapKayit_Kullanici] FOREIGN KEY([kullaniciID])
REFERENCES [dbo].[Kullanici] ([kullaniciID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KitapKayit] CHECK CONSTRAINT [FK_KitapKayit_Kullanici]
GO
ALTER TABLE [dbo].[Kullanici]  WITH CHECK ADD  CONSTRAINT [FK_Kullanici_IletisimBilgileri] FOREIGN KEY([iletisimID])
REFERENCES [dbo].[IletisimBilgileri] ([iletisimID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kullanici] CHECK CONSTRAINT [FK_Kullanici_IletisimBilgileri]
GO
ALTER TABLE [dbo].[Okul]  WITH CHECK ADD  CONSTRAINT [FK_Okul_Ilce] FOREIGN KEY([ilceID])
REFERENCES [dbo].[Ilce] ([ilceID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Okul] CHECK CONSTRAINT [FK_Okul_Ilce]
GO
ALTER TABLE [dbo].[Personel]  WITH CHECK ADD  CONSTRAINT [FK_Personel_Kullanici] FOREIGN KEY([kullaniciID])
REFERENCES [dbo].[Kullanici] ([kullaniciID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Personel] CHECK CONSTRAINT [FK_Personel_Kullanici]
GO
ALTER TABLE [dbo].[Uye]  WITH CHECK ADD  CONSTRAINT [FK_Uye_Kullanici] FOREIGN KEY([kullaniciID])
REFERENCES [dbo].[Kullanici] ([kullaniciID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Uye] CHECK CONSTRAINT [FK_Uye_Kullanici]
GO
/****** Object:  StoredProcedure [dbo].[bilgisayarEkle]    Script Date: 15.12.2019 22:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[bilgisayarEkle]
	@marka varchar(50),
	@model varchar(50),
	@stokSayisi int
as
Begin
	Insert Into Bilgisayar(marka,model,stokSayisi)
	Values (@marka,@model,@stokSayisi)
End
GO
/****** Object:  StoredProcedure [dbo].[bilgisayarUpdate]    Script Date: 15.12.2019 22:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc [dbo].[bilgisayarUpdate]
	@BilgisayarID int,
	@marka varchar(50),
	@model varchar(50),
	@stokSayisi int
as
begin
Update Bilgisayar
set marka=@marka, model=@model, stokSayisi=@stokSayisi
where BilgisayarID=@BilgisayarID
end
GO
USE [master]
GO
ALTER DATABASE [Kutuphane_db] SET  READ_WRITE 
GO
