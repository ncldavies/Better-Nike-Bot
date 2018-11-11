using System;
using IMLokesh.Extensions;

namespace Better_Nike_Bot
{
	// Token: 0x0200002A RID: 42
	public static class NotificationExtensions
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00022014 File Offset: 0x00020214
		public static string ReplacePlaceholders(this string str, AtcItem item)
		{
			if (item.IsNullOrDefault<AtcItem>())
			{
				item = new AtcItem(new Account("admin@betternikebot.com", "pass", "10", "", "", "", "", "", "", "", null, false), "http://store.nike.com/us/en_us/pd/zoom-field-general-training-shoe/pid-1546292/pgid-10266634", "10")
				{
					Details = new ProductDetails
					{
						Line1 = "Nike Zoom Field General (Ohio State)",
						Line2 = "Men's Training Shoe",
						Price = "130.0",
						ProductImage = "http://images.nike.com/is/image/DotCom/THN_PS/Nike-Zoom-Field-General-Ohio-State-Mens-Training-Shoe-654859_061.jpg?hei=620&wid=620&fmt=png",
						Size = "10",
						StyleCode = "654859-061"
					}
				};
			}
			return str.ReplaceStringArray(NotificationSettings.AvailableFields, new string[]
			{
				item.Account.EmailAddress + (Form1.SendPassword ? ", password: {0}".With(new object[]
				{
					item.Account.Password
				}) : ""),
				item.SnkrsBuyUrl.IsNullOrWhiteSpace() ? NikeUrls.NikeCart : item.SnkrsBuyUrl,
				item.Details.Line2,
				item.Details.ProductImage,
				item.Url,
				item.Details.Line1,
				item.Details.Price,
				item.Details.Size,
				item.Details.StyleCode,
				NotificationSettings.ServiceLink,
				NotificationSettings.ServiceLogoImage,
				NotificationSettings.ServiceName,
				NotificationSettings.ServiceTwitterHandle
			});
		}
	}
}
