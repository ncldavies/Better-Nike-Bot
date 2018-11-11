using System;
using IMLokesh.Extensions;

namespace Better_Nike_Bot
{
	// Token: 0x02000007 RID: 7
	public class CheckoutInfo
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00004BE0 File Offset: 0x00002DE0
		public CheckoutInfo(bool payPalCheckout, bool ccCheckout, string payPalEmailAddress, string payPalPassword, bool finalizeOrder, string ccProfile)
		{
			this.PayPalCheckout = payPalCheckout;
			this.CcCheckout = ccCheckout;
			this.FinalizeOrder = finalizeOrder;
			if (payPalCheckout && ccCheckout)
			{
				throw new Exception("You cannot select both paypal and cc checkout at the same time.");
			}
			if (this.PayPalCheckout && (payPalEmailAddress.IsNullOrWhiteSpace() || payPalPassword.IsNullOrWhiteSpace()))
			{
				throw new Exception("Please enter paypal email and password or deselect PP checkout.");
			}
			if (ccCheckout && ccProfile.IsNull())
			{
				throw new Exception("Please select a CC Profile.");
			}
			if (this.PayPalCheckout && !payPalEmailAddress.Contains("@"))
			{
				throw new Exception("An email address must contain '@' symbol.");
			}
			this.PayPalEmailAddress = payPalEmailAddress;
			this.PayPalPassword = payPalPassword;
			this.CcProfile = ccProfile;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000021BB File Offset: 0x000003BB
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000021C3 File Offset: 0x000003C3
		public bool PayPalCheckout { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000021CC File Offset: 0x000003CC
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000021D4 File Offset: 0x000003D4
		public bool CcCheckout { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000021DD File Offset: 0x000003DD
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000021E5 File Offset: 0x000003E5
		public bool FinalizeOrder { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000021EE File Offset: 0x000003EE
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000021F6 File Offset: 0x000003F6
		public string PayPalEmailAddress { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000021FF File Offset: 0x000003FF
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002207 File Offset: 0x00000407
		public string PayPalPassword { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002210 File Offset: 0x00000410
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002218 File Offset: 0x00000418
		public string CcProfile { get; set; }
	}
}
