namespace Better_Nike_Bot
{
	// Token: 0x02000026 RID: 38
	public partial class LinkScheduler : global::System.Windows.Forms.Form
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00002FC8 File Offset: 0x000011C8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00020E0C File Offset: 0x0001F00C
		private void InitializeComponent()
		{
			this.dataGridView1 = new global::System.Windows.Forms.DataGridView();
			this.buttonSave = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.dataGridView1.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new global::System.Drawing.Point(12, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new global::System.Drawing.Size(308, 203);
			this.dataGridView1.TabIndex = 0;
			this.buttonSave.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonSave.Location = new global::System.Drawing.Point(245, 224);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new global::System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 12;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new global::System.EventHandler(this.buttonSave_Click);
			base.AcceptButton = this.buttonSave;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(332, 259);
			base.Controls.Add(this.buttonSave);
			base.Controls.Add(this.dataGridView1);
			base.Name = "LinkScheduler";
			this.Text = "Link Booster";
			base.Load += new global::System.EventHandler(this.LinkScheduler_Load);
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x040001A2 RID: 418
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040001A3 RID: 419
		private global::System.Windows.Forms.DataGridView dataGridView1;

		// Token: 0x040001A4 RID: 420
		private global::System.Windows.Forms.Button buttonSave;
	}
}
