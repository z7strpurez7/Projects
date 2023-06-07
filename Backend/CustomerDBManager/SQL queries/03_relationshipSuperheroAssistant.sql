--Add Id to Assistant table
Alter table Assistant
ADD SuperheroId INT,
--Add Foreign key contraint
CONSTRAINT FK_Assistant_Superhero
FOREIGN KEY (SuperheroId) REFERENCES Superhero(id);

