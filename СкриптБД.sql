/*    ==Параметры сценариев==

    Версия исходного сервера : SQL Server 2017 (14.0.1000)
    Выпуск исходного ядра СУБД : Выпуск Microsoft SQL Server Express Edition
    Тип исходного ядра СУБД : Изолированный SQL Server

    Версия целевого сервера : SQL Server 2017
    Выпуск целевого ядра СУБД : Выпуск Microsoft SQL Server Standard Edition
    Тип целевого ядра СУБД : Изолированный SQL Server
*/
USE [master]
GO
/****** Object:  Database [AirDB]    Script Date: 15.05.2020 0:48:19 ******/
CREATE DATABASE [AirDB]
GO
USE [AirDB]
GO
/****** Object:  Table [dbo].[Air]    Script Date: 15.05.2020 0:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Air](
	[AirID] [int] IDENTITY(1,1) NOT NULL,
	[AirName] [nvarchar](200) NOT NULL,
	[AirDescription] [nvarchar](max) NULL,
	[Photo] [nvarchar](50) NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_Air] PRIMARY KEY CLUSTERED 
(
	[AirID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirOrder]    Script Date: 15.05.2020 0:48:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirOrder](
	[AirOrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[AirID] [int] NOT NULL,
 CONSTRAINT [PK_AirOrder] PRIMARY KEY CLUSTERED 
(
	[AirOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 15.05.2020 0:48:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ClientID] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[Phone] [nvarchar](30) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 15.05.2020 0:48:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[StatusID] [int] NOT NULL,
	[ClientID] [int] NULL,
	[Address] [nvarchar](200) NULL,
	[TotalPrice] [float] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 15.05.2020 0:48:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[ServiceID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [nvarchar](100) NOT NULL,
	[ServicePrice] [float] NOT NULL,
	[ServiceDescription] [nvarchar](2000) NULL,
	[ServiceCategoryID] [int] NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceCategory]    Script Date: 15.05.2020 0:48:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCategory](
	[ServiceCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceCategoryName] [nvarchar](50) NULL,
 CONSTRAINT [PK_ServiceCategory] PRIMARY KEY CLUSTERED 
(
	[ServiceCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceOrder]    Script Date: 15.05.2020 0:48:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceOrder](
	[ServiceOrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ServiceID] [int] NULL,
 CONSTRAINT [PK_ServiceOrder] PRIMARY KEY CLUSTERED 
(
	[ServiceOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 15.05.2020 0:48:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NULL,
	[Color] [nvarchar](50) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Air] ON 

INSERT [dbo].[Air] ([AirID], [AirName], [AirDescription], [Photo], [Price]) VALUES (10, N'Настенная сплит-система Electrolux EACS-07HG2/N3', N'Тип кондиционера - настенная сплит-система, максимальная длина коммуникаций -15 м, класс энергопотребления – A, основные режимы - охлаждение / обогрев, максимальный воздушный поток - 7.83 куб. м/мин, мощность в режиме охлаждения - 2200 Вт, мощность в режиме обогрева - 2400 Вт, потребляемая мощность при обогреве - 664 Вт, потребляемая мощность при охлаждении - 685 Вт', N'el.png', 24000)
INSERT [dbo].[Air] ([AirID], [AirName], [AirDescription], [Photo], [Price]) VALUES (11, N'Настенная сплит-система Ballu BSD-09HN1', N'линейка: Lagoon, обогрев и охлаждение помещений до 26м², режим вентиляции, поддержания температуры, ночной, осушения воздуха, дезодорирующий фильтр, мощность охлаждения 2640 Вт / обогрева 2780 Вт, управление с пульта, энергоэффективность A класса, уровень шума внутреннего блока 26 дБ, тип дисплея: скрытый.', N'ballu.png', 18750)
INSERT [dbo].[Air] ([AirID], [AirName], [AirDescription], [Photo], [Price]) VALUES (12, N'Настенная сплит-система Roda RS-A09F/RU-A09F', N'обогрев и охлаждение помещений до 25м², режим вентиляции, поддержания температуры, ночной, осушения воздуха, мощность охлаждения 2650 Вт / обогрева 2750 Вт, управление с пульта, энергоэффективность A класса, уровень шума внутреннего блока 26 дБ', N'roda.png', 12999)
INSERT [dbo].[Air] ([AirID], [AirName], [AirDescription], [Photo], [Price]) VALUES (13, N'Настенная сплит-система Electrolux EACS/I-09HM/N3_15Y', N'линейка: Monaco Super DC Inverter, обогрев и охлаждение помещений до 25м², режим вентиляции, поддержания температуры, ночной, осушения воздуха, дезодорирующий фильтр, фильтр тонкой очистки, мощность охлаждения 2490 Вт / обогрева 2800 Вт, управление с пульта, энергоэффективность A класса, уровень шума внутреннего блока 23 дБ, тип дисплея: обычный.', N'el2.png', 36200)
INSERT [dbo].[Air] ([AirID], [AirName], [AirDescription], [Photo], [Price]) VALUES (14, N'Мобильный кондиционер Ballu BPAC-09 CM', N'линейка: Smart Mechanic, только охлаждение, режим вентиляции, мощность охлаждения 2638 Вт, энергоэффективность A класса, уровень шума 45 дБ - 51 дБ.', N'np.png', 16999)
SET IDENTITY_INSERT [dbo].[Air] OFF
SET IDENTITY_INSERT [dbo].[AirOrder] ON 

INSERT [dbo].[AirOrder] ([AirOrderID], [OrderID], [AirID]) VALUES (28, 19, 12)
INSERT [dbo].[AirOrder] ([AirOrderID], [OrderID], [AirID]) VALUES (29, 19, 12)
INSERT [dbo].[AirOrder] ([AirOrderID], [OrderID], [AirID]) VALUES (30, 20, 11)
INSERT [dbo].[AirOrder] ([AirOrderID], [OrderID], [AirID]) VALUES (31, 21, 14)
INSERT [dbo].[AirOrder] ([AirOrderID], [OrderID], [AirID]) VALUES (32, 21, 14)
INSERT [dbo].[AirOrder] ([AirOrderID], [OrderID], [AirID]) VALUES (33, 22, 12)
INSERT [dbo].[AirOrder] ([AirOrderID], [OrderID], [AirID]) VALUES (34, 23, 13)
INSERT [dbo].[AirOrder] ([AirOrderID], [OrderID], [AirID]) VALUES (35, 23, 13)
SET IDENTITY_INSERT [dbo].[AirOrder] OFF
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([ClientID], [LastName], [FirstName], [MiddleName], [Phone], [Email]) VALUES (3, N'Шашкова', N'Алина', N'Ильдаровна', N'+7 (969) 325-95-89', N'ShashkovaFlor179@mail.ru')
INSERT [dbo].[Client] ([ClientID], [LastName], [FirstName], [MiddleName], [Phone], [Email]) VALUES (4, N'Петухов', N'Тимур', N'Олегович', N'+7 (917) 459-24-33', N'Petuhov@gmail.ru')
INSERT [dbo].[Client] ([ClientID], [LastName], [FirstName], [MiddleName], [Phone], [Email]) VALUES (5, N'Генералова', N'Людмила', N'Сергеевна', N'+7 (991) 240-73-10', N'GeneralovaJibek254@yandex.ru')
INSERT [dbo].[Client] ([ClientID], [LastName], [FirstName], [MiddleName], [Phone], [Email]) VALUES (8, N'Бражников', N'Абжемиль', N'Андреевич', N'+7 (900) 745-32-34', N'BrajnikovAbjemil464@mail.ru')
INSERT [dbo].[Client] ([ClientID], [LastName], [FirstName], [MiddleName], [Phone], [Email]) VALUES (9, N'Виноградова', N'Амата', N'Николаевна', N'+7 (942) 988-43-60', N'VinogradovaAmata322@mail.ru')
SET IDENTITY_INSERT [dbo].[Client] OFF
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderID], [StartDate], [EndDate], [StatusID], [ClientID], [Address], [TotalPrice]) VALUES (19, CAST(N'2020-05-13' AS Date), CAST(N'2020-05-14' AS Date), 1, 3, N'Казань', 39998)
INSERT [dbo].[Order] ([OrderID], [StartDate], [EndDate], [StatusID], [ClientID], [Address], [TotalPrice]) VALUES (20, CAST(N'2020-05-07' AS Date), CAST(N'2020-05-09' AS Date), 3, 4, N'Зеленодольск', 36150)
INSERT [dbo].[Order] ([OrderID], [StartDate], [EndDate], [StatusID], [ClientID], [Address], [TotalPrice]) VALUES (21, CAST(N'2020-05-06' AS Date), CAST(N'2020-05-13' AS Date), 3, 8, N'Казань', 41998)
INSERT [dbo].[Order] ([OrderID], [StartDate], [EndDate], [StatusID], [ClientID], [Address], [TotalPrice]) VALUES (22, CAST(N'2020-04-01' AS Date), CAST(N'2020-05-03' AS Date), 2, 5, N'Казань', 26499)
INSERT [dbo].[Order] ([OrderID], [StartDate], [EndDate], [StatusID], [ClientID], [Address], [TotalPrice]) VALUES (23, CAST(N'2020-05-01' AS Date), CAST(N'2020-05-07' AS Date), 2, 9, N'Зеленодольск', 92400)
SET IDENTITY_INSERT [dbo].[Order] OFF
SET IDENTITY_INSERT [dbo].[Service] ON 

INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (1, N'Стандартный монтаж кондиционера (07-09) ', 7000, N'В стандартный монтаж входит 3.5м коммуникаций, биение отверстия наружной стены, монтаж внутреннего и наружного блока, короб канал 1м,кронштейны,обязательное вакуумирование и пуско-наладка. Гарантия от 2х лет.', 1)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (2, N'Стандартный монтаж кондиционера (12) ', 8000, N'В стандартный монтаж входит 3.5м коммуникаций, биение отверстия наружной стены, монтаж внутреннего и наружного блока, короб канал 1м, кронштейны, обязательное вакуумирование и пуско-наладка. Гарантия от .2х лет.', 1)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (3, N'Стандартный монтаж кондиционера (18) ', 8800, N'В стандартный монтаж входит 3.5м коммуникаций, биение отверстия наружной стены, монтаж внутреннего и наружного блока, короб канал 1м,кронштейны,обязательное вакуумирование и пуско-наладка. Гарантия от 2х лет.', 1)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (4, N'Стандартный монтаж кондиционера (24) ', 10000, N'В стандартный монтаж входит 3.5м коммуникаций, биение отверстия наружной стены, монтаж внутреннего и наружного блока, короб канал 1м,кронштейны,обязательное вакуумирование и пуско-наладка. Гарантия от 2х лет.', 1)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (5, N'Стандартный монтаж кондиционера (30) ', 11500, N'В стандартный монтаж входит 5 м коммуникаций, биение отверстия наружной стены, монтаж внутреннего и наружного блока, короб канал 1м,кронштейны,обязательное вакуумирование и пуско-наладка. Гарантия от 2х лет.', 1)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (6, N'Стандартный монтаж кондиционера (36) ', 13500, N'В стандартный монтаж входит 5 м коммуникаций, биение отверстия наружной стены, монтаж внутреннего и наружного блока, короб канал 1м,кронштейны,обязательное вакуумирование и пуско-наладка. Гарантия от 2х лет.', 1)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (7, N'Стандартный монтаж кондиционера (48) ', 16500, N'В стандартный монтаж входит 5 м коммуникаций, биение отверстия наружной стены, монтаж внутреннего и наружного блока, короб канал 1м,кронштейны,обязательное вакуумирование и пуско-наладка. Гарантия от 2х лет.', 1)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (8, N'Стандартный монтаж кондиционера (60) ', 18900, N'В стандартный монтаж входит 5 м коммуникаций, биение отверстия наружной стены, монтаж внутреннего и наружного блока, короб канал 1м,кронштейны,обязательное вакуумирование и пуско-наладка. Гарантия от 2х лет.', 1)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (9, N'Демонтаж кондиционера (07-12)', 3000, N'Демонтаж кондиционера', 1)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (10, N'Демонтаж кондиционера (18-24)', 4000, N'', 1)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (11, N'Альп работа', 4500, N'', 1)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (12, N'Обслуживание кондиционера (07-12) ', 2500, N'Список работа: дозаправка фреоном, чистка наружного блока «Кёрхером» под высоким давлением, чистка дренажной ванны, чистка внутреннего блока и обеззараживание парогенератором , протяжка вальцовочных соединений, протяжка электрических соединений, проверка во всех режимах работы', 2)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (13, N'Обслуживание кондиционера (18 - 24) ', 3000, N'Список работа: дозаправка фреоном, чистка наружного блока «Кёрхером» под высоким давлением, чистка дренажной ванны, чистка внутреннего блока и обеззараживание парогенератором , протяжка вальцовочных соединений, протяжка электрических соединений, проверка во всех режимах работы', 2)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (14, N'Обслуживание кондиционера (30-60 ', 3500, N'Список работа: дозаправка фреоном, чистка наружного блока «Кёрхером» под высоким давлением, чистка дренажной ванны, чистка внутреннего блока и обеззараживание парогенератором , протяжка вальцовочных соединений, протяжка электрических соединений, проверка во всех режимах работы', 2)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (15, N'Обслуживание VRV сисмем', 4000, N'', 2)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (16, N'Обслуживание Чиллера', 5000, N'', 2)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (17, N'Обслуживание Центрального кондиционера', 3500, N'', 2)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (18, N'Диагностика бытового кондиционера', 1000, N'', 3)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (19, N'Диагностика Промышленного кондиционера', 2500, N'', 3)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (20, N'Диагностика мультизональной VRV и VRF системы', 4000, N'', 3)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (21, N'Диагностика чиллера', 4500, N'', 3)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (22, N'Легкий ремонт бытового кондиционера', 2000, N'', 3)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (23, N'Средний ремонт бытового кондиционера', 3000, N'', 3)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (24, N'Сложный ремонт бытового кондиционера', 5000, N'', 3)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (25, N'Легкий ремонт промышленного кондиционера', 4000, N'', 3)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (26, N'Средний ремонт промышленного кондиционера', 7000, N'', 3)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (27, N'Сложный ремонт промышленного кондиционера', 12000, N'', 3)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (28, N'Штраба (в кирпиче)', 850, N'', 4)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (29, N'Штраба (в битоне)', 1500, N'', 4)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (30, N'Штраба под дренаж (кирпич)', 400, N'', 4)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (31, N'Штроба под дренаж (бетон)', 750, N'', 4)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (32, N'Работа с вентилируемым фасадом', 800, N'', 4)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (33, N'Удлинение электропровода по питанию (м)', 150, N'', 4)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (34, N'Удлинение кабеля по питанию (в коробе) м.', 230, N'', 4)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (35, N'Ночные работы', 30, N'', 4)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (36, N'Монтаж дренажной помпы', 2500, N'', 4)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (37, N'Монтаж зимнего комплекта', 2500, N'', 4)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (38, N'Дополнительное отверстие в межкомнатной стене', 500, N'', 4)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (39, N'Дополнительный метр трассы (07-09)', 850, N'', 5)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (40, N'Дополнительный метр трассы (12-18)', 1050, N'', 5)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (41, N'Дополнительный метр трассы (24)', 1200, N'', 5)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (42, N'Дополнительный метр трассы (30 - 36)', 1350, N'', 5)
INSERT [dbo].[Service] ([ServiceID], [ServiceName], [ServicePrice], [ServiceDescription], [ServiceCategoryID]) VALUES (43, N'Дополнительный метр трассы (48 - 60)', 2596, N'', 5)
SET IDENTITY_INSERT [dbo].[Service] OFF
SET IDENTITY_INSERT [dbo].[ServiceCategory] ON 

INSERT [dbo].[ServiceCategory] ([ServiceCategoryID], [ServiceCategoryName]) VALUES (1, N'Монтаж')
INSERT [dbo].[ServiceCategory] ([ServiceCategoryID], [ServiceCategoryName]) VALUES (2, N'Обслуживание')
INSERT [dbo].[ServiceCategory] ([ServiceCategoryID], [ServiceCategoryName]) VALUES (3, N'Ремонт')
INSERT [dbo].[ServiceCategory] ([ServiceCategoryID], [ServiceCategoryName]) VALUES (4, N'Дополнительные работы')
INSERT [dbo].[ServiceCategory] ([ServiceCategoryID], [ServiceCategoryName]) VALUES (5, N'Расходные материалы')
SET IDENTITY_INSERT [dbo].[ServiceCategory] OFF
SET IDENTITY_INSERT [dbo].[ServiceOrder] ON 

INSERT [dbo].[ServiceOrder] ([ServiceOrderID], [OrderID], [ServiceID]) VALUES (33, 19, 1)
INSERT [dbo].[ServiceOrder] ([ServiceOrderID], [OrderID], [ServiceID]) VALUES (34, 19, 1)
INSERT [dbo].[ServiceOrder] ([ServiceOrderID], [OrderID], [ServiceID]) VALUES (35, 20, 4)
INSERT [dbo].[ServiceOrder] ([ServiceOrderID], [OrderID], [ServiceID]) VALUES (36, 20, 11)
INSERT [dbo].[ServiceOrder] ([ServiceOrderID], [OrderID], [ServiceID]) VALUES (37, 20, 12)
INSERT [dbo].[ServiceOrder] ([ServiceOrderID], [OrderID], [ServiceID]) VALUES (38, 20, 30)
INSERT [dbo].[ServiceOrder] ([ServiceOrderID], [OrderID], [ServiceID]) VALUES (39, 21, 2)
INSERT [dbo].[ServiceOrder] ([ServiceOrderID], [OrderID], [ServiceID]) VALUES (40, 22, 6)
INSERT [dbo].[ServiceOrder] ([ServiceOrderID], [OrderID], [ServiceID]) VALUES (41, 23, 4)
INSERT [dbo].[ServiceOrder] ([ServiceOrderID], [OrderID], [ServiceID]) VALUES (42, 23, 4)
SET IDENTITY_INSERT [dbo].[ServiceOrder] OFF
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([StatusID], [StatusName], [Color]) VALUES (1, N'Создана', N'#FFFFFACD')
INSERT [dbo].[Status] ([StatusID], [StatusName], [Color]) VALUES (2, N'Принята', N'#FF98FB98')
INSERT [dbo].[Status] ([StatusID], [StatusName], [Color]) VALUES (3, N'Выполнена', N'#FFAFEEEE')
SET IDENTITY_INSERT [dbo].[Status] OFF
ALTER TABLE [dbo].[AirOrder]  WITH CHECK ADD  CONSTRAINT [FK_AirOrder_Air] FOREIGN KEY([AirID])
REFERENCES [dbo].[Air] ([AirID])
GO
ALTER TABLE [dbo].[AirOrder] CHECK CONSTRAINT [FK_AirOrder_Air]
GO
ALTER TABLE [dbo].[AirOrder]  WITH CHECK ADD  CONSTRAINT [FK_AirOrder_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[AirOrder] CHECK CONSTRAINT [FK_AirOrder_Order]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([ClientID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Client]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Status]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_ServiceCategory] FOREIGN KEY([ServiceCategoryID])
REFERENCES [dbo].[ServiceCategory] ([ServiceCategoryID])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_ServiceCategory]
GO
ALTER TABLE [dbo].[ServiceOrder]  WITH CHECK ADD  CONSTRAINT [FK_ServiceOrder_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[ServiceOrder] CHECK CONSTRAINT [FK_ServiceOrder_Order]
GO
ALTER TABLE [dbo].[ServiceOrder]  WITH CHECK ADD  CONSTRAINT [FK_ServiceOrder_Service] FOREIGN KEY([ServiceID])
REFERENCES [dbo].[Service] ([ServiceID])
GO
ALTER TABLE [dbo].[ServiceOrder] CHECK CONSTRAINT [FK_ServiceOrder_Service]
GO
USE [master]
GO
ALTER DATABASE [AirDB] SET  READ_WRITE 
GO
