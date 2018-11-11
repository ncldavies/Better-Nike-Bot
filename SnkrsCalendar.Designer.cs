namespace Better_Nike_Bot
{
	// Token: 0x02000051 RID: 81
	public partial class SnkrsCalendar : global::System.Windows.Forms.Form
	{
		// Token: 0x060002DA RID: 730 RVA: 0x00003642 File Offset: 0x00001842
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0002CE60 File Offset: 0x0002B060
		private void InitializeComponent()
		{
			this.objectListView1 = new global::BrightIdeasSoftware.ObjectListView();
			this.olvColumn1 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn2 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn3 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn9 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn7 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn4 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn5 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn6 = new global::BrightIdeasSoftware.OLVColumn();
			this.olvColumn8 = new global::BrightIdeasSoftware.OLVColumn();
			this.textBoxSearch = new global::System.Windows.Forms.TextBox();
			((global::System.ComponentModel.ISupportInitialize)this.objectListView1).BeginInit();
			base.SuspendLayout();
			this.objectListView1.AllColumns.Add(this.olvColumn1);
			this.objectListView1.AllColumns.Add(this.olvColumn2);
			this.objectListView1.AllColumns.Add(this.olvColumn3);
			this.objectListView1.AllColumns.Add(this.olvColumn9);
			this.objectListView1.AllColumns.Add(this.olvColumn7);
			this.objectListView1.AllColumns.Add(this.olvColumn4);
			this.objectListView1.AllColumns.Add(this.olvColumn5);
			this.objectListView1.AllColumns.Add(this.olvColumn6);
			this.objectListView1.AllColumns.Add(this.olvColumn8);
			this.objectListView1.AlternateRowBackColor = global::System.Drawing.Color.WhiteSmoke;
			this.objectListView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.olvColumn1,
				this.olvColumn2,
				this.olvColumn3,
				this.olvColumn9,
				this.olvColumn7,
				this.olvColumn4,
				this.olvColumn5,
				this.olvColumn6
			});
			this.objectListView1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.objectListView1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.objectListView1.FullRowSelect = true;
			this.objectListView1.Location = new global::System.Drawing.Point(0, 0);
			this.objectListView1.Name = "objectListView1";
			this.objectListView1.ShowGroups = false;
			this.objectListView1.Size = new global::System.Drawing.Size(784, 462);
			this.objectListView1.TabIndex = 1;
			this.objectListView1.UseAlternatingBackColors = true;
			this.objectListView1.UseCompatibleStateImageBehavior = false;
			this.objectListView1.UseFiltering = true;
			this.objectListView1.View = global::System.Windows.Forms.View.Details;
			this.objectListView1.DoubleClick += new global::System.EventHandler(this.objectListView1_DoubleClick);
			this.olvColumn1.AspectName = "Name";
			this.olvColumn1.CellPadding = null;
			this.olvColumn1.Text = "Name";
			this.olvColumn1.Width = 219;
			this.olvColumn2.AspectName = "Date";
			this.olvColumn2.AspectToStringFormat = "";
			this.olvColumn2.CellPadding = null;
			this.olvColumn2.Text = "Date (Local)";
			this.olvColumn2.Width = 125;
			this.olvColumn3.AspectName = "StyleCode";
			this.olvColumn3.CellPadding = null;
			this.olvColumn3.Text = "Style Code";
			this.olvColumn3.Width = 100;
			this.olvColumn9.AspectName = "PublishType";
			this.olvColumn9.CellPadding = null;
			this.olvColumn9.Text = "PublishType";
			this.olvColumn9.Width = 96;
			this.olvColumn7.AspectName = "SelectionEngine";
			this.olvColumn7.CellPadding = null;
			this.olvColumn7.Text = "Selection Engine";
			this.olvColumn7.Width = 116;
			this.olvColumn4.AspectName = "Available";
			this.olvColumn4.CellPadding = null;
			this.olvColumn4.Text = "Available";
			this.olvColumn4.Width = 120;
			this.olvColumn5.AspectName = "HeatLevel";
			this.olvColumn5.CellPadding = null;
			this.olvColumn5.Text = "Heat Level";
			this.olvColumn5.Width = 70;
			this.olvColumn6.AspectName = "WaitLineEnabled";
			this.olvColumn6.CellPadding = null;
			this.olvColumn6.Text = "Wait Line Enabled";
			this.olvColumn6.Width = 76;
			this.olvColumn8.AspectName = "Link";
			this.olvColumn8.CellPadding = null;
			this.olvColumn8.DisplayIndex = 7;
			this.olvColumn8.IsVisible = false;
			this.olvColumn8.Text = "Link";
			this.olvColumn8.Width = 113;
			this.textBoxSearch.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxSearch.Location = new global::System.Drawing.Point(561, 392);
			this.textBoxSearch.Name = "textBoxSearch";
			this.textBoxSearch.Size = new global::System.Drawing.Size(211, 20);
			this.textBoxSearch.TabIndex = 2;
			this.textBoxSearch.Text = "Search products here...";
			this.textBoxSearch.TextChanged += new global::System.EventHandler(this.textBoxSearch_TextChanged);
			this.textBoxSearch.Enter += new global::System.EventHandler(this.textBoxSearch_Enter);
			this.textBoxSearch.Leave += new global::System.EventHandler(this.textBoxSearch_Leave);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(784, 462);
			base.Controls.Add(this.textBoxSearch);
			base.Controls.Add(this.objectListView1);
			base.Name = "SnkrsCalendar";
			this.Text = "SNKRS Calendar (Double click row to use)";
			base.Load += new global::System.EventHandler(this.SnkrsCalendar_Load);
			((global::System.ComponentModel.ISupportInitialize)this.objectListView1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002E2 RID: 738
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040002E3 RID: 739
		private global::BrightIdeasSoftware.ObjectListView objectListView1;

		// Token: 0x040002E4 RID: 740
		private global::BrightIdeasSoftware.OLVColumn olvColumn1;

		// Token: 0x040002E5 RID: 741
		private global::BrightIdeasSoftware.OLVColumn olvColumn2;

		// Token: 0x040002E6 RID: 742
		private global::BrightIdeasSoftware.OLVColumn olvColumn3;

		// Token: 0x040002E7 RID: 743
		private global::BrightIdeasSoftware.OLVColumn olvColumn4;

		// Token: 0x040002E8 RID: 744
		private global::BrightIdeasSoftware.OLVColumn olvColumn7;

		// Token: 0x040002E9 RID: 745
		private global::BrightIdeasSoftware.OLVColumn olvColumn5;

		// Token: 0x040002EA RID: 746
		private global::BrightIdeasSoftware.OLVColumn olvColumn6;

		// Token: 0x040002EB RID: 747
		private global::BrightIdeasSoftware.OLVColumn olvColumn8;

		// Token: 0x040002EC RID: 748
		private global::BrightIdeasSoftware.OLVColumn olvColumn9;

		// Token: 0x040002ED RID: 749
		private global::System.Windows.Forms.TextBox textBoxSearch;
	}
}
