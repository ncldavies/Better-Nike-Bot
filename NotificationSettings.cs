using System;
using System.IO;
using Better_Nike_Bot.Properties;
using IMLokesh.Extensions;
using IMLokesh.FileUtility;

namespace Better_Nike_Bot
{
	// Token: 0x02000029 RID: 41
	public static class NotificationSettings
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00021DDC File Offset: 0x0001FFDC
		public static void Save()
		{
			Settings.Default.Subject = NotificationSettings.Subject;
			Settings.Default.SendFromName = NotificationSettings.SendFromName;
			Settings.Default.SendFromEmail = NotificationSettings.SendFromEmail;
			Settings.Default.HtmlTemplate = NotificationSettings.HtmlTemplate;
			Settings.Default.TextTemplate = NotificationSettings.TextTemplate;
			Settings.Default.ServiceLink = NotificationSettings.ServiceLink;
			Settings.Default.ServiceLogoImage = NotificationSettings.ServiceLogoImage;
			Settings.Default.ServiceName = NotificationSettings.ServiceName;
			Settings.Default.ServiceTwitterHandle = NotificationSettings.ServiceTwitterHandle;
			Settings.Default.Save();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00021E7C File Offset: 0x0002007C
		public static void LoadSettings()
		{
			if (File.Exists(FileHelper.FileInSameDirectory("notification-settings.txt")))
			{
				string[] linesArray = File.ReadAllText(FileHelper.FileInSameDirectory("notification-settings.txt")).GetLinesArray(true);
				NotificationSettings.Subject = linesArray[6];
				NotificationSettings.SendFromName = linesArray[4];
				NotificationSettings.SendFromEmail = linesArray[5];
				NotificationSettings.TextTemplate = linesArray[7];
				NotificationSettings.ServiceLink = linesArray[1];
				NotificationSettings.ServiceLogoImage = linesArray[2];
				NotificationSettings.ServiceName = linesArray[0];
				NotificationSettings.ServiceTwitterHandle = linesArray[3];
				return;
			}
			if (!Settings.Default.Subject.IsNullOrWhiteSpace())
			{
				NotificationSettings.Subject = Settings.Default.Subject;
			}
			if (!Settings.Default.SendFromName.IsNullOrWhiteSpace())
			{
				NotificationSettings.SendFromName = Settings.Default.SendFromName;
			}
			if (!Settings.Default.SendFromEmail.IsNullOrWhiteSpace())
			{
				NotificationSettings.SendFromEmail = Settings.Default.SendFromEmail;
			}
			if (!Settings.Default.HtmlTemplate.IsNullOrWhiteSpace())
			{
				NotificationSettings.HtmlTemplate = Settings.Default.HtmlTemplate;
			}
			if (!Settings.Default.TextTemplate.IsNullOrWhiteSpace())
			{
				NotificationSettings.TextTemplate = Settings.Default.TextTemplate;
			}
			if (!Settings.Default.ServiceLink.IsNullOrWhiteSpace())
			{
				NotificationSettings.ServiceLink = Settings.Default.ServiceLink;
			}
			if (!Settings.Default.ServiceLogoImage.IsNullOrWhiteSpace())
			{
				NotificationSettings.ServiceLogoImage = Settings.Default.ServiceLogoImage;
			}
			if (!Settings.Default.ServiceName.IsNullOrWhiteSpace())
			{
				NotificationSettings.ServiceName = Settings.Default.ServiceName;
			}
			if (!Settings.Default.ServiceTwitterHandle.IsNullOrWhiteSpace())
			{
				NotificationSettings.ServiceTwitterHandle = Settings.Default.ServiceTwitterHandle;
			}
		}

		// Token: 0x040001A6 RID: 422
		public static string[] AvailableFields = new string[]
		{
			"{{login_details}}",
			"{{product_cart_link}}",
			"{{product_type}}",
			"{{product_image}}",
			"{{product_link}}",
			"{{product_name}}",
			"{{product_price}}",
			"{{product_size}}",
			"{{product_style_code}}",
			"{{service_link}}",
			"{{service_logo_image}}",
			"{{service_name}}",
			"{{service_twitter_handle}}"
		};

		// Token: 0x040001A7 RID: 423
		public static NotificationService[] AvailableTextNotificationServices = new NotificationService[]
		{
			new NotificationService("AT&T", "@txt.att.net"),
			new NotificationService("Boost Mobile", "@myboostmobile.com"),
			new NotificationService("Nextel", "@messaging.nextel.com"),
			new NotificationService("Sprint", "@messaging.sprintpcs.com"),
			new NotificationService("T-Mobile", "@tmomail.net"),
			new NotificationService("US Cellular", "@email.uscc.net"),
			new NotificationService("Verizon", "@vtext.com"),
			new NotificationService("Virgin Mobile", "@vmobl.com"),
			new NotificationService("3 River Wireless", "@sms.3rivers.net"),
			new NotificationService("ACS Wireless", "@paging.acswireless.com"),
			new NotificationService("Alltel", "@message.alltel.com"),
			new NotificationService("Bell Mobility", "@txt.bellmobility.ca"),
			new NotificationService("Blue Sky Frog", "@blueskyfrog.com"),
			new NotificationService("Bluegrass Cellular", "@sms.bluecell.com"),
			new NotificationService("BPL Mobile", "@bplmobile.com"),
			new NotificationService("Carolina West Wireless", "10digit10digitnumber@cwwsms.com"),
			new NotificationService("Cellular One", "@mobile.celloneusa.com"),
			new NotificationService("Cellular South", "@csouth1.com"),
			new NotificationService("Centennial Wireless", "@cwemail.com"),
			new NotificationService("CenturyTel", "@messaging.centurytel.net"),
			new NotificationService("Clearnet", "@msg.clearnet.com"),
			new NotificationService("Comcast", "@comcastpcs.textmsg.com"),
			new NotificationService("Corr Wireless Communications", "@corrwireless.net"),
			new NotificationService("Dobson", "@mobile.dobson.net"),
			new NotificationService("Edge Wireless", "@sms.edgewireless.com"),
			new NotificationService("Fido", "@fido.ca"),
			new NotificationService("Golden Telecom", "@sms.goldentele.com"),
			new NotificationService("Helio", "@messaging.sprintpcs.com"),
			new NotificationService("Houston Cellular", "@text.houstoncellular.net"),
			new NotificationService("Idea Cellular", "@ideacellular.net"),
			new NotificationService("Illinois Valley Cellular", "@ivctext.com"),
			new NotificationService("Inland Cellular Telephone", "@inlandlink.com"),
			new NotificationService("MCI", "@pagemci.com"),
			new NotificationService("Metrocall", "10digitpagernumber@page.metrocall.com"),
			new NotificationService("Metrocall 2-way", "10digitpagernumber@my2way.com"),
			new NotificationService("Metro PCS", "@mymetropcs.com"),
			new NotificationService("Microcell", "@fido.ca"),
			new NotificationService("Midwest Wireless", "@clearlydigital.com"),
			new NotificationService("Mobilcomm", "@mobilecomm.net"),
			new NotificationService("MTS", "@text.mtsmobility.com"),
			new NotificationService("OnlineBeep", "@onlinebeep.net"),
			new NotificationService("PCS One", "@pcsone.net"),
			new NotificationService("President's Choice", "@txt.bell.ca"),
			new NotificationService("Public Service Cellular", "@sms.pscel.com"),
			new NotificationService("Qwest", "@qwestmp.com"),
			new NotificationService("Rogers AT&T Wireless", "@pcs.rogers.com"),
			new NotificationService("Rogers Canada", "@pcs.rogers.com"),
			new NotificationService("Satellink", "10digitpagernumber.pageme@satellink.net"),
			new NotificationService("Southwestern Bell", "@email.swbw.com"),
			new NotificationService("Sumcom", "@tms.suncom.com"),
			new NotificationService("Surewest Communicaitons", "@mobile.surewest.com"),
			new NotificationService("Telus", "@msg.telus.com"),
			new NotificationService("Tracfone", "@txt.att.net"),
			new NotificationService("Triton", "@tms.suncom.com"),
			new NotificationService("Unicel", "@utext.com"),
			new NotificationService("Solo Mobile", "@txt.bell.ca"),
			new NotificationService("Sprint", "@messaging.sprintpcs.com"),
			new NotificationService("Sumcom", "@tms.suncom.com"),
			new NotificationService("Surewest Communicaitons", "@mobile.surewest.com"),
			new NotificationService("Telus", "@msg.telus.com"),
			new NotificationService("Triton", "@tms.suncom.com"),
			new NotificationService("Unicel", "@utext.com"),
			new NotificationService("US Cellular", "@email.uscc.net"),
			new NotificationService("US West", "@uswestdatamail.com"),
			new NotificationService("Virgin Mobile Canada", "@vmobile.ca"),
			new NotificationService("West Central Wireless", "@sms.wcc.net"),
			new NotificationService("Western Wireless", "@cellularonewest.com")
		};

		// Token: 0x040001A8 RID: 424
		public static string ServiceName = "Better Nike Bot";

		// Token: 0x040001A9 RID: 425
		public static string ServiceTwitterHandle = "BetterNikeBot";

		// Token: 0x040001AA RID: 426
		public static string ServiceLink = "http://www.betternikebot.com";

		// Token: 0x040001AB RID: 427
		public static string ServiceLogoImage = "http://www.betternikebot.com/logo.png";

		// Token: 0x040001AC RID: 428
		public static string SendFromName = "Better Nike Bot";

		// Token: 0x040001AD RID: 429
		public static string SendFromEmail = "admin@betternikebot.com";

		// Token: 0x040001AE RID: 430
		public static string Subject = "{{product_name}} has been added to your cart!";

		// Token: 0x040001AF RID: 431
		public static string HtmlTemplate = "<html>\r\n  <head/>\r\n  <body style=\"background-color: #ebe7e1;font-family: Georgia, serif\">\r\n    <div id=\"header\" align=\"center\">\r\n        <a href=\"{{service_link}}\">\r\n            <img src=\"{{service_logo_image}}\" alt=\"{{service_name}}\" id=\"logo\" style=\"display: block;margin: 0 auto;padding-top: 20px\"/></a>\r\n    </div>\r\n    <div id=\"container\" style=\"margin: 35px auto;max-width: 600px;min-height: 500px;background-color: #FFF;-webkit-border-radius: 10px;-moz-border-radius: 10px;border-radius: 10px;padding: 30px 70px\">\r\n        <h1 style=\"font-size: 22px;font-weight: bold;color: #333\">Congratulations!</h1>\r\n        <h2 style=\"font-weight: normal;font-size: 20px\">A new item has been added to your Nike.com cart {{login_details}}. You'll find the details of the item below.</h2>\r\n\r\n        <p>&nbsp;</p>\r\n        <hr/><p>&nbsp;</p>\r\n\r\n        <div id=\"product\">\r\n            <div id=\"product-image\" style=\"float: left;padding-right: 40px\">\r\n                <img src=\"{{product_image}}\" alt=\"\" style=\"width: 200px;height: 200px\"/></div>\r\n\r\n            <div id=\"product-details\">\r\n                <ul style=\"padding: 0;list-style-type: none;font-family: helvetica\"><li id=\"line1\" style=\"padding-bottom: 5px;font-size: 30px !important;color: #333;letter-spacing: -1px;font-weight: bold;line-height: 0.9em\">{{product_name}}</li>\r\n                    <li id=\"line2\" style=\"padding-bottom: 5px;font-size: 14px !important;color: #333;font-style: italic;text-transform: uppercase\">{{product_type}}</li>\r\n                    <li id=\"size\" style=\"padding-bottom: 5px;font-size: 17px;color: #333;margin-top: 30px\">Size: {{product_size}}</li>\r\n                    <li id=\"price\" style=\"padding-bottom: 5px;font-size: 17px;color: #333\">Price: {{product_price}}</li>\r\n                    <li id=\"style-code\" style=\"padding-bottom: 5px;font-size: 17px;color: #333\">Style Code: {{product_style_code}}</li>\r\n                </ul></div>\r\n\r\n            <div id=\"buttons\" style=\"clear: both;margin-top: 100px\">\r\n                <ul style=\"padding: 0;list-style-type: none;font-family: helvetica\"><a href=\"{{product_cart_link}}\" style=\"text-decoration: none;color: #ddd\">\r\n                        <li style=\"background-color: #38434d;margin: 10px 0;width: 100%;padding: 6px;text-align: center\">View Cart</li>\r\n                    </a>\r\n                    <a href=\"{{product_link}}\" style=\"text-decoration: none;color: #ddd\">\r\n                        <li style=\"background-color: #38434d;margin: 10px 0;width: 100%;padding: 6px;text-align: center\">View Product</li>\r\n                    </a>\r\n                    <a href=\"{{service_link}}\" style=\"text-decoration: none;color: #ddd\">\r\n                        <li style=\"background-color: #38434d;margin: 10px 0;width: 100%;padding: 6px;text-align: center\">Visit {{service_name}}</li>\r\n                    </a>\r\n                </ul></div>\r\n        </div>\r\n\r\n    </div>\r\n\r\n    <footer style=\"background-color: #f2efe9;margin: 0;box-shadow: 0 500px 0 500px #f2efe9\"><div id=\"footer\" style=\"max-width: 600px;margin: 0 auto;padding: 20px 0;color: #333\">\r\n            <p class=\"content\" style=\"font-size: 14px\">\r\n                This email was sent by <a href=\"{{service_link}}\">{{service_name}}</a>.\r\n            </p>\r\n            <p class=\"content\" style=\"font-size: 14px\">\r\n                Twitter: <a href=\"http://twitter.com/{{service_twitter_handle}}\">@{{service_twitter_handle}}</a>\r\n            </p>\r\n            <p class=\"content\" style=\"font-size: 14px\">---</p>\r\n            <p class=\"disclaimer\" style=\"font-size: 12px\">You're receiving this email because you used {{service_name}} to secure a pair of shoes on Nike.com. </p>\r\n        </div>\r\n    </footer></body>\r\n</html>";

		// Token: 0x040001B0 RID: 432
		public static string TextTemplate = "BetterNikeBot.com: {{product_name}} size {{product_size}} has been added to account {{login_details}}. Checkout {{product_cart_link}}";
	}
}
