namespace Better_Nike_Bot
{
	// Token: 0x0200001F RID: 31
	public partial class AddedProductsForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00002BBC File Offset: 0x00000DBC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00015BFC File Offset: 0x00013DFC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.objectListView1 = new global::BrightIdeasSoftware.ObjectListView();
			this.olvColumn8 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn9 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn2 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn10 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn1 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn3 = new global::BrightIdeasSoftware.OLVColumn();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyUsernameToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.copyPasswordToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.copySnkrsUrlToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.openInBrowserToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.exportToJsonToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			((global::System.ComponentModel.ISupportInitialize)this.objectListView1).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.objectListView1.AllColumns.Add(this.olvColumn8);
			this.objectListView1.AllColumns.Add(this.olvColumn9);
			this.objectListView1.AllColumns.Add(this.olvColumn2);
			this.objectListView1.AllColumns.Add(this.olvColumn10);
			this.objectListView1.AllColumns.Add(this.olvColumn1);
			this.objectListView1.AllColumns.Add(this.olvColumn3);
			this.objectListView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.olvColumn8,
				this.olvColumn9,
				this.olvColumn2,
				this.olvColumn10,
				this.olvColumn1,
				this.olvColumn3
			});
			this.objectListView1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.objectListView1.Location = new global::System.Drawing.Point(0, 0);
			this.objectListView1.Name = "objectListView1";
			this.objectListView1.ShowGroups = false;
			this.objectListView1.Size = new global::System.Drawing.Size(587, 258);
			this.objectListView1.TabIndex = 1;
			this.objectListView1.UseCompatibleStateImageBehavior = false;
			this.objectListView1.UseFiltering = true;
			this.objectListView1.View = global::System.Windows.Forms.View.Details;
			this.objectListView1.CellRightClick += new global::System.EventHandler<global::BrightIdeasSoftware.CellRightClickEventArgs>(this.objectListView1_CellRightClick);
			this.olvColumn8.AspectName = "TimeStamp";
			this.olvColumn8.CellPadding = null;
			this.olvColumn8.Text = "DateTime";
			this.olvColumn8.Width = 100;
			this.olvColumn9.AspectName = "ProductName";
			this.olvColumn9.CellPadding = null;
			this.olvColumn9.Text = "Product Name";
			this.olvColumn9.Width = 174;
			this.olvColumn2.AspectName = "StyleCode";
			this.olvColumn2.CellPadding = null;
			this.olvColumn2.Text = "Style Code";
			this.olvColumn10.AspectName = "ProductType";
			this.olvColumn10.CellPadding = null;
			this.olvColumn10.Text = "Product Type";
			this.olvColumn10.Width = 108;
			this.olvColumn1.AspectName = "EmailAddress";
			this.olvColumn1.CellPadding = null;
			this.olvColumn1.Text = "Email Address";
			this.olvColumn1.Width = 101;
			this.olvColumn3.AspectName = "Size";
			this.olvColumn3.CellPadding = null;
			this.olvColumn3.Text = "Size";
			this.olvColumn3.Width = 41;
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.copyUsernameToolStripMenuItem,
				this.copyPasswordToolStripMenuItem,
				this.copySnkrsUrlToolStripMenuItem,
				this.openInBrowserToolStripMenuItem,
				this.exportToJsonToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new global::System.Drawing.Size(162, 136);
			this.copyUsernameToolStripMenuItem.Name = "copyUsernameToolStripMenuItem";
			this.copyUsernameToolStripMenuItem.Size = new global::System.Drawing.Size(161, 22);
			this.copyUsernameToolStripMenuItem.Text = "Copy Username";
			this.copyUsernameToolStripMenuItem.Click += new global::System.EventHandler(this.copyUsernameToolStripMenuItem_Click);
			this.copyPasswordToolStripMenuItem.Name = "copyPasswordToolStripMenuItem";
			this.copyPasswordToolStripMenuItem.Size = new global::System.Drawing.Size(161, 22);
			this.copyPasswordToolStripMenuItem.Text = "Copy Password";
			this.copyPasswordToolStripMenuItem.Click += new global::System.EventHandler(this.copyPasswordToolStripMenuItem_Click);
			this.copySnkrsUrlToolStripMenuItem.Name = "copySnkrsUrlToolStripMenuItem";
			this.copySnkrsUrlToolStripMenuItem.Size = new global::System.Drawing.Size(161, 22);
			this.copySnkrsUrlToolStripMenuItem.Text = "Copy Snkrs Url";
			this.copySnkrsUrlToolStripMenuItem.Click += new global::System.EventHandler(this.copySnkrsUrlToolStripMenuItem_Click);
			this.openInBrowserToolStripMenuItem.Name = "openInBrowserToolStripMenuItem";
			this.openInBrowserToolStripMenuItem.Size = new global::System.Drawing.Size(161, 22);
			this.openInBrowserToolStripMenuItem.Text = "Open in Browser";
			this.openInBrowserToolStripMenuItem.Click += new global::System.EventHandler(this.openInBrowserToolStripMenuItem_Click);
			this.exportToJsonToolStripMenuItem.Name = "exportToJsonToolStripMenuItem";
			this.exportToJsonToolStripMenuItem.Size = new global::System.Drawing.Size(161, 22);
			this.exportToJsonToolStripMenuItem.Text = "Export to Json";
			this.exportToJsonToolStripMenuItem.Visible = false;
			this.exportToJsonToolStripMenuItem.Click += new global::System.EventHandler(this.exportToJsonToolStripMenuItem_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(587, 258);
			base.Controls.Add(this.objectListView1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.Name = "AddedProductsForm";
			this.Text = "AddedProductsForm - Better Nike Bot";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.AddedProductsForm_FormClosing);
			base.Load += new global::System.EventHandler(this.AddedProductsForm_Load);
			((global::System.ComponentModel.ISupportInitialize)this.objectListView1).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0400013A RID: 314
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400013B RID: 315
		private global::BrightIdeasSoftware.ObjectListView objectListView1;

		// Token: 0x0400013C RID: 316
		private global::BrightIdeasSoftware.OLVColumn olvColumn8;

		// Token: 0x0400013D RID: 317
		private global::BrightIdeasSoftware.OLVColumn olvColumn9;

		// Token: 0x0400013E RID: 318
		private global::BrightIdeasSoftware.OLVColumn olvColumn10;

		// Token: 0x0400013F RID: 319
		private global::BrightIdeasSoftware.OLVColumn olvColumn1;

		// Token: 0x04000140 RID: 320
		private global::BrightIdeasSoftware.OLVColumn olvColumn3;

		// Token: 0x04000141 RID: 321
		private global::BrightIdeasSoftware.OLVColumn olvColumn2;

		// Token: 0x04000142 RID: 322
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000143 RID: 323
		private global::System.Windows.Forms.ToolStripMenuItem copyUsernameToolStripMenuItem;

		// Token: 0x04000144 RID: 324
		private global::System.Windows.Forms.ToolStripMenuItem copyPasswordToolStripMenuItem;

		// Token: 0x04000145 RID: 325
		private global::System.Windows.Forms.ToolStripMenuItem copySnkrsUrlToolStripMenuItem;

		// Token: 0x04000146 RID: 326
		private global::System.Windows.Forms.ToolStripMenuItem openInBrowserToolStripMenuItem;

		// Token: 0x04000147 RID: 327
		private global::System.Windows.Forms.ToolStripMenuItem exportToJsonToolStripMenuItem;
	}
}
