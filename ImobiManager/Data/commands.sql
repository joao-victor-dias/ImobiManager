CREATE DATABASE ImobiManager;


USE ImobiManager;

CREATE TABLE Clients (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255),
    CpfCnpj NVARCHAR(20) UNIQUE,
    IsCompany BIT,
    DateOfBirth DATE,
    Email NVARCHAR(255) UNIQUE,
    Phone NVARCHAR(20),
    Address NVARCHAR(255)
);

CREATE TABLE Apartaments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Number INT,
    BlockOrTower NVARCHAR(255),
    Floor INT,
    Area FLOAT,
    Bedrooms INT,
    Bathrooms INT,
    GarageSpaces INT,
    Price DECIMAL(18,2),
    Address NVARCHAR(255),
    Status INT,
    Description NVARCHAR(MAX)
);

CREATE TABLE Reservations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClientId INT NOT NULL,
    ApartamentId INT NOT NULL,
    ReservationDate DATETIME2 NOT NULL,
    CONSTRAINT FK_Reservations_Clients FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Reservations_Apartaments FOREIGN KEY (ApartamentId) REFERENCES Apartaments(Id) ON DELETE CASCADE
);

CREATE TABLE Sales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClientId INT NOT NULL,
    ApartamentId INT NOT NULL,
    SaleDate DATETIME2 NOT NULL,
    CONSTRAINT FK_Sales_Clients FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Sales_Apartaments FOREIGN KEY (ApartamentId) REFERENCES Apartaments(Id) ON DELETE CASCADE
);
