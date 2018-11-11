using System;
using IMLokesh.Extensions;
using IMLokesh.HttpUtility;

namespace Better_Nike_Bot
{
	// Token: 0x02000024 RID: 36
	public class AtcItem
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00020898 File Offset: 0x0001EA98
		public AtcItem(Account acc, string url, string size)
		{
			this.Account = acc;
			this.Size = size;
			this.Url = url;
			this.Details = new ProductDetails();
			this.Attempts = (this.CaptchaAttempts = 0);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00002DB3 File Offset: 0x00000FB3
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00002DBB File Offset: 0x00000FBB
		public Account Account { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00002DC4 File Offset: 0x00000FC4
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00002DCC File Offset: 0x00000FCC
		public string Size { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00002DD5 File Offset: 0x00000FD5
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00002DDD File Offset: 0x00000FDD
		public string Url { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00002DE6 File Offset: 0x00000FE6
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00002DEE File Offset: 0x00000FEE
		public string ServerKey { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00002DF7 File Offset: 0x00000FF7
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00002DFF File Offset: 0x00000FFF
		public string AuthToken { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00002E08 File Offset: 0x00001008
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00002E10 File Offset: 0x00001010
		public string String_0 { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00002E19 File Offset: 0x00001019
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00002E21 File Offset: 0x00001021
		public string String_1 { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00002E2A File Offset: 0x0000102A
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00002E32 File Offset: 0x00001032
		public string String_2 { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00002E3B File Offset: 0x0000103B
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00002E43 File Offset: 0x00001043
		public string SnkrsBuyUrl { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00002E4C File Offset: 0x0000104C
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00002E54 File Offset: 0x00001054
		public ProductDetails Details { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00002E5D File Offset: 0x0000105D
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00002E65 File Offset: 0x00001065
		public int Attempts { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00002E6E File Offset: 0x0000106E
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00002E76 File Offset: 0x00001076
		public int CaptchaAttempts { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00002E7F File Offset: 0x0000107F
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00002E87 File Offset: 0x00001087
		public ShouldStop ShouldStop { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000208DC File Offset: 0x0001EADC
		public string AtcUrl
		{
			get
			{
				string text = NikeUrls.NikeAtc;
				text = text.With(new object[]
				{
					this.Details.Ticks,
					this.Details.Ticks,
					this.Details.CatalogId,
					this.Details.ProductId,
					this.Details.Price.UrlEncode(false),
					this.Details.SiteId,
					this.Details.Line1.UrlEncode(false),
					this.Details.Line2.UrlEncode(false),
					this.Details.SkuAndSize.UrlEncode(false),
					this.Details.Quantity,
					this.Details.SkuId,
					this.Details.Size.UrlEncode(false),
					this.Details.Ticks
				});
				return this.ServerKey.IsNullOrWhiteSpace() ? text : "{0}&auth_token={1}&server_key={2}&status=solved&captchaIsValidated=true".With(new object[]
				{
					text,
					this.AuthToken,
					this.ServerKey
				});
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00020A14 File Offset: 0x0001EC14
		public string AtcWaitUrl
		{
			get
			{
				string s = this.ServerKey.IsNullOrWhiteSpace() ? NikeUrls.NikeAtcWaitNoCaptcha : NikeUrls.NikeAtcWait;
				return s.With(new object[]
				{
					this.Details.Ticks,
					this.Details.Ticks,
					this.Details.CatalogId,
					this.Details.ProductId,
					this.Details.Price.UrlEncode(false),
					this.Details.SiteId,
					this.Details.Line1.UrlEncode(false),
					this.Details.Line2.UrlEncode(false),
					this.Details.SkuAndSize.UrlEncode(false),
					this.Details.Quantity,
					this.Details.SkuId,
					this.Details.Size.UrlEncode(false),
					this.Details.Ticks,
					this.String_0,
					this.String_2,
					this.AuthToken,
					this.ServerKey
				});
			}
		}
	}
}
