namespace Better_Nike_Bot
{
	// Token: 0x0200001B RID: 27
	public partial class ScheduleForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600010C RID: 268 RVA: 0x000029E4 File Offset: 0x00000BE4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00013C7C File Offset: 0x00011E7C
		private void InitializeComponent()
		{
			this.buttonSave = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.dateTimePicker1 = new global::System.Windows.Forms.DateTimePicker();
			base.SuspendLayout();
			this.buttonSave.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonSave.Location = new global::System.Drawing.Point(399, 61);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new global::System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 11;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new global::System.EventHandler(this.buttonSave_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(462, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Set the date and time when you want to start the bot. This follows your computer's date and time.";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(12, 33);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(301, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Make sure you schedule for at least 5 minutes before the drop.";
			this.dateTimePicker1.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new global::System.Drawing.Point(15, 63);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new global::System.Drawing.Size(200, 20);
			this.dateTimePicker1.TabIndex = 14;
			base.AcceptButton = this.buttonSave;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(486, 96);
			base.Controls.Add(this.dateTimePicker1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.buttonSave);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximumSize = new global::System.Drawing.Size(1080, 400);
			this.MinimumSize = new global::System.Drawing.Size(200, 10);
			base.Name = "ScheduleForm";
			this.Text = "Start Later";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000101 RID: 257
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000102 RID: 258
		private global::System.Windows.Forms.Button buttonSave;

		// Token: 0x04000103 RID: 259
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000104 RID: 260
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000105 RID: 261
		private global::System.Windows.Forms.DateTimePicker dateTimePicker1;
	}
}
