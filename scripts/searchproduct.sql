
/****** Object:  StoredProcedure [dbo].[SearchProduct]    Script Date: 06/05/2013 22:56:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

if object_id('[dbo].[SearchProduct]') is not null
 drop procedure [dbo].[SearchProduct]
 go

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SearchProduct] 
	-- Add the parameters for the stored procedure here

	@productmark nvarchar(128) = null, 
	@companyid int = null,
	@companyname nvarchar(128) = null,
	@saleman nvarchar(128) = null,
	@pageindex int = 1,
	@pagesize int = 10
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	if @productmark is not null
	begin
	select COUNT(1)/@pagesize as TotalPage, @pageindex as CurrentIndex, @pagesize as PageSize
	from dbo.Product p 
	left join dbo.[User] u on p.UserId = u.UserId
	left join dbo.Company c on u.CompanyId = c.CompanyId
	left join UserProfile up on u.UserProfileId = up.UserProfileId
	where p.ProductMark like '%'+@productmark+'%' escape '/';
	
	
	select p.ProductId, p.ProductMark, p.ProductAdditions Setting
	, p.DeliveryPrice as price, p.ProductStock, isnull(up.NickName, u.UserName) as UserName, c.CompanyName, up.Tel as TelNumber
	from dbo.Product p 
	left join dbo.[User] u on p.UserId = u.UserId
	left join dbo.Company c on u.CompanyId = c.CompanyId
	left join UserProfile up on u.UserProfileId = up.UserProfileId
	where p.ProductMark like '%'+@productmark+'%' escape '/';
	
	end
	else if @saleman is not null
	begin
	select (COUNT(1)+@pagesize-1)/@pagesize as TotalPage, @pageindex as CurrentIndex, @pagesize as PageSize
	from dbo.Product p 
	left join dbo.[User] u on p.UserId = u.UserId
	left join UserProfile up on u.UserProfileId = up.UserProfileId
	where u.UserName = @saleman;
	
	;with r as (
	select ROW_NUMBER() over (order by p.Updatetime desc) od, p.ProductId, p.ProductMark, p.ProductAdditions Setting
	, p.DeliveryPrice, p.ProductStock, isnull(up.NickName, u.UserName) as UserName, up.Tel as TelNumber
	from dbo.Product p 
	left join dbo.[User] u on p.UserId = u.UserId
	left join UserProfile up on u.UserProfileId = up.UserProfileId
	where u.UserName = @saleman)
	select r.ProductId, r.ProductMark,r.Setting,r.ProductStock,r.TelNumber,r.UserName, r.DeliveryPrice from r
	where od > @pageindex * @pagesize-@pagesize and od<=@pageindex * @pagesize;
	
	end
	else
	begin
	select (COUNT(1)+@pagesize-1)/@pagesize as TotalPage, @pageindex as CurrentIndex, @pagesize as PageSize
	from dbo.Product p 
	left join dbo.[User] u on p.UserId = u.UserId
	left join dbo.Company c on u.CompanyId = c.CompanyId
	left join UserProfile up on u.UserProfileId = up.UserProfileId
	where c.CompanyId = @companyid or c.CompanyName = @companyname;
	
	;with r as (
	select ROW_NUMBER() over (order by p.Updatetime desc) od, p.ProductId, p.ProductMark, p.ProductAdditions Setting
	, p.DeliveryPrice, p.ProductStock, isnull(up.NickName, u.UserName) as UserName, up.Tel as TelNumber
	from dbo.Product p 
	left join dbo.[User] u on p.UserId = u.UserId
	left join dbo.Company c on u.CompanyId = c.CompanyId
	left join UserProfile up on u.UserProfileId = up.UserProfileId
	where c.CompanyId = @companyid or c.CompanyName = @companyname)
	select r.ProductId, r.ProductMark,r.Setting,r.ProductStock,r.TelNumber,r.UserName, r.DeliveryPrice from r
	where od > @pageindex * @pagesize-@pagesize and od<=@pageindex * @pagesize
	;
	end
END


GO

