using System;
using IMLokesh.Extensions;

namespace Better_Nike_Bot
{
	// Token: 0x02000052 RID: 82
	public class SnkrsItem
	{
		// Token: 0x060002DD RID: 733 RVA: 0x0002D4BC File Offset: 0x0002B6BC
		public SnkrsItem()
		{
			this.Name = "-";
			this.DateTime = DateTime.MinValue.AddYears(10);
			this.StyleCode = "-";
			this.Available = "-";
			this.PublishType = "-";
			this.SelectionEngine = "-";
			this.HeatLevel = "-";
			this.WaitLineEnabled = "-";
			this.Link = "-";
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00003679 File Offset: 0x00001879
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00003681 File Offset: 0x00001881
		public string Name { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000368A File Offset: 0x0000188A
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00003692 File Offset: 0x00001892
		public DateTime DateTime { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0002D53C File Offset: 0x0002B73C
		public string Date
		{
			get
			{
				return this.DateTime.ToString("MM/dd/yy hh:mm tt");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000369B File Offset: 0x0000189B
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x000036A3 File Offset: 0x000018A3
		public string StyleCode { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000036AC File Offset: 0x000018AC
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x000036B4 File Offset: 0x000018B4
		public string Available { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x000036BD File Offset: 0x000018BD
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x000036C5 File Offset: 0x000018C5
		public string PublishType { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000036CE File Offset: 0x000018CE
		// (set) Token: 0x060002EA RID: 746 RVA: 0x000036D6 File Offset: 0x000018D6
		public string SelectionEngine { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000036DF File Offset: 0x000018DF
		// (set) Token: 0x060002EC RID: 748 RVA: 0x000036E7 File Offset: 0x000018E7
		public string HeatLevel { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000036F0 File Offset: 0x000018F0
		// (set) Token: 0x060002EE RID: 750 RVA: 0x000036F8 File Offset: 0x000018F8
		public string WaitLineEnabled { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00003701 File Offset: 0x00001901
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00003709 File Offset: 0x00001909
		public string Link { get; set; }

		// Token: 0x060002F1 RID: 753 RVA: 0x0002D55C File Offset: 0x0002B75C
		public override string ToString()
		{
			return "{0} {1} {2} {3}".With(new object[]
			{
				this.Name,
				this.StyleCode,
				this.SelectionEngine,
				this.Date
			});
		}
	}
}
