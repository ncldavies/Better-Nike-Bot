using System;

namespace wyDay.TurboActivate
{
	// Token: 0x0200006C RID: 108
	public class InvalidFlagsException : TurboActivateException
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0000391E File Offset: 0x00001B1E
		public InvalidFlagsException() : base("The flags you passed to the function were invalid (or missing). Flags like \"TA_SYSTEM\" and \"TA_USER\" are mutually exclusive -- you can only use one or the other.")
		{
		}
	}
}
