CREATE DATABASE SchemaPalDb;
GO

USE SchemaPalDb;
GO

CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY,     
    Username NVARCHAR(255) NOT NULL,         
    PasswordHash NVARCHAR(MAX) NOT NULL     
);
GO

CREATE TABLE DatabaseSchemaRecords (
    Id UNIQUEIDENTIFIER PRIMARY KEY,    
    UserId UNIQUEIDENTIFIER NOT NULL,    
    Name NVARCHAR(255) NOT NULL,         
    SchemaJsonFormat NVARCHAR(MAX) NOT NULL,     
    LastSaved DATETIME NOT NULL        
);
GO