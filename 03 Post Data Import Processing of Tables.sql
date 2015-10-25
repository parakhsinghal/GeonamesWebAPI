-- Changing the recovery mode of database to simple for minimal transactional logging
ALTER DATABASE Geonames
SET RECOVERY SIMPLE
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

-- Create data in the dbo.Country table from dbo.Rawdata
-- that does not exist in the dbo.Country table
Insert Into dbo.Country (ISOCountryCode, CountryName, GeonameId)
Select ISOCountryCode, ASCIIName, GeonameId from dbo.RawData
where dbo.RawData.ISOCountryCode not in (Select ISOCountryCode from Country)

-- Remove data from AlternateName that is not available in LanguageCode table

-- Creating RowVersion Columns in all the tables and 
-- populating it with data
-- Note that creation of column is a fully logged operations


-- Update GeonameIds in dbo.Country table
-- to bring them at par with as they exists in 
-- dbo.RawData table
UPDATE dbo.Country
set dbo.Country.GeonameId = dbo.RawData.GeonameId
from dbo.Country
inner join dbo.RawData on dbo.Country.ISOCountryCode = dbo.RawData.ISOCountryCode
where dbo.RawData.FeatureCategoryId = 'A' and dbo.RawData.FeatureCode = 'PCLI'