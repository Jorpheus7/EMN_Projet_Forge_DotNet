UPDATE [AdventureWorks2008].[Person].[Person]
   SET [FirstName] = 'Anne'
      ,[MiddleName] = 'C'
      ,[LastName] = 'Honyme'
 WHERE [BusinessEntityID] = 1
GO

UPDATE [AdventureWorks2008].[Person].[Password]
SET PasswordHash = '80a8ce21dba7d77547bfc61144731b96='
WHERE BusinessEntityID = 1
GO

UPDATE [AdventureWorks2008].[Person].[EmailAddress]
SET EmailAddress = 'demo'
WHERE BusinessEntityID = 1
GO