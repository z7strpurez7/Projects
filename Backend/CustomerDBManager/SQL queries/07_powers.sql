--Inserting 4 hero powers
INSERT INTO Power (Name, Description)
VALUES('Frost', 'Frosty powers');

INSERT INTO Power (Name, Description)
VALUES('Fire', 'Manipulates fire');

INSERT INTO Power (Name, Description)
VALUES('Man of steel', 'Super strength and armor');


INSERT INTO Power (Name, Description)
VALUES('Backend Wizard', 'Super crazy backend skills');


--Inserting the 4 hero powers to heroes
INSERT INTO SuperheroPower(SuperheroId, PowerId)
VALUES(1, 2);


INSERT INTO SuperheroPower(SuperheroId, PowerId)
VALUES(2, 3);


INSERT INTO SuperheroPower(SuperheroId, PowerId)
VALUES(3,1);

INSERT INTO SuperheroPower(SuperheroId, PowerId)
VALUES(3,4);