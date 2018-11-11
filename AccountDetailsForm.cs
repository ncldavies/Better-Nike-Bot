using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IMLokesh.Extensions;

namespace Better_Nike_Bot
{
	// Token: 0x0200001D RID: 29
	public partial class AccountDetailsForm : Form
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00013F88 File Offset: 0x00012188
		public AccountDetailsForm(Account acc = null, string link = "", string styleCode = "")
		{
			this.InitializeComponent();
			base.CenterToParent();
			this.comboBoxCardProfile.Items.Clear();
			Form1.CreditCardProfiles = (from c in Form1.CreditCardProfiles
			where !c.Name.IsNullOrWhiteSpace()
			select c).ToList<CreditCardProfile>();
			this.comboBoxCardProfile.Items.AddRange((from c in Form1.CreditCardProfiles
			select c.Name).ToArray<string>());
			this.textBoxEarlyLinks.Text = link;
			this.textBoxStyleCodes.Text = styleCode;
			if (acc.IsNotNull())
			{
				this.textBoxEmailAddress.Text = acc.EmailAddress;
				this.textBoxPassword.Text = acc.Password;
				this.textBoxSize.Text = acc.Size.JoinToString(",");
				this.textBoxTweetKeywords.Text = acc.Keywords.JoinToString(",");
				this.textBoxCollectionKeywords.Text = acc.CollectionKeywords.JoinToString(",");
				this.textBoxEarlyLinks.Text = acc.EarlyLinks.JoinToString(",");
				this.textBoxStyleCodes.Text = acc.ProductStyleCodes.JoinToString(",");
				this.checkBoxGuestCheckout.Checked = acc.IsGuest;
				this.textBoxNotificationEmail.Text = acc.NotificationEmail;
				if (acc.CheckoutInfo.IsNotNull())
				{
					this.checkBoxEnableCCCheckout.Checked = acc.CheckoutInfo.CcCheckout;
					try
					{
						this.comboBoxCardProfile.SelectedItem = acc.CheckoutInfo.CcProfile;
					}
					catch (Exception)
					{
						"Error selecting cc profile".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
					}
				}
				if (!acc.NotificationText.IsNullOrWhiteSpace())
				{
					string[] items = acc.NotificationText.Split(new char[]
					{
						'@'
					});
					string text = items[0];
					NotificationService notificationService = NotificationSettings.AvailableTextNotificationServices.First((NotificationService n) => n.EmailSuffix.EndsWith(items[1]));
					this.textBoxNumber.Text = text;
					this.comboBoxCarrier.SelectedItem = notificationService.Carrier;
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00002A9C File Offset: 0x00000C9C
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public Account Account { get; set; }

		// Token: 0x06000124 RID: 292 RVA: 0x000141E4 File Offset: 0x000123E4
		private void buttonSave_Click(object sender, EventArgs e)
		{
			try
			{
				CheckoutInfo cInfo = new CheckoutInfo(false, this.checkBoxEnableCCCheckout.Checked, "", "", false, this.comboBoxCardProfile.SelectedItem.IsNull() ? null : this.comboBoxCardProfile.SelectedItem.ToString());
				this.Account = new Account(this.textBoxEmailAddress.Text, this.textBoxPassword.Text, this.textBoxSize.Text, this.textBoxTweetKeywords.Text, this.textBoxCollectionKeywords.Text, this.textBoxEarlyLinks.Text, this.textBoxStyleCodes.Text, this.textBoxNotificationEmail.Text, this.comboBoxCarrier.SelectedItem.IsNull() ? "" : this.comboBoxCarrier.SelectedItem.ToString(), this.textBoxNumber.Text, cInfo, this.checkBoxGuestCheckout.Checked);
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return;
			}
			base.DialogResult = DialogResult.OK;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00014308 File Offset: 0x00012508
		private void textBox4_Enter(object sender, EventArgs e)
		{
			TextBox window = (TextBox)sender;
			ToolTip toolTip = new ToolTip();
			toolTip.Show("Separate multiple values by comma.", window, 0, 22, 3000);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00014308 File Offset: 0x00012508
		private void textBox5_Enter(object sender, EventArgs e)
		{
			TextBox window = (TextBox)sender;
			ToolTip toolTip = new ToolTip();
			toolTip.Show("Separate multiple values by comma.", window, 0, 22, 3000);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00014338 File Offset: 0x00012538
		private void textBox6_Enter(object sender, EventArgs e)
		{
			TextBox window = (TextBox)sender;
			ToolTip toolTip = new ToolTip();
			toolTip.Show("Please enter style code that looks like xxxxxx-xxx.", window, 0, 22, 3000);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00002AAD File Offset: 0x00000CAD
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.textBoxPassword.PasswordChar = (this.checkBoxShowPassword.Checked ? '\0' : '•');
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00014308 File Offset: 0x00012508
		private void textBox7_Enter(object sender, EventArgs e)
		{
			TextBox window = (TextBox)sender;
			ToolTip toolTip = new ToolTip();
			toolTip.Show("Separate multiple values by comma.", window, 0, 22, 3000);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00014368 File Offset: 0x00012568
		private void textBox8_Enter(object sender, EventArgs e)
		{
			TextBox window = (TextBox)sender;
			ToolTip toolTip = new ToolTip();
			toolTip.Show("Defaults to the account login address. ", window, 0, 22, 3000);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00014398 File Offset: 0x00012598
		private void linkLabelManageCards_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			new CreditCardProfilesForm().ShowDialog();
			this.comboBoxCardProfile.Items.Clear();
			this.comboBoxCardProfile.Items.AddRange((from c in Form1.CreditCardProfiles
			select c.Name).ToArray<string>());
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000143FC File Offset: 0x000125FC
		private void checkBoxRandomSize_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxRandomSize.Checked)
			{
				this.textBoxSize.Text = "RANDOM";
				this.textBoxSize.Enabled = false;
				return;
			}
			this.textBoxSize.Text = "";
			this.textBoxSize.Enabled = true;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00002ACF File Offset: 0x00000CCF
		private void checkBoxGuestCheckout_CheckedChanged(object sender, EventArgs e)
		{
			this.textBoxPassword.Enabled = !this.checkBoxGuestCheckout.Checked;
		}
	}
}
