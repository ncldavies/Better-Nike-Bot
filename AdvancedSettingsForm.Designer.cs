namespace Better_Nike_Bot
{
	// Token: 0x02000023 RID: 35
	public partial class AdvancedSettingsForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00002D94 File Offset: 0x00000F94
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0001FD80 File Offset: 0x0001DF80
		private void InitializeComponent()
		{
			this.groupBox2 = new global::System.Windows.Forms.GroupBox();
			this.textBoxTextTemplate = new global::System.Windows.Forms.TextBox();
			this.textBoxSubject = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.textBoxSendFromName = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.textBoxSendFromEmail = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.buttonSave = new global::System.Windows.Forms.Button();
			this.linkLabel1 = new global::System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new global::System.Windows.Forms.LinkLabel();
			this.textBoxServiceLink = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.textBoxServiceName = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.textBoxServiceLogo = new global::System.Windows.Forms.TextBox();
			this.label6 = new global::System.Windows.Forms.Label();
			this.textBoxServiceTwitter = new global::System.Windows.Forms.TextBox();
			this.label7 = new global::System.Windows.Forms.Label();
			this.linkLabel3 = new global::System.Windows.Forms.LinkLabel();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.groupBox2.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBox2.Controls.Add(this.textBoxTextTemplate);
			this.groupBox2.Location = new global::System.Drawing.Point(18, 231);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new global::System.Drawing.Size(530, 89);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Text Template";
			this.textBoxTextTemplate.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.textBoxTextTemplate.Location = new global::System.Drawing.Point(3, 16);
			this.textBoxTextTemplate.Multiline = true;
			this.textBoxTextTemplate.Name = "textBoxTextTemplate";
			this.textBoxTextTemplate.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.textBoxTextTemplate.Size = new global::System.Drawing.Size(524, 70);
			this.textBoxTextTemplate.TabIndex = 1;
			this.textBoxSubject.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxSubject.Location = new global::System.Drawing.Point(110, 205);
			this.textBoxSubject.Name = "textBoxSubject";
			this.textBoxSubject.Size = new global::System.Drawing.Size(432, 20);
			this.textBoxSubject.TabIndex = 9;
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(58, 208);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(46, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Subject:";
			this.textBoxSendFromName.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxSendFromName.Location = new global::System.Drawing.Point(110, 153);
			this.textBoxSendFromName.Name = "textBoxSendFromName";
			this.textBoxSendFromName.Size = new global::System.Drawing.Size(432, 20);
			this.textBoxSendFromName.TabIndex = 11;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(12, 156);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(92, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Send From Name:";
			this.textBoxSendFromEmail.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxSendFromEmail.Location = new global::System.Drawing.Point(110, 179);
			this.textBoxSendFromEmail.Name = "textBoxSendFromEmail";
			this.textBoxSendFromEmail.Size = new global::System.Drawing.Size(432, 20);
			this.textBoxSendFromEmail.TabIndex = 13;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(15, 182);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(89, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Send From Email:";
			this.buttonSave.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonSave.Location = new global::System.Drawing.Point(467, 341);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new global::System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 15;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new global::System.EventHandler(this.buttonSave_Click);
			this.linkLabel1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new global::System.Drawing.Point(12, 346);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new global::System.Drawing.Size(103, 13);
			this.linkLabel1.TabIndex = 16;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Test Email Template";
			this.linkLabel1.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			this.linkLabel2.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new global::System.Drawing.Point(160, 346);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new global::System.Drawing.Size(99, 13);
			this.linkLabel2.TabIndex = 17;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Test Text Template";
			this.linkLabel2.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			this.textBoxServiceLink.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxServiceLink.Location = new global::System.Drawing.Point(110, 42);
			this.textBoxServiceLink.Name = "textBoxServiceLink";
			this.textBoxServiceLink.Size = new global::System.Drawing.Size(432, 20);
			this.textBoxServiceLink.TabIndex = 22;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(35, 45);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(69, 13);
			this.label3.TabIndex = 23;
			this.label3.Text = "Service Link:";
			this.textBoxServiceName.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxServiceName.Location = new global::System.Drawing.Point(110, 16);
			this.textBoxServiceName.Name = "textBoxServiceName";
			this.textBoxServiceName.Size = new global::System.Drawing.Size(432, 20);
			this.textBoxServiceName.TabIndex = 20;
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(27, 19);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(77, 13);
			this.label4.TabIndex = 21;
			this.label4.Text = "Service Name:";
			this.textBoxServiceLogo.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxServiceLogo.Location = new global::System.Drawing.Point(110, 68);
			this.textBoxServiceLogo.Name = "textBoxServiceLogo";
			this.textBoxServiceLogo.Size = new global::System.Drawing.Size(432, 20);
			this.textBoxServiceLogo.TabIndex = 18;
			this.label6.AutoSize = true;
			this.label6.Location = new global::System.Drawing.Point(15, 71);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(89, 13);
			this.label6.TabIndex = 19;
			this.label6.Text = "Service Logo Url:";
			this.textBoxServiceTwitter.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxServiceTwitter.Location = new global::System.Drawing.Point(110, 94);
			this.textBoxServiceTwitter.Name = "textBoxServiceTwitter";
			this.textBoxServiceTwitter.Size = new global::System.Drawing.Size(432, 20);
			this.textBoxServiceTwitter.TabIndex = 24;
			this.label7.AutoSize = true;
			this.label7.Location = new global::System.Drawing.Point(23, 97);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(81, 13);
			this.label7.TabIndex = 25;
			this.label7.Text = "Service Twitter:";
			this.linkLabel3.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.Location = new global::System.Drawing.Point(299, 346);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new global::System.Drawing.Size(106, 13);
			this.linkLabel3.TabIndex = 26;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "View Available Fields";
			this.linkLabel3.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
			base.AcceptButton = this.buttonSave;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(554, 371);
			base.Controls.Add(this.linkLabel3);
			base.Controls.Add(this.textBoxServiceTwitter);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.textBoxServiceLink);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.textBoxServiceName);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.textBoxServiceLogo);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.linkLabel2);
			base.Controls.Add(this.linkLabel1);
			base.Controls.Add(this.buttonSave);
			base.Controls.Add(this.textBoxSendFromEmail);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBoxSendFromName);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBoxSubject);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "AdvancedSettingsForm";
			this.Text = "Notification Settings";
			base.Load += new global::System.EventHandler(this.AdvancedSettingsForm_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400016E RID: 366
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400016F RID: 367
		private global::System.Windows.Forms.GroupBox groupBox2;

		// Token: 0x04000170 RID: 368
		private global::System.Windows.Forms.TextBox textBoxTextTemplate;

		// Token: 0x04000171 RID: 369
		private global::System.Windows.Forms.TextBox textBoxSubject;

		// Token: 0x04000172 RID: 370
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000173 RID: 371
		private global::System.Windows.Forms.TextBox textBoxSendFromName;

		// Token: 0x04000174 RID: 372
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000175 RID: 373
		private global::System.Windows.Forms.TextBox textBoxSendFromEmail;

		// Token: 0x04000176 RID: 374
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000177 RID: 375
		private global::System.Windows.Forms.Button buttonSave;

		// Token: 0x04000178 RID: 376
		private global::System.Windows.Forms.LinkLabel linkLabel1;

		// Token: 0x04000179 RID: 377
		private global::System.Windows.Forms.LinkLabel linkLabel2;

		// Token: 0x0400017A RID: 378
		private global::System.Windows.Forms.TextBox textBoxServiceLink;

		// Token: 0x0400017B RID: 379
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400017C RID: 380
		private global::System.Windows.Forms.TextBox textBoxServiceName;

		// Token: 0x0400017D RID: 381
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400017E RID: 382
		private global::System.Windows.Forms.TextBox textBoxServiceLogo;

		// Token: 0x0400017F RID: 383
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000180 RID: 384
		private global::System.Windows.Forms.TextBox textBoxServiceTwitter;

		// Token: 0x04000181 RID: 385
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000182 RID: 386
		private global::System.Windows.Forms.LinkLabel linkLabel3;
	}
}
