using System;
using Newtonsoft.Json;

namespace Better_Nike_Bot
{
	// Token: 0x02000019 RID: 25
	public class NikeToken
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00002972 File Offset: 0x00000B72
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000297A File Offset: 0x00000B7A
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00002983 File Offset: 0x00000B83
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000298B File Offset: 0x00000B8B
		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002994 File Offset: 0x00000B94
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000299C File Offset: 0x00000B9C
		public long Timestamp { get; set; }
	}
}
