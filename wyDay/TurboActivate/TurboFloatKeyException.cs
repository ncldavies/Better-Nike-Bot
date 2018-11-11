using System;

namespace wyDay.TurboActivate
{
	// Token: 0x02000070 RID: 112
	public class TurboFloatKeyException : TurboActivateException
	{
		// Token: 0x06000352 RID: 850 RVA: 0x00003952 File Offset: 0x00001B52
		public TurboFloatKeyException() : base("The product key used is for TurboFloat, not TurboActivate.")
		{
		}
	}
}
