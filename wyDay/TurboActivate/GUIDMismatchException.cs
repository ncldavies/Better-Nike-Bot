using System;

namespace wyDay.TurboActivate
{
	// Token: 0x02000066 RID: 102
	public class GUIDMismatchException : TurboActivateException
	{
		// Token: 0x06000348 RID: 840 RVA: 0x000038C6 File Offset: 0x00001AC6
		public GUIDMismatchException() : base("The product details file \"TurboActivate.dat\" is corrupt.")
		{
		}
	}
}
