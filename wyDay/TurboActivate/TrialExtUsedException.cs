using System;

namespace wyDay.TurboActivate
{
	// Token: 0x02000068 RID: 104
	public class TrialExtUsedException : TurboActivateException
	{
		// Token: 0x0600034A RID: 842 RVA: 0x000038E0 File Offset: 0x00001AE0
		public TrialExtUsedException() : base("The trial extension has already been used.")
		{
		}
	}
}
