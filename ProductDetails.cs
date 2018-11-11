using System;

namespace Better_Nike_Bot
{
	// Token: 0x02000025 RID: 37
	public class ProductDetails
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00002EA3 File Offset: 0x000010A3
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00002EAB File Offset: 0x000010AB
		public string Ticks { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00002EB4 File Offset: 0x000010B4
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00002EBC File Offset: 0x000010BC
		public string CatalogId
		{
			get
			{
				return this._catalogId;
			}
			set
			{
				this._catalogId = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00002EC5 File Offset: 0x000010C5
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00002ECD File Offset: 0x000010CD
		public string ProductId { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00002ED6 File Offset: 0x000010D6
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00002EDE File Offset: 0x000010DE
		public string Price { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00002EE7 File Offset: 0x000010E7
		public string SiteId
		{
			get
			{
				return "null";
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00002EEE File Offset: 0x000010EE
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00002EF6 File Offset: 0x000010F6
		public string Line1 { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00002EFF File Offset: 0x000010FF
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00002F07 File Offset: 0x00001107
		public string Line2 { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00002F10 File Offset: 0x00001110
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00002F18 File Offset: 0x00001118
		public string SkuAndSize { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00020B4C File Offset: 0x0001ED4C
		public string Quantity
		{
			get
			{
				return 1.ToString();
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00002F21 File Offset: 0x00001121
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00002F29 File Offset: 0x00001129
		public string SkuId { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00002F32 File Offset: 0x00001132
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00002F3A File Offset: 0x0000113A
		public string Size { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00002F43 File Offset: 0x00001143
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00002F4B File Offset: 0x0000114B
		public string StyleCode { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00002F54 File Offset: 0x00001154
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00002F5C File Offset: 0x0000115C
		public string ProductImage { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00002F65 File Offset: 0x00001165
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00002F6D File Offset: 0x0000116D
		public string SnkrsToken { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00002F76 File Offset: 0x00001176
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00002F7E File Offset: 0x0000117E
		public string NikeWho { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00002F87 File Offset: 0x00001187
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00002F8F File Offset: 0x0000118F
		public string NikeBirthmark { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00002F98 File Offset: 0x00001198
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00002FA0 File Offset: 0x000011A0
		public string SnkrsShopperToken { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00002FA9 File Offset: 0x000011A9
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00002FB1 File Offset: 0x000011B1
		public string SnkrsUrl { get; set; }

		// Token: 0x04000191 RID: 401
		private string _catalogId = "1";
	}
}
