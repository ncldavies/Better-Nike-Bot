using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Better_Nike_Bot.Browser;
using BrightIdeasSoftware;
using IMLokesh.Extensions;
using IMLokesh.FileUtility;
using Newtonsoft.Json.Linq;

namespace Better_Nike_Bot
{
	// Token: 0x0200001F RID: 31
	public partial class AddedProductsForm : Form
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00002B36 File Offset: 0x00000D36
		public AddedProductsForm()
		{
			this.InitializeComponent();
			base.CenterToParent();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00002B55 File Offset: 0x00000D55
		private void AddedProductsForm_Load(object sender, EventArgs e)
		{
			this.objectListView1.FullRowSelect = true;
			this.exportToJsonToolStripMenuItem.Visible = File.Exists(FileHelper.FileInSameDirectory("greeksneaks.json"));
			this.UpdateForm();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00015A60 File Offset: 0x00013C60
		public void UpdateForm()
		{
			lock (this._lock)
			{
				try
				{
					this.objectListView1.SetObjects(Form1.AddedProducts.List);
				}
				catch (Exception value)
				{
					Console.WriteLine(value);
				}
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00002B83 File Offset: 0x00000D83
		private void AddedProductsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			base.Hide();
			e.Cancel = true;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00015AC8 File Offset: 0x00013CC8
		private void copyUsernameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddedProduct addedProduct = this.objectListView1.SelectedObjects.Cast<AddedProduct>().First<AddedProduct>();
			Clipboard.SetText(addedProduct.EmailAddress);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00002B92 File Offset: 0x00000D92
		private void objectListView1_CellRightClick(object sender, CellRightClickEventArgs e)
		{
			if (e.RowIndex >= 0 && this.objectListView1.SelectedObjects.Count != 0)
			{
				e.MenuStrip = this.contextMenuStrip1;
				return;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00015AF8 File Offset: 0x00013CF8
		private void copyPasswordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddedProduct addedProduct = this.objectListView1.SelectedObjects.Cast<AddedProduct>().First<AddedProduct>();
			Clipboard.SetText(addedProduct.Password);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00015B28 File Offset: 0x00013D28
		private void copySnkrsUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddedProduct addedProduct = this.objectListView1.SelectedObjects.Cast<AddedProduct>().First<AddedProduct>();
			Clipboard.SetText(addedProduct.SnkrsUrl);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00015B58 File Offset: 0x00013D58
		private void openInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Account account = this.objectListView1.SelectedObjects.Cast<AddedProduct>().First<AddedProduct>().Account;
			new Browser
			{
				Acc = account
			}.Show();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00015B94 File Offset: 0x00013D94
		private void exportToJsonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string contents = JArray.FromObject(from s in Form1.AddedProducts.List
			select new
			{
				NikeEmail = s.EmailAddress,
				NikePass = s.Password,
				ProductName = s.ProductName + " " + s.ProductType,
				Size = s.Size
			}).ToString();
			File.WriteAllText(FileHelper.FileInSameDirectory("greeksneaks.json"), contents);
			"Done".Show(MessageBoxIcon.Asterisk, "", MessageBoxButtons.OK);
		}

		// Token: 0x04000139 RID: 313
		private object _lock = new object();
	}
}
