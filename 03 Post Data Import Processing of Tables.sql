-- Changing the recovery mode of database to simple for minimal transactional logging
ALTER DATABASE Geonames
SET RECOVERY SIMPLE

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

-- Remove data from AlternateName that is not available in LanguageCode table

-- Creating RowVersion Columns in all the tables and 
-- populating it with data
-- Note that creation of column is a fully logged operations

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

Create NonClustered index NCI_RawData_Name_ASCIIName on RawData (Name, ASCIIName)
Go 

Create NonClustered Index NCI_RawData_Latitude_Longitude on RawData (Latitude, Longitude)
Go

Create NonClustered Index NCI_RawData_ISOCountryCode on RawData (ISOCountryCode)
Go

Create NonClustered Index NCI_RawData_FeatureCategoryId_FeatureCode
on RawData (FeatureCategoryId, FeatureCode)
Go
