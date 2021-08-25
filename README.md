# BlockbusterApp

Script para criação do Banco do Dados e tabelas:
CREATE DATABASE Blockbuster
GO

USE Blockbuster
GO

CREATE TABLE Movie (
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Title VARCHAR (50),
ReleaseDate DATE,
IsActive BIT,
Genre VARCHAR (50)
)
GO

CREATE TABLE Genre (
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Title VARCHAR (50),
ReleaseDate DATE,
IsActive BIT,
)
GO

