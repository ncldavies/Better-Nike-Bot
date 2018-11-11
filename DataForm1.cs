using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IMLokesh.Extensions;

namespace Better_Nike_Bot
{
	// Token: 0x02000010 RID: 16
	public partial class DataForm1 : Form
	{
		// Token: 0x0600009E RID: 158 RVA: 0x0000B540 File Offset: 0x00009740
		public DataForm1(bool multiline, string title, string message, string text, int width, int height, string checkBoxCaption, bool checkBoxChecked = false, string checkBoxCaption2 = "")
		{
			this.InitializeComponent();
			this.textBox1.Multiline = multiline;
			this.Text = title;
			this.labelMessage.Text = message;
			this.textBox1.Text = text;
			base.Width = width + 43;
			base.Height = height + 102;
			this.CheckBoxChecked = false;
			if (multiline)
			{
				this.textBox1.AcceptsReturn = true;
			}
			if (!checkBoxCaption.IsNullOrWhiteSpace())
			{
				this.checkBox1.Visible = true;
				this.checkBox1.Text = checkBoxCaption;
				CheckBox checkBox = this.checkBox1;
				this.CheckBoxChecked = checkBoxChecked;
				checkBox.Checked = checkBoxChecked;
			}
			if (!checkBoxCaption2.IsNullOrWhiteSpace())
			{
				this.checkBox2.Visible = true;
				this.checkBox2.Text = checkBoxCaption2;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000025FA File Offset: 0x000007FA
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00002602 File Offset: 0x00000802
		public string TextBoxText { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000260B File Offset: 0x0000080B
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002613 File Offset: 0x00000813
		public bool CheckBoxChecked { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000261C File Offset: 0x0000081C
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002624 File Offset: 0x00000824
		public bool CheckBoxChecked2 { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000262D File Offset: 0x0000082D
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002635 File Offset: 0x00000835
		public bool CheckWeb { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000263E File Offset: 0x0000083E
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002646 File Offset: 0x00000846
		public bool CheckSnkrs { get; set; }

		// Token: 0x060000A9 RID: 169 RVA: 0x0000B60C File Offset: 0x0000980C
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (this.textBox1.Multiline)
			{
				this.labelLines.Text = "Line Count: " + this.textBox1.Lines.Count<string>();
			}
			this.TextBoxText = this.textBox1.Text;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000264F File Offset: 0x0000084F
		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
			{
				if (sender != null)
				{
					((TextBox)sender).SelectAll();
				}
				e.Handled = true;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002678 File Offset: 0x00000878
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.CheckBoxChecked = this.checkBox1.Checked;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000268B File Offset: 0x0000088B
		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			this.CheckBoxChecked2 = this.checkBox2.Checked;
			this.radioButton1.Enabled = this.checkBox2.Checked;
			this.radioButton2.Enabled = this.checkBox2.Checked;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000026CA File Offset: 0x000008CA
		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			this.CheckWeb = this.radioButton1.Checked;
			this.CheckSnkrs = !this.radioButton1.Checked;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000026CA File Offset: 0x000008CA
		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			this.CheckWeb = this.radioButton1.Checked;
			this.CheckSnkrs = !this.radioButton1.Checked;
		}

		// Token: 0x02000011 RID: 17
		public enum FormType
		{
			// Token: 0x040000CD RID: 205
			InputForm,
			// Token: 0x040000CE RID: 206
			OutputForm
		}
	}
}
