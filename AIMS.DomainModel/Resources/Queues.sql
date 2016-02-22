

CREATE QUEUE [dbo].Generic_SendQueue
GO
CREATE SERVICE TX_Generic
ON QUEUE [dbo].Generic_SendQueue ([DEFAULT])
GO

CREATE QUEUE [dbo].QTransactionTrigger
GO
CREATE SERVICE RX_TransactionTrigger
ON QUEUE [dbo].QTransactionTrigger ([DEFAULT])
GO

ALTER QUEUE [dbo].[QTransactionTrigger] WITH STATUS = ON , RETENTION = OFF , POISON_MESSAGE_HANDLING (STATUS = OFF) 
go
