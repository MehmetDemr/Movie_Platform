# Movie_Platform
## Overview
This platform allows you to share your thoughts about movies and provides you with movie statistics.
You can comment on movies on this platform, which enriches everyone's movie experience. 
You can rate and interact with other moviegoers while discovering interesting statistics.
## Features
As an user:

You can see popular movies, comment on movies, rate movies, and even create a movie list.

As a director:

You can see popular movies and even make a movie yourself

## Usage

1.Clone or download the repository to your local machine.

2.Open the project in Visual Studio or your preferred C# development environment.

3.Once the project is loaded, locate the SQL Server Object Explorer in Visual Studio.

4.Connect to your SQL Server instance using the SQL Server Object Explorer.

5.Navigate to the database you want to work with.

6.Explore the database schema, tables, views, stored procedures, and other objects using the SQL Server Object Explorer.

7.Perform various tasks such as querying data, creating or modifying database objects, and managing database connections directly from Visual Studio.

8.After making any changes, build and run the project to apply the changes to your application.

## Required Tables:
CREATE TABLE [dbo].[Filmler] (
    [Id]                 INT          IDENTITY (1, 1) NOT NULL,
    [FilmAdi]            VARCHAR (30) NOT NULL,
    [Yonetmen]           VARCHAR (20) NOT NULL,
    [Oyuncular]          TEXT         NOT NULL,
    [Tur]                TEXT         NOT NULL,
    [YayinYili]          DATETIME     NOT NULL,
    [DegerlendirmePuani] FLOAT (53)   NOT NULL,
    [YoneticiId]         INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([YoneticiId]) REFERENCES [dbo].[Yoneticiler] ([y_id])
);

CREATE TABLE [dbo].[Filmplaylist] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [FilmAdi]       NVARCHAR (100) NULL,
    [Yonetmen]      NVARCHAR (100) NULL,
    [Yil]           DATETIME       NULL,
    [Degerlendirme] FLOAT (53)     NULL,
    [KisiId]        INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([KisiId]) REFERENCES [dbo].[Kullanicilar] ([k_id])
);

CREATE TABLE [dbo].[Filmyorumlari] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [FilmAdi]       NVARCHAR (100) NULL,
    [Yonetmen]      NVARCHAR (100) NULL,
    [KullaniciAdi]  VARCHAR (20)   NULL,
    [Yorum]         TEXT           NULL,
    [Degerlendirme] FLOAT (53)     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Kullanicilar] (
    [k_id]         INT          IDENTITY (1, 1) NOT NULL,
    [TCNo]         TEXT         NOT NULL,
    [Ad]           VARCHAR (20) NOT NULL,
    [Soyad]        VARCHAR (15) NOT NULL,
    [DogumTarihi]  DATETIME     NOT NULL,
    [Cinsiyet]     TEXT         NOT NULL,
    [Uyelik]       TEXT         NOT NULL,
    [KullaniciAdi] VARCHAR (20) NOT NULL,
    [Sifre]        VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([k_id] ASC)
);

CREATE TABLE [dbo].[Yoneticiler] (
    [y_id]         INT          IDENTITY (1, 1) NOT NULL,
    [TCNo]         TEXT         NOT NULL,
    [Ad]           VARCHAR (20) NOT NULL,
    [Soyad]        VARCHAR (15) NOT NULL,
    [DogumTarihi]  DATETIME     NOT NULL,
    [Cinsiyet]     TEXT         NOT NULL,
    [KullaniciAdi] VARCHAR (20) NOT NULL,
    [Sifre]        VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([y_id] ASC)
);




## Note

- If there are any issues, an error message will be displayed.

## Contributions

-Contributions are welcome! Feel free to open issues or submit pull requests.
