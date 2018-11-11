using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IMLokesh.Extensions;

namespace Better_Nike_Bot
{
	// Token: 0x0200001B RID: 27
	public partial class ScheduleForm : Form
	{
		// Token: 0x06000108 RID: 264 RVA: 0x000029A5 File Offset: 0x00000BA5
		public ScheduleForm(Account acc = null)
		{
			this.InitializeComponent();
			this.dateTimePicker1.Value = DateTime.Now;
			this.dateTimePicker1.CustomFormat = "MMMM dd, yyyy hh:mm:ss tt";
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000029D3 File Offset: 0x00000BD3
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000029DB File Offset: 0x00000BDB
		public DateTime DateTime { get; set; }

		// Token: 0x0600010B RID: 267 RVA: 0x00013C2C File Offset: 0x00011E2C
		private void buttonSave_Click(object sender, EventArgs e)
		{
			try
			{
				this.DateTime = this.dateTimePicker1.Value;
			}
			catch (Exception ex)
			{
				ex.Message.Show(MessageBoxIcon.Hand, "", MessageBoxButtons.OK);
				return;
			}
			base.DialogResult = DialogResult.OK;
		}
	}
}
