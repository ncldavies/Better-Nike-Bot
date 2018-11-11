namespace Better_Nike_Bot
{
	// Token: 0x0200000D RID: 13
	public partial class CreateAccountsForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600005D RID: 93 RVA: 0x0000238F File Offset: 0x0000058F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00009D68 File Offset: 0x00007F68
		private void InitializeComponent()
		{
			this.textBoxPassword = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.textBoxEmailAddress = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.textBoxName = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.numericUpDownNumberOfAccounts = new global::System.Windows.Forms.NumericUpDown();
			this.checkBoxRandomName = new global::System.Windows.Forms.CheckBox();
			this.label6 = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.label10 = new global::System.Windows.Forms.Label();
			this.buttonStart = new global::System.Windows.Forms.Button();
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.textBoxSMSAPI = new global::System.Windows.Forms.TextBox();
			this.label8 = new global::System.Windows.Forms.Label();
			this.textBoxSMSEmail = new global::System.Windows.Forms.TextBox();
			this.label9 = new global::System.Windows.Forms.Label();
			this.linkLabel1 = new global::System.Windows.Forms.LinkLabel();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label11 = new global::System.Windows.Forms.Label();
			this.checkBox1 = new global::System.Windows.Forms.CheckBox();
			this.label12 = new global::System.Windows.Forms.Label();
			this.checkBoxPlusSign = new global::System.Windows.Forms.CheckBox();
			this.label13 = new global::System.Windows.Forms.Label();
			this.label15 = new global::System.Windows.Forms.Label();
			this.label14 = new global::System.Windows.Forms.Label();
			this.label16 = new global::System.Windows.Forms.Label();
			this.label17 = new global::System.Windows.Forms.Label();
			this.linkLabel2 = new global::System.Windows.Forms.LinkLabel();
			this.label18 = new global::System.Windows.Forms.Label();
			this.linkLabel3 = new global::System.Windows.Forms.LinkLabel();
			this.label19 = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDownNumberOfAccounts).BeginInit();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.textBoxPassword.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxPassword.Location = new global::System.Drawing.Point(96, 40);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.Size = new global::System.Drawing.Size(171, 20);
			this.textBoxPassword.TabIndex = 5;
			this.textBoxPassword.Text = "MyPassword123";
			this.textBoxPassword.Enter += new global::System.EventHandler(this.textBoxPassword_Enter);
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(30, 46);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(56, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Password:";
			this.textBoxEmailAddress.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxEmailAddress.Location = new global::System.Drawing.Point(96, 14);
			this.textBoxEmailAddress.Name = "textBoxEmailAddress";
			this.textBoxEmailAddress.Size = new global::System.Drawing.Size(242, 20);
			this.textBoxEmailAddress.TabIndex = 4;
			this.textBoxEmailAddress.Text = "teambnb@gmail.com";
			this.textBoxEmailAddress.Enter += new global::System.EventHandler(this.textBoxEmailAddress_Enter);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(10, 17);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(76, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Email Address:";
			this.textBoxName.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxName.Location = new global::System.Drawing.Point(96, 66);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new global::System.Drawing.Size(193, 20);
			this.textBoxName.TabIndex = 9;
			this.textBoxName.Text = "Team Bnb";
			this.textBoxName.Enter += new global::System.EventHandler(this.textBoxName_Enter);
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(46, 72);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(38, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Name:";
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(-1, 96);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(87, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "No. of Accounts:";
			this.numericUpDownNumberOfAccounts.Location = new global::System.Drawing.Point(96, 94);
			global::System.Windows.Forms.NumericUpDown numericUpDown = this.numericUpDownNumberOfAccounts;
			int[] array = new int[4];
			array[0] = 99999;
			numericUpDown.Maximum = new decimal(array);
			global::System.Windows.Forms.NumericUpDown numericUpDown2 = this.numericUpDownNumberOfAccounts;
			int[] array2 = new int[4];
			array2[0] = 1;
			numericUpDown2.Minimum = new decimal(array2);
			this.numericUpDownNumberOfAccounts.Name = "numericUpDownNumberOfAccounts";
			this.numericUpDownNumberOfAccounts.Size = new global::System.Drawing.Size(88, 20);
			this.numericUpDownNumberOfAccounts.TabIndex = 12;
			global::System.Windows.Forms.NumericUpDown numericUpDown3 = this.numericUpDownNumberOfAccounts;
			int[] array3 = new int[4];
			array3[0] = 5;
			numericUpDown3.Value = new decimal(array3);
			this.checkBoxRandomName.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.checkBoxRandomName.AutoSize = true;
			this.checkBoxRandomName.Location = new global::System.Drawing.Point(305, 68);
			this.checkBoxRandomName.Name = "checkBoxRandomName";
			this.checkBoxRandomName.Size = new global::System.Drawing.Size(144, 17);
			this.checkBoxRandomName.TabIndex = 14;
			this.checkBoxRandomName.Text = "Generate Random Name";
			this.checkBoxRandomName.UseVisualStyleBackColor = true;
			this.checkBoxRandomName.CheckedChanged += new global::System.EventHandler(this.checkBoxRandomName_CheckedChanged);
			this.label6.AutoSize = true;
			this.label6.Location = new global::System.Drawing.Point(12, 279);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(62, 13);
			this.label6.TabIndex = 15;
			this.label6.Text = "Information:";
			this.label7.AutoSize = true;
			this.label7.Location = new global::System.Drawing.Point(30, 298);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(448, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = "1. Accounts generated will be added to the bot. You can then export the accounts from there.";
			this.label10.AutoSize = true;
			this.label10.Location = new global::System.Drawing.Point(30, 321);
			this.label10.Name = "label10";
			this.label10.Size = new global::System.Drawing.Size(388, 13);
			this.label10.TabIndex = 19;
			this.label10.Text = "2. If you don't generate a random name, same name will be used on all accounts.";
			this.buttonStart.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonStart.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.buttonStart.Location = new global::System.Drawing.Point(331, 513);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new global::System.Drawing.Size(150, 23);
			this.buttonStart.TabIndex = 21;
			this.buttonStart.Text = "Start Account Creation";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new global::System.EventHandler(this.buttonStart_Click);
			this.groupBox1.Controls.Add(this.label19);
			this.groupBox1.Controls.Add(this.linkLabel3);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.textBoxSMSAPI);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.textBoxSMSEmail);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.linkLabel1);
			this.groupBox1.Location = new global::System.Drawing.Point(12, 151);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new global::System.Drawing.Size(469, 114);
			this.groupBox1.TabIndex = 24;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "SMS Verification Service";
			this.textBoxSMSAPI.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxSMSAPI.Location = new global::System.Drawing.Point(104, 55);
			this.textBoxSMSAPI.Name = "textBoxSMSAPI";
			this.textBoxSMSAPI.Size = new global::System.Drawing.Size(258, 20);
			this.textBoxSMSAPI.TabIndex = 9;
			this.textBoxSMSAPI.Text = "xxxxx";
			this.label8.AutoSize = true;
			this.label8.Location = new global::System.Drawing.Point(6, 58);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(88, 13);
			this.label8.TabIndex = 10;
			this.label8.Text = "API Key (Token):";
			this.textBoxSMSEmail.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxSMSEmail.Location = new global::System.Drawing.Point(104, 29);
			this.textBoxSMSEmail.Name = "textBoxSMSEmail";
			this.textBoxSMSEmail.Size = new global::System.Drawing.Size(258, 20);
			this.textBoxSMSEmail.TabIndex = 8;
			this.textBoxSMSEmail.Text = "teambnb@gmail.com";
			this.label9.AutoSize = true;
			this.label9.Location = new global::System.Drawing.Point(18, 32);
			this.label9.Name = "label9";
			this.label9.Size = new global::System.Drawing.Size(76, 13);
			this.label9.TabIndex = 7;
			this.label9.Text = "Email Address:";
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new global::System.Drawing.Point(368, 32);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new global::System.Drawing.Size(71, 13);
			this.linkLabel1.TabIndex = 0;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Sign Up Here";
			this.linkLabel1.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(30, 343);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(455, 13);
			this.label3.TabIndex = 25;
			this.label3.Text = "3. Signup for the sms verification service and enter the email you use to signup and the api key.";
			this.label11.AutoSize = true;
			this.label11.Location = new global::System.Drawing.Point(30, 365);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(426, 13);
			this.label11.TabIndex = 26;
			this.label11.Text = "4. The bot will create accounts for the nike locale that you select in the main bot window.";
			this.checkBox1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new global::System.Drawing.Point(19, 128);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new global::System.Drawing.Size(136, 17);
			this.checkBox1.TabIndex = 27;
			this.checkBox1.Text = "Disable sms verification";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new global::System.EventHandler(this.checkBox1_CheckedChanged);
			this.label12.AutoSize = true;
			this.label12.Location = new global::System.Drawing.Point(30, 386);
			this.label12.Name = "label12";
			this.label12.Size = new global::System.Drawing.Size(290, 13);
			this.label12.TabIndex = 28;
			this.label12.Text = "5. The bot will automatically use proxies you enter in the bot.";
			this.checkBoxPlusSign.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.checkBoxPlusSign.AutoSize = true;
			this.checkBoxPlusSign.Location = new global::System.Drawing.Point(344, 17);
			this.checkBoxPlusSign.Name = "checkBoxPlusSign";
			this.checkBoxPlusSign.Size = new global::System.Drawing.Size(122, 17);
			this.checkBoxPlusSign.TabIndex = 29;
			this.checkBoxPlusSign.Text = "Do not use plus sign";
			this.checkBoxPlusSign.UseVisualStyleBackColor = true;
			this.label13.AutoSize = true;
			this.label13.Location = new global::System.Drawing.Point(30, 409);
			this.label13.Name = "label13";
			this.label13.Size = new global::System.Drawing.Size(446, 13);
			this.label13.TabIndex = 30;
			this.label13.Text = "6. By default, the bot adds plus sign to account email address to make them unique. This way";
			this.label15.AutoSize = true;
			this.label15.Location = new global::System.Drawing.Point(45, 445);
			this.label15.Name = "label15";
			this.label15.Size = new global::System.Drawing.Size(446, 13);
			this.label15.TabIndex = 32;
			this.label15.Text = "teambnb@gmail.com and teambnb+1234@gmail.com both go to the same gmail account but ";
			this.label14.AutoSize = true;
			this.label14.Location = new global::System.Drawing.Point(44, 427);
			this.label14.Name = "label14";
			this.label14.Size = new global::System.Drawing.Size(438, 13);
			this.label14.TabIndex = 31;
			this.label14.Text = "you can check your nike emails from all accounts in a single gmail account. This is because";
			this.label16.AutoSize = true;
			this.label16.Location = new global::System.Drawing.Point(45, 462);
			this.label16.Name = "label16";
			this.label16.Size = new global::System.Drawing.Size(313, 13);
			this.label16.TabIndex = 33;
			this.label16.Text = "are unique in nike's eyes. You can choose to disable this feature.";
			this.label17.AutoSize = true;
			this.label17.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label17.Location = new global::System.Drawing.Point(30, 485);
			this.label17.Name = "label17";
			this.label17.Size = new global::System.Drawing.Size(313, 13);
			this.label17.TabIndex = 34;
			this.label17.Text = "7. Regular users can only create 5 accounts per day. ";
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new global::System.Drawing.Point(341, 485);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new global::System.Drawing.Size(118, 13);
			this.linkLabel2.TabIndex = 35;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Click here to learn more";
			this.linkLabel2.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			this.label18.AutoSize = true;
			this.label18.Location = new global::System.Drawing.Point(6, 89);
			this.label18.Name = "label18";
			this.label18.Size = new global::System.Drawing.Size(83, 13);
			this.label18.TabIndex = 11;
			this.label18.Text = "Before first use, ";
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.Location = new global::System.Drawing.Point(84, 89);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new global::System.Drawing.Size(53, 13);
			this.linkLabel3.TabIndex = 12;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "click here";
			this.linkLabel3.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
			this.label19.AutoSize = true;
			this.label19.Location = new global::System.Drawing.Point(135, 89);
			this.label19.Name = "label19";
			this.label19.Size = new global::System.Drawing.Size(163, 13);
			this.label19.TabIndex = 13;
			this.label19.Text = "and add Nike (#462) to favorites.";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(493, 548);
			base.Controls.Add(this.linkLabel2);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.label16);
			base.Controls.Add(this.label15);
			base.Controls.Add(this.label14);
			base.Controls.Add(this.label13);
			base.Controls.Add(this.checkBoxPlusSign);
			base.Controls.Add(this.label12);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.label11);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.buttonStart);
			base.Controls.Add(this.label10);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.checkBoxRandomName);
			base.Controls.Add(this.numericUpDownNumberOfAccounts);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.textBoxName);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.textBoxPassword);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBoxEmailAddress);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "CreateAccountsForm";
			this.Text = "Create Accounts";
			base.Load += new global::System.EventHandler(this.CreateAccountsForm_Load);
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDownNumberOfAccounts).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000078 RID: 120
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000079 RID: 121
		private global::System.Windows.Forms.TextBox textBoxPassword;

		// Token: 0x0400007A RID: 122
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400007B RID: 123
		private global::System.Windows.Forms.TextBox textBoxEmailAddress;

		// Token: 0x0400007C RID: 124
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400007D RID: 125
		private global::System.Windows.Forms.TextBox textBoxName;

		// Token: 0x0400007E RID: 126
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400007F RID: 127
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000080 RID: 128
		private global::System.Windows.Forms.NumericUpDown numericUpDownNumberOfAccounts;

		// Token: 0x04000081 RID: 129
		private global::System.Windows.Forms.CheckBox checkBoxRandomName;

		// Token: 0x04000082 RID: 130
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000083 RID: 131
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000084 RID: 132
		private global::System.Windows.Forms.Label label10;

		// Token: 0x04000085 RID: 133
		private global::System.Windows.Forms.Button buttonStart;

		// Token: 0x04000086 RID: 134
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x04000087 RID: 135
		private global::System.Windows.Forms.LinkLabel linkLabel1;

		// Token: 0x04000088 RID: 136
		private global::System.Windows.Forms.TextBox textBoxSMSAPI;

		// Token: 0x04000089 RID: 137
		private global::System.Windows.Forms.Label label8;

		// Token: 0x0400008A RID: 138
		private global::System.Windows.Forms.TextBox textBoxSMSEmail;

		// Token: 0x0400008B RID: 139
		private global::System.Windows.Forms.Label label9;

		// Token: 0x0400008C RID: 140
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400008D RID: 141
		private global::System.Windows.Forms.Label label11;

		// Token: 0x0400008E RID: 142
		private global::System.Windows.Forms.CheckBox checkBox1;

		// Token: 0x0400008F RID: 143
		private global::System.Windows.Forms.Label label12;

		// Token: 0x04000090 RID: 144
		private global::System.Windows.Forms.CheckBox checkBoxPlusSign;

		// Token: 0x04000091 RID: 145
		private global::System.Windows.Forms.Label label13;

		// Token: 0x04000092 RID: 146
		private global::System.Windows.Forms.Label label15;

		// Token: 0x04000093 RID: 147
		private global::System.Windows.Forms.Label label14;

		// Token: 0x04000094 RID: 148
		private global::System.Windows.Forms.Label label16;

		// Token: 0x04000095 RID: 149
		private global::System.Windows.Forms.Label label17;

		// Token: 0x04000096 RID: 150
		private global::System.Windows.Forms.LinkLabel linkLabel2;

		// Token: 0x04000097 RID: 151
		private global::System.Windows.Forms.Label label19;

		// Token: 0x04000098 RID: 152
		private global::System.Windows.Forms.LinkLabel linkLabel3;

		// Token: 0x04000099 RID: 153
		private global::System.Windows.Forms.Label label18;
	}
}
