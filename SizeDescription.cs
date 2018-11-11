using System;

namespace Better_Nike_Bot
{
	// Token: 0x02000056 RID: 86
	public class SizeDescription
	{
		// Token: 0x060002FD RID: 765 RVA: 0x000037AB File Offset: 0x000019AB
		public SizeDescription(string sizeInCm, string nativeSize, SiteType[] siteType)
		{
			this.SizeInCm = sizeInCm;
			this.NativeSize = nativeSize;
			this.SiteType = siteType;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000037C8 File Offset: 0x000019C8
		// (set) Token: 0x060002FF RID: 767 RVA: 0x000037D0 File Offset: 0x000019D0
		public string SizeInCm { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000037D9 File Offset: 0x000019D9
		// (set) Token: 0x06000301 RID: 769 RVA: 0x000037E1 File Offset: 0x000019E1
		public string NativeSize { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000037EA File Offset: 0x000019EA
		// (set) Token: 0x06000303 RID: 771 RVA: 0x000037F2 File Offset: 0x000019F2
		public SiteType[] SiteType { get; set; }
	}
}
