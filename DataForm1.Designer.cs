namespace Better_Nike_Bot
{
	// Token: 0x02000010 RID: 16
	public partial class DataForm1 : global::System.Windows.Forms.Form
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000026F1 File Offset: 0x000008F1
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000B664 File Offset: 0x00009864
		private void InitializeComponent()
		{
			this.labelMessage = new global::System.Windows.Forms.Label();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.labelLines = new global::System.Windows.Forms.Label();
			this.checkBox1 = new global::System.Windows.Forms.CheckBox();
			this.radioButton2 = new global::System.Windows.Forms.RadioButton();
			this.radioButton1 = new global::System.Windows.Forms.RadioButton();
			this.checkBox2 = new global::System.Windows.Forms.CheckBox();
			this.label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.labelMessage.AutoSize = true;
			this.labelMessage.Location = new global::System.Drawing.Point(12, 9);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new global::System.Drawing.Size(53, 13);
			this.labelMessage.TabIndex = 0;
			this.labelMessage.Text = "Message:";
			this.textBox1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBox1.Location = new global::System.Drawing.Point(15, 29);
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new global::System.Drawing.Size(500, 20);
			this.textBox1.TabIndex = 1;
			this.textBox1.TextChanged += new global::System.EventHandler(this.textBox1_TextChanged);
			this.textBox1.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
			this.buttonOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.buttonOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new global::System.Drawing.Point(15, 91);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new global::System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.labelLines.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.labelLines.AutoSize = true;
			this.labelLines.Location = new global::System.Drawing.Point(105, 96);
			this.labelLines.Name = "labelLines";
			this.labelLines.Size = new global::System.Drawing.Size(0, 13);
			this.labelLines.TabIndex = 3;
			this.checkBox1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new global::System.Drawing.Point(14, 55);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new global::System.Drawing.Size(80, 17);
			this.checkBox1.TabIndex = 4;
			this.checkBox1.Text = "checkBox1";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.Visible = false;
			this.checkBox1.CheckedChanged += new global::System.EventHandler(this.checkBox1_CheckedChanged);
			this.radioButton2.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.radioButton2.AutoSize = true;
			this.radioButton2.Enabled = false;
			this.radioButton2.Location = new global::System.Drawing.Point(376, 78);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new global::System.Drawing.Size(116, 17);
			this.radioButton2.TabIndex = 11;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Test for Nike Snkrs";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new global::System.EventHandler(this.radioButton2_CheckedChanged);
			this.radioButton1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.radioButton1.AutoSize = true;
			this.radioButton1.Enabled = false;
			this.radioButton1.Location = new global::System.Drawing.Point(376, 55);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new global::System.Drawing.Size(109, 17);
			this.radioButton1.TabIndex = 10;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Test for Nike.com";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new global::System.EventHandler(this.radioButton1_CheckedChanged);
			this.checkBox2.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new global::System.Drawing.Point(200, 55);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new global::System.Drawing.Size(80, 17);
			this.checkBox2.TabIndex = 9;
			this.checkBox2.Text = "checkBox2";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.Visible = false;
			this.checkBox2.CheckedChanged += new global::System.EventHandler(this.checkBox2_CheckedChanged);
			this.label1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(109, 97);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(0, 13);
			this.label1.TabIndex = 8;
			base.AcceptButton = this.buttonOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(527, 120);
			base.Controls.Add(this.radioButton2);
			base.Controls.Add(this.radioButton1);
			base.Controls.Add(this.checkBox2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.labelLines);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.labelMessage);
			base.Name = "DataForm1";
			this.Text = "Title";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000BD RID: 189
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000BE RID: 190
		private global::System.Windows.Forms.Label labelMessage;

		// Token: 0x040000BF RID: 191
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x040000C0 RID: 192
		private global::System.Windows.Forms.Button buttonOK;

		// Token: 0x040000C1 RID: 193
		private global::System.Windows.Forms.Label labelLines;

		// Token: 0x040000C2 RID: 194
		private global::System.Windows.Forms.CheckBox checkBox1;

		// Token: 0x040000C3 RID: 195
		private global::System.Windows.Forms.RadioButton radioButton2;

		// Token: 0x040000C4 RID: 196
		private global::System.Windows.Forms.RadioButton radioButton1;

		// Token: 0x040000C5 RID: 197
		private global::System.Windows.Forms.CheckBox checkBox2;

		// Token: 0x040000C6 RID: 198
		private global::System.Windows.Forms.Label label1;
	}
}
