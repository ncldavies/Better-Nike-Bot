using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using Better_Nike_Bot.Utilities;
using IMLokesh.Extensions;
using IMLokesh.Http;
using IMLokesh.RandomUtility;
using IMLokesh.ThreadUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Better_Nike_Bot
{
	// Token: 0x0200000B RID: 11
	public static class CreateAccounts
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00008438 File Offset: 0x00006638
		public static List<Cookie> GetAllCookies(this CookieContainer cc)
		{
			List<Cookie> list = new List<Cookie>();
			Hashtable hashtable = (Hashtable)cc.GetType().InvokeMember("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField, null, cc, new object[0]);
			foreach (object obj in hashtable.Values)
			{
				SortedList sortedList = (SortedList)obj.GetType().InvokeMember("m_list", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField, null, obj, new object[0]);
				foreach (object obj2 in sortedList.Values)
				{
					CookieCollection cookieCollection = (CookieCollection)obj2;
					foreach (object obj3 in cookieCollection)
					{
						Cookie item = (Cookie)obj3;
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00008578 File Offset: 0x00006778
		public static void Create(CreateAccountsForm frm)
		{
			Logger.Log("Starting account creation...", true, true);
			bool disableMobile = frm.disableMobile;
			Account.NikeAccount[] array = new Account.NikeAccount[frm.count];
			try
			{
				string text = frm.email.Split(new char[]
				{
					'@'
				}).FirstOrDefault<string>();
				string text2 = "@" + frm.email.Split(new char[]
				{
					'@'
				}).LastOrDefault<string>();
				string text3 = frm.name.Split(new char[]
				{
					' '
				}).First<string>();
				string text4 = frm.name.Split(new char[]
				{
					' '
				}).Last<string>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new Account.NikeAccount
					{
						EmailAddress = string.Concat(new object[]
						{
							text,
							frm.disablePlusSign ? "" : "+",
							RandomHelper.RandomInt(10, 99),
							RandomHelper.RandomInt(10, 99),
							text2
						}),
						Password = frm.pass,
						FirstName = (frm.randomName ? Names.NextName : text3),
						LastName = (frm.randomName ? Names.NextSurname : text4),
						DateTime_0 = RandomHelper.RandomDate(new DateTime(1980, 1, 1), new DateTime(1994, 1, 1)),
						Id = i
					};
					array[i].ScreenName = string.Concat(new object[]
					{
						array[i].FirstName,
						array[i].LastName,
						RandomHelper.RandomInt(10, 99),
						RandomHelper.RandomInt(10, 99)
					});
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Error initializing account creation!. {0}".With(new object[]
				{
					ex.Message
				}), true, true);
				return;
			}
			ConcurrentList<Account.NikeAccount> successAccounts = new ConcurrentList<Account.NikeAccount>(null);
			string pid = "462";
			Http http = new Http();
			NameValueCollection nameValueCollection = QueryHelper.New();
			if (!disableMobile)
			{
				nameValueCollection["action"] = "login";
				nameValueCollection["token"] = frm.smsApiKey;
				nameValueCollection["username"] = frm.smsEmail;
				try
				{
					HttpResponse httpResponse = http.Post("http://www.getsmscode.com/do.php", null, nameValueCollection);
					if (!httpResponse.Text.Contains(frm.smsEmail))
					{
						throw new Exception(httpResponse.Text.Split(new char[]
						{
							'|'
						}).Last<string>());
					}
				}
				catch (Exception ex2)
				{
					Logger.Log("Error connecting to sms verifications service. " + ex2.Message, true, true);
					return;
				}
				try
				{
					nameValueCollection = QueryHelper.New();
					nameValueCollection["action"] = "mobilelist";
					nameValueCollection["token"] = frm.smsApiKey;
					nameValueCollection["username"] = frm.smsEmail;
					HttpResponse httpResponse2 = http.Post("http://www.getsmscode.com/do.php", null, nameValueCollection);
					IEnumerable<string> enumerable = from s in httpResponse2.Text.Split(new char[]
					{
						','
					})
					select s.Split(new char[]
					{
						'|'
					}).First<string>() into s
					where !s.IsNullOrWhiteSpace()
					select s;
					foreach (string value in enumerable)
					{
						try
						{
							nameValueCollection = QueryHelper.New();
							nameValueCollection["action"] = "addblack";
							nameValueCollection["mobile"] = value;
							nameValueCollection["pid"] = pid;
							nameValueCollection["token"] = frm.smsApiKey;
							nameValueCollection["username"] = frm.smsEmail;
							http.Post("http://www.getsmscode.com/do.php", null, nameValueCollection);
						}
						catch (Exception)
						{
						}
					}
				}
				catch (Exception)
				{
				}
			}
			int maxThreads = 1;
			new ThreadHelper<Account.NikeAccount>(array, delegate(Account.NikeAccount a)
			{
				string nextObject = Form1.ProxyEnumeration.GetNextObject();
				Http http = new Http
				{
					Proxy = nextObject
				};
				NameValueCollection nameValueCollection2 = QueryHelper.New();
				try
				{
					if (!CreateAccounts.CheckStatus() && !frm.isUltimate)
					{
						throw new Exception("Regular users can only create 5 accounts per day. Please check back tomorrow. http://www.betternikebot.com/important-update-regarding-nike-account-creator/");
					}
					Stopwatch stopwatch = Stopwatch.StartNew();
					bool flag = false;
					while (!flag)
					{
						if (stopwatch.Elapsed.TotalMinutes > 5.0)
						{
							IL_AF2:
							throw new Exception("Cannot get cookies. Please try again in an hour...");
						}
						Logger.Log("Account #" + a.Id + ": Waiting for cookies for account creation...", true, true);
						List<Cookie> cookies = LoginBrowser.GetCookies(http.Proxy);
						foreach (Cookie cookie in cookies)
						{
							try
							{
								if (cookie.Value.Contains(","))
								{
									cookie.Value = cookie.Value.UrlEncode(Encoding.UTF8);
								}
								flag = true;
								http.Cookies.Add(cookie);
							}
							catch (Exception)
							{
							}
						}
					}
					http.Cookies.GetAllCookies();
					if (!disableMobile)
					{
						try
						{
							Logger.Log("Account #" + a.Id + ": Getting mobile number...", true, true);
							nameValueCollection2 = QueryHelper.New();
							nameValueCollection2["action"] = "getmobile";
							nameValueCollection2["pid"] = pid;
							nameValueCollection2["token"] = frm.smsApiKey;
							nameValueCollection2["username"] = frm.smsEmail;
							HttpResponse httpResponse3 = http.Post("http://www.getsmscode.com/do.php", null, nameValueCollection2);
							if (!httpResponse3.Text.RegexIsMatch("[0-9]+"))
							{
								throw new Exception(httpResponse3.Text);
							}
							a.Phone = httpResponse3.Text.Substring(2);
						}
						catch (Exception ex3)
						{
							Logger.Log(string.Concat(new object[]
							{
								"Account #",
								a.Id,
								": Error getting mobile number. ",
								ex3.Message
							}), true, true);
							return;
						}
					}
					Logger.Log(string.Concat(new object[]
					{
						"Account #",
						a.Id,
						": Using ",
						a.Phone.IsNullOrWhiteSpace() ? "" : ("mobile number " + a.Phone + " and "),
						"email ",
						a.EmailAddress,
						" to signup..."
					}), true, true);
					WebUtilities.SetLocaleChinaCookies(http);
					string[] args = new string[]
					{
						a.EmailAddress,
						NikeUrls.NikeLangLocale,
						a.Phone,
						a.Password,
						a.FirstName,
						a.LastName,
						a.DateTime_0.ToString("yyyy-MM-dd"),
						NikeUrls.NikeCountryCode
					};
					string postData = "{{\"username\": \"{0}\", \"locale\": \"{1}\", \"receiveEmail\": true, \"registrationSiteId\": \"nikedotcom\", \"mobileNumber\": \"{2}\", \"account\": {{\"passwordSettings\": {{\"password\": \"{3}\", \"passwordConfirm\": \"{3}\"}}, \"email\": \"{0}\"}}, \"welcomeEmailTemplate\": \"TSD_PROF_COMM_WELCOME_V1.0\", \"firstName\": \"{4}\", \"gender\": \"male\", \"lastName\": \"{5}\", \"dateOfBirth\": \"{6}\", \"country\": \"{7}\"}}".With(args);
					JObject jobject = JObject.Parse("{\r\n  \"account\": {\r\n    \"email\": \"hdbdhdhhddhhe@gmail.com\",\r\n    \"passwordSettings\": {\r\n      \"password\": \"Welcome55\",\r\n      \"passwordConfirm\": \"Welcome55\"\r\n    }\r\n  },\r\n  \"locale\": \"en_US\",\r\n  \"welcomeEmailTemplate\": \"TSD_PROF_MS_WELC_T0_GENERIC_V1.0\",\r\n  \"registrationSiteId\": \"snkrsios\",\r\n  \"username\": \"hdbdhdhhddhhe@gmail.com\",\r\n  \"firstName\": \"Nasa\",\r\n  \"lastName\": \"Isro\",\r\n  \"dateOfBirth\": \"1989-04-04\",\r\n  \"country\": \"US\",\r\n  \"gender\": \"M\",\r\n  \"receiveEmail\": false\r\n}");
					jobject["username"] = (jobject["account"]["email"] = a.EmailAddress);
					jobject["account"]["passwordSettings"]["password"] = a.Password;
					jobject["account"]["passwordSettings"]["passwordConfirm"] = a.Password;
					jobject["locale"] = NikeUrls.NikeLangLocale;
					jobject["firstName"] = a.FirstName;
					jobject["lastName"] = a.LastName;
					jobject["dateOfBirth"] = a.DateTime_0.ToString("yyyy-MM-dd");
					jobject["country"] = NikeUrls.NikeCountryCode;
					postData = jobject.ToString(Formatting.None, new JsonConverter[0]);
					string url = "https://unite.nike.com/join?{0}&uxid=com.nike.commerce.snkrs.web&locale=zh_CN&backendEnvironment=identity&browser=Google%20Inc.&os=undefined&mobile=false&native=false&visit=1&visitor=".With(new object[]
					{
						WebSnkrs.VersionString
					}) + Guid.NewGuid().ToString();
					url = "https://s3.nikecdn.com/join?{0}&uxid=com.nike.commerce.snkrs.ios&locale={1}&backendEnvironment=identity&browser=Apple%20Computer%2C%20Inc.&os=undefined&mobile=true&native=true&visit=1&visitor={2}".With(new object[]
					{
						WebSnkrs.VersionString,
						NikeUrls.NikeLangLocale,
						Guid.NewGuid()
					}).ToString();
					try
					{
						http.Post(url, WebSnkrs.MobileLoginUrl, postData, "text/plain");
					}
					catch (WebException ex4)
					{
						string message = ex4.Message;
						try
						{
							WebResponse response = ex4.Response;
							using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
							{
								message = streamReader.ReadToEnd();
							}
						}
						catch (Exception)
						{
						}
						throw new Exception(message);
					}
					Logger.Log("Account #" + a.Id + ": Account created... Logging in now... ", true, true);
					string str = "";
					try
					{
						str = WebSnkrs.PerformLogin(new Account(a.EmailAddress, a.Password, "RANDOM", "", "", "", "", "", "", "", null, false)
						{
							Http = new Http(http)
						});
					}
					catch (WebException ex5)
					{
						string message2 = ex5.Message;
						try
						{
							WebResponse response2 = ex5.Response;
							using (StreamReader streamReader2 = new StreamReader(response2.GetResponseStream()))
							{
								message2 = streamReader2.ReadToEnd();
							}
						}
						catch (Exception)
						{
						}
						throw new Exception(message2);
					}
					if (disableMobile)
					{
						Logger.Log("Account #" + a.Id + ": New account created! {0}\t{1}".With(new object[]
						{
							a.EmailAddress,
							a.Password
						}), true, true);
						successAccounts.Add(a);
						CreateAccounts.ReportSuccess();
					}
					else
					{
						Logger.Log("Account #" + a.Id + ": Submitting phone number... ", true, true);
						try
						{
							url = "https://idn.nike.com/idn/phone/+86" + a.Phone;
							http.Request("PUT", url, "https://www.nike.com/cn/zh_cn/p/settings", "", null, new string[]
							{
								"Origin: https://www.nike.com",
								"Content-Locale: zh_CN",
								"Authorization: Bearer " + str
							}, true, http.TimeOut);
						}
						catch (WebException ex6)
						{
							string message3 = ex6.Message;
							try
							{
								WebResponse response3 = ex6.Response;
								using (StreamReader streamReader3 = new StreamReader(response3.GetResponseStream()))
								{
									message3 = streamReader3.ReadToEnd();
								}
							}
							catch (Exception)
							{
							}
							throw new Exception(message3);
						}
						Logger.Log("Account #" + a.Id + ": Waiting for sms... ", true, true);
						Stopwatch stopwatch2 = Stopwatch.StartNew();
						string text5 = "";
						while (stopwatch2.Elapsed.TotalSeconds <= 60.0)
						{
							Thread.Sleep(7000);
							try
							{
								Logger.Log("Account #" + a.Id + ": Getting list of sms...", true, true);
								nameValueCollection2 = QueryHelper.New();
								nameValueCollection2["action"] = "getsms";
								nameValueCollection2["mobile"] = "86" + a.Phone;
								nameValueCollection2["pid"] = pid;
								nameValueCollection2["token"] = frm.smsApiKey;
								nameValueCollection2["username"] = frm.smsEmail;
								HttpResponse httpResponse4 = http.Post("http://www.getsmscode.com/do.php", null, nameValueCollection2);
								if (httpResponse4.Text.Contains("NIKE"))
								{
									text5 = httpResponse4.Text.RegexMatch("[0-9]{4,}").Value;
									break;
								}
								Logger.Log(string.Concat(new object[]
								{
									"Account #",
									a.Id,
									": ",
									httpResponse4
								}), true, true);
							}
							catch (Exception ex7)
							{
								Logger.Log(string.Concat(new object[]
								{
									"Account #",
									a.Id,
									": Error getting list of sms... ",
									ex7.Message
								}), true, true);
							}
						}
						if (text5.IsNullOrWhiteSpace())
						{
							try
							{
								nameValueCollection2 = QueryHelper.New();
								nameValueCollection2["action"] = "addblack";
								nameValueCollection2["mobile"] = "86" + a.Phone;
								nameValueCollection2["pid"] = pid;
								nameValueCollection2["token"] = frm.smsApiKey;
								nameValueCollection2["username"] = frm.smsEmail;
								http.Post("http://www.getsmscode.com/do.php", null, nameValueCollection2);
							}
							catch (Exception)
							{
							}
							throw new Exception("Did not receive sms in time...");
						}
						Logger.Log(string.Concat(new object[]
						{
							"Account #",
							a.Id,
							": Submitting verification code ",
							text5
						}), true, true);
						try
						{
							url = "https://idn.nike.com/idn/phone/" + text5;
							http.Post(url, null, "", null, new string[]
							{
								"Authorization: Bearer " + str
							});
							Logger.Log("Account #" + a.Id + ": New account created! {0}\t{1}".With(new object[]
							{
								a.EmailAddress,
								a.Password
							}), true, true);
							successAccounts.Add(a);
							CreateAccounts.ReportSuccess();
						}
						catch (WebException ex8)
						{
							string message4 = ex8.Message;
							try
							{
								WebResponse response4 = ex8.Response;
								using (StreamReader streamReader4 = new StreamReader(response4.GetResponseStream()))
								{
									message4 = streamReader4.ReadToEnd();
								}
							}
							catch (Exception)
							{
							}
							throw new Exception(message4);
						}
						try
						{
							nameValueCollection2 = QueryHelper.New();
							nameValueCollection2["action"] = "addblack";
							nameValueCollection2["mobile"] = "86" + a.Phone;
							nameValueCollection2["pid"] = pid;
							nameValueCollection2["token"] = frm.smsApiKey;
							nameValueCollection2["username"] = frm.smsEmail;
							http.Post("http://www.getsmscode.com/do.php", null, nameValueCollection2);
							goto IL_AFD;
						}
						catch (Exception)
						{
							goto IL_AFD;
						}
						goto IL_AF2;
						IL_AFD:;
					}
				}
				catch (Exception ex9)
				{
					Logger.Log("Account #" + a.Id + ": Account creation failed! Email: {1} {0}".With(new object[]
					{
						ex9.Message,
						a.EmailAddress
					}), true, true);
				}
			}, maxThreads).Start();
			if (successAccounts.List.IsAny<Account.NikeAccount>())
			{
				Logger.Log("Account creation finished... created {0} accounts successfuly...".With(new object[]
				{
					successAccounts.Count
				}), true, true);
				Logger.Log("Created accounts have been added above. Please export the accounts if you wish to.".With(new object[]
				{
					successAccounts.Count
				}), true, true);
				Form1.DefaultForm.InvokeAction(delegate
				{
					Form1.DefaultForm.ImportAccounts((from a in successAccounts.List
					select "{0}\t{1}\tRANDOM".With(new object[]
					{
						a.EmailAddress,
						a.Password
					})).ToArray<string>());
				});
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00008B8C File Offset: 0x00006D8C
		public static void ReportSuccess()
		{
			Http http = new Http();
			NameValueCollection nameValueCollection = QueryHelper.New();
			nameValueCollection.Add("serial_code", Form1.SerialCode);
			http.Post("http://api.betternikebot.com/v1/acc1", null, nameValueCollection);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00008BC4 File Offset: 0x00006DC4
		public static bool CheckStatus()
		{
			Http http = new Http();
			NameValueCollection nameValueCollection = QueryHelper.New();
			nameValueCollection.Add("serial_code", Form1.SerialCode);
			HttpResponse httpResponse = http.Post("http://api.betternikebot.com/v1/acc2", null, nameValueCollection);
			return httpResponse.Text == "2589597838b8b4e9a4b99ba59dc4bd3a";
		}
	}
}
