using System;

namespace wyDay.TurboActivate
{
	// Token: 0x02000061 RID: 97
	public class PkeyRevokedException : TurboActivateException
	{
		// Token: 0x06000343 RID: 835 RVA: 0x00003885 File Offset: 0x00001A85
		public PkeyRevokedException() : base("The product key has been revoked.")
		{
		}
	}
}
