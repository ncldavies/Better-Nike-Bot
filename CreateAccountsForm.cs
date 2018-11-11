using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using IMLokesh.Extensions;
using IMLokesh.HttpUtility;

namespace Better_Nike_Bot
{
	// Token: 0x0200000D RID: 13
	public partial class CreateAccountsForm : Form
	{
		// Token: 0x06000052 RID: 82 RVA: 0x0000232B File Offset: 0x0000052B
		public CreateAccountsForm()
		{
			this.InitializeComponent();
			base.CenterToParent();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00009990 File Offset: 0x00007B90
		private void CreateAccountsForm_Load(object sender, EventArgs e)
		{
			this.checkBoxRandomName.Checked = true;
			try
			{
				this.isUltimate = HttpHelper.PostUrl("http://www.betternikebot.com/bnb/isUltimate.php", "pkey={0}".With(new object[]
				{
					Form1.SerialCode
				}), null, 10, "", "application/x-www-form-urlencoded", null, true).ParseToBool();
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				this.isUltimate = false;
			}
			if (!this.isUltimate)
			{
				this.numericUpDownNumberOfAccounts.Maximum = 5m;
			}
			bool flag = false;
			if (!this.isUltimate)
			{
				try
				{
					flag = !CreateAccounts.CheckStatus();
				}
				catch (Exception)
				{
				}
			}
			if (flag)
			{
				DialogResult dialogResult = "Regular users can only create 5 accounts per day. Please check back tomorrow. Would you like to learn more?".Show(MessageBoxIcon.Asterisk, "", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					Process.Start("http://www.betternikebot.com/important-update-regarding-nike-account-creator/");
				}
				this.buttonStart.Enabled = false;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000233F File Offset: 0x0000053F
		private void checkBoxRandomName_CheckedChanged(object sender, EventArgs e)
		{
			this.textBoxName.Enabled = !this.checkBoxRandomName.Checked;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00009A88 File Offset: 0x00007C88
		private void buttonStart_Click(object sender, EventArgs e)
		{
			if (this.textBoxEmailAddress.Text.Split(new char[]
			{
				'@'
			}).Length == 2)
			{
				if (this.textBoxSMSEmail.Text.Split(new char[]
				{
					'@'
				}).Length == 2)
				{
					if (this.textBoxPassword.Text.Length < 8)
					{
						"Password must be at least 8 characters long. ".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
						return;
					}
					if (!this.textBoxPassword.Text.RegexIsMatch("[0-9]"))
					{
						"Password must contain at least one numeric character. ".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
						return;
					}
					if (!this.textBoxPassword.Text.RegexIsMatch("[A-Z]") || !this.textBoxPassword.Text.RegexIsMatch("[a-z]"))
					{
						"Password must contain mixed case letters (capital letters and small letters). ".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
						return;
					}
					if (!this.checkBoxRandomName.Checked && this.textBoxName.Text.Split(new char[]
					{
						' '
					}).Length < 2)
					{
						"Name must contain a first and a last name. ".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
						return;
					}
					this.email = this.textBoxEmailAddress.Text.Trim();
					this.pass = this.textBoxPassword.Text.Trim();
					this.name = this.textBoxName.Text.Trim();
					this.randomName = this.checkBoxRandomName.Checked;
					this.count = this.numericUpDownNumberOfAccounts.Value.ParseToInt();
					this.smsEmail = this.textBoxSMSEmail.Text.Trim();
					this.smsApiKey = this.textBoxSMSAPI.Text.Trim();
					this.disableMobile = this.checkBox1.Checked;
					this.disablePlusSign = this.checkBoxPlusSign.Checked;
					base.DialogResult = DialogResult.OK;
					return;
				}
			}
			"Please enter a valid email address. ".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00009C98 File Offset: 0x00007E98
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start("http://www.getsmscode.com/?ref=165220");
			}
			catch (Exception)
			{
				"Please open getsmscode.com in your browser.".Show(MessageBoxIcon.Asterisk, "", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00009CD8 File Offset: 0x00007ED8
		private void textBoxEmailAddress_Enter(object sender, EventArgs e)
		{
			TextBox window = (TextBox)sender;
			ToolTip toolTip = new ToolTip();
			toolTip.Show("Variations of this email will be used to signup.", window, 0, 22, 3000);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00009D08 File Offset: 0x00007F08
		private void textBoxPassword_Enter(object sender, EventArgs e)
		{
			TextBox window = (TextBox)sender;
			ToolTip toolTip = new ToolTip();
			toolTip.Show("This will be your nike+ account's password.", window, 0, 22, 3000);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00009D38 File Offset: 0x00007F38
		private void textBoxName_Enter(object sender, EventArgs e)
		{
			TextBox window = (TextBox)sender;
			ToolTip toolTip = new ToolTip();
			toolTip.Show("This will be your nike+ account's name. Enter full name. Or select random name.", window, 0, 22, 3000);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000235A File Offset: 0x0000055A
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.groupBox1.Enabled = !this.checkBox1.Checked;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002375 File Offset: 0x00000575
		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.betternikebot.com/important-update-regarding-nike-account-creator/");
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002382 File Offset: 0x00000582
		private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.getsmscode.com/itemlist.php");
		}

		// Token: 0x0400006E RID: 110
		public string email;

		// Token: 0x0400006F RID: 111
		public string pass;

		// Token: 0x04000070 RID: 112
		public string name;

		// Token: 0x04000071 RID: 113
		public string smsEmail;

		// Token: 0x04000072 RID: 114
		public string smsApiKey;

		// Token: 0x04000073 RID: 115
		public bool randomName;

		// Token: 0x04000074 RID: 116
		public int count;

		// Token: 0x04000075 RID: 117
		public bool disableMobile;

		// Token: 0x04000076 RID: 118
		public bool disablePlusSign;

		// Token: 0x04000077 RID: 119
		public bool isUltimate;
	}
}
