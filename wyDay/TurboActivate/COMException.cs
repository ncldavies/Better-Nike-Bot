using System;

namespace wyDay.TurboActivate
{
	// Token: 0x02000060 RID: 96
	public class COMException : TurboActivateException
	{
		// Token: 0x06000342 RID: 834 RVA: 0x00003878 File Offset: 0x00001A78
		public COMException() : base("CoInitializeEx failed. Re-enable Windows Management Instrumentation (WMI) service. Contact your system admin for more information.")
		{
		}
	}
}
