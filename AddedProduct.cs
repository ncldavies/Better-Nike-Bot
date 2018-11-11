using System;

namespace Better_Nike_Bot
{
	// Token: 0x0200001C RID: 28
	public class AddedProduct
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00013F30 File Offset: 0x00012130
		public AddedProduct(DateTime timeStamp, string productName, string styleCode, string productType, string emailAddress, string size, string pass, string snkrs, Account acc)
		{
			this.TimeStamp = timeStamp;
			this.ProductName = productName;
			this.StyleCode = styleCode;
			this.ProductType = productType;
			this.EmailAddress = emailAddress;
			this.Size = size;
			this.Password = pass;
			this.SnkrsUrl = snkrs;
			this.Account = acc;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002A03 File Offset: 0x00000C03
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00002A0B File Offset: 0x00000C0B
		public DateTime TimeStamp { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00002A14 File Offset: 0x00000C14
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00002A1C File Offset: 0x00000C1C
		public string ProductName { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00002A25 File Offset: 0x00000C25
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00002A2D File Offset: 0x00000C2D
		public string StyleCode { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00002A36 File Offset: 0x00000C36
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00002A3E File Offset: 0x00000C3E
		public string ProductType { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00002A47 File Offset: 0x00000C47
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00002A4F File Offset: 0x00000C4F
		public string EmailAddress { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00002A58 File Offset: 0x00000C58
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00002A60 File Offset: 0x00000C60
		public string Size { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00002A69 File Offset: 0x00000C69
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00002A71 File Offset: 0x00000C71
		public string Password { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00002A7A File Offset: 0x00000C7A
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00002A82 File Offset: 0x00000C82
		public string SnkrsUrl { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00002A8B File Offset: 0x00000C8B
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00002A93 File Offset: 0x00000C93
		public Account Account { get; set; }
	}
}
