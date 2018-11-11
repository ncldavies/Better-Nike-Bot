using System;
using IMLokesh.RandomUtility;

namespace Better_Nike_Bot
{
	// Token: 0x0200004E RID: 78
	public static class NikeWaitIntervals
	{
		// Token: 0x040002D1 RID: 721
		public static int RetryAtc = 1000;

		// Token: 0x040002D2 RID: 722
		public static int RetryAtcFailure = 1100;

		// Token: 0x040002D3 RID: 723
		public static int RetryAtcFailureTimer = 900;

		// Token: 0x040002D4 RID: 724
		public static int TimeoutRetryAtc = 2200;

		// Token: 0x040002D5 RID: 725
		public static int WaitAtc = 2000;

		// Token: 0x040002D6 RID: 726
		public static int RetrySolveCaptcha = 300;

		// Token: 0x040002D7 RID: 727
		public static int RetryGetProductDetails = RandomHelper.RandomInt(1000, 1500);

		// Token: 0x040002D8 RID: 728
		public static int RetryGetProductDetailsTimer = RandomHelper.RandomInt(1000, 1150);

		// Token: 0x040002D9 RID: 729
		public static int Monitor = RandomHelper.RandomInt(1000, 1500);

		// Token: 0x040002DA RID: 730
		public static int MonitorTimer = RandomHelper.RandomInt(500, 1000);

		// Token: 0x040002DB RID: 731
		public static int RetrySoldOut = 3000;
	}
}
