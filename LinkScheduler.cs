using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IMLokesh.Extensions;

namespace Better_Nike_Bot
{
	// Token: 0x02000026 RID: 38
	public partial class LinkScheduler : Form
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00002FBA File Offset: 0x000011BA
		public LinkScheduler()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00020B64 File Offset: 0x0001ED64
		private void LinkScheduler_Load(object sender, EventArgs e)
		{
			base.CenterToParent();
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.BackgroundColor = SystemColors.ButtonFace;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._table = new DataTable("Links");
			this._table.Columns.Add(new DataColumn("Link", typeof(string)));
			this._table.Columns.Add(new DataColumn("DateTime", typeof(DateTime)));
			foreach (MonitorLink monitorLink in Form1.MonitorLinks)
			{
				this._table.Rows.Add(new object[]
				{
					monitorLink.Link,
					monitorLink.StartTime
				});
			}
			this.dataGridView1.DataSource = this._table;
			this.dataGridView1.Columns[1].DefaultCellStyle.Format = "G";
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00020CA4 File Offset: 0x0001EEA4
		private void buttonSave_Click(object sender, EventArgs e)
		{
			foreach (DataRow dataRow in this._table.Select())
			{
				foreach (MonitorLink monitorLink3 in Form1.MonitorLinks)
				{
					if (dataRow[0].ToString() == monitorLink3.Link)
					{
						monitorLink3.StartTime = (DateTime)dataRow[1];
					}
				}
			}
			using (List<MonitorLink>.Enumerator enumerator2 = Form1.MonitorLinks.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					MonitorLink monitorLink = enumerator2.Current;
					MonitorLink monitorLink2 = Form1.MonitorLinkCache.FirstOrDefault((MonitorLink l) => l.Link == monitorLink.Link);
					if (monitorLink2.IsNotNull())
					{
						monitorLink2.StartTime = monitorLink.StartTime;
					}
					else
					{
						Form1.MonitorLinkCache.Add(new MonitorLink
						{
							StartTime = monitorLink.StartTime,
							Link = monitorLink.Link
						});
					}
				}
			}
			base.DialogResult = DialogResult.OK;
		}

		// Token: 0x040001A1 RID: 417
		private DataTable _table;
	}
}
