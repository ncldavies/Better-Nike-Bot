namespace Better_Nike_Bot
{
	// Token: 0x0200001D RID: 29
	public partial class AccountDetailsForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00002AEA File Offset: 0x00000CEA
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00014450 File Offset: 0x00012650
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.textBoxEmailAddress = new global::System.Windows.Forms.TextBox();
			this.textBoxPassword = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.textBoxSize = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.textBoxTweetKeywords = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.textBoxEarlyLinks = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.textBoxStyleCodes = new global::System.Windows.Forms.TextBox();
			this.label6 = new global::System.Windows.Forms.Label();
			this.buttonSave = new global::System.Windows.Forms.Button();
			this.checkBoxShowPassword = new global::System.Windows.Forms.CheckBox();
			this.textBoxCollectionKeywords = new global::System.Windows.Forms.TextBox();
			this.label7 = new global::System.Windows.Forms.Label();
			this.textBoxNotificationEmail = new global::System.Windows.Forms.TextBox();
			this.label8 = new global::System.Windows.Forms.Label();
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.label10 = new global::System.Windows.Forms.Label();
			this.label9 = new global::System.Windows.Forms.Label();
			this.textBoxNumber = new global::System.Windows.Forms.TextBox();
			this.comboBoxCarrier = new global::System.Windows.Forms.ComboBox();
			this.tabControl1 = new global::System.Windows.Forms.TabControl();
			this.tabPage1 = new global::System.Windows.Forms.TabPage();
			this.checkBoxGuestCheckout = new global::System.Windows.Forms.CheckBox();
			this.checkBoxRandomSize = new global::System.Windows.Forms.CheckBox();
			this.label11 = new global::System.Windows.Forms.Label();
			this.tabPage2 = new global::System.Windows.Forms.TabPage();
			this.groupBox3 = new global::System.Windows.Forms.GroupBox();
			this.linkLabelManageCards = new global::System.Windows.Forms.LinkLabel();
			this.comboBoxCardProfile = new global::System.Windows.Forms.ComboBox();
			this.label19 = new global::System.Windows.Forms.Label();
			this.checkBoxEnableCCCheckout = new global::System.Windows.Forms.CheckBox();
			this.label12 = new global::System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(31, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(80, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "*Email Address:";
			this.textBoxEmailAddress.Location = new global::System.Drawing.Point(117, 6);
			this.textBoxEmailAddress.Name = "textBoxEmailAddress";
			this.textBoxEmailAddress.Size = new global::System.Drawing.Size(262, 20);
			this.textBoxEmailAddress.TabIndex = 1;
			this.textBoxPassword.Location = new global::System.Drawing.Point(117, 32);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.PasswordChar = '•';
			this.textBoxPassword.Size = new global::System.Drawing.Size(262, 20);
			this.textBoxPassword.TabIndex = 2;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(51, 38);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(60, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "*Password:";
			this.textBoxSize.Location = new global::System.Drawing.Point(117, 58);
			this.textBoxSize.Name = "textBoxSize";
			this.textBoxSize.Size = new global::System.Drawing.Size(262, 20);
			this.textBoxSize.TabIndex = 4;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(77, 64);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(34, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "*Size:";
			this.textBoxTweetKeywords.Location = new global::System.Drawing.Point(117, 84);
			this.textBoxTweetKeywords.Name = "textBoxTweetKeywords";
			this.textBoxTweetKeywords.Size = new global::System.Drawing.Size(361, 20);
			this.textBoxTweetKeywords.TabIndex = 5;
			this.textBoxTweetKeywords.Enter += new global::System.EventHandler(this.textBox4_Enter);
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(22, 87);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(89, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Tweet Keywords:";
			this.textBoxEarlyLinks.Location = new global::System.Drawing.Point(117, 138);
			this.textBoxEarlyLinks.Name = "textBoxEarlyLinks";
			this.textBoxEarlyLinks.Size = new global::System.Drawing.Size(361, 20);
			this.textBoxEarlyLinks.TabIndex = 8;
			this.textBoxEarlyLinks.Enter += new global::System.EventHandler(this.textBox5_Enter);
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(50, 141);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(61, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Early Links:";
			this.textBoxStyleCodes.Location = new global::System.Drawing.Point(117, 164);
			this.textBoxStyleCodes.Name = "textBoxStyleCodes";
			this.textBoxStyleCodes.Size = new global::System.Drawing.Size(159, 20);
			this.textBoxStyleCodes.TabIndex = 9;
			this.textBoxStyleCodes.Enter += new global::System.EventHandler(this.textBox6_Enter);
			this.label6.AutoSize = true;
			this.label6.Location = new global::System.Drawing.Point(50, 167);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(61, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Style Code:";
			this.buttonSave.Location = new global::System.Drawing.Point(430, 311);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new global::System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 11;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new global::System.EventHandler(this.buttonSave_Click);
			this.checkBoxShowPassword.AutoSize = true;
			this.checkBoxShowPassword.Location = new global::System.Drawing.Point(382, 33);
			this.checkBoxShowPassword.Name = "checkBoxShowPassword";
			this.checkBoxShowPassword.Size = new global::System.Drawing.Size(102, 17);
			this.checkBoxShowPassword.TabIndex = 3;
			this.checkBoxShowPassword.Text = "Show Password";
			this.checkBoxShowPassword.UseVisualStyleBackColor = true;
			this.checkBoxShowPassword.CheckedChanged += new global::System.EventHandler(this.checkBox1_CheckedChanged);
			this.textBoxCollectionKeywords.Location = new global::System.Drawing.Point(117, 112);
			this.textBoxCollectionKeywords.Name = "textBoxCollectionKeywords";
			this.textBoxCollectionKeywords.Size = new global::System.Drawing.Size(361, 20);
			this.textBoxCollectionKeywords.TabIndex = 7;
			this.textBoxCollectionKeywords.Enter += new global::System.EventHandler(this.textBox7_Enter);
			this.label7.AutoSize = true;
			this.label7.Location = new global::System.Drawing.Point(6, 115);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(105, 13);
			this.label7.TabIndex = 14;
			this.label7.Text = "Collection Keywords:";
			this.textBoxNotificationEmail.Location = new global::System.Drawing.Point(117, 190);
			this.textBoxNotificationEmail.Name = "textBoxNotificationEmail";
			this.textBoxNotificationEmail.Size = new global::System.Drawing.Size(361, 20);
			this.textBoxNotificationEmail.TabIndex = 15;
			this.textBoxNotificationEmail.Enter += new global::System.EventHandler(this.textBox8_Enter);
			this.label8.AutoSize = true;
			this.label8.Location = new global::System.Drawing.Point(20, 193);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(91, 13);
			this.label8.TabIndex = 16;
			this.label8.Text = "Notification Email:";
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.textBoxNumber);
			this.groupBox1.Controls.Add(this.comboBoxCarrier);
			this.groupBox1.Location = new global::System.Drawing.Point(23, 216);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new global::System.Drawing.Size(455, 40);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Text Notification";
			this.label10.AutoSize = true;
			this.label10.Location = new global::System.Drawing.Point(27, 16);
			this.label10.Name = "label10";
			this.label10.Size = new global::System.Drawing.Size(40, 13);
			this.label10.TabIndex = 21;
			this.label10.Text = "Carrier:";
			this.label9.AutoSize = true;
			this.label9.Location = new global::System.Drawing.Point(259, 16);
			this.label9.Name = "label9";
			this.label9.Size = new global::System.Drawing.Size(47, 13);
			this.label9.TabIndex = 20;
			this.label9.Text = "Number:";
			this.textBoxNumber.Location = new global::System.Drawing.Point(312, 13);
			this.textBoxNumber.Name = "textBoxNumber";
			this.textBoxNumber.Size = new global::System.Drawing.Size(137, 20);
			this.textBoxNumber.TabIndex = 19;
			this.comboBoxCarrier.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCarrier.FormattingEnabled = true;
			this.comboBoxCarrier.Items.AddRange(new object[]
			{
				"AT&T",
				"Boost Mobile",
				"Nextel",
				"Sprint",
				"T-Mobile",
				"US Cellular",
				"Verizon",
				"Virgin Mobile",
				"3 River Wireless",
				"ACS Wireless",
				"Alltel",
				"Bell Mobility",
				"Blue Sky Frog",
				"Bluegrass Cellular",
				"BPL Mobile",
				"Carolina West Wireless",
				"Cellular One",
				"Cellular South",
				"Centennial Wireless",
				"CenturyTel",
				"Clearnet",
				"Comcast",
				"Corr Wireless Communications",
				"Dobson",
				"Edge Wireless",
				"Fido",
				"Golden Telecom",
				"Helio",
				"Houston Cellular",
				"Idea Cellular",
				"Illinois Valley Cellular",
				"Inland Cellular Telephone",
				"MCI",
				"Metrocall",
				"Metrocall 2-way",
				"Metro PCS",
				"Microcell",
				"Midwest Wireless",
				"Mobilcomm",
				"MTS",
				"OnlineBeep",
				"PCS One",
				"President's Choice",
				"Public Service Cellular",
				"Qwest",
				"Rogers AT&T Wireless",
				"Rogers Canada",
				"Satellink",
				"Southwestern Bell",
				"Sumcom",
				"Surewest Communicaitons",
				"Telus",
				"Tracfone",
				"Triton",
				"Unicel",
				"Solo Mobile",
				"Sprint",
				"Sumcom",
				"Surewest Communicaitons",
				"Telus",
				"Triton",
				"Unicel",
				"US Cellular",
				"US West",
				"Virgin Mobile Canada",
				"West Central Wireless",
				"Western Wireless"
			});
			this.comboBoxCarrier.Location = new global::System.Drawing.Point(73, 13);
			this.comboBoxCarrier.Name = "comboBoxCarrier";
			this.comboBoxCarrier.Size = new global::System.Drawing.Size(180, 21);
			this.comboBoxCarrier.TabIndex = 18;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new global::System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new global::System.Drawing.Size(493, 293);
			this.tabControl1.TabIndex = 22;
			this.tabPage1.BackColor = global::System.Drawing.SystemColors.Control;
			this.tabPage1.Controls.Add(this.checkBoxGuestCheckout);
			this.tabPage1.Controls.Add(this.checkBoxRandomSize);
			this.tabPage1.Controls.Add(this.label11);
			this.tabPage1.Controls.Add(this.textBoxSize);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.textBoxEmailAddress);
			this.tabPage1.Controls.Add(this.textBoxNotificationEmail);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Controls.Add(this.textBoxPassword);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.textBoxCollectionKeywords);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.textBoxTweetKeywords);
			this.tabPage1.Controls.Add(this.checkBoxShowPassword);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.textBoxEarlyLinks);
			this.tabPage1.Controls.Add(this.textBoxStyleCodes);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Location = new global::System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new global::System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new global::System.Drawing.Size(485, 267);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Account Details";
			this.checkBoxGuestCheckout.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.checkBoxGuestCheckout.AutoSize = true;
			this.checkBoxGuestCheckout.Location = new global::System.Drawing.Point(381, 8);
			this.checkBoxGuestCheckout.Name = "checkBoxGuestCheckout";
			this.checkBoxGuestCheckout.Size = new global::System.Drawing.Size(97, 17);
			this.checkBoxGuestCheckout.TabIndex = 24;
			this.checkBoxGuestCheckout.Text = "Guest Account";
			this.checkBoxGuestCheckout.UseVisualStyleBackColor = true;
			this.checkBoxGuestCheckout.CheckedChanged += new global::System.EventHandler(this.checkBoxGuestCheckout_CheckedChanged);
			this.checkBoxRandomSize.AutoSize = true;
			this.checkBoxRandomSize.Location = new global::System.Drawing.Point(382, 60);
			this.checkBoxRandomSize.Name = "checkBoxRandomSize";
			this.checkBoxRandomSize.Size = new global::System.Drawing.Size(89, 17);
			this.checkBoxRandomSize.TabIndex = 19;
			this.checkBoxRandomSize.Text = "Random Size";
			this.checkBoxRandomSize.UseVisualStyleBackColor = true;
			this.checkBoxRandomSize.CheckedChanged += new global::System.EventHandler(this.checkBoxRandomSize_CheckedChanged);
			this.label11.AutoSize = true;
			this.label11.Location = new global::System.Drawing.Point(282, 167);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(97, 13);
			this.label11.TabIndex = 18;
			this.label11.Text = "*Required for snkrs";
			this.tabPage2.BackColor = global::System.Drawing.SystemColors.Control;
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.Location = new global::System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new global::System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new global::System.Drawing.Size(485, 267);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Checkout";
			this.groupBox3.Controls.Add(this.linkLabelManageCards);
			this.groupBox3.Controls.Add(this.comboBoxCardProfile);
			this.groupBox3.Controls.Add(this.label19);
			this.groupBox3.Controls.Add(this.checkBoxEnableCCCheckout);
			this.groupBox3.Location = new global::System.Drawing.Point(20, 15);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new global::System.Drawing.Size(460, 88);
			this.groupBox3.TabIndex = 22;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Credit Card Details";
			this.linkLabelManageCards.AutoSize = true;
			this.linkLabelManageCards.Location = new global::System.Drawing.Point(233, 53);
			this.linkLabelManageCards.Name = "linkLabelManageCards";
			this.linkLabelManageCards.Size = new global::System.Drawing.Size(76, 13);
			this.linkLabelManageCards.TabIndex = 22;
			this.linkLabelManageCards.TabStop = true;
			this.linkLabelManageCards.Text = "Manage Cards";
			this.linkLabelManageCards.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelManageCards_LinkClicked);
			this.comboBoxCardProfile.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCardProfile.FormattingEnabled = true;
			this.comboBoxCardProfile.Location = new global::System.Drawing.Point(84, 50);
			this.comboBoxCardProfile.Name = "comboBoxCardProfile";
			this.comboBoxCardProfile.Size = new global::System.Drawing.Size(121, 21);
			this.comboBoxCardProfile.TabIndex = 23;
			this.label19.AutoSize = true;
			this.label19.Location = new global::System.Drawing.Point(9, 53);
			this.label19.Name = "label19";
			this.label19.Size = new global::System.Drawing.Size(65, 13);
			this.label19.TabIndex = 22;
			this.label19.Text = "Select Card:";
			this.checkBoxEnableCCCheckout.AutoSize = true;
			this.checkBoxEnableCCCheckout.Location = new global::System.Drawing.Point(10, 22);
			this.checkBoxEnableCCCheckout.Name = "checkBoxEnableCCCheckout";
			this.checkBoxEnableCCCheckout.Size = new global::System.Drawing.Size(163, 17);
			this.checkBoxEnableCCCheckout.TabIndex = 11;
			this.checkBoxEnableCCCheckout.Text = "Enable Credit Card Checkout";
			this.checkBoxEnableCCCheckout.UseVisualStyleBackColor = true;
			this.label12.AutoSize = true;
			this.label12.Location = new global::System.Drawing.Point(11, 316);
			this.label12.Name = "label12";
			this.label12.Size = new global::System.Drawing.Size(334, 13);
			this.label12.TabIndex = 24;
			this.label12.Text = "IMPORTANT: Enter only style code (do not enter link) for snkrs tasks.";
			base.AcceptButton = this.buttonSave;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(512, 344);
			base.Controls.Add(this.label12);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.buttonSave);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximumSize = new global::System.Drawing.Size(1080, 1000);
			this.MinimumSize = new global::System.Drawing.Size(400, 230);
			base.Name = "AccountDetailsForm";
			this.Text = "Account Details";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000110 RID: 272
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000111 RID: 273
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000112 RID: 274
		private global::System.Windows.Forms.TextBox textBoxEmailAddress;

		// Token: 0x04000113 RID: 275
		private global::System.Windows.Forms.TextBox textBoxPassword;

		// Token: 0x04000114 RID: 276
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000115 RID: 277
		private global::System.Windows.Forms.TextBox textBoxSize;

		// Token: 0x04000116 RID: 278
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000117 RID: 279
		private global::System.Windows.Forms.TextBox textBoxTweetKeywords;

		// Token: 0x04000118 RID: 280
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000119 RID: 281
		private global::System.Windows.Forms.TextBox textBoxEarlyLinks;

		// Token: 0x0400011A RID: 282
		private global::System.Windows.Forms.Label label5;

		// Token: 0x0400011B RID: 283
		private global::System.Windows.Forms.TextBox textBoxStyleCodes;

		// Token: 0x0400011C RID: 284
		private global::System.Windows.Forms.Label label6;

		// Token: 0x0400011D RID: 285
		private global::System.Windows.Forms.Button buttonSave;

		// Token: 0x0400011E RID: 286
		private global::System.Windows.Forms.CheckBox checkBoxShowPassword;

		// Token: 0x0400011F RID: 287
		private global::System.Windows.Forms.TextBox textBoxCollectionKeywords;

		// Token: 0x04000120 RID: 288
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000121 RID: 289
		private global::System.Windows.Forms.TextBox textBoxNotificationEmail;

		// Token: 0x04000122 RID: 290
		private global::System.Windows.Forms.Label label8;

		// Token: 0x04000123 RID: 291
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x04000124 RID: 292
		private global::System.Windows.Forms.Label label9;

		// Token: 0x04000125 RID: 293
		private global::System.Windows.Forms.TextBox textBoxNumber;

		// Token: 0x04000126 RID: 294
		private global::System.Windows.Forms.ComboBox comboBoxCarrier;

		// Token: 0x04000127 RID: 295
		private global::System.Windows.Forms.Label label10;

		// Token: 0x04000128 RID: 296
		private global::System.Windows.Forms.TabControl tabControl1;

		// Token: 0x04000129 RID: 297
		private global::System.Windows.Forms.TabPage tabPage1;

		// Token: 0x0400012A RID: 298
		private global::System.Windows.Forms.TabPage tabPage2;

		// Token: 0x0400012B RID: 299
		private global::System.Windows.Forms.GroupBox groupBox3;

		// Token: 0x0400012C RID: 300
		private global::System.Windows.Forms.LinkLabel linkLabelManageCards;

		// Token: 0x0400012D RID: 301
		private global::System.Windows.Forms.ComboBox comboBoxCardProfile;

		// Token: 0x0400012E RID: 302
		private global::System.Windows.Forms.Label label19;

		// Token: 0x0400012F RID: 303
		private global::System.Windows.Forms.CheckBox checkBoxEnableCCCheckout;

		// Token: 0x04000130 RID: 304
		private global::System.Windows.Forms.Label label11;

		// Token: 0x04000131 RID: 305
		private global::System.Windows.Forms.CheckBox checkBoxRandomSize;

		// Token: 0x04000132 RID: 306
		private global::System.Windows.Forms.CheckBox checkBoxGuestCheckout;

		// Token: 0x04000133 RID: 307
		private global::System.Windows.Forms.Label label12;
	}
}
