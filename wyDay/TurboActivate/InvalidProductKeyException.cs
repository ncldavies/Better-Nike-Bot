using System;

namespace wyDay.TurboActivate
{
	// Token: 0x02000064 RID: 100
	public class InvalidProductKeyException : TurboActivateException
	{
		// Token: 0x06000346 RID: 838 RVA: 0x000038AC File Offset: 0x00001AAC
		public InvalidProductKeyException() : base("The product key is invalid or there's no product key.")
		{
		}
	}
}
