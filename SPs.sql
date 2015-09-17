-- Exec dbo.GetDistinctTimeZones

Create Procedure dbo.GetDistinctTimeZones 
	
 @PageNumber	int		= NULL
,@PageSize		int		= NULL

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

 @TimeZoneId		nvarchar(128)	= NULL
,@ISOCountryCode	char(10)		= NULL
,@ISO3Code			char(3)			= NULL
,@ISONumeric		int				= NULL
,@CountryName		nvarchar(200)	= NULL
,@Latitude			float			= NULL
,@Longitude			float			= NULL
,@PageNumber		int				= NULL
,@PageSize			int				= NULL

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

@PlaceName nvarchar(200) = NULL

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

-- Exec dbo.GetCountryInfo @ISOCountryCode = NULL, @CountryName  = NULL, @PageNumber = NULL, @PageSize = NULL

Create Procedure dbo.GetCountryInfo  

 @ISOCountryCode	char(2)			= NULL
,@CountryName		nvarchar(200)	= NULL
,@PageNumber		int				= NULL
,@PageSize			int				= NULL

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

-- Exec dbo.GetStateInfo  @CountryName = NULL,@ISOCountryCode = 'IN',@StateGeonameId = NULL,@StateName = NULL, @FeatureCategoryId = 'A' ,@FeatureCode = 'ADM1',@PageNumber = 2,@PageSize = 20

Create Procedure dbo.GetStateInfo  

 @CountryName		nvarchar(200)	= NULL
,@ISOCountryCode	char(2)			= NULL
,@StateGeonameId	bigint			= NULL
,@StateName			nvarchar(200)	= NULL
,@FeatureCategoryId char(1)			= 'A'
,@FeatureCode		nvarchar(10)	= 'ADM1'
,@PageNumber		int				= NULL
,@PageSize			int				= NULL

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

	@CountryName		nvarchar(100)	= NULL -- 'United States'
	,@ISOCountryCode	char(2)			= NULL
	,@StateName			nvarchar(100)	= NULL
	,@StateGeonameId	bigint			= NULL
	,@FeatureCategoryId char(1)			= 'A' -- Country, State, Region, etc.
	,@CityGeonameId		bigint			= NULL 
	,@CityName			nvarchar(100)	= NULL
	,@PageNumber		int				= NULL
	,@PageSize			int				= NULL
AS
BEGIN

	If(@ISOCountryCode is NULL)
    Begin
        Select @ISOCountryCode = dbo.Country.ISOCountryCode from dbo.Country Where dbo.Country.CountryName = @CountryName;
    End

	If(@StateGeonameId is NULL)
	Begin
		Select @StateGeonameId = dbo.RawData.GeonameId from dbo.RawData
		Where dbo.RawData.ASCIIName = @StateName
		and dbo.RawData.ISOCountryCode = @ISOCountryCode
		and dbo.RawData.FeatureCategoryId = @FeatureCategoryId;
	End

	If(@PageSize is not NULL and @PageNumber is not NULL)
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

-- Exec dbo.GetContinentInfo @ContinentCodeId=NULL,@GeonameId=NULL,@Continent=NULL

Create Procedure dbo.GetContinentInfo  

 @ContinentCodeId	char(2)			= NULL
,@GeonameId			bigint			= NULL
,@Continent			nvarchar(32)	= NULL

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

-- Exec dbo.GetLanguageInfo @ISO6393Code=NULL,@Language=NULL,@PageNumber=2,@PageSize=20

Create Procedure dbo.GetLanguageInfo  

 @ISO6393Code	nvarchar(24)	= NULL
,@Language		nvarchar(128)	= NULL
,@PageNumber	int				= NULL
,@PageSize		int				= NULL

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

@FeatureCategoryId char(1) = NULL

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

-- Exec dbo.GetFeatureCodeInfo  @FeatureCodeId = NULL,@PageNumber = 2,@PageSize = 20

Create Procedure dbo.GetFeatureCodeInfo  

 @FeatureCodeId		nvarchar(16)	= NULL
,@PageNumber		int				= NULL
,@PageSize			int				= NULL

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

 @ContinentName		nvarchar(100)	= NULL
,@ContinentCodeId	nvarchar(100)	= NULL
,@GeonameId			bigint			= NULL
,@PageNumber		int				= NULL
,@PageSize			int				= NULL

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
@ISOCountryCode			char(2)			= NULL
,@CountryName			nvarchar(200)	= NULL
,@FeatureCategoryId		char(1)			= NULL
,@FeatureCode			nvarchar(10)	= NULL
,@PageNumber			int				= NULL
,@PageSize				int				= NULL
AS
BEGIN
	If @ISOCountryCode is NULL and @CountryName is not NULL
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

Create Procedure [dbo].[GetPostalCodeInfo]

	@ISOCountryCode		char(2)			= NULL
	,@CountryName		nvarchar(100)	= NULL
	,@PostalCode		nvarchar(20)	= NULL
	,@PageSize			int				= NULL
	,@PageNumber		int				= NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF @PageSize is not NULL and @PageNumber is not NULL
		Begin
		  SELECT dbo.RawPostal.ISOCountryCode
		  ,dbo.RawPostal.PostalCode
		  ,dbo.RawPostal.PlaceName
		  ,dbo.RawPostal.Admin1Name
		  ,dbo.RawPostal.Admin1Code
		  ,dbo.RawPostal.Admin2Name
		  ,dbo.RawPostal.Admin2Code
		  ,dbo.RawPostal.Admin3Name
		  ,dbo.RawPostal.Admin3Code
		  ,dbo.RawPostal.Latitude
		  ,dbo.RawPostal.Longitude
		  ,dbo.RawPostal.Accuracy
		  ,dbo.RawPostal.RowId
		  FROM dbo.RawPostal
		  Inner Join dbo.Country on dbo.Country.ISOCountryCode = dbo.RawPostal.ISOCountryCode
		  Where dbo.RawPostal.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.RawPostal.ISOCountryCode)
		  and dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
		  and dbo.RawPostal.PostalCode = Coalesce(@PostalCode,dbo.RawPostal.PostalCode)
		  Order by dbo.RawPostal.PostalCode
		  Offset @PageSize*(@PageNumber-1) Rows Fetch Next @PageSize Rows Only;
		End
		Else
		Begin
			SELECT dbo.RawPostal.ISOCountryCode
			  ,dbo.RawPostal.PostalCode
			  ,dbo.RawPostal.PlaceName
			  ,dbo.RawPostal.Admin1Name
			  ,dbo.RawPostal.Admin1Code
			  ,dbo.RawPostal.Admin2Name
			  ,dbo.RawPostal.Admin2Code
			  ,dbo.RawPostal.Admin3Name
			  ,dbo.RawPostal.Admin3Code
			  ,dbo.RawPostal.Latitude
			  ,dbo.RawPostal.Longitude
			  ,dbo.RawPostal.Accuracy
			  ,dbo.RawPostal.RowId
			  FROM dbo.RawPostal
			  Inner Join dbo.Country on dbo.Country.ISOCountryCode = dbo.RawPostal.ISOCountryCode
			  Where dbo.RawPostal.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.RawPostal.ISOCountryCode)
			  and dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
			  and dbo.RawPostal.PostalCode = Coalesce(@PostalCode,dbo.RawPostal.PostalCode)
			  Order by dbo.RawPostal.PostalCode;
		End
END
Go

