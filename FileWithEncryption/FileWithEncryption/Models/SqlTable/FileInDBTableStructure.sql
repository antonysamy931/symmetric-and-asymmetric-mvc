USE [POC]
GO

/****** Object:  Table [dbo].[FileUploadDB]    Script Date: 01/22/2015 12:10:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FileUploadDB](
	[asymfileid] [int] IDENTITY(1000,1) NOT NULL,
	[name] [varchar](max) NULL,
	[type] [varchar](max) NULL,
	[filedata] [varbinary](max) NULL,
	[symmentryKey] [varchar](max) NULL,
	[asymprivatepublicKey] [varchar](max) NULL,
 CONSTRAINT [PK_FileUpload] PRIMARY KEY CLUSTERED 
(
	[asymfileid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


