using System;

namespace wyDay.TurboActivate
{
	// Token: 0x0200006A RID: 106
	public class DateTimeException : TurboActivateException
	{
		// Token: 0x0600034C RID: 844 RVA: 0x000038FA File Offset: 0x00001AFA
		public DateTimeException(bool either) : base(either ? "Either the activation response file has expired or your date and time settings are incorrect. Fix your date and time settings, restart your computer, and try to activate again." : "Failed because your system date and time settings are incorrect. Fix your date and time settings, restart your computer, and try to activate again.")
		{
		}
	}
}
