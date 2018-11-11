using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using CsQuery;
using IMLokesh.Extensions;
using IMLokesh.Http;
using IMLokesh.HttpUtility;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Better_Nike_Bot
{
	// Token: 0x02000014 RID: 20
	public class Account
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00002763 File Offset: 0x00000963
		public Account()
		{
			this.Request = (this.Request ?? new HttpHelper("", true, null, 15, null));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000117EC File Offset: 0x0000F9EC
		public Account(Account acc)
		{
			this.EmailAddress = acc.EmailAddress;
			this.Password = acc.Password;
			this.Size = acc.Size;
			this.Keywords = acc.Keywords;
			this.CollectionKeywords = acc.CollectionKeywords;
			this.EarlyLinks = acc.EarlyLinks;
			this.ProductStyleCodes = acc.ProductStyleCodes;
			this.Request = (this.Request ?? new HttpHelper("", true, null, 15, null));
			this.NotificationEmail = acc.NotificationEmail;
			this.NotificationText = acc.NotificationText;
			this.CheckoutInfo = acc.CheckoutInfo;
			this.SnkrsExploit = acc.SnkrsExploit;
			this.IsGuest = acc.IsGuest;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000118B0 File Offset: 0x0000FAB0
		public Account(string emailID, string password, string size, string keywords, string collectionKeywords, string earlyLink, string productStyleCode, string notificationEmail, string notificationCarrier, string notificationNumber, CheckoutInfo cInfo, bool isGuest = false)
		{
			this.EmailAddress = emailID.ToLower();
			this.Password = password;
			this.Size = (from s in size.Split(new char[]
			{
				','
			})
			select s.Trim().Replace(".0", "").TrimStart(new char[]
			{
				'0'
			})).ToArray<string>();
			this.Keywords = (keywords.IsNullOrWhiteSpace() ? new List<string>() : keywords.ToLower().Split(new char[]
			{
				','
			}).TrimAll().ToList<string>());
			this.CollectionKeywords = (collectionKeywords.IsNullOrWhiteSpace() ? new List<string>() : collectionKeywords.ToLower().Split(new char[]
			{
				','
			}).TrimAll().ToList<string>());
			this.EarlyLinks = (earlyLink.IsNullOrWhiteSpace() ? new List<string>() : earlyLink.Split(new char[]
			{
				','
			}).TrimAll().ToList<string>());
			this.ProductStyleCodes = (productStyleCode.IsNullOrWhiteSpace() ? new List<string>() : productStyleCode.Split(new char[]
			{
				','
			}).TrimAll().ToList<string>());
			this.Request = (this.Request ?? new HttpHelper("", true, null, 15, null));
			this.NotificationEmail = notificationEmail;
			this.CheckoutInfo = cInfo;
			this.SnkrsExploit = false;
			this.IsGuest = isGuest;
			if (!notificationCarrier.IsNullOrWhiteSpace() && !notificationNumber.IsNullOrWhiteSpace())
			{
				if (notificationNumber.Length != 10)
				{
					throw new Exception("Notification mobile number must be a 10 digit number. {0}".With(new object[]
					{
						notificationNumber
					}));
				}
				try
				{
					notificationNumber.ParseToLong();
				}
				catch (Exception)
				{
					throw new Exception("Notification mobile number must be a 10 digit number. {0}".With(new object[]
					{
						notificationNumber
					}));
				}
				NotificationService notificationService = NotificationSettings.AvailableTextNotificationServices.FirstOrDefault((NotificationService n) => n.Carrier.ToLower().StartsWith(notificationCarrier.ToLower()));
				if (notificationService.IsNull())
				{
					throw new Exception("Invalid mobile carrier. {0}".With(new object[]
					{
						notificationCarrier
					}));
				}
				this.NotificationText = notificationNumber + notificationService.EmailSuffix;
			}
			if (!this.EmailAddress.Contains("@") && this.EmailAddress != "demo")
			{
				throw new Exception("An email address must contain '@' symbol.");
			}
			if (!this.NotificationEmail.IsNullOrWhiteSpace() && !this.NotificationEmail.Contains("@"))
			{
				throw new Exception("An email address must contain '@' symbol.");
			}
			if (this.EmailAddress.IsNullOrWhiteSpace() || this.Password.IsNullOrWhiteSpace() || this.Size[0].IsNullOrWhiteSpace())
			{
				throw new Exception("Email, Password and Size are required fields.");
			}
			if (!earlyLink.IsNullOrWhiteSpace())
			{
				if (earlyLink.Split(new char[]
				{
					','
				}).TrimAll().Any((string s) => !s.StartsWith("http")))
				{
					throw new Exception("Invalid early link found. An early link must start with http://");
				}
			}
			if (!earlyLink.IsNullOrWhiteSpace() && earlyLink.Contains("/t/") && productStyleCode.IsNullOrWhiteSpace())
			{
				throw new Exception("For new nike links, please enter the style code also. If you are running for snkrs only enter style code. Otherwise enter both link and style code.");
			}
			if (isGuest && this.IsWebSnkrs)
			{
				throw new Exception("You cannot use guest account for web snkrs. Please enter a product link.");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000278A File Offset: 0x0000098A
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00002792 File Offset: 0x00000992
		public string EmailAddress { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000279B File Offset: 0x0000099B
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000027A3 File Offset: 0x000009A3
		public string Password { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000027AC File Offset: 0x000009AC
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000027B4 File Offset: 0x000009B4
		public string[] Size { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000027BD File Offset: 0x000009BD
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000027C5 File Offset: 0x000009C5
		public bool IsGuest { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000027CE File Offset: 0x000009CE
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000027D6 File Offset: 0x000009D6
		public List<string> Keywords { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000027DF File Offset: 0x000009DF
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000027E7 File Offset: 0x000009E7
		public List<string> CollectionKeywords { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000027F0 File Offset: 0x000009F0
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000027F8 File Offset: 0x000009F8
		public List<string> EarlyLinks { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002801 File Offset: 0x00000A01
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002809 File Offset: 0x00000A09
		public List<string> ProductStyleCodes { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002812 File Offset: 0x00000A12
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000281A File Offset: 0x00000A1A
		public string NotificationEmail { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00002823 File Offset: 0x00000A23
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000282B File Offset: 0x00000A2B
		public string NotificationText { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002834 File Offset: 0x00000A34
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000283C File Offset: 0x00000A3C
		public CheckoutInfo CheckoutInfo { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002845 File Offset: 0x00000A45
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x0000284D File Offset: 0x00000A4D
		public bool SnkrsExploit { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002856 File Offset: 0x00000A56
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x0000285E File Offset: 0x00000A5E
		[JsonIgnore]
		public string ProxyAddress { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00011C54 File Offset: 0x0000FE54
		[JsonIgnore]
		public string Checkout
		{
			get
			{
				string result = "Invalid Details";
				try
				{
					if (this.CheckoutInfo.IsNull() || false.EqualsAll(new bool[]
					{
						this.CheckoutInfo.PayPalCheckout,
						this.CheckoutInfo.CcCheckout
					}))
					{
						result = "Checkout Disabled";
						return result;
					}
					result = (this.CheckoutInfo.PayPalCheckout ? "PayPal Checkout - {0}".With(new object[]
					{
						this.CheckoutInfo.PayPalEmailAddress
					}) : "CC Checkout - {0}".With(new object[]
					{
						this.CheckoutInfo.CcProfile
					}));
				}
				catch (Exception)
				{
				}
				return result;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002867 File Offset: 0x00000A67
		[JsonIgnore]
		public bool IsWebSnkrs
		{
			get
			{
				return !this.EarlyLinks.IsAny<string>() && this.ProductStyleCodes.IsAny<string>();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002886 File Offset: 0x00000A86
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000288E File Offset: 0x00000A8E
		[JsonIgnore]
		public string WebSnkrsToken { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002897 File Offset: 0x00000A97
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000289F File Offset: 0x00000A9F
		public bool Disabled { get; set; }

		// Token: 0x060000D9 RID: 217 RVA: 0x00011D18 File Offset: 0x0000FF18
		public void UpdateDetails(Account acc)
		{
			this.EmailAddress = acc.EmailAddress;
			this.Password = acc.Password;
			this.Size = acc.Size;
			this.Keywords = acc.Keywords;
			this.CollectionKeywords = acc.CollectionKeywords;
			this.EarlyLinks = acc.EarlyLinks;
			this.ProductStyleCodes = acc.ProductStyleCodes;
			this.Request = (this.Request ?? new HttpHelper("", true, null, 15, null));
			this.NotificationEmail = acc.NotificationEmail;
			this.NotificationText = acc.NotificationText;
			this.CheckoutInfo = acc.CheckoutInfo;
			this.SnkrsExploit = acc.SnkrsExploit;
			this.IsGuest = acc.IsGuest;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00011DD4 File Offset: 0x0000FFD4
		public string ExportAccount()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.EmailAddress);
			stringBuilder.Append("\t");
			stringBuilder.Append(this.Password);
			stringBuilder.Append("\t");
			stringBuilder.Append(this.Size.JoinToString(","));
			stringBuilder.Append("\t");
			stringBuilder.Append(this.Keywords.JoinToString(","));
			stringBuilder.Append("\t");
			stringBuilder.Append(this.CollectionKeywords.JoinToString("\r\n"));
			stringBuilder.Append("\t");
			stringBuilder.Append(this.EarlyLinks.JoinToString("\r\n"));
			stringBuilder.Append("\t");
			stringBuilder.Append(this.ProductStyleCodes.JoinToString("\r\n"));
			stringBuilder.Append("\t");
			stringBuilder.Append(this.NotificationEmail);
			stringBuilder.Append("\t");
			if (this.NotificationText.IsNullOrWhiteSpace())
			{
				stringBuilder.Append("");
				stringBuilder.Append("\t");
				stringBuilder.Append("");
			}
			else
			{
				string[] number = this.NotificationText.Split(new char[]
				{
					'@'
				});
				stringBuilder.Append(NotificationSettings.AvailableTextNotificationServices.FirstOrDefault((NotificationService n) => n.EmailSuffix.Contains(number[1])).Carrier);
				stringBuilder.Append("\t");
				stringBuilder.Append(number[0]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00011F7C File Offset: 0x0001017C
		public void LoginForBrowser()
		{
			if (this.IsGuest)
			{
				return;
			}
			try
			{
				Logger.Log("Logging in to browser... {0}".With(new object[]
				{
					this.EmailAddress
				}), true, true);
				this.Request.Cookies.SetCookies(new Uri("http://www.nike.com"), "nike_locale={0}/{1}".With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeLangLocale
				}));
				this.Request.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "nike_locale={0}/{1}".With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeLangLocale
				}));
				this.Request.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "NIKE_COMMERCE_COUNTRY={0}".With(new object[]
				{
					NikeUrls.NikeCountryCode
				}));
				this.Request.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "NIKE_COMMERCE_LANG_LOCALE={0}".With(new object[]
				{
					NikeUrls.NikeLangLocale
				}));
				this.Request.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "CONSUMERCHOICE={0}/{1}".With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeLangLocale
				}));
				Dictionary<string, string> postData = new Dictionary<string, string>
				{
					{
						"login",
						this.EmailAddress
					},
					{
						"rememberMe",
						"true"
					},
					{
						"password",
						this.Password
					}
				};
				string url = NikeUrls.NikeLogin;
				this.Request.PostRequest(url, postData, NikeUrls.NikeStore, "application/x-www-form-urlencoded", new string[]
				{
					"X-Requested-With: XMLHttpRequest"
				}, true);
				url = NikeUrls.NikeCartSummary;
				this.Request.GetRequest(url, null, null, true, true);
				this.IsLoggedIn = true;
			}
			catch (Exception)
			{
				Logger.Log("Error logging in to browser. {0}".With(new object[]
				{
					this.EmailAddress
				}), true, true);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000121B4 File Offset: 0x000103B4
		public void SnkrsLogin(int retryCount = 0, int maxRetries = 0)
		{
			for (;;)
			{
				if (maxRetries > 0)
				{
					goto IL_6E;
				}
				IL_04:
				Logger.Log("{0}: Logging in to Nike+ account".With(new object[]
				{
					this.EmailAddress
				}), true, true);
				try
				{
					WebSnkrs.PerformLogin(this);
					goto IL_73;
				}
				catch (Exception ex)
				{
					Logger.Log("{0}: Login Error! {1}".With(new object[]
					{
						this.EmailAddress,
						ex.Message
					}), true, true);
					Thread.Sleep(2000);
					retryCount++;
					continue;
				}
				IL_6E:
				if (retryCount >= maxRetries)
				{
					break;
				}
				goto IL_04;
			}
			return;
			IL_73:
			this.IsLoggedIn = true;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0001224C File Offset: 0x0001044C
		public void Login(int retryCount = 0, int maxRetries = 0)
		{
			for (;;)
			{
				if (maxRetries > 0)
				{
					goto IL_419;
				}
				IL_07:
				if ("demo".EqualsAll(new string[]
				{
					this.EmailAddress,
					this.Password
				}) || Form1.ShouldStop)
				{
					goto IL_421;
				}
				Logger.Log("{0}: Logging in to Nike.".With(new object[]
				{
					this.EmailAddress
				}), true, true);
				this.Request.Cookies.SetCookies(new Uri("http://www.nike.com"), "nike_locale={0}/{1}".With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeLangLocale
				}));
				this.Request.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "nike_locale={0}/{1}".With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeLangLocale
				}));
				this.Request.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "NIKE_COMMERCE_COUNTRY={0}".With(new object[]
				{
					NikeUrls.NikeCountryCode
				}));
				this.Request.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "NIKE_COMMERCE_LANG_LOCALE={0}".With(new object[]
				{
					NikeUrls.NikeLangLocale
				}));
				this.Request.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "CONSUMERCHOICE={0}/{1}".With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeLangLocale
				}));
				this.Request.Cookies.SetCookies(new Uri("http://www.nike.com"), "nike_locale={0}/{1}".With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeLangLocale
				}));
				this.Request.Cookies.SetCookies(new Uri("http://www.nike.com"), "NIKE_COMMERCE_COUNTRY={0}".With(new object[]
				{
					NikeUrls.NikeCountryCode
				}));
				this.Request.Cookies.SetCookies(new Uri("http://www.nike.com"), "NIKE_COMMERCE_LANG_LOCALE={0}".With(new object[]
				{
					NikeUrls.NikeLangLocale
				}));
				this.Request.Cookies.SetCookies(new Uri("http://www.nike.com"), "CONSUMERCHOICE={0}/{1}".With(new object[]
				{
					NikeUrls.NikeCountrySmallCode,
					NikeUrls.NikeLangLocale
				}));
				this.Request.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "guidU=" + Guid.NewGuid().ToString().ToLower());
				this.Request.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "guidS=" + Guid.NewGuid().ToString().ToLower());
				Dictionary<string, string> dictionary = new Dictionary<string, string>
				{
					{
						"login",
						this.EmailAddress
					},
					{
						"rememberMe",
						"true"
					},
					{
						"password",
						this.Password
					}
				};
				if (this.IsGuest)
				{
					dictionary["password"] = "a";
				}
				try
				{
					string nikeLogin = NikeUrls.NikeLogin;
					this.Request.PostRequest(nikeLogin, dictionary, NikeUrls.NikeStore, "application/x-www-form-urlencoded", new string[]
					{
						"X-Requested-With: XMLHttpRequest"
					}, true);
					Logger.Log("{0}: Login Successful!.".With(new object[]
					{
						this.EmailAddress
					}), true, true);
					goto IL_421;
				}
				catch (Exception ex)
				{
					if (this.IsGuest)
					{
						Logger.Log("{0}: Login Successful!.".With(new object[]
						{
							this.EmailAddress
						}), true, true);
						goto IL_421;
					}
					Logger.Log("{0}: Login Error! {1}".With(new object[]
					{
						this.EmailAddress,
						ex.Message
					}), true, true);
					Thread.Sleep(2000);
					retryCount++;
					continue;
				}
				IL_419:
				if (retryCount >= maxRetries)
				{
					break;
				}
				goto IL_07;
			}
			return;
			try
			{
				IL_421:
				string nikeCartSummary = NikeUrls.NikeCartSummary;
				if (!Form1.UseSnkrsForNonUS && !this.SnkrsExploit && !this.IsWebSnkrs)
				{
					this.Request.GetRequest(nikeCartSummary, null, null, true, true);
				}
			}
			catch (Exception)
			{
			}
			this.IsLoggedIn = true;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000126D4 File Offset: 0x000108D4
		public string CheckCart()
		{
			Logger.Log("{0}: Checking nike cart.".With(new object[]
			{
				this.EmailAddress
			}), true, true);
			string nikeCart = NikeUrls.NikeCart;
			string text = string.Empty;
			string result;
			try
			{
				text = this.Request.GetRequest(nikeCart, null, null, true, true);
				goto IL_7C;
			}
			catch (Exception ex)
			{
				Logger.Log("{0}: Error checking nike cart. {1}".With(new object[]
				{
					this.EmailAddress,
					ex.Message
				}), true, true);
				result = text;
			}
			return result;
			IL_7C:
			StringBuilder stringBuilder = new StringBuilder();
			CQ cq = text.CQSelect("form#lineitemform div[class$=cartItem]");
			if (!cq.IsAny<IDomObject>())
			{
				stringBuilder.AppendLine("Your nike cart is empty!");
			}
			else
			{
				foreach (IDomObject domObject in cq)
				{
					stringBuilder.AppendLine("Item Name: " + domObject.Cq().Find("[class$=cartItemTitle]").Text().ReplaceNewLine("").HtmlDecode().Trim());
					CQ cq2 = domObject.Cq().Find("[class$=cartItemOption]");
					foreach (IDomObject domObject2 in cq2)
					{
						stringBuilder.AppendLine(domObject2.Cq().Text().ReplaceNewLine("").HtmlDecode().Trim());
					}
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
				}
			}
			cq = text.CQSelect("form#giftlistform div[class$=cartItem]");
			if (cq.IsAny<IDomObject>())
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Wishlist/Locker Items:");
				stringBuilder.AppendLine();
				foreach (IDomObject domObject3 in cq)
				{
					stringBuilder.AppendLine("Item Name: " + domObject3.Cq().Find("[class$=cartItemTitle]").Text().ReplaceNewLine("").HtmlDecode().Trim());
					CQ cq3 = domObject3.Cq().Find("[class$=cartItemOption]");
					foreach (IDomObject domObject4 in cq3)
					{
						stringBuilder.AppendLine(domObject4.Cq().Text().ReplaceNewLine("").HtmlDecode().Trim());
					}
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000129CC File Offset: 0x00010BCC
		public void ClearCart()
		{
			string nikeCart = NikeUrls.NikeCart;
			string request = this.Request.GetRequest(nikeCart, null, null, true, true);
			string url = NikeUrls.NikeCartDargs;
			string text = request.CQSelect("input[name=_dynSessConf]").Val();
			string text2 = request.CQSelect("input[name=_dyncharset]").Val();
			CQ cq = request.CQSelect("form#lineitemform div[class$=cartItem]");
			if (cq.IsAny<IDomObject>())
			{
				foreach (IDomObject domObject in cq)
				{
					string text3 = domObject.Cq().Find("[class$=cartItemTitle]").Text().ReplaceNewLine("").HtmlDecode().Trim();
					string text4 = domObject.Cq().Find("input[name='remove']").Attr("params").Split(new string[]
					{
						"||"
					}, StringSplitOptions.None)[0];
					string postData = "_dyncharset={0}&_dynSessConf={1}&route=html&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemSuccessURL=cartPageURL&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemSuccessURL=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemErrorURL=cartPageURL&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemErrorURL=+&commerceid={2}&_D%3Acommerceid=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.quantity=0&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.quantity=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListId=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListQuantity=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListQuantity=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.productId=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.productId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.skuId=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.skuId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.metrics_id=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.metrics_id=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.metric_type=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.metric_type=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.prebuild_pid=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.prebuild_pid=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.channel=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.channel=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.action=removeItem&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.action=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.itemReturnUrl=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.itemReturnUrl=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItem=true&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItem=+&_DARGS=%2Fus%2Fcheckout%2Fcommon%2Fincludes%2FlineItem.jsp.lineitemform".With(new object[]
					{
						text2,
						text,
						text4
					});
					this.Request.PostRequest(url, postData, nikeCart, "application/x-www-form-urlencoded", null, true, "POST");
					Logger.Log("{0}: Item {1} has been removed from cart.".With(new object[]
					{
						this.EmailAddress,
						text3
					}), true, true);
				}
			}
			cq = request.CQSelect("form#giftlistform div[class$=cartItem]");
			if (cq.IsAny<IDomObject>())
			{
				foreach (IDomObject domObject2 in cq)
				{
					string text5 = domObject2.Cq().Find("[class$=cartItemTitle]").Text().ReplaceNewLine("").HtmlDecode().Trim();
					string[] array = domObject2.Cq().Find("input[name='remove']").Attr("params").Split(new string[]
					{
						"||"
					}, StringSplitOptions.None);
					string postData2 = "_dyncharset={0}&_dynSessConf={1}&{2}=1&route=html&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemSuccessURL=cartPageURL&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemSuccessURL=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemErrorURL=cartPageURL&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemErrorURL=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.skuId=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.skuId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.productId=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.productId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListId={3}&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.quantity=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.quantity=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.catalogId=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.catalogId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.siteId=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.siteId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.sizeType=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.sizeType=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.displaySize=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.displaySize=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListItemId={2}&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListItemId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.action=removeFromLocker&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.action=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItem=true&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItem=+&_DARGS=%2Fus%2Fcheckout%2Fcommon%2Fincludes%2FmyLocker.jsp.giftlistform".With(new object[]
					{
						text2,
						text,
						array[1],
						array[0]
					});
					url = NikeUrls.NikeCartLockerDargs;
					this.Request.PostRequest(url, postData2, nikeCart, "application/x-www-form-urlencoded", null, true, "POST");
					Logger.Log("{0}: Item {1} has been removed from locker.".With(new object[]
					{
						this.EmailAddress,
						text5
					}), true, true);
				}
			}
			Logger.Log("{0}: Cart cleared successfuly".With(new object[]
			{
				this.EmailAddress
			}), true, true);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00012CA4 File Offset: 0x00010EA4
		public string GetAllSkusNew(string el)
		{
			string text = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			string[] segments = new Uri(el).Segments;
			string text2 = "";
			string displayName = "";
			foreach (string text3 in segments)
			{
				if (text3.Contains("pid-"))
				{
					text2 = text3.Replace("pid-", "").Replace("/", "");
				}
				if (text3.Contains("shoe"))
				{
					displayName = text3.Replace("-", " ").Replace("/", "").ToLower();
				}
			}
			if (text2.IsNullOrWhiteSpace())
			{
				Logger.Log("{0} invalid pid".With(new object[]
				{
					el
				}), true, true);
				return "";
			}
			JObject jobject = null;
			JArray jarray = null;
			try
			{
				text = this.Request.GetRequest(NikeUrls.NikeLoadSkus.With(new object[]
				{
					text2
				}), null, null, true, true);
				jobject = JObject.Parse(text);
				jarray = jobject["product"].Value<JArray>("childSKUs");
			}
			catch (Exception)
			{
			}
			if (!jarray.IsAny<JToken>())
			{
				Logger.Log("No available sizes found!", true, true);
				return "";
			}
			foreach (JToken jtoken in jarray)
			{
				stringBuilder.AppendLine("Size: {0}\t\tSkuId: {1}".With(new object[]
				{
					jtoken["sizeDescription"].ToString().ConvertToNativeSize(),
					jtoken["id"].ToString()
				}));
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(NikeUrls.NikeLoadSkus.With(new object[]
			{
				text2
			}));
			stringBuilder.AppendLine(text);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			Dictionary<string, string> o = new Dictionary<string, string>
			{
				{
					NikeUrls.NikeLoadSkus.With(new object[]
					{
						text2
					}),
					text
				}
			};
			stringBuilder.AppendLine(JObject.FromObject(o).ToString());
			try
			{
				string nikeLoadProduct = NikeUrls.NikeLoadProduct;
				text = this.Request.GetRequest(nikeLoadProduct.With(new object[]
				{
					text2
				}), null, null, true, true);
				jobject = (JObject)((JObject)JsonConvert.DeserializeObject(text))["product"];
			}
			catch (Exception)
			{
				var o2 = new
				{
					displayName = displayName,
					id = text2,
					styleNumber = "",
					colorNumber = "",
					colorDescription = "",
					listPrice = ""
				};
				jobject = JObject.FromObject(o2);
			}
			return "Product Name: {0}\r\nLink: {7}\r\nProduct ID: {1}\r\nStyle #: {2}\r\nColor Desc: {3}\r\nPrice: {6}\r\nImage: {4}\r\n\r\nSize Details:\r\n{5}".With(new object[]
			{
				jobject["displayName"],
				jobject["id"],
				"{0}-{1}".With(new object[]
				{
					jobject["styleNumber"],
					jobject["colorNumber"]
				}),
				jobject["colorDescription"],
				"https://secure-images.nike.com/is/image/DotCom/{0}_{1}".With(new object[]
				{
					jobject["styleNumber"],
					jobject["colorNumber"]
				}),
				stringBuilder.ToString(),
				jobject["listPrice"],
				el
			});
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00013060 File Offset: 0x00011260
		public string GetAllSkus(string el)
		{
			string text = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			string[] segments = new Uri(el).Segments;
			string text2 = "";
			string displayName = "";
			foreach (string text3 in segments)
			{
				if (text3.Contains("pid-"))
				{
					text2 = text3.Replace("pid-", "").Replace("/", "");
				}
				if (text3.Contains("shoe"))
				{
					displayName = text3.Replace("-", " ").Replace("/", "").ToLower();
				}
			}
			if (text2.IsNullOrWhiteSpace())
			{
				Logger.Log("{0} invalid pid".With(new object[]
				{
					el
				}), true, true);
				return "";
			}
			JObject jobject = null;
			JArray jarray = null;
			try
			{
				text = this.Request.GetRequest(NikeUrls.NikeInventory.With(new object[]
				{
					text2
				}), null, null, true, true);
				jobject = JObject.Parse(text);
				jarray = jobject["product"].Value<JArray>("childSKUs");
			}
			catch (Exception)
			{
			}
			if (!jarray.IsAny<JToken>())
			{
				Logger.Log("No available sizes found!", true, true);
				return "";
			}
			string text4 = NikeUrls.NikeCart;
			foreach (object arg in jarray)
			{
				if (Account.<GetAllSkus>o__SiteContainere.<>p__Sitef == null)
				{
					Account.<GetAllSkus>o__SiteContainere.<>p__Sitef = CallSite<Action<CallSite, Account, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "AddToLocker", null, typeof(Account), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, Account, string, object> target = Account.<GetAllSkus>o__SiteContainere.<>p__Sitef.Target;
				CallSite <>p__Sitef = Account.<GetAllSkus>o__SiteContainere.<>p__Sitef;
				if (Account.<GetAllSkus>o__SiteContainere.<>p__Site10 == null)
				{
					Account.<GetAllSkus>o__SiteContainere.<>p__Site10 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(Account), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, object> target2 = Account.<GetAllSkus>o__SiteContainere.<>p__Site10.Target;
				CallSite <>p__Site = Account.<GetAllSkus>o__SiteContainere.<>p__Site10;
				if (Account.<GetAllSkus>o__SiteContainere.<>p__Site11 == null)
				{
					Account.<GetAllSkus>o__SiteContainere.<>p__Site11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "id", typeof(Account), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__Sitef, this, el, target2(<>p__Site, Account.<GetAllSkus>o__SiteContainere.<>p__Site11.Target(Account.<GetAllSkus>o__SiteContainere.<>p__Site11, arg)));
				try
				{
					text = this.Request.GetRequest(text4, null, null, true, true);
				}
				catch (Exception ex)
				{
					Logger.Log("{0}: Error checking nike cart. {1}".With(new object[]
					{
						this.EmailAddress,
						ex.Message
					}), true, true);
					return text;
				}
				CQ cq = text.CQSelect("form#giftlistform div[class$=cartItem] input[name=addToCart]");
				if (cq.IsAny<IDomObject>())
				{
					foreach (IDomObject domObject in cq)
					{
						string[] array2 = domObject.GetAttribute("params").Split(new string[]
						{
							"||"
						}, StringSplitOptions.None);
						if (array2[1] == text2)
						{
							stringBuilder.AppendLine("Size: {0}\tSkuId: {1}".With(new object[]
							{
								array2[7],
								array2[2]
							}));
						}
					}
				}
				this.ClearCart();
			}
			jobject = null;
			try
			{
				text4 = NikeUrls.NikeLoadProduct;
				text = this.Request.GetRequest(text4.With(new object[]
				{
					text2
				}), null, null, true, true);
				jobject = (JObject)((JObject)JsonConvert.DeserializeObject(text))["product"];
			}
			catch (Exception)
			{
				var o = new
				{
					displayName = displayName,
					id = text2,
					styleNumber = "",
					colorNumber = "",
					colorDescription = "",
					listPrice = ""
				};
				jobject = JObject.FromObject(o);
			}
			return "Product Name: {0}\r\nLink: {7}\r\nProduct ID: {1}\r\nStyle #: {2}\r\nColor Desc: {3}\r\nPrice: {6}\r\nImage: {4}\r\n\r\nSize Details:\r\n{5}".With(new object[]
			{
				jobject["displayName"],
				jobject["id"],
				"{0}-{1}".With(new object[]
				{
					jobject["styleNumber"],
					jobject["colorNumber"]
				}),
				jobject["colorDescription"],
				"https://secure-images.nike.com/is/image/DotCom/{0}_{1}".With(new object[]
				{
					jobject["styleNumber"],
					jobject["colorNumber"]
				}),
				stringBuilder.ToString(),
				jobject["listPrice"],
				el
			});
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000135B8 File Offset: 0x000117B8
		public void AddToLocker(string el, string sku = null)
		{
			string[] segments = new Uri(el).Segments;
			string text = "";
			foreach (string text2 in segments)
			{
				if (text2.Contains("pid-"))
				{
					text = text2.Replace("pid-", "").Replace("/", "");
				}
			}
			if (text.IsNullOrWhiteSpace())
			{
				Logger.Log("{0} invalid pid".With(new object[]
				{
					el
				}), true, true);
				return;
			}
			AddToCart addToCart = new AddToCart(new AtcItem(this, el, this.Size[0]), null);
			addToCart.AtcItem.Details.ProductId = text;
			addToCart.Simulate = true;
			if (sku == null)
			{
				addToCart.GetProductDetailsNew();
				sku = addToCart.AtcItem.Details.SkuId;
			}
			if (sku.IsNullOrWhiteSpace())
			{
				Logger.Log("{0}: Size {1} not found".With(new object[]
				{
					el,
					this.Size[0]
				}), true, true);
				return;
			}
			addToCart.AtcItem.Details.Ticks = DateTime.Now.Ticks.ToString();
			string nikeCart = NikeUrls.NikeCart;
			string request = this.Request.GetRequest(nikeCart, null, null, true, true);
			string text3 = request.CQSelect("input[name=_dynSessConf]").Val();
			string text4 = request.CQSelect("input[name=_dyncharset]").Val();
			string nikeCartDargs = NikeUrls.NikeCartDargs;
			string postData = "_dyncharset={0}&_dynSessConf={1}&route=html&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemSuccessURL=cartPageURL&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemSuccessURL=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemErrorURL=cartPageURL&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItemErrorURL=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.commerceid={2}&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.commerceid=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.quantity=0&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.quantity=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListId=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListQuantity=1&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.giftListQuantity=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.productId={3}&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.productId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.skuId={4}&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.skuId=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.metrics_id=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.metrics_id=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.prebuild_pid=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.prebuild_pid=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.channel=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.channel=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.action=saveToLocker&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.action=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.itemReturnUrl=&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.dataMap.itemReturnUrl=+&%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItem=true&_D%3A%2Fatg%2Fcommerce%2Forder%2Fpurchase%2FMiniCartModifierFormHandler.addRemoveUpdateItem=+&_DARGS=%2Fus%2Fcheckout%2Fcommon%2Fincludes%2FlineItem.jsp.lineitemform".With(new object[]
			{
				text4,
				text3,
				"",
				text,
				sku
			});
			this.Request.PostRequest(nikeCartDargs, postData, nikeCart, "application/x-www-form-urlencoded", null, true, "POST");
			Logger.Log("item moved to locker! sku: {0}".With(new object[]
			{
				sku
			}), true, true);
		}

		// Token: 0x040000D7 RID: 215
		[JsonIgnore]
		public bool IsLoggedIn;

		// Token: 0x040000D8 RID: 216
		[JsonIgnore]
		public HttpHelper Request;

		// Token: 0x040000D9 RID: 217
		[JsonIgnore]
		public Http Http;

		// Token: 0x02000015 RID: 21
		public class NikeAccount
		{
			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000E6 RID: 230 RVA: 0x000028B8 File Offset: 0x00000AB8
			// (set) Token: 0x060000E7 RID: 231 RVA: 0x000028C0 File Offset: 0x00000AC0
			public string EmailAddress { get; set; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000E8 RID: 232 RVA: 0x000028C9 File Offset: 0x00000AC9
			// (set) Token: 0x060000E9 RID: 233 RVA: 0x000028D1 File Offset: 0x00000AD1
			public string Password { get; set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000EA RID: 234 RVA: 0x000028DA File Offset: 0x00000ADA
			// (set) Token: 0x060000EB RID: 235 RVA: 0x000028E2 File Offset: 0x00000AE2
			public string FirstName { get; set; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000EC RID: 236 RVA: 0x000028EB File Offset: 0x00000AEB
			// (set) Token: 0x060000ED RID: 237 RVA: 0x000028F3 File Offset: 0x00000AF3
			public string LastName { get; set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060000EE RID: 238 RVA: 0x000028FC File Offset: 0x00000AFC
			// (set) Token: 0x060000EF RID: 239 RVA: 0x00002904 File Offset: 0x00000B04
			public string ScreenName { get; set; }

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000290D File Offset: 0x00000B0D
			// (set) Token: 0x060000F1 RID: 241 RVA: 0x00002915 File Offset: 0x00000B15
			public DateTime DateTime_0 { get; set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000291E File Offset: 0x00000B1E
			// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002926 File Offset: 0x00000B26
			public int Id { get; set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000292F File Offset: 0x00000B2F
			// (set) Token: 0x060000F5 RID: 245 RVA: 0x00002937 File Offset: 0x00000B37
			public string Phone { get; set; }
		}

		// Token: 0x02000018 RID: 24
		[CompilerGenerated]
		private static class <GetAllSkus>o__SiteContainere
		{
			// Token: 0x040000F5 RID: 245
			public static CallSite<Action<CallSite, Account, string, object>> <>p__Sitef;

			// Token: 0x040000F6 RID: 246
			public static CallSite<Func<CallSite, object, object>> <>p__Site10;

			// Token: 0x040000F7 RID: 247
			public static CallSite<Func<CallSite, object, object>> <>p__Site11;
		}
	}
}
