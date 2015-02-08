-- Exec dbo.GetDistinctTimeZones

Create Procedure dbo.GetDistinctTimeZones 
	
 @PageNumber int = null
,@PageSize int = null

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		Select Distinct dbo.TimeZone.TimeZoneId, dbo.TimeZone.RowId From dbo.TimeZone
		Order By dbo.TimeZone.TimeZoneId
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		Select Distinct dbo.TimeZone.TimeZoneId, dbo.TimeZone.RowId From dbo.TimeZone
		Order By dbo.TimeZone.TimeZoneId;
	End
End
Go

-- Exec dbo.GetTimeZoneDetails 

Create Procedure dbo.GetTimeZoneDetails 

 @TimeZoneId nvarchar(128) = null
,@ISOCountryCode Char(10) = null
,@ISO3Code char(3) = null
,@ISONumeric int = null
,@CountryName Nvarchar(200) = null
,@Latitude float = null
,@Longitude float = null
,@PageNumber int = null
,@PageSize int = null

As

Begin

	Set NoCount On;	

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		Select
		dbo.TimeZone.ISOCountryCode		
		,dbo.TimeZone.TimeZoneId
		,dbo.TimeZone.DST
		,dbo.TimeZone.GMT
		,dbo.TimeZone.RawOffset
		,dbo.TimeZone.RowId
		From dbo.TimeZone
		inner join dbo.Country on dbo.Country.ISOCountryCode = dbo.TimeZone.ISOCountryCode
		inner join dbo.RawData on dbo.RawData.GeonameId = dbo.Country.GeonameId
		Where
		dbo.TimeZone.TimeZoneId = Coalesce(LTrim(RTrim(@TimeZoneId)),dbo.TimeZone.TimeZoneId)
		And dbo.TimeZone.ISOCountryCode = Coalesce(LTrim(RTrim(@ISOCountryCode)), TimeZone.ISOCountryCode)
		And dbo.Country.CountryName = Coalesce(LTrim(RTrim(@CountryName)),dbo.Country.CountryName)
		And dbo.Country.ISO3Code = Coalesce(LTrim(RTrim(@ISO3Code)),dbo.Country.ISO3Code)
		And dbo.Country.ISONumeric = Coalesce(LTrim(RTrim(@ISONumeric)),dbo.Country.ISONumeric)
		And dbo.RawData.Latitude = Coalesce(@Latitude,dbo.Rawdata.Latitude)
		And dbo.RawData.Longitude = Coalesce(@Longitude,dbo.Rawdata.Longitude)
		Group By
		dbo.TimeZone.ISOCountryCode
		,dbo.TimeZone.TimeZoneId
		,dbo.TimeZone.DST
		,dbo.TimeZone.GMT
		,dbo.TimeZone.RawOffset
		,dbo.TimeZone.RowId
		Order By dbo.TimeZone.ISOCountryCode
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		Select
		dbo.TimeZone.ISOCountryCode		
		,dbo.TimeZone.TimeZoneId
		,dbo.TimeZone.DST
		,dbo.TimeZone.GMT
		,dbo.TimeZone.RawOffset
		,dbo.TimeZone.RowId
		From dbo.TimeZone
		inner join dbo.Country on dbo.Country.ISOCountryCode = dbo.TimeZone.ISOCountryCode
		inner join dbo.RawData on dbo.RawData.GeonameId = dbo.Country.GeonameId
		Where
		dbo.TimeZone.TimeZoneId = Coalesce(LTrim(RTrim(@TimeZoneId)),dbo.TimeZone.TimeZoneId)
		And dbo.TimeZone.ISOCountryCode = Coalesce(LTrim(RTrim(@ISOCountryCode)), TimeZone.ISOCountryCode)
		And dbo.Country.CountryName = Coalesce(LTrim(RTrim(@CountryName)),dbo.Country.CountryName)
		And dbo.Country.ISO3Code = Coalesce(LTrim(RTrim(@ISO3Code)),dbo.Country.ISO3Code)
		And dbo.Country.ISONumeric = Coalesce(LTrim(RTrim(@ISONumeric)),dbo.Country.ISONumeric)
		And dbo.RawData.Latitude = Coalesce(@Latitude,dbo.Rawdata.Latitude)
		And dbo.RawData.Longitude = Coalesce(@Longitude,dbo.Rawdata.Longitude)
		Group By
		dbo.TimeZone.ISOCountryCode
		,dbo.TimeZone.TimeZoneId
		,dbo.TimeZone.DST
		,dbo.TimeZone.GMT
		,dbo.TimeZone.RawOffset
		,dbo.TimeZone.RowId
		Order By dbo.TimeZone.ISOCountryCode
	End

End
Go

-- Exec dbo.GetTimeZoneDetails_ByPlaceName @PlaceName = 'Ajmer'

Create Procedure  dbo.GetTimeZoneDetailsByPlaceName

@PlaceName Nvarchar(200) = null

As
Begin

	Set NoCount On;	

	Select
	dbo.TimeZone.ISOCountryCode
	,dbo.TimeZone.TimeZoneId
	,dbo.TimeZone.DST
	,dbo.TimeZone.GMT
	,dbo.TimeZone.RawOffset
	,dbo.TimeZone.RowId
	From RawData
	inner join dbo.Country on dbo.Country.ISOCountryCode = dbo.RawData.ISOCountryCode
	inner join dbo.TimeZone on dbo.Country.ISOCountryCode = dbo.TimeZone.ISOCountryCode
	Where dbo.RawData.ASCIIName = LTrim(RTrim(@PlaceName))
	Group By
	dbo.TimeZone.ISOCountryCode
	,dbo.TimeZone.TimeZoneId
	,dbo.TimeZone.DST
	,dbo.TimeZone.GMT
	,dbo.TimeZone.RawOffset
	,dbo.TimeZone.RowId
	Order By dbo.TimeZone.ISOCountryCode

End
Go

-- Exec dbo.GetCountryInfo @ISOCountryCode = null, @CountryName  = null, @PageNumber = null, @PageSize = null

Create Procedure dbo.GetCountryInfo  

 @ISOCountryCode char(2) = null
,@CountryName nvarchar(200) = null
,@PageNumber int = null
,@PageSize int = null

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		SELECT
		dbo.Country.ISOCountryCode
		,dbo.Country.ISO3Code
		,dbo.Country.ISONumeric
		,dbo.Country.FIPSCode
		,dbo.Country.CountryName
		,dbo.Country.Capital
		,dbo.Country.SqKmArea
		,dbo.Country.TotalPopulation
		,dbo.Country.ContinentCodeId
		,dbo.Country.TopLevelDomain
		,dbo.Country.CurrencyCode
		,dbo.Country.CurrencyName
		,dbo.Country.Phone
		,dbo.Country.PostalFormat
		,dbo.Country.PostalRegex
		,dbo.Country.Languages
		,dbo.Country.GeonameId
		,dbo.Country.Neighbors
		,dbo.Country.EquivalentFipsCode
		,dbo.Country.RowId
		From Geonames.dbo.Country
		Where
		dbo.Country.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.Country.ISOCountryCode)
		and
		dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
		Order by dbo.Country.CountryName
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		SELECT
		dbo.Country.ISOCountryCode
		,dbo.Country.ISO3Code
		,dbo.Country.ISONumeric
		,dbo.Country.FIPSCode
		,dbo.Country.CountryName
		,dbo.Country.Capital
		,dbo.Country.SqKmArea
		,dbo.Country.TotalPopulation
		,dbo.Country.ContinentCodeId
		,dbo.Country.TopLevelDomain
		,dbo.Country.CurrencyCode
		,dbo.Country.CurrencyName
		,dbo.Country.Phone
		,dbo.Country.PostalFormat
		,dbo.Country.PostalRegex
		,dbo.Country.Languages
		,dbo.Country.GeonameId
		,dbo.Country.Neighbors
		,dbo.Country.EquivalentFipsCode
		,dbo.Country.RowId
		From Geonames.dbo.Country
		Where
		dbo.Country.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.Country.ISOCountryCode)
		and
		dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
		Order by dbo.Country.CountryName;
	End
End
Go

-- Exec dbo.GetStateInfo  @CountryName = null,@ISOCountryCode = 'IN',@StateGeonameId = null,@StateName = null, @FeatureCategoryId = 'A' ,@FeatureCode = 'ADM1',@PageNumber = 2,@PageSize = 20

Create Procedure dbo.GetStateInfo  

 @CountryName nvarchar(200) = null
,@ISOCountryCode char(2) = null
,@StateGeonameId bigint = null
,@StateName nvarchar(200) = null
,@FeatureCategoryId char(1) = 'A'
,@FeatureCode nvarchar(10) = 'ADM1'
,@PageNumber int = null
,@PageSize int = null

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin		
		SELECT
		   dbo.RawData.GeonameId
		  ,dbo.RawData.Name
		  ,dbo.RawData.ASCIIName
		  ,dbo.RawData.AlternateNames
		  ,dbo.RawData.Latitude
		  ,dbo.RawData.Longitude
		  ,dbo.RawData.FeatureCategoryId
		  ,dbo.RawData.FeatureCode
		  ,dbo.RawData.ISOCountryCode
		  ,dbo.RawData.CC2
		  ,dbo.RawData.Admin1Code
		  ,dbo.RawData.Admin2Code
		  ,dbo.RawData.Admin3Code
		  ,dbo.RawData.Admin4Code
		  ,dbo.RawData.Population
		  ,dbo.RawData.Elevation
		  ,dbo.RawData.DEM
		  ,dbo.RawData.TimeZoneId
		  ,dbo.RawData.ModificationDate
		  ,dbo.RawData.RowId
		From dbo.RawData
		inner join dbo.Hierarchy on dbo.Hierarchy.ChildId = dbo.RawData.GeonameId
		inner join dbo.Country on dbo.Country.GeonameId = dbo.Hierarchy.ParentId
		where 
		dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
		and dbo.Country.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.Country.ISOCountryCode)
		and dbo.RawData.GeonameId = Coalesce(@StateGeonameId,dbo.RawData.GeonameId)
		and dbo.RawData.ASCIIName = Coalesce(@StateName,dbo.RawData.ASCIIName)
		and dbo.RawData.FeatureCategoryId = @FeatureCategoryId
		and dbo.RawData.FeatureCode = @FeatureCode
		Order by dbo.RawData.ASCIIName
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		SELECT
		   dbo.RawData.GeonameId
		  ,dbo.RawData.Name
		  ,dbo.RawData.ASCIIName
		  ,dbo.RawData.AlternateNames
		  ,dbo.RawData.Latitude
		  ,dbo.RawData.Longitude
		  ,dbo.RawData.FeatureCategoryId
		  ,dbo.RawData.FeatureCode
		  ,dbo.RawData.ISOCountryCode
		  ,dbo.RawData.CC2
		  ,dbo.RawData.Admin1Code
		  ,dbo.RawData.Admin2Code
		  ,dbo.RawData.Admin3Code
		  ,dbo.RawData.Admin4Code
		  ,dbo.RawData.Population
		  ,dbo.RawData.Elevation
		  ,dbo.RawData.DEM
		  ,dbo.RawData.TimeZoneId
		  ,dbo.RawData.ModificationDate
		  ,dbo.RawData.RowId
		From dbo.RawData
		inner join dbo.Hierarchy on dbo.Hierarchy.ChildId = dbo.RawData.GeonameId
		inner join dbo.Country on dbo.Country.GeonameId = dbo.Hierarchy.ParentId
		where 
		dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
		and dbo.Country.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.Country.ISOCountryCode)
		and dbo.RawData.GeonameId = Coalesce(@StateGeonameId,dbo.RawData.GeonameId)
		and dbo.RawData.ASCIIName = Coalesce(@StateName,dbo.RawData.ASCIIName)
		and dbo.RawData.FeatureCategoryId = @FeatureCategoryId
		and dbo.RawData.FeatureCode = @FeatureCode
		Order by dbo.RawData.ASCIIName;
	End
End
Go


Create Procedure dbo.GetCitiesInAState	

	@CountryName nvarchar(100) = null -- 'United States'
	,@ISOCountryCode char(2) = null
	,@StateName nvarchar(100) = null
	,@StateGeonameId bigint = null
	,@FeatureCategoryId char(1) = 'A' -- Country, State, Region, etc.
	,@CityGeonameId bigint = null 
	,@CityName nvarchar(100) = null
	,@PageNumber int = null
	,@PageSize int = null
AS
BEGIN

	If(@ISOCountryCode is null)
    Begin
        Select @ISOCountryCode = dbo.Country.ISOCountryCode from dbo.Country Where dbo.Country.CountryName = @CountryName;
    End

	If(@StateGeonameId is null)
	Begin
		Select @StateGeonameId = dbo.RawData.GeonameId from dbo.RawData
		Where dbo.RawData.ASCIIName = @StateName
		and dbo.RawData.ISOCountryCode = @ISOCountryCode
		and dbo.RawData.FeatureCategoryId = @FeatureCategoryId;
	End

	If(@PageSize is not null and @PageNumber is not null)
    Begin
        SELECT
        dbo.RawData.GeonameId
        ,dbo.RawData.Name
        ,dbo.RawData.ASCIIName
        ,dbo.RawData.AlternateNames
        ,dbo.RawData.Latitude
        ,dbo.RawData.Longitude
        ,dbo.RawData.FeatureCategoryId
        ,dbo.RawData.FeatureCode
        ,dbo.RawData.ISOCountryCode
        ,dbo.RawData.CC2
        ,dbo.RawData.Admin1Code
        ,dbo.RawData.Admin2Code
        ,dbo.RawData.Admin3Code
        ,dbo.RawData.Admin4Code
        ,dbo.RawData.Population
        ,dbo.RawData.Elevation
        ,dbo.RawData.DEM
        ,dbo.RawData.TimeZoneId
        ,dbo.RawData.ModificationDate
        ,dbo.RawData.RowId
        FROM dbo.RawData 
        inner join dbo.Hierarchy on dbo.Hierarchy.ChildId = dbo.RawData.GeonameId
        where
        dbo.Hierarchy.ParentId = @StateGeonameId
        and dbo.RawData.GeonameId = Coalesce(@CityGeonameId, dbo.RawData.GeonameId)
        and dbo.RawData.ASCIIName = Coalesce(@CityName, dbo.RawData.ASCIIName)
        Order by dbo.RawData.ASCIIName Asc
        Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
    End

    Else
    Begin
        SELECT
        dbo.RawData.GeonameId
        ,dbo.RawData.Name
        ,dbo.RawData.ASCIIName
        ,dbo.RawData.AlternateNames
        ,dbo.RawData.Latitude
        ,dbo.RawData.Longitude
        ,dbo.RawData.FeatureCategoryId
        ,dbo.RawData.FeatureCode
        ,dbo.RawData.ISOCountryCode
        ,dbo.RawData.CC2
        ,dbo.RawData.Admin1Code
        ,dbo.RawData.Admin2Code
        ,dbo.RawData.Admin3Code
        ,dbo.RawData.Admin4Code
        ,dbo.RawData.Population
        ,dbo.RawData.Elevation
        ,dbo.RawData.DEM
        ,dbo.RawData.TimeZoneId
        ,dbo.RawData.ModificationDate
        ,dbo.RawData.RowId
        FROM dbo.RawData 
        inner join dbo.Hierarchy on dbo.Hierarchy.ChildId = dbo.RawData.GeonameId
        where
        dbo.Hierarchy.ParentId = @StateGeonameId
        --and dbo.RawData.FeatureCategoryId = Coalesce(dbo.Rawdata.FeatureCategoryId ,@FeatureCategoryId)
        --and dbo.RawData.FeatureCode = Coalesce(dbo.RawData.FeatureCode,@FeatureCode)
        and dbo.RawData.GeonameId = Coalesce(@CityGeonameId, dbo.RawData.GeonameId)
        and dbo.RawData.ASCIIName = Coalesce(@CityName, dbo.RawData.ASCIIName)
        Order by dbo.RawData.ASCIIName Asc;
    End
END
Go

-- Exec dbo.GetContinentInfo @ContinentCodeId=null,@GeonameId=null,@Continent=null

Create Procedure dbo.GetContinentInfo  

 @ContinentCodeId char(2) = null
,@GeonameId bigint = null
,@Continent nvarchar(32) = null

As
Begin

	Set NoCount On;

	Select 
	   dbo.Continent.ContinentCodeId      
      ,dbo.Continent.Continent
	  ,dbo.Continent.GeonameId
	  ,dbo.RawData.ASCIIName
	  ,dbo.RawData.AlternateNames
	  ,dbo.RawData.Latitude
	  ,dbo.RawData.Longitude
	  ,dbo.RawData.FeatureCategoryId
	  ,dbo.RawData.FeatureCode
	  ,dbo.RawData.TimeZoneId
	  ,dbo.RawData.RowId
	From dbo.Continent
	Inner Join dbo.RawData
	on dbo.RawData.GeonameId = dbo.Continent.GeonameId
	Where
	dbo.Continent.ContinentCodeId = Coalesce(@ContinentCodeId,dbo.Continent.ContinentCodeId)
	and dbo.Continent.GeonameId = Coalesce(@GeonameId,dbo.Continent.GeonameId)
	and dbo.Continent.Continent = Coalesce(@Continent,dbo.Continent.Continent)
End
Go

-- Exec dbo.GetLanguageInfo @ISO6393Code=null,@Language=null,@PageNumber=2,@PageSize=20

Create Procedure dbo.GetLanguageInfo  

 @ISO6393Code nvarchar(24) = null
,@Language nvarchar(128) = null
,@PageNumber int = null
,@PageSize int = null

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		SELECT 
		dbo.LanguageCode.ISO6393
		,dbo.LanguageCode.ISO6392
		,dbo.LanguageCode.ISO6391
		,dbo.LanguageCode.Language
		,dbo.LanguageCode.RowId
		FROM dbo.LanguageCode
		Where 
		dbo.LanguageCode.ISO6393 = Coalesce(@ISO6393Code,dbo.LanguageCode.ISO6393)
		and dbo.LanguageCode.Language = Coalesce(@Language,dbo.LanguageCode.Language)
		Order by dbo.LanguageCode.Language
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End
	
	Else
	Begin
		SELECT 
		dbo.LanguageCode.ISO6393
		,dbo.LanguageCode.ISO6392
		,dbo.LanguageCode.ISO6391
		,dbo.LanguageCode.Language
		,dbo.LanguageCode.RowId
		FROM dbo.LanguageCode
		Where 
		dbo.LanguageCode.ISO6393 = Coalesce(@ISO6393Code,dbo.LanguageCode.ISO6393)
		and dbo.LanguageCode.Language = Coalesce(@Language,dbo.LanguageCode.Language)
		Order by dbo.LanguageCode.Language
	End
End
Go


Create Procedure dbo.GetFeatureCategoryInfo  

@FeatureCategoryId char(1) = null

As
Begin

	Set NoCount On;

	Select 
	dbo.FeatureCategory.FeatureCategoryId
    ,dbo.FeatureCategory.FeatureCategoryName
	,dbo.FeatureCategory.RowId
	From dbo.FeatureCategory
	Where dbo.FeatureCategory.FeatureCategoryId = Coalesce(@FeatureCategoryId,dbo.FeatureCategory.FeatureCategoryId);

End
Go

-- Exec dbo.GetFeatureCodeInfo  @FeatureCodeId = null,@PageNumber = 2,@PageSize = 20

Create Procedure dbo.GetFeatureCodeInfo  

 @FeatureCodeId nvarchar(16) = null
,@PageNumber int = null
,@PageSize int = null

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		Select 
		 dbo.FeatureCode.FeatureCodeId
		,dbo.FeatureCode.FeatureCodeName
		,dbo.FeatureCode.Description
		,dbo.FeatureCode.RowId
		From dbo.FeatureCode
		Where dbo.FeatureCode.FeatureCodeId = Coalesce(@FeatureCodeId,dbo.FeatureCode.FeatureCodeId)
		Order By dbo.FeatureCode.FeatureCodeId
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		Select 
		dbo.FeatureCode.FeatureCodeId
		,dbo.FeatureCode.FeatureCodeName
		,dbo.FeatureCode.Description
		,dbo.FeatureCode.RowId
		From dbo.FeatureCode
		Where dbo.FeatureCode.FeatureCodeId = Coalesce(@FeatureCodeId,dbo.FeatureCode.FeatureCodeId)
		Order By dbo.FeatureCode.FeatureCodeId
	End
End
Go

Create Procedure dbo.GetCountriesInAContinent

 @ContinentName nvarchar(100) = null
,@ContinentCodeId nvarchar(100) = null
,@GeonameId bigint = null
,@PageNumber int = null
,@PageSize int = null

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		SELECT
		dbo.Country.ISOCountryCode
		,dbo.Country.ISO3Code
		,dbo.Country.ISONumeric
		,dbo.Country.FIPSCode
		,dbo.Country.CountryName
		,dbo.Country.Capital
		,dbo.Country.SqKmArea
		,dbo.Country.TotalPopulation
		,dbo.Country.ContinentCodeId
		,dbo.Country.TopLevelDomain
		,dbo.Country.CurrencyCode
		,dbo.Country.CurrencyName
		,dbo.Country.Phone
		,dbo.Country.PostalFormat
		,dbo.Country.PostalRegex
		,dbo.Country.Languages
		,dbo.Country.GeonameId
		,dbo.Country.Neighbors
		,dbo.Country.EquivalentFipsCode
		,dbo.Country.RowId
		From Geonames.dbo.Country
		Inner Join dbo.Continent on dbo.Continent.ContinentCodeId = dbo.Country.ContinentCodeId
		Where
		dbo.Continent.Continent = Coalesce(@ContinentName,dbo.Continent.Continent)
		and dbo.Continent.ContinentCodeId = Coalesce(@ContinentCodeId,dbo.Continent.ContinentCodeId)
		and dbo.Continent.GeonameId = Coalesce(@GeonameId,dbo.Continent.GeonameId)
		Order by dbo.Country.CountryName
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		SELECT
		dbo.Country.ISOCountryCode
		,dbo.Country.ISO3Code
		,dbo.Country.ISONumeric
		,dbo.Country.FIPSCode
		,dbo.Country.CountryName
		,dbo.Country.Capital
		,dbo.Country.SqKmArea
		,dbo.Country.TotalPopulation
		,dbo.Country.ContinentCodeId
		,dbo.Country.TopLevelDomain
		,dbo.Country.CurrencyCode
		,dbo.Country.CurrencyName
		,dbo.Country.Phone
		,dbo.Country.PostalFormat
		,dbo.Country.PostalRegex
		,dbo.Country.Languages
		,dbo.Country.GeonameId
		,dbo.Country.Neighbors
		,dbo.Country.EquivalentFipsCode
		,dbo.Country.RowId
		From Geonames.dbo.Country
		Inner Join dbo.Continent on dbo.Continent.ContinentCodeId = dbo.Country.ContinentCodeId
		Where
		dbo.Continent.Continent = Coalesce(@ContinentName,dbo.Continent.Continent)
		and dbo.Continent.ContinentCodeId = Coalesce(@ContinentCodeId,dbo.Continent.ContinentCodeId)
		and dbo.Continent.GeonameId = Coalesce(@GeonameId,dbo.Continent.GeonameId)
		Order by dbo.Country.CountryName
	End
End
Go

Create Procedure dbo.GetCountryFeatureCategoryFeatureCode
@ISOCountryCode char(2) = null
,@CountryName nvarchar(200) = null
,@FeatureCategoryId char(1) = null
,@FeatureCode nvarchar(10) = null
,@PageNumber int = null
,@PageSize int = null
AS
BEGIN
	If @ISOCountryCode is null and @CountryName is not null
		Begin

			Select @ISOCountryCode = dbo.Country.ISOCountryCode from dbo.Country
			Where dbo.Country.CountryName = @CountryName;

		End

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
		Begin
			Select 
			dbo.RawData.GeonameId
			,dbo.RawData.Name
			,dbo.RawData.ASCIIName
			,dbo.RawData.AlternateNames
			,dbo.RawData.Latitude
			,dbo.RawData.Longitude
			,dbo.RawData.FeatureCategoryId
			,dbo.RawData.FeatureCode
			,dbo.RawData.ISOCountryCode
			,dbo.RawData.CC2
			,dbo.RawData.Admin1Code
			,dbo.RawData.Admin2Code
			,dbo.RawData.Admin3Code
			,dbo.RawData.Admin4Code
			,dbo.RawData.Population
			,dbo.RawData.Elevation
			,dbo.RawData.DEM
			,dbo.RawData.TimeZoneId
			,dbo.RawData.ModificationDate
			,dbo.RawData.RowId
			from dbo.RawData
			inner join dbo.Country on dbo.RawData.ISOCountryCode = dbo.Country.ISOCountryCode
			Where
			dbo.RawData.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.RawData.ISOCountryCode)
			and 
			dbo.RawData.FeatureCategoryId = Coalesce(@FeatureCategoryId,dbo.RawData.FeatureCategoryId)
			and 
			dbo.RawData.FeatureCode = Coalesce(@FeatureCode,dbo.RawData.FeatureCode)
			Order by dbo.RawData.ASCIIName
			Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;

		End
	Else
		Begin
			Select 
			dbo.RawData.GeonameId
			,dbo.RawData.Name
			,dbo.RawData.ASCIIName
			,dbo.RawData.AlternateNames
			,dbo.RawData.Latitude
			,dbo.RawData.Longitude
			,dbo.RawData.FeatureCategoryId
			,dbo.RawData.FeatureCode
			,dbo.RawData.ISOCountryCode
			,dbo.RawData.CC2
			,dbo.RawData.Admin1Code
			,dbo.RawData.Admin2Code
			,dbo.RawData.Admin3Code
			,dbo.RawData.Admin4Code
			,dbo.RawData.Population
			,dbo.RawData.Elevation
			,dbo.RawData.DEM
			,dbo.RawData.TimeZoneId
			,dbo.RawData.ModificationDate
			,dbo.RawData.RowId
			from dbo.RawData
			inner join dbo.Country on dbo.RawData.ISOCountryCode = dbo.Country.ISOCountryCode
			Where
			dbo.RawData.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.RawData.ISOCountryCode)
			and 
			dbo.RawData.FeatureCategoryId = Coalesce(@FeatureCategoryId,dbo.RawData.FeatureCategoryId)
			and 
			dbo.RawData.FeatureCode = Coalesce(@FeatureCode,dbo.RawData.FeatureCode)
			Order by dbo.RawData.ASCIIName;
		End
END
Go
