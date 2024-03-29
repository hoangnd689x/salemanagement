USE [master]
GO
/****** Object:  Database [BatTrangOnline]    Script Date: 04/01/2018 12:37:30 SA ******/
CREATE DATABASE [BatTrangOnline]

GO
USE [BatTrangOnline]
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](250) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[Roles] [nvarchar](250) NOT NULL,
	[Published] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[UserCreated] [nvarchar](500) NOT NULL,
	[UserModified] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 04/01/2018 12:37:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NULL,
	[CustomerName] [nvarchar](max) NOT NULL,
	[CustomerAddress] [nvarchar](max) NULL,
	[CustomerCity] [nvarchar](max) NULL,
	[CustomerPhone] [nvarchar](max) NULL,
	[CustomerEmail] [nvarchar](max) NOT NULL,
	[DiscountCode] [nvarchar](max) NULL,
	[TotalMoney] [money] NULL,
	[Status] [int] NULL,
	[DateCreate] [datetime] NULL,
	[DateUpdate] [datetime] NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeedBack]    Script Date: 04/01/2018 12:37:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedBack](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[DateCreate] [datetime] NULL,
	[Status] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_FeedBack] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 04/01/2018 12:37:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[AvatarImage] [nvarchar](200) NULL,
	[Thumbnail] [nvarchar](200) NULL,
	[ImageUrl] [nvarchar](200) NOT NULL,
	[Published] [int] NOT NULL,
	[IsAvatar] [bit] NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 04/01/2018 12:37:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Avatar] [nchar](50) NOT NULL,
	[DateCreate] [datetime] NULL,
	[DateUpdate] [datetime] NULL,
	[UserID] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Published] [int] NOT NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 04/01/2018 12:37:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductTypeID] [int] NOT NULL,
	[Code] [int] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Price] [money] NOT NULL,
	[ChangePrice] [money] NOT NULL,
	[Discount] [int] NOT NULL,
	[VAT] [int] NOT NULL,
	[Published] [int] NOT NULL,
	[PurchaseCount] [int] NOT NULL,
	[ViewCount] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductBill]    Script Date: 04/01/2018 12:37:33 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductBill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BillID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[Success] [int] NOT NULL,
 CONSTRAINT [PK_ProductBill] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 04/01/2018 12:37:33 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NULL,
	[ParentId] [int] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Published] [int] NOT NULL,
	[IconUrl] [nvarchar](500) NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RelatedProduct]    Script Date: 04/01/2018 12:37:33 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelatedProduct](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[RelatedProductID] [int] NOT NULL,
	[Published] [int] NOT NULL,
 CONSTRAINT [PK_RelatedProduct] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [Username], [Password], [Roles], [Published], [DateCreated], [DateModified], [UserCreated], [UserModified]) VALUES (1, N'admin', N'bcb15f821479b4d5772bd0ca866c00ad5f926e3580720659cc80d39c9d09802a', N'Admin', 1, CAST(N'2016-08-23T11:05:45.067' AS DateTime), CAST(N'2016-08-23T11:05:45.067' AS DateTime), N'admin', N'admin')
INSERT [dbo].[Account] ([Id], [Username], [Password], [Roles], [Published], [DateCreated], [DateModified], [UserCreated], [UserModified]) VALUES (2, N'hoangnd', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Admin', 1, CAST(N'2017-12-17T20:31:57.093' AS DateTime), CAST(N'2017-12-17T20:31:57.093' AS DateTime), N'admin', N'admin')
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([ID], [Code], [CustomerName], [CustomerAddress], [CustomerCity], [CustomerPhone], [CustomerEmail], [DiscountCode], [TotalMoney], [Status], [DateCreate], [DateUpdate], [Notes]) VALUES (3, 123, N'HOangnd', N'23 Lac Trung', N'Ha Noi', N'0985646384', N'hoangnd@vtc.vn', N'10', 120000.0000, 3, CAST(N'2017-12-30T17:07:58.830' AS DateTime), CAST(N'2017-12-30T17:07:58.830' AS DateTime), N'Giao nhanh truoc tet')
INSERT [dbo].[Bill] ([ID], [Code], [CustomerName], [CustomerAddress], [CustomerCity], [CustomerPhone], [CustomerEmail], [DiscountCode], [TotalMoney], [Status], [DateCreate], [DateUpdate], [Notes]) VALUES (4, NULL, N'Hoang Nguyen Duy', N'23 Lac Trung, Hai Ba Trung, Ha Noi', N'64', N'969786569', N'hoangnd689x@gmail.com', N'abc', 121.0000, 0, CAST(N'2018-01-04T00:35:18.957' AS DateTime), CAST(N'2018-01-04T00:35:18.957' AS DateTime), N'giao nhanh trong 3 ngày')
SET IDENTITY_INSERT [dbo].[Bill] OFF
SET IDENTITY_INSERT [dbo].[Image] ON 

INSERT [dbo].[Image] ([ID], [ProductId], [AvatarImage], [Thumbnail], [ImageUrl], [Published], [IsAvatar]) VALUES (2, 2, NULL, NULL, N'/Resources/Images/am-chen-su-men-co-boc-dong-bo-am-chen-vuot-boc-dong-so-2-ve-truc-lam-that-hien-22778-1.jpg', 1, 0)
INSERT [dbo].[Image] ([ID], [ProductId], [AvatarImage], [Thumbnail], [ImageUrl], [Published], [IsAvatar]) VALUES (3, 2, NULL, NULL, N'/Resources/Images/am-chen-su-men-co-boc-dong-bo-am-chen-vuot-boc-dong-so-2-ve-truc-lam-that-hien-22778-1.jpg', 1, 1)
INSERT [dbo].[Image] ([ID], [ProductId], [AvatarImage], [Thumbnail], [ImageUrl], [Published], [IsAvatar]) VALUES (4, 3, NULL, NULL, N'/Resources/Images/am-chen-men-ran-co-bo-am-chen-dang-nhat-men-ran-co-boc-dong-23179-1.jpg', 1, 0)
INSERT [dbo].[Image] ([ID], [ProductId], [AvatarImage], [Thumbnail], [ImageUrl], [Published], [IsAvatar]) VALUES (5, 3, NULL, NULL, N'/Resources/Images/am-chen-men-ran-co-bo-am-chen-dang-nhat-men-ran-co-boc-dong-23179-1.jpg', 1, 1)
INSERT [dbo].[Image] ([ID], [ProductId], [AvatarImage], [Thumbnail], [ImageUrl], [Published], [IsAvatar]) VALUES (6, 4, NULL, NULL, N'/Resources/Images/nam-ruou-nam-ruou-mini-rong-noi-men-ran-co-cao-25-cm-22834-1.jpg', 1, 0)
INSERT [dbo].[Image] ([ID], [ProductId], [AvatarImage], [Thumbnail], [ImageUrl], [Published], [IsAvatar]) VALUES (7, 4, NULL, NULL, N'/Resources/Images/nam-ruou-nam-ruou-mini-rong-noi-men-ran-co-cao-25-cm-22834-2.jpg', 1, 0)
INSERT [dbo].[Image] ([ID], [ProductId], [AvatarImage], [Thumbnail], [ImageUrl], [Published], [IsAvatar]) VALUES (8, 4, NULL, NULL, N'/Resources/Images/nam-ruou-nam-ruou-mini-rong-noi-men-ran-co-cao-25-cm-22834-1.jpg', 1, 1)
INSERT [dbo].[Image] ([ID], [ProductId], [AvatarImage], [Thumbnail], [ImageUrl], [Published], [IsAvatar]) VALUES (9, 5, NULL, NULL, N'/Resources/Images/nam-ruou-nam-ruou-mini-dap-noi-hoa-sen-men-ran-co-cao-23-cm-24515-1.jpg', 1, 0)
INSERT [dbo].[Image] ([ID], [ProductId], [AvatarImage], [Thumbnail], [ImageUrl], [Published], [IsAvatar]) VALUES (10, 5, NULL, NULL, N'/Resources/Images/nam-ruou-nam-ruou-mini-dap-noi-hoa-sen-men-ran-co-cao-23-cm-24515-1.jpg', 1, 1)
SET IDENTITY_INSERT [dbo].[Image] OFF
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([ID], [Title], [Content], [Avatar], [DateCreate], [DateUpdate], [UserID], [ProductId], [Published]) VALUES (1003, N'Tiêu cmn đề', N'<p><img src="https://ibb.co/jQFhUw" alt="m&ocirc; cmn tả ảnh" /></p>
<p>&nbsp;</p>
<p>Test nội dung n&agrave;y</p>
<p><strong>Test in đậm n&agrave;y !</strong></p>
<p>&nbsp;</p>
<p><em><strong>Test in vừa đậm vừa nghi&ecirc;ng n&agrave;y !</strong></em></p>', N'                                                  ', CAST(N'2018-01-02T10:30:30.673' AS DateTime), CAST(N'2018-01-02T10:30:30.673' AS DateTime), 0, 0, 1)
INSERT [dbo].[News] ([ID], [Title], [Content], [Avatar], [DateCreate], [DateUpdate], [UserID], [ProductId], [Published]) VALUES (1004, N'Tiêu cmn đề', N'<p>M&ocirc; tả&nbsp;<strong>chữ to</strong> rồi lại đến&nbsp;<em>chữ nghi&ecirc;ng</em> nh&eacute; !</p>
<p>&nbsp;</p>
<p>Test dấu c&aacute;ch fat !</p>', N'                                                  ', CAST(N'2018-01-02T10:57:01.083' AS DateTime), CAST(N'2018-01-02T10:57:01.083' AS DateTime), 0, 0, 1)
SET IDENTITY_INSERT [dbo].[News] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [ProductTypeID], [Code], [Name], [Description], [Price], [ChangePrice], [Discount], [VAT], [Published], [PurchaseCount], [ViewCount], [DateCreated], [DateModified]) VALUES (2, 10, 572426, N'Bộ ấm chén vuốt bọc đồng số 2 - vẽ trúc lâm thất hiền', N'Bộ ấm chén vuốt bọc đồng số 2 - vẽ trúc lâm thất hiền', 20.0000, 18.0000, 10, 6, 1, 0, 0, CAST(N'2017-12-30T17:05:49.357' AS DateTime), CAST(N'2018-01-03T21:30:50.757' AS DateTime))
INSERT [dbo].[Product] ([ID], [ProductTypeID], [Code], [Name], [Description], [Price], [ChangePrice], [Discount], [VAT], [Published], [PurchaseCount], [ViewCount], [DateCreated], [DateModified]) VALUES (3, 10, 305434, N'Bộ ấm chén dáng Nhật - men rạn cổ bọc đồng', N'Bộ ấm chén dáng Nhật - men rạn cổ bọc đồng', 40.0000, 38.0000, 5, 8, 1, 0, 0, CAST(N'2017-12-30T17:07:58.830' AS DateTime), CAST(N'2017-12-30T17:07:58.830' AS DateTime))
INSERT [dbo].[Product] ([ID], [ProductTypeID], [Code], [Name], [Description], [Price], [ChangePrice], [Discount], [VAT], [Published], [PurchaseCount], [ViewCount], [DateCreated], [DateModified]) VALUES (4, 11, 22928, N'Nậm rượu mini Rồng nổi - men rạn cổ - cao 25 cm', N'Nậm rượu mini Rồng nổi - men rạn cổ - cao 25 cm', 10.0000, 9.0000, 10, 10, 1, 0, 0, CAST(N'2017-12-30T17:10:28.903' AS DateTime), CAST(N'2017-12-30T17:10:28.903' AS DateTime))
INSERT [dbo].[Product] ([ID], [ProductTypeID], [Code], [Name], [Description], [Price], [ChangePrice], [Discount], [VAT], [Published], [PurchaseCount], [ViewCount], [DateCreated], [DateModified]) VALUES (5, 11, 457610, N'Nậm rượu mini đắp nổi hoa sen - men rạn cổ - cao 23 cm', N'Nậm rượu mini đắp nổi hoa sen - men rạn cổ - cao 23 cm', 10.0000, 9.0000, 10, 10, 1, 0, 0, CAST(N'2017-12-30T17:11:44.947' AS DateTime), CAST(N'2017-12-30T17:12:02.197' AS DateTime))
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[ProductBill] ON 

INSERT [dbo].[ProductBill] ([ID], [BillID], [ProductID], [Quantity], [UnitPrice], [Success]) VALUES (1002, 3, 2, 3, 120000.0000, 0)
INSERT [dbo].[ProductBill] ([ID], [BillID], [ProductID], [Quantity], [UnitPrice], [Success]) VALUES (1003, 3, 3, 1, 121000.0000, 0)
INSERT [dbo].[ProductBill] ([ID], [BillID], [ProductID], [Quantity], [UnitPrice], [Success]) VALUES (1004, 4, 2, 2, 36.0000, 0)
INSERT [dbo].[ProductBill] ([ID], [BillID], [ProductID], [Quantity], [UnitPrice], [Success]) VALUES (1005, 4, 3, 2, 76.0000, 0)
INSERT [dbo].[ProductBill] ([ID], [BillID], [ProductID], [Quantity], [UnitPrice], [Success]) VALUES (1006, 4, 5, 1, 9.0000, 0)
SET IDENTITY_INSERT [dbo].[ProductBill] OFF
SET IDENTITY_INSERT [dbo].[ProductType] ON 

INSERT [dbo].[ProductType] ([ID], [Code], [ParentId], [Name], [Description], [Published], [IconUrl]) VALUES (10, NULL, 0, N'Ấm chén Bát Tràng', N'Gốm sứ được làm từ các nghệ nhân Bát Tràng', 1, N'/Resources/Images/18718.png')
INSERT [dbo].[ProductType] ([ID], [Code], [ParentId], [Name], [Description], [Published], [IconUrl]) VALUES (11, NULL, 0, N'Đồ thờ cúng men rạn', N'Đồ thờ cúng làm từ men rạn', 1, N'/Resources/Images/18774.png')
INSERT [dbo].[ProductType] ([ID], [Code], [ParentId], [Name], [Description], [Published], [IconUrl]) VALUES (12, NULL, 10, N'Ấm chén bọc đồng', N'Ấm chén bọc đồng', 1, N'')
INSERT [dbo].[ProductType] ([ID], [Code], [ParentId], [Name], [Description], [Published], [IconUrl]) VALUES (13, NULL, 10, N'Ấm chén gốm', N'Ấm chén gốm', 1, N'')
INSERT [dbo].[ProductType] ([ID], [Code], [ParentId], [Name], [Description], [Published], [IconUrl]) VALUES (14, NULL, 11, N'Đèn dầu thờ', N'Đèn dầu thờ', 1, N'')
INSERT [dbo].[ProductType] ([ID], [Code], [ParentId], [Name], [Description], [Published], [IconUrl]) VALUES (15, NULL, 11, N'Đỉnh sứ', N'Đỉnh sứ', 1, N'')
SET IDENTITY_INSERT [dbo].[ProductType] OFF
SET IDENTITY_INSERT [dbo].[RelatedProduct] ON 

INSERT [dbo].[RelatedProduct] ([ID], [ProductID], [RelatedProductID], [Published]) VALUES (2, 2, 5, 1)
SET IDENTITY_INSERT [dbo].[RelatedProduct] OFF
USE [master]
GO
ALTER DATABASE [BatTrangOnline] SET  READ_WRITE 
GO
