using System;

namespace wyDay.TurboActivate
{
	// Token: 0x0200006E RID: 110
	public class ExtraDataTooLongException : TurboActivateException
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00003938 File Offset: 0x00001B38
		public ExtraDataTooLongException() : base("The \"extra data\" was too long. You're limited to 255 UTF-8 characters. Or, on Windows, a Unicode string that will convert into 255 UTF-8 characters or less.")
		{
		}
	}
}
