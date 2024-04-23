SET IDENTITY_INSERT Roles ON;

IF NOT EXISTS (SELECT *
                 FROM Roles
                WHERE Id = 1)
BEGIN

 INSERT
   INTO Roles
        (Id,
        [Name],
        IsActive)
 VALUES (1,
         'Super Admin',
         1);
END

IF NOT EXISTS (SELECT *
                 FROM Roles
                WHERE Id = 2)
BEGIN

 INSERT
   INTO Roles
        (Id,
        [Name],
        IsActive)
 VALUES (2,
         'Admin',
         1);
END

IF NOT EXISTS (SELECT *
                 FROM Roles
                WHERE Id = 3)
BEGIN

 INSERT
   INTO Roles
        (Id,
        [Name],
        IsActive)
 VALUES (3,
         'Merchant',
         1);
END

IF NOT EXISTS (SELECT *
                 FROM Roles
                WHERE Id = 4)
BEGIN

 INSERT
   INTO Roles
        (Id,
        [Name],
        IsActive)
 VALUES (4,
         'User',
         1);
END

SET IDENTITY_INSERT Roles OFF;