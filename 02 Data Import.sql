-- Change the transaction logging mode to only do minimal logging.

ALTER DATABASE Geonames
SET RECOVERY BULK_LOGGED;

-- Insert data into the created tables

BULK INSERT LanguageCode FROM 'Place where the LanguageCode file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

BULK INSERT FeatureCode FROM 'Place where the FeatureCode file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

INSERT INTO FeatureCateGOry (FeatureCateGOryId, FeatureCateGOryName)
VALUES  ('A', 'Country, State, Region, etc.'),
        ('H', 'Stream, Lake, etc.'),
        ('L', 'Parks, Areas, etc.'),
        ('P', 'Cities, Villages, etc.'),
        ('R', 'Roads, Railroads, etc.'),
        ('S', 'Spots, buildings, farms, etc.'),
        ('T', 'Mountain, Hill, Rock, etc.'),
        ('U', 'Undersea'),
        ('V', 'Forest, Heath, etc.');
GO

SET IDENTITY_INSERT dbo.RawData OFF 
GO

BULK INSERT RawData FROM 'Place where the RawData file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

SET IDENTITY_INSERT dbo.RawData ON 
GO

-- Make sure that you delete the first line in the csv file, if it has coulmn names in it.

BULK INSERT Country FROM 'Place where the Country data file is' 
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

BULK INSERT Admin1Code FROM 'place where the Admin1Code is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

BULK INSERT Admin2Code FROM 'Place where the Admin2Code file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

INSERT INTO  Continent (ContinentCodeId, GeonameId, Continent)
VALUES  ('AF', 6255146, 'Africa'),
        ('AS', 6255147, 'Asia'),
        ('EU', 6255148, 'Europe'),
        ('NA', 6255149, 'North America'),
        ('OC', 6255151, 'Oceania'),
        ('SA', 6255150, 'South America'),
        ('AN', 6255152, 'Antarctica');
GO

SET IDENTITY_INSERT dbo.AlternateName OFF
GO

BULK INSERT dbo.AlternateName FROM 'Place where the AlterNames file is' 
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

SET IDENTITY_INSERT dbo.AlternateName ON
GO

BULK INSERT Timezone FROM 'Palce where teh TimeZone file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

BULK INSERT RawPostal FROM 'Place where the RawPostal file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

BULK INSERT Hierarchy FROM 'Place where the Hierarchy file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

-- Change the transaction logging mode to only do full logging.

ALTER DATABASE Geonames
SET RECOVERY FULL;