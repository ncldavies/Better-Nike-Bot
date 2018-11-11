using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Xml;
using CsQuery;
using IMLokesh.Extensions;
using IMLokesh.HttpUtility;
using Jint;
using Jint.Native;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Better_Nike_Bot
{
	// Token: 0x0200003B RID: 59
	public class MonitorLink
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0002AA38 File Offset: 0x00028C38
		public MonitorLink(string link, IEnumerable<Account> ndcAccounts)
		{
			if (!link.Contains("/pw/n/1j7?sl="))
			{
				if (Form1.MonitorLinks.Any((MonitorLink l) => !l.Link.IsNullOrEmpty() && MonitorLink.RemoveQueryString(l.Link) == MonitorLink.RemoveQueryString(link)))
				{
					Form1.MonitorLinks.Remove(this);
					Logger.Log("Link {0} has already been processed...".With(new object[]
					{
						link
					}), true, true);
					return;
				}
			}
			this.Link = (link.IsNikeUrl() ? link : HttpHelper.GetRedirectUrl(link, Form1.ProxyEnumeration.GetNextObject(), false));
			if (this.Link != link)
			{
				Logger.Log("Link {0} parses to {1}...".With(new object[]
				{
					link,
					this.Link
				}), true, true);
			}
			this.NdcAccounts = ndcAccounts.ToList<Account>();
			Logger.Log("Monitoring link {0}.".With(new object[]
			{
				link
			}), true, true);
			this.StartTime = new DateTime(2014, 10, 23);
			MonitorLink monitorLink = Form1.MonitorLinkCache.FirstOrDefault((MonitorLink l) => l.Link == link);
			if (monitorLink.IsNotNull())
			{
				this.StartTime = monitorLink.StartTime;
			}
			new Thread(new ThreadStart(this.DoMonitor))
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0002AC14 File Offset: 0x00028E14
		public MonitorLink()
		{
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00003490 File Offset: 0x00001690
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00003498 File Offset: 0x00001698
		public string Link { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000034A1 File Offset: 0x000016A1
		// (set) Token: 0x0600029E RID: 670 RVA: 0x000034A9 File Offset: 0x000016A9
		public List<Account> NdcAccounts { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000034B2 File Offset: 0x000016B2
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x000034BA File Offset: 0x000016BA
		public DateTime StartTime { get; set; }

		// Token: 0x060002A1 RID: 673 RVA: 0x0002AC6C File Offset: 0x00028E6C
		public void DoMonitor()
		{
			bool flag = false;
			while (!Form1.ShouldStop && !flag)
			{
				Logger.Log("Checking link {0}".With(new object[]
				{
					this.Link
				}), true, Form1.VerboseLogging);
				this.CheckDirectSku();
				if (!this.NdcAccounts.IsAny<Account>())
				{
					return;
				}
				if (this.NdcAccounts.All((Account a) => a.IsWebSnkrs))
				{
					string pid = this.Link.RegexMatch("nike.com/(snkrs|launch)/thread/(?<pid>[a-z0-9]*)").Groups["pid"].Value;
					foreach (Account account in this.NdcAccounts)
					{
						KeyValuePair<Account, string> entry = new KeyValuePair<Account, string>(account, this.Link);
						if (!MonitorLink.ProcessedLinks.Contains(entry))
						{
							MonitorLink.ProcessedLinks.Add(entry);
							string[] size = entry.Key.Size;
							for (int i = 0; i < size.Length; i++)
							{
								string s = size[i];
								new Thread(delegate()
								{
									new AddToCart(new AtcItem(entry.Key, entry.Value, s)
									{
										Details = 
										{
											ProductId = pid
										}
									}, this).Atc();
								}).Start();
								Logger.Log("{0} (Size {2}): Processing link {1}".With(new object[]
								{
									account.EmailAddress,
									this.Link,
									s
								}), true, true);
							}
						}
						else
						{
							Logger.Log("{0}: Link {1} has already been processed.".With(new object[]
							{
								account.EmailAddress,
								this.Link
							}), true, true);
						}
					}
					return;
				}
				if (!Form1.UseOldMethod && !this.Link.IsNikeCollection() && !Form1.ForceOldMethod)
				{
					flag = this.MonitorLoadSkus();
					this.DoWait(!flag);
				}
				else
				{
					flag = (this.Link.Contains("/t/") ? this.MonitorProductPageT() : this.MonitorProductPage());
					this.DoWait(!flag);
				}
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0002AF00 File Offset: 0x00029100
		private void CheckDirectSku()
		{
			Account[] array = (from acc in this.NdcAccounts
			where acc.Size.Length == 2 && acc.Size.First<string>().Length > 4
			select acc).ToArray<Account>();
			if (array.IsAny<Account>())
			{
				this.GetProductDetails();
				if (this.Pid.IsNullOrWhiteSpace())
				{
					return;
				}
				foreach (Account account in array)
				{
					KeyValuePair<Account, string> item2 = new KeyValuePair<Account, string>(account, this.Link);
					if (!MonitorLink.ProcessedLinks.Contains(item2))
					{
						MonitorLink.ProcessedLinks.Add(item2);
						AtcItem item = new AtcItem(item2.Key, item2.Value, account.Size[1])
						{
							Details = 
							{
								ProductId = this.Pid,
								Line1 = this.Line1,
								Line2 = this.Line2,
								StyleCode = this.StyleCode,
								ProductImage = this.Img,
								Price = this.Price,
								Size = account.Size[1],
								SkuId = account.Size[0],
								SkuAndSize = "{0}:{1}".With(new object[]
								{
									account.Size[0],
									account.Size[1]
								})
							}
						};
						new Thread(delegate()
						{
							new AddToCart(item, this)
							{
								ForceOldMethod = Form1.ForceOldMethod
							}.Atc();
						}).Start();
						Logger.Log("{0} (Size {2}): Processing link {1}".With(new object[]
						{
							account.EmailAddress,
							this.Link,
							account.Size[1]
						}), true, true);
					}
					Logger.Log("{0}: Link {1} has already been processed.".With(new object[]
					{
						account.EmailAddress,
						this.Link
					}), true, true);
					this.NdcAccounts.Remove(account);
				}
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0002B12C File Offset: 0x0002932C
		public void DoWait(bool wait)
		{
			if (wait)
			{
				if (DateTime.Now > this.StartTime + TimeSpan.FromSeconds(30.0) && DateTime.Now < this.StartTime + TimeSpan.FromMinutes(4.0))
				{
					Thread.Sleep(NikeWaitIntervals.MonitorTimer);
					return;
				}
				Thread.Sleep(NikeWaitIntervals.Monitor);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0002B19C File Offset: 0x0002939C
		public static string RemoveQueryString(string url)
		{
			Uri uri = new Uri(url);
			return "{0}://{1}{2}".With(new object[]
			{
				uri.Scheme,
				uri.Host,
				uri.AbsolutePath
			});
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0002B1E0 File Offset: 0x000293E0
		private bool MonitorAU()
		{
			string html;
			bool result;
			try
			{
				html = HttpHelper.GetUrl(this.Link, null, Form1.ProxyEnumeration.GetNextObject(), 10, null);
				goto IL_69;
			}
			catch (Exception ex)
			{
				Logger.Log("Error: Link {0} - {1}".With(new object[]
				{
					this.Link,
					ex.Message
				}), true, true);
				result = false;
			}
			return result;
			IL_69:
			if (html.Contains("Product.Config"))
			{
				Logger.Log("Product is up at {0}.".With(new object[]
				{
					this.Link
				}), true, true);
				foreach (Account account in this.NdcAccounts)
				{
					KeyValuePair<Account, string> entry = new KeyValuePair<Account, string>(account, this.Link);
					if (!MonitorLink.ProcessedLinks.Contains(entry))
					{
						MonitorLink.ProcessedLinks.Add(entry);
						string[] size = entry.Key.Size;
						for (int i = 0; i < size.Length; i++)
						{
							string s = size[i];
							new Thread(delegate()
							{
								new AddToCart(new AtcItem(entry.Key, entry.Value, s), this)
								{
									OldHtml = html,
									ForceOldMethod = Form1.ForceOldMethod
								}.AUAtc("");
							}).Start();
							Logger.Log("{0} (Size {2}): Processing link {1}".With(new object[]
							{
								account.EmailAddress,
								this.Link,
								s
							}), true, true);
						}
					}
					else
					{
						Logger.Log("{0}: Link {1} has already been processed.".With(new object[]
						{
							account.EmailAddress,
							this.Link
						}), true, true);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0002B41C File Offset: 0x0002961C
		private bool MonitorProductPage()
		{
			string html = null;
			bool hasCache = false;
			try
			{
				if (this.Pid.IsNullOrWhiteSpace())
				{
					this.Pid = this.Link.RegexMatch("\\/pid-(?<pid>[0-9]+)\\/?").Groups["pid"].Value;
				}
			}
			catch (Exception)
			{
			}
			string key = NikeUrls.NikeLoadSkus.With(new object[]
			{
				this.Pid
			});
			html = null;
			try
			{
				html = Form1.ResponseDictionary[key];
				hasCache = true;
				if (this.StyleCode.IsNullOrWhiteSpace())
				{
					this.GetProductDetails();
				}
			}
			catch (Exception)
			{
			}
			bool result;
			try
			{
				html = (html ?? this.NdcAccounts.GetRandom<Account>().Request.GetRequest(this.Link, null, null, true, true));
				goto IL_122;
			}
			catch (Exception ex)
			{
				Logger.Log("Error: Link {0} - {1}".With(new object[]
				{
					this.Link,
					ex.Message
				}), true, true);
				result = false;
			}
			return result;
			IL_122:
			Account account;
			if (html.CQSelect("select[name=skuAndSize]").FirstOrDefault<IDomObject>().IsNotNull() || hasCache)
			{
				Logger.Log("Product is up at {0}.".With(new object[]
				{
					this.Link
				}), true, true);
				using (List<Account>.Enumerator enumerator = this.NdcAccounts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						account = enumerator.Current;
						KeyValuePair<Account, string> entry = new KeyValuePair<Account, string>(account, this.Link);
						if (!MonitorLink.ProcessedLinks.Contains(entry))
						{
							MonitorLink.ProcessedLinks.Add(entry);
							string[] size = entry.Key.Size;
							for (int i = 0; i < size.Length; i++)
							{
								string s = size[i];
								new Thread(delegate()
								{
									new AddToCart(new AtcItem(entry.Key, entry.Value, s)
									{
										Details = 
										{
											ProductId = this.Pid,
											Line1 = this.Line1,
											Line2 = this.Line2,
											StyleCode = this.StyleCode,
											ProductImage = this.Img,
											Price = this.Price
										}
									}, this)
									{
										OldHtml = html,
										ForceOldMethod = !hasCache
									}.Atc();
								}).Start();
								Logger.Log("{0} (Size {2}): Processing link {1}".With(new object[]
								{
									account.EmailAddress,
									this.Link,
									s
								}), true, true);
							}
						}
						else
						{
							Logger.Log("{0}: Link {1} has already been processed.".With(new object[]
							{
								account.EmailAddress,
								this.Link
							}), true, true);
						}
					}
				}
				return true;
			}
			if (!html.CQSelect(".grid-item.fullSize[data-pdpurl*=shoe]").FirstOrDefault<IDomObject>().IsNotNull())
			{
				Logger.Log("Error: Link {0} - {1}".With(new object[]
				{
					this.Link,
					"product not live yet"
				}), true, true);
				return false;
			}
			string[] array = html.CQSelect(".grid-item.fullSize[data-pdpurl] p.product-display-name").GetInnerTexts(false).ToArray<string>();
			string[] array2 = (from el in html.CQSelect(".grid-item.fullSize[data-pdpurl]")
			select el.GetAttribute("data-pdpurl", "")).ToArray<string>();
			string[] array3 = html.CQSelect(".grid-item.fullSize[data-pdpurl] .product-subtitle").GetInnerTexts(false).ToArray<string>();
			List<string[]> list = new List<string[]>();
			for (int j = 0; j < array2.Length; j++)
			{
				if (Form1.AllSize || (array3[j].ContainsAny(Form1.MatchStrings) && array3[j].ContainsAny(Form1.ShoeStrings) && !array3[j].ContainsAny(Form1.NegativeStrings)))
				{
					list.Add(new string[]
					{
						array[j],
						array2[j],
						array3[j]
					});
				}
			}
			if (!list.Any<string[]>())
			{
				return false;
			}
			list = (from shoe in list
			where this.NdcAccounts.Any((Account account) => !account.CollectionKeywords.Any<string>() || shoe[0].ContainsAny(account.CollectionKeywords.ToArray()))
			select shoe).ToList<string[]>();
			Logger.Log("Found {0} matching shoes in the collection. {1}".With(new object[]
			{
				list.Count,
				this.Link
			}), true, true);
			using (List<string[]>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					string[] shoeUrl = enumerator2.Current;
					IEnumerable<Account> ndcAccounts = from account in this.NdcAccounts
					where !account.CollectionKeywords.Any<string>() || shoeUrl[0].ContainsAny(account.CollectionKeywords.ToArray())
					select account;
					Form1.MonitorLinks.Add(new MonitorLink(shoeUrl[1], ndcAccounts));
				}
			}
			return true;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0002B99C File Offset: 0x00029B9C
		private bool MonitorProductPageT()
		{
			bool result;
			try
			{
				string html = null;
				try
				{
					html = this.NdcAccounts.GetRandom<Account>().Request.GetRequest(this.Link, null, null, true, true);
				}
				catch (Exception ex)
				{
					Logger.Log("Error: Link {0} - {1}".With(new object[]
					{
						this.Link,
						ex.Message
					}), true, true);
					return false;
				}
				string text = html.GetLinesArray(true).FirstOrDefault((string l) => l.Contains("window.INITIAL_REDUX_STATE="));
				if (text == null)
				{
					throw new Exception("Product data not found...");
				}
				text = text.Replace("<script>window.INITIAL_REDUX_STATE=", "var x22xx123 = ");
				text = text.Replace(";</script>", ";");
				string text2 = "";
				JObject jobject;
				try
				{
					Engine engine = new Engine();
					engine.Execute("window = {};");
					engine.Execute(text);
					JsValue value = engine.GetValue("x22xx123");
					jobject = JObject.Parse(engine.Json.Stringify(value, new JsValue[]
					{
						value
					}).ToString());
				}
				catch (Exception)
				{
					throw new Exception("Error parsing product data...");
				}
				MonitorLink.NikeSku[] skus;
				foreach (Account account in this.NdcAccounts)
				{
					KeyValuePair<Account, string> item2 = new KeyValuePair<Account, string>(account, this.Link);
					if (!MonitorLink.ProcessedLinks.Contains(item2))
					{
						MonitorLink.ProcessedLinks.Add(item2);
						string productId;
						string line;
						string line2;
						string price;
						string productImage;
						try
						{
							text2 = account.ProductStyleCodes.First<string>();
							skus = jobject["Threads"]["products"][text2]["skus"].ToObject<MonitorLink.NikeSku[]>();
							productId = jobject["Threads"]["products"][text2]["productId"].Value<string>();
							line = jobject["Threads"]["products"][text2]["title"].Value<string>();
							line2 = jobject["Threads"]["products"][text2]["subTitle"].Value<string>();
							price = jobject["Threads"]["products"][text2]["currentPrice"].Value<string>();
							productImage = jobject["Threads"]["products"][text2]["firstImageUrl"].Value<string>();
						}
						catch (Exception)
						{
							Logger.Log("{0}: Error parsing product data... {1}".With(new object[]
							{
								account.EmailAddress,
								text2
							}), true, true);
							continue;
						}
						string[] size = item2.Key.Size;
						for (int i = 0; i < size.Length; i++)
						{
							string text3 = size[i];
							AtcItem item = new AtcItem(item2.Key, item2.Value, text3)
							{
								Details = 
								{
									ProductId = productId,
									Line1 = line,
									Line2 = line2,
									StyleCode = text2,
									ProductImage = productImage,
									Price = price
								}
							};
							new Thread(delegate()
							{
								new AddToCart(item, this)
								{
									OldHtml = html,
									ForceOldMethod = Form1.ForceOldMethod,
									Skus = skus
								}.Atc();
							}).Start();
							Logger.Log("{0} (Size {2}): Processing link {1}".With(new object[]
							{
								account.EmailAddress,
								this.Link,
								text3
							}), true, true);
						}
					}
					else
					{
						Logger.Log("{0}: Link {1} has already been processed.".With(new object[]
						{
							account.EmailAddress,
							this.Link
						}), true, true);
					}
				}
				result = true;
			}
			catch (Exception ex2)
			{
				Logger.Log("Error: Link {0} - {1}".With(new object[]
				{
					this.Link,
					ex2.Message
				}), true, true);
				result = false;
			}
			return result;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0002BE90 File Offset: 0x0002A090
		private bool MonitorLoadSkus()
		{
			string html = "";
			if (this.StyleCode.IsNullOrWhiteSpace())
			{
				this.GetProductDetails();
			}
			if (this.Pid.IsNullOrWhiteSpace())
			{
				return true;
			}
			bool result;
			try
			{
				string text = NikeUrls.NikeLoadSkus.With(new object[]
				{
					this.Pid
				});
				html = null;
				try
				{
					html = Form1.ResponseDictionary[text];
				}
				catch (Exception)
				{
				}
				html = (html ?? this.NdcAccounts.GetRandom<Account>().Request.GetRequest(text, null, null, true, true));
				if (html.Contains("nike_Cart_handleJCartResponse"))
				{
					throw new Exception("Sizes are not yet live...");
				}
				if (html.Contains("xml version="))
				{
					if (Form1.SiteType == SiteType.NikeCN)
					{
						Form1.ForceOldMethod = true;
						return false;
					}
					html = html.Trim(new char[]
					{
						'}',
						'{'
					});
					html = ((!html.Contains("<response>") || html.Contains("</response>")) ? html : (html + "</response>"));
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(html);
					html = JsonConvert.SerializeXmlNode(xmlDocument["response"]);
					html = JObject.Parse(html)["response"].ToString();
				}
				JObject jobject = JObject.Parse(html);
				if (jobject["status"].ToString() == "success")
				{
					JArray list = jobject["product"].Value<JArray>("childSKUs");
					if (list.IsAny<JToken>())
					{
						using (List<Account>.Enumerator enumerator = this.NdcAccounts.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Account account = enumerator.Current;
								KeyValuePair<Account, string> item2 = new KeyValuePair<Account, string>(account, this.Link);
								if (!MonitorLink.ProcessedLinks.Contains(item2))
								{
									MonitorLink.ProcessedLinks.Add(item2);
									string[] size = item2.Key.Size;
									for (int i = 0; i < size.Length; i++)
									{
										string text2 = size[i];
										AtcItem item = new AtcItem(item2.Key, item2.Value, text2)
										{
											Details = 
											{
												ProductId = this.Pid,
												Line1 = this.Line1,
												Line2 = this.Line2,
												StyleCode = this.StyleCode,
												ProductImage = this.Img,
												Price = this.Price
											}
										};
										new Thread(delegate()
										{
											new AddToCart(item, this)
											{
												OldHtml = html,
												ForceOldMethod = Form1.ForceOldMethod
											}.Atc();
										}).Start();
										Logger.Log("{0} (Size {2}): Processing link {1}".With(new object[]
										{
											account.EmailAddress,
											this.Link,
											text2
										}), true, true);
									}
								}
								else
								{
									Logger.Log("{0}: Link {1} has already been processed.".With(new object[]
									{
										account.EmailAddress,
										this.Link
									}), true, true);
								}
							}
							goto IL_429;
						}
						goto IL_3AA;
						IL_429:
						return true;
					}
					IL_3AA:
					throw new Exception("Size details not available");
				}
				JToken jtoken = jobject["exceptions"];
				if (jtoken.IsAny<JToken>())
				{
					jtoken = jtoken["error"][0];
					throw new Exception("Error getting size.. Code: {0} Message: {1}".With(new object[]
					{
						jtoken["errorcode"],
						jtoken["message"]
					}));
				}
				throw new Exception("Size details not available");
			}
			catch (Exception ex)
			{
				Logger.Log("Error: Link {0} - {1}".With(new object[]
				{
					this.Link,
					ex.Message
				}), true, true);
				result = false;
			}
			return result;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0002C350 File Offset: 0x0002A550
		private void GetProductDetails()
		{
			try
			{
				if (this.Pid.IsNullOrWhiteSpace())
				{
					try
					{
						this.Pid = this.Link.RegexMatch("\\/pid-(?<pid>[0-9]+)\\/?").Groups["pid"].Value;
						this.Line1 = this.Link.RegexMatch("\\/pd\\/(?<line1>.*)\\/pid").Groups["line1"].Value.Replace("-", " ").Replace("/", "").ToLower();
						this.Line1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Line1).Replace(" Shoe", "");
					}
					catch (Exception)
					{
					}
					if (this.Pid.IsNullOrWhiteSpace())
					{
						Logger.Log("Link {0} has invalid pid.".With(new object[]
						{
							this.Link
						}), true, true);
						return;
					}
				}
				if (this.StyleCode.IsNullOrWhiteSpace())
				{
					string request = this.NdcAccounts.GetRandom<Account>().Request.GetRequest(NikeUrls.NikeLoadProduct.With(new object[]
					{
						this.Pid
					}), null, null, true, true);
					JObject jobject = JObject.Parse(request);
					if (jobject["status"].ToString() == "success")
					{
						this.Line1 = jobject["product"]["productName1"].ToString();
						this.Line2 = jobject["product"]["productName2"].ToString();
						this.StyleCode = "{0}-{1}".With(new object[]
						{
							jobject["product"]["styleNumber"].ToString(),
							jobject["product"]["colorNumber"].ToString()
						});
						this.Img = "https://secure-images.nike.com/is/image/DotCom/{0}".With(new object[]
						{
							this.StyleCode.Replace("-", "_")
						});
						this.Price = jobject["product"]["listPrice"].ToString();
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0400029D RID: 669
		public static List<KeyValuePair<Account, string>> ProcessedLinks = new List<KeyValuePair<Account, string>>();

		// Token: 0x0400029E RID: 670
		public static int IntervalStart;

		// Token: 0x0400029F RID: 671
		public static int IntervalStop;

		// Token: 0x040002A0 RID: 672
		public string Pid = "";

		// Token: 0x040002A1 RID: 673
		public string Line1 = "";

		// Token: 0x040002A2 RID: 674
		public string Line2 = "";

		// Token: 0x040002A3 RID: 675
		public string StyleCode = "";

		// Token: 0x040002A4 RID: 676
		public string Img = "";

		// Token: 0x040002A5 RID: 677
		public string Price = "";

		// Token: 0x0200003C RID: 60
		public class NikeSku
		{
			// Token: 0x040002AD RID: 685
			public string id;

			// Token: 0x040002AE RID: 686
			public string nikeSize;
		}
	}
}
