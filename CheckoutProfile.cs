using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IMLokesh.Extensions;

namespace Better_Nike_Bot
{
	// Token: 0x02000008 RID: 8
	public partial class CheckoutProfile : Form
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00004C8C File Offset: 0x00002E8C
		public CheckoutProfile(CreditCardProfile c = null)
		{
			this.InitializeComponent();
			base.CenterToParent();
			this.comboBoxBillingState.SelectedIndex = 0;
			this.comboBoxShippingState.SelectedIndex = 0;
			this.comboBoxBillingStateJP.SelectedIndex = 0;
			this.comboBoxShippingStateJP.SelectedIndex = 0;
			if (c.IsNull())
			{
				return;
			}
			this.label26.Visible = true;
			this.textBoxProfileName.Text = c.Name;
			this.textBoxBillingFirstName.Text = c.BillingFirstName;
			this.textBoxBillingLastName.Text = c.BillingLastName;
			this.textBoxBillingAddress1.Text = c.BillingAddress1;
			this.textBoxBillingZipCode.Text = c.BillingZipCode;
			this.textBoxBillingCity.Text = c.BillingCity;
			this.textBoxBillingPhone.Text = c.BillingPhone;
			if (!c.BillingState.IsNullOrWhiteSpace())
			{
				this.comboBoxBillingState.SelectedItem = this.comboBoxBillingState.Items.Cast<string>().FirstOrDefault((string i) => i.EndsWith(c.BillingState));
			}
			if (!c.ShippingState.IsNullOrWhiteSpace())
			{
				this.comboBoxShippingState.SelectedItem = this.comboBoxShippingState.Items.Cast<string>().FirstOrDefault((string i) => i.EndsWith(c.ShippingState));
			}
			if (!c.BillingStateJP.IsNullOrWhiteSpace())
			{
				this.comboBoxBillingStateJP.SelectedItem = this.comboBoxBillingStateJP.Items.Cast<string>().FirstOrDefault((string i) => i.EndsWith(c.BillingStateJP));
			}
			if (!c.ShippingStateJP.IsNullOrWhiteSpace())
			{
				this.comboBoxShippingStateJP.SelectedItem = this.comboBoxShippingStateJP.Items.Cast<string>().FirstOrDefault((string i) => i.EndsWith(c.ShippingStateJP));
			}
			this.textBoxShippingFirst.Text = c.ShippingFirstName;
			this.textBoxShippingPhone.Text = c.ShippingPhone;
			this.textBoxShippingLast.Text = c.ShippingLastName;
			this.textBoxShippingAddress1.Text = c.ShippingAddress1;
			this.textBoxShippingCity.Text = c.ShippingCity;
			this.textBoxShippingPostalCode.Text = c.ShippingZipCode;
			this.comboBoxCardType.SelectedItem = c.CreditCardType;
			this.textBoxCardNumber.Text = c.CreditCardNumber;
			this.comboBoxExpirationMonth.SelectedItem = c.CreditCardExpiryMonth;
			this.comboBoxExpirationYear.SelectedItem = c.CreditCardExpiryYear;
			this.textBoxCvv.Text = c.CreditCardCvv;
			this.textBoxMaxCheckouts.Text = c.MaxCheckouts.ToString();
			this.textBoxBillingAddress2.Text = c.BillingAddress2;
			this.textBoxShippingAddress2.Text = c.ShippingAddress2;
			this.textBoxBillingAddress3.Text = c.BillingAddress3;
			this.textBoxShippingAddress3.Text = c.ShippingAddress3;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000503C File Offset: 0x0000323C
		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (this.checkBoxShippingSame.Checked)
			{
				this.comboBoxShippingState.SelectedItem = this.comboBoxBillingState.SelectedItem;
				this.comboBoxShippingStateJP.SelectedItem = this.comboBoxBillingStateJP.SelectedItem;
				this.textBoxShippingAddress1.Text = this.textBoxBillingAddress1.Text;
				this.textBoxShippingAddress2.Text = this.textBoxBillingAddress2.Text;
				this.textBoxShippingAddress3.Text = this.textBoxBillingAddress3.Text;
				this.textBoxShippingCity.Text = this.textBoxBillingCity.Text;
				this.textBoxShippingFirst.Text = this.textBoxBillingFirstName.Text;
				this.textBoxShippingLast.Text = this.textBoxBillingLastName.Text;
				this.textBoxShippingPhone.Text = this.textBoxBillingPhone.Text;
				this.textBoxShippingPostalCode.Text = this.textBoxBillingZipCode.Text;
			}
			try
			{
				string[] source = new string[]
				{
					this.textBoxProfileName.Text,
					this.textBoxBillingFirstName.Text,
					this.textBoxBillingLastName.Text,
					this.textBoxBillingAddress1.Text,
					this.textBoxBillingZipCode.Text,
					this.textBoxBillingCity.Text,
					this.textBoxBillingPhone.Text,
					this.textBoxShippingFirst.Text,
					this.textBoxShippingPhone.Text,
					this.textBoxShippingLast.Text,
					this.textBoxShippingAddress1.Text,
					this.textBoxShippingCity.Text,
					this.textBoxShippingPostalCode.Text,
					this.comboBoxCardType.SelectedItem.IsNull() ? "" : this.comboBoxCardType.SelectedItem.ToString(),
					this.textBoxCardNumber.Text,
					this.comboBoxExpirationMonth.SelectedItem.IsNull() ? "" : this.comboBoxExpirationMonth.SelectedItem.ToString(),
					this.comboBoxExpirationYear.SelectedItem.IsNull() ? "" : this.comboBoxExpirationYear.SelectedItem.ToString(),
					this.textBoxCvv.Text,
					this.textBoxMaxCheckouts.Text
				};
				if (source.Any((string s) => s.IsNullOrWhiteSpace()))
				{
					throw new Exception("A required value is missing. Make sure you fill all details.");
				}
				if (new string[]
				{
					this.textBoxShippingPhone.Text,
					this.textBoxBillingPhone.Text
				}.Any((string s) => !s.RegexIsMatch("^[0-9]{10,}$")))
				{
					throw new Exception("Please enter a valid 10 digit phone number.");
				}
				if (new string[]
				{
					this.textBoxCardNumber.Text,
					this.textBoxCvv.Text
				}.Any((string s) => !s.RegexIsMatch("^[0-9]+$")))
				{
					throw new Exception("Please enter valid card details. Remove any blank spaces.");
				}
				if (this.comboBoxBillingStateJP.SelectedIndex != 0 && this.textBoxBillingAddress3.Text.IsNullOrWhiteSpace())
				{
					"Please also enter 町域 (Town Area) for running for Japan.".Show(MessageBoxIcon.Asterisk, "", MessageBoxButtons.OK);
				}
				CreditCardProfile creditCardProfile = new CreditCardProfile
				{
					Name = this.textBoxProfileName.Text,
					BillingFirstName = this.textBoxBillingFirstName.Text,
					BillingLastName = this.textBoxBillingLastName.Text,
					BillingAddress1 = this.textBoxBillingAddress1.Text,
					BillingAddress2 = this.textBoxBillingAddress2.Text,
					BillingAddress3 = this.textBoxBillingAddress3.Text,
					BillingZipCode = this.textBoxBillingZipCode.Text,
					BillingCity = this.textBoxBillingCity.Text,
					BillingPhone = this.textBoxBillingPhone.Text,
					BillingState = ((this.comboBoxBillingState.SelectedIndex == 0) ? "" : this.comboBoxBillingState.SelectedItem.ToString().Split(new string[]
					{
						" - "
					}, StringSplitOptions.None).Last<string>()),
					ShippingState = ((this.comboBoxShippingState.SelectedIndex == 0) ? "" : this.comboBoxShippingState.SelectedItem.ToString().Split(new string[]
					{
						" - "
					}, StringSplitOptions.None).Last<string>()),
					BillingStateJP = ((this.comboBoxBillingStateJP.SelectedIndex == 0) ? "" : this.comboBoxBillingStateJP.SelectedItem.ToString().Split(new string[]
					{
						" - "
					}, StringSplitOptions.None).Last<string>()),
					ShippingStateJP = ((this.comboBoxShippingStateJP.SelectedIndex == 0) ? "" : this.comboBoxShippingStateJP.SelectedItem.ToString().Split(new string[]
					{
						" - "
					}, StringSplitOptions.None).Last<string>()),
					ShippingFirstName = this.textBoxShippingFirst.Text,
					ShippingPhone = this.textBoxShippingPhone.Text,
					ShippingLastName = this.textBoxShippingLast.Text,
					ShippingAddress1 = this.textBoxShippingAddress1.Text,
					ShippingAddress2 = this.textBoxShippingAddress2.Text,
					ShippingAddress3 = this.textBoxShippingAddress3.Text,
					ShippingCity = this.textBoxShippingCity.Text,
					ShippingZipCode = this.textBoxShippingPostalCode.Text,
					CreditCardType = this.comboBoxCardType.SelectedItem.ToString(),
					CreditCardNumber = this.textBoxCardNumber.Text,
					CreditCardExpiryMonth = this.comboBoxExpirationMonth.SelectedItem.ToString(),
					CreditCardExpiryYear = this.comboBoxExpirationYear.SelectedItem.ToString(),
					CreditCardCvv = this.textBoxCvv.Text,
					MaxCheckouts = this.textBoxMaxCheckouts.Text.ParseToInt()
				};
				if (Form1.CreditCardProfiles.Any((CreditCardProfile c) => c.Name == this.textBoxProfileName.Text))
				{
					int index = Form1.CreditCardProfiles.IndexOf(Form1.CreditCardProfiles.First((CreditCardProfile c) => c.Name == this.textBoxProfileName.Text));
					Form1.CreditCardProfiles[index] = creditCardProfile;
				}
				else
				{
					Form1.CreditCardProfiles.Add(creditCardProfile);
				}
				Form1.DefaultForm.SaveSettings();
				base.DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
			}
		}
	}
}
