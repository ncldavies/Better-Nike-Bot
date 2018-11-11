using System;

namespace wyDay.TurboActivate
{
	// Token: 0x02000067 RID: 103
	public class ProductDetailsException : TurboActivateException
	{
		// Token: 0x06000349 RID: 841 RVA: 0x000038D3 File Offset: 0x00001AD3
		public ProductDetailsException() : base("The product details file \"TurboActivate.dat\" failed to load. It's either missing or corrupt.")
		{
		}
	}
}
