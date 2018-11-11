using System;

namespace wyDay.TurboActivate
{
	// Token: 0x02000071 RID: 113
	public class NoMoreDeactivationsException : TurboActivateException
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000395F File Offset: 0x00001B5F
		public NoMoreDeactivationsException() : base("No more deactivations are allowed for the product key. This product is still activated on this computer.")
		{
		}
	}
}
