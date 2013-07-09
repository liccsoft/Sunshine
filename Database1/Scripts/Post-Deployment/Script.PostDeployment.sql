/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- 公司类型 初始数据
SET IDENTITY_INSERT TradeKinds on;
GO
if not exists(select 1 from TradeKinds where TraderKindId in (0,1,2,3,4))
insert into TradeKinds (TraderKindId, [TraderKindName]
           ,[Description])
values
(0,'未定义','尚未定义'),
(1,'一级代理商','一级代理商'),
(2,'二级代理商','二级代理商'),
(3,'三级代理商','三级代理商'),
(4,'零售商','零售商')
GO
SET IDENTITY_INSERT TradeKinds off;
GO
--
-- 分类初始数据
if not exists (select 1 from [dbo].[Category] where [CategoryLevel] = 0)
INSERT INTO [dbo].[Category]
           ([ParentCategoryId]
           ,[CategoryLevel]
           ,[Description]
           ,[CategoryName]
           ,[Title])
     VALUES
           (null
           ,0
           ,'更目录，仅用于结构的根，不做其他用处'
           ,'root'
           ,'根目录')
GO