using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Better_Nike_Bot.Browser;
using Better_Nike_Bot.Properties;
using BrightIdeasSoftware;
using ComponentOwl.BetterSplitButton;
using CsQuery.ExtensionMethods.Internal;
using DotNetBrowser;
using IMLokesh.EnumerationUtility;
using IMLokesh.Extensions;
using IMLokesh.FileUtility;
using IMLokesh.FormUtility;
using IMLokesh.Http;
using IMLokesh.HttpUtility;
using IMLokesh.RandomUtility;
using IMLokesh.ThreadUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using wyDay.Controls;
using wyDay.TurboActivate;

namespace Better_Nike_Bot
{
	// Token: 0x0200002C RID: 44
	public partial class Form1 : Form
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x000221BC File Offset: 0x000203BC
		public Form1()
		{
			this._ndcAccounts = new List<Account>();
			this._processedTweets = new HashSet<long>();
			this._shouldStop = new List<ShouldStop>();
			this._scheduleWait = new ManualResetEvent(false);
			this._showAll = true;
			this.InitializeComponent();
			base.CenterToParent();
			this.LoadSettings();
			Form1.DefaultForm = this;
			this.textBoxMonitorAccount.Text = this._monitorAccount;
			this.textBoxTwitterPassword.Text = this._twitterPassword;
			this.textBoxUsername.Text = this._twitterUsername;
			this.labelAccountCount.Text = this._ndcAccounts.Count.ToString();
			this.checkBoxDisableTwitter.Checked = this.DisableTwitter;
			ServicePointManager.DefaultConnectionLimit = 10000;
			BrowserPreferences.SetChromiumSwitches(new string[]
			{
				"--disable-web-security",
				"--ignore-certificate-errors"
			});
			BrowserPreferences.SetUserAgent("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36");
			try
			{
				Form1.BrowserDataDir = Path.Combine(FileHelper.CurrentDirectory(), "browser_data");
				if (!Directory.Exists(Form1.BrowserDataDir))
				{
					Directory.CreateDirectory(Form1.BrowserDataDir);
				}
				new DirectoryInfo(Form1.BrowserDataDir).Empty();
			}
			catch (Exception)
			{
			}
			ServicePointManager.ServerCertificateValidationCallback = ((object param0, X509Certificate param1, X509Chain param2, SslPolicyErrors param3) => true);
			ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
			if (Form1.Abc != "cba")
			{
				Environment.Exit(0);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00022438 File Offset: 0x00020638
		private void LoadSettings()
		{
			try
			{
				if (!Settings.Default.TwitterUsername.IsNullOrWhiteSpace())
				{
					this._twitterUsername = Settings.Default.TwitterUsername;
				}
				if (!Settings.Default.TwitterPassword.IsNullOrWhiteSpace())
				{
					this._twitterPassword = Settings.Default.TwitterPassword;
				}
				if (!Settings.Default.MonitorUser.IsNullOrWhiteSpace())
				{
					this._monitorAccount = Settings.Default.MonitorUser;
				}
				if (!Settings.Default.NdcAccounts.IsNullOrWhiteSpace())
				{
					this._ndcAccounts = JsonConvert.DeserializeObject<List<Account>>(Settings.Default.NdcAccounts);
				}
				if (!Settings.Default.Proxies.IsNullOrWhiteSpace())
				{
					Form1.Proxies = JsonConvert.DeserializeObject<List<string>>(Settings.Default.Proxies);
				}
				if (!Settings.Default.MonitorLinkCache.IsNullOrWhiteSpace())
				{
					Form1.MonitorLinkCache = JsonConvert.DeserializeObject<List<MonitorLink>>(Settings.Default.MonitorLinkCache);
				}
				if (!Settings.Default.CreditCardProfiles.IsNullOrWhiteSpace())
				{
					Form1.CreditCardProfiles = JsonConvert.DeserializeObject<List<CreditCardProfile>>(Settings.Default.CreditCardProfiles);
				}
				if (!Settings.Default.Tokens.IsNullOrWhiteSpace())
				{
					Form1.Tokens = JsonConvert.DeserializeObject<Dictionary<string, NikeToken>>(Settings.Default.Tokens);
				}
				Form1.GsSize = Settings.Default.GsSize;
				Form1.AllSize = Settings.Default.AllSize;
				Form1.SiteType = (SiteType)Settings.Default.SiteType;
				Form1.SendNotifications = Settings.Default.SendNotifications;
				Form1.SendPassword = Settings.Default.SendPassword;
				Form1.VerboseLogging = Settings.Default.VerboseLogging;
				MonitorLink.IntervalStart = Settings.Default.IntervalStart;
				MonitorLink.IntervalStop = Settings.Default.IntervalEnd;
				this.DisableTwitter = Settings.Default.DisableTwitter;
				NotificationSettings.LoadSettings();
			}
			catch (Exception)
			{
				Settings.Default.Reset();
				Settings.Default.Save();
				this.LoadSettings();
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0002262C File Offset: 0x0002082C
		public void SaveSettings()
		{
			Settings.Default.TwitterUsername = this._twitterUsername;
			Settings.Default.TwitterPassword = this._twitterPassword;
			Settings.Default.MonitorUser = this._monitorAccount;
			Settings.Default.NdcAccounts = JsonConvert.SerializeObject(this._ndcAccounts);
			Settings.Default.Proxies = JsonConvert.SerializeObject(Form1.Proxies);
			Settings.Default.CreditCardProfiles = JsonConvert.SerializeObject(Form1.CreditCardProfiles);
			Settings.Default.MonitorLinkCache = JsonConvert.SerializeObject(Form1.MonitorLinkCache);
			Settings.Default.GsSize = Form1.GsSize;
			Settings.Default.AllSize = Form1.AllSize;
			Settings.Default.SiteType = Form1.SiteType.GetValue();
			Settings.Default.VerboseLogging = Form1.VerboseLogging;
			Settings.Default.SendNotifications = Form1.SendNotifications;
			Settings.Default.SendPassword = Form1.SendPassword;
			Settings.Default.IntervalStart = MonitorLink.IntervalStart;
			Settings.Default.IntervalEnd = MonitorLink.IntervalStop;
			Settings.Default.DisableTwitter = this.DisableTwitter;
			this.labelAccountCount.Text = this._ndcAccounts.Count.ToString();
			Settings.Default.Tokens = JsonConvert.SerializeObject(Form1.Tokens);
			Settings.Default.Save();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00022788 File Offset: 0x00020988
		private void buttonStart_Click(object sender, EventArgs e)
		{
			Form1.ShouldStop = false;
			if (this._ndcAccounts.Any<Account>())
			{
				if (!this._ndcAccounts.All((Account a) => a.Disabled))
				{
					if ((this._monitorAccount.IsNullOrWhiteSpace() || this._twitterPassword.IsNullOrWhiteSpace() || this._twitterUsername.IsNullOrWhiteSpace()) && !this.DisableTwitter)
					{
						"Please fill in twitter settings or disable twitter.".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
						return;
					}
					this.SaveSettings();
					try
					{
						Dictionary<string, string> dictionary = new Dictionary<string, string>();
						dictionary["serial_code"] = Form1.SerialCode;
						dictionary["product_name"] = "aio";
						dictionary["product_version"] = Form1.Version;
						HttpHelper.PostUrl("http://api.betternikebot.com/v1/verify", dictionary, null, 10, "", "application/x-www-form-urlencoded", null, true);
					}
					catch (WebException ex)
					{
						string str = "";
						if (ex.Message.Contains("401"))
						{
							str = "Please install the software with a valid serial code.";
						}
						(ex.Message + " " + str).Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
						Application.Exit();
					}
					Form1.InitializeProxies();
					this.EnableControls(false);
					try
					{
						this.SaveSettings();
						MonitorLink.ProcessedLinks = new List<KeyValuePair<Account, string>>();
						Form1.MonitorLinks = new List<MonitorLink>();
						new Thread(new ThreadStart(this.StartMonitor))
						{
							IsBackground = true
						}.Start();
					}
					catch (Exception ex2)
					{
						Logger.Log("ERROR!: " + ex2.Message, true, true);
						this.EnableControls(true);
					}
					return;
				}
			}
			"Add at least one account.".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00022954 File Offset: 0x00020B54
		private static void InitializeProxies()
		{
			Form1.ProxyEnumeration = (Form1.Proxies.IsAny<string>() ? new EnumerationHelper<string>(Form1.Proxies, true) : new EnumerationHelper<string>(new string[]
			{
				""
			}, true));
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00022998 File Offset: 0x00020B98
		private void StartMonitor()
		{
			Logger.Log("Waiting for all accounts to log in. ", true, true);
			foreach (Account account in this._ndcAccounts)
			{
				account.Request = new HttpHelper("", true, null, 15, null);
				account.Http = new Http
				{
					SwallowWebExceptions = true
				};
				string proxyAddress = Form1.Proxies.IsAny<string>() ? Form1.ProxyEnumeration.GetNextObject() : "";
				account.Request.Proxy = (account.Http.Proxy = (account.ProxyAddress = proxyAddress));
			}
			this.InvokeAction(new Action(this.RefreshTable));
			foreach (Account account2 in this._ndcAccounts)
			{
				account2.IsLoggedIn = false;
			}
			foreach (CreditCardProfile creditCardProfile in Form1.CreditCardProfiles)
			{
				creditCardProfile.CheckoutCount = 0;
				creditCardProfile.Lock = new object();
			}
			new ThreadHelper<Account>(from acc in this._ndcAccounts
			where !acc.Disabled && !acc.IsWebSnkrs
			select acc, delegate(Account acc)
			{
				acc.Login(0, Form1.SkipFailedLogins ? 3 : 0);
			}, 10).Start();
			new ThreadHelper<Account>(from acc in this._ndcAccounts
			where !acc.Disabled && acc.IsWebSnkrs
			select acc, delegate(Account acc)
			{
				acc.SnkrsLogin(0, Form1.SkipFailedLogins ? 3 : 0);
			}, 10).Start();
			if (Form1.ShouldStop)
			{
				return;
			}
			IEnumerable<Account> source = from acc in this._ndcAccounts
			where acc.IsLoggedIn && !acc.Disabled
			select acc;
			Logger.Log("All ndc accounts are now logged in.", true, true);
			using (IEnumerator<string> enumerator4 = source.SelectMany((Account s) => s.EarlyLinks).Distinct<string>().GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					string link = enumerator4.Current;
					Form1.MonitorLinks.Add(new MonitorLink(link, from a in source
					where a.EarlyLinks.Contains(link) && !a.IsWebSnkrs
					select a));
				}
			}
			ShouldStop shouldStop = new ShouldStop();
			this._shouldStop.Add(shouldStop);
			using (IEnumerator<string> enumerator5 = source.SelectMany((Account s) => s.ProductStyleCodes).Distinct<string>().GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					string style = enumerator5.Current;
					IEnumerable<Account> enumerable = from a in source
					where a.ProductStyleCodes.Contains(style) && a.IsWebSnkrs
					select a;
					foreach (Account account3 in enumerable)
					{
						string[] size = account3.Size;
						for (int i = 0; i < size.Length; i++)
						{
							string text = size[i];
							Account acc1 = account3;
							string s1 = text;
							new Thread(delegate()
							{
								new WebSnkrs(new AtcItem(acc1, "", s1)
								{
									Details = 
									{
										StyleCode = style
									},
									ShouldStop = shouldStop
								}).Atc();
							}).Start();
							Logger.Log("{0} (Size {2}): Processing style code {1}".With(new object[]
							{
								account3.EmailAddress,
								style,
								text
							}), true, true);
						}
					}
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00022E7C File Offset: 0x0002107C
		private void Form1_Load(object sender, EventArgs e)
		{
			this.checkForUpdatesToolStripMenuItem.PerformClick();
			this.textBoxTwitterPassword.PasswordChar = '•';
			if (Form1.AllSize)
			{
				this.comboBoxSizeType.SelectedIndex = 2;
			}
			else if (Form1.GsSize)
			{
				this.comboBoxSizeType.SelectedIndex = 1;
			}
			else
			{
				this.comboBoxSizeType.SelectedIndex = 0;
			}
			this.comboBoxSiteType.SelectedIndex = Form1.SiteType.GetValue();
			this.checkBoxNotifications.Checked = Form1.SendNotifications;
			this.checkBoxSendPassword.Checked = Form1.SendPassword;
			Logger.Log("Application loaded.", true, true);
			try
			{
				this.CallHome();
			}
			catch (Exception)
			{
				"Error getting license information. ".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				Application.Exit();
			}
			this.objectListView1.FullRowSelect = true;
			this.objectListView1.HideSelection = false;
			this.olvColumn3.AspectToStringConverter = ((object value) => ((string[])value).JoinToString(","));
			this.olvColumn5.AspectToStringConverter = ((object value) => ((List<string>)value).JoinToString(","));
			this.olvColumn9.AspectToStringConverter = ((object value) => ((List<string>)value).JoinToString(","));
			this.olvColumn2.IsVisible = false;
			this.objectListView1.SetObjects(this._ndcAccounts);
			this.objectListView1.SmallImageList = null;
			this.RefreshTable();
			this.newToolStripMenuItem.Click += this.getSKUsToolStripMenuItem_Click;
			this.oldToolStripMenuItem.Click += this.getSKUsToolStripMenuItem_Click;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00002FFF File Offset: 0x000011FF
		private void CallHome()
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00023040 File Offset: 0x00021240
		private void RefreshTable()
		{
			try
			{
				this.objectListView1.SetObjects(from acc in this._ndcAccounts
				where this._showAll || !acc.Disabled
				select acc);
				this.objectListView1.BuildList();
				this.olvColumn1.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
				this.olvColumn2.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
				this.olvColumn2.IsVisible = false;
				this.objectListView1.RebuildColumns();
				this.SaveSettings();
			}
			catch (Exception ex)
			{
				Logger.Log("Error updating accounts view... {0}".With(new object[]
				{
					ex
				}), true, true);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000230E8 File Offset: 0x000212E8
		private void buttonAddAccount_Click(object sender, EventArgs e)
		{
			using (AccountDetailsForm accountDetailsForm = new AccountDetailsForm(null, "", ""))
			{
				DialogResult dialogResult = accountDetailsForm.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					this._ndcAccounts.Add(accountDetailsForm.Account);
					this.RefreshTable();
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00023144 File Offset: 0x00021344
		private void buttonRemove_Click(object sender, EventArgs e)
		{
			IList selectedObjects = this.objectListView1.SelectedObjects;
			if (selectedObjects.Count == 0)
			{
				Button window = (Button)sender;
				ToolTip toolTip = new ToolTip();
				toolTip.Show("Please select the accounts you want to remove.", window, 0, 25, 3000);
				return;
			}
			foreach (object obj in selectedObjects)
			{
				this._ndcAccounts.Remove((Account)obj);
			}
			this.RefreshTable();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000231E4 File Offset: 0x000213E4
		private void buttonEditAccount_Click(object sender, EventArgs e)
		{
			Account account = (this.objectListView1.SelectedObjects.Count > 0) ? ((Account)this.objectListView1.SelectedObjects[0]) : null;
			if (account == null)
			{
				Button window = (Button)sender;
				ToolTip toolTip = new ToolTip();
				toolTip.Show("Please select an account to edit.", window, 0, 25, 3000);
				return;
			}
			using (AccountDetailsForm accountDetailsForm = new AccountDetailsForm(account, "", ""))
			{
				DialogResult dialogResult = accountDetailsForm.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					account.UpdateDetails(accountDetailsForm.Account);
					this.RefreshTable();
				}
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00003001 File Offset: 0x00001201
		private void textBoxUsername_TextChanged(object sender, EventArgs e)
		{
			this._twitterUsername = ((TextBox)sender).Text.Trim();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00003019 File Offset: 0x00001219
		private void textBoxMonitorAccount_TextChanged(object sender, EventArgs e)
		{
			this._monitorAccount = ((TextBox)sender).Text.Trim();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00003031 File Offset: 0x00001231
		private void textBoxTwitterPassword_TextChanged(object sender, EventArgs e)
		{
			this._twitterPassword = ((TextBox)sender).Text.Trim();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00023290 File Offset: 0x00021490
		private void buttonClone_Click(object sender, EventArgs e)
		{
			IList selectedObjects = this.objectListView1.SelectedObjects;
			if (selectedObjects.Count == 0)
			{
				Button window = (Button)sender;
				ToolTip toolTip = new ToolTip();
				toolTip.Show("Please select the accounts you want to clone.", window, 0, 25, 3000);
				return;
			}
			foreach (object obj in selectedObjects)
			{
				this._ndcAccounts.Add(new Account((Account)obj));
			}
			this.RefreshTable();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00003049 File Offset: 0x00001249
		private void objectListView1_DoubleClick(object sender, EventArgs e)
		{
			this.buttonEditAccount.PerformClick();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00023334 File Offset: 0x00021534
		private void buttonStop_Click(object sender, EventArgs e)
		{
			this.EnableControls(true);
			Form1.ShouldStop = true;
			foreach (ShouldStop shouldStop in this._shouldStop)
			{
				shouldStop.Value = true;
			}
			Logger.Log("Stopped by user.", true, true);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000233A4 File Offset: 0x000215A4
		private void EnableControls(bool enabled = true)
		{
			List<Control> list = new List<Control>();
			list.AddRange(this.GetAllChildren(typeof(TextBox)));
			list.AddRange(this.GetAllChildren(typeof(ComboBox)));
			list.AddRange(this.GetAllChildren(typeof(Button)));
			list.AddRange(this.GetAllChildren(typeof(BetterSplitButton)));
			list.AddRange(this.GetAllChildren(typeof(CheckBox)));
			foreach (Control control in list)
			{
				if (!control.Name.EqualsAny(new string[]
				{
					"checkBoxVerboseLogging",
					"buttonTestCaptcha",
					"textBoxLog",
					"checkBoxNotifications",
					"checkBoxAutoScrollLog",
					"checkBoxSoldOutRetry",
					"buttonPauseLog",
					"buttonClearLog"
				}))
				{
					if (control.Name.EqualsAny(new string[]
					{
						"buttonStop"
					}))
					{
						control.Enabled = !enabled;
					}
					else
					{
						control.Enabled = enabled;
					}
				}
			}
			if (enabled)
			{
				this.checkBoxDisableTwitter_CheckedChanged(null, null);
			}
			this.deactivateToolStripMenuItem.Enabled = enabled;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00023508 File Offset: 0x00021708
		private void comboBoxSizeType_SelectedIndexChanged(object sender, EventArgs e)
		{
			Form1.GsSize = (this.comboBoxSizeType.SelectedIndex == 1);
			Form1.AllSize = (this.comboBoxSizeType.SelectedIndex == 2);
			Form1.MatchStrings = (Form1.GsSize ? new string[]
			{
				"Boys'",
				"Kids'"
			} : new string[]
			{
				"Men's"
			});
			this.SaveSettings();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00023578 File Offset: 0x00021778
		private void textBoxIntervalStart_Enter(object sender, EventArgs e)
		{
			TextBox window = (TextBox)sender;
			ToolTip toolTip = new ToolTip();
			toolTip.Show("The bot will wait random amount of ms within this range between requests.", window, 0, 22, 3000);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00003056 File Offset: 0x00001256
		private void comboBoxSiteType_SelectedIndexChanged(object sender, EventArgs e)
		{
			Form1.SiteType = (SiteType)this.comboBoxSiteType.SelectedIndex;
			this.SaveSettings();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000306E File Offset: 0x0000126E
		private void objectListView1_CellRightClick(object sender, CellRightClickEventArgs e)
		{
			if (e.RowIndex >= 0 && this.objectListView1.SelectedObjects.Count != 0)
			{
				e.MenuStrip = this.contextMenuStrip1;
				return;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000235A8 File Offset: 0x000217A8
		private void checkCartToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IEnumerable<Account> accs = this.objectListView1.SelectedObjects.Cast<Account>();
			Task.Factory.StartNew(delegate()
			{
				foreach (Account account in accs)
				{
					try
					{
						if (!this.IsRunning)
						{
							account.Login(0, 0);
						}
						FormHelper.OutputHelper(true, "{0}: Cart Items".With(new object[]
						{
							account.EmailAddress
						}), "Cart Items:", account.CheckCart(), 500, 0);
					}
					catch (Exception ex)
					{
						Logger.Log("{0}: Error checking cart. ".With(new object[]
						{
							account.EmailAddress,
							ex.Message
						}), true, true);
					}
				}
			});
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000235F0 File Offset: 0x000217F0
		private bool IsRunning
		{
			get
			{
				bool x = false;
				this.buttonStop.InvokeAction(delegate
				{
					x = this.buttonStop.Enabled;
				});
				return x;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00003098 File Offset: 0x00001298
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000309F File Offset: 0x0000129F
		public static bool UltimateVersion { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000030A7 File Offset: 0x000012A7
		// (set) Token: 0x0600020D RID: 525 RVA: 0x000030AE File Offset: 0x000012AE
		public static string SerialCode { get; set; }

		// Token: 0x0600020E RID: 526 RVA: 0x00023630 File Offset: 0x00021830
		private void openInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Account account = (Account)this.objectListView1.SelectedObjects[0];
			Account account2 = new Account(account);
			account2.Request.ClearCookies();
			account.LoginForBrowser();
			new Better_Nike_Bot.Browser.Browser
			{
				Acc = account
			}.Show();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00023680 File Offset: 0x00021880
		private void clearCartToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = "Warning! This will remove all items from cart. Do you want to continue?".Show(MessageBoxIcon.Asterisk, "Warning!", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.No)
			{
				return;
			}
			IEnumerable<Account> accs = this.objectListView1.SelectedObjects.Cast<Account>();
			Task.Factory.StartNew(delegate()
			{
				foreach (Account account in accs)
				{
					try
					{
						if (!this.IsRunning)
						{
							account.Login(0, 0);
						}
						account.ClearCart();
					}
					catch (Exception ex)
					{
						Logger.Log("{0}: Error clearing cart. ".With(new object[]
						{
							account.EmailAddress,
							ex.Message
						}), true, true);
					}
				}
			});
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000030B6 File Offset: 0x000012B6
		private void checkBoxNotifications_CheckedChanged(object sender, EventArgs e)
		{
			Form1.SendNotifications = this.checkBoxNotifications.Checked;
			this.SaveSettings();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000030CE File Offset: 0x000012CE
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Form1.AddedProductsForm.Show();
			Form1.AddedProductsForm.BringToFront();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000236E0 File Offset: 0x000218E0
		private void buttonPasteAccounts_Click(object sender, EventArgs e)
		{
			string[] linesArray = Clipboard.GetText().GetLinesArray(true);
			string[] array = linesArray;
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				List<string> list = text.Split(new char[]
				{
					'\t'
				}).ToList<string>();
				if (list.Count < 3)
				{
					goto IL_FC;
				}
				if (list.Count > 11)
				{
					goto IL_FC;
				}
				while (list.Count != 11)
				{
					list.Add("");
				}
				try
				{
					this._ndcAccounts.Add(new Account(list[0], list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9], null, false));
					goto IL_129;
				}
				catch (Exception ex)
				{
					"Error adding account.\r\n{0}".With(new object[]
					{
						ex.Message
					}).Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
					goto IL_129;
				}
				goto IL_FC;
				IL_129:
				i++;
				continue;
				IL_FC:
				DialogResult dialogResult = "Account is not in correct format.\r\n\r\n{0}\r\n\r\nClick Cancel to cancel the import or OK to continue.".With(new object[]
				{
					text
				}).Show(MessageBoxIcon.Hand, "Error", MessageBoxButtons.OKCancel);
				if (dialogResult != DialogResult.OK)
				{
					return;
				}
				goto IL_129;
			}
			this.RefreshTable();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0002383C File Offset: 0x00021A3C
		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string text = "";
			try
			{
				Form1.UltimateVersion = HttpHelper.PostUrl("http://www.betternikebot.com/bnb/isUltimate.php", "pkey={0}".With(new object[]
				{
					Form1.SerialCode
				}), null, 10, "", "application/x-www-form-urlencoded", null, true).ParseToBool();
			}
			catch (Exception ex)
			{
				text = "Error: {0}".With(new object[]
				{
					ex.Message
				});
			}
			if (!Form1.UltimateVersion)
			{
				"Notification settings are only available in the ultimate version. {0}\r\n\r\nVisit BetterNikeBot.com for more information.\r\n\r\n If you recently upgraded to ultimate please allow a few minutes for changes. ".With(new object[]
				{
					text
				}).Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
			new AdvancedSettingsForm().ShowDialog();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000238F8 File Offset: 0x00021AF8
		private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DialogResult dialogResult = "Are you sure?".Show(MessageBoxIcon.Exclamation, "", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.No)
			{
				return;
			}
			Settings.Default.Reset();
			Settings.Default.Save();
			"Settings reset. Please restart bot for changes to take effect.".Show(MessageBoxIcon.Asterisk, "", MessageBoxButtons.OK);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00023944 File Offset: 0x00021B44
		private void checkBoxDisableTwitter_CheckedChanged(object sender, EventArgs e)
		{
			this.DisableTwitter = this.checkBoxDisableTwitter.Checked;
			this.textBoxMonitorAccount.Enabled = !this.checkBoxDisableTwitter.Checked;
			this.textBoxUsername.Enabled = !this.checkBoxDisableTwitter.Checked;
			this.textBoxTwitterPassword.Enabled = !this.checkBoxDisableTwitter.Checked;
			this.SaveSettings();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000030E4 File Offset: 0x000012E4
		private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (this.buttonStart.Enabled)
			{
				"You can only set link booster settings when the bot is running! Click START.".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return;
			}
			new LinkScheduler().ShowDialog();
			this.SaveSettings();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00003118 File Offset: 0x00001318
		private void checkBoxOldMethod_CheckedChanged(object sender, EventArgs e)
		{
			Form1.UseOldMethod = this.checkBoxOldMethod.Checked;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000239B4 File Offset: 0x00021BB4
		private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			bool checkBoxChecked = Form1.Proxies.IsAny<string>() && !Form1.Proxies.Contains("");
			using (DataForm1 dataForm = new DataForm1(true, "Add or Edit Proxies", "Please enter proxies one per line below. Proxies will be automatically alloted to accounts.", Form1.Proxies.IsAny<string>() ? Form1.Proxies.JoinToString("\r\n").GetLinesArray(true).JoinToString("\r\n") : "ip:port\r\nip:port\r\nip:port:username:password", 500, 300, "Do not allot real ip to any account", checkBoxChecked, "Test proxies before adding"))
			{
				DialogResult dialogResult = dataForm.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					Form1.Proxies = new List<string>();
					if (!dataForm.TextBoxText.IsNullOrWhiteSpace())
					{
						if (!dataForm.CheckBoxChecked)
						{
							Form1.Proxies.Add("");
						}
						Form1.Proxies.AddRange(dataForm.TextBoxText.GetLinesArray(true).Distinct<string>());
						if (!dataForm.CheckBoxChecked2)
						{
							this.SaveSettings();
							Logger.Log("Proxy settings saved.", true, true);
						}
						else if (dataForm.CheckSnkrs)
						{
							Task.Factory.StartNew(delegate()
							{
								Logger.Log("Checking proxies... please wait...", true, true);
								Dictionary<string, string> dictionary = new Dictionary<string, string>();
								foreach (string text in Form1.Proxies)
								{
									HttpHelper httpHelper = new HttpHelper(text, true, null, 20, null);
									httpHelper.Cookies.SetCookies(new Uri("http://www.nike.com"), "nike_locale={0}/{1}".With(new object[]
									{
										NikeUrls.NikeCountrySmallCode,
										NikeUrls.NikeLangLocale
									}));
									httpHelper.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "nike_locale={0}/{1}".With(new object[]
									{
										NikeUrls.NikeCountrySmallCode,
										NikeUrls.NikeLangLocale
									}));
									httpHelper.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "NIKE_COMMERCE_COUNTRY={0}".With(new object[]
									{
										NikeUrls.NikeCountryCode
									}));
									httpHelper.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "NIKE_COMMERCE_LANG_LOCALE={0}".With(new object[]
									{
										NikeUrls.NikeLangLocale
									}));
									httpHelper.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "CONSUMERCHOICE={0}/{1}".With(new object[]
									{
										NikeUrls.NikeCountrySmallCode,
										NikeUrls.NikeLangLocale
									}));
									httpHelper.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0\r\nMozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36\r\nMozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36\r\nMozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.71 Safari/537.36\r\nMozilla/5.0 (Macintosh; Intel Mac OS X 10_11) AppleWebKit/601.1.56 (KHTML, like Gecko) Version/9.0 Safari/601.1.56\r\nMozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36\r\nMozilla/5.0 (Macintosh; Intel Mac OS X 10_11_1) AppleWebKit/601.2.7 (KHTML, like Gecko) Version/9.0.1 Safari/601.2.7\r\nMozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko".GetLinesArray(true).GetRandom<string>();
									try
									{
										httpHelper.GetRequest("https://api.nike.com/commerce/productfeed/products/snkrs/A/thread?country=US&locale=en_US&withCards=true", null, null, true, true);
										dictionary.Add(text, "403 (Forbidden)");
									}
									catch (Exception ex)
									{
										dictionary.Add(text, ex.Message);
									}
								}
								if (dictionary.Count((KeyValuePair<string, string> p) => p.Value.Contains("400")) == Form1.Proxies.Count)
								{
									Logger.Log("All proxies are working correctly!", true, true);
									return;
								}
								string s = "Following proxies had errors: \r\n{0}";
								object[] array = new object[1];
								array[0] = (from p in dictionary
								where !p.Value.Contains("400")
								select "Proxy: {0} Error: {1}".With(new object[]
								{
									p.Key,
									p.Value
								})).JoinToString("\r\n");
								string msg = s.With(array);
								Logger.Log(msg, true, true);
								DialogResult dialogResult2 = "Some proxies had errors. Press OK to remove those proxies. Press cancel to keep them anyways. ".Show(MessageBoxIcon.Hand, "Proxies", MessageBoxButtons.OKCancel);
								if (dialogResult2 == DialogResult.OK)
								{
									Form1.Proxies = (from p in dictionary
									where p.Value.Contains("400")
									select p.Key).ToList<string>();
									this.SaveSettings();
								}
							});
						}
						else
						{
							Task.Factory.StartNew(delegate()
							{
								Logger.Log("Checking proxies... please wait...", true, true);
								Dictionary<string, string> dictionary = new Dictionary<string, string>();
								foreach (string text in Form1.Proxies)
								{
									HttpHelper httpHelper = new HttpHelper(text, true, null, 20, null);
									httpHelper.Cookies.SetCookies(new Uri("http://www.nike.com"), "nike_locale={0}/{1}".With(new object[]
									{
										NikeUrls.NikeCountrySmallCode,
										NikeUrls.NikeLangLocale
									}));
									httpHelper.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "nike_locale={0}/{1}".With(new object[]
									{
										NikeUrls.NikeCountrySmallCode,
										NikeUrls.NikeLangLocale
									}));
									httpHelper.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "NIKE_COMMERCE_COUNTRY={0}".With(new object[]
									{
										NikeUrls.NikeCountryCode
									}));
									httpHelper.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "NIKE_COMMERCE_LANG_LOCALE={0}".With(new object[]
									{
										NikeUrls.NikeLangLocale
									}));
									httpHelper.Cookies.SetCookies(new Uri("http://secure-store.nike.com"), "CONSUMERCHOICE={0}/{1}".With(new object[]
									{
										NikeUrls.NikeCountrySmallCode,
										NikeUrls.NikeLangLocale
									}));
									httpHelper.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0\r\nMozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36\r\nMozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36\r\nMozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.71 Safari/537.36\r\nMozilla/5.0 (Macintosh; Intel Mac OS X 10_11) AppleWebKit/601.1.56 (KHTML, like Gecko) Version/9.0 Safari/601.1.56\r\nMozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36\r\nMozilla/5.0 (Macintosh; Intel Mac OS X 10_11_1) AppleWebKit/601.2.7 (KHTML, like Gecko) Version/9.0.1 Safari/601.2.7\r\nMozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko".GetLinesArray(true).GetRandom<string>();
									Dictionary<string, string> postData = new Dictionary<string, string>
									{
										{
											"login",
											"{0}@gmail.com".With(new object[]
											{
												RandomHelper.RandomString(10, true)
											})
										},
										{
											"rememberMe",
											"true"
										},
										{
											"password",
											"BNBFOREVA"
										}
									};
									try
									{
										httpHelper.PostRequest(NikeUrls.NikeLogin, postData, NikeUrls.NikeStore, "application/x-www-form-urlencoded", new string[]
										{
											"X-Requested-With: XMLHttpRequest"
										}, true);
										dictionary.Add(text, "403 (Forbidden)");
									}
									catch (Exception ex)
									{
										dictionary.Add(text, ex.Message);
									}
								}
								if (dictionary.Count((KeyValuePair<string, string> p) => p.Value.Contains("401")) == Form1.Proxies.Count)
								{
									Logger.Log("All proxies are working correctly!", true, true);
									return;
								}
								string s = "Following proxies had errors: \r\n{0}";
								object[] array = new object[1];
								array[0] = (from p in dictionary
								where !p.Value.Contains("401")
								select "Proxy: {0} Error: {1}".With(new object[]
								{
									p.Key,
									p.Value
								})).JoinToString("\r\n");
								string msg = s.With(array);
								Logger.Log(msg, true, true);
								DialogResult dialogResult2 = "Some proxies had errors. Press OK to remove those proxies. Press cancel to keep them anyways. ".Show(MessageBoxIcon.Hand, "Proxies", MessageBoxButtons.OKCancel);
								if (dialogResult2 == DialogResult.OK)
								{
									Form1.Proxies = (from p in dictionary
									where p.Value.Contains("401")
									select p.Key).ToList<string>();
									this.SaveSettings();
								}
							});
						}
					}
				}
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000312A File Offset: 0x0000132A
		private void checkBoxSoldOutRetry_CheckedChanged(object sender, EventArgs e)
		{
			Form1.DoNotRetrySoldOut = this.checkBoxSoldOutRetry.Checked;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00023B1C File Offset: 0x00021D1C
		private void addToLockerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IEnumerable<Account> accs = this.objectListView1.SelectedObjects.Cast<Account>();
			Task.Factory.StartNew(delegate()
			{
				foreach (Account account in accs)
				{
					try
					{
						if (!this.IsRunning)
						{
							account.Login(0, 0);
						}
						foreach (string el in account.EarlyLinks)
						{
							account.AddToLocker(el, null);
						}
					}
					catch (Exception ex)
					{
						Logger.Log("{0}: Error adding to locker. {1}".With(new object[]
						{
							account.EmailAddress,
							ex.Message
						}), true, true);
					}
				}
			});
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00023B64 File Offset: 0x00021D64
		private void deactivateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = "Are you sure? This will deactivate BnB on this computer.\r\n\r\nUse this only when moving between computers. Click No to cancel".Show(MessageBoxIcon.Question, "Deactivate?", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.Yes)
			{
				TurboActivate.Deactivate(true);
				Process current = Process.GetCurrentProcess();
				(from t in Process.GetProcessesByName(current.ProcessName)
				where t.Id != current.Id
				select t).ToList<Process>().ForEach(delegate(Process t)
				{
					t.Kill();
				});
				base.Close();
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000313C File Offset: 0x0000133C
		private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.betternikebot.com/documentation/");
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00003149 File Offset: 0x00001349
		private void fAQsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.betternikebot.com/frequently-asked-questions/");
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00003156 File Offset: 0x00001356
		private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			"Please email us directly at admin@betternikebot.com from your registered email.".Show(MessageBoxIcon.None, "", MessageBoxButtons.OK);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00023BF0 File Offset: 0x00021DF0
		private void buttonEditELs_Click(object sender, EventArgs e)
		{
			string text = FormHelper.InputHelper(false, "Enter Early Links", "Enter ELs separated by comma. This will update ELs in all accounts.", "", 500, 0);
			if (text.IsNotNull())
			{
				if (!text.IsNullOrWhiteSpace())
				{
					if (text.Split(new char[]
					{
						','
					}).TrimAll().Any((string s) => !s.StartsWith("http://")))
					{
						"Invalid early link found. An early link must start with http://".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
						return;
					}
				}
				List<string> earlyLinks = text.IsNullOrWhiteSpace() ? new List<string>() : text.Split(new char[]
				{
					','
				}).TrimAll().ToList<string>();
				foreach (Account account in this._ndcAccounts)
				{
					account.EarlyLinks = earlyLinks;
				}
				this.SaveSettings();
			}
			this.RefreshTable();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000316A File Offset: 0x0000136A
		private void rentAServerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://sneakerserver.com/");
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00003177 File Offset: 0x00001377
		private void earlyLinksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://njsneaks.com/pages/links");
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00003184 File Offset: 0x00001384
		private void startScheduledToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new Thread(delegate()
			{
				Form1.ShouldStop = false;
				this._scheduleWait.Reset();
				using (ScheduleForm scheduleForm = new ScheduleForm(null))
				{
					DialogResult dialogResult = scheduleForm.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						this.InvokeAction(delegate
						{
							this.EnableControls(false);
						});
						TimeSpan timeout = scheduleForm.DateTime - DateTime.Now;
						if (scheduleForm.DateTime < DateTime.Now)
						{
							this.InvokeAction(delegate
							{
								this.buttonStart.PerformClick();
							});
						}
						else
						{
							Logger.Log("Bot will start at {0}".With(new object[]
							{
								scheduleForm.DateTime.ToString()
							}), true, true);
							this._scheduleWait.WaitOne(timeout);
							if (Form1.ShouldStop)
							{
								Logger.Log("Schedule cancelled by user.".With(new object[]
								{
									timeout.ToString()
								}), true, true);
							}
							else
							{
								this.InvokeAction(delegate
								{
									this.buttonStart.PerformClick();
								});
							}
						}
					}
				}
			}).Start();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00023D00 File Offset: 0x00021F00
		private void fromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string[] linesArray = Clipboard.GetText().GetLinesArray(true);
			this.ImportAccounts(linesArray);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00023D20 File Offset: 0x00021F20
		public void ImportAccounts(string[] data)
		{
			int i = 0;
			while (i < data.Length)
			{
				string text = data[i];
				List<string> list = text.Split(new char[]
				{
					'\t'
				}).ToList<string>();
				if (list.Count < 3)
				{
					goto IL_104;
				}
				if (list.Count > 11)
				{
					goto IL_104;
				}
				while (list.Count != 11)
				{
					list.Add("");
				}
				try
				{
					this._ndcAccounts.Add(new Account(list[0], list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9], new CheckoutInfo(false, false, "", "", false, ""), false));
					goto IL_131;
				}
				catch (Exception ex)
				{
					"Error adding account.\r\n{0}".With(new object[]
					{
						ex.Message
					}).Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
					goto IL_131;
				}
				goto IL_104;
				IL_131:
				i++;
				continue;
				IL_104:
				DialogResult dialogResult = "Account is not in correct format.\r\n\r\n{0}\r\n\r\nClick Cancel to cancel the import or OK to continue.".With(new object[]
				{
					text
				}).Show(MessageBoxIcon.Hand, "Error", MessageBoxButtons.OKCancel);
				if (dialogResult != DialogResult.OK)
				{
					return;
				}
				goto IL_131;
			}
			this.RefreshTable();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00023E84 File Offset: 0x00022084
		private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = FileHelper.OpenFile("Import Accounts File", "Text Files|*.txt", true, null);
			if (text.IsNullOrWhiteSpace())
			{
				return;
			}
			this.ImportAccounts(text.GetLinesArray(true));
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00023EBC File Offset: 0x000220BC
		private void getSKUsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Account acc = this.objectListView1.SelectedObjects.Cast<Account>().First<Account>();
			Task.Factory.StartNew(delegate()
			{
				try
				{
					if (!this.IsRunning)
					{
						acc.Login(0, 0);
					}
					foreach (string el in acc.EarlyLinks)
					{
						FormHelper.OutputHelper(true, "{0}: Product Details".With(new object[]
						{
							acc.EmailAddress
						}), "Here you go:", (sender.ToString() == "New") ? acc.GetAllSkusNew(el) : acc.GetAllSkus(el), 500, 0);
					}
				}
				catch (Exception ex)
				{
					Logger.Log("{0}: Error getting all skus. {1}".With(new object[]
					{
						acc.EmailAddress,
						ex.Message
					}), true, true);
				}
			});
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00023F10 File Offset: 0x00022110
		private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "size";
				string text2 = "all";
				string text3 = FormHelper.InputHelper(false, "Enter {0}".With(new object[]
				{
					text
				}), "Enter {0}. This will update {0} in {1} accounts.".With(new object[]
				{
					text,
					text2
				}), "", 500, 0);
				if (text3.IsNotNull())
				{
					Account account = new Account("test@test.com", "help", text3, "", "", "", "", "", "", "", null, false);
					foreach (Account account2 in this._ndcAccounts)
					{
						account2.Size = account.Size;
					}
					this.SaveSettings();
				}
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00024034 File Offset: 0x00022234
		private void earlyLinksToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "early links";
				string text2 = "all";
				string text3 = FormHelper.InputHelper(false, "Enter {0}".With(new object[]
				{
					text
				}), "Enter {0}. This will update {0} in {1} accounts.".With(new object[]
				{
					text,
					text2
				}), "", 500, 0);
				if (text3.IsNotNull())
				{
					Account account = new Account("test@test.com", "help", "10", "", "", text3, "", "", "", "", null, false);
					foreach (Account account2 in this._ndcAccounts)
					{
						account2.EarlyLinks = account.EarlyLinks;
					}
					this.SaveSettings();
				}
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00024158 File Offset: 0x00022358
		private void keywordsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "keywords";
				string text2 = "all";
				string text3 = FormHelper.InputHelper(false, "Enter {0}".With(new object[]
				{
					text
				}), "Enter {0}. This will update {0} in {1} accounts.".With(new object[]
				{
					text,
					text2
				}), "", 500, 0);
				if (text3.IsNotNull())
				{
					Account account = new Account("test@test.com", "help", "10", text3, "", "", "", "", "", "", null, false);
					foreach (Account account2 in this._ndcAccounts)
					{
						account2.Keywords = account.Keywords;
					}
					this.SaveSettings();
				}
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0002427C File Offset: 0x0002247C
		private void collectionKeywordsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "collection keywords";
				string text2 = "all";
				string text3 = FormHelper.InputHelper(false, "Enter {0}".With(new object[]
				{
					text
				}), "Enter {0}. This will update {0} in {1} accounts.".With(new object[]
				{
					text,
					text2
				}), "", 500, 0);
				if (text3.IsNotNull())
				{
					Account account = new Account("test@test.com", "help", "10", "", text3, "", "", "", "", "", null, false);
					foreach (Account account2 in this._ndcAccounts)
					{
						account2.CollectionKeywords = account.CollectionKeywords;
					}
					this.SaveSettings();
				}
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000243A0 File Offset: 0x000225A0
		private void sizeToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "size";
				string text2 = "selected";
				string text3 = FormHelper.InputHelper(false, "Enter {0}".With(new object[]
				{
					text
				}), "Enter {0}. This will update {0} in {1} accounts.".With(new object[]
				{
					text,
					text2
				}), "", 500, 0);
				if (text3.IsNotNull())
				{
					Account account = new Account("test@test.com", "help", text3, "", "", "", "", "", "", "", null, false);
					List<Account> list = this.objectListView1.SelectedObjects.Cast<Account>().ToList<Account>();
					if (!list.IsAny<Account>())
					{
						return;
					}
					foreach (Account account2 in list)
					{
						account2.Size = account.Size;
					}
					this.SaveSettings();
				}
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000244FC File Offset: 0x000226FC
		private void earlyLinksToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "early links";
				string text2 = "selected";
				string text3 = FormHelper.InputHelper(false, "Enter {0}".With(new object[]
				{
					text
				}), "Enter {0}. This will update {0} in {1} accounts.".With(new object[]
				{
					text,
					text2
				}), "", 500, 0);
				if (text3.IsNotNull())
				{
					Account account = new Account("test@test.com", "help", "10", "", "", text3, "", "", "", "", null, false);
					List<Account> list = this.objectListView1.SelectedObjects.Cast<Account>().ToList<Account>();
					if (!list.IsAny<Account>())
					{
						return;
					}
					foreach (Account account2 in list)
					{
						account2.EarlyLinks = account.EarlyLinks;
					}
					this.SaveSettings();
				}
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00024658 File Offset: 0x00022858
		private void keywordsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "keywords";
				string text2 = "selected";
				string text3 = FormHelper.InputHelper(false, "Enter {0}".With(new object[]
				{
					text
				}), "Enter {0}. This will update {0} in {1} accounts.".With(new object[]
				{
					text,
					text2
				}), "", 500, 0);
				if (text3.IsNotNull())
				{
					Account account = new Account("test@test.com", "help", "10", text3, "", "", "", "", "", "", null, false);
					List<Account> list = this.objectListView1.SelectedObjects.Cast<Account>().ToList<Account>();
					if (!list.IsAny<Account>())
					{
						return;
					}
					foreach (Account account2 in list)
					{
						account2.Keywords = account.Keywords;
					}
					this.SaveSettings();
				}
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000247B4 File Offset: 0x000229B4
		private void collectionKeywordsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "collection keywords";
				string text2 = "selected";
				string text3 = FormHelper.InputHelper(false, "Enter {0}".With(new object[]
				{
					text
				}), "Enter {0}. This will update {0} in {1} accounts.".With(new object[]
				{
					text,
					text2
				}), "", 500, 0);
				if (text3.IsNotNull())
				{
					Account account = new Account("test@test.com", "help", "10", "", text3, "", "", "", "", "", null, false);
					List<Account> list = this.objectListView1.SelectedObjects.Cast<Account>().ToList<Account>();
					if (!list.IsAny<Account>())
					{
						return;
					}
					foreach (Account account2 in list)
					{
						account2.CollectionKeywords = account.CollectionKeywords;
					}
					this.SaveSettings();
				}
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00024910 File Offset: 0x00022B10
		private void enableSnkrsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (Account account in this._ndcAccounts)
				{
					account.SnkrsExploit = true;
				}
				this.SaveSettings();
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00024998 File Offset: 0x00022B98
		private void disableSnkrsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (Account account in this._ndcAccounts)
				{
					account.SnkrsExploit = false;
				}
				this.SaveSettings();
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00024A20 File Offset: 0x00022C20
		private void enableSnkrsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				List<Account> list = this.objectListView1.SelectedObjects.Cast<Account>().ToList<Account>();
				foreach (Account account in list)
				{
					account.SnkrsExploit = true;
				}
				this.SaveSettings();
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00024AB8 File Offset: 0x00022CB8
		private void disableSnkrsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				List<Account> list = this.objectListView1.SelectedObjects.Cast<Account>().ToList<Account>();
				foreach (Account account in list)
				{
					account.SnkrsExploit = false;
				}
				this.SaveSettings();
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00024B50 File Offset: 0x00022D50
		private void timerLog_Tick(object sender, EventArgs e)
		{
			if (this._timerRunning)
			{
				return;
			}
			this._timerRunning = true;
			try
			{
				string text = "";
				lock (Logger.ObjectLock)
				{
					IEnumerable<string> list = Logger.Logs.Skip(this._offset);
					text = list.JoinToString("\r\n");
					this._offset = Logger.Count;
					if (this._offset > 500)
					{
						Logger.Logs = new List<string>();
						Logger.Count = 0;
						this._offset = 0;
					}
				}
				if (!text.IsNullOrWhiteSpace() && !Logger.IsPaused)
				{
					if (this.textBoxLog.Lines.Length > 8000)
					{
						this.textBoxLog.Text = "{0} - Application log reset\r\n".With(new object[]
						{
							DateTime.Now.ToString(Logger.DateFormat)
						});
						GC.Collect();
					}
					this.textBoxLog.ClearUndo();
					this.textBoxLog.AppendText("{0}\r\n".With(new object[]
					{
						text
					}));
					Logger.LogFile.AppendAllText(text, true);
				}
			}
			catch (Exception)
			{
			}
			this._timerRunning = false;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00024CBC File Offset: 0x00022EBC
		private void clearCartInAllRegionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SiteType currentRegion = Form1.SiteType;
			DialogResult dialogResult = "Warning! This will remove all items from cart in all nike regions. This may take some time. Do you want to continue?".Show(MessageBoxIcon.Asterisk, "Warning!", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.No)
			{
				return;
			}
			if (this.IsRunning)
			{
				"This operation can't be performed while bot is running. Please click the stop button and try again.".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return;
			}
			IEnumerable<Account> accs = this.objectListView1.SelectedObjects.Cast<Account>();
			Task.Factory.StartNew(delegate()
			{
				foreach (object obj in Enum.GetValues(typeof(SiteType)))
				{
					SiteType siteType = (SiteType)obj;
					Logger.Log("Cleaning cart in {0} region. ".With(new object[]
					{
						siteType.ToString()
					}), true, true);
					Form1.SiteType = siteType;
					foreach (Account account in accs)
					{
						try
						{
							account.Request.ClearCookies();
							account.Login(0, 0);
							account.ClearCart();
						}
						catch (Exception ex)
						{
							Logger.Log("{0}: Error clearing cart. ".With(new object[]
							{
								account.EmailAddress,
								ex.Message
							}), true, true);
						}
					}
				}
				Logger.Log("Carts cleared in all regions", true, true);
				Form1.SiteType = currentRegion;
			});
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00024D3C File Offset: 0x00022F3C
		private void buttonPauseLog_Click(object sender, EventArgs e)
		{
			if (this.buttonPauseLog.Text == "Pause Log")
			{
				Logger.IsPaused = true;
				this.buttonPauseLog.Text = "Resume Log";
			}
			else
			{
				Logger.IsPaused = false;
				this.buttonPauseLog.Text = "Pause Log";
			}
			string value = JsonConvert.SerializeObject(Form1.Tokens);
			JsonConvert.DeserializeObject<Dictionary<string, NikeToken>>(value);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000319C File Offset: 0x0000139C
		private void buttonClearLog_Click(object sender, EventArgs e)
		{
			this.textBoxLog.Clear();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000031A9 File Offset: 0x000013A9
		private void checkBoxSkipFailedLogins_CheckedChanged(object sender, EventArgs e)
		{
			Form1.SkipFailedLogins = this.checkBoxSkipFailedLogins.Checked;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000031BB File Offset: 0x000013BB
		private void checkBoxSendPassword_CheckedChanged(object sender, EventArgs e)
		{
			Form1.SendPassword = this.checkBoxSendPassword.Checked;
			this.SaveSettings();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000031D3 File Offset: 0x000013D3
		private void linkLabelManageCards_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			new CreditCardProfilesForm().ShowDialog();
			this.SaveSettings();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00024DA0 File Offset: 0x00022FA0
		private void cCInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "cc profile";
				string text2 = "all";
				bool multiline = false;
				string title = "Enter exact {0}".With(new object[]
				{
					text
				});
				string s = "Enter {0} name. Leaving blank will disable cc checkout. This will update {0} in {1} accounts. Available profiles: {2}";
				object[] array = new object[3];
				array[0] = text;
				array[1] = text2;
				array[2] = (from c in Form1.CreditCardProfiles
				select c.Name).JoinToString(", ");
				string text3 = FormHelper.InputHelper(multiline, title, s.With(array), "", 500, 0);
				foreach (Account account in this._ndcAccounts)
				{
					account.CheckoutInfo = (account.CheckoutInfo ?? new CheckoutInfo(false, false, "", "", false, ""));
				}
				if (text3.IsNotNull())
				{
					if (text3.IsNullOrWhiteSpace())
					{
						using (List<Account>.Enumerator enumerator2 = this._ndcAccounts.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Account account2 = enumerator2.Current;
								account2.CheckoutInfo.CcCheckout = false;
								account2.CheckoutInfo.CcProfile = null;
							}
							goto IL_1C4;
						}
					}
					if (!text3.EqualsAny((from c in Form1.CreditCardProfiles
					select c.Name).ToArray<string>()))
					{
						throw new Exception("Please enter exact profile name. ");
					}
					foreach (Account account3 in this._ndcAccounts)
					{
						account3.CheckoutInfo.CcCheckout = true;
						account3.CheckoutInfo.CcProfile = text3;
					}
					IL_1C4:
					this.SaveSettings();
				}
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0002500C File Offset: 0x0002320C
		private void cCInfoToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "cc profile";
				string text2 = "selected";
				bool multiline = false;
				string title = "Enter exact {0}".With(new object[]
				{
					text
				});
				string s = "Enter {0} name. Leaving blank will disable cc checkout. This will update {0} in {1} accounts. Available profiles: {2}";
				object[] array = new object[3];
				array[0] = text;
				array[1] = text2;
				array[2] = (from c in Form1.CreditCardProfiles
				select c.Name).JoinToString(", ");
				string text3 = FormHelper.InputHelper(multiline, title, s.With(array), "", 500, 0);
				List<Account> list = this.objectListView1.SelectedObjects.Cast<Account>().ToList<Account>();
				if (list.IsAny<Account>())
				{
					if (text3.IsNotNull())
					{
						if (text3.IsNullOrWhiteSpace())
						{
							using (List<Account>.Enumerator enumerator = list.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									Account account = enumerator.Current;
									account.CheckoutInfo.CcCheckout = false;
									account.CheckoutInfo.CcProfile = null;
								}
								goto IL_185;
							}
						}
						if (!text3.EqualsAny((from c in Form1.CreditCardProfiles
						select c.Name).ToArray<string>()))
						{
							throw new Exception("Please enter exact profile name. ");
						}
						foreach (Account account2 in list)
						{
							account2.CheckoutInfo.CcCheckout = true;
							account2.CheckoutInfo.CcProfile = text3;
						}
						IL_185:
						this.SaveSettings();
					}
					this.RefreshTable();
				}
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00025220 File Offset: 0x00023420
		private void toggleDisableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IList selectedObjects = this.objectListView1.SelectedObjects;
			if (selectedObjects.Count == 0)
			{
				"Please select accounts to disable.".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
			foreach (object obj in selectedObjects)
			{
				((Account)obj).Disabled = !((Account)obj).Disabled;
			}
			this.RefreshTable();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000252B0 File Offset: 0x000234B0
		private void objectListView1_FormatRow(object sender, FormatRowEventArgs e)
		{
			Account account = (Account)e.Model;
			if (account.Disabled)
			{
				e.Item.ForeColor = Color.Gray;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000252E4 File Offset: 0x000234E4
		public static void InitializeCaptchaDB()
		{
			string json = "";
			using (Stream stream = File.OpenRead("captcha.dat"))
			{
				using (Stream stream2 = new GZipStream(stream, CompressionMode.Decompress))
				{
					using (StreamReader streamReader = new StreamReader(stream2))
					{
						json = streamReader.ReadToEnd();
					}
				}
			}
			JArray jarray = JArray.Parse(json);
			Form1.CaptchaDB = new Dictionary<string, List<Bitmap>>();
			foreach (string key in (from el in jarray
			select el["name"].Value<string>().RegexMatch("^.*(?=[0-9][0-9][0-9][0-9] - BetterNikeBot\\.com\\.bmp$)").Value).Distinct<string>())
			{
				Form1.CaptchaDB.Add(key, new List<Bitmap>());
			}
			foreach (JToken jtoken in jarray)
			{
				foreach (KeyValuePair<string, List<Bitmap>> keyValuePair in Form1.CaptchaDB)
				{
					if (jtoken["name"].Value<string>().Contains(keyValuePair.Key))
					{
						keyValuePair.Value.Add((Bitmap)Image.FromStream(new MemoryStream(Convert.FromBase64String(jtoken["bitmap"].Value<string>()))));
					}
				}
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000254B4 File Offset: 0x000236B4
		private void timerData_Tick(object sender, EventArgs e)
		{
			this.timerData.Interval = 300000;
			try
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary["serial_code"] = Form1.SerialCode;
				dictionary["product_name"] = "bnb";
				dictionary["product_version"] = Form1.Version;
				Form1.RemoteSettings = JObject.Parse(HttpHelper.PostUrl("http://api.betternikebot.com/v1/settings", dictionary, null, 10, "", "application/x-www-form-urlencoded", null, true));
			}
			catch (WebException ex)
			{
				string str = "";
				if (ex.Message.Contains("401"))
				{
					str = "Please install the software with a valid serial code.";
				}
				(ex.Message + " " + str).Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return;
			}
			try
			{
				HttpResponse httpResponse = new Http().Get("https://s3.nikecdn.com/unite/scripts/unite.min.js");
				string value = httpResponse.Text.RegexMatch(Regex.Escape("[\"app-version\"]||\"300\"").Replace("300", "(?<ver>[0-9]+)")).Groups["ver"].Value;
				string value2 = httpResponse.Text.RegexMatch(Regex.Escape("[\"experience-version\"]||\"300\"").Replace("300", "(?<ver>[0-9]+)")).Groups["ver"].Value;
				if (value.Length > 2 && value2.Length > 2)
				{
					Form1.RemoteSettings["version_string"] = "appVersion={0}&experienceVersion={1}".With(new object[]
					{
						value,
						value2
					});
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00025660 File Offset: 0x00023860
		private void sNKRSCalendarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Logger.Log("Getting SNKRS calendar for {0}, please wait...".With(new object[]
			{
				Form1.SiteType.ToString()
			}), true, true);
			try
			{
				List<SnkrsItem> list = new List<SnkrsItem>();
				Form1.InitializeProxies();
				string url = "https://api.nike.com/commerce/productfeed/products/v1.5/snkrs/threads?country={0}&locale={1}".With(new object[]
				{
					NikeUrls.NikeCountryCode,
					NikeUrls.NikeLangLocale
				});
				string url2 = HttpHelper.GetUrl(url, null, Form1.ProxyEnumeration.GetNextObject(), 10, null);
				JArray jarray = (JArray)JObject.Parse(url2)["threads"];
				foreach (JToken jtoken in jarray)
				{
					try
					{
						SnkrsItem snkrsItem = new SnkrsItem();
						try
						{
							snkrsItem.DateTime = ((jtoken["product"]["startSellDate"] == null) ? DateTime.MinValue.AddYears(10) : DateTime.Parse(jtoken["product"]["startSellDate"].Value<string>() + "Z", CultureInfo.InvariantCulture));
						}
						catch (Exception)
						{
						}
						try
						{
							snkrsItem.StyleCode = ((jtoken["product"]["style"] == null) ? "" : jtoken["product"]["style"].Value<string>()) + "-" + ((jtoken["product"]["colorCode"] == null) ? "" : jtoken["product"]["colorCode"].Value<string>());
						}
						catch (Exception)
						{
						}
						try
						{
							snkrsItem.WaitLineEnabled = ((jtoken["product"]["waitlineEnabled"] == null) ? "" : jtoken["product"]["waitlineEnabled"].Value<string>());
						}
						catch (Exception)
						{
						}
						try
						{
							snkrsItem.PublishType = ((jtoken["product"]["publishType"] == null) ? "" : jtoken["product"]["publishType"].Value<string>());
						}
						catch (Exception)
						{
						}
						try
						{
							snkrsItem.Available = ((jtoken["product"]["available"] == null) ? "" : jtoken["product"]["available"].Value<string>());
						}
						catch (Exception)
						{
						}
						try
						{
							snkrsItem.SelectionEngine = ((jtoken["product"]["selectionEngine"] == null) ? "" : jtoken["product"]["selectionEngine"].Value<string>());
						}
						catch (Exception)
						{
						}
						try
						{
							snkrsItem.HeatLevel = ((jtoken["product"]["heatLevel"] == null) ? "" : jtoken["product"]["heatLevel"].Value<string>());
						}
						catch (Exception)
						{
						}
						try
						{
							snkrsItem.Link = "https://www.nike.com/snkrs/thread/" + ((jtoken["id"] == null) ? "" : jtoken["id"].Value<string>());
						}
						catch (Exception)
						{
						}
						try
						{
							snkrsItem.Name = ((jtoken["product"]["title"] == null) ? "" : jtoken["product"]["title"].Value<string>().Trim()) + " " + ((jtoken["product"]["subtitle"] == null) ? "" : jtoken["product"]["subtitle"].Value<string>().Trim());
						}
						catch (Exception)
						{
							snkrsItem.Name = jtoken["product"]["title"].Value<string>().Trim();
						}
						list.Add(snkrsItem);
					}
					catch (Exception)
					{
					}
				}
				new SnkrsCalendar((from item in list
				orderby item.DateTime descending
				select item).ToArray<SnkrsItem>()).Show();
			}
			catch (Exception ex)
			{
				Logger.Log("Error getting snkrs calendar! Please try again... " + ex.Message, true, true);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000031E6 File Offset: 0x000013E6
		public void AddAccount(Account acc)
		{
			this._ndcAccounts.Add(acc);
			this.RefreshTable();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00025BF8 File Offset: 0x00023DF8
		private void nikeAccountCreatorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateAccountsForm f = new CreateAccountsForm();
			DialogResult dialogResult = f.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				Form1.InitializeProxies();
				new Thread(delegate()
				{
					CreateAccounts.Create(f);
				}).Start();
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00025C48 File Offset: 0x00023E48
		private void styleCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "style code";
				string text2 = "all";
				string text3 = FormHelper.InputHelper(false, "Enter {0}".With(new object[]
				{
					text
				}), "Enter {0}. This will update {0} in {1} accounts.".With(new object[]
				{
					text,
					text2
				}), "", 500, 0);
				if (text3.IsNotNull())
				{
					Account account = new Account("test@test.com", "help", "10", "", "", "", text3, "", "", "", null, false);
					foreach (Account account2 in this._ndcAccounts)
					{
						account2.ProductStyleCodes = account.ProductStyleCodes;
					}
					this.SaveSettings();
				}
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00025D6C File Offset: 0x00023F6C
		private void styleCodeToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "style code";
				string text2 = "selected";
				string text3 = FormHelper.InputHelper(false, "Enter {0}".With(new object[]
				{
					text
				}), "Enter {0}. This will update {0} in {1} accounts.".With(new object[]
				{
					text,
					text2
				}), "", 500, 0);
				if (text3.IsNotNull())
				{
					Account account = new Account("test@test.com", "help", "10", "", "", "", text3, "", "", "", null, false);
					List<Account> list = this.objectListView1.SelectedObjects.Cast<Account>().ToList<Account>();
					if (!list.IsAny<Account>())
					{
						return;
					}
					foreach (Account account2 in list)
					{
						account2.ProductStyleCodes = account.ProductStyleCodes;
					}
					this.SaveSettings();
				}
				this.RefreshTable();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00025EC8 File Offset: 0x000240C8
		private void toClipboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = (from n in this._ndcAccounts
			select n.ExportAccount()).JoinToString("\r\n");
			Clipboard.SetText(text);
			"Account info copied to clipboard.".Show(MessageBoxIcon.Asterisk, "", MessageBoxButtons.OK);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00025F24 File Offset: 0x00024124
		private void toFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string data = (from n in this._ndcAccounts
			select n.ExportAccount()).JoinToString("\r\n");
			if (FileHelper.SaveFile(data, false, "Export Account", "Text Files|*.txt", true, null))
			{
				"Accounts exported. ".Show(MessageBoxIcon.Asterisk, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00025F8C File Offset: 0x0002418C
		private void toClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (!Form1.CreditCardProfiles.IsAny<CreditCardProfile>())
			{
				"No credit card profiles found. ".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return;
			}
			string text = JArray.FromObject(Form1.CreditCardProfiles).ToString();
			try
			{
				Clipboard.SetText(text);
				"Data copied to clipboard.".Show(MessageBoxIcon.Asterisk, "", MessageBoxButtons.OK);
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0002600C File Offset: 0x0002420C
		private void fromClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			List<CreditCardProfile> collection = null;
			try
			{
				collection = JArray.Parse(Clipboard.GetText()).ToObject<List<CreditCardProfile>>();
			}
			catch (Exception ex)
			{
				"Invalid data. {0}".With(new object[]
				{
					ex.Message
				}).Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return;
			}
			Form1.CreditCardProfiles.AddRange(collection);
			this.SaveSettings();
			"CC Profiles imported succesfully.".Show(MessageBoxIcon.Asterisk, "", MessageBoxButtons.OK);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00026090 File Offset: 0x00024290
		private void fromFileToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			List<CreditCardProfile> collection = null;
			try
			{
				string text = FileHelper.OpenFile("Open File", "Text Files|*.txt", true, null);
				if (text.IsNullOrWhiteSpace())
				{
					throw new Exception("No file selected or blank file. ");
				}
				collection = JArray.Parse(text).ToObject<List<CreditCardProfile>>();
			}
			catch (Exception ex)
			{
				"Invalid data. {0}".With(new object[]
				{
					ex.Message
				}).Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return;
			}
			Form1.CreditCardProfiles.AddRange(collection);
			this.SaveSettings();
			"CC Profiles imported succesfully.".Show(MessageBoxIcon.Asterisk, "", MessageBoxButtons.OK);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00026134 File Offset: 0x00024334
		private void toFileToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (!Form1.CreditCardProfiles.IsAny<CreditCardProfile>())
			{
				"No credit card profiles found. ".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return;
			}
			string data = JArray.FromObject(Form1.CreditCardProfiles).ToString();
			if (FileHelper.SaveFile(data, false, "Save File", "Text Files|*.txt", true, null))
			{
				"File exported succesfully".Show(MessageBoxIcon.Asterisk, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000031FA File Offset: 0x000013FA
		private void buttonHideDisabled_Click(object sender, EventArgs e)
		{
			this._showAll = !this._showAll;
			this.buttonHideDisabled.Text = (this._showAll ? "Hide Disabled" : "Show All");
			this.RefreshTable();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00003230 File Offset: 0x00001430
		private void snkrsTutorialToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.youtube.com/watch?v=b3nHoiklx2M");
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000323D File Offset: 0x0000143D
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00003249 File Offset: 0x00001449
		private void importFormatToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			Process.Start("https://docs.google.com/spreadsheets/d/15ubWQUOXKyeJaPPA7RT2tP7Ju9emNENAlFi_fnbdk4s");
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0002619C File Offset: 0x0002439C
		private void importFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = Clipboard.GetText();
			string[][] array = (from s in text.GetLinesArray(true)
			select s.Split(new char[]
			{
				'\t'
			})).ToArray<string[]>();
			if (array.Any((string[] l) => l.Length != 3))
			{
				"Invalid data on clipboard. Make sure you copy all columns. ".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
			foreach (string array3 in array)
			{
				try
				{
					this._ndcAccounts.Add(new Account(array3[0], array3[1], array3[2], "", "", "", "", "", "", "", null, false));
				}
				catch (Exception ex)
				{
					DialogResult dialogResult = ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OKCancel);
					if (dialogResult == DialogResult.Cancel)
					{
						break;
					}
				}
			}
			this.RefreshTable();
		}

		// Token: 0x040001C0 RID: 448
		private string _monitorAccount;

		// Token: 0x040001C1 RID: 449
		private List<Account> _ndcAccounts;

		// Token: 0x040001C2 RID: 450
		public static List<MonitorLink> MonitorLinks = new List<MonitorLink>();

		// Token: 0x040001C3 RID: 451
		private string _twitterPassword;

		// Token: 0x040001C4 RID: 452
		private string _twitterUsername;

		// Token: 0x040001C5 RID: 453
		public static bool ShouldStop;

		// Token: 0x040001C6 RID: 454
		private HashSet<long> _processedTweets;

		// Token: 0x040001C7 RID: 455
		public static bool GsSize;

		// Token: 0x040001C8 RID: 456
		public static bool AllSize;

		// Token: 0x040001C9 RID: 457
		public static bool VerboseLogging;

		// Token: 0x040001CA RID: 458
		public static bool SendNotifications;

		// Token: 0x040001CB RID: 459
		public static bool SendPassword;

		// Token: 0x040001CC RID: 460
		public static string[] MatchStrings;

		// Token: 0x040001CD RID: 461
		public static string[] NegativeStrings = new string[]
		{
			"women",
			"preschool",
			"infant"
		};

		// Token: 0x040001CE RID: 462
		public static string[] ShoeStrings = new string[]
		{
			"shoe",
			"slide",
			"boot",
			"cleat",
			"flip flop"
		};

		// Token: 0x040001CF RID: 463
		public static Dictionary<string, List<Bitmap>> CaptchaDB;

		// Token: 0x040001D0 RID: 464
		public static ConcurrentList<AddedProduct> AddedProducts = new ConcurrentList<AddedProduct>(null);

		// Token: 0x040001D1 RID: 465
		public static List<string> Proxies = new List<string>();

		// Token: 0x040001D2 RID: 466
		public static EnumerationHelper<string> ProxyEnumeration;

		// Token: 0x040001D3 RID: 467
		public static List<MonitorLink> MonitorLinkCache = new List<MonitorLink>();

		// Token: 0x040001D4 RID: 468
		public static SiteType SiteType = SiteType.NikeUS;

		// Token: 0x040001D5 RID: 469
		public static Form1 DefaultForm;

		// Token: 0x040001D6 RID: 470
		public static List<CreditCardProfile> CreditCardProfiles = new List<CreditCardProfile>();

		// Token: 0x040001D7 RID: 471
		public static bool ForceOldMethod = true;

		// Token: 0x040001D8 RID: 472
		public static bool UseSnkrsForNonUS = false;

		// Token: 0x040001D9 RID: 473
		public static Dictionary<string, string> ResponseDictionary = new Dictionary<string, string>();

		// Token: 0x040001DA RID: 474
		public static string Abc = "abc";

		// Token: 0x040001DB RID: 475
		public static Dictionary<string, NikeToken> Tokens = new Dictionary<string, NikeToken>();

		// Token: 0x040001DC RID: 476
		public static string BrowserDataDir;

		// Token: 0x040001DD RID: 477
		private List<ShouldStop> _shouldStop;

		// Token: 0x040001DE RID: 478
		public static AddedProductsForm AddedProductsForm = new AddedProductsForm();

		// Token: 0x040001DF RID: 479
		public bool DisableTwitter;

		// Token: 0x040001E0 RID: 480
		public static bool UseOldMethod = false;

		// Token: 0x040001E1 RID: 481
		public static bool DoNotRetrySoldOut = false;

		// Token: 0x040001E2 RID: 482
		public static bool SkipFailedLogins = false;

		// Token: 0x040001E3 RID: 483
		private ManualResetEvent _scheduleWait;

		// Token: 0x040001E4 RID: 484
		private int _offset;

		// Token: 0x040001E5 RID: 485
		private bool _timerRunning;

		// Token: 0x040001E6 RID: 486
		private bool _dataTimerRunning;

		// Token: 0x040001E7 RID: 487
		public static string Version = "2016-05-27";

		// Token: 0x040001E8 RID: 488
		public static JObject RemoteSettings = null;

		// Token: 0x040001E9 RID: 489
		private bool _showAll;
	}
}
