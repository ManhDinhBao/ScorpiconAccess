using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallSQL
{
    public class SQLTableQuery
    {
		public string strCreateTableCard = @"CREATE TABLE [dbo].[LCard](
	[CardNumber][int] IDENTITY(1,1) NOT NULL,
	[CardSerial] [varchar] (12) NOT NULL,
	[Holder] [int] NULL,
	[PinCode] [int] NULL,
	[Type] [int] NOT NULL,
	[STime] [datetime] NOT NULL,
	[ETime] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,CONSTRAINT[PK_LCard] PRIMARY KEY CLUSTERED 
	(
		[CardNumber] ASC
	)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
	) ON[PRIMARY]";
    }
}
