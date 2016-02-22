IF EXISTS (SELECT * FROM sysobjects WHERE name = 'EnqueueMessage')
	DROP PROCEDURE [dbo].[EnqueueMessage];
GO

CREATE PROCEDURE [dbo].[EnqueueMessage]
(	
	@Service varchar(100),
	@ID int
)
AS
SET NOCOUNT ON;

DECLARE @dialog uniqueidentifier

BEGIN DIALOG  @dialog
FROM SERVICE TX_Generic
TO SERVICE @Service
ON CONTRACT [DEFAULT]
WITH ENCRYPTION = OFF;

SEND ON CONVERSATION @dialog
MESSAGE TYPE [DEFAULT] (@ID)


GO

