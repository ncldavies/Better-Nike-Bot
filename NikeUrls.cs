using System;
using IMLokesh.Extensions;
using Newtonsoft.Json.Linq;

namespace Better_Nike_Bot
{
	// Token: 0x02000028 RID: 40
	public static class NikeUrls
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00020FD4 File Offset: 0x0001F1D4
		public static string[] MatchUrls
		{
			get
			{
				return new string[]
				{
					"store.nike.com/us/en_US/pd/",
					"store.nike.com/gb/en_GB/pw/",
					"store.nike.com/au/en_GB/pw/",
					"store.nike.com/cn/zh_CN/pd/",
					"store.nike.com/jp/ja_JP/pw/",
					"store.nike.com/fr/fr_FR/pd/",
					"store.nike.com/de/de_DE/pw/",
					"store.nike.com/it/it_IT/pd/",
					"store.nike.com/us/en_US/pw/",
					"store.nike.com/gb/en_GB/pd/",
					"store.nike.com/au/en_GB/pd/",
					"store.nike.com/cn/zh_CN/pw/",
					"store.nike.com/jp/ja_JP/pd/",
					"store.nike.com/fr/fr_FR/pw/",
					"store.nike.com/de/de_DE/pd/",
					"store.nike.com/it/it_IT/pw/",
					"www.nike.com/snkrs",
					"/t/",
					"store.nike.com/pl/pl_PL/pd/",
					"store.nike.com/pl/pl_PL/pw/"
				};
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00021098 File Offset: 0x0001F298
		public static string NikeCountryCode
		{
			get
			{
				switch (Form1.SiteType)
				{
				case SiteType.NikeUS:
					return "US";
				case SiteType.NikeUK:
					return "GB";
				case SiteType.NikeCN:
					return "CN";
				case SiteType.NikeJP:
					return "JP";
				case SiteType.NikeFR:
					return "FR";
				case SiteType.NikeDE:
					return "DE";
				case SiteType.NikeAU:
					return "AU";
				case SiteType.NikeIT:
					return "IT";
				case SiteType.NikePL:
					return "PL";
				case SiteType.NikeCA:
					return "CA";
				case SiteType.NikeNL:
					return "NL";
				case SiteType.NikeES:
					return "ES";
				case SiteType.NikeSE:
					return "SE";
				case SiteType.NikeDK:
					return "DK";
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00021098 File Offset: 0x0001F298
		public static string SnkrsCountryCode
		{
			get
			{
				switch (Form1.SiteType)
				{
				case SiteType.NikeUS:
					return "US";
				case SiteType.NikeUK:
					return "GB";
				case SiteType.NikeCN:
					return "CN";
				case SiteType.NikeJP:
					return "JP";
				case SiteType.NikeFR:
					return "FR";
				case SiteType.NikeDE:
					return "DE";
				case SiteType.NikeAU:
					return "AU";
				case SiteType.NikeIT:
					return "IT";
				case SiteType.NikePL:
					return "PL";
				case SiteType.NikeCA:
					return "CA";
				case SiteType.NikeNL:
					return "NL";
				case SiteType.NikeES:
					return "ES";
				case SiteType.NikeSE:
					return "SE";
				case SiteType.NikeDK:
					return "DK";
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00021144 File Offset: 0x0001F344
		public static string SnkrsCurrency
		{
			get
			{
				switch (Form1.SiteType)
				{
				case SiteType.NikeUS:
					return "USD";
				case SiteType.NikeUK:
					return "GBP";
				case SiteType.NikeCN:
					return "CNY";
				case SiteType.NikeJP:
					return "JPY";
				case SiteType.NikeFR:
					return "EUR";
				case SiteType.NikeDE:
					return "EUR";
				case SiteType.NikeAU:
					return "AU";
				case SiteType.NikeIT:
					return "EUR";
				case SiteType.NikePL:
					return "PLN";
				case SiteType.NikeCA:
					return "CA";
				case SiteType.NikeNL:
					return "EUR";
				case SiteType.NikeES:
					return "EUR";
				case SiteType.NikeSE:
					return "SEK";
				case SiteType.NikeDK:
					return "DKK";
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000211F0 File Offset: 0x0001F3F0
		public static string NikeCountrySmallCode
		{
			get
			{
				switch (Form1.SiteType)
				{
				case SiteType.NikeUS:
					return "us";
				case SiteType.NikeUK:
					return "gb";
				case SiteType.NikeCN:
					return "cn";
				case SiteType.NikeJP:
					return "jp";
				case SiteType.NikeFR:
					return "fr";
				case SiteType.NikeDE:
					return "de";
				case SiteType.NikeAU:
					return "au";
				case SiteType.NikeIT:
					return "it";
				case SiteType.NikePL:
					return "pl";
				case SiteType.NikeCA:
					return "ca";
				case SiteType.NikeNL:
					return "nl";
				case SiteType.NikeES:
					return "es";
				case SiteType.NikeSE:
					return "se";
				case SiteType.NikeDK:
					return "dk";
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0002129C File Offset: 0x0001F49C
		public static string NikeCountryServiceCode
		{
			get
			{
				switch (Form1.SiteType)
				{
				case SiteType.NikeUS:
					return "us";
				case SiteType.NikeUK:
					return "eu";
				case SiteType.NikeCN:
					return "ap";
				case SiteType.NikeJP:
					return "ap";
				case SiteType.NikeFR:
					return "eu";
				case SiteType.NikeDE:
					return "eu";
				case SiteType.NikeAU:
					return "ap";
				case SiteType.NikeIT:
					return "eu";
				case SiteType.NikePL:
					return "eu";
				case SiteType.NikeCA:
					return "ap";
				case SiteType.NikeNL:
					return "eu";
				case SiteType.NikeES:
					return "eu";
				case SiteType.NikeSE:
					return "eu";
				case SiteType.NikeDK:
					return "eu";
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00021348 File Offset: 0x0001F548
		public static string NikeLangLocale
		{
			get
			{
				switch (Form1.SiteType)
				{
				case SiteType.NikeUS:
					return "en_US";
				case SiteType.NikeUK:
					return "en_GB";
				case SiteType.NikeCN:
					return "zh_CN";
				case SiteType.NikeJP:
					return "ja_JP";
				case SiteType.NikeFR:
					return "fr_FR";
				case SiteType.NikeDE:
					return "de_DE";
				case SiteType.NikeAU:
					return "en_GB";
				case SiteType.NikeIT:
					return "it_IT";
				case SiteType.NikePL:
					return "pl_PL";
				case SiteType.NikeCA:
					return "en_GB";
				case SiteType.NikeNL:
					return "en_GB";
				case SiteType.NikeES:
					return "es_ES";
				case SiteType.NikeSE:
					return "en_GB";
				case SiteType.NikeDK:
					return "en_GB";
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000213F4 File Offset: 0x0001F5F4
		public static string NikeCartSummary
		{
			get
			{
				return Form1.RemoteSettings["NikeCartSummary_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountryServiceCode,
					NikeUrls.NikeLangLocale,
					NikeUrls.NikeCountryCode,
					DateTime.Now.Ticks.ToString()
				});
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00021454 File Offset: 0x0001F654
		public static string NikeLogin
		{
			get
			{
				return Form1.RemoteSettings["NikeLogin_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeLangLocale
				});
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0002148C File Offset: 0x0001F68C
		public static string NikeStore
		{
			get
			{
				return Form1.RemoteSettings["NikeStore_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeLangLocale.ToLower()
				});
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000214D0 File Offset: 0x0001F6D0
		public static string NikeAtc
		{
			get
			{
				return Form1.RemoteSettings["NikeAtc_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountryServiceCode,
					NikeUrls.NikeLangLocale,
					NikeUrls.NikeCountryCode
				});
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00021518 File Offset: 0x0001F718
		public static string NikeAtcWait
		{
			get
			{
				return Form1.RemoteSettings["NikeAtcWait_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountryServiceCode,
					NikeUrls.NikeLangLocale,
					NikeUrls.NikeCountryCode
				});
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00021560 File Offset: 0x0001F760
		public static string NikeAtcWaitNoCaptcha
		{
			get
			{
				return Form1.RemoteSettings["NikeAtcWaitNoCaptcha_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountryServiceCode,
					NikeUrls.NikeLangLocale,
					NikeUrls.NikeCountryCode
				});
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000215A8 File Offset: 0x0001F7A8
		public static string NikeCart
		{
			get
			{
				return Form1.RemoteSettings["NikeCart_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeCountryCode
				});
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000215E8 File Offset: 0x0001F7E8
		public static string NikeCartDargs
		{
			get
			{
				return Form1.RemoteSettings["NikeCartDargs_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeCountryServiceCode
				});
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00021628 File Offset: 0x0001F828
		public static string NikeCartLockerDargs
		{
			get
			{
				return Form1.RemoteSettings["NikeCartLockerDargs_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeCountryServiceCode
				});
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00021668 File Offset: 0x0001F868
		public static string NikeCreateCaptcha
		{
			get
			{
				return Form1.RemoteSettings["NikeCreateCaptcha_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountryServiceCode
				});
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000216A0 File Offset: 0x0001F8A0
		public static string NikeLoadSkus
		{
			get
			{
				return Form1.RemoteSettings["NikeLoadSkus_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountryServiceCode,
					NikeUrls.NikeLangLocale,
					NikeUrls.NikeCountryCode
				});
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000216E8 File Offset: 0x0001F8E8
		public static string NikeInventory
		{
			get
			{
				return Form1.RemoteSettings["NikeInventory_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountryServiceCode,
					NikeUrls.NikeLangLocale,
					NikeUrls.NikeCountryCode
				});
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00021730 File Offset: 0x0001F930
		public static string NikeSearch
		{
			get
			{
				return Form1.RemoteSettings["NikeSearch_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeLangLocale.ToLower()
				});
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00021774 File Offset: 0x0001F974
		public static string NikeLoadProduct
		{
			get
			{
				return Form1.RemoteSettings["NikeLoadProduct_url"].Value<string>().With(new object[]
				{
					NikeUrls.NikeCountryServiceCode,
					NikeUrls.NikeLangLocale,
					NikeUrls.NikeCountryCode
				});
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000217BC File Offset: 0x0001F9BC
		public static string NikeWishlistService
		{
			get
			{
				return "https://secure-store.nike.com/{0}/services/wishlistService?{1}".With(new object[]
				{
					NikeUrls.NikeCountryServiceCode,
					DateTime.Now.Ticks.ToString()
				});
			}
		}
	}
}
