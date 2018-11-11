using System;
using System.Collections.Generic;
using IMLokesh.Extensions;
using IMLokesh.ThreadUtility;

namespace Better_Nike_Bot
{
	// Token: 0x02000012 RID: 18
	internal static class Logger
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x0000BC80 File Offset: 0x00009E80
		public static void Log(object msg, bool includeDate = true, bool onlyLogIf = true)
		{
			string text = msg.ToString();
			text = (includeDate ? "{0} - {1}".With(new object[]
			{
				DateTime.Now.ToString(Logger.DateFormat),
				text
			}) : text);
			lock (Logger.ObjectLock)
			{
				Logger.Logs.Add(text);
				Logger.Count++;
			}
		}

		// Token: 0x040000CF RID: 207
		public static object ObjectLock = new object();

		// Token: 0x040000D0 RID: 208
		public static List<string> Logs = new List<string>();

		// Token: 0x040000D1 RID: 209
		public static string DateFormat = "dd-MMM-yy h:m:s.FFFF tt";

		// Token: 0x040000D2 RID: 210
		public static int Count = 0;

		// Token: 0x040000D3 RID: 211
		public static ConcurrentFile LogFile = new ConcurrentFile("bnb_log.txt");

		// Token: 0x040000D4 RID: 212
		public static bool IsPaused = false;
	}
}
