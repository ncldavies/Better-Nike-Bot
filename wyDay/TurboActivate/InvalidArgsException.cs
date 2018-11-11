using System;

namespace wyDay.TurboActivate
{
	// Token: 0x0200006F RID: 111
	public class InvalidArgsException : TurboActivateException
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00003945 File Offset: 0x00001B45
		public InvalidArgsException() : base("The arguments passed to the function are invalid. Double check your logic.")
		{
		}
	}
}
