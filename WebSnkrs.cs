using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using Better_Nike_Bot.Utilities;
using CsQuery.ExtensionMethods;
using IMLokesh.Extensions;
using IMLokesh.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Better_Nike_Bot
{
	// Token: 0x02000077 RID: 119
	public class WebSnkrs
	{
		// Token: 0x0600036A RID: 874 RVA: 0x00003A4B File Offset: 0x00001C4B
		public WebSnkrs(AtcItem atcItem)
		{
			this.AtcItem = atcItem;
			this.Http = atcItem.Account.Http;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00003A76 File Offset: 0x00001C76
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00003A7E File Offset: 0x00001C7E
		public Http Http { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00003A87 File Offset: 0x00001C87
		// (set) Token: 0x0600036E RID: 878 RVA: 0x00003A8F File Offset: 0x00001C8F
		public AtcItem AtcItem { get; set; }

		// Token: 0x0600036F RID: 879 RVA: 0x0002FA6C File Offset: 0x0002DC6C
		public void Atc()
		{
			bool flag = false;
			while (!this.AtcItem.ShouldStop.Value && !flag && this.AtcItem.Details.SkuAndSize.IsNullOrWhiteSpace())
			{
				if (!(flag = this.GetProductDetails()))
				{
					Thread.Sleep(2000);
				}
			}
			flag = false;
			while (!this.AtcItem.ShouldStop.Value && !flag)
			{
				flag = this.Process();
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0002FAE0 File Offset: 0x0002DCE0
		private bool Process()
		{
			bool result;
			try
			{
				this._cc = Form1.CreditCardProfiles.FirstOrDefault((CreditCardProfile c) => c.Name == this.AtcItem.Account.CheckoutInfo.CcProfile);
				if (this._cc.IsNull())
				{
					Thread.Sleep(1000);
					throw new Exception("Checkout profile not found. You must have checkout profile for websnkrs. {0}".With(new object[]
					{
						this.AtcItem.Account.CheckoutInfo.CcProfile
					}));
				}
				int millisecondsTimeout = 3000;
				bool flag = true;
				while (flag)
				{
					if (this.CheckCheckoutLimitReached(this._cc))
					{
						return true;
					}
					try
					{
						Logger.Log("{0} ({1}): Generating tokens... ".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1
						}), true, true);
						this.AtcItem.Account.WebSnkrsToken = WebSnkrs.PerformLogin(this.AtcItem.Account);
						this.Http.AdditionalHeaders = new string[]
						{
							"Authorization: Bearer {0}".With(new object[]
							{
								this.AtcItem.Account.WebSnkrsToken
							}),
							"Origin: https://www.nike.com"
						};
						flag = false;
						goto IL_19C;
					}
					catch (Exception ex)
					{
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							ex.Message
						}), true, true);
						goto IL_19C;
					}
					IL_191:
					Thread.Sleep(millisecondsTimeout);
					continue;
					IL_19C:
					if (flag)
					{
						goto IL_191;
					}
				}
				flag = true;
				while (flag)
				{
					if (this.CheckCheckoutLimitReached(this._cc))
					{
						return true;
					}
					try
					{
						Logger.Log("{0} ({1}): Adding card to account...".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1
						}), true, true);
						this.SaveCC();
						flag = false;
						goto IL_265;
					}
					catch (Exception ex2)
					{
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							ex2.Message
						}), true, true);
						goto IL_265;
					}
					IL_25A:
					Thread.Sleep(millisecondsTimeout);
					continue;
					IL_265:
					if (flag)
					{
						goto IL_25A;
					}
				}
				flag = true;
				while (flag)
				{
					if (this.CheckCheckoutLimitReached(this._cc))
					{
						return true;
					}
					try
					{
						this.GetSkuId();
						flag = false;
						goto IL_2E7;
					}
					catch (Exception ex3)
					{
						Logger.Log("{0} ({1}): {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							ex3.Message
						}), true, true);
						goto IL_2E7;
					}
					IL_2DF:
					Thread.Sleep(millisecondsTimeout);
					continue;
					IL_2E7:
					if (flag)
					{
						goto IL_2DF;
					}
				}
				DateTime d = this._launchDate;
				d = d.Subtract(TimeSpan.FromSeconds(10.0));
				DateTime d2 = DateTime.Parse(this._res.Headers["Date"], CultureInfo.InvariantCulture);
				TimeSpan span = d - d2;
				if (span.TotalSeconds > 0.0)
				{
					double num = 0.0;
					double totalSeconds = span.TotalSeconds;
					Logger.Log("{0} (Size: {2}): Waiting for product {1} to go live. Resuming in {3}".With(new object[]
					{
						this.AtcItem.Account.EmailAddress,
						this.AtcItem.Details.Line1,
						this.AtcItem.Size,
						Better_Nike_Bot.AddToCart.ToReadableString(span)
					}), true, true);
					while (num < totalSeconds)
					{
						Thread.Sleep(5000);
						num += 5.0;
						if (this.AtcItem.ShouldStop.Value)
						{
							return true;
						}
					}
				}
				flag = true;
				while (flag)
				{
					if (this.CheckCheckoutLimitReached(this._cc))
					{
						return true;
					}
					try
					{
						Logger.Log("{0} ({1}): Creating checkout session...".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1
						}), true, true);
						this.AddToCart();
						flag = false;
						goto IL_4BC;
					}
					catch (Exception ex4)
					{
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							ex4.Message
						}), true, true);
						goto IL_4BC;
					}
					IL_4B1:
					Thread.Sleep(millisecondsTimeout);
					continue;
					IL_4BC:
					if (flag)
					{
						goto IL_4B1;
					}
				}
				if (this.CheckCheckoutLimitReached(this._cc))
				{
					result = true;
				}
				else
				{
					this.AddToCartStatus();
					flag = true;
					while (flag)
					{
						if (this.CheckCheckoutLimitReached(this._cc))
						{
							return true;
						}
						try
						{
							Logger.Log("{0} ({1}): Submitting payment info...".With(new object[]
							{
								this.AtcItem.Account.EmailAddress,
								this.AtcItem.Details.Line1
							}), true, true);
							this.SubmitPayment();
							flag = false;
							goto IL_5A3;
						}
						catch (Exception ex5)
						{
							Logger.Log("{0} ({1}): Error... {2}".With(new object[]
							{
								this.AtcItem.Account.EmailAddress,
								this.AtcItem.Details.Line1,
								ex5.Message
							}), true, true);
							goto IL_5A3;
						}
						IL_598:
						Thread.Sleep(millisecondsTimeout);
						continue;
						IL_5A3:
						if (flag)
						{
							goto IL_598;
						}
					}
					if (this.CheckCheckoutLimitReached(this._cc))
					{
						result = true;
					}
					else
					{
						this.SubmitPaymentStatus();
						if (this._isLeo)
						{
							flag = true;
							while (flag)
							{
								if (this.CheckCheckoutLimitReached(this._cc))
								{
									return true;
								}
								try
								{
									Logger.Log("{0} ({1}): Entering draw...".With(new object[]
									{
										this.AtcItem.Account.EmailAddress,
										this.AtcItem.Details.Line1
									}), true, true);
									this.LaunchEntry();
									Logger.Log("{0} ({1}): Waiting for draw result...".With(new object[]
									{
										this.AtcItem.Account.EmailAddress,
										this.AtcItem.Details.Line1
									}), true, true);
									flag = false;
									goto IL_6ED;
								}
								catch (Exception ex6)
								{
									if (ex6.Message.Contains("ENTRY_LIMIT_EXCEEDED"))
									{
										throw;
									}
									Logger.Log("{0} ({1}): Error... {2}".With(new object[]
									{
										this.AtcItem.Account.EmailAddress,
										this.AtcItem.Details.Line1,
										ex6.Message
									}), true, true);
									goto IL_6ED;
								}
								IL_6E2:
								Thread.Sleep(millisecondsTimeout);
								continue;
								IL_6ED:
								if (flag)
								{
									goto IL_6E2;
								}
							}
							if (this.CheckCheckoutLimitReached(this._cc))
							{
								result = true;
							}
							else
							{
								JObject jobject = this.LaunchEntryStatus();
								if (!(jobject["result"]["status"].Value<string>() == "WINNER"))
								{
									if (jobject["result"]["status"].Value<string>() == "NON_WINNER")
									{
										Logger.Log("{0}: {1}".With(new object[]
										{
											this.AtcItem.Account.EmailAddress,
											jobject["result"].ToString()
										}), true, true);
										try
										{
											if (jobject["result"]["reentryPermitted"].Value<bool>())
											{
												throw new Exception(jobject["result"].ToString());
											}
											return true;
										}
										catch (Exception)
										{
											return true;
										}
									}
									throw new Exception(jobject["result"].ToString());
								}
								this._cc.CheckoutCount++;
								Logger.Log("{0}: Web Snkrs Checkout success!".With(new object[]
								{
									this.AtcItem.Account.EmailAddress
								}), true, true);
								try
								{
									Form1.AddedProducts.Add(new AddedProduct(DateTime.Now, "(Checkout Success) {1} {1}".With(new object[]
									{
										this.AtcItem.Details.Line1,
										this.AtcItem.Details.Line2
									}), this.AtcItem.Details.StyleCode, "Cart Total: {0}".With(new object[]
									{
										this._total
									}), this.AtcItem.Account.EmailAddress, this.AtcItem.Details.Size, this.AtcItem.Account.Password, this.AtcItem.SnkrsBuyUrl, this.AtcItem.Account));
									Form1.AddedProductsForm.UpdateForm();
									Better_Nike_Bot.AddToCart.Callback("{0} (Leo Snkrs Checkout) {1} {2}".With(new object[]
									{
										Form1.SiteType,
										this.AtcItem.Details.Line1,
										this.AtcItem.Details.Line2
									}), "Cart Total: {0}".With(new object[]
									{
										this._total
									}), this.AtcItem.Account.EmailAddress, this.AtcItem.Details.StyleCode);
								}
								catch (Exception)
								{
								}
								result = true;
							}
						}
						else
						{
							flag = true;
							while (flag)
							{
								if (this.CheckCheckoutLimitReached(this._cc))
								{
									return true;
								}
								try
								{
									Logger.Log("{0} ({1}): Finalizing order...".With(new object[]
									{
										this.AtcItem.Account.EmailAddress,
										this.AtcItem.Details.Line1
									}), true, true);
									this.FinalizeOrder();
									flag = false;
									goto IL_A86;
								}
								catch (Exception ex7)
								{
									Logger.Log("{0} ({1}): Error... {2}".With(new object[]
									{
										this.AtcItem.Account.EmailAddress,
										this.AtcItem.Details.Line1,
										ex7.Message
									}), true, true);
									goto IL_A86;
								}
								IL_A7B:
								Thread.Sleep(millisecondsTimeout);
								continue;
								IL_A86:
								if (flag)
								{
									goto IL_A7B;
								}
							}
							if (this.CheckCheckoutLimitReached(this._cc))
							{
								result = true;
							}
							else
							{
								this.FinalizeOrderStatus();
								this._cc.CheckoutCount++;
								Logger.Log("{0}: Web SNKRS Checkout success!".With(new object[]
								{
									this.AtcItem.Account.EmailAddress
								}), true, true);
								try
								{
									Form1.AddedProducts.Add(new AddedProduct(DateTime.Now, "(Checkout Success) {0} {1}".With(new object[]
									{
										this.AtcItem.Details.Line1,
										this.AtcItem.Details.Line2
									}), this.AtcItem.Details.StyleCode, "Cart Total: {0}".With(new object[]
									{
										this._total
									}), this.AtcItem.Account.EmailAddress, this.AtcItem.Details.Size, this.AtcItem.Account.Password, this.AtcItem.SnkrsBuyUrl, this.AtcItem.Account));
									Form1.AddedProductsForm.UpdateForm();
									Better_Nike_Bot.AddToCart.Callback("{0} (Snkrs Checkout) {1} {2}".With(new object[]
									{
										Form1.SiteType,
										this.AtcItem.Details.Line1,
										this.AtcItem.Details.Line2
									}), "Cart Total: {0}".With(new object[]
									{
										this._total
									}), this.AtcItem.Account.EmailAddress, this.AtcItem.Details.StyleCode);
								}
								catch (Exception)
								{
								}
								result = true;
							}
						}
					}
				}
			}
			catch (Exception ex8)
			{
				Logger.Log("{0} ({1}): Error in web snkrs... {2}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Details.Line1,
					ex8.Message
				}), true, true);
				if (ex8.Message.Contains("ENTRY_LIMIT_EXCEEDED"))
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000308EC File Offset: 0x0002EAEC
		private void SaveCC()
		{
			this._cardId = Guid.NewGuid().ToString();
			string url = "https://paymentcc.nike.com/creditcardsubmit/{0}/store".With(new object[]
			{
				this._cardId
			});
			Dictionary<string, string> objectToSerialize = new Dictionary<string, string>
			{
				{
					"expirationMonth",
					this._cc.CreditCardExpiryMonth.PadLeft(2, '0')
				},
				{
					"accountNumber",
					this._cc.CreditCardNumber
				},
				{
					"creditCardInfoId",
					this._cardId
				},
				{
					"cvNumber",
					this._cc.CreditCardCvv
				},
				{
					"cardType",
					this._cc.CreditCardType.ToUpper()
				},
				{
					"expirationYear",
					this._cc.CreditCardExpiryYear
				}
			};
			HttpResponse res = this.Http.Post(url, "https://paymentcc.nike.com/services?id={0}".With(new object[]
			{
				this._cardId
			}), objectToSerialize.ToJSON(), "application/json", new string[]
			{
				"Origin: https://www.nike.com"
			});
			this.CheckNikeSnkrsError(res);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00030A18 File Offset: 0x0002EC18
		private void CheckNikeSnkrsError(HttpResponse res)
		{
			if (res.Exception != null)
			{
				if (res.Exception.ToString().ContainsAny(new string[]
				{
					"403",
					"429"
				}))
				{
					Thread.Sleep(4000);
				}
				throw new Exception(res.Text.IsNullOrWhiteSpace() ? res.Exception.Message : res.Text);
			}
			if (res.Text.IsNullOrWhiteSpace())
			{
				return;
			}
			string text = "";
			if (!res.IsJson)
			{
				throw new Exception("received invalid json...");
			}
			try
			{
				text = res.JObject["error"]["message"].Value<string>() + "... ";
			}
			catch (Exception)
			{
			}
			try
			{
				text += res.JObject["error"]["errors"][0]["message"].Value<string>();
			}
			catch (Exception)
			{
			}
			if (!text.IsNullOrWhiteSpace())
			{
				throw new Exception(text);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00030B48 File Offset: 0x0002ED48
		private void GetSkuId()
		{
			this._pid = this._snkrsProduct["id"].Value<string>();
			this._skuId = "";
			try
			{
				HttpResponse httpResponse = this.Http.Get("https://api.nike.com/deliver/available_skus/v1/?filter=productIds({0})".With(new object[]
				{
					this._pid
				}));
				this._res = httpResponse;
				this.CheckNikeSnkrsError(httpResponse);
				foreach (JToken jtoken in ((JArray)this._snkrsProduct["skus"]))
				{
					if (jtoken["nikeSize"].Value<string>() == this.AtcItem.Size)
					{
						this._skuId = jtoken["id"].Value<string>();
					}
				}
				if (this.AtcItem.Size == "RANDOM")
				{
					try
					{
						this._skuId = (from e in httpResponse.JObject["objects"]
						where e["available"].Value<bool>()
						select e).Randomize<JToken>().First<JToken>()["skuId"].Value<string>();
						if (this._skuId.IsNullOrWhiteSpace())
						{
							throw new Exception("No size is available to buy...");
						}
					}
					catch (Exception)
					{
						throw new Exception("No size is available to buy...");
					}
				}
				if (!this._skuId.IsNullOrWhiteSpace())
				{
					if (!httpResponse.JObject["objects"].First((JToken e) => e["skuId"].Value<string>() == this._skuId)["available"].Value<bool>())
					{
						throw new Exception("Size is not availabe to purchase...");
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error getting sku details... " + ex.Message);
			}
			if (this._skuId.IsNullOrWhiteSpace())
			{
				throw new Exception("No matching size found. Please use US size for snkrs.");
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00030D88 File Offset: 0x0002EF88
		private void AddToCart()
		{
			this._checkoutGuid = Guid.NewGuid().ToString();
			this.Http.Accept = "application/json";
			JObject jobject = JObject.Parse("{\"request\":{\"email\":\"clayton.jolin20@gmail.com\",\"country\":\"US\",\"currency\":\"USD\",\"locale\":\"en_US\",\"channel\":\"SNKRS\",\"items\":[{\"id\":\"9e5a771a-cebc-531f-8a96-c5b8c48d3875\",\"skuId\":\"a834899b-3a89-549f-9d3d-abe3938ae5e0\",\"quantity\":1,\"recipient\":{\"firstName\":\"Clayton\",\"lastName\":\"Jolin\"},\"shippingAddress\":{\"address1\":\"ADDR1\",\"address2\":\"Addr2\",\"city\":\"My City\",\"state\":\"NJ\",\"postalCode\":\"08854\",\"country\":\"US\"},\"contactInfo\":{\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"3478368677\"},\"shippingMethod\":\"STANDARD\"}]}}");
			if (Form1.SiteType == SiteType.NikeUS)
			{
				jobject = JObject.Parse("{\"request\":{\"email\":\"clayton.jolin20@gmail.com\",\"country\":\"US\",\"currency\":\"USD\",\"locale\":\"en_US\",\"channel\":\"SNKRS\",\"items\":[{\"id\":\"9e5a771a-cebc-531f-8a96-c5b8c48d3875\",\"skuId\":\"a834899b-3a89-549f-9d3d-abe3938ae5e0\",\"quantity\":1,\"recipient\":{\"firstName\":\"Clayton\",\"lastName\":\"Jolin\"},\"shippingAddress\":{\"address1\":\"ADDR1\",\"address2\":\"Addr2\",\"city\":\"My City\",\"state\":\"NJ\",\"postalCode\":\"08854\",\"country\":\"US\"},\"contactInfo\":{\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"3478368677\"},\"shippingMethod\":\"STANDARD\"}]}}");
				jobject["request"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["currency"] = NikeUrls.SnkrsCurrency;
				jobject["request"]["locale"] = NikeUrls.NikeLangLocale;
				jobject["request"]["items"][0]["id"] = this._pid;
				jobject["request"]["items"][0]["skuId"] = this._skuId;
				jobject["request"]["items"][0]["recipient"]["firstName"] = this._cc.ShippingFirstName;
				jobject["request"]["items"][0]["recipient"]["lastName"] = this._cc.ShippingLastName;
				jobject["request"]["items"][0]["shippingAddress"]["address1"] = this._cc.ShippingAddress1;
				jobject["request"]["items"][0]["shippingAddress"]["address2"] = this._cc.ShippingAddress2;
				jobject["request"]["items"][0]["shippingAddress"]["city"] = this._cc.ShippingCity;
				jobject["request"]["items"][0]["shippingAddress"]["state"] = this._cc.ShippingState;
				jobject["request"]["items"][0]["shippingAddress"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["request"]["items"][0]["shippingAddress"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["items"][0]["contactInfo"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["items"][0]["contactInfo"]["phoneNumber"] = this._cc.ShippingPhone;
			}
			if (Form1.SiteType.EqualsAny(new SiteType[]
			{
				SiteType.NikeDE,
				SiteType.NikeFR,
				SiteType.NikeIT,
				SiteType.NikePL,
				SiteType.NikeUK,
				SiteType.NikeES,
				SiteType.NikeNL,
				SiteType.NikeSE,
				SiteType.NikeDK
			}))
			{
				jobject = JObject.Parse("{\"request\":{\"email\":\"clayton.jolin20@gmail.com\",\"country\":\"GB\",\"currency\":\"GBP\",\"locale\":\"en_GB\",\"channel\":\"SNKRS\",\"items\":[{\"id\":\"bf23597d-3ac7-5a61-96d1-86f461765ce6\",\"skuId\":\"497b5fa8-4eb4-584a-8d94-9668fb74b60d\",\"quantity\":1,\"recipient\":{\"firstName\":\"Lokesh\",\"lastName\":\"Pharma\"},\"shippingAddress\":{\"address1\":\"90 Exning Road\",\"city\":\"HARLOW HILL\",\"postalCode\":\"NE15 7DN\",\"country\":\"GB\"},\"contactInfo\":{\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"7441909052\"},\"shippingMethod\":\"GROUND_SERVICE\"}]}}");
				jobject["request"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["currency"] = NikeUrls.SnkrsCurrency;
				jobject["request"]["locale"] = NikeUrls.NikeLangLocale;
				jobject["request"]["items"][0]["id"] = this._pid;
				jobject["request"]["items"][0]["skuId"] = this._skuId;
				jobject["request"]["items"][0]["recipient"]["firstName"] = this._cc.ShippingFirstName;
				jobject["request"]["items"][0]["recipient"]["lastName"] = this._cc.ShippingLastName;
				jobject["request"]["items"][0]["shippingAddress"]["address1"] = this._cc.ShippingAddress1;
				jobject["request"]["items"][0]["shippingAddress"]["address2"] = this._cc.ShippingAddress2;
				jobject["request"]["items"][0]["shippingAddress"]["city"] = this._cc.ShippingCity;
				jobject["request"]["items"][0]["shippingAddress"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["request"]["items"][0]["shippingAddress"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["items"][0]["contactInfo"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["items"][0]["contactInfo"]["phoneNumber"] = this._cc.ShippingPhone;
			}
			if (Form1.SiteType == SiteType.NikeJP)
			{
				jobject = JObject.Parse("{\"request\":{\"email\":\"clayton.jolin20@gmail.com\",\"country\":\"JP\",\"currency\":\"JPY\",\"locale\":\"ja_JP\",\"channel\":\"SNKRS\",\"items\":[{\"id\":\"8db67869-f451-5293-85f3-4319e1cedae1\",\"skuId\":\"579012de-3154-5049-a798-ae814ede8922\",\"quantity\":1,\"recipient\":{\"firstName\":\"nmm\",\"altFirstName\":\"nmm\",\"lastName\":\"name\",\"altLastName\":\"name\"},\"shippingAddress\":{\"address1\":\"A1\",\"address2\":\"A2\",\"address3\":\"Town Area\",\"city\":\"City\",\"state\":\"JP-04\",\"postalCode\":\"145-8589\",\"country\":\"JP\"},\"contactInfo\":{\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"85987458968\"},\"shippingMethod\":\"GROUND_SERVICE\"}]}}");
				jobject["request"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["currency"] = NikeUrls.SnkrsCurrency;
				jobject["request"]["locale"] = NikeUrls.NikeLangLocale;
				jobject["request"]["items"][0]["id"] = this._pid;
				jobject["request"]["items"][0]["skuId"] = this._skuId;
				jobject["request"]["items"][0]["recipient"]["firstName"] = this._cc.ShippingFirstName;
				jobject["request"]["items"][0]["recipient"]["altFirstName"] = this._cc.ShippingFirstName;
				jobject["request"]["items"][0]["recipient"]["lastName"] = this._cc.ShippingLastName;
				jobject["request"]["items"][0]["recipient"]["altLastName"] = this._cc.ShippingLastName;
				jobject["request"]["items"][0]["shippingAddress"]["address1"] = this._cc.ShippingAddress1;
				jobject["request"]["items"][0]["shippingAddress"]["address2"] = this._cc.ShippingAddress2;
				jobject["request"]["items"][0]["shippingAddress"]["address3"] = this._cc.ShippingAddress3;
				jobject["request"]["items"][0]["shippingAddress"]["city"] = this._cc.ShippingCity;
				jobject["request"]["items"][0]["shippingAddress"]["state"] = this._cc.ShippingStateJP;
				jobject["request"]["items"][0]["shippingAddress"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["request"]["items"][0]["shippingAddress"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["items"][0]["contactInfo"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["items"][0]["contactInfo"]["phoneNumber"] = this._cc.ShippingPhone;
			}
			this._buyJson = JObject.Parse(jobject.ToString());
			string url = "https://api.nike.com/buy/checkout_previews/v2/" + this._checkoutGuid;
			HttpResponse httpResponse = this.Http.Request("PUT", url, null, jobject.ToString(), "application/json; charset=UTF-8", null, this.Http.AllowAutoRedirect, this.Http.TimeOut);
			this.CheckNikeSnkrsError(httpResponse);
			if (httpResponse.JObject["status"].Value<string>() != "PENDING")
			{
				throw new Exception(httpResponse.Text);
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00031A28 File Offset: 0x0002FC28
		private void AddToCartStatus()
		{
			string url = "https://api.nike.com/buy/checkout_previews/v2/jobs/" + this._checkoutGuid;
			HttpResponse httpResponse = null;
			do
			{
				Thread.Sleep(1500);
				httpResponse = this.Http.Get(url);
				this.CheckNikeSnkrsError(httpResponse);
			}
			while (httpResponse.JObject["status"].Value<string>().EqualsAny(new string[]
			{
				"PENDING",
				"IN_PROGRESS"
			}));
			if (httpResponse.JObject["status"].Value<string>() != "COMPLETED")
			{
				throw new Exception(httpResponse.Text);
			}
			try
			{
				this._total = httpResponse.JObject["response"]["shippingGroups"][0]["items"][0]["priceInfo"]["total"].Value<float>();
			}
			catch (Exception)
			{
			}
			try
			{
				this._priceChecksum = httpResponse.JObject["response"]["priceChecksum"].Value<string>();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00031B6C File Offset: 0x0002FD6C
		private void SubmitPayment()
		{
			this.Http.Accept = "application/json";
			JObject jobject = JObject.Parse("{\"checkoutId\":\"5f966caa-c130-4fdb-8b79-778f3a7d16bc\",\"total\":230,\"currency\":\"USD\",\"country\":\"US\",\"items\":[{\"productId\":\"9e5a771a-cebc-531f-8a96-c5b8c48d3875\",\"shippingAddress\":{\"address1\":\"ADDR1\",\"address2\":\"Addr2\",\"city\":\"My City\",\"state\":\"NJ\",\"postalCode\":\"08854\",\"country\":\"US\"}}],\"paymentInfo\":[{\"id\":\"a8d2598c-3c53-4ea9-b6c0-dcb9aba1a4b4\",\"billingInfo\":{\"name\":{\"firstName\":\"Anna\",\"lastName\":\"Hazare\"},\"address\":{\"address1\":\"4278 Wines Lane\",\"city\":\"Houston\",\"state\":\"TX\",\"postalCode\":\"77002\",\"country\":\"US\"},\"contactInfo\":{\"phoneNumber\":\"8323272774\",\"email\":\"clayton.jolin20@gmail.com\"}},\"type\":\"CreditCard\",\"creditCardInfoId\":\"bc35760d-faf2-4ab9-927b-76e1b587ca68\"}]}");
			if (Form1.SiteType == SiteType.NikeUS)
			{
				jobject = JObject.Parse("{\"checkoutId\":\"5f966caa-c130-4fdb-8b79-778f3a7d16bc\",\"total\":230,\"currency\":\"USD\",\"country\":\"US\",\"items\":[{\"productId\":\"9e5a771a-cebc-531f-8a96-c5b8c48d3875\",\"shippingAddress\":{\"address1\":\"ADDR1\",\"address2\":\"Addr2\",\"city\":\"My City\",\"state\":\"NJ\",\"postalCode\":\"08854\",\"country\":\"US\"}}],\"paymentInfo\":[{\"id\":\"a8d2598c-3c53-4ea9-b6c0-dcb9aba1a4b4\",\"billingInfo\":{\"name\":{\"firstName\":\"Anna\",\"lastName\":\"Hazare\"},\"address\":{\"address1\":\"4278 Wines Lane\",\"city\":\"Houston\",\"state\":\"TX\",\"postalCode\":\"77002\",\"country\":\"US\"},\"contactInfo\":{\"phoneNumber\":\"8323272774\",\"email\":\"clayton.jolin20@gmail.com\"}},\"type\":\"CreditCard\",\"creditCardInfoId\":\"bc35760d-faf2-4ab9-927b-76e1b587ca68\"}]}");
				jobject["checkoutId"] = this._checkoutGuid;
				jobject["total"] = this._total;
				jobject["currency"] = NikeUrls.SnkrsCurrency;
				jobject["country"] = NikeUrls.NikeCountryCode;
				jobject["items"][0]["productId"] = this._pid;
				jobject["items"][0]["shippingAddress"]["address1"] = this._cc.ShippingAddress1;
				jobject["items"][0]["shippingAddress"]["address2"] = this._cc.ShippingAddress2;
				jobject["items"][0]["shippingAddress"]["city"] = this._cc.ShippingCity;
				jobject["items"][0]["shippingAddress"]["state"] = this._cc.ShippingState;
				jobject["items"][0]["shippingAddress"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["items"][0]["shippingAddress"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["paymentInfo"][0]["id"] = Guid.NewGuid().ToString();
				jobject["paymentInfo"][0]["billingInfo"]["name"]["firstName"] = this._cc.BillingFirstName;
				jobject["paymentInfo"][0]["billingInfo"]["name"]["lastName"] = this._cc.BillingLastName;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["address1"] = this._cc.BillingAddress1;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["address2"] = this._cc.BillingAddress2;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["city"] = this._cc.BillingCity;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["state"] = this._cc.BillingState;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["postalCode"] = this._cc.BillingZipCode;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["paymentInfo"][0]["billingInfo"]["contactInfo"]["phoneNumber"] = this._cc.BillingPhone;
				jobject["paymentInfo"][0]["billingInfo"]["contactInfo"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["paymentInfo"][0]["type"] = "CreditCard";
				jobject["paymentInfo"][0]["creditCardInfoId"] = this._cardId;
			}
			if (Form1.SiteType.EqualsAny(new SiteType[]
			{
				SiteType.NikeDE,
				SiteType.NikeFR,
				SiteType.NikeIT,
				SiteType.NikePL,
				SiteType.NikeUK,
				SiteType.NikeES,
				SiteType.NikeNL,
				SiteType.NikeSE,
				SiteType.NikeDK
			}))
			{
				jobject = JObject.Parse("{\"checkoutId\":\"ed9522c6-3225-4f5d-b5ce-6e669421c853\",\"total\":114.95,\"currency\":\"GBP\",\"country\":\"GB\",\"items\":[{\"productId\":\"bf23597d-3ac7-5a61-96d1-86f461765ce6\",\"shippingAddress\":{\"address1\":\"90 Exning Road\",\"city\":\"HARLOW HILL\",\"postalCode\":\"NE15 7DN\",\"country\":\"GB\"}}],\"paymentInfo\":[{\"id\":\"2858949e-2daf-4ecd-a76f-40d5b5d6ab57\",\"billingInfo\":{\"name\":{\"firstName\":\"Anna\",\"lastName\":\"Hazare\"},\"address\":{\"address1\":\"4278 Wines Lane\",\"city\":\"Houston\",\"postalCode\":\"77002\",\"country\":\"US\"},\"contactInfo\":{\"phoneNumber\":\"8323272774\",\"email\":\"clayton.jolin20@gmail.com\"}},\"type\":\"CreditCard\",\"creditCardInfoId\":\"52900715-6645-4bb0-8dfb-fbfaa1f42c45\"}]}");
				jobject["checkoutId"] = this._checkoutGuid;
				jobject["total"] = this._total;
				jobject["currency"] = NikeUrls.SnkrsCurrency;
				jobject["country"] = NikeUrls.NikeCountryCode;
				jobject["items"][0]["productId"] = this._pid;
				jobject["items"][0]["shippingAddress"]["address1"] = this._cc.ShippingAddress1;
				jobject["items"][0]["shippingAddress"]["address2"] = this._cc.ShippingAddress2;
				jobject["items"][0]["shippingAddress"]["city"] = this._cc.ShippingCity;
				jobject["items"][0]["shippingAddress"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["items"][0]["shippingAddress"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["paymentInfo"][0]["id"] = Guid.NewGuid().ToString();
				jobject["paymentInfo"][0]["billingInfo"]["name"]["firstName"] = this._cc.BillingFirstName;
				jobject["paymentInfo"][0]["billingInfo"]["name"]["lastName"] = this._cc.BillingLastName;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["address1"] = this._cc.BillingAddress1;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["address2"] = this._cc.BillingAddress2;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["city"] = this._cc.BillingCity;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["postalCode"] = this._cc.BillingZipCode;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["paymentInfo"][0]["billingInfo"]["contactInfo"]["phoneNumber"] = this._cc.BillingPhone;
				jobject["paymentInfo"][0]["billingInfo"]["contactInfo"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["paymentInfo"][0]["type"] = "CreditCard";
				jobject["paymentInfo"][0]["creditCardInfoId"] = this._cardId;
			}
			if (Form1.SiteType == SiteType.NikeJP)
			{
				jobject = JObject.Parse("{\"checkoutId\":\"9b6e3c58-df55-40c9-8f83-72fe4346a318\",\"total\":17280,\"currency\":\"JPY\",\"country\":\"JP\",\"items\":[{\"productId\":\"8db67869-f451-5293-85f3-4319e1cedae1\",\"shippingAddress\":{\"address1\":\"A1\",\"address2\":\"A2\",\"address3\":\"Town Area\",\"city\":\"City\",\"state\":\"JP-04\",\"postalCode\":\"145-8589\",\"country\":\"JP\"}}],\"paymentInfo\":[{\"id\":\"fcb55227-5c36-4602-8d50-54951b5d4814\",\"billingInfo\":{\"name\":{\"firstName\":\"nmm\",\"lastName\":\"name\"},\"address\":{\"address1\":\"A11\",\"city\":\"City\",\"state\":\"JP-03\",\"postalCode\":\"145-8965\",\"country\":\"JP\"},\"contactInfo\":{\"phoneNumber\":\"85987458968\",\"email\":\"clayton.jolin20@gmail.com\"}},\"type\":\"CreditCard\",\"creditCardInfoId\":\"b1cb9009-f6ab-47ca-9165-02fa89261a23\"}]}");
				jobject["checkoutId"] = this._checkoutGuid;
				jobject["total"] = this._total;
				jobject["currency"] = NikeUrls.SnkrsCurrency;
				jobject["country"] = NikeUrls.NikeCountryCode;
				jobject["items"][0]["productId"] = this._pid;
				jobject["items"][0]["shippingAddress"]["address1"] = this._cc.ShippingAddress1;
				jobject["items"][0]["shippingAddress"]["address2"] = this._cc.ShippingAddress2;
				jobject["items"][0]["shippingAddress"]["address3"] = this._cc.ShippingAddress3;
				jobject["items"][0]["shippingAddress"]["city"] = this._cc.ShippingCity;
				jobject["items"][0]["shippingAddress"]["state"] = this._cc.ShippingStateJP;
				jobject["items"][0]["shippingAddress"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["items"][0]["shippingAddress"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["paymentInfo"][0]["id"] = Guid.NewGuid().ToString();
				jobject["paymentInfo"][0]["billingInfo"]["name"]["firstName"] = this._cc.BillingFirstName;
				jobject["paymentInfo"][0]["billingInfo"]["name"]["lastName"] = this._cc.BillingLastName;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["address1"] = this._cc.BillingAddress1;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["address2"] = this._cc.BillingAddress2;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["address3"] = this._cc.BillingAddress3;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["city"] = this._cc.BillingCity;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["state"] = this._cc.BillingStateJP;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["postalCode"] = this._cc.BillingZipCode;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["paymentInfo"][0]["billingInfo"]["contactInfo"]["phoneNumber"] = this._cc.BillingPhone;
				jobject["paymentInfo"][0]["billingInfo"]["contactInfo"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["paymentInfo"][0]["type"] = "CreditCard";
				jobject["paymentInfo"][0]["creditCardInfoId"] = this._cardId;
			}
			string url = "https://api.nike.com/payment/preview/v2/";
			HttpResponse httpResponse = this.Http.Post(url, null, jobject.ToString(), "application/json; charset=UTF-8");
			this.CheckNikeSnkrsError(httpResponse);
			if (httpResponse.JObject["status"].Value<string>() != "PENDING")
			{
				throw new Exception(httpResponse.Text);
			}
			this._paymentGuid = httpResponse.JObject["id"].Value<string>();
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00032BAC File Offset: 0x00030DAC
		private void SubmitPaymentStatus()
		{
			string url = "https://api.nike.com/payment/preview/v2/jobs/" + this._paymentGuid;
			HttpResponse httpResponse;
			do
			{
				Thread.Sleep(1500);
				httpResponse = this.Http.Get(url);
				this.CheckNikeSnkrsError(httpResponse);
			}
			while (httpResponse.JObject["status"].Value<string>().EqualsAny(new string[]
			{
				"PENDING",
				"IN_PROGRESS"
			}));
			if (httpResponse.JObject["status"].Value<string>() != "COMPLETED")
			{
				throw new Exception(httpResponse.Text);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00032C4C File Offset: 0x00030E4C
		private void FinalizeOrder()
		{
			this.Http.Accept = "application/json";
			JObject jobject = JObject.Parse("{\"request\":{\"email\":\"clayton.jolin20@gmail.com\",\"country\":\"US\",\"currency\":\"USD\",\"locale\":\"en_US\",\"channel\":\"SNKRS\",\"items\":[{\"id\":\"9e5a771a-cebc-531f-8a96-c5b8c48d3875\",\"skuId\":\"a834899b-3a89-549f-9d3d-abe3938ae5e0\",\"quantity\":1,\"recipient\":{\"firstName\":\"Clayton\",\"lastName\":\"Jolin\"},\"shippingAddress\":{\"address1\":\"ADDR1\",\"address2\":\"Addr2\",\"city\":\"My City\",\"state\":\"NJ\",\"postalCode\":\"08854\",\"country\":\"US\"},\"contactInfo\":{\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"3478368677\"},\"shippingMethod\":\"STANDARD\"}],\"paymentToken\":\"95431f90-07b6-4122-b1dd-763fd39f124f\"}}");
			if (Form1.SiteType == SiteType.NikeUS)
			{
				jobject = JObject.Parse("{\"request\":{\"email\":\"clayton.jolin20@gmail.com\",\"country\":\"US\",\"currency\":\"USD\",\"locale\":\"en_US\",\"channel\":\"SNKRS\",\"items\":[{\"id\":\"9e5a771a-cebc-531f-8a96-c5b8c48d3875\",\"skuId\":\"a834899b-3a89-549f-9d3d-abe3938ae5e0\",\"quantity\":1,\"recipient\":{\"firstName\":\"Clayton\",\"lastName\":\"Jolin\"},\"shippingAddress\":{\"address1\":\"ADDR1\",\"address2\":\"Addr2\",\"city\":\"My City\",\"state\":\"NJ\",\"postalCode\":\"08854\",\"country\":\"US\"},\"contactInfo\":{\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"3478368677\"},\"shippingMethod\":\"STANDARD\"}],\"paymentToken\":\"95431f90-07b6-4122-b1dd-763fd39f124f\"}}");
				jobject["request"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["currency"] = NikeUrls.SnkrsCurrency;
				jobject["request"]["locale"] = NikeUrls.NikeLangLocale;
				jobject["request"]["items"][0]["id"] = this._pid;
				jobject["request"]["items"][0]["skuId"] = this._skuId;
				jobject["request"]["items"][0]["recipient"]["firstName"] = this._cc.ShippingFirstName;
				jobject["request"]["items"][0]["recipient"]["lastName"] = this._cc.ShippingLastName;
				jobject["request"]["items"][0]["shippingAddress"]["address1"] = this._cc.ShippingAddress1;
				jobject["request"]["items"][0]["shippingAddress"]["address2"] = this._cc.ShippingAddress2;
				jobject["request"]["items"][0]["shippingAddress"]["city"] = this._cc.ShippingCity;
				jobject["request"]["items"][0]["shippingAddress"]["state"] = this._cc.ShippingState;
				jobject["request"]["items"][0]["shippingAddress"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["request"]["items"][0]["shippingAddress"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["items"][0]["contactInfo"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["items"][0]["contactInfo"]["phoneNumber"] = this._cc.ShippingPhone;
				jobject["request"]["paymentToken"] = this._paymentGuid;
			}
			if (Form1.SiteType.EqualsAny(new SiteType[]
			{
				SiteType.NikeDE,
				SiteType.NikeFR,
				SiteType.NikeIT,
				SiteType.NikePL,
				SiteType.NikeUK,
				SiteType.NikeES,
				SiteType.NikeNL,
				SiteType.NikeSE,
				SiteType.NikeDK
			}))
			{
				jobject = JObject.Parse("{\"request\":{\"email\":\"clayton.jolin20@gmail.com\",\"country\":\"GB\",\"currency\":\"GBP\",\"locale\":\"en_GB\",\"channel\":\"SNKRS\",\"items\":[{\"id\":\"bf23597d-3ac7-5a61-96d1-86f461765ce6\",\"skuId\":\"497b5fa8-4eb4-584a-8d94-9668fb74b60d\",\"quantity\":1,\"recipient\":{\"firstName\":\"Lokesh\",\"lastName\":\"Pharma\"},\"shippingAddress\":{\"address1\":\"90 Exning Road\",\"city\":\"HARLOW HILL\",\"postalCode\":\"NE15 7DN\",\"country\":\"GB\"},\"contactInfo\":{\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"7441909052\"},\"shippingMethod\":\"GROUND_SERVICE\"}],\"paymentToken\":\"01a15428-7787-4a88-b72f-e2e29a36f2e2\"}}");
				jobject["request"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["currency"] = NikeUrls.SnkrsCurrency;
				jobject["request"]["locale"] = NikeUrls.NikeLangLocale;
				jobject["request"]["items"][0]["id"] = this._pid;
				jobject["request"]["items"][0]["skuId"] = this._skuId;
				jobject["request"]["items"][0]["recipient"]["firstName"] = this._cc.ShippingFirstName;
				jobject["request"]["items"][0]["recipient"]["lastName"] = this._cc.ShippingLastName;
				jobject["request"]["items"][0]["shippingAddress"]["address1"] = this._cc.ShippingAddress1;
				jobject["request"]["items"][0]["shippingAddress"]["address2"] = this._cc.ShippingAddress2;
				jobject["request"]["items"][0]["shippingAddress"]["city"] = this._cc.ShippingCity;
				jobject["request"]["items"][0]["shippingAddress"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["request"]["items"][0]["shippingAddress"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["items"][0]["contactInfo"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["items"][0]["contactInfo"]["phoneNumber"] = this._cc.ShippingPhone;
				jobject["request"]["paymentToken"] = this._paymentGuid;
			}
			if (Form1.SiteType == SiteType.NikeJP)
			{
				jobject = JObject.Parse("{\"request\":{\"email\":\"clayton.jolin20@gmail.com\",\"country\":\"JP\",\"currency\":\"JPY\",\"locale\":\"ja_JP\",\"channel\":\"SNKRS\",\"items\":[{\"id\":\"8db67869-f451-5293-85f3-4319e1cedae1\",\"skuId\":\"4ec209db-26e8-5d3e-86c8-712b92324a1b\",\"quantity\":1,\"recipient\":{\"firstName\":\"nmm\",\"altFirstName\":\"nmm\",\"lastName\":\"name\",\"altLastName\":\"name\"},\"shippingAddress\":{\"address1\":\"A1\",\"address2\":\"A2\",\"address3\":\"Town Area\",\"city\":\"City\",\"state\":\"JP-04\",\"postalCode\":\"145-8589\",\"country\":\"JP\"},\"contactInfo\":{\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"85987458968\"},\"shippingMethod\":\"GROUND_SERVICE\"}],\"paymentToken\":\"c41220cb-8fe4-41e3-8607-be32932756de\"}}");
				jobject["request"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["currency"] = NikeUrls.SnkrsCurrency;
				jobject["request"]["locale"] = NikeUrls.NikeLangLocale;
				jobject["request"]["items"][0]["id"] = this._pid;
				jobject["request"]["items"][0]["skuId"] = this._skuId;
				jobject["request"]["items"][0]["recipient"]["firstName"] = this._cc.ShippingFirstName;
				jobject["request"]["items"][0]["recipient"]["altFirstName"] = this._cc.ShippingFirstName;
				jobject["request"]["items"][0]["recipient"]["lastName"] = this._cc.ShippingLastName;
				jobject["request"]["items"][0]["recipient"]["altLastName"] = this._cc.ShippingLastName;
				jobject["request"]["items"][0]["shippingAddress"]["address1"] = this._cc.ShippingAddress1;
				jobject["request"]["items"][0]["shippingAddress"]["address2"] = this._cc.ShippingAddress2;
				jobject["request"]["items"][0]["shippingAddress"]["address3"] = this._cc.ShippingAddress3;
				jobject["request"]["items"][0]["shippingAddress"]["city"] = this._cc.ShippingCity;
				jobject["request"]["items"][0]["shippingAddress"]["state"] = this._cc.ShippingStateJP;
				jobject["request"]["items"][0]["shippingAddress"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["request"]["items"][0]["shippingAddress"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["items"][0]["contactInfo"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["items"][0]["contactInfo"]["phoneNumber"] = this._cc.ShippingPhone;
				jobject["request"]["paymentToken"] = this._paymentGuid;
			}
			this._buyJson = JObject.Parse(jobject.ToString());
			string url = "https://api.nike.com/buy/checkouts/v2/" + this._checkoutGuid;
			HttpResponse httpResponse = this.Http.Request("PUT", url, null, jobject.ToString(), "application/json; charset=UTF-8", null, this.Http.AllowAutoRedirect, this.Http.TimeOut);
			this.CheckNikeSnkrsError(httpResponse);
			if (httpResponse.JObject["status"].Value<string>() != "PENDING")
			{
				throw new Exception(httpResponse.Text);
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00033928 File Offset: 0x00031B28
		private void FinalizeOrderStatus()
		{
			string url = "https://api.nike.com/buy/checkouts/v2/jobs/" + this._checkoutGuid;
			HttpResponse httpResponse;
			do
			{
				Thread.Sleep(1500);
				httpResponse = this.Http.Get(url);
				this.CheckNikeSnkrsError(httpResponse);
			}
			while (httpResponse.JObject["status"].Value<string>().EqualsAny(new string[]
			{
				"PENDING",
				"IN_PROGRESS"
			}));
			if (httpResponse.JObject["status"].Value<string>() != "COMPLETED")
			{
				throw new Exception(httpResponse.Text);
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000339C8 File Offset: 0x00031BC8
		private void LaunchEntry()
		{
			HttpResponse httpResponse = this.Http.Get("https://api.nike.com/launch/launch_views/v2/?filter=productId({0})".With(new object[]
			{
				this._pid
			}));
			this.CheckNikeSnkrsError(httpResponse);
			this._launchId = httpResponse.JObject["objects"][0]["id"].Value<string>();
			this.Http.Accept = "application/json";
			JObject jobject = JObject.Parse("{\"launchId\":\"82f0ded1-ecad-4a4a-8205-92f89ba16159\",\"skuId\":\"7d24ce5c-8358-5d30-8896-97eea24e0f52\",\"locale\":\"en_GB\",\"currency\":\"GBP\",\"checkoutId\":\"1e114487-7285-416c-8c57-b19eb70b3e84\",\"shipping\":{\"recipient\":{\"firstName\":\"Lokesh\",\"lastName\":\"Pharma\",\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"7441909052\"},\"address\":{\"address1\":\"ADDR1\",\"address2\":\"Addr2\",\"city\":\"My City\",\"state\":\"NJ\",\"postalCode\":\"08854\",\"country\":\"US\"},\"method\":\"STANDARD\"},\"priceChecksum\":\"5213ddf22a9fd6c3eac7d45d5114dc09\",\"paymentToken\":\"7f7ca660-0b75-4516-89de-8e35552b39c1\",\"channel\":\"SNKRS\"}");
			if (Form1.SiteType == SiteType.NikeUS)
			{
				jobject = JObject.Parse("{\"launchId\":\"82f0ded1-ecad-4a4a-8205-92f89ba16159\",\"skuId\":\"7d24ce5c-8358-5d30-8896-97eea24e0f52\",\"locale\":\"en_GB\",\"currency\":\"GBP\",\"checkoutId\":\"1e114487-7285-416c-8c57-b19eb70b3e84\",\"shipping\":{\"recipient\":{\"firstName\":\"Lokesh\",\"lastName\":\"Pharma\",\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"7441909052\"},\"address\":{\"address1\":\"ADDR1\",\"address2\":\"Addr2\",\"city\":\"My City\",\"state\":\"NJ\",\"postalCode\":\"08854\",\"country\":\"US\"},\"method\":\"STANDARD\"},\"priceChecksum\":\"5213ddf22a9fd6c3eac7d45d5114dc09\",\"paymentToken\":\"7f7ca660-0b75-4516-89de-8e35552b39c1\",\"channel\":\"SNKRS\"}");
				jobject["launchId"] = this._launchId;
				jobject["skuId"] = this._skuId;
				jobject["locale"] = NikeUrls.NikeLangLocale;
				jobject["currency"] = NikeUrls.SnkrsCurrency;
				jobject["checkoutId"] = this._checkoutGuid;
				jobject["shipping"]["recipient"]["firstName"] = this._cc.ShippingFirstName;
				jobject["shipping"]["recipient"]["lastName"] = this._cc.ShippingLastName;
				jobject["shipping"]["recipient"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["shipping"]["recipient"]["phoneNumber"] = this._cc.ShippingPhone;
				jobject["shipping"]["address"]["address1"] = this._cc.ShippingAddress1;
				jobject["shipping"]["address"]["address2"] = this._cc.ShippingAddress2;
				jobject["shipping"]["address"]["city"] = this._cc.ShippingCity;
				jobject["shipping"]["address"]["state"] = this._cc.ShippingState;
				jobject["shipping"]["address"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["shipping"]["address"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["priceChecksum"] = this._priceChecksum;
				jobject["paymentToken"] = this._paymentGuid;
			}
			if (Form1.SiteType.EqualsAny(new SiteType[]
			{
				SiteType.NikeDE,
				SiteType.NikeFR,
				SiteType.NikeIT,
				SiteType.NikePL,
				SiteType.NikeUK,
				SiteType.NikeES,
				SiteType.NikeNL,
				SiteType.NikeSE,
				SiteType.NikeDK
			}))
			{
				jobject = JObject.Parse("{\"launchId\":\"82f0ded1-ecad-4a4a-8205-92f89ba16159\",\"skuId\":\"7d24ce5c-8358-5d30-8896-97eea24e0f52\",\"locale\":\"en_GB\",\"currency\":\"GBP\",\"checkoutId\":\"1e114487-7285-416c-8c57-b19eb70b3e84\",\"shipping\":{\"recipient\":{\"firstName\":\"Lokesh\",\"lastName\":\"Pharma\",\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"7441909052\"},\"address\":{\"address1\":\"ADDR1\",\"address2\":\"Addr2\",\"city\":\"My City\",\"postalCode\":\"08854\",\"country\":\"US\"},\"method\":\"GROUND_SERVICE\"},\"priceChecksum\":\"5213ddf22a9fd6c3eac7d45d5114dc09\",\"paymentToken\":\"7f7ca660-0b75-4516-89de-8e35552b39c1\",\"channel\":\"SNKRS\"}");
				jobject["launchId"] = this._launchId;
				jobject["skuId"] = this._skuId;
				jobject["locale"] = NikeUrls.NikeLangLocale;
				jobject["currency"] = NikeUrls.SnkrsCurrency;
				jobject["checkoutId"] = this._checkoutGuid;
				jobject["shipping"]["recipient"]["firstName"] = this._cc.ShippingFirstName;
				jobject["shipping"]["recipient"]["lastName"] = this._cc.ShippingLastName;
				jobject["shipping"]["recipient"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["shipping"]["recipient"]["phoneNumber"] = this._cc.ShippingPhone;
				jobject["shipping"]["address"]["address1"] = this._cc.ShippingAddress1;
				jobject["shipping"]["address"]["address2"] = this._cc.ShippingAddress2;
				jobject["shipping"]["address"]["city"] = this._cc.ShippingCity;
				jobject["shipping"]["address"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["shipping"]["address"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["priceChecksum"] = this._priceChecksum;
				jobject["paymentToken"] = this._paymentGuid;
			}
			if (Form1.SiteType == SiteType.NikeJP)
			{
				jobject = JObject.Parse("{\"launchId\":\"8ced592c-832e-41d4-b4ee-c660e7b53729\",\"skuId\":\"f830e2da-7f58-5702-a763-bf5738516db3\",\"locale\":\"ja_JP\",\"currency\":\"JPY\",\"checkoutId\":\"44fb6b30-43de-4e30-bf35-c778e75a6572\",\"shipping\":{\"recipient\":{\"firstName\":\"Lok\",\"altFirstName\":\"KIjsdh\",\"lastName\":\"Name\",\"altLastName\":\"Kij\",\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"8958965874589\"},\"address\":{\"address1\":\"シズオカケン\",\"address2\":\"\",\"address3\":\"シズオカケン\",\"city\":\"シズオカシアオイク\",\"state\":\"JP-04\",\"postalCode\":\"489-1096\",\"country\":\"JP\"},\"method\":\"GROUND_SERVICE\"},\"priceChecksum\":\"af9cbd7168bbd45ce37c5f2ccd26bf93\",\"paymentToken\":\"79c31ab5-cbba-461c-a4a7-b5718fa0d2ad\",\"channel\":\"SNKRS\"}");
				jobject["launchId"] = this._launchId;
				jobject["skuId"] = this._skuId;
				jobject["locale"] = NikeUrls.NikeLangLocale;
				jobject["currency"] = NikeUrls.SnkrsCurrency;
				jobject["checkoutId"] = this._checkoutGuid;
				jobject["shipping"]["recipient"]["firstName"] = this._cc.ShippingFirstName;
				jobject["shipping"]["recipient"]["altFirstName"] = this._cc.ShippingFirstName;
				jobject["shipping"]["recipient"]["lastName"] = this._cc.ShippingLastName;
				jobject["shipping"]["recipient"]["altLastName"] = this._cc.ShippingLastName;
				jobject["shipping"]["recipient"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["shipping"]["recipient"]["phoneNumber"] = this._cc.ShippingPhone;
				jobject["shipping"]["address"]["address1"] = this._cc.ShippingAddress1;
				jobject["shipping"]["address"]["address2"] = this._cc.ShippingAddress2;
				jobject["shipping"]["address"]["address3"] = this._cc.ShippingAddress3;
				jobject["shipping"]["address"]["city"] = this._cc.ShippingCity;
				jobject["shipping"]["address"]["state"] = this._cc.ShippingStateJP;
				jobject["shipping"]["address"]["postalCode"] = this._cc.ShippingZipCode;
				jobject["shipping"]["address"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["priceChecksum"] = this._priceChecksum;
				jobject["paymentToken"] = this._paymentGuid;
			}
			string url = "https://api.nike.com/launch/entries/v2/";
			httpResponse = this.Http.Post(url, null, jobject.ToString(), "application/json; charset=UTF-8");
			this.CheckNikeSnkrsError(httpResponse);
			this._drawGuid = httpResponse.JObject["id"].Value<string>();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000342B8 File Offset: 0x000324B8
		private JObject LaunchEntryStatus()
		{
			string url = "https://api.nike.com/launch/entries/v2/" + this._drawGuid;
			HttpResponse httpResponse;
			do
			{
				Thread.Sleep(1500);
				httpResponse = this.Http.Get(url);
				this.CheckNikeSnkrsError(httpResponse);
			}
			while (httpResponse.JObject["result"] == null);
			return httpResponse.JObject;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00034310 File Offset: 0x00032510
		public bool GetProductDetails()
		{
			bool result;
			try
			{
				string url = "https://api.nike.com/commerce/productfeed/products/snkrs/{0}/thread?country={1}&locale={2}&withCards=true".With(new object[]
				{
					this.AtcItem.Details.StyleCode,
					NikeUrls.NikeCountryCode,
					NikeUrls.NikeLangLocale
				});
				string url2 = "https://api.nike.com/commerce/productfeed/products/launch/{0}/thread?country={1}&locale={2}&withCards=true".With(new object[]
				{
					this.AtcItem.Details.StyleCode,
					NikeUrls.NikeCountryCode,
					NikeUrls.NikeLangLocale
				});
				Logger.Log("{0} (Size: {2}): Getting product details. {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Details.StyleCode,
					this.AtcItem.Size
				}), true, true);
				HttpResponse httpResponse = this.Http.Get(url);
				if (httpResponse.Exception != null)
				{
					HttpResponse httpResponse2 = this.Http.Get(url2);
					if (httpResponse2.Exception != null)
					{
						throw new Exception(httpResponse.Text ?? httpResponse.Exception.Message);
					}
					httpResponse = httpResponse2;
				}
				if (!httpResponse.IsJson)
				{
					throw new Exception("Received invalid json from nike calendar service.");
				}
				try
				{
					this.AtcItem.Details.ProductId = httpResponse.JObject["id"].Value<string>();
					this.AtcItem.Details.StyleCode = httpResponse.JObject["product"]["style"].Value<string>() + "-" + httpResponse.JObject["product"]["colorCode"].Value<string>();
					this.AtcItem.Details.Line1 = httpResponse.JObject["product"]["title"].Value<string>();
					this.AtcItem.Details.Line2 = httpResponse.JObject["product"]["subtitle"].Value<string>();
					this.AtcItem.Details.ProductImage = httpResponse.JObject["product"]["imageUrl"].Value<string>();
					this.AtcItem.Details.Price = httpResponse.JObject["product"]["price"]["fullRetailPrice"].Value<string>();
					this._snkrsProduct = JObject.Parse(httpResponse.JObject["product"].ToString());
				}
				catch (Exception)
				{
					Thread.Sleep(3000);
					throw new Exception("Unable to get product details...");
				}
				this._isLeo = false;
				try
				{
					this._isLeo = httpResponse.JObject["product"]["selectionEngine"].Value<string>().EqualsAny(new string[]
					{
						"LEO",
						"DAN"
					});
				}
				catch (Exception)
				{
				}
				this._launchDate = DateTime.Parse(httpResponse.JObject["product"]["startSellDate"].Value<string>() + "Z", CultureInfo.InvariantCulture);
				DateTime d = this._launchDate;
				d = d.Subtract(TimeSpan.FromMinutes(25.0));
				DateTime d2 = DateTime.Parse(httpResponse.Headers["Date"], CultureInfo.InvariantCulture);
				TimeSpan timeSpan = d - d2;
				TimeSpan span = this._launchDate - d2;
				if (timeSpan.TotalSeconds > 0.0)
				{
					double num = 0.0;
					double totalSeconds = timeSpan.TotalSeconds;
					Logger.Log("{0} (Size: {2}): Waiting for product {1} to go live. Product will be live in {3}".With(new object[]
					{
						this.AtcItem.Account.EmailAddress,
						this.AtcItem.Details.Line1,
						this.AtcItem.Size,
						Better_Nike_Bot.AddToCart.ToReadableString(span)
					}), true, true);
					while (num < totalSeconds)
					{
						Thread.Sleep(5000);
						num += 5.0;
						if (this.AtcItem.ShouldStop.Value)
						{
							return true;
						}
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				Logger.Log("{0}: Error getting product details!!! Retrying... {2} {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Url,
					ex.Message
				}), true, true);
				Thread.Sleep(3000);
				result = false;
			}
			return result;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00003A98 File Offset: 0x00001C98
		public static string VersionString
		{
			get
			{
				return Form1.RemoteSettings["version_string"].Value<string>();
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00034808 File Offset: 0x00032A08
		public static string MobileLoginUrl
		{
			get
			{
				return "https://s3.nikecdn.com/unite/mobile.html?mid=66339197893423567262507232821955543170?iOSSDKVersion=2.8.4&clientId=G64vA0b95ZruUtGk1K0FkAgaO3Ch30sj&uxId=com.nike.commerce.snkrs.ios&view=none&locale={0}&backendEnvironment=identity".With(new object[]
				{
					NikeUrls.NikeLangLocale
				});
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00034830 File Offset: 0x00032A30
		public static string PerformLoginNew(Account acc)
		{
			if (Form1.ShouldStop)
			{
				return null;
			}
			Http http = acc.Http;
			List<Cookie> cookies = LoginBrowser.GetCookies(http.Proxy);
			foreach (Cookie cookie in cookies)
			{
				http.Cookies.Add(cookie);
			}
			string url = "https://s3.nikecdn.com/login?{0}&uxid=com.nike.commerce.snkrs.ios&locale={1}&backendEnvironment=identity&browser=Apple%20Computer%2C%20Inc.&os=undefined&mobile=true&native=true&visit=1&visitor={2}".With(new object[]
			{
				WebSnkrs.VersionString,
				NikeUrls.NikeLangLocale,
				Guid.NewGuid().ToString()
			});
			string mobileLoginUrl = WebSnkrs.MobileLoginUrl;
			string str = "https://s3.nikecdn.com";
			string postContentType = "text/plain";
			http.ResetAllHeaders();
			http.Accept = "*/*";
			Dictionary<string, string> objectToSerialize = new Dictionary<string, string>
			{
				{
					"username",
					acc.EmailAddress
				},
				{
					"password",
					acc.Password
				},
				{
					"client_id",
					"G64vA0b95ZruUtGk1K0FkAgaO3Ch30sj"
				},
				{
					"keepMeLoggedIn",
					"true"
				},
				{
					"ux_id",
					"com.nike.commerce.snkrs.ios"
				},
				{
					"grant_type",
					"password"
				}
			};
			string postContentType2 = http.PostContentType;
			http.PostContentType = postContentType;
			HttpResponse httpResponse = http.Post(url, mobileLoginUrl, objectToSerialize.ToJSON(), new string[]
			{
				"Origin: " + str
			});
			http.PostContentType = postContentType2;
			if (httpResponse.Exception != null)
			{
				if (httpResponse.Exception.ToString().ContainsAny(new string[]
				{
					"403",
					"429"
				}))
				{
					Thread.Sleep(3500);
				}
				throw (!httpResponse.Text.IsNullOrWhiteSpace()) ? new Exception(httpResponse.Text) : httpResponse.Exception;
			}
			if (!httpResponse.IsJson)
			{
				throw new Exception("Login returned invalid json");
			}
			WebSnkrs.SaveToken(acc.EmailAddress, httpResponse.Text);
			return httpResponse.JObject["access_token"].Value<string>();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00034A60 File Offset: 0x00032C60
		public static string TokenRefresh(Account acc)
		{
			if (Form1.ShouldStop)
			{
				return null;
			}
			NikeToken nikeToken = Form1.Tokens[acc.EmailAddress];
			if (nikeToken.RefreshToken.IsNullOrWhiteSpace())
			{
				throw new Exception();
			}
			if (WebSnkrs.GetCurrentSeconds() - nikeToken.Timestamp < 600L)
			{
				return nikeToken.AccessToken;
			}
			Http http = acc.Http;
			string url = "https://unite.nike.com/tokenRefresh?{0}&uxid=com.nike.commerce.snkrs.ios&locale={1}&backendEnvironment=identity&browser=Apple%20Computer%2C%20Inc.&os=undefined&mobile=true&native=true&visit=1&visitor={2}".With(new object[]
			{
				WebSnkrs.VersionString,
				NikeUrls.NikeLangLocale,
				Guid.NewGuid().ToString()
			});
			string mobileLoginUrl = WebSnkrs.MobileLoginUrl;
			string str = "https://unite.nike.com";
			string postContentType = "text/plain";
			http.ResetAllHeaders();
			http.Accept = "*/*";
			Dictionary<string, string> objectToSerialize = new Dictionary<string, string>
			{
				{
					"refresh_token",
					nikeToken.RefreshToken
				},
				{
					"client_id",
					"G64vA0b95ZruUtGk1K0FkAgaO3Ch30sj"
				},
				{
					"grant_type",
					"refresh_token"
				}
			};
			string postContentType2 = http.PostContentType;
			http.PostContentType = postContentType;
			HttpResponse httpResponse = http.Post(url, mobileLoginUrl, objectToSerialize.ToJSON(), new string[]
			{
				"Origin: " + str
			});
			http.PostContentType = postContentType2;
			if (httpResponse.Exception != null)
			{
				if (httpResponse.Exception.ToString().ContainsAny(new string[]
				{
					"403",
					"429"
				}))
				{
					Thread.Sleep(3500);
				}
				throw (!httpResponse.Text.IsNullOrWhiteSpace()) ? new Exception(httpResponse.Text) : httpResponse.Exception;
			}
			if (!httpResponse.IsJson)
			{
				throw new Exception("Login returned invalid json");
			}
			WebSnkrs.SaveToken(acc.EmailAddress, httpResponse.Text);
			return httpResponse.JObject["access_token"].Value<string>();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00034C44 File Offset: 0x00032E44
		public static long GetCurrentSeconds()
		{
			return (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1).ToUniversalTime()).TotalSeconds;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00034C7C File Offset: 0x00032E7C
		public static string PerformLogin(Account acc)
		{
			string text = "";
			try
			{
				text = WebSnkrs.TokenRefresh(acc);
			}
			catch (Exception)
			{
			}
			if (!text.IsNullOrWhiteSpace())
			{
				return text;
			}
			try
			{
				text = WebSnkrs.PerformLoginOld(acc);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("401"))
				{
					Logger.Log("401 Error = Wrong Email/Password... Please check the details entered...", true, true);
					throw;
				}
			}
			if (!text.IsNullOrWhiteSpace())
			{
				return text;
			}
			return WebSnkrs.PerformLoginNew(acc);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00034D00 File Offset: 0x00032F00
		public static string PerformLoginOld(Account acc)
		{
			if (Form1.ShouldStop)
			{
				return null;
			}
			Http http = acc.Http;
			string url = "https://s3.nikecdn.com/login?{0}&uxid=com.nike.commerce.omega.ios&locale={1}&backendEnvironment=identity&browser=Apple%20Computer%2C%20Inc.&os=undefined&mobile=true&native=true&visit=1&visitor={2}".With(new object[]
			{
				WebSnkrs.VersionString,
				NikeUrls.NikeLangLocale,
				Guid.NewGuid().ToString()
			});
			string referer = "https://s3.nikecdn.com/unite/mobile.html?iOSSDKVersion=2.8.4&clientId=0Zrbh9wN0CwjeczJAoKvc8US44Ogf49X&uxId=com.nike.commerce.omega.ios&view=none&locale=en_US&backendEnvironment=identity&facebookAppId=1055885851158192";
			string str = "https://awr.svs.nike.com";
			string postContentType = "text/plain";
			try
			{
				string text = Form1.RemoteSettings["snkrs_login_url"].Value<string>();
				string text2 = Form1.RemoteSettings["snkrs_login_ref"].Value<string>();
				string text3 = Form1.RemoteSettings["snkrs_login_origin"].Value<string>();
				string text4 = Form1.RemoteSettings["snkrs_login_cType"].Value<string>();
				if (text.Length > 5 && text2.Length > 5 && text3.Length > 5 && text4.Length > 5)
				{
					url = text.With(new object[]
					{
						WebSnkrs.VersionString,
						NikeUrls.NikeLangLocale,
						Guid.NewGuid().ToString()
					});
					referer = text2;
					str = text3;
					postContentType = text4;
				}
			}
			catch (Exception)
			{
			}
			http.ResetAllHeaders();
			http.Accept = "*/*";
			Dictionary<string, string> objectToSerialize = new Dictionary<string, string>
			{
				{
					"username",
					acc.EmailAddress
				},
				{
					"password",
					acc.Password
				},
				{
					"client_id",
					"G64vA0b95ZruUtGk1K0FkAgaO3Ch30sj"
				},
				{
					"keepMeLoggedIn",
					"true"
				},
				{
					"ux_id",
					"com.nike.commerce.snkrs.ios"
				},
				{
					"grant_type",
					"password"
				},
				{
					"contentLocale",
					NikeUrls.NikeLangLocale
				}
			};
			string postContentType2 = http.PostContentType;
			http.PostContentType = postContentType;
			HttpResponse httpResponse = http.Post(url, referer, objectToSerialize.ToJSON(), new string[]
			{
				"Origin: " + str
			});
			http.PostContentType = postContentType2;
			if (httpResponse.Exception != null)
			{
				if (httpResponse.Exception.ToString().ContainsAny(new string[]
				{
					"403",
					"429"
				}))
				{
					Thread.Sleep(3500);
				}
				throw (!httpResponse.Text.IsNullOrWhiteSpace()) ? new Exception(httpResponse.Text) : httpResponse.Exception;
			}
			if (!httpResponse.IsJson)
			{
				throw new Exception("Login returned invalid json");
			}
			WebSnkrs.SaveToken(acc.EmailAddress, httpResponse.Text);
			return httpResponse.JObject["access_token"].Value<string>();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00034FC4 File Offset: 0x000331C4
		public static void SaveToken(string email, string json)
		{
			try
			{
				NikeToken nikeToken = JsonConvert.DeserializeObject<NikeToken>(json);
				nikeToken.Timestamp = WebSnkrs.GetCurrentSeconds();
				Form1.Tokens[email] = nikeToken;
				Form1.DefaultForm.SaveSettings();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00035010 File Offset: 0x00033210
		private bool CheckCheckoutLimitReached(CreditCardProfile cc)
		{
			if (this.AtcItem.ShouldStop.Value)
			{
				Logger.Log("stopped by user.", true, true);
				return true;
			}
			if (cc.MaxCheckouts == 0)
			{
				return false;
			}
			if (cc.CheckoutCount >= cc.MaxCheckouts)
			{
				Logger.Log("Checkout Limit Reached for cc {0}. Max checkout is set to {1}".With(new object[]
				{
					cc.Name,
					cc.MaxCheckouts
				}), true, true);
				return true;
			}
			return false;
		}

		// Token: 0x04000338 RID: 824
		private JObject _snkrsProduct;

		// Token: 0x04000339 RID: 825
		private bool _isLeo;

		// Token: 0x0400033A RID: 826
		private CreditCardProfile _cc;

		// Token: 0x0400033B RID: 827
		private string _cardId;

		// Token: 0x0400033C RID: 828
		private string _pid;

		// Token: 0x0400033D RID: 829
		private string _skuId;

		// Token: 0x0400033E RID: 830
		private string _checkoutGuid;

		// Token: 0x0400033F RID: 831
		private float _total;

		// Token: 0x04000340 RID: 832
		private string _priceChecksum;

		// Token: 0x04000341 RID: 833
		private string _paymentGuid;

		// Token: 0x04000342 RID: 834
		private JObject _buyJson;

		// Token: 0x04000343 RID: 835
		private string _launchId;

		// Token: 0x04000344 RID: 836
		private string _drawGuid;

		// Token: 0x04000345 RID: 837
		private DateTime _launchDate = DateTime.MinValue;

		// Token: 0x04000346 RID: 838
		private HttpResponse _res;
	}
}
