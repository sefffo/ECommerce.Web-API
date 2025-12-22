    IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
    BEGIN
        CREATE TABLE [__EFMigrationsHistory] (
            [MigrationId] nvarchar(150) NOT NULL,
            [ProductVersion] nvarchar(32) NOT NULL,
            CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
        );
    END;
    GO

    BEGIN TRANSACTION;
    CREATE TABLE [ProductBrands] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ProductBrands] PRIMARY KEY ([Id])
    );

    CREATE TABLE [ProductTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ProductTypes] PRIMARY KEY ([Id])
    );

    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [PictureUrl] nvarchar(max) NOT NULL,
        [Price] decimal(10,2) NOT NULL,
        [TypeId] int NOT NULL,
        [BrandId] int NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_ProductBrands_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [ProductBrands] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Products_ProductTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [ProductTypes] ([Id]) ON DELETE CASCADE
    );

    CREATE INDEX [IX_Products_BrandId] ON [Products] ([BrandId]);

    CREATE INDEX [IX_Products_TypeId] ON [Products] ([TypeId]);

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251025120713_Product module v2 ', N'9.0.10');

    CREATE TABLE [DeliveryMethods] (
        [Id] int NOT NULL IDENTITY,
        [ShortName] varchar(100) NOT NULL,
        [Description] varchar(100) NOT NULL,
        [DeliveryTime] varchar(50) NOT NULL,
        [Price] decimal(8,2) NOT NULL,
        CONSTRAINT [PK_DeliveryMethods] PRIMARY KEY ([Id])
    );

    CREATE TABLE [Orders] (
        [Id] uniqueidentifier NOT NULL,
        [UserEmail] nvarchar(max) NOT NULL,
        [OrderDate] datetimeoffset NOT NULL,
        [Address_FirstName] nvarchar(max) NOT NULL,
        [Address_LastName] nvarchar(max) NOT NULL,
        [Address_Street] nvarchar(max) NOT NULL,
        [Address_City] nvarchar(max) NOT NULL,
        [Address_Country] nvarchar(max) NOT NULL,
        [DeliveryMethodeId] int NOT NULL,
        [Status] int NOT NULL,
        [SubTotal] decimal(8,2) NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Orders_DeliveryMethods_DeliveryMethodeId] FOREIGN KEY ([DeliveryMethodeId]) REFERENCES [DeliveryMethods] ([Id]) ON DELETE CASCADE
    );

    CREATE TABLE [OrderItems] (
        [Id] int NOT NULL IDENTITY,
        [Price] decimal(8,2) NOT NULL,
        [Quantity] int NOT NULL,
        [Product_ProductId] int NOT NULL,
        [Product_ProductName] nvarchar(max) NOT NULL,
        [Product_ProductImgUrl] nvarchar(max) NOT NULL,
        [OrderId] uniqueidentifier NULL,
        CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id])
    );

    CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);

    CREATE INDEX [IX_Orders_DeliveryMethodeId] ON [Orders] ([DeliveryMethodeId]);

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118191254_Order-Module', N'9.0.10');

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118195329_Edit on DeliveryMethod', N'9.0.10');

    ALTER TABLE [Orders] ADD [PaymentIntentId] nvarchar(max) NOT NULL DEFAULT N'';

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251121215127_fix-shit', N'9.0.10');

    COMMIT;
    GO

