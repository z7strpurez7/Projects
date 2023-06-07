--Creating table that links Superhero and Power
CREATE TABLE SuperheroPower (
  SuperheroId INT,
  PowerId INT,
  -- Two primary keys
  PRIMARY KEY (SuperheroId, PowerId),
  FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id),
  FOREIGN KEY (PowerId) REFERENCES Power(Id)
);