using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using IMLokesh.Extensions;

namespace Better_Nike_Bot
{
	// Token: 0x02000051 RID: 81
	public partial class SnkrsCalendar : Form
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x000035CF File Offset: 0x000017CF
		public SnkrsCalendar(SnkrsItem[] snkrsItems)
		{
			this.InitializeComponent();
			this._snkrsItems = snkrsItems;
			base.CenterToParent();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0002CD0C File Offset: 0x0002AF0C
		private void SnkrsCalendar_Load(object sender, EventArgs e)
		{
			this.Text = "SNKRS Calendar - {0} (Double click row to use)".With(new object[]
			{
				Form1.SiteType
			});
			this.objectListView1.SetObjects(this._snkrsItems);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0002CD50 File Offset: 0x0002AF50
		private void objectListView1_DoubleClick(object sender, EventArgs e)
		{
			SnkrsItem snkrsItem = (this.objectListView1.SelectedObjects.Count > 0) ? ((SnkrsItem)this.objectListView1.SelectedObjects[0]) : null;
			if (snkrsItem == null)
			{
				return;
			}
			using (AccountDetailsForm accountDetailsForm = new AccountDetailsForm(null, "", snkrsItem.StyleCode))
			{
				DialogResult dialogResult = accountDetailsForm.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					Form1.DefaultForm.AddAccount(accountDetailsForm.Account);
					base.Close();
				}
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000035F5 File Offset: 0x000017F5
		private void textBoxSearch_Enter(object sender, EventArgs e)
		{
			if (this.textBoxSearch.Text == "Search products here...")
			{
				this.textBoxSearch.Text = "";
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000361E File Offset: 0x0000181E
		private void textBoxSearch_Leave(object sender, EventArgs e)
		{
			if (this.textBoxSearch.Text.IsNullOrWhiteSpace())
			{
				this.textBoxSearch.Text = "Search products here...";
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0002CDE0 File Offset: 0x0002AFE0
		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
			this._filter = this.textBoxSearch.Text.ToLower();
			if (!this._filter.IsNullOrWhiteSpace() && this._filter != "search products here...")
			{
				this.objectListView1.SetObjects(from s in this._snkrsItems
				where s.ToString().ToLower().Contains(this._filter)
				select s);
				return;
			}
			this.objectListView1.SetObjects(this._snkrsItems);
		}

		// Token: 0x040002E0 RID: 736
		private SnkrsItem[] _snkrsItems;

		// Token: 0x040002E1 RID: 737
		private string _filter = "";
	}
}
