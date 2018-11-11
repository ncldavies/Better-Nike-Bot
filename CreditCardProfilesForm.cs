using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace Better_Nike_Bot
{
	// Token: 0x0200000F RID: 15
	public partial class CreditCardProfilesForm : Form
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00002579 File Offset: 0x00000779
		public CreditCardProfilesForm()
		{
			this.InitializeComponent();
			base.CenterToParent();
			this.objectListView1.SetObjects(Form1.CreditCardProfiles);
			this.RefreshTable();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000025A3 File Offset: 0x000007A3
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			new CheckoutProfile(null).ShowDialog();
			this.RefreshTable();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000025B7 File Offset: 0x000007B7
		private void RefreshTable()
		{
			this.objectListView1.BuildList();
			this.olvColumn1.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
			this.objectListView1.RebuildColumns();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000B100 File Offset: 0x00009300
		private void buttonEdit_Click(object sender, EventArgs e)
		{
			CreditCardProfile creditCardProfile = (this.objectListView1.SelectedObjects.Count > 0) ? ((CreditCardProfile)this.objectListView1.SelectedObjects[0]) : null;
			if (creditCardProfile != null)
			{
				new CheckoutProfile(creditCardProfile).ShowDialog();
				this.RefreshTable();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000B150 File Offset: 0x00009350
		private void buttonRemove_Click(object sender, EventArgs e)
		{
			IList selectedObjects = this.objectListView1.SelectedObjects;
			if (selectedObjects.Count == 0)
			{
				return;
			}
			foreach (object obj in selectedObjects)
			{
				Form1.CreditCardProfiles.Remove((CreditCardProfile)obj);
			}
			this.RefreshTable();
		}
	}
}
