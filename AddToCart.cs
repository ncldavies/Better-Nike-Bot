using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using CsQuery;
using CsQuery.ExtensionMethods;
using IMLokesh.Extensions;
using IMLokesh.HttpUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Better_Nike_Bot
{
	// Token: 0x02000020 RID: 32
	public class AddToCart
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00002C0A File Offset: 0x00000E0A
		public AddToCart(AtcItem item, MonitorLink monitorLink)
		{
			this.AtcItem = item;
			this.Req = item.Account.Request;
			this.MonitorLink = monitorLink;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00002C3C File Offset: 0x00000E3C
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00002C44 File Offset: 0x00000E44
		public AtcItem AtcItem { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00002C4D File Offset: 0x00000E4D
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00002C55 File Offset: 0x00000E55
		public HttpHelper Req { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00002C5E File Offset: 0x00000E5E
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00002C66 File Offset: 0x00000E66
		public MonitorLink MonitorLink { get; set; }

		// Token: 0x06000149 RID: 329 RVA: 0x00016264 File Offset: 0x00014464
		public void Atc()
		{
			bool flag = false;
			while (!Form1.ShouldStop && !flag && this.AtcItem.Details.SkuAndSize.IsNullOrWhiteSpace())
			{
				if (this.Skus.IsNotNull())
				{
					if (this.AtcItem.Size != "RANDOM")
					{
						if (this.Skus.All((MonitorLink.NikeSku s) => s.nikeSize != this.AtcItem.Size))
						{
							Logger.Log("Size {0} not found for {1}. Link: {2}".With(new object[]
							{
								this.AtcItem.Size,
								this.AtcItem.Details.Line1,
								this.AtcItem.Url
							}), true, true);
							return;
						}
					}
					if (this.AtcItem.Size != "RANDOM")
					{
						MonitorLink.NikeSku nikeSku = this.Skus.First((MonitorLink.NikeSku s) => s.nikeSize == this.AtcItem.Size);
						this.AtcItem.Details.SkuAndSize = "{0}:{1}".With(new object[]
						{
							nikeSku.id,
							nikeSku.nikeSize
						});
						this.AtcItem.Details.SkuId = nikeSku.id;
					}
					else
					{
						MonitorLink.NikeSku random = this.Skus.GetRandom<MonitorLink.NikeSku>();
						this.AtcItem.Details.SkuAndSize = "{0}:{1}".With(new object[]
						{
							random.id,
							random.nikeSize
						});
						this.AtcItem.Details.SkuId = random.id;
					}
				}
				else if (this.AtcItem.Account.IsWebSnkrs)
				{
					flag = this.GetProductDetailsForeignSnkrs();
				}
				else if (!Form1.UseOldMethod && !this.ForceOldMethod)
				{
					flag = this.GetProductDetailsNew();
				}
				else
				{
					flag = this.GetProductDetailsOld();
				}
			}
			flag = false;
			while (!Form1.ShouldStop && !flag)
			{
				this.AtcItem.Details.Ticks = DateTime.Now.Ticks.ToString();
				bool flag2 = !this.AtcItem.String_0.IsNullOrWhiteSpace() && !this.AtcItem.String_0.EqualsAny(new string[]
				{
					"-5",
					"-1"
				});
				flag = this.PerformAtcRequest(flag2);
				Thread.Sleep(flag2 ? NikeWaitIntervals.WaitAtc : NikeWaitIntervals.RetryAtc);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00016504 File Offset: 0x00014704
		private bool PerformAtcRequest(bool isWait)
		{
			string text;
			try
			{
				text = this.Req.GetRequest(isWait ? this.AtcItem.AtcWaitUrl : this.AtcItem.AtcUrl, this.AtcItem.Url, null, true, true);
			}
			catch (Exception ex)
			{
				Logger.Log("{0}: Error executing ATC!!! Retrying... {2} {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Url,
					ex.Message
				}), true, true);
				return false;
			}
			bool result;
			try
			{
				text = (text.Contains("nike_Cart_hanleJCartResponse(") ? text.Replace("nike_Cart_hanleJCartResponse(", "").TrimEnd(new char[]
				{
					';',
					')'
				}) : text);
				text = (text.Contains("nike_Cart_handleJCartResponse(") ? text.Replace("nike_Cart_handleJCartResponse(", "").TrimEnd(new char[]
				{
					';',
					')'
				}) : text);
				if (text.Contains("TIME OUT"))
				{
					Logger.Log("{0}: Error executing ATC!!! Retrying... {2} {1}".With(new object[]
					{
						this.AtcItem.Account.EmailAddress,
						this.AtcItem.Url,
						"TIMEOUT error, nike is probably facing issues"
					}), true, true);
					result = false;
				}
				else
				{
					JObject jobject = JObject.Parse(text);
					string text2 = jobject["status"].ToString();
					if (text2 == "failure")
					{
						JToken jtoken = jobject["exceptions"][0];
						string text3 = jtoken["errorcode"].ToString();
						string text4 = jtoken["message"].ToString();
						string s = "\r\nError: {0}\r\nEmail: {1} Size: {2} Product: {3}\r\nProduct Url: {4}\r\n";
						string[] array = new string[]
						{
							"",
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Size,
							this.AtcItem.Details.Line1,
							this.AtcItem.Url
						};
						if (text3.EqualsAny(new string[]
						{
							"InvalidItemInCart",
							"CantFindProduct"
						}))
						{
							array[0] = "{1} (Product not available)\r\nMessage: {0}".With(new object[]
							{
								text4,
								text3
							});
						}
						else
						{
							if (text3 == "ProductLimitExeeded")
							{
								array[0] = "{1} (Product added to cart!)\r\nMessage: {0}".With(new object[]
								{
									text4,
									text3
								});
								Logger.Log(s.With(array), true, true);
								this.SendNotification();
								return true;
							}
							array[0] = "{1}\r\nMessage: {0}".With(new object[]
							{
								text4,
								text3
							});
						}
						Logger.Log(s.With(array), true, true);
						result = false;
					}
					else if (text2 == "success")
					{
						try
						{
							SystemSounds.Exclamation.Play();
						}
						catch (Exception)
						{
						}
						string msg = "\r\nATC SUCCESS!!!\r\nEmail: {0} Size: {1} Product: {2}\r\nProduct Url: {3}\r\nOrderID: {4}\r\nCart Total: {5}\r\n".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Size,
							this.AtcItem.Details.Line1,
							this.AtcItem.Url,
							jobject["order"]["id"].ToString(),
							jobject["order"]["priceInfo"]["amount"].ToString()
						});
						Logger.Log(msg, true, true);
						this.SendNotification();
						result = true;
					}
					else if (text2 == "captcha")
					{
						result = false;
					}
					else if (text2.Contains("wait"))
					{
						try
						{
							this.AtcItem.String_0 = jobject["pil"].ToString();
							this.AtcItem.String_1 = jobject["ewt"].ToString();
							this.AtcItem.String_2 = jobject["psh"].ToString();
						}
						catch (Exception)
						{
						}
						string msg2 = "\r\nYou've a spot in line!\r\nEmail: {0} Size: {1} Product: {2}\r\nProduct Url: {3}\r\nPIL: {4}\r\nEWT: {5}\r\n".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Size,
							this.AtcItem.Details.Line1,
							this.AtcItem.Url,
							this.AtcItem.String_0,
							this.AtcItem.String_1
						});
						Logger.Log(msg2, true, true);
						result = false;
					}
					else
					{
						string msg3 = "\r\n{0}\r\nEmail: {1} Size: {2} Product: {3}\r\nProduct Url: {4}\r\n".With(new object[]
						{
							"Sorry product is sold out! {0}".With(new object[]
							{
								text2
							}),
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Size,
							this.AtcItem.Details.Line1,
							this.AtcItem.Url
						});
						Logger.Log(msg3, true, true);
						if (!Form1.DoNotRetrySoldOut)
						{
							result = false;
						}
						else
						{
							result = true;
						}
					}
				}
			}
			catch (Exception ex2)
			{
				Logger.Log("{0}: Error executing ATC!!! Unexpected response Retrying... {2} {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Url,
					ex2.Message
				}), true, true);
				result = false;
			}
			return result;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00016B34 File Offset: 0x00014D34
		private void SendNotification()
		{
			if (this.AtcItem.Details.Line1.IsNullOrWhiteSpace())
			{
				this.GetImportantProductDetails(0);
			}
			Form1.AddedProducts.Add(new AddedProduct(DateTime.Now, this.AtcItem.Details.Line1, this.AtcItem.Details.StyleCode, this.AtcItem.SnkrsBuyUrl.IsNullOrWhiteSpace() ? this.AtcItem.Details.Line2 : this.AtcItem.SnkrsBuyUrl, this.AtcItem.Account.EmailAddress, this.AtcItem.Details.Size, this.AtcItem.Account.Password, this.AtcItem.SnkrsBuyUrl, this.AtcItem.Account));
			Form1.AddedProductsForm.UpdateForm();
			Task.Factory.StartNew(delegate()
			{
				AddToCart.Callback(this.AtcItem.Details.Line1, this.AtcItem.Details.Size, this.AtcItem.Account.EmailAddress, this.AtcItem.SnkrsBuyUrl.IsNullOrWhiteSpace() ? this.AtcItem.Url : (this.AtcItem.Url + " " + this.AtcItem.SnkrsBuyUrl));
			});
			if (Form1.SendNotifications)
			{
				Task.Factory.StartNew(new Action(this.SendNotificationEmail));
			}
			if (this.AtcItem.Account.CheckoutInfo.IsNotNull() && this.AtcItem.Account.CheckoutInfo.CcCheckout)
			{
				bool flag = true;
				while (flag && !Form1.ShouldStop)
				{
					try
					{
						flag = this.CcCheckout();
					}
					catch (Exception ex)
					{
						Logger.Log("{0}: Checkout Error! {1}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							ex.Message
						}), true, true);
						if (ex.Message.ContainsAny(new string[]
						{
							"Checkout profile not found",
							"is currently not supported for checkout...",
							"Checkout Limit Reached for cc",
							"Your cart is empty"
						}))
						{
							flag = false;
						}
					}
					Thread.Sleep(NikeWaitIntervals.TimeoutRetryAtc);
				}
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00016D24 File Offset: 0x00014F24
		private bool CcCheckout()
		{
			if (Form1.SiteType != SiteType.NikeUS)
			{
				throw new Exception("Sorry, {0} is currently not supported for checkout...".With(new object[]
				{
					Form1.SiteType
				}));
			}
			CreditCardProfile creditCardProfile = Form1.CreditCardProfiles.FirstOrDefault((CreditCardProfile c) => c.Name == this.AtcItem.Account.CheckoutInfo.CcProfile);
			if (creditCardProfile.IsNull())
			{
				throw new Exception("Checkout profile not found. {0}".With(new object[]
				{
					this.AtcItem.Account.CheckoutInfo.CcProfile
				}));
			}
			Logger.Log("{0}: Starting cc checkout.".With(new object[]
			{
				this.AtcItem.Account.EmailAddress
			}), true, true);
			creditCardProfile.BillingState = creditCardProfile.BillingState.Trim();
			creditCardProfile.ShippingState = creditCardProfile.ShippingState.Trim();
			this.CheckCheckoutLimitReached(creditCardProfile);
			this.DeleteAllCards();
			if (this.AtcItem.Account.SnkrsExploit)
			{
				return this.SnkrsCheckout(creditCardProfile);
			}
			string text = "https://secure-store.nike.com/us/checkout/html/redirectToCheckout.jsp?country=US&l=cart";
			string text2 = this.Req.GetRequest(text, null, null, true, true);
			Dictionary<string, string> dictionary = text2.GetHiddenFormFieldsDictionary("form#cartForm");
			text = "https://secure-store.nike.com/us/checkout/html/cart.jsp?_DARGS=/us/checkout/common/includes/beginCheckout.jsp.cartForm";
			text2 = this.Req.PostRequest(text, dictionary, "https://secure-store.nike.com/us/checkout/html/cart.jsp?country=US&country=US&l=cart", "application/x-www-form-urlencoded", null, true);
			text = "https://secure-store.nike.com/us/checkout/html/shipping.jsp";
			text2 = this.Req.PostRequest(text, "https://secure-store.nike.com/us/checkout/html/billing.jsp", null, "application/x-www-form-urlencoded", null, true, "POST");
			dictionary = text2.GetHiddenFormFieldsDictionary("form#shippingForm");
			dictionary["/atg/commerce/order/purchase/ShippingGroupFormHandler.saveShipAddressToProfile"] = "false";
			dictionary["/atg/commerce/order/purchase/ShippingGroupFormHandler.shippingGroup.shippingAddress.firstName"] = (dictionary["fname"] = creditCardProfile.ShippingFirstName);
			dictionary["/atg/commerce/order/purchase/ShippingGroupFormHandler.shippingGroup.shippingAddress.lastName"] = (dictionary["lname"] = creditCardProfile.ShippingLastName);
			dictionary["/atg/commerce/order/purchase/ShippingGroupFormHandler.shippingGroup.shippingAddress.postalCode"] = (dictionary["postalCodeField"] = creditCardProfile.ShippingZipCode);
			dictionary["/atg/commerce/order/purchase/ShippingGroupFormHandler.shippingGroup.shippingAddress.address1"] = (dictionary["address1Field"] = creditCardProfile.ShippingAddress1);
			dictionary["/atg/commerce/order/purchase/ShippingGroupFormHandler.shippingGroup.shippingAddress.address2"] = (dictionary["address2Field"] = creditCardProfile.ShippingAddress2);
			dictionary["/atg/commerce/order/purchase/ShippingGroupFormHandler.shippingGroup.shippingAddress.city"] = (dictionary["city"] = creditCardProfile.ShippingCity);
			dictionary["/atg/commerce/order/purchase/ShippingGroupFormHandler.shippingGroup.shippingAddress.state"] = (dictionary["singleState"] = creditCardProfile.ShippingState);
			dictionary["/atg/commerce/order/purchase/ShippingGroupFormHandler.shippingGroup.shippingAddress.phoneNumber"] = creditCardProfile.ShippingPhone;
			dictionary["/atg/commerce/order/purchase/ShippingGroupFormHandler.shippingGroup.shippingAddress.email"] = this.AtcItem.Account.EmailAddress;
			dictionary["hasSavedAddresses"] = "false";
			dictionary["apoCity"] = "APO";
			dictionary["apoState"] = "AA";
			dictionary["shippingMethod_single"] = "Ground Service";
			dictionary["preferredDeliveryDay"] = "noPreference";
			dictionary["reqEmail"] = this.AtcItem.Account.EmailAddress;
			dictionary["reqPassword"] = this.AtcItem.Account.Password;
			dictionary["monthDob"] = "";
			dictionary["dayDob"] = "";
			dictionary["yearDob"] = "";
			dictionary["receiveNikeOffers"] = "on";
			try
			{
				dictionary.Remove("shipGroupNickname");
				dictionary.Remove("savedAddrFlag");
			}
			catch (Exception)
			{
			}
			text = "https://secure-store.nike.com/us/checkout/html/shipping.jsp?_DARGS=/us/checkout/common/shipping.jsp.shippingForm";
			text2 = this.Req.PostRequest(text, dictionary.BuildPostQuery().Replace("%20", "+"), "https://secure-store.nike.com/us/checkout/html/shipping.jsp", "application/x-www-form-urlencoded", null, true, "POST");
			this.CheckNikeErrors(text2);
			this.DeleteAllCards();
			string text3 = "";
			try
			{
				text3 = text2.CQSelect("#subTotalAmount").Text();
			}
			catch (Exception)
			{
			}
			text = "https://payment.nike.com/payment/checkout/addNewCard.htm?view=html&route=html&country=US&billingCountry=US&creditCardCountry=US&langLocale=en_US&action=getPaymentInfo&selectedPaymentType=creditCard&isNew=true";
			text2 = this.Req.GetRequest(text, "https://secure-store.nike.com/us/checkout/html/billing.jsp", null, true, true);
			dictionary = new Dictionary<string, string>();
			dictionary["selectPaymentType"] = "creditCard";
			dictionary["bankName"] = "";
			dictionary["paymentType"] = "creditCard";
			dictionary["creditCardCountry"] = "US";
			dictionary["cardTypeSelect"] = (dictionary["creditCardType"] = creditCardProfile.CreditCardType);
			dictionary["creditCardNumber"] = creditCardProfile.CreditCardNumber;
			dictionary["expirationMonth"] = creditCardProfile.CreditCardExpiryMonth;
			dictionary["expirationYear"] = creditCardProfile.CreditCardExpiryYear;
			dictionary["cvNumber"] = creditCardProfile.CreditCardCvv;
			dictionary["storeCreditCard"] = "true";
			dictionary["setDefaultCard"] = "true";
			dictionary["backFromReview"] = "false";
			dictionary["firstName"] = creditCardProfile.BillingFirstName;
			dictionary["lastName"] = creditCardProfile.BillingLastName;
			dictionary["address1"] = creditCardProfile.BillingAddress1;
			dictionary["address2"] = creditCardProfile.BillingAddress2;
			dictionary["city"] = creditCardProfile.BillingCity;
			dictionary["postalCode"] = creditCardProfile.BillingZipCode;
			dictionary["stateSelector"] = (dictionary["state"] = creditCardProfile.BillingState);
			dictionary["phoneNumber"] = creditCardProfile.BillingPhone;
			dictionary["faxNumber"] = "";
			dictionary["email"] = this.AtcItem.Account.EmailAddress;
			dictionary["secureProxyKey"] = text2.CQSelect("input[name=secureProxyKey]").Val();
			dictionary["action"] = "addNewCreditCard";
			dictionary["route"] = "html";
			dictionary["selectedPaymentType"] = "creditCard";
			dictionary["country"] = "US";
			dictionary["returnUrl"] = "https://secure-store.nike.com/us/checkout/html/review.jsp";
			dictionary["successUrl"] = "/checkout/billing.htm?action=&view=html&route=html&country=US&billingCountry=US&creditCardCountry=US&langLocale=en_US";
			dictionary["billingUrl"] = "/checkout/billing.htm?action=&view=html&route=html&country=US&billingCountry=US&creditCardCountry=US&langLocale=en_US";
			dictionary["reauthUrl"] = "";
			string referer = text;
			text = "https://payment.nike.com/payment/checkout/addNewCard.htm?country=US&view=html&billingCountry=US&langLocale=en_US";
			text2 = this.Req.PostRequest(text, dictionary.BuildPostQuery().Replace("%20", "+"), referer, "application/x-www-form-urlencoded", null, true, "POST");
			this.CheckNikeErrors(text2);
			dictionary = new Dictionary<string, string>();
			string value = text2.RegexMatch("'id': '(?<id>usercc[0-9]*)'").Groups["id"].Value;
			string value2 = text2.RegexMatch("'subscriptionId': '(?<subs>[0-9]*)'").Groups["subs"].Value;
			string value3 = text2.RegexMatch("'cardNumber': '(?<cardnumber>.*)'").Groups["cardnumber"].Value;
			string value4 = text2.CQSelect("input[name=secureProxyKey]").Val();
			dictionary["secureProxyKey"] = value4;
			dictionary["currentPage"] = "billing.jsp";
			dictionary["returnUrl"] = "https://secure-store.nike.com/us/checkout/html/review.jsp";
			dictionary["mobile"] = "";
			dictionary["view"] = "html";
			dictionary["route"] = "html";
			dictionary["langLocale"] = "en_US";
			dictionary["country"] = "US";
			dictionary["billingCountry"] = "US";
			dictionary["action"] = "setPaymentInfo";
			dictionary["successUrl"] = "https://secure-store.nike.com/us/checkout/html/review.jsp";
			dictionary["reauthUrl"] = "";
			dictionary["subscriptionId"] = value2;
			dictionary["creditCardId"] = value;
			dictionary["creditCardType"] = creditCardProfile.CreditCardType;
			dictionary["creditCardNumber"] = value3;
			dictionary["expirationYear"] = creditCardProfile.CreditCardExpiryYear;
			dictionary["expirationMonth"] = creditCardProfile.CreditCardExpiryMonth;
			dictionary["creditCardCountry"] = "US";
			dictionary["cvNumber"] = "";
			dictionary["selectPaymentType"] = "creditCard";
			dictionary["bankName"] = "";
			dictionary["paymentType"] = "creditCard";
			dictionary["cvv"] = creditCardProfile.CreditCardCvv;
			dictionary["backFromReview"] = "false";
			dictionary["firstName"] = creditCardProfile.BillingFirstName;
			dictionary["lastName"] = creditCardProfile.BillingLastName;
			dictionary["address1"] = creditCardProfile.BillingAddress1;
			dictionary["address2"] = creditCardProfile.BillingAddress2;
			dictionary["city"] = creditCardProfile.BillingCity;
			dictionary["postalCode"] = creditCardProfile.BillingZipCode;
			dictionary["stateSelector"] = (dictionary["state"] = creditCardProfile.BillingState);
			dictionary["phoneNumber"] = creditCardProfile.BillingPhone;
			dictionary["faxNumber"] = "";
			dictionary["email"] = this.AtcItem.Account.EmailAddress;
			text = "https://payment.nike.com/payment/checkout/billing.htm";
			text2 = this.Req.PostRequest(text, dictionary.BuildPostQuery().Replace("%20", "+"), "https://payment.nike.com/payment/checkout/billing.htm?country=US&billingCountry=US&langLocale=en_US&view=html&route=html&action=getPaymentInfo&fromReview=true&country=US", "application/x-www-form-urlencoded", null, true, "POST");
			this.CheckNikeErrors(text2);
			dictionary = text2.GetHiddenFormFieldsDictionary("form#reviewForm");
			text = "https://secure-store.nike.com/us/checkout/html/review.jsp?_DARGS=/us/checkout/common/includes/formReviewSubmit.jsp.reviewForm";
			if (Form1.ShouldStop)
			{
				return false;
			}
			lock (creditCardProfile.Lock)
			{
				this.CheckCheckoutLimitReached(creditCardProfile);
				text2 = this.Req.PostRequest(text, dictionary.BuildPostQuery().Replace("%20", "+"), "https://payment.nike.com/payment/checkout/review.jsp", "application/x-www-form-urlencoded", null, true, "POST");
				this.CheckNikeErrors(text2);
				Logger.Log("{0}: Checkout success!".With(new object[]
				{
					this.AtcItem.Account.EmailAddress
				}), true, true);
				creditCardProfile.CheckoutCount++;
			}
			try
			{
				Form1.AddedProducts.Add(new AddedProduct(DateTime.Now, "Checkout Success!", this.AtcItem.Details.StyleCode, "Cart Total: {0}".With(new object[]
				{
					text3
				}), this.AtcItem.Account.EmailAddress, this.AtcItem.Details.Size, this.AtcItem.Account.Password, this.AtcItem.SnkrsBuyUrl, this.AtcItem.Account));
				Form1.AddedProductsForm.UpdateForm();
				AddToCart.Callback("Checkout Success!", "Cart Total: {0}".With(new object[]
				{
					text3
				}), this.AtcItem.Account.EmailAddress, this.AtcItem.Url);
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00017868 File Offset: 0x00015A68
		private bool SnkrsCheckout(CreditCardProfile cc)
		{
			if (this.AtcItem.SnkrsBuyUrl == "Snkrs CHECKOUT SUCCESS!")
			{
				return false;
			}
			string text = "https://secure-store.nike.com/us/checkout/app/shipping.jsp?country=US";
			string text2 = this.Req.GetRequest(text, null, null, true, true);
			Dictionary<string, string> hiddenFormFieldsDictionary = text2.GetHiddenFormFieldsDictionary("#shippingForm");
			hiddenFormFieldsDictionary["address1Field"] = cc.ShippingAddress1;
			hiddenFormFieldsDictionary["address2Field"] = cc.ShippingAddress2;
			hiddenFormFieldsDictionary["cityField"] = cc.ShippingCity;
			hiddenFormFieldsDictionary["country"] = "US";
			hiddenFormFieldsDictionary["countryField"] = "US";
			hiddenFormFieldsDictionary["fname"] = cc.ShippingFirstName;
			hiddenFormFieldsDictionary["langLocale"] = "en_US";
			hiddenFormFieldsDictionary["lname"] = cc.ShippingLastName;
			hiddenFormFieldsDictionary["postalCodeField"] = cc.ShippingZipCode;
			hiddenFormFieldsDictionary["shipSubmit"] = "NEXT";
			hiddenFormFieldsDictionary["stateField"] = cc.ShippingState;
			string referer = text;
			text = "https://secure-store.nike.com/us/checkout/app/shipping.jsp?_DARGS=/us/checkout/app/shipping.jsp.shippingForm";
			text2 = this.Req.PostRequest(text, hiddenFormFieldsDictionary.BuildPostQuery().Replace("%20", "+"), referer, "application/x-www-form-urlencoded", null, true, "POST");
			this.CheckNikeErrors(text2);
			text = "https://payment.nike.com/payment/checkout/addNewCard.htm?country=US&creditCardCountry=US&langLocale=en_US&view=valiant&siteName=uscommerceapp&route=app&fromBilling=true";
			text2 = this.Req.GetRequest(text, null, null, true, true);
			referer = text;
			text = "https://payment.nike.com/payment/checkout/addNewCard.htm";
			hiddenFormFieldsDictionary["creditCardCountry"] = (hiddenFormFieldsDictionary["countrySelector"] = (hiddenFormFieldsDictionary["countrySelectorRadios"] = "US"));
			hiddenFormFieldsDictionary["cardTypeSelect"] = (hiddenFormFieldsDictionary["creditCardType"] = cc.CreditCardType);
			hiddenFormFieldsDictionary["creditCardNumber"] = cc.CreditCardNumberFormatted;
			hiddenFormFieldsDictionary["expirationMonth"] = cc.CreditCardExpiryMonth;
			hiddenFormFieldsDictionary["expirationYear"] = cc.CreditCardExpiryYear;
			hiddenFormFieldsDictionary["cvNumber"] = cc.CreditCardCvv;
			hiddenFormFieldsDictionary["storeCreditCard"] = "false";
			hiddenFormFieldsDictionary["backFromReview"] = "false";
			hiddenFormFieldsDictionary["firstName"] = cc.BillingFirstName;
			hiddenFormFieldsDictionary["lastName"] = cc.BillingLastName;
			hiddenFormFieldsDictionary["address1"] = cc.BillingAddress1;
			hiddenFormFieldsDictionary["address2"] = cc.BillingAddress2;
			hiddenFormFieldsDictionary["city"] = cc.BillingCity;
			hiddenFormFieldsDictionary["state"] = (hiddenFormFieldsDictionary["usState"] = cc.BillingState);
			hiddenFormFieldsDictionary["caProvince"] = "";
			hiddenFormFieldsDictionary["postalCode"] = cc.BillingZipCode;
			hiddenFormFieldsDictionary["login"] = (hiddenFormFieldsDictionary["email"] = this.AtcItem.Account.EmailAddress);
			hiddenFormFieldsDictionary["phoneNumber"] = cc.BillingPhone;
			hiddenFormFieldsDictionary["siteName"] = "uscommerceapp";
			hiddenFormFieldsDictionary["secureProxyKey"] = text2.CQSelect("input[name=secureProxyKey]").Val();
			hiddenFormFieldsDictionary["action"] = "addNewCreditCard";
			hiddenFormFieldsDictionary["route"] = "app";
			hiddenFormFieldsDictionary["view"] = "valiant";
			hiddenFormFieldsDictionary["langLocale"] = "en_US";
			hiddenFormFieldsDictionary["selectedPaymentType"] = "creditCard";
			hiddenFormFieldsDictionary["billingCountry"] = "US";
			hiddenFormFieldsDictionary["country"] = "US";
			hiddenFormFieldsDictionary["returnUrl"] = "https://secure-store.nike.com/us/checkout/app/review.jsp";
			hiddenFormFieldsDictionary["successUrl"] = "/checkout/billing.htm?fromAddNew=true&siteName=uscommerceapp&view=valiant&route=app&country=US&billingCountry=US&creditCardCountry=US&langLocale=en_US";
			hiddenFormFieldsDictionary["billingUrl"] = "/checkout/billing.htm?fromAddNew=true&siteName=uscommerceapp&view=valiant&route=app&country=US&billingCountry=US&creditCardCountry=US&langLocale=en_US";
			hiddenFormFieldsDictionary["reauthUrl"] = "";
			hiddenFormFieldsDictionary["reauthUrl"] = "";
			text2 = this.Req.PostRequest(text, hiddenFormFieldsDictionary.BuildPostQuery().Replace("%20", "+"), referer, "application/x-www-form-urlencoded", null, true, "POST");
			this.CheckNikeErrors(text2);
			string value = text2.RegexMatch("'id': '(?<id>usercc[0-9]*)'").Groups["id"].Value;
			string value2 = text2.RegexMatch("'subscriptionId': '(?<subs>[0-9]*)'").Groups["subs"].Value;
			string value3 = text2.RegexMatch("'cardNumber': '(?<cardnumber>.*)'").Groups["cardnumber"].Value;
			string text3 = "unknown";
			hiddenFormFieldsDictionary = text2.GetHiddenFormFieldsDictionary("#creditCardForm");
			hiddenFormFieldsDictionary["mobile"] = cc.ShippingPhone;
			hiddenFormFieldsDictionary["subscriptionId"] = value2;
			hiddenFormFieldsDictionary["creditCardId"] = value;
			hiddenFormFieldsDictionary["creditCardType"] = cc.CreditCardType;
			hiddenFormFieldsDictionary["creditCardNumber"] = value3;
			hiddenFormFieldsDictionary["expirationYear"] = cc.CreditCardExpiryYear;
			hiddenFormFieldsDictionary["expirationMonth"] = cc.CreditCardExpiryMonth;
			hiddenFormFieldsDictionary["cvNumber"] = cc.CreditCardCvv;
			hiddenFormFieldsDictionary["successUrl"] = "https://secure-store.nike.com/us/checkout/app/review.jsp";
			referer = text;
			text = "https://payment.nike.com/payment/checkout/billing.htm";
			text2 = this.Req.PostRequest(text, hiddenFormFieldsDictionary.BuildPostQuery().Replace("%20", "+"), referer, "application/x-www-form-urlencoded", null, true, "POST");
			this.CheckNikeErrors(text2);
			hiddenFormFieldsDictionary = text2.GetHiddenFormFieldsDictionary("form#reviewForm");
			text = "https://secure-store.nike.com/us/checkout/app/review.jsp?_DARGS=/us/checkout/app/includes/formReviewSubmit.jsp.reviewForm";
			if (Form1.ShouldStop)
			{
				return false;
			}
			lock (cc.Lock)
			{
				this.CheckCheckoutLimitReached(cc);
				text2 = this.Req.PostRequest(text, hiddenFormFieldsDictionary.BuildPostQuery().Replace("%20", "+"), "https://secure-store.nike.com/us/checkout/app/review.jsp", "application/x-www-form-urlencoded", null, true, "POST");
				this.CheckNikeErrors(text2);
				Logger.Log("{0}: Checkout success!".With(new object[]
				{
					this.AtcItem.Account.EmailAddress
				}), true, true);
				cc.CheckoutCount++;
			}
			try
			{
				Form1.AddedProducts.Add(new AddedProduct(DateTime.Now, "Checkout Success!", this.AtcItem.Details.StyleCode, "Cart Total: {0}".With(new object[]
				{
					text3
				}), this.AtcItem.Account.EmailAddress, this.AtcItem.Details.Size, this.AtcItem.Account.Password, this.AtcItem.SnkrsBuyUrl, this.AtcItem.Account));
				Form1.AddedProductsForm.UpdateForm();
				AddToCart.Callback("Checkout Success! SNKRS", "Cart Total: {0}".With(new object[]
				{
					text3
				}), this.AtcItem.Account.EmailAddress, this.AtcItem.Account.ProductStyleCodes.First<string>());
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00017F68 File Offset: 0x00016168
		private void DeleteAllCards()
		{
			if (this.AtcItem.Account.SnkrsExploit)
			{
				return;
			}
			string url = "https://payment.nike.com/payment/checkout/billing.htm";
			string text = this.Req.GetRequest(url, null, null, true, true);
			string[] array = (from m in text.RegexMatches("'id': '(?<id>usercc[0-9]*)'")
			select m.Groups["id"].Value).ToArray<string>();
			string[] array2 = (from m in text.RegexMatches("'subscriptionId': '(?<subs>[0-9]*)'")
			select m.Groups["subs"].Value).ToArray<string>();
			for (int i = 0; i < array.Length; i++)
			{
				string postData = "action=deleteSavedCreditCard&creditCardId={0}&subscriptionId={1}&billingCountry=US&country=US&creditCardCountry=US&view=ajax&route=".With(new object[]
				{
					array[i],
					array2[i]
				});
				text = this.Req.PostRequest("https://payment.nike.com/payment/profile/creditCards.htm", postData, "https://payment.nike.com/payment/profile/creditCards.htm", "application/x-www-form-urlencoded", null, true, "POST");
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00018064 File Offset: 0x00016264
		private void CheckNikeErrors(string html)
		{
			int num = html.IndexOf("nike.ocp.errors");
			if (num < 0 && (num = html.IndexOf("nike.ocp.serverErrors")) < 0)
			{
				return;
			}
			int num2 = html.Substring(num).IndexOf("];") + num;
			string text = html.Substring(num, num2 + 2 - num);
			Match match = text.RegexMatch("'(?<error>.*)'");
			if (match.Success)
			{
				throw new Exception(match.Groups["error"].Value.HtmlDecode());
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000180E8 File Offset: 0x000162E8
		private void CheckCheckoutLimitReached(CreditCardProfile cc)
		{
			if (cc.MaxCheckouts == 0)
			{
				return;
			}
			if (cc.CheckoutCount >= cc.MaxCheckouts)
			{
				throw new Exception("Checkout Limit Reached for cc {0}. Max checkout is set to {1}".With(new object[]
				{
					cc.Name,
					cc.MaxCheckouts
				}));
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0001813C File Offset: 0x0001633C
		private bool CheckCheckoutLimitReached1(CreditCardProfile cc)
		{
			if (Form1.ShouldStop)
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

		// Token: 0x06000152 RID: 338 RVA: 0x000181AC File Offset: 0x000163AC
		public static void Callback(string productName, string size, string emailAddress, string url)
		{
			try
			{
				string serialCode = Form1.SerialCode;
				DateTime utcNow = DateTime.UtcNow;
				HttpHelper.PostUrl("http://www.betternikebot.com/bnb/notifs.php", new Dictionary<string, string>
				{
					{
						"details",
						JsonConvert.SerializeObject(new
						{
							productKey = serialCode,
							date = utcNow,
							productName = productName,
							size = size,
							emailAddress = emailAddress,
							url = url
						})
					}
				}, null, 10, "", "application/x-www-form-urlencoded", null, true);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00018218 File Offset: 0x00016418
		private void SendNotificationEmail()
		{
			string value = NotificationSettings.HtmlTemplate.ReplacePlaceholders(this.AtcItem);
			string value2 = NotificationSettings.Subject.ReplacePlaceholders(this.AtcItem);
			string value3 = NotificationSettings.TextTemplate.ReplacePlaceholders(this.AtcItem);
			string text = this.AtcItem.Account.NotificationEmail.IsNullOrWhiteSpace() ? this.AtcItem.Account.EmailAddress : this.AtcItem.Account.NotificationEmail;
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"serial",
					Form1.SerialCode
				},
				{
					"from",
					NotificationSettings.SendFromEmail
				},
				{
					"from_name",
					NotificationSettings.SendFromName
				},
				{
					"to",
					text
				},
				{
					"text",
					value3
				},
				{
					"html",
					value
				},
				{
					"subject",
					value2
				}
			};
			try
			{
				string text2 = HttpHelper.PostUrl("http://www.betternikebot.com/bnb/send_notification.php", dictionary, null, 20, "", "application/x-www-form-urlencoded", null, true);
				Logger.Log((text2 == "success") ? "{0}: Email notification sent succesfully to {1}!".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					text
				}) : "{0}: Error sending notification to {2}. {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					text2,
					text
				}), true, true);
			}
			catch (Exception ex)
			{
				Logger.Log("{0}: Error sending notification to {2}. {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					ex.Message,
					text
				}), true, true);
			}
			if (this.AtcItem.Account.NotificationText.IsNullOrWhiteSpace())
			{
				return;
			}
			text = this.AtcItem.Account.NotificationText;
			dictionary["html"] = "null";
			dictionary["to"] = text;
			try
			{
				string text3 = HttpHelper.PostUrl("http://www.betternikebot.com/bnb/send_notification.php", dictionary, null, 20, "", "application/x-www-form-urlencoded", null, true);
				Logger.Log((text3 == "success") ? "{0}: Email notification sent succesfully to {1}!".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					text
				}) : "{0}: Error sending notification to {2}. {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					text3,
					text
				}), true, true);
			}
			catch (Exception ex2)
			{
				Logger.Log("{0}: Error sending notification to {2}. {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					ex2.Message,
					text
				}), true, true);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0001851C File Offset: 0x0001671C
		public void GetImportantProductDetails(int retryCount = 0)
		{
			if (retryCount >= 3)
			{
				return;
			}
			try
			{
				string request = this.Req.GetRequest(this.AtcItem.Url, null, null, true, true);
				if (request.Contains("add-to-cart-form"))
				{
					this.AtcItem.Details.StyleCode = request.CQSelect("span.exp-style-color").Text().Replace("Style: ", "").Replace("Style:", "").Trim();
					CQ cq = request.CQSelect("form.add-to-cart-form");
					this.AtcItem.Details.Line1 = cq.Select("[name='line1']").Val();
					this.AtcItem.Details.Line2 = cq.Select("[name='line2']").Val();
					this.AtcItem.Details.Price = cq.Select("[name='price']").Val();
					try
					{
						this.AtcItem.Details.ProductImage = request.CQSelect("meta[property=og:image]")[0].GetAttribute("content");
						goto IL_11A;
					}
					catch (Exception)
					{
						goto IL_11A;
					}
					goto IL_10F;
					IL_11A:
					return;
				}
				IL_10F:
				throw new Exception("");
			}
			catch (Exception)
			{
				Thread.Sleep(500);
				this.GetImportantProductDetails(++retryCount);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00018694 File Offset: 0x00016894
		public bool GetProductDetailsOld()
		{
			this._newMethod = false;
			Logger.Log("{0} (Size: {1}): Getting product details. {2}".With(new object[]
			{
				this.AtcItem.Account.EmailAddress,
				this.AtcItem.Size,
				this.AtcItem.Url
			}), true, true);
			bool result;
			try
			{
				string text = this.OldHtml ?? this.Req.GetRequest(this.AtcItem.Url, null, null, true, true);
				this.OldHtml = null;
				if (!text.Contains("add-to-cart-form"))
				{
					throw new Exception("Product not live yet".With(new object[]
					{
						this.AtcItem.Size
					}));
				}
				this.AtcItem.Details.StyleCode = text.CQSelect("span.exp-style-color").Text().Replace("Style: ", "").Replace("Style:", "").Replace("款式: ", "").Replace("款式:", "").Trim();
				CQ cq = text.CQSelect("form.add-to-cart-form");
				this.AtcItem.Details.ProductId = cq.Select("[name='productId']").Val();
				this.AtcItem.Details.Price = cq.Select("[name='price']").Val();
				this.AtcItem.Details.Line1 = cq.Select("[name='line1']").Val();
				this.AtcItem.Details.Line2 = cq.Select("[name='line2']").Val();
				this.AtcItem.Details.CatalogId = cq.Select("[name='catalogId']").Val();
				CQ cq2 = cq.Select("select.exp-pdp-size-dropdown option[name=skuId]");
				if (!cq2.IsAny<IDomObject>())
				{
					throw new Exception("Sizes are not live!!!");
				}
				List<IDomObject> source = cq2.ToList<IDomObject>();
				try
				{
					source = (from x in cq2
					orderby x.InnerText.RegexReplace("[^0-9]", "").ParseToInt()
					select x).ToList<IDomObject>();
				}
				catch (Exception)
				{
				}
				try
				{
					ProductDetails details = this.AtcItem.Details;
					string value;
					if (!(this.AtcItem.Size == "RANDOM"))
					{
						value = source.First((IDomObject el) => el.GetAttribute("value", "").Contains(":{0}".With(new object[]
						{
							this.AtcItem.Size
						}))).Value;
					}
					else
					{
						value = source.GetRandom<IDomObject>().Value;
					}
					details.SkuAndSize = value;
				}
				catch (Exception)
				{
					throw new Exception("Size {0} is not available".With(new object[]
					{
						this.AtcItem.Size
					}));
				}
				string[] array = this.AtcItem.Details.SkuAndSize.Split(new char[]
				{
					':'
				});
				try
				{
					this.AtcItem.Details.ProductImage = text.CQSelect("meta[property=og:image]")[0].GetAttribute("content");
				}
				catch (Exception)
				{
				}
				if (this.AtcItem.Details.SkuAndSize.IsNullOrWhiteSpace() || array.Length != 2)
				{
					throw new Exception();
				}
				this.AtcItem.Details.SkuId = array[0];
				this.AtcItem.Details.Size = (this.AtcItem.Size = array[1]);
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
				if (this.MonitorLink != null)
				{
					Thread.Sleep(this.EnableTimer() ? NikeWaitIntervals.RetryGetProductDetailsTimer : NikeWaitIntervals.RetryGetProductDetails);
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00018AD0 File Offset: 0x00016CD0
		public bool GetProductDetailsNew()
		{
			this._newMethod = true;
			bool result;
			try
			{
				string text = NikeUrls.NikeLoadSkus.With(new object[]
				{
					this.AtcItem.Details.ProductId
				});
				Logger.Log("{0} (Size: {2}): Getting product details. {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Url,
					this.AtcItem.Size
				}), true, true);
				string text2 = null;
				try
				{
					text2 = Form1.ResponseDictionary[text];
				}
				catch (Exception)
				{
				}
				string text3;
				if ((text3 = this.OldHtml) == null)
				{
					text3 = (text2 ?? this.Req.GetRequest(text, null, null, true, true));
				}
				text2 = text3;
				this.OldHtml = null;
				if (text2.Contains("xml version="))
				{
					text2 = text2.Trim(new char[]
					{
						'}',
						'{'
					});
					text2 = ((!text2.Contains("<response>") || text2.Contains("</response>")) ? text2 : (text2 + "</response>"));
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(text2);
					text2 = JsonConvert.SerializeXmlNode(xmlDocument["response"]);
					text2 = JObject.Parse(text2)["response"].ToString();
				}
				JObject jobject = JObject.Parse(text2);
				if (jobject["status"].ToString() == "success")
				{
					JArray jarray = jobject["product"].Value<JArray>("childSKUs");
					if (!jarray.IsAny<JToken>())
					{
						throw new Exception("Size details not available.");
					}
					bool flag = false;
					if (this.AtcItem.Size == "RANDOM")
					{
						flag = true;
						jarray = JArray.FromObject(jarray.Randomize<JToken>());
					}
					foreach (JToken jtoken in jarray)
					{
						jtoken["displaySize"] = jtoken["sizeDescription"].ToString().ConvertToNativeSize();
						if (jtoken["displaySize"].ToString().StartsWith(this.AtcItem.Size) || flag)
						{
							this.AtcItem.Details.Size = (this.AtcItem.Size = jtoken["displaySize"].ToString());
							this.AtcItem.Details.SkuId = jtoken["id"].ToString();
							this.AtcItem.Details.SkuAndSize = "{0}:{1}".With(new object[]
							{
								this.AtcItem.Details.SkuId,
								this.AtcItem.Details.Size
							});
							return true;
						}
					}
					if (this.AtcItem.Details.SkuAndSize.IsNullOrWhiteSpace())
					{
						throw new Exception("Size {0} is not available.".With(new object[]
						{
							this.AtcItem.Size
						}));
					}
					result = false;
				}
				else
				{
					JToken jtoken2 = jobject["exceptions"];
					if (jtoken2.IsAny<JToken>())
					{
						jtoken2 = jtoken2["error"][0];
						throw new Exception("Error getting size.. Code: {0} Message: {1}".With(new object[]
						{
							jtoken2["errorcode"],
							jtoken2["message"]
						}));
					}
					throw new Exception("Catalog service return not success.");
				}
			}
			catch (Exception ex)
			{
				Logger.Log("{0}: Error getting product details!!! Retrying... {2} {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Url,
					ex.Message
				}), true, true);
				if (this.MonitorLink != null)
				{
					Thread.Sleep(this.EnableTimer() ? NikeWaitIntervals.RetryGetProductDetailsTimer : NikeWaitIntervals.RetryGetProductDetails);
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00018F38 File Offset: 0x00017138
		public bool GetProductDetailsWebSnkrs()
		{
			bool result;
			try
			{
				string url = "https://api.nike.com/commerce/productfeed/products/snkrs/{0}/thread?country=US&locale=en_US&withCards=true".With(new object[]
				{
					this.AtcItem.Details.StyleCode,
					NikeUrls.NikeCountryCode,
					NikeUrls.NikeLangLocale
				});
				Logger.Log("{0} (Size: {2}): Getting product details. {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Url,
					this.AtcItem.Size
				}), true, true);
				string json = null;
				try
				{
					json = this.Req.GetRequest(url, null, null, true, true);
				}
				catch (WebException ex)
				{
					string text = null;
					try
					{
						HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
						using (httpWebResponse)
						{
							string text2 = null;
							Stream responseStream = httpWebResponse.GetResponseStream();
							if (responseStream != null)
							{
								using (StreamReader streamReader = new StreamReader(responseStream, new UTF8Encoding()))
								{
									text2 = streamReader.ReadToEnd();
								}
							}
							text = (from ss in text2.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
							select ss.Groups["msg"].Value).JoinToString(" ");
						}
					}
					catch (Exception)
					{
					}
					throw new Exception(text ?? ex.Message);
				}
				JObject jobject = JObject.Parse(json);
				try
				{
					this.AtcItem.Details.ProductId = jobject["id"].Value<string>();
					this.AtcItem.Details.StyleCode = jobject["product"]["style"].Value<string>() + "-" + jobject["product"]["colorCode"].Value<string>();
					this.AtcItem.Details.Line1 = jobject["product"]["title"].Value<string>();
					this.AtcItem.Details.Line2 = jobject["product"]["subtitle"].Value<string>();
					this.AtcItem.Details.ProductImage = jobject["product"]["imageUrl"].Value<string>();
					this.AtcItem.Details.Price = jobject["product"]["price"]["formattedFullRetailPrice"].Value<string>();
					this._waitLineEnabled = jobject["product"]["waitlineEnabled"].Value<bool>();
				}
				catch (Exception)
				{
					throw new Exception("Unable to get product details...");
				}
				this._isRandom = false;
				try
				{
					this._isRandom = (jobject["product"]["selectionEngine"].Value<string>() == "RANDOM");
				}
				catch (Exception)
				{
				}
				DateTime d = DateTime.Parse(jobject["product"]["startSellDate"].Value<string>() + "Z", CultureInfo.InvariantCulture);
				if (this._isRandom)
				{
					d = d.AddMinutes(2.0);
				}
				else
				{
					d = d.Subtract(TimeSpan.FromSeconds(8.0));
				}
				DateTime d2 = DateTime.Parse(this.Req.LastHeaders["Date"], CultureInfo.InvariantCulture);
				TimeSpan span = d - d2;
				if (span.TotalSeconds > 0.0)
				{
					double num = 0.0;
					double totalSeconds = span.TotalSeconds;
					Logger.Log("{0} (Size: {2}): Waiting for {4} {1} to go live. Resuming in {3}".With(new object[]
					{
						this.AtcItem.Account.EmailAddress,
						this.AtcItem.Details.Line1,
						this.AtcItem.Size,
						AddToCart.ToReadableString(span),
						this._isRandom ? "draw" : "product"
					}), true, true);
					while (num < totalSeconds)
					{
						Thread.Sleep(5000);
						num += 5.0;
						if (Form1.ShouldStop)
						{
							return true;
						}
					}
				}
				result = true;
			}
			catch (Exception ex2)
			{
				Logger.Log("{0}: Error getting product details... {2} {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Url,
					ex2.Message
				}), true, true);
				Thread.Sleep(3000);
				result = false;
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0001948C File Offset: 0x0001768C
		public bool GetProductDetailsForeignSnkrs()
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
				Logger.Log("{0} (Size: {2}): Getting product details. {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Url,
					this.AtcItem.Size
				}), true, true);
				string json = null;
				try
				{
					json = this.Req.GetRequest(url, null, null, true, true);
				}
				catch (WebException ex)
				{
					string text = null;
					try
					{
						HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
						using (httpWebResponse)
						{
							string text2 = null;
							Stream responseStream = httpWebResponse.GetResponseStream();
							if (responseStream != null)
							{
								using (StreamReader streamReader = new StreamReader(responseStream, new UTF8Encoding()))
								{
									text2 = streamReader.ReadToEnd();
								}
							}
							text = (from ss in text2.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
							select ss.Groups["msg"].Value).JoinToString(" ");
						}
					}
					catch (Exception)
					{
					}
					throw new Exception(text ?? ex.Message);
				}
				JObject jobject = JObject.Parse(json);
				try
				{
					this.AtcItem.Details.ProductId = jobject["id"].Value<string>();
					this.AtcItem.Details.StyleCode = jobject["product"]["style"].Value<string>() + "-" + jobject["product"]["colorCode"].Value<string>();
					this.AtcItem.Details.Line1 = jobject["product"]["title"].Value<string>();
					this.AtcItem.Details.Line2 = jobject["product"]["subtitle"].Value<string>();
					this.AtcItem.Details.ProductImage = jobject["product"]["imageUrl"].Value<string>();
					this.AtcItem.Details.Price = jobject["product"]["price"]["formattedFullRetailPrice"].Value<string>();
					this._waitLineEnabled = jobject["product"]["waitlineEnabled"].Value<bool>();
					this._snkrsProduct = JObject.Parse(jobject["product"].ToString());
				}
				catch (Exception)
				{
					Thread.Sleep(3000);
					throw new Exception("Unable to get product details...");
				}
				this._isRandom = false;
				try
				{
					this._isRandom = (jobject["product"]["selectionEngine"].Value<string>() == "LEO");
				}
				catch (Exception)
				{
				}
				DateTime d = DateTime.Parse(jobject["product"]["startSellDate"].Value<string>() + "Z", CultureInfo.InvariantCulture);
				if (this._isRandom)
				{
					d = d.AddMinutes(2.0);
				}
				else
				{
					d = d.Subtract(TimeSpan.FromSeconds(8.0));
				}
				DateTime d2 = DateTime.Parse(this.Req.LastHeaders["Date"], CultureInfo.InvariantCulture);
				TimeSpan span = d - d2;
				if (span.TotalSeconds > 0.0)
				{
					double num = 0.0;
					double totalSeconds = span.TotalSeconds;
					Logger.Log("{0} (Size: {2}): Waiting for {4} {1} to go live. Resuming in {3}".With(new object[]
					{
						this.AtcItem.Account.EmailAddress,
						this.AtcItem.Details.Line1,
						this.AtcItem.Size,
						AddToCart.ToReadableString(span),
						this._isRandom ? "draw" : "product"
					}), true, true);
					while (num < totalSeconds)
					{
						Thread.Sleep(5000);
						num += 5.0;
						if (Form1.ShouldStop)
						{
							return true;
						}
					}
				}
				result = true;
			}
			catch (Exception ex2)
			{
				Logger.Log("{0}: Error getting product details!!! Retrying... {2} {1}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Url,
					ex2.Message
				}), true, true);
				Thread.Sleep(3000);
				result = false;
			}
			return result;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00019A04 File Offset: 0x00017C04
		public static string ToReadableString(TimeSpan span)
		{
			string text = string.Format("{0}{1}{2}{3}", new object[]
			{
				(span.Duration().Days > 0) ? string.Format("{0:0} day{1}, ", span.Days, (span.Days == 1) ? string.Empty : "s") : string.Empty,
				(span.Duration().Hours > 0) ? string.Format("{0:0} hour{1}, ", span.Hours, (span.Hours == 1) ? string.Empty : "s") : string.Empty,
				(span.Duration().Minutes > 0) ? string.Format("{0:0} minute{1}, ", span.Minutes, (span.Minutes == 1) ? string.Empty : "s") : string.Empty,
				(span.Duration().Seconds > 0) ? string.Format("{0:0} second{1}", span.Seconds, (span.Seconds == 1) ? string.Empty : "s") : string.Empty
			});
			if (text.EndsWith(", "))
			{
				text = text.Substring(0, text.Length - 2);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "0 seconds";
			}
			return text;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00019B74 File Offset: 0x00017D74
		private bool EnableTimer()
		{
			return DateTime.Now > this.MonitorLink.StartTime - TimeSpan.FromSeconds(30.0) && DateTime.Now < this.MonitorLink.StartTime + TimeSpan.FromMinutes(4.0);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00019BD8 File Offset: 0x00017DD8
		public void AUAtc(string url = "")
		{
			if (Form1.ShouldStop || this.Simulate)
			{
				return;
			}
			if (!this.AtcItem.Details.SkuId.IsNullOrWhiteSpace() && !url.IsNullOrWhiteSpace())
			{
				try
				{
					string str = "?product={0}&qty=1&related_product=&super_attribute%5B{2}%5D={1}&block%5B%5D=options&awacp=1&no_cache=1".With(new object[]
					{
						this.AtcItem.Details.ProductId,
						this.AtcItem.Details.SkuId,
						this._auId
					});
					string request = this.Req.GetRequest(url + str, this.AtcItem.Url, new string[]
					{
						"X-Requested-With: XMLHttpRequest",
						"X-Prototype-Version: 1.7"
					}, true, true);
					JObject jobject = JObject.Parse(request);
					if (!jobject["status"].Value<bool>())
					{
						throw new Exception(jobject["message"].Value<string>());
					}
					string msg = "\r\nATC SUCCESS!!!\r\nEmail: {0} Size: {1} Product: {2}\r\nProduct Url: {3}\r\n".With(new object[]
					{
						this.AtcItem.Account.EmailAddress,
						this.AtcItem.Size,
						this.AtcItem.Details.Line1,
						this.AtcItem.Url
					});
					Logger.Log(msg, true, true);
					this.SendNotification();
				}
				catch (Exception ex)
				{
					string msg2 = "\r\nError: {0}\r\nEmail: {1} Size: {2} Product: {3}\r\nProduct Url: {4}\r\n".With(new object[]
					{
						ex.Message,
						this.AtcItem.Account.EmailAddress,
						this.AtcItem.Size,
						this.AtcItem.Details.Line1,
						this.AtcItem.Url
					});
					Logger.Log(msg2, true, true);
					Thread.Sleep(NikeWaitIntervals.RetryAtc);
					Task.Factory.StartNew(delegate()
					{
						this.AUAtc("");
					});
				}
				return;
			}
			Task.Factory.StartNew(new Action(this.GetAUDetails));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00019E14 File Offset: 0x00018014
		public void GetAUDetails()
		{
			if (!Form1.ShouldStop && !this.Simulate)
			{
				try
				{
					Logger.Log("{0} (Size: {1}): Getting product details. {2}".With(new object[]
					{
						this.AtcItem.Account.EmailAddress,
						this.AtcItem.Size,
						this.AtcItem.Url
					}), true, true);
					string text = this.OldHtml ?? this.Req.GetRequest(this.AtcItem.Url, null, null, true, true);
					this.OldHtml = null;
					string url = text.CQSelect("form#product_addtocart_form").Attr("action");
					this.AtcItem.Details.ProductId = text.CQSelect("input[name='product']").Val();
					int num = text.IndexOf("Product.Config(") + 15;
					int length = text.IndexOf(";", num + 1) - 1 - num;
					JObject jobject = JObject.Parse(text.Substring(num, length));
					this._auId = jobject["attributes"].First<JToken>().First<JToken>()["id"].Value<string>();
					if (this.AtcItem.Size == "RANDOM")
					{
						JToken random = ((JArray)jobject["attributes"][this._auId]["options"]).GetRandom<JToken>();
						this.AtcItem.Details.SkuId = random["id"].Value<string>();
						this.AtcItem.Details.Size = (this.AtcItem.Size = random["label"].Value<string>());
					}
					else
					{
						foreach (JToken jtoken in ((JArray)jobject["attributes"][this._auId]["options"]))
						{
							if (jtoken["label"].Value<string>().Contains(this.AtcItem.Size))
							{
								this.AtcItem.Details.SkuId = jtoken["id"].Value<string>();
								this.AtcItem.Details.Size = (this.AtcItem.Size = jtoken["label"].Value<string>());
								break;
							}
						}
					}
					try
					{
						this.AtcItem.Details.Line1 = text.CQSelect("h1.productname").Text().Trim();
					}
					catch (Exception)
					{
					}
					Task.Factory.StartNew(delegate()
					{
						this.AUAtc(url);
					});
				}
				catch (Exception ex)
				{
					string msg = "\r\nError: {0}\r\nEmail: {1} Size: {2} Product: {3}\r\nProduct Url: {4}\r\n".With(new object[]
					{
						ex.Message,
						this.AtcItem.Account.EmailAddress,
						this.AtcItem.Size,
						this.AtcItem.Details.Line1,
						this.AtcItem.Url
					});
					Logger.Log(msg, true, true);
					Thread.Sleep(NikeWaitIntervals.RetryAtc);
					Task.Factory.StartNew(delegate()
					{
						this.AUAtc("");
					});
				}
				return;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0001A1EC File Offset: 0x000183EC
		public static string RandomString(int length)
		{
			Random random = new Random();
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
			select s[random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0001A230 File Offset: 0x00018430
		public bool ProcessSnkrs()
		{
			bool result;
			try
			{
				this.Req.ClearCookies();
				string text;
				JObject jobject;
				if (this.AtcItem.Details.SnkrsUrl.IsNullOrWhiteSpace())
				{
					this.Req.ResetHeaders(false);
					this.Req.UserAgent = "onenikecommerce-inhouse/8988 (iPhone; iOS 8.1.3; Scale/2.00)";
					this.Req.Accept = "*/*";
					this.Req.KeepAlive = true;
					Dictionary<string, string> objectToSerialize = new Dictionary<string, string>
					{
						{
							"username",
							this.AtcItem.Account.EmailAddress
						},
						{
							"password",
							this.AtcItem.Account.Password
						},
						{
							"client_id",
							"0239a00434d37e14e8644b9821d1322c"
						},
						{
							"ux_id",
							"com.nike.valiant.ios"
						},
						{
							"grant_type",
							"password"
						}
					};
					text = this.Req.PostRequest("https://unite.nikecloud.com/login?locale={0}&backendEnvironment=prd".With(new object[]
					{
						NikeUrls.NikeLangLocale
					}), objectToSerialize.ToJSON(), "https://s3.nikecdn.com/unite/mobile.html?iOSSDKVersion=1.0.0&clientId=0239a00434d37e14e8644b9821d1322c&uxId=com.nike.valiant.ios&view=login&locale={0}&backendEnvironment=prd".With(new object[]
					{
						NikeUrls.NikeLangLocale
					}), "application/x-www-form-urlencoded", new string[]
					{
						"Origin: https://s3.nikecdn.com",
						"X-Nike-Who: " + AddToCart.RandomString(40)
					}, true, "POST");
					jobject = (JObject)JsonConvert.DeserializeObject(text);
					this.AtcItem.Details.SnkrsUrl = "https://buy.nike.com/commerce/{3}/buy?access_token={0}&shopper_token={2}&int_productId={1}".With(new object[]
					{
						jobject["access_token"].ToString(),
						this.AtcItem.Details.ProductId,
						AddToCart.RandomString(20),
						NikeUrls.NikeCountryServiceCode
					});
				}
				string postData = "action=purchaseItem&catalog=1&country={4}&deviceId=&login={0}&password={1}&productId={2}&qty=1&rt=json&siteId=1000&skuId={3}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress.UrlEncode(false),
					this.AtcItem.Account.Password.UrlEncode(false),
					this.AtcItem.Details.ProductId,
					this.AtcItem.Details.SkuId,
					NikeUrls.NikeCountryCode
				});
				if (!this.AtcItem.String_0.IsNullOrWhiteSpace())
				{
					postData = "action=purchaseItem&catalog=1&country={6}&deviceId=&login={0}&password={1}&pil={2}&productId={3}&psh={4}&qty=1&rt=json&siteId=1000&skuId={5}".With(new object[]
					{
						this.AtcItem.Account.EmailAddress.UrlEncode(false),
						this.AtcItem.Account.Password.UrlEncode(false),
						this.AtcItem.String_0,
						this.AtcItem.Details.ProductId,
						this.AtcItem.String_2,
						this.AtcItem.Details.SkuId,
						NikeUrls.NikeCountryCode
					});
				}
				string[] additionalHeaders = new string[]
				{
					"X-Nike-Product-Id: " + this.AtcItem.Details.ProductId,
					"X-Nike-Birthmark: " + Guid.NewGuid().ToString().ToUpper(),
					"X-Nike-Who: " + AddToCart.RandomString(40)
				};
				this.Req.ResetHeaders(false);
				this.Req.UserAgent = "onenikecommerce-inhouse/8988 (iPhone; iOS 8.1.3; Scale/2.00)";
				this.Req.Accept = "*/*";
				this.Req.KeepAlive = true;
				text = this.Req.PostRequest(this.AtcItem.Details.SnkrsUrl, postData, null, "application/x-www-form-urlencoded", additionalHeaders, true, "POST");
				jobject = JObject.Parse(text);
				if (jobject["status"].ToString() == "redirect")
				{
					Logger.Log("{0}: Snkrs buy now! Link: {1}".With(new object[]
					{
						this.AtcItem.Account.EmailAddress,
						jobject["redirectURL"].ToString()
					}), true, true);
					this.AtcItem.SnkrsBuyUrl = jobject["redirectURL"].ToString();
					this.SendNotification();
				}
				else
				{
					if (jobject["status"].ToString() == "failure")
					{
						throw new Exception(jobject["exceptions"][0]["message"].ToString());
					}
					if (jobject["status"].ToString().Contains("wait"))
					{
						this.AtcItem.String_0 = jobject["pil"].ToString();
						this.AtcItem.String_2 = jobject["psh"].ToString();
						this.AtcItem.String_1 = jobject["ewt"].ToString();
						Logger.Log("\r\nYou've a spot in SNKRS line!\r\nEmail: {0} Size: {1} Product: {2}\r\nPIL: {3}\r\nEWT: {4}\r\n".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Size,
							this.AtcItem.Url,
							this.AtcItem.String_0,
							this.AtcItem.String_1
						}), true, true);
						Thread.Sleep(NikeWaitIntervals.WaitAtc);
						return false;
					}
					if (jobject["status"].ToString() == "success")
					{
						Logger.Log("{0}: Snkrs CHECKOUT SUCCESS! ".With(new object[]
						{
							this.AtcItem.Account.EmailAddress
						}), true, true);
						this.AtcItem.SnkrsBuyUrl = "Snkrs CHECKOUT SUCCESS!";
						this.SendNotification();
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				Logger.Log("{0}: Error in snkrs - {1} ".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					ex.Message
				}), true, true);
				Thread.Sleep(NikeWaitIntervals.RetryAtcFailure);
				this.AtcItem.String_0 = "";
				result = false;
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0001A888 File Offset: 0x00018A88
		private void CheckNikeSnkrsError(string html)
		{
			string text = "";
			if (html.IsNullOrWhiteSpace())
			{
				return;
			}
			try
			{
				JObject jobject = JObject.Parse(html);
				try
				{
					text = jobject["error"]["message"].Value<string>() + "... ";
				}
				catch (Exception)
				{
				}
				try
				{
					text += jobject["error"]["errors"][0]["message"].Value<string>();
				}
				catch (Exception)
				{
				}
			}
			catch (Exception)
			{
				text = "Received invalid json response.";
			}
			if (!text.IsNullOrWhiteSpace())
			{
				throw new Exception(text);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0001A954 File Offset: 0x00018B54
		public bool ProcessForeignSnkrs()
		{
			bool result;
			try
			{
				string url = "";
				CreditCardProfile creditCardProfile = Form1.CreditCardProfiles.FirstOrDefault((CreditCardProfile c) => c.Name == this.AtcItem.Account.CheckoutInfo.CcProfile);
				if (creditCardProfile.IsNull())
				{
					Thread.Sleep(1000);
					throw new Exception("Checkout profile not found. You must have checkout profile for websnkrs. {0}".With(new object[]
					{
						this.AtcItem.Account.CheckoutInfo.CcProfile
					}));
				}
				url = "https://unite.nikecloud.com/login?appVersion=290&experienceVersion=252&uxid=com.nike.commerce.snkrs.web&locale={0}&backendEnvironment=identity&browser=Google%20Inc.&os=undefined&mobile=false&native=false&visit=1&visitor={1}".With(new object[]
				{
					NikeUrls.NikeLangLocale,
					Guid.NewGuid().ToString()
				});
				this.Req.ResetHeaders(true);
				this.Req.Headers = new List<string>();
				this.Req.Accept = "*/*";
				this.Req.KeepAlive = true;
				Dictionary<string, string> objectToSerialize = new Dictionary<string, string>
				{
					{
						"username",
						this.AtcItem.Account.EmailAddress
					},
					{
						"password",
						this.AtcItem.Account.Password
					},
					{
						"client_id",
						"PbCREuPr3iaFANEDjtiEzXooFl7mXGQ7"
					},
					{
						"keepMeLoggedIn",
						"true"
					},
					{
						"ux_id",
						"com.nike.commerce.snkrs.web"
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
				bool flag = true;
				JObject jobject;
				while (flag)
				{
					int millisecondsTimeout = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					try
					{
						Logger.Log("{0} ({1}): Generating tokens...".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1
						}), true, true);
						string text = this.Req.PostRequest(url, objectToSerialize.ToJSON(), null, "application/json", null, true, "POST");
						jobject = JObject.Parse(text);
						this.AtcItem.Account.WebSnkrsToken = jobject["access_token"].Value<string>();
						this.Req.Headers.Add("Authorization: Bearer {0}".With(new object[]
						{
							this.AtcItem.Account.WebSnkrsToken
						}));
						flag = false;
						goto IL_3B0;
					}
					catch (WebException ex)
					{
						string text2 = null;
						try
						{
							HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
							using (httpWebResponse)
							{
								string text3 = null;
								Stream responseStream = httpWebResponse.GetResponseStream();
								if (responseStream != null)
								{
									using (StreamReader streamReader = new StreamReader(responseStream, new UTF8Encoding()))
									{
										text3 = streamReader.ReadToEnd();
									}
								}
								text2 = (from ss in text3.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text2 ?? ex.Message
						}), true, true);
						if (ex.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout = 4000;
						}
						else if (ex.Message.Contains("time"))
						{
							millisecondsTimeout = 500;
						}
						goto IL_3B0;
					}
					IL_39D:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout);
						continue;
					}
					continue;
					IL_3B0:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_39D;
				}
				string text4 = Guid.NewGuid().ToString();
				url = "https://paymentcc.nike.com/creditcardsubmit/{0}/store".With(new object[]
				{
					text4
				});
				objectToSerialize = new Dictionary<string, string>
				{
					{
						"expirationMonth",
						creditCardProfile.CreditCardExpiryMonth.PadLeft(2, '0')
					},
					{
						"accountNumber",
						creditCardProfile.CreditCardNumber
					},
					{
						"creditCardInfoId",
						text4
					},
					{
						"cvNumber",
						creditCardProfile.CreditCardCvv
					},
					{
						"cardType",
						creditCardProfile.CreditCardType.ToUpper()
					},
					{
						"expirationYear",
						creditCardProfile.CreditCardExpiryYear
					}
				};
				flag = true;
				while (flag)
				{
					int millisecondsTimeout2 = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
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
						string text = this.Req.PostRequest(url, objectToSerialize.ToJSON(), "https://paymentcc.nike.com/services?id={0}".With(new object[]
						{
							text4
						}), "application/json", new string[]
						{
							"Origin: https://www.nike.com"
						}, true, "POST");
						this.CheckNikeSnkrsError(text);
						flag = false;
						goto IL_679;
					}
					catch (WebException ex2)
					{
						string text5 = null;
						try
						{
							HttpWebResponse httpWebResponse3 = (HttpWebResponse)ex2.Response;
							using (httpWebResponse3)
							{
								string text6 = null;
								Stream responseStream2 = httpWebResponse3.GetResponseStream();
								if (responseStream2 != null)
								{
									using (StreamReader streamReader2 = new StreamReader(responseStream2, new UTF8Encoding()))
									{
										text6 = streamReader2.ReadToEnd();
									}
								}
								text5 = (from ss in text6.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text5 ?? ex2.Message
						}), true, true);
						if (ex2.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout2 = 4000;
						}
						else if (ex2.Message.Contains("time"))
						{
							millisecondsTimeout2 = 4000;
						}
						goto IL_679;
					}
					IL_666:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout2);
						continue;
					}
					continue;
					IL_679:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_666;
				}
				string text7 = this._snkrsProduct["id"].Value<string>();
				string text8 = "";
				try
				{
					foreach (JToken jtoken in ((JArray)this._snkrsProduct["skus"]))
					{
						if (jtoken["nikeSize"].Value<string>() == this.AtcItem.Size)
						{
							text8 = jtoken["id"].Value<string>();
						}
					}
				}
				catch (Exception)
				{
					throw new Exception("Error getting sku details...");
				}
				if (text8.IsNullOrWhiteSpace())
				{
					throw new Exception("No matching size found. Please use US size for snkrs.");
				}
				string text9 = Guid.NewGuid().ToString();
				this.Req.Accept = "application/json";
				jobject = JObject.Parse("{\"request\":{\"email\":\"clayton.jolin20@gmail.com\",\"country\":\"GB\",\"currency\":\"\",\"locale\":\"en_GB\",\"channel\":\"SNKRS\",\"clientInfo\":{\"deviceId\":\"\"},\"items\":[{\"id\":\"64cfc047-f01f-57de-a691-6a085bdb8ec4\",\"skuId\":\"94a56cea-e2b9-503f-a5d8-c9e722b10c47\",\"quantity\":1,\"recipient\":{\"firstName\":\"ccc\",\"lastName\":\"Pharma\"},\"shippingAddress\":{\"address1\":\"90 Exning Road\",\"city\":\"HARLOW HILL\",\"postalCode\":\"NE15 7DN\",\"country\":\"GB\"},\"contactInfo\":{\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"7441909052\"},\"shippingMethod\":\"GROUND_SERVICE\"}]}}");
				jobject["request"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["currency"] = NikeUrls.SnkrsCurrency;
				jobject["request"]["locale"] = NikeUrls.NikeLangLocale;
				jobject["request"]["items"][0]["id"] = text7;
				jobject["request"]["items"][0]["skuId"] = text8;
				jobject["request"]["items"][0]["recipient"]["firstName"] = creditCardProfile.ShippingFirstName;
				jobject["request"]["items"][0]["recipient"]["lastName"] = creditCardProfile.ShippingLastName;
				if (Form1.SiteType == SiteType.NikeJP)
				{
					jobject["request"]["items"][0]["recipient"]["altFirstName"] = creditCardProfile.ShippingFirstName;
					jobject["request"]["items"][0]["recipient"]["altLastName"] = creditCardProfile.ShippingLastName;
				}
				jobject["request"]["items"][0]["shippingAddress"]["address1"] = creditCardProfile.ShippingAddress1;
				jobject["request"]["items"][0]["shippingAddress"]["address2"] = creditCardProfile.ShippingAddress2;
				jobject["request"]["items"][0]["shippingAddress"]["city"] = creditCardProfile.ShippingCity;
				jobject["request"]["items"][0]["shippingAddress"]["postalCode"] = creditCardProfile.ShippingZipCode;
				if (Form1.SiteType == SiteType.NikeJP)
				{
					jobject["request"]["items"][0]["shippingAddress"]["address3"] = creditCardProfile.ShippingAddress1;
					jobject["request"]["items"][0]["shippingAddress"]["state"] = creditCardProfile.ShippingStateJP;
				}
				jobject["request"]["items"][0]["shippingAddress"]["postalCode"] = creditCardProfile.ShippingZipCode;
				jobject["request"]["items"][0]["shippingAddress"]["country"] = NikeUrls.SnkrsCountryCode;
				jobject["request"]["items"][0]["contactInfo"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["request"]["items"][0]["contactInfo"]["phoneNumber"] = creditCardProfile.ShippingPhone;
				JObject jobject2 = JObject.Parse(jobject.ToString());
				url = "https://api.nike.com/buy/checkout_previews/v2/" + text9;
				flag = true;
				int num = 0;
				string value = "";
				while (flag)
				{
					int millisecondsTimeout3 = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
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
						string text = this.Req.PostRequest(url, jobject.ToString(), null, "application/json; charset=UTF-8", null, true, "PUT");
						jobject = (JObject)JsonConvert.DeserializeObject(text);
						this.CheckNikeSnkrsError(text);
						if (jobject["status"].Value<string>() != "PENDING")
						{
							throw new Exception(text);
						}
						url = "https://api.nike.com/buy/checkout_previews/v2/jobs/" + text9;
						while (jobject["status"].Value<string>().EqualsAny(new string[]
						{
							"PENDING",
							"IN_PROGRESS"
						}))
						{
							Thread.Sleep(1500);
							text = this.Req.GetRequest(url, null, null, true, true);
							jobject = (JObject)JsonConvert.DeserializeObject(text);
						}
						this.CheckNikeSnkrsError(text);
						if (jobject["status"].Value<string>() != "COMPLETED")
						{
							throw new Exception(text);
						}
						try
						{
							num = jobject["response"]["shippingGroups"][0]["items"][0]["priceInfo"]["total"].Value<int>();
						}
						catch (Exception)
						{
						}
						try
						{
							value = jobject["response"]["priceChecksum"].Value<string>();
						}
						catch (Exception)
						{
						}
						flag = false;
						goto IL_F1B;
					}
					catch (WebException ex3)
					{
						string text10 = null;
						try
						{
							HttpWebResponse httpWebResponse4 = (HttpWebResponse)ex3.Response;
							using (httpWebResponse4)
							{
								string text11 = null;
								Stream responseStream3 = httpWebResponse4.GetResponseStream();
								if (responseStream3 != null)
								{
									using (StreamReader streamReader3 = new StreamReader(responseStream3, new UTF8Encoding()))
									{
										text11 = streamReader3.ReadToEnd();
									}
								}
								text10 = (from ss in text11.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text10 ?? ex3.Message
						}), true, true);
						if (ex3.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout3 = 4000;
						}
						else if (ex3.Message.Contains("time"))
						{
							millisecondsTimeout3 = 500;
						}
						goto IL_F1B;
					}
					IL_F08:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout3);
						continue;
					}
					continue;
					IL_F1B:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_F08;
				}
				string value2 = Guid.NewGuid().ToString();
				this.Req.Accept = "application/json";
				jobject = JObject.Parse("{\"checkoutId\":\"4eac17a1-75ad-eaa0-caed-d4d094e3eb44\",\"total\":115,\"currency\":\"GBP\",\"country\":\"GB\",\"items\":[{\"productId\":\"64cfc047-f01f-57de-a691-6a085bdb8ec4\",\"shippingAddress\":{\"address1\":\"90 Exning Road\",\"city\":\"HARLOW HILL\",\"postalCode\":\"NE15 7DN\",\"country\":\"GB\"}}],\"paymentInfo\":[{\"id\":\"36c9f00e-1ba0-074c-70e5-d47daaa97d2a\",\"billingInfo\":{\"name\":{\"firstName\":\"xxx\",\"lastName\":\"xxx\"},\"address\":{\"address1\":\"90 Exning Road\",\"city\":\"HARLOW HILL\",\"postalCode\":\"NE15 7DN\",\"country\":\"GB\"},\"contactInfo\":{\"phoneNumber\":\"7441909052\",\"email\":\"clayton.jolin20@gmail.com\"}},\"type\":\"CreditCard\",\"creditCardInfoId\":\"d178097f-f771-4c45-ac1a-72f2aaa91429\",\"creditCardInfoId\":null}]}");
				jobject["checkoutId"] = text9;
				jobject["total"] = num;
				jobject["currency"] = NikeUrls.SnkrsCurrency;
				jobject["country"] = NikeUrls.NikeCountryCode;
				jobject["items"][0]["productId"] = text7;
				jobject["items"][0]["shippingAddress"]["address1"] = creditCardProfile.ShippingAddress1;
				jobject["items"][0]["shippingAddress"]["address2"] = creditCardProfile.ShippingAddress2;
				jobject["items"][0]["shippingAddress"]["city"] = creditCardProfile.ShippingCity;
				jobject["items"][0]["shippingAddress"]["postalCode"] = creditCardProfile.ShippingZipCode;
				jobject["items"][0]["shippingAddress"]["country"] = NikeUrls.SnkrsCountryCode;
				if (Form1.SiteType == SiteType.NikeJP)
				{
					jobject["items"][0]["shippingAddress"]["address3"] = creditCardProfile.ShippingAddress1;
					jobject["items"][0]["shippingAddress"]["state"] = creditCardProfile.ShippingStateJP;
				}
				jobject["paymentInfo"][0]["id"] = Guid.NewGuid().ToString();
				jobject["paymentInfo"][0]["billingInfo"]["name"]["firstName"] = creditCardProfile.BillingFirstName;
				jobject["paymentInfo"][0]["billingInfo"]["name"]["lastName"] = creditCardProfile.BillingLastName;
				if (Form1.SiteType == SiteType.NikeJP)
				{
					jobject["paymentInfo"][0]["billingInfo"]["name"]["altFirstName"] = creditCardProfile.BillingFirstName;
					jobject["paymentInfo"][0]["billingInfo"]["name"]["altLastName"] = creditCardProfile.BillingLastName;
				}
				jobject["paymentInfo"][0]["billingInfo"]["address"]["address1"] = creditCardProfile.BillingAddress1;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["address2"] = creditCardProfile.BillingAddress2;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["city"] = creditCardProfile.BillingCity;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["postalCode"] = creditCardProfile.BillingZipCode;
				jobject["paymentInfo"][0]["billingInfo"]["address"]["country"] = NikeUrls.SnkrsCountryCode;
				if (Form1.SiteType == SiteType.NikeJP)
				{
					jobject["paymentInfo"][0]["billingInfo"]["address"]["address3"] = creditCardProfile.BillingAddress1;
					jobject["paymentInfo"][0]["billingInfo"]["address"]["state"] = creditCardProfile.BillingStateJP;
				}
				jobject["paymentInfo"][0]["billingInfo"]["contactInfo"]["phoneNumber"] = creditCardProfile.BillingPhone;
				jobject["paymentInfo"][0]["billingInfo"]["contactInfo"]["email"] = this.AtcItem.Account.EmailAddress;
				jobject["paymentInfo"][0]["type"] = "CreditCard";
				jobject["paymentInfo"][0]["creditCardInfoId"] = text4;
				url = "https://api.nike.com/payment/preview/v2/";
				flag = true;
				while (flag)
				{
					int millisecondsTimeout4 = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
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
						string text = this.Req.PostRequest(url, jobject.ToString(), null, "application/json; charset=UTF-8", null, true, "POST");
						jobject = (JObject)JsonConvert.DeserializeObject(text);
						this.CheckNikeSnkrsError(text);
						if (jobject["status"].Value<string>() != "PENDING")
						{
							throw new Exception(text);
						}
						string str = jobject["id"].Value<string>();
						url = "https://api.nike.com/payment/preview/v2/jobs/" + str;
						while (jobject["status"].Value<string>().EqualsAny(new string[]
						{
							"PENDING",
							"IN_PROGRESS"
						}))
						{
							Thread.Sleep(1500);
							text = this.Req.GetRequest(url, null, null, true, true);
							jobject = (JObject)JsonConvert.DeserializeObject(text);
						}
						this.CheckNikeSnkrsError(text);
						if (jobject["status"].Value<string>() != "COMPLETED")
						{
							throw new Exception(text);
						}
						flag = false;
						goto IL_17D7;
					}
					catch (WebException ex4)
					{
						string text12 = null;
						try
						{
							HttpWebResponse httpWebResponse5 = (HttpWebResponse)ex4.Response;
							using (httpWebResponse5)
							{
								string text13 = null;
								Stream responseStream4 = httpWebResponse5.GetResponseStream();
								if (responseStream4 != null)
								{
									using (StreamReader streamReader4 = new StreamReader(responseStream4, new UTF8Encoding()))
									{
										text13 = streamReader4.ReadToEnd();
									}
								}
								text12 = (from ss in text13.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text12 ?? ex4.Message
						}), true, true);
						if (ex4.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout4 = 4000;
						}
						else if (ex4.Message.Contains("time"))
						{
							millisecondsTimeout4 = 500;
						}
						goto IL_17D7;
					}
					IL_17C4:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout4);
						continue;
					}
					continue;
					IL_17D7:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_17C4;
				}
				if (!this._isRandom)
				{
					jobject = jobject2;
					jobject["request"]["paymentToken"] = value2;
					url = "https://api.nike.com/buy/checkouts/v2/" + text9;
					flag = true;
					while (flag)
					{
						int millisecondsTimeout5 = 1500;
						if (this.CheckCheckoutLimitReached1(creditCardProfile))
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
							string text = this.Req.PostRequest(url, jobject.ToString(), null, "application/json; charset=UTF-8", null, true, "PUT");
							jobject = (JObject)JsonConvert.DeserializeObject(text);
							this.CheckNikeSnkrsError(text);
							if (jobject["status"].Value<string>() != "PENDING")
							{
								throw new Exception(text);
							}
							url = "https://api.nike.com/buy/checkouts/v2/jobs/" + text9;
							while (jobject["status"].Value<string>().EqualsAny(new string[]
							{
								"PENDING",
								"IN_PROGRESS"
							}))
							{
								Thread.Sleep(1500);
								text = this.Req.GetRequest(url, null, null, true, true);
								jobject = (JObject)JsonConvert.DeserializeObject(text);
							}
							this.CheckNikeSnkrsError(text);
							if (jobject["status"].Value<string>() != "COMPLETED")
							{
								throw new Exception(text);
							}
							creditCardProfile.CheckoutCount++;
							Logger.Log("{0}: Web SNKRS Checkout success!".With(new object[]
							{
								this.AtcItem.Account.EmailAddress
							}), true, true);
							flag = false;
							goto IL_1AF9;
						}
						catch (WebException ex5)
						{
							string text14 = null;
							try
							{
								HttpWebResponse httpWebResponse6 = (HttpWebResponse)ex5.Response;
								using (httpWebResponse6)
								{
									string text15 = null;
									Stream responseStream5 = httpWebResponse6.GetResponseStream();
									if (responseStream5 != null)
									{
										using (StreamReader streamReader5 = new StreamReader(responseStream5, new UTF8Encoding()))
										{
											text15 = streamReader5.ReadToEnd();
										}
									}
									text14 = (from ss in text15.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
									select ss.Groups["msg"].Value).JoinToString(" ");
								}
							}
							catch (Exception)
							{
							}
							Logger.Log("{0} ({1}): Error... {2}".With(new object[]
							{
								this.AtcItem.Account.EmailAddress,
								this.AtcItem.Details.Line1,
								text14 ?? ex5.Message
							}), true, true);
							if (ex5.Message.ContainsAny(new string[]
							{
								"403",
								"429"
							}))
							{
								millisecondsTimeout5 = 4000;
							}
							else if (ex5.Message.Contains("time"))
							{
								millisecondsTimeout5 = 500;
							}
							goto IL_1AF9;
						}
						IL_1AE6:
						if (flag)
						{
							Thread.Sleep(millisecondsTimeout5);
							continue;
						}
						continue;
						IL_1AF9:
						if (this.CheckCheckoutLimitReached1(creditCardProfile))
						{
							return true;
						}
						goto IL_1AE6;
					}
					try
					{
						Form1.AddedProducts.Add(new AddedProduct(DateTime.Now, "Web SNKRS Checkout Success!", this.AtcItem.Details.StyleCode, "Cart Total: {0}".With(new object[]
						{
							num
						}), this.AtcItem.Account.EmailAddress, this.AtcItem.Details.Size, this.AtcItem.Account.Password, this.AtcItem.SnkrsBuyUrl, this.AtcItem.Account));
						Form1.AddedProductsForm.UpdateForm();
						AddToCart.Callback("{0} Web SNKRS Checkout Success!".With(new object[]
						{
							Form1.SiteType
						}), "Cart Total: {0}".With(new object[]
						{
							num
						}), this.AtcItem.Account.EmailAddress, this.AtcItem.Account.ProductStyleCodes.First<string>());
					}
					catch (Exception)
					{
					}
					result = true;
				}
				else
				{
					jobject = JObject.Parse("{\"launchId\":\"5b4a0db4-0e31-45f0-8def-f1d3f1eeb693\",\"skuId\":\"d41e673b-7cd2-5725-9268-a63a82ffb41e\",\"locale\":\"en_GB\",\"currency\":\"GBP\",\"deviceId\":\"\",\"shipping\":{\"recipient\":{\"firstName\":\"Lokesh\",\"lastName\":\"Pharma\",\"email\":\"clayton.jolin20@gmail.com\",\"phoneNumber\":\"7441909052\"},\"address\":{\"address1\":\"90 Exning Road\",\"city\":\"HARLOW HILL\",\"postalCode\":\"NE15 7DN\",\"country\":\"GB\"},\"method\":\"GROUND_SERVICE\"},\"priceChecksum\":\"82f52a1aeee379b8c021152bd5768265\",\"paymentToken\":\"20b9585c-e0ab-446b-92af-b776a70a1627\",\"channel\":\"SNKRS\"}");
					string value3 = "";
					try
					{
						string text = this.Req.GetRequest("https://api.nike.com/launch/launch_views/v2/?filter=productId({0})".With(new object[]
						{
							text7
						}), null, null, true, true);
						value3 = JObject.Parse(text)["objects"][0]["id"].Value<string>();
					}
					catch (Exception)
					{
					}
					jobject["launchId"] = value3;
					jobject["skuId"] = text8;
					jobject["locale"] = NikeUrls.NikeLangLocale;
					jobject["currency"] = NikeUrls.SnkrsCurrency;
					jobject["checkoutId"] = text9;
					jobject["shipping"]["recipient"]["firstName"] = creditCardProfile.ShippingFirstName;
					jobject["shipping"]["recipient"]["lastName"] = creditCardProfile.ShippingLastName;
					if (Form1.SiteType == SiteType.NikeJP)
					{
						jobject["shipping"]["recipient"]["altFirstName"] = creditCardProfile.ShippingFirstName;
						jobject["shipping"]["recipient"]["altLastName"] = creditCardProfile.ShippingLastName;
					}
					jobject["shipping"]["recipient"]["email"] = this.AtcItem.Account.EmailAddress;
					jobject["shipping"]["recipient"]["phoneNumber"] = creditCardProfile.ShippingPhone;
					jobject["shipping"]["address"]["address1"] = creditCardProfile.ShippingAddress1;
					jobject["shipping"]["address"]["address2"] = creditCardProfile.ShippingAddress2;
					jobject["shipping"]["address"]["city"] = creditCardProfile.ShippingCity;
					jobject["shipping"]["address"]["postalCode"] = creditCardProfile.ShippingZipCode;
					jobject["shipping"]["address"]["country"] = NikeUrls.SnkrsCountryCode;
					if (Form1.SiteType == SiteType.NikeJP)
					{
						jobject["shipping"]["address"]["address3"] = creditCardProfile.ShippingAddress1;
						jobject["shipping"]["address"]["state"] = creditCardProfile.ShippingStateJP;
					}
					jobject["shipping"]["method"] = "GROUND_SERVICE";
					jobject["priceChecksum"] = value;
					jobject["paymentToken"] = value2;
					jobject["channel"] = "SNKRS";
					url = "https://api.nike.com/launch/entries/v2/";
					flag = true;
					while (flag)
					{
						int millisecondsTimeout6 = 1500;
						if (this.CheckCheckoutLimitReached1(creditCardProfile))
						{
							return true;
						}
						try
						{
							Logger.Log("{0} ({1}): Submitting draw entry...".With(new object[]
							{
								this.AtcItem.Account.EmailAddress,
								this.AtcItem.Details.Line1
							}), true, true);
							string text = this.Req.PostRequest(url, jobject.ToString(), null, "application/json; charset=UTF-8", null, true, "POST");
							jobject = (JObject)JsonConvert.DeserializeObject(text);
							this.CheckNikeSnkrsError(text);
							if (jobject["status"].Value<string>() != "PENDING")
							{
								throw new Exception(text);
							}
							url = "https://api.nike.com/launch/entries/v2/jobs/" + text9;
							while (jobject["status"].Value<string>().EqualsAny(new string[]
							{
								"PENDING",
								"IN_PROGRESS"
							}))
							{
								Thread.Sleep(1500);
								text = this.Req.GetRequest(url, null, null, true, true);
								jobject = (JObject)JsonConvert.DeserializeObject(text);
							}
							this.CheckNikeSnkrsError(text);
							if (jobject["status"].Value<string>() != "COMPLETED")
							{
								throw new Exception(text);
							}
							creditCardProfile.CheckoutCount++;
							Logger.Log("{0}: Web SNKRS Draw Entry success!".With(new object[]
							{
								this.AtcItem.Account.EmailAddress
							}), true, true);
							flag = false;
							goto IL_226B;
						}
						catch (WebException ex6)
						{
							string text16 = null;
							try
							{
								HttpWebResponse httpWebResponse7 = (HttpWebResponse)ex6.Response;
								using (httpWebResponse7)
								{
									string text17 = null;
									Stream responseStream6 = httpWebResponse7.GetResponseStream();
									if (responseStream6 != null)
									{
										using (StreamReader streamReader6 = new StreamReader(responseStream6, new UTF8Encoding()))
										{
											text17 = streamReader6.ReadToEnd();
										}
									}
									text16 = (from ss in text17.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
									select ss.Groups["msg"].Value).JoinToString(" ");
								}
							}
							catch (Exception)
							{
							}
							Logger.Log("{0} ({1}): Error... {2}".With(new object[]
							{
								this.AtcItem.Account.EmailAddress,
								this.AtcItem.Details.Line1,
								text16 ?? ex6.Message
							}), true, true);
							if (ex6.Message.ContainsAny(new string[]
							{
								"403",
								"429"
							}))
							{
								millisecondsTimeout6 = 4000;
							}
							else if (ex6.Message.Contains("time"))
							{
								millisecondsTimeout6 = 500;
							}
							goto IL_226B;
						}
						IL_2258:
						if (flag)
						{
							Thread.Sleep(millisecondsTimeout6);
							continue;
						}
						continue;
						IL_226B:
						if (this.CheckCheckoutLimitReached1(creditCardProfile))
						{
							return true;
						}
						goto IL_2258;
					}
					try
					{
						Form1.AddedProducts.Add(new AddedProduct(DateTime.Now, "Web SNKRS Draw Entry Success!", this.AtcItem.Details.StyleCode, "Cart Total: {0}".With(new object[]
						{
							num
						}), this.AtcItem.Account.EmailAddress, this.AtcItem.Details.Size, this.AtcItem.Account.Password, this.AtcItem.SnkrsBuyUrl, this.AtcItem.Account));
						Form1.AddedProductsForm.UpdateForm();
						AddToCart.Callback("{0} Web SNKRS Draw Entry Success!".With(new object[]
						{
							Form1.SiteType
						}), "Cart Total: {0}".With(new object[]
						{
							num
						}), this.AtcItem.Account.EmailAddress, this.AtcItem.Account.ProductStyleCodes.First<string>());
					}
					catch (Exception)
					{
					}
					result = true;
				}
			}
			catch (Exception ex7)
			{
				Logger.Log("{0} ({1}): Error in web snkrs... {2}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Details.Line1,
					ex7.Message
				}), true, true);
				result = false;
			}
			return result;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0001D07C File Offset: 0x0001B27C
		public bool ProcessWebSnkrs()
		{
			bool result;
			try
			{
				string url = "";
				string text = "";
				CreditCardProfile creditCardProfile = Form1.CreditCardProfiles.FirstOrDefault((CreditCardProfile c) => c.Name == this.AtcItem.Account.CheckoutInfo.CcProfile);
				if (creditCardProfile.IsNull())
				{
					Thread.Sleep(1000);
					throw new Exception("Checkout profile not found. You must have checkout profile for websnkrs. {0}".With(new object[]
					{
						this.AtcItem.Account.CheckoutInfo.CcProfile
					}));
				}
				url = "https://unite.nikecloud.com/login?appVersion=281&experienceVersion=244&uxid=com.nike.commerce.snkrs.ios&locale=en_US&backendEnvironment=identity&browser=Apple+Computer%2C+Inc.&os=undefined&mobile=true&native=true".With(new object[]
				{
					NikeUrls.NikeLangLocale
				});
				this.Req.ResetHeaders(false);
				this.Req.Headers = new List<string>();
				this.Req.Accept = "*/*";
				this.Req.KeepAlive = true;
				Dictionary<string, string> objectToSerialize = new Dictionary<string, string>
				{
					{
						"username",
						this.AtcItem.Account.EmailAddress
					},
					{
						"password",
						this.AtcItem.Account.Password
					},
					{
						"client_id",
						"G64vA0b95ZruUtGk1K0FkAgaO3Ch30sj"
					},
					{
						"ux_id",
						"com.nike.commerce.snkrs.web"
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
				bool flag = true;
				while (flag)
				{
					int millisecondsTimeout = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					try
					{
						Logger.Log("{0} ({1}): Generating tokens...".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1
						}), true, true);
						string text2 = this.Req.PostRequest(url, objectToSerialize.ToJSON(), "https://s3.nikecdn.com/unite/mobile.html?iOSSDKVersion=2.4.2&clientId=G64vA0b95ZruUtGk1K0FkAgaO3Ch30sj&uxId=com.nike.commerce.snkrs.ios&view=none&locale=en_US&backendEnvironment=identity", "application/json", new string[]
						{
							"Origin: https://s3.nikecdn.com"
						}, true, "POST");
						JObject jobject = JObject.Parse(text2);
						this.AtcItem.Account.WebSnkrsToken = jobject["access_token"].Value<string>();
						this.Req.Headers.Add("authorization: Bearer {0}".With(new object[]
						{
							this.AtcItem.Account.WebSnkrsToken
						}));
						flag = false;
						goto IL_3A6;
					}
					catch (WebException ex)
					{
						string text3 = null;
						try
						{
							HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
							using (httpWebResponse)
							{
								string text4 = null;
								Stream responseStream = httpWebResponse.GetResponseStream();
								if (responseStream != null)
								{
									using (StreamReader streamReader = new StreamReader(responseStream, new UTF8Encoding()))
									{
										text4 = streamReader.ReadToEnd();
									}
								}
								text3 = (from ss in text4.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text3 ?? ex.Message
						}), true, true);
						if (ex.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout = 4000;
						}
						else if (ex.Message.Contains("time"))
						{
							millisecondsTimeout = 500;
						}
						goto IL_3A6;
					}
					IL_393:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout);
						continue;
					}
					continue;
					IL_3A6:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_393;
				}
				url = "https://api.nike.com/orders/v1/orders/checkouts";
				objectToSerialize = new Dictionary<string, string>
				{
					{
						"locale",
						NikeUrls.NikeLangLocale
					},
					{
						"country",
						NikeUrls.NikeCountryCode
					},
					{
						"email",
						this.AtcItem.Account.EmailAddress
					},
					{
						"currency",
						"USD"
					}
				};
				flag = true;
				while (flag)
				{
					int millisecondsTimeout2 = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
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
						string text2 = this.Req.PostRequest(url, objectToSerialize.ToJSON(), null, "application/json", new string[]
						{
							"Origin: https://www.nike.com"
						}, true, "POST");
						JObject jobject = (JObject)JsonConvert.DeserializeObject(text2);
						text = jobject["id"].Value<string>();
						flag = false;
						goto IL_61D;
					}
					catch (WebException ex2)
					{
						string text5 = null;
						try
						{
							HttpWebResponse httpWebResponse3 = (HttpWebResponse)ex2.Response;
							using (httpWebResponse3)
							{
								string text6 = null;
								Stream responseStream2 = httpWebResponse3.GetResponseStream();
								if (responseStream2 != null)
								{
									using (StreamReader streamReader2 = new StreamReader(responseStream2, new UTF8Encoding()))
									{
										text6 = streamReader2.ReadToEnd();
									}
								}
								text5 = (from ss in text6.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text5 ?? ex2.Message
						}), true, true);
						if (ex2.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout2 = 4000;
						}
						else if (ex2.Message.Contains("time"))
						{
							millisecondsTimeout2 = 500;
						}
						goto IL_61D;
					}
					IL_60A:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout2);
						continue;
					}
					continue;
					IL_61D:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_60A;
				}
				url = "https://api.nike.com/orders/v1/orders/checkouts/{0}/shippingaddress".With(new object[]
				{
					text
				});
				objectToSerialize = new Dictionary<string, string>
				{
					{
						"city",
						creditCardProfile.ShippingCity
					},
					{
						"state",
						creditCardProfile.ShippingState
					},
					{
						"phoneNumber",
						creditCardProfile.ShippingPhone
					},
					{
						"firstName",
						creditCardProfile.ShippingFirstName
					},
					{
						"country",
						"US"
					},
					{
						"postalCode",
						creditCardProfile.ShippingZipCode
					},
					{
						"lastName",
						creditCardProfile.ShippingLastName
					},
					{
						"address1",
						creditCardProfile.ShippingAddress1
					},
					{
						"address2",
						creditCardProfile.ShippingAddress2
					},
					{
						"preferred",
						"true"
					}
				};
				flag = true;
				while (flag)
				{
					int millisecondsTimeout3 = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					try
					{
						Logger.Log("{0} ({1}): Setting shipping address...".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1
						}), true, true);
						string text2 = this.Req.PostRequest(url, objectToSerialize.ToJSON(), null, "application/json", new string[]
						{
							"Origin: https://www.nike.com"
						}, true, "PUT");
						flag = false;
						goto IL_8F6;
					}
					catch (WebException ex3)
					{
						string text7 = null;
						try
						{
							HttpWebResponse httpWebResponse4 = (HttpWebResponse)ex3.Response;
							using (httpWebResponse4)
							{
								string text8 = null;
								Stream responseStream3 = httpWebResponse4.GetResponseStream();
								if (responseStream3 != null)
								{
									using (StreamReader streamReader3 = new StreamReader(responseStream3, new UTF8Encoding()))
									{
										text8 = streamReader3.ReadToEnd();
									}
								}
								text7 = (from ss in text8.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text7 ?? ex3.Message
						}), true, true);
						if (ex3.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout3 = 4000;
						}
						else if (ex3.Message.Contains("time"))
						{
							millisecondsTimeout3 = 500;
						}
						goto IL_8F6;
					}
					IL_8E3:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout3);
						continue;
					}
					continue;
					IL_8F6:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_8E3;
				}
				string text9 = Guid.NewGuid().ToString();
				url = "https://paymentcc.nike.com/creditcardsubmit/{0}/store".With(new object[]
				{
					text9
				});
				objectToSerialize = new Dictionary<string, string>
				{
					{
						"expirationMonth",
						creditCardProfile.CreditCardExpiryMonth.PadLeft(2, '0')
					},
					{
						"accountNumber",
						creditCardProfile.CreditCardNumber
					},
					{
						"creditCardInfoId",
						text9
					},
					{
						"cvNumber",
						creditCardProfile.CreditCardCvv
					},
					{
						"cardType",
						creditCardProfile.CreditCardType.ToUpper()
					},
					{
						"expirationYear",
						creditCardProfile.CreditCardExpiryYear
					}
				};
				flag = true;
				while (flag)
				{
					int millisecondsTimeout4 = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
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
						string text2 = this.Req.PostRequest(url, objectToSerialize.ToJSON(), "https://paymentcc.nike.com/services?id={0}".With(new object[]
						{
							text9
						}), "application/json", new string[]
						{
							"Origin: https://www.nike.com"
						}, true, "POST");
						flag = false;
						goto IL_BBF;
					}
					catch (WebException ex4)
					{
						string text10 = null;
						try
						{
							HttpWebResponse httpWebResponse5 = (HttpWebResponse)ex4.Response;
							using (httpWebResponse5)
							{
								string text11 = null;
								Stream responseStream4 = httpWebResponse5.GetResponseStream();
								if (responseStream4 != null)
								{
									using (StreamReader streamReader4 = new StreamReader(responseStream4, new UTF8Encoding()))
									{
										text11 = streamReader4.ReadToEnd();
									}
								}
								text10 = (from ss in text11.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text10 ?? ex4.Message
						}), true, true);
						if (ex4.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout4 = 4000;
						}
						else if (ex4.Message.Contains("time"))
						{
							millisecondsTimeout4 = 500;
						}
						goto IL_BBF;
					}
					IL_BAC:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout4);
						continue;
					}
					continue;
					IL_BBF:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_BAC;
				}
				url = "https://api.nike.com/orders/v1/orders/checkouts/{0}/payments".With(new object[]
				{
					text
				});
				objectToSerialize = new Dictionary<string, string>
				{
					{
						"paymentType",
						"creditcard"
					},
					{
						"creditCardInfoId",
						text9
					}
				};
				string text12 = "";
				flag = true;
				while (flag)
				{
					int millisecondsTimeout5 = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					try
					{
						Logger.Log("{0} ({1}): Setting payment method...".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1
						}), true, true);
						string text2 = this.Req.PostRequest(url, objectToSerialize.ToJSON(), null, "application/json", new string[]
						{
							"Origin: https://www.nike.com"
						}, true, "POST");
						try
						{
							text12 = "$" + JObject.Parse(text2)["total"].Value<string>();
						}
						catch (Exception)
						{
						}
						flag = false;
						goto IL_E2A;
					}
					catch (WebException ex5)
					{
						string text13 = null;
						try
						{
							HttpWebResponse httpWebResponse6 = (HttpWebResponse)ex5.Response;
							using (httpWebResponse6)
							{
								string text14 = null;
								Stream responseStream5 = httpWebResponse6.GetResponseStream();
								if (responseStream5 != null)
								{
									using (StreamReader streamReader5 = new StreamReader(responseStream5, new UTF8Encoding()))
									{
										text14 = streamReader5.ReadToEnd();
									}
								}
								text13 = (from ss in text14.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text13 ?? ex5.Message
						}), true, true);
						if (ex5.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout5 = 4000;
						}
						else if (ex5.Message.Contains("time"))
						{
							millisecondsTimeout5 = 500;
						}
						goto IL_E2A;
					}
					IL_E17:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout5);
						continue;
					}
					continue;
					IL_E2A:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_E17;
				}
				url = "https://api.nike.com/orders/v1/orders/checkouts/{0}/billingaddress".With(new object[]
				{
					text
				});
				objectToSerialize = new Dictionary<string, string>
				{
					{
						"city",
						creditCardProfile.BillingCity
					},
					{
						"state",
						creditCardProfile.BillingState
					},
					{
						"phoneNumber",
						creditCardProfile.BillingPhone
					},
					{
						"firstName",
						creditCardProfile.BillingFirstName
					},
					{
						"country",
						"US"
					},
					{
						"postalCode",
						creditCardProfile.BillingZipCode
					},
					{
						"lastName",
						creditCardProfile.BillingLastName
					},
					{
						"address1",
						creditCardProfile.BillingAddress1
					},
					{
						"address2",
						creditCardProfile.BillingAddress2
					}
				};
				flag = true;
				while (flag)
				{
					int millisecondsTimeout6 = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					try
					{
						Logger.Log("{0} ({1}): Setting shipping address...".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1
						}), true, true);
						string text2 = this.Req.PostRequest(url, objectToSerialize.ToJSON(), null, "application/json", new string[]
						{
							"Origin: https://www.nike.com"
						}, true, "PUT");
						flag = false;
						goto IL_10F2;
					}
					catch (WebException ex6)
					{
						string text15 = null;
						try
						{
							HttpWebResponse httpWebResponse7 = (HttpWebResponse)ex6.Response;
							using (httpWebResponse7)
							{
								string text16 = null;
								Stream responseStream6 = httpWebResponse7.GetResponseStream();
								if (responseStream6 != null)
								{
									using (StreamReader streamReader6 = new StreamReader(responseStream6, new UTF8Encoding()))
									{
										text16 = streamReader6.ReadToEnd();
									}
								}
								text15 = (from ss in text16.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text15 ?? ex6.Message
						}), true, true);
						if (ex6.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout6 = 4000;
						}
						else if (ex6.Message.Contains("time"))
						{
							millisecondsTimeout6 = 500;
						}
						goto IL_10F2;
					}
					IL_10DF:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout6);
						continue;
					}
					continue;
					IL_10F2:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_10DF;
				}
				url = "https://api.nike.com/orders/v1/orders/checkouts/{0}/items".With(new object[]
				{
					text
				});
				string[] array = this.AtcItem.Details.StyleCode.Split(new char[]
				{
					'-'
				});
				objectToSerialize = new Dictionary<string, string>
				{
					{
						"color",
						array[1]
					},
					{
						"style",
						array[0]
					},
					{
						"size",
						this.AtcItem.Size
					},
					{
						"quantity",
						"1"
					}
				};
				flag = true;
				while (flag)
				{
					int millisecondsTimeout7 = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					try
					{
						Logger.Log("{0} ({1}): Adding item to cart...".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1
						}), true, true);
						string text2 = this.Req.PostRequest(url, objectToSerialize.ToJSON(), null, "application/json", new string[]
						{
							"Origin: https://www.nike.com"
						}, true, "PUT");
						JObject jobject = (JObject)JsonConvert.DeserializeObject(text2);
						if (!jobject["items"].ToObject<JArray>().IsAny<JToken>())
						{
							throw new Exception("Unknown error");
						}
						flag = false;
						goto IL_13AE;
					}
					catch (WebException ex7)
					{
						string text17 = null;
						try
						{
							HttpWebResponse httpWebResponse8 = (HttpWebResponse)ex7.Response;
							using (httpWebResponse8)
							{
								string text18 = null;
								Stream responseStream7 = httpWebResponse8.GetResponseStream();
								if (responseStream7 != null)
								{
									using (StreamReader streamReader7 = new StreamReader(responseStream7, new UTF8Encoding()))
									{
										text18 = streamReader7.ReadToEnd();
									}
								}
								text17 = (from ss in text18.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text17 ?? ex7.Message
						}), true, true);
						if (ex7.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout7 = 4000;
						}
						else if (ex7.Message.Contains("time"))
						{
							millisecondsTimeout7 = 500;
						}
						goto IL_13AE;
					}
					IL_139B:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout7);
						continue;
					}
					continue;
					IL_13AE:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_139B;
				}
				Logger.Log("{0} ({1}): Succesfuly added item to cart...".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Details.Line1
				}), true, true);
				url = "https://api.nike.com/orders/v1/orders/checkouts/{0}/shippingmethod".With(new object[]
				{
					text
				});
				flag = true;
				while (flag)
				{
					int millisecondsTimeout8 = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					try
					{
						Logger.Log("{0} ({1}): Setting shipping method...".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1
						}), true, true);
						string text2 = this.Req.PostRequest(url, "{\"id\": \"standard\"}", null, "application/json", new string[]
						{
							"Origin: https://www.nike.com"
						}, true, "PUT");
						JObject jobject = JObject.Parse(text2);
						this.AtcItem.Details.SkuId = jobject["items"][0]["sku"]["skuId"].Value<string>();
						try
						{
							text12 = "$" + JObject.Parse(text2)["total"].Value<string>();
						}
						catch (Exception)
						{
						}
						flag = false;
						goto IL_166F;
					}
					catch (WebException ex8)
					{
						string text19 = null;
						try
						{
							HttpWebResponse httpWebResponse9 = (HttpWebResponse)ex8.Response;
							using (httpWebResponse9)
							{
								string text20 = null;
								Stream responseStream8 = httpWebResponse9.GetResponseStream();
								if (responseStream8 != null)
								{
									using (StreamReader streamReader8 = new StreamReader(responseStream8, new UTF8Encoding()))
									{
										text20 = streamReader8.ReadToEnd();
									}
								}
								text19 = (from ss in text20.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
								select ss.Groups["msg"].Value).JoinToString(" ");
							}
						}
						catch (Exception)
						{
						}
						Logger.Log("{0} ({1}): Error... {2}".With(new object[]
						{
							this.AtcItem.Account.EmailAddress,
							this.AtcItem.Details.Line1,
							text19 ?? ex8.Message
						}), true, true);
						if (ex8.Message.ContainsAny(new string[]
						{
							"403",
							"429"
						}))
						{
							millisecondsTimeout8 = 4000;
						}
						else if (ex8.Message.Contains("time"))
						{
							millisecondsTimeout8 = 500;
						}
						goto IL_166F;
					}
					IL_165C:
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout8);
						continue;
					}
					continue;
					IL_166F:
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					goto IL_165C;
				}
				string text21 = null;
				flag = true;
				int num = 0;
				string text22 = "";
				while (this._waitLineEnabled && flag)
				{
					if (num <= 5 || !this._isRandom)
					{
						num++;
						int millisecondsTimeout9 = 1500;
						if (this.CheckCheckoutLimitReached1(creditCardProfile))
						{
							return true;
						}
						lock (creditCardProfile.Lock)
						{
							try
							{
								if (text21 == null)
								{
									Logger.Log("{0} ({1}): Generating wait token...".With(new object[]
									{
										this.AtcItem.Account.EmailAddress,
										this.AtcItem.Details.Line1
									}), true, true);
									JObject jobject2 = new JObject();
									jobject2["personalInfo"] = new JObject();
									jobject2["personalInfo"]["shippingAddress"] = new JObject();
									jobject2["personalInfo"]["mobilePhone"] = "+1" + creditCardProfile.ShippingPhone;
									jobject2["personalInfo"]["email"] = this.AtcItem.Account.EmailAddress;
									jobject2["personalInfo"]["shippingAddress"]["city"] = creditCardProfile.ShippingCity;
									jobject2["personalInfo"]["shippingAddress"]["state"] = creditCardProfile.ShippingState;
									jobject2["personalInfo"]["shippingAddress"]["phoneNumber"] = creditCardProfile.ShippingPhone;
									jobject2["personalInfo"]["shippingAddress"]["firstName"] = creditCardProfile.ShippingFirstName;
									jobject2["personalInfo"]["shippingAddress"]["country"] = "US";
									jobject2["personalInfo"]["shippingAddress"]["postalCode"] = creditCardProfile.ShippingZipCode;
									jobject2["personalInfo"]["shippingAddress"]["lastName"] = creditCardProfile.ShippingLastName;
									jobject2["personalInfo"]["shippingAddress"]["address1"] = creditCardProfile.ShippingAddress1;
									jobject2["personalInfo"]["shippingAddress"]["address2"] = creditCardProfile.ShippingAddress2;
									jobject2["country"] = "US";
									jobject2["styleColor"] = this.AtcItem.Details.StyleCode;
									jobject2["checkoutId"] = text;
									jobject2["skuId"] = this.AtcItem.Details.SkuId;
									url = "https://api.nike.com/commerce/launchwaitline/launch/waitline/turnToken";
									string text2 = this.Req.PostRequest(url, jobject2.ToString(Newtonsoft.Json.Formatting.None, new JsonConverter[0]), null, "application/json", new string[]
									{
										"Origin: https://www.nike.com"
									}, true, "POST");
									JObject jobject = JObject.Parse(text2);
									text21 = jobject["turnToken"].Value<string>();
									Logger.Log("{0} ({1}): Succesfuly generate token... {2}".With(new object[]
									{
										this.AtcItem.Account.EmailAddress,
										this.AtcItem.Details.Line1,
										text21
									}), true, true);
								}
								else
								{
									Logger.Log("{0} ({1}): Waiting in line...".With(new object[]
									{
										this.AtcItem.Account.EmailAddress,
										this.AtcItem.Details.Line1
									}), true, true);
									url = "https://api.nike.com/commerce/launchwaitline/launch/waitline/turnToken/" + text21;
									string text2 = this.Req.GetRequest(url, null, new string[]
									{
										"Origin: https://www.nike.com"
									}, true, true);
									JObject jobject = JObject.Parse(text2);
									Logger.Log("{0} ({1}): Waitlinestatus: {2}".With(new object[]
									{
										this.AtcItem.Account.EmailAddress,
										this.AtcItem.Details.Line1,
										jobject["waitlineStatus"].Value<string>()
									}), true, true);
									if (jobject["waitlineStatus"].Value<string>() == "COMPLETED")
									{
										try
										{
											Logger.Log("Checkout Success... {0} {1}".With(new object[]
											{
												this.AtcItem.Account.EmailAddress,
												this.AtcItem.Details.Line1 + " " + this.AtcItem.Url
											}), true, true);
											Form1.AddedProducts.Add(new AddedProduct(DateTime.Now, "Checkout Success! {0}".With(new object[]
											{
												this.AtcItem.Details.Line1
											}), this.AtcItem.Details.StyleCode, "Cart Total: {0}".With(new object[]
											{
												text12
											}), this.AtcItem.Account.EmailAddress, this.AtcItem.Details.Size, this.AtcItem.Account.Password, this.AtcItem.SnkrsBuyUrl, this.AtcItem.Account));
											Form1.AddedProductsForm.UpdateForm();
											AddToCart.Callback("{1} Checkout Success! {0}".With(new object[]
											{
												this.AtcItem.Details.Line1,
												Form1.SiteType
											}), "Cart Total: {0}".With(new object[]
											{
												text12
											}), this.AtcItem.Account.EmailAddress, this.AtcItem.Account.ProductStyleCodes.First<string>());
										}
										catch (Exception)
										{
										}
										return true;
									}
								}
								text22 = "";
							}
							catch (WebException ex9)
							{
								string text23 = null;
								text22 = ex9.Message;
								try
								{
									HttpWebResponse httpWebResponse10 = (HttpWebResponse)ex9.Response;
									using (httpWebResponse10)
									{
										string text24 = null;
										Stream responseStream9 = httpWebResponse10.GetResponseStream();
										if (responseStream9 != null)
										{
											using (StreamReader streamReader9 = new StreamReader(responseStream9, new UTF8Encoding()))
											{
												text24 = streamReader9.ReadToEnd();
											}
										}
										text23 = (from ss in text24.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
										select ss.Groups["msg"].Value).JoinToString(" ");
									}
								}
								catch (Exception)
								{
								}
								Logger.Log("{0} ({1}): Error... {2}".With(new object[]
								{
									this.AtcItem.Account.EmailAddress,
									this.AtcItem.Details.Line1,
									text23 ?? ex9.Message
								}), true, true);
								if (ex9.Message.ContainsAny(new string[]
								{
									"403",
									"429"
								}))
								{
									millisecondsTimeout9 = 4000;
								}
								else if (ex9.Message.Contains("time"))
								{
									millisecondsTimeout9 = 500;
								}
							}
						}
						if (this.CheckCheckoutLimitReached1(creditCardProfile))
						{
							return true;
						}
						if (flag)
						{
							Thread.Sleep(millisecondsTimeout9);
						}
					}
					else
					{
						if (!text22.IsNullOrWhiteSpace())
						{
							Logger.Log("Draw Entry Failure... {0} {1}".With(new object[]
							{
								this.AtcItem.Account.EmailAddress,
								this.AtcItem.Url
							}), true, true);
							return true;
						}
						try
						{
							Logger.Log("Draw Entry Success... {0} {1}".With(new object[]
							{
								this.AtcItem.Account.EmailAddress,
								this.AtcItem.Url
							}), true, true);
							Form1.AddedProducts.Add(new AddedProduct(DateTime.Now, "Draw Entry Success!", this.AtcItem.Details.StyleCode, "Cart Total: {0}".With(new object[]
							{
								text12
							}), this.AtcItem.Account.EmailAddress, this.AtcItem.Details.Size, this.AtcItem.Account.Password, this.AtcItem.SnkrsBuyUrl, this.AtcItem.Account));
							Form1.AddedProductsForm.UpdateForm();
							AddToCart.Callback("{0} Draw Entry Success!".With(new object[]
							{
								Form1.SiteType
							}), "Cart Total: {0}".With(new object[]
							{
								text12
							}), this.AtcItem.Account.EmailAddress, this.AtcItem.Account.ProductStyleCodes.First<string>());
						}
						catch (Exception)
						{
						}
						return true;
					}
				}
				url = "https://api.nike.com/orders/v1/orders/checkouts/{0}/submit".With(new object[]
				{
					text
				});
				objectToSerialize = new Dictionary<string, string>
				{
					{
						"id",
						text
					}
				};
				flag = true;
				while (flag)
				{
					int millisecondsTimeout10 = 1500;
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					lock (creditCardProfile.Lock)
					{
						try
						{
							Logger.Log("{0} ({1}): Submitting order...".With(new object[]
							{
								this.AtcItem.Account.EmailAddress,
								this.AtcItem.Details.Line1
							}), true, true);
							string text2 = this.Req.PostRequest(url, objectToSerialize.ToJSON(), null, "application/json", new string[]
							{
								"Origin: https://www.nike.com"
							}, true, "PUT");
							creditCardProfile.CheckoutCount++;
							Logger.Log("{0}: Web SNKRS Checkout success!".With(new object[]
							{
								this.AtcItem.Account.EmailAddress
							}), true, true);
							flag = false;
						}
						catch (WebException ex10)
						{
							string text25 = null;
							try
							{
								HttpWebResponse httpWebResponse11 = (HttpWebResponse)ex10.Response;
								using (httpWebResponse11)
								{
									string text26 = null;
									Stream responseStream10 = httpWebResponse11.GetResponseStream();
									if (responseStream10 != null)
									{
										using (StreamReader streamReader10 = new StreamReader(responseStream10, new UTF8Encoding()))
										{
											text26 = streamReader10.ReadToEnd();
										}
									}
									text25 = (from ss in text26.RegexMatches("\"message\"[\\s:]+\"(?<msg>.*?)\"")
									select ss.Groups["msg"].Value).JoinToString(" ");
								}
							}
							catch (Exception)
							{
							}
							Logger.Log("{0} ({1}): Error... {2}".With(new object[]
							{
								this.AtcItem.Account.EmailAddress,
								this.AtcItem.Details.Line1,
								text25 ?? ex10.Message
							}), true, true);
							if (ex10.Message.ContainsAny(new string[]
							{
								"403",
								"429"
							}))
							{
								millisecondsTimeout10 = 4000;
							}
							else if (ex10.Message.Contains("time"))
							{
								millisecondsTimeout10 = 500;
							}
						}
					}
					if (this.CheckCheckoutLimitReached1(creditCardProfile))
					{
						return true;
					}
					if (flag)
					{
						Thread.Sleep(millisecondsTimeout10);
					}
				}
				try
				{
					Form1.AddedProducts.Add(new AddedProduct(DateTime.Now, "Web SNKRS Checkout Success!", this.AtcItem.Details.StyleCode, "Cart Total: {0}".With(new object[]
					{
						text12
					}), this.AtcItem.Account.EmailAddress, this.AtcItem.Details.Size, this.AtcItem.Account.Password, this.AtcItem.SnkrsBuyUrl, this.AtcItem.Account));
					Form1.AddedProductsForm.UpdateForm();
					AddToCart.Callback("{0} Web SNKRS Checkout Success!".With(new object[]
					{
						Form1.SiteType
					}), "Cart Total: {0}".With(new object[]
					{
						text12
					}), this.AtcItem.Account.EmailAddress, this.AtcItem.Account.ProductStyleCodes.First<string>());
				}
				catch (Exception)
				{
				}
				result = true;
			}
			catch (Exception ex11)
			{
				Logger.Log("{0} ({1}): Error in web snkrs... {2}".With(new object[]
				{
					this.AtcItem.Account.EmailAddress,
					this.AtcItem.Details.Line1,
					ex11.Message
				}), true, true);
				result = false;
			}
			return result;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0001F960 File Offset: 0x0001DB60
		public static string GetUnixTimeStamp()
		{
			return ((long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1).ToUniversalTime()).TotalMilliseconds).ToString();
		}

		// Token: 0x04000149 RID: 329
		public string OldHtml;

		// Token: 0x0400014A RID: 330
		public MonitorLink.NikeSku[] Skus;

		// Token: 0x0400014B RID: 331
		public bool Simulate;

		// Token: 0x0400014C RID: 332
		public bool ForceOldMethod;

		// Token: 0x0400014D RID: 333
		private bool _newMethod;

		// Token: 0x0400014E RID: 334
		private bool _waitLineEnabled;

		// Token: 0x0400014F RID: 335
		private bool _isRandom;

		// Token: 0x04000150 RID: 336
		private JObject _snkrsProduct;

		// Token: 0x04000151 RID: 337
		private string _auId;

		// Token: 0x04000152 RID: 338
		private string _authHeader = "";
	}
}
