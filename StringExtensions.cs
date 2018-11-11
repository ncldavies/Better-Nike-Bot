using System;
using System.Collections.Generic;
using System.Linq;
using IMLokesh.Extensions;

namespace Better_Nike_Bot
{
	// Token: 0x02000053 RID: 83
	public static class StringExtensions
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x0002D5A0 File Offset: 0x0002B7A0
		static StringExtensions()
		{
			List<SizeDescription> sizeDetailsCollection = StringExtensions.SizeDetailsCollection;
			string sizeInCm = "21.6";
			string nativeSize = "3.5Y";
			SiteType[] siteType = new SiteType[1];
			sizeDetailsCollection.Add(new SizeDescription(sizeInCm, nativeSize, siteType));
			List<SizeDescription> sizeDetailsCollection2 = StringExtensions.SizeDetailsCollection;
			string sizeInCm2 = "22";
			string nativeSize2 = "4Y";
			SiteType[] siteType2 = new SiteType[1];
			sizeDetailsCollection2.Add(new SizeDescription(sizeInCm2, nativeSize2, siteType2));
			List<SizeDescription> sizeDetailsCollection3 = StringExtensions.SizeDetailsCollection;
			string sizeInCm3 = "22.4";
			string nativeSize3 = "4.5Y";
			SiteType[] siteType3 = new SiteType[1];
			sizeDetailsCollection3.Add(new SizeDescription(sizeInCm3, nativeSize3, siteType3));
			List<SizeDescription> sizeDetailsCollection4 = StringExtensions.SizeDetailsCollection;
			string sizeInCm4 = "22.9";
			string nativeSize4 = "5Y";
			SiteType[] siteType4 = new SiteType[1];
			sizeDetailsCollection4.Add(new SizeDescription(sizeInCm4, nativeSize4, siteType4));
			List<SizeDescription> sizeDetailsCollection5 = StringExtensions.SizeDetailsCollection;
			string sizeInCm5 = "23.3";
			string nativeSize5 = "5.5Y";
			SiteType[] siteType5 = new SiteType[1];
			sizeDetailsCollection5.Add(new SizeDescription(sizeInCm5, nativeSize5, siteType5));
			List<SizeDescription> sizeDetailsCollection6 = StringExtensions.SizeDetailsCollection;
			string sizeInCm6 = "23.7";
			string nativeSize6 = "6Y";
			SiteType[] siteType6 = new SiteType[1];
			sizeDetailsCollection6.Add(new SizeDescription(sizeInCm6, nativeSize6, siteType6));
			List<SizeDescription> sizeDetailsCollection7 = StringExtensions.SizeDetailsCollection;
			string sizeInCm7 = "24.1";
			string nativeSize7 = "6.5Y";
			SiteType[] siteType7 = new SiteType[1];
			sizeDetailsCollection7.Add(new SizeDescription(sizeInCm7, nativeSize7, siteType7));
			List<SizeDescription> sizeDetailsCollection8 = StringExtensions.SizeDetailsCollection;
			string sizeInCm8 = "24.5";
			string nativeSize8 = "7Y";
			SiteType[] siteType8 = new SiteType[1];
			sizeDetailsCollection8.Add(new SizeDescription(sizeInCm8, nativeSize8, siteType8));
			List<SizeDescription> sizeDetailsCollection9 = StringExtensions.SizeDetailsCollection;
			string sizeInCm9 = "22";
			string nativeSize9 = "4";
			SiteType[] siteType9 = new SiteType[1];
			sizeDetailsCollection9.Add(new SizeDescription(sizeInCm9, nativeSize9, siteType9));
			List<SizeDescription> sizeDetailsCollection10 = StringExtensions.SizeDetailsCollection;
			string sizeInCm10 = "22.4";
			string nativeSize10 = "4.5";
			SiteType[] siteType10 = new SiteType[1];
			sizeDetailsCollection10.Add(new SizeDescription(sizeInCm10, nativeSize10, siteType10));
			List<SizeDescription> sizeDetailsCollection11 = StringExtensions.SizeDetailsCollection;
			string sizeInCm11 = "22.9";
			string nativeSize11 = "5";
			SiteType[] siteType11 = new SiteType[1];
			sizeDetailsCollection11.Add(new SizeDescription(sizeInCm11, nativeSize11, siteType11));
			List<SizeDescription> sizeDetailsCollection12 = StringExtensions.SizeDetailsCollection;
			string sizeInCm12 = "23.3";
			string nativeSize12 = "5.5";
			SiteType[] siteType12 = new SiteType[1];
			sizeDetailsCollection12.Add(new SizeDescription(sizeInCm12, nativeSize12, siteType12));
			List<SizeDescription> sizeDetailsCollection13 = StringExtensions.SizeDetailsCollection;
			string sizeInCm13 = "23.7";
			string nativeSize13 = "6";
			SiteType[] siteType13 = new SiteType[1];
			sizeDetailsCollection13.Add(new SizeDescription(sizeInCm13, nativeSize13, siteType13));
			List<SizeDescription> sizeDetailsCollection14 = StringExtensions.SizeDetailsCollection;
			string sizeInCm14 = "24.1";
			string nativeSize14 = "6.5";
			SiteType[] siteType14 = new SiteType[1];
			sizeDetailsCollection14.Add(new SizeDescription(sizeInCm14, nativeSize14, siteType14));
			List<SizeDescription> sizeDetailsCollection15 = StringExtensions.SizeDetailsCollection;
			string sizeInCm15 = "24.5";
			string nativeSize15 = "7";
			SiteType[] siteType15 = new SiteType[1];
			sizeDetailsCollection15.Add(new SizeDescription(sizeInCm15, nativeSize15, siteType15));
			List<SizeDescription> sizeDetailsCollection16 = StringExtensions.SizeDetailsCollection;
			string sizeInCm16 = "25";
			string nativeSize16 = "7.5";
			SiteType[] siteType16 = new SiteType[1];
			sizeDetailsCollection16.Add(new SizeDescription(sizeInCm16, nativeSize16, siteType16));
			List<SizeDescription> sizeDetailsCollection17 = StringExtensions.SizeDetailsCollection;
			string sizeInCm17 = "25.4";
			string nativeSize17 = "8";
			SiteType[] siteType17 = new SiteType[1];
			sizeDetailsCollection17.Add(new SizeDescription(sizeInCm17, nativeSize17, siteType17));
			List<SizeDescription> sizeDetailsCollection18 = StringExtensions.SizeDetailsCollection;
			string sizeInCm18 = "25.8";
			string nativeSize18 = "8.5";
			SiteType[] siteType18 = new SiteType[1];
			sizeDetailsCollection18.Add(new SizeDescription(sizeInCm18, nativeSize18, siteType18));
			List<SizeDescription> sizeDetailsCollection19 = StringExtensions.SizeDetailsCollection;
			string sizeInCm19 = "26.2";
			string nativeSize19 = "9";
			SiteType[] siteType19 = new SiteType[1];
			sizeDetailsCollection19.Add(new SizeDescription(sizeInCm19, nativeSize19, siteType19));
			List<SizeDescription> sizeDetailsCollection20 = StringExtensions.SizeDetailsCollection;
			string sizeInCm20 = "26.7";
			string nativeSize20 = "9.5";
			SiteType[] siteType20 = new SiteType[1];
			sizeDetailsCollection20.Add(new SizeDescription(sizeInCm20, nativeSize20, siteType20));
			List<SizeDescription> sizeDetailsCollection21 = StringExtensions.SizeDetailsCollection;
			string sizeInCm21 = "27.1";
			string nativeSize21 = "10";
			SiteType[] siteType21 = new SiteType[1];
			sizeDetailsCollection21.Add(new SizeDescription(sizeInCm21, nativeSize21, siteType21));
			List<SizeDescription> sizeDetailsCollection22 = StringExtensions.SizeDetailsCollection;
			string sizeInCm22 = "27.5";
			string nativeSize22 = "10.5";
			SiteType[] siteType22 = new SiteType[1];
			sizeDetailsCollection22.Add(new SizeDescription(sizeInCm22, nativeSize22, siteType22));
			List<SizeDescription> sizeDetailsCollection23 = StringExtensions.SizeDetailsCollection;
			string sizeInCm23 = "27.9";
			string nativeSize23 = "11";
			SiteType[] siteType23 = new SiteType[1];
			sizeDetailsCollection23.Add(new SizeDescription(sizeInCm23, nativeSize23, siteType23));
			List<SizeDescription> sizeDetailsCollection24 = StringExtensions.SizeDetailsCollection;
			string sizeInCm24 = "28.3";
			string nativeSize24 = "11.5";
			SiteType[] siteType24 = new SiteType[1];
			sizeDetailsCollection24.Add(new SizeDescription(sizeInCm24, nativeSize24, siteType24));
			List<SizeDescription> sizeDetailsCollection25 = StringExtensions.SizeDetailsCollection;
			string sizeInCm25 = "28.8";
			string nativeSize25 = "12";
			SiteType[] siteType25 = new SiteType[1];
			sizeDetailsCollection25.Add(new SizeDescription(sizeInCm25, nativeSize25, siteType25));
			List<SizeDescription> sizeDetailsCollection26 = StringExtensions.SizeDetailsCollection;
			string sizeInCm26 = "29.2";
			string nativeSize26 = "12.5";
			SiteType[] siteType26 = new SiteType[1];
			sizeDetailsCollection26.Add(new SizeDescription(sizeInCm26, nativeSize26, siteType26));
			List<SizeDescription> sizeDetailsCollection27 = StringExtensions.SizeDetailsCollection;
			string sizeInCm27 = "29.6";
			string nativeSize27 = "13";
			SiteType[] siteType27 = new SiteType[1];
			sizeDetailsCollection27.Add(new SizeDescription(sizeInCm27, nativeSize27, siteType27));
			List<SizeDescription> sizeDetailsCollection28 = StringExtensions.SizeDetailsCollection;
			string sizeInCm28 = "30";
			string nativeSize28 = "13.5";
			SiteType[] siteType28 = new SiteType[1];
			sizeDetailsCollection28.Add(new SizeDescription(sizeInCm28, nativeSize28, siteType28));
			List<SizeDescription> sizeDetailsCollection29 = StringExtensions.SizeDetailsCollection;
			string sizeInCm29 = "30.5";
			string nativeSize29 = "14";
			SiteType[] siteType29 = new SiteType[1];
			sizeDetailsCollection29.Add(new SizeDescription(sizeInCm29, nativeSize29, siteType29));
			List<SizeDescription> sizeDetailsCollection30 = StringExtensions.SizeDetailsCollection;
			string sizeInCm30 = "31.3";
			string nativeSize30 = "15";
			SiteType[] siteType30 = new SiteType[1];
			sizeDetailsCollection30.Add(new SizeDescription(sizeInCm30, nativeSize30, siteType30));
			List<SizeDescription> sizeDetailsCollection31 = StringExtensions.SizeDetailsCollection;
			string sizeInCm31 = "32.2";
			string nativeSize31 = "16";
			SiteType[] siteType31 = new SiteType[1];
			sizeDetailsCollection31.Add(new SizeDescription(sizeInCm31, nativeSize31, siteType31));
			List<SizeDescription> sizeDetailsCollection32 = StringExtensions.SizeDetailsCollection;
			string sizeInCm32 = "33";
			string nativeSize32 = "17";
			SiteType[] siteType32 = new SiteType[1];
			sizeDetailsCollection32.Add(new SizeDescription(sizeInCm32, nativeSize32, siteType32));
			List<SizeDescription> sizeDetailsCollection33 = StringExtensions.SizeDetailsCollection;
			string sizeInCm33 = "33.9";
			string nativeSize33 = "18";
			SiteType[] siteType33 = new SiteType[1];
			sizeDetailsCollection33.Add(new SizeDescription(sizeInCm33, nativeSize33, siteType33));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22", "36", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22.4", "36.5", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22.9", "37.5", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("23.3", "38", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("23.7", "38.5", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("24.1", "39", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("24.5", "40", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25", "40.5", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25.4", "41", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25.8", "42", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("26.2", "42.5", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("26.7", "43", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.1", "44", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.5", "44.5", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.9", "45", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("28.3", "45.5", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("28.8", "46", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("29.2", "47", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("29.6", "47.5", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("30", "48", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("30.5", "48.5", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("31.3", "49", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("32.2", "49.5", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("33", "50", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("33.9", "50.5", new SiteType[]
			{
				SiteType.NikeFR,
				SiteType.NikeDE,
				SiteType.NikeIT
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("21.6", "3", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22", "3.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22.4", "4", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22.9", "4.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("23.3", "5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("23.7", "5.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("24.1", "6", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("24.5", "6", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22", "3.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22.4", "4", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22.9", "4.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("23.3", "5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("23.7", "5.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("24.1", "6", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("24.5", "6", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25", "6.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25.4", "7", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25.8", "7.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("26.2", "8", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("26.7", "8.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.1", "9", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.5", "9.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.9", "10", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("28.3", "10.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("28.8", "11", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("29.2", "11.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("29.6", "12", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("30", "12.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("30.5", "13", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("31.3", "13.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("32.2", "14", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("33", "14.5", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("33.9", "15", new SiteType[]
			{
				SiteType.NikeUK
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22", "23", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22.4", "23.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22.9", "23.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("23.3", "24", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("23.7", "24", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("24.1", "24.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("24.5", "25", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25", "25.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25.4", "26", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25.8", "26.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("26.2", "27", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("26.7", "27.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.1", "28", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.5", "28.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.9", "29", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("28.3", "29.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("28.8", "30", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("29.2", "30.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("29.6", "31", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("30", "31.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("30.5", "32", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("31.3", "32.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("32.2", "33", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("33", "33.5", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("33.9", "34", new SiteType[]
			{
				SiteType.NikeJP
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22", "36", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22.4", "36.5", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("22.9", "37.5", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("23.3", "38", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("23.7", "38.5", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("24.1", "39", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("24.5", "40", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25", "40.5", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25.4", "41", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("25.8", "42", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("26.2", "42.5", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("26.7", "43", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.1", "44", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.5", "44.5", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("27.9", "45", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("28.3", "45.5", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("28.8", "46", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("29.2", "47", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("29.6", "47.5", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("30", "48", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("30.5", "48.5", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("31.3", "49", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("32.2", "49.5", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("33", "50", new SiteType[]
			{
				SiteType.NikeCN
			}));
			StringExtensions.SizeDetailsCollection.Add(new SizeDescription("33.9", "50.5", new SiteType[]
			{
				SiteType.NikeCN
			}));
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00003712 File Offset: 0x00001912
		public static bool IsNikeUrl(this string url)
		{
			return url.ContainsAny(NikeUrls.MatchUrls);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000371F File Offset: 0x0000191F
		public static bool IsNikeCollection(this string url)
		{
			return url.ContainsAny((from s in NikeUrls.MatchUrls
			where s.EndsWith("pw/")
			select s).ToArray<string>());
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0002EB1C File Offset: 0x0002CD1C
		public static string ConvertToNativeSize(this string sizeDescription)
		{
			if (NikeUrls.NikeCountryCode == "US")
			{
				return sizeDescription;
			}
			string result;
			try
			{
				SizeDescription cmSize = (from s in StringExtensions.SizeDetailsCollection
				where s.SiteType.Contains(SiteType.NikeUS)
				select s).FirstOrDefault((SizeDescription s) => s.NativeSize == sizeDescription);
				SizeDescription sizeDescription2 = (from s in StringExtensions.SizeDetailsCollection
				where s.SiteType.Contains(Form1.SiteType)
				select s).FirstOrDefault((SizeDescription s) => s.SizeInCm == cmSize.SizeInCm);
				result = sizeDescription2.NativeSize;
			}
			catch (Exception)
			{
				result = sizeDescription;
			}
			return result;
		}

		// Token: 0x040002F7 RID: 759
		public static List<SizeDescription> SizeDetailsCollection = new List<SizeDescription>();
	}
}
