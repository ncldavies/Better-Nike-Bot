using System;

namespace Better_Nike_Bot
{
	// Token: 0x02000057 RID: 87
	public class NotificationService
	{
		// Token: 0x06000304 RID: 772 RVA: 0x000037FB File Offset: 0x000019FB
		public NotificationService(string carrier, string emailSuffix)
		{
			this.Carrier = carrier;
			this.EmailSuffix = emailSuffix;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00003811 File Offset: 0x00001A11
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00003819 File Offset: 0x00001A19
		public string Carrier { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00003822 File Offset: 0x00001A22
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000382A File Offset: 0x00001A2A
		public string EmailSuffix { get; set; }
	}
}
