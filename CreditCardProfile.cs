using System;
using System.Text;
using Newtonsoft.Json;

namespace Better_Nike_Bot
{
	// Token: 0x0200000E RID: 14
	public class CreditCardProfile
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000023AE File Offset: 0x000005AE
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000023B6 File Offset: 0x000005B6
		public string Name { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000023BF File Offset: 0x000005BF
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000023C7 File Offset: 0x000005C7
		public string BillingFirstName { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000023D0 File Offset: 0x000005D0
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000023D8 File Offset: 0x000005D8
		public string BillingLastName { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000023E1 File Offset: 0x000005E1
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000023E9 File Offset: 0x000005E9
		public string BillingAddress1 { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000023F2 File Offset: 0x000005F2
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000023FA File Offset: 0x000005FA
		public string BillingAddress2 { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002403 File Offset: 0x00000603
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000240B File Offset: 0x0000060B
		public string BillingAddress3 { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002414 File Offset: 0x00000614
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000241C File Offset: 0x0000061C
		public string BillingZipCode { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002425 File Offset: 0x00000625
		// (set) Token: 0x0600006F RID: 111 RVA: 0x0000242D File Offset: 0x0000062D
		public string BillingPhone { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002436 File Offset: 0x00000636
		// (set) Token: 0x06000071 RID: 113 RVA: 0x0000243E File Offset: 0x0000063E
		public string BillingCity { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002447 File Offset: 0x00000647
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000244F File Offset: 0x0000064F
		public string BillingState { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002458 File Offset: 0x00000658
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002460 File Offset: 0x00000660
		public string BillingStateJP { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002469 File Offset: 0x00000669
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002471 File Offset: 0x00000671
		public string ShippingFirstName { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000247A File Offset: 0x0000067A
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002482 File Offset: 0x00000682
		public string ShippingLastName { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000248B File Offset: 0x0000068B
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002493 File Offset: 0x00000693
		public string ShippingAddress1 { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000249C File Offset: 0x0000069C
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000024A4 File Offset: 0x000006A4
		public string ShippingAddress2 { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000024AD File Offset: 0x000006AD
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000024B5 File Offset: 0x000006B5
		public string ShippingAddress3 { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000024BE File Offset: 0x000006BE
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000024C6 File Offset: 0x000006C6
		public string ShippingZipCode { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000024CF File Offset: 0x000006CF
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000024D7 File Offset: 0x000006D7
		public string ShippingPhone { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000024E0 File Offset: 0x000006E0
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000024E8 File Offset: 0x000006E8
		public string ShippingCity { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000024F1 File Offset: 0x000006F1
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000024F9 File Offset: 0x000006F9
		public string ShippingState { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002502 File Offset: 0x00000702
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000250A File Offset: 0x0000070A
		public string ShippingStateJP { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002513 File Offset: 0x00000713
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000251B File Offset: 0x0000071B
		public string CreditCardType { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002524 File Offset: 0x00000724
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000252C File Offset: 0x0000072C
		public string CreditCardNumber { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002535 File Offset: 0x00000735
		// (set) Token: 0x0600008F RID: 143 RVA: 0x0000253D File Offset: 0x0000073D
		public string CreditCardExpiryMonth { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002546 File Offset: 0x00000746
		// (set) Token: 0x06000091 RID: 145 RVA: 0x0000254E File Offset: 0x0000074E
		public string CreditCardExpiryYear { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002557 File Offset: 0x00000757
		// (set) Token: 0x06000093 RID: 147 RVA: 0x0000255F File Offset: 0x0000075F
		public string CreditCardCvv { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002568 File Offset: 0x00000768
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00002570 File Offset: 0x00000770
		public int MaxCheckouts { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000B09C File Offset: 0x0000929C
		[JsonIgnore]
		public string CreditCardNumberFormatted
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 1; i <= this.CreditCardNumber.Length; i++)
				{
					stringBuilder.Append(this.CreditCardNumber[i - 1]);
					if (i % 4 == 0 && i != this.CreditCardNumber.Length)
					{
						stringBuilder.Append(" ");
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0400009A RID: 154
		[JsonIgnore]
		public int CheckoutCount;

		// Token: 0x0400009B RID: 155
		[JsonIgnore]
		public object Lock;
	}
}
