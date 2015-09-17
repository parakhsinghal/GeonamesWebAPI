-- Drop the old database if exists and create a new one

USE master;
Go

IF DB_ID('Geonames') IS NOT NULL
DROP DATABASE Geonames;
Go

CREATE DATABASE Geonames;
Go

USE Geonames;
Go

Alter Database Geonames
Set Recovery Bulk_Logged;

-- Create Tables

CREATE TABLE dbo.Country
(	
    ISOCountryCode		char(2)			NOT NULL,
    ISO3Code			char(3)			NULL,
    ISONumeric			int				NULL,
    FIPSCode			char(2)			NULL,
    CountryName			nvarchar(200)	NULL,
    Capital				nvarchar(200)	NULL,
    SqKmArea			float			NULL,
    TotalPopulation		bigint			NULL,
    ContinentCodeId		char(2)			NULL,
    TopLevelDomain		nvarchar(10)	NULL,
    CurrencyCode		char(4)			NULL,
    CurrencyName		nvarchar(128)	NULL,
    Phone				nvarchar(32)	NULL,
    PostalFormat		nvarchar(64)	NULL,
    PostalRegex			nvarchar(256)	NULL,
    Languages			nvarchar(256)	NULL,
    GeonameId			bigint			NULL,
    Neighbors			nvarchar(256)	NULL,
    EquivalentFipsCode	nvarchar(64)	NULL
);
Go

CREATE TABLE dbo.Admin1Code
(
    Admin1CodeId	nvarchar(32)	NOT NULL,
    Admin1CodeName	nvarchar(128)	NULL,
    ASCIIName		nvarchar(128)	NULL,
    GeonameId		bigint			NULL
);
Go

CREATE TABLE dbo.Admin2Code
(
    Admin2CodeId	nvarchar(32)	NOT NULL,
    Admin2CodeName	nvarchar(128)	NULL,
    ASCIIName		nvarchar(128)	NULL,
    GeonameId		bigint			NULL
);
Go

CREATE TABLE dbo.AlternateName
(
    AlternateNameId			int				NOT NULL,
    GeonameId				bigint			NULL,
    ISO6393LanguageCode		nvarchar(24)	NULL,
    AlternateName			nvarchar(512)	NULL,
    IsPreferredName			bit				NULL,
    IsShortName				bit				NULL,
    IsColloquial			bit				NULL,
    IsHistoric				bit				NULL
);
Go

CREATE TABLE dbo.Continent
(
    ContinentCodeId		char(2)			NOT NULL,
    GeonameId			bigint			NULL,
    Continent			nvarchar(32)	NULL
);
Go

CREATE TABLE dbo.FeatureCategory
(
    FeatureCategoryId		char(1)			NOT NULL,
    FeatureCategoryName		nvarchar(128)	NULL
);
Go

CREATE TABLE dbo.FeatureCode
(
    FeatureCodeId		nvarchar(16)	NOT NULL,
    FeatureCodeName		nvarchar(128)	NULL,
    Description			nvarchar(512)	NULL
);
Go

CREATE TABLE dbo.LanguageCode
(
    ISO6393		nvarchar(24)	NOT NULL,
    ISO6392		nvarchar(24)	NULL,
    ISO6391		nvarchar(24)	NULL,
    Language	nvarchar(128)	NULL
);
Go

CREATE TABLE dbo.RawData
(
    GeonameId			bigint			NOT NULL,
    Name				nvarchar(200)	NULL,
    ASCIIName			nvarchar(200)	NULL,
    AlternateNames		text			NULL,
    Latitude			float			NULL,
    Longitude			float			NULL,
    FeatureCategoryId	char(1)			NULL,
    FeatureCode			nvarchar(10)	NULL,
    ISOCountryCode		char(2)			NULL,
    CC2					nvarchar(60)	NULL,
    Admin1Code			nvarchar(20)	NULL,
    Admin2Code			nvarchar(80)	NULL,
    Admin3Code			nvarchar(20)	NULL,
    Admin4Code			nvarchar(20)	NULL,
    Population			bigint			NULL,
    Elevation			int				NULL,
    DEM					int				NULL,
    TimeZoneId			nvarchar(128)	NULL,
    ModificationDate	datetime		NULL
);
Go

CREATE TABLE dbo.RawPostal
(
    ISOCountryCode	char(2)			NOT NULL,
    PostalCode		nvarchar(20)	NOT NULL,
    PlaceName		nvarchar(180)	NOT NULL,
    Admin1Name		nvarchar(100)	NULL,
    Admin1Code		nvarchar(20)	NULL,
    Admin2Name		nvarchar(100)	NULL,
    Admin2Code		nvarchar(20)	NULL,
    Admin3Name		nvarchar(100)	NULL,
    Admin3Code		nvarchar(20)	NULL,
    Latitude		float			NULL,
    Longitude		float			NULL,
    Accuracy		int				NULL
);
Go

CREATE TABLE dbo.TimeZone
(
    ISOCountryCode	char(2)			NULL,
    TimeZoneId		nvarchar(128)	NOT NULL,
    GMT				decimal(18, 3)	NULL,
    DST				decimal(18, 3)	NULL,
    RawOffset		decimal(18, 3)	NULL
);
Go

CREATE TABLE dbo.Hierarchy
(
	ParentId	bigint			NOT NULL,
	ChildId		bigint			NOT NULL,
	Type		nvarchar(50)	NULL
)
Go

-- Insert data into the created tables

BULK INSERT LanguageCode FROM 'Place where the LanguageCode file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
Go

BULK INSERT FeatureCode FROM 'Place where the FeatureCode file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
Go

INSERT INTO FeatureCateGory (FeatureCateGoryId, FeatureCateGoryName)
VALUES  ('A', 'Country, State, Region, etc.'),
        ('H', 'Stream, Lake, etc.'),
        ('L', 'Parks, Areas, etc.'),
        ('P', 'Cities, Villages, etc.'),
        ('R', 'Roads, Railroads, etc.'),
        ('S', 'Spots, buildings, farms, etc.'),
        ('T', 'Mountain, Hill, Rock, etc.'),
        ('U', 'Undersea'),
        ('V', 'Forest, Heath, etc.');
Go

BULK INSERT RawData FROM 'Place where the RawData file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
Go

BULK INSERT Country FROM 'Place where the Country data file is' 
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
Go

BULK INSERT Admin1Code FROM 'place where the Admin1Code is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
Go

BULK INSERT Admin2Code FROM 'Place where the Admin2Code file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
Go

INSERT INTO  Continent (ContinentCodeId, GeonameId, Continent)
VALUES  ('AF', 6255146, 'Africa'),
        ('AS', 6255147, 'Asia'),
        ('EU', 6255148, 'Europe'),
        ('NA', 6255149, 'North America'),
        ('OC', 6255151, 'Oceania'),
        ('SA', 6255150, 'South America'),
        ('AN', 6255152, 'Antarctica');
Go

BULK INSERT AlternateName FROM 'Place where the AlterNames file is' 
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
Go

BULK INSERT Timezone FROM 'Palce where teh TimeZone file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
Go

BULK INSERT RawPostal FROM 'Place where the RawPostal file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
Go

BULK INSERT Hierarchy FROM 'Place where the Hierarchy file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
Go

-- Remove duplicate entries from the Hierarchy table
With DuplicateData_Hierarchy_CTE As
(
	Select ParentId, ChildId, Type
	,ROW_NUMBER() Over (Partition by ParentId, ChildId Order By ParentId) as RowNumber
	from Hierarchy
)

Delete from DuplicateData_Hierarchy_CTE where RowNumber > 1;
Go

-- Remove data from AlternateName that is not available in Rawdata table
Delete from dbo.AlternateName where dbo.AlternateName.GeonameId not in 
(Select GeonameId from dbo.RawData);
Go

-- Remove data from AlternateName that is not available in anguageCode table

-- Creating RowVersion Columns in all the tables and 
-- populating it with data

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

-- Create constraints

Alter Table Rawdata
Add Constraint PK_Rawdata_GeonameId Primary Key (GeonameId);
Go

Alter Table TimeZone
Add Constraint PK_TimeZone_TimeZoneId Primary Key (TimeZoneId);
Go

Alter Table LanguageCode
Add Constraint PK_LanguageCode_ISO6393 Primary Key (ISO6393);
Go

Alter Table Country
Add Constraint PK_Country_ISOCountryCode Primary Key (ISOCountryCode);
Go

Alter Table FeatureCategory
Add Constraint PK_FeatureCategory_FeatureCateGoryId Primary Key (FeatureCateGoryId);
Go

Alter Table Admin2Code
Add Constraint PK_Admin2Code_Admin2CodeId Primary Key (Admin2CodeId);
Go

Alter Table FeatureCode
Add Constraint PK_FeatureCode_FeatureCodeId Primary Key (FeatureCodeId);
Go

Alter Table Continent
Add Constraint PK_Continent_ContinentCodeId Primary Key (ContinentCodeId);
Go

Alter Table Admin1Code
Add Constraint PK_Admin1Code_Admin1CodeId Primary Key (Admin1CodeId);
Go

Alter Table AlternateName
Add Constraint PK_AlternateName_AlternateNameId Primary Key (AlternateNameId);
Go

Alter Table Rawdata
Add Constraint FK_Rawdata_TimeZoneId Foreign Key (TimeZoneId) References TimeZone(TimeZoneId);
Go

Alter Table Continent
Add Constraint FK_Continent_Rawdata Foreign Key (GeonameId) References Rawdata(GeonameId);
Go

Alter Table Admin1Code
Add Constraint FK_Admin1Code_Rawdata Foreign Key (GeonameId) References Rawdata(GeonameId);
Go

Alter Table Admin2Code
Add Constraint FK_Admin2Code_Rawdata Foreign Key (GeonameId) References Rawdata(GeonameId);
Go

Alter Table AlternateName
Add Constraint FK_AlternateName_Rawdata Foreign Key (GeonameId) References Rawdata(GeonameId);
Go

Alter Table AlternateName
Add Constraint FK_AlternateName_LanguageCode Foreign Key (ISO6393LanguageCode)
References LanguageCode(ISO6393);
Go

Alter Table Rawdata
Add Constraint FK_Rawdata_FeatureCategory Foreign Key (FeatureCategoryId)
References FeatureCategory(FeatureCateGoryId);
Go

Alter Table Rawdata
Add Constraint FK_Rawdata_Country Foreign Key (ISOCountryCode)
References Country(ISOCountryCode);
Go

Alter Table TimeZone
Add Constraint FK_TimeZone_Country Foreign Key (ISOCountryCode)
References Country(ISOCountryCode);
Go

Alter Table RawPostal
Add Constraint FK_RawPostal_Country Foreign Key (ISOCountryCode)
References Country(ISOCountryCode);
Go

Alter Table Hierarchy
Add Constraint PK_Hierarchy_ParentId_ChildId Primary Key(ParentId, ChildId)
Go

-- Create Indexes

Create NonClustered Index NCI_Admin1Code_CoveringIndex on Admin1Code(Admin1CodeName, ASCIIName, GeonameId);
Go

Create NonClustered Index NCI_Admin2Code_CoveringIndex on Admin2Code(Admin2CodeName, ASCIIName, GeonameId);
Go

Create NonClustered Index NCI_Continent_CoveringIndex on Continent(Continent, GeonameId);
Go

Create NonClustered Index NCI_FeatureCode_CoveringIndex on FeatureCode(FeatureCodeName);
Go

Create NonClustered Index NCI_FeatureCategory_CoveringIndex on FeatureCategory(FeatureCategoryName);
Go

Create NonClustered Index NCI_TimeZone_CoveringIndex on TimeZone(TimeZoneId, GMT, DST, RawOffset);
Go

Create NonClustered Index NCI_LanguageCode_CoveringIndex on LanguageCode(ISO6392, ISO6391, Language);
Go

Create NonClustered index NCI_RawData_Name_ASCIIName on RawData ( Name, ASCIIName)
Go 

Create NonClustered Index NCI_RawData_Latitude_Longitude on RawData ( Latitude,Longitude)
Go

Create NonClustered Index NCI_RawData_ISOCountryCode on RawData (ISOCountryCode)
Go

Create NonClustered Index NCI_RawData_FeatureCategoryId_FeatureCode
on RawData (FeatureCategoryId, FeatureCode)
Go

--Select and test the import of data

Select Count(1) as Count_Rawdata from rawdata
Go

Select Count(1) as Count_Admin1Code from Admin1Code;
Go

Select Count(1) as Count_Admin2Code from Admin2Code;
Go

Select Count(1) as Count_AlternateName from AlternateName;
Go

Select Count(1) as Count_Continent from Continent;
Go

Select Count(1) as Count_FeatureCategory from FeatureCategory;
Go

Select Count(1) as Count_FeatureCode from FeatureCode;
Go

Select Count(1) as Count_LanguageCode from LanguageCode;
Go

Select Count(1) as Count_Rawdata from RawData;
Go

Select Count(1) as Count_Timezone from TimeZone;
Go

-- Set the database to full recovery mode

Alter Database Geonames
Set Recovery Full;


Declare
@TimeZoneId nvarchar(128) = null
,@Latitude Decimal = null
,@Longitude Decimal = null
,@ISOCountryCode Char(10) = null
,@CountryName Nvarchar(200) = null
,@PlaceName Nvarchar(200) = null

Select
RawData.ISOCountryCode
,Country.CountryName
,RawData.Latitude
,RawData.Longitude
,RawData.TimeZoneId
,TimeZone.DST
,TimeZone.GMT,
TimeZone.RawOffset
From RawData
inner join TimeZone on RawData.TimeZoneId = TimeZone.TimeZoneId
inner join Country on RawData.ISOCountryCode = Country.ISOCountryCode
Where
RawData.TimeZoneId = Coalesce(LTrim(RTrim(@TimeZoneId)),RawData.TimeZoneId)
And RawData.Latitude = Coalesce(@Latitude,RawData.Latitude)
And RawData.Longitude = Coalesce(@Longitude,Rawdata.Longitude)
And RawData.ISOCountryCode = Coalesce(LTrim(RTrim(@ISOCountryCode)), RawData.ISOCountryCode)
And Country.CountryName = Coalesce(LTrim(RTrim(@CountryName)),Country.CountryName)
And RawData.Name = Coalesce(LTrim(RTrim(@PlaceName)),Rawdata.Name)
Group By
RawData.ISOCountryCode
,Country.CountryName
,RawData.Latitude
,RawData.Longitude
,RawData.TimeZoneId
,TimeZone.DST
,TimeZone.GMT
,TimeZone.RawOffset