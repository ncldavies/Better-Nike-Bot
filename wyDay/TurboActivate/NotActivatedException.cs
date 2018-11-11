using System;

namespace wyDay.TurboActivate
{
	// Token: 0x02000065 RID: 101
	public class NotActivatedException : TurboActivateException
	{
		// Token: 0x06000347 RID: 839 RVA: 0x000038B9 File Offset: 0x00001AB9
		public NotActivatedException() : base("The product needs to be activated.")
		{
		}
	}
}
