using System;

namespace wyDay.TurboActivate
{
	// Token: 0x02000062 RID: 98
	public class PkeyMaxUsedException : TurboActivateException
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00003892 File Offset: 0x00001A92
		public PkeyMaxUsedException() : base("The product key has already been activated with the maximum number of computers.")
		{
		}
	}
}
