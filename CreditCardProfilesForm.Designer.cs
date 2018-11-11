namespace Better_Nike_Bot
{
	// Token: 0x0200000F RID: 15
	public partial class CreditCardProfilesForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000025DB File Offset: 0x000007DB
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000B1C8 File Offset: 0x000093C8
		private void InitializeComponent()
		{
			this.objectListView1 = new global::BrightIdeasSoftware.ObjectListView();
			this.olvColumn1 = new global::BrightIdeasSoftware.OLVColumn();
			this.buttonAdd = new global::System.Windows.Forms.Button();
			this.buttonEdit = new global::System.Windows.Forms.Button();
			this.buttonRemove = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.objectListView1).BeginInit();
			base.SuspendLayout();
			this.objectListView1.AllColumns.Add(this.olvColumn1);
			this.objectListView1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.objectListView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.olvColumn1
			});
			this.objectListView1.Location = new global::System.Drawing.Point(12, 12);
			this.objectListView1.Name = "objectListView1";
			this.objectListView1.ShowGroups = false;
			this.objectListView1.Size = new global::System.Drawing.Size(175, 218);
			this.objectListView1.TabIndex = 1;
			this.objectListView1.UseCompatibleStateImageBehavior = false;
			this.objectListView1.UseFiltering = true;
			this.objectListView1.View = global::System.Windows.Forms.View.Details;
			this.olvColumn1.AspectName = "Name";
			this.olvColumn1.CellPadding = null;
			this.olvColumn1.Text = "Profile Name";
			this.olvColumn1.Width = 124;
			this.buttonAdd.Location = new global::System.Drawing.Point(12, 236);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new global::System.Drawing.Size(175, 23);
			this.buttonAdd.TabIndex = 2;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new global::System.EventHandler(this.buttonAdd_Click);
			this.buttonEdit.Location = new global::System.Drawing.Point(12, 265);
			this.buttonEdit.Name = "buttonEdit";
			this.buttonEdit.Size = new global::System.Drawing.Size(175, 23);
			this.buttonEdit.TabIndex = 3;
			this.buttonEdit.Text = "Edit";
			this.buttonEdit.UseVisualStyleBackColor = true;
			this.buttonEdit.Click += new global::System.EventHandler(this.buttonEdit_Click);
			this.buttonRemove.Location = new global::System.Drawing.Point(12, 294);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new global::System.Drawing.Size(175, 23);
			this.buttonRemove.TabIndex = 4;
			this.buttonRemove.Text = "Remove";
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new global::System.EventHandler(this.buttonRemove_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(199, 324);
			base.Controls.Add(this.buttonRemove);
			base.Controls.Add(this.buttonEdit);
			base.Controls.Add(this.buttonAdd);
			base.Controls.Add(this.objectListView1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "CreditCardProfilesForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Manage Cards";
			((global::System.ComponentModel.ISupportInitialize)this.objectListView1).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x040000B7 RID: 183
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000B8 RID: 184
		private global::BrightIdeasSoftware.ObjectListView objectListView1;

		// Token: 0x040000B9 RID: 185
		private global::BrightIdeasSoftware.OLVColumn olvColumn1;

		// Token: 0x040000BA RID: 186
		private global::System.Windows.Forms.Button buttonAdd;

		// Token: 0x040000BB RID: 187
		private global::System.Windows.Forms.Button buttonEdit;

		// Token: 0x040000BC RID: 188
		private global::System.Windows.Forms.Button buttonRemove;
	}
}
