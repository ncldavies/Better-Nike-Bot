using System;

namespace wyDay.TurboActivate
{
	// Token: 0x0200006B RID: 107
	public class PermissionException : TurboActivateException
	{
		// Token: 0x0600034D RID: 845 RVA: 0x00003911 File Offset: 0x00001B11
		public PermissionException() : base("Insufficient system permission. Either start your process as an admin / elevated user or call the function again with the TA_USER flag.")
		{
		}
	}
}
