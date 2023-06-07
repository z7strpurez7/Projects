-- Create superhero
CREATE TABLE SuperHero(
Id int NOT NULL IDENTITY PRIMARY KEY,
Name nvarchar(50) UNIQUE,
Alias nvarchar(50),
Origin nvarchar(50),

)
--Create Assistant
CREATE TABLE Assistant(
Id int NOT NULL  IDENTITY PRIMARY KEY,
Name nvarchar(50),
)
--Create Power
CREATE TABLE Power(
Id int NOT NULL IDENTITY PRIMARY KEY,
Name nvarchar(50),
Description nvarchar(50),
)