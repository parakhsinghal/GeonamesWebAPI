-- Add RowVersion for optimistic concurrency

Use Geonames;
Go

Alter Table dbo.Admin1Code
Add RowId RowVersion Not Null;
Go

Alter Table dbo.Admin2Code
Add  RowId RowVersion Not Null;
Go

Alter Table dbo.AlternateName
Add  RowId RowVersion Not Null;
Go

Alter Table dbo.Continent
Add  RowId RowVersion Not Null;
Go

Alter Table dbo.Country
Add  RowId RowVersion Not Null;
Go

Alter Table dbo.FeatureCategory
Add  RowId RowVersion Not Null;
Go

Alter Table dbo.FeatureCode
Add  RowId RowVersion Not Null;
Go

Alter Table dbo.Hierarchy
Add  RowId RowVersion Not Null;
Go

Alter Table dbo.LanguageCode
Add  RowId RowVersion Not Null;
Go

Alter Table dbo.RawData
Add  RowId RowVersion Not Null;
Go

Alter Table dbo.RawPostal
Add  RowId RowVersion Not Null;
Go

Alter Table dbo.TimeZone
Add  RowId RowVersion Not Null;
Go