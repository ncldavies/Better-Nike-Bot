using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;
using IMLokesh.Extensions;
using IMLokesh.FormUtility;
using IMLokesh.HttpUtility;
using wyDay.TurboActivate;

namespace Better_Nike_Bot
{
	// Token: 0x0200004F RID: 79
	internal static class Program
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0002C964 File Offset: 0x0002AB64
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			TurboActivate.VersionGUID = "69bf09c1537bbc0a134337.83592116";
			Program.CheckLicense();
			if (!Program.IsUserAdministrator())
			{
				"You must start the application as an administrator. \r\n\r\nTo do this, right click and select 'Run as administrator'.\r\n\r\nYou can also right click, go to properties and select 'Always Run as Admin' under the compatibility tab.\r\n\r\nPlease contact us if you keep facing issues.".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return;
			}
			if (Program.PriorProcess() != null)
			{
				try
				{
					if (!HttpHelper.PostUrl("https://olejov.000webhostapp.com/startup2", "pkey=123456".With(new object[]
					{
						Program.serial_code
					}), null, 10, "", "application/x-www-form-urlencoded", null, true).ParseToBool())
					{
						if ("BNB is already running. Only ultimate users can spawn multiple instances on the same machine. Please contact admin@betternikebot.com for more info.\r\n\r\nWould you like to upgrade to ultimate now?".Show(MessageBoxIcon.Hand, "BNB Error", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
						{
							Process.Start("http://www.betternikebot.com/upgrade-to-ultimate/");
						}
						return;
					}
				}
				catch (Exception ex)
				{
					("Error initializing instance... " + ex.Message).Show(MessageBoxIcon.Hand, "BNB Error", MessageBoxButtons.OK);
					return;
				}
			}
			if (Program.run)
			{
				Application.Run(new AddedProductsForm());
				Application.Run(new Form1());
				return;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0002CA58 File Offset: 0x0002AC58
		private static Process PriorProcess()
		{
			Process currentProcess = Process.GetCurrentProcess();
			Process[] processesByName = Process.GetProcessesByName(currentProcess.ProcessName);
			foreach (Process process in processesByName)
			{
				if (process.Id != currentProcess.Id && process.MainModule.FileName == currentProcess.MainModule.FileName)
				{
					return process;
				}
			}
			return null;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0002CAC8 File Offset: 0x0002ACC8
		public static void CheckLicense()
		{
			string text = "";
			try
			{
				text = TurboActivate.GetPKey();
			}
			catch (Exception)
			{
			}
			bool flag = true;
			IsGenuineResult isGenuineResult;
			try
			{
				isGenuineResult = TurboActivate.IsGenuine(0u, 0u, false, false);
				flag = (isGenuineResult == IsGenuineResult.Genuine || isGenuineResult == IsGenuineResult.GenuineFeaturesChanged || isGenuineResult == IsGenuineResult.InternetError);
			}
			catch (TurboActivateException ex)
			{
				if (MessageBox.Show("Failed to check license. Click OK to retry. Please disable any antivirus or firewall that could be blocking bnb. \r\n" + ex.Message, "Checking License", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) == DialogResult.OK)
				{
					Program.CheckLicense();
				}
				return;
			}
			if (flag)
			{
				if (isGenuineResult != IsGenuineResult.InternetError)
				{
					Program.run = true;
					Form1.Abc = "cba";
					string text2 = "invalid";
					try
					{
						text2 = TurboActivate.GetPKey();
					}
					catch (Exception ex2)
					{
						"Error getting license details {0}".With(new object[]
						{
							ex2.Message
						}).Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
					}
					Form1.SerialCode = (Program.serial_code = text2);
					return;
				}
				if (MessageBox.Show("Could not connect to server. Click OK to retry.", "Checking License", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) == DialogResult.OK)
				{
					Program.CheckLicense();
					return;
				}
			}
			else
			{
				string text3 = FormHelper.InputHelper(false, "Better Nike Bot: Enter Serial Key", "Please enter the serial code that you received upon purchase", text, 500, 0);
				if (text3.IsNull())
				{
					"You need a valid serial to run BetterNikeBot.".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
					return;
				}
				try
				{
					TurboActivate.CheckAndSavePKey(text3, TurboActivate.TA_Flags.TA_USER);
					TurboActivate.Activate();
					isGenuineResult = TurboActivate.IsGenuine(0u, 0u, false, false);
					Program.run = true;
					Form1.Abc = "cba";
					string serialCode = "invalid";
					try
					{
						serialCode = TurboActivate.GetPKey();
					}
					catch (Exception ex3)
					{
						"Error getting license details. {0}".With(new object[]
						{
							ex3.Message
						}).Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
					}
					Form1.SerialCode = serialCode;
				}
				catch (Exception ex4)
				{
					("Error in serial key:\r\n" + ex4.Message).Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
					Program.CheckLicense();
				}
				return;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0002CCBC File Offset: 0x0002AEBC
		public static bool IsUserAdministrator()
		{
			bool result;
			try
			{
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
				result = windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
			}
			catch (UnauthorizedAccessException)
			{
				result = false;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040002DC RID: 732
		private const string Version = "2015-07-31";

		// Token: 0x040002DD RID: 733
		private static bool run = false;

		// Token: 0x040002DE RID: 734
		private static string serial_code = "";
	}
}
