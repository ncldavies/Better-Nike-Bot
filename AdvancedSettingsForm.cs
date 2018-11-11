using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Better_Nike_Bot.Browser;
using IMLokesh.Extensions;
using IMLokesh.FormUtility;

namespace Better_Nike_Bot
{
	// Token: 0x02000023 RID: 35
	public partial class AdvancedSettingsForm : Form
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0001FA68 File Offset: 0x0001DC68
		public AdvancedSettingsForm()
		{
			this.InitializeComponent();
			base.CenterToParent();
			this.textBoxServiceLink.Text = NotificationSettings.ServiceLink;
			this.textBoxServiceLogo.Text = NotificationSettings.ServiceLogoImage;
			this.textBoxServiceName.Text = NotificationSettings.ServiceName;
			this.textBoxServiceTwitter.Text = NotificationSettings.ServiceTwitterHandle;
			this.textBoxSubject.Text = NotificationSettings.Subject;
			this.textBoxSendFromName.Text = NotificationSettings.SendFromName;
			this.textBoxSendFromEmail.Text = NotificationSettings.SendFromEmail;
			this.textBoxTextTemplate.Text = NotificationSettings.TextTemplate;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00002D5B File Offset: 0x00000F5B
		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (!this.SaveSettings())
			{
				return;
			}
			base.DialogResult = DialogResult.OK;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0001FB08 File Offset: 0x0001DD08
		private bool SaveSettings()
		{
			if (new string[]
			{
				this.textBoxSubject.Text,
				this.textBoxSendFromEmail.Text,
				this.textBoxSendFromEmail.Text,
				this.textBoxTextTemplate.Text,
				this.textBoxServiceLink.Text,
				this.textBoxServiceLogo.Text,
				this.textBoxServiceName.Text,
				this.textBoxServiceTwitter.Text
			}.Any((string s) => s.IsNullOrWhiteSpace()))
			{
				"All fields are required. ".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return false;
			}
			if (!this.textBoxSendFromEmail.Text.Contains("@"))
			{
				"An email address must contain '@' symbol. ".Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return false;
			}
			NotificationSettings.Subject = this.textBoxSubject.Text;
			NotificationSettings.SendFromName = this.textBoxSendFromName.Text;
			NotificationSettings.SendFromEmail = this.textBoxSendFromEmail.Text;
			NotificationSettings.TextTemplate = this.textBoxTextTemplate.Text;
			NotificationSettings.ServiceLink = this.textBoxServiceLink.Text;
			NotificationSettings.ServiceLogoImage = this.textBoxServiceLogo.Text;
			NotificationSettings.ServiceName = this.textBoxServiceName.Text;
			NotificationSettings.ServiceTwitterHandle = this.textBoxServiceTwitter.Text;
			NotificationSettings.Save();
			return true;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0001FC78 File Offset: 0x0001DE78
		private void AdvancedSettingsForm_Load(object sender, EventArgs e)
		{
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				control.Enabled = Form1.UltimateVersion;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0001FCD8 File Offset: 0x0001DED8
		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DialogResult dialogResult = "Above settings will be automatically saved. ".Show(MessageBoxIcon.Exclamation, "", MessageBoxButtons.OKCancel);
			if (dialogResult == DialogResult.Cancel)
			{
				return;
			}
			if (!this.SaveSettings())
			{
				return;
			}
			NotificationSettings.TextTemplate.ReplacePlaceholders(null).Show(MessageBoxIcon.None, "", MessageBoxButtons.OK);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0001FD20 File Offset: 0x0001DF20
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DialogResult dialogResult = "Above settings will be automatically saved. ".Show(MessageBoxIcon.Exclamation, "", MessageBoxButtons.OKCancel);
			if (dialogResult == DialogResult.Cancel)
			{
				return;
			}
			if (!this.SaveSettings())
			{
				return;
			}
			new Browser
			{
				Html = NotificationSettings.HtmlTemplate.ReplacePlaceholders(null),
				Title = NotificationSettings.Subject.ReplacePlaceholders(null)
			}.ShowDialog();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00002D6D File Offset: 0x00000F6D
		private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			FormHelper.OutputHelper(true, "Available Fields", "You can use the following fields in subject and text template.", NotificationSettings.AvailableFields.JoinToString("\r\n"), 500, 0);
		}
	}
}
