SET IDENTITY_INSERT Genders ON;

IF NOT EXISTS (SELECT *
                 FROM Genders
                WHERE Id = 1)

BEGIN

 INSERT
   INTO Genders
        (Id,
        [Name])
 VALUES (1,
        'Male')
END

IF NOT EXISTS (SELECT *
                 FROM Genders
                WHERE Id = 2)

BEGIN

 INSERT
   INTO Genders
        (Id,
        [Name])
 VALUES (2,
        'Female')
END

SET IDENTITY_INSERT Genders OFF;