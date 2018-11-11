using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace ns0
{
	// Token: 0x0200000A RID: 10
	internal class Class0
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00007EA4 File Offset: 0x000060A4
		internal static void smethod_0()
		{
			string text = "ﾶﾲﾳﾐﾔﾚﾌﾗￓ￟ﾩﾚﾍﾌﾖﾐﾑￂￎ￑ￏ￑ￏ￑ￏￓ￟ﾼﾊﾓﾋﾊﾍﾚￂﾑﾚﾊﾋﾍﾞﾓￓ￟ﾯﾊﾝﾓﾖﾜﾴﾚﾆﾫﾐﾔﾚﾑￂﾑﾊﾓﾓﾟﾑﾻﾶﾊﾊﾭￏﾑﾖﾞﾘﾷﾖﾞﾞﾑﾪﾕﾺﾫﾥﾾￂￂﾟﾶﾲﾳﾐﾔﾚﾌﾗ￑ﾺﾇﾋﾚﾑﾌﾖﾐﾑﾌￓ￟ﾩﾚﾍﾌﾖﾐﾑￂￎ￑ￏ￑ￏ￑ￏￓ￟ﾼﾊﾓﾋﾊﾍﾚￂﾑﾚﾊﾋﾍﾞﾓￓ￟ﾯﾊﾝﾓﾖﾜﾴﾚﾆﾫﾐﾔﾚﾑￂﾑﾊﾓﾓﾟﾋﾖ￉ﾻ￐ﾙﾘﾽﾫﾦ￈ﾰﾌﾻￏﾨﾝﾍￔﾲﾍﾈￂￂﾟﾶﾲﾳﾐﾔﾚﾌﾗ￑ﾷﾋﾋﾏﾪﾋﾖﾓﾖﾋﾆￓ￟ﾩﾚﾍﾌﾖﾐﾑￂￎ￑ￏ￑ￏ￑ￏￓ￟ﾼﾊﾓﾋﾊﾍﾚￂﾑﾚﾊﾋﾍﾞﾓￓ￟ﾯﾊﾝﾓﾖﾜﾴﾚﾆﾫﾐﾔﾚﾑￂﾑﾊﾓﾓﾟﾥﾔￏﾆﾛﾚ￐ﾽￍￍﾞﾫﾹﾴﾻﾙﾖﾸﾭￆﾴﾈￂￂﾟﾶﾲﾳﾐﾔﾚﾌﾗ￑ﾭﾞﾑﾛﾐﾒﾪﾋﾖﾓﾖﾋﾆￓ￟ﾩﾚﾍﾌﾖﾐﾑￂￎ￑ￏ￑ￏ￑ￏￓ￟ﾼﾊﾓﾋﾊﾍﾚￂﾑﾚﾊﾋﾍﾞﾓￓ￟ﾯﾊﾝﾓﾖﾜﾴﾚﾆﾫﾐﾔﾚﾑￂﾑﾊﾓﾓﾟ￉ﾑﾌﾙﾧﾊﾕﾹﾲﾇﾎﾖﾅﾾﾨﾴﾬﾴﾍﾭ￉ﾈￂￂﾟﾶﾲﾳﾐﾔﾚﾌﾗ￑ﾷﾋﾋﾏￓ￟ﾩﾚﾍﾌﾖﾐﾑￂￎ￑ￏ￑ￏ￑ￏￓ￟ﾼﾊﾓﾋﾊﾍﾚￂﾑﾚﾊﾋﾍﾞﾓￓ￟ﾯﾊﾝﾓﾖﾜﾴﾚﾆﾫﾐﾔﾚﾑￂﾑﾊﾓﾓﾟﾴﾹﾫﾨￌﾐﾈￇﾓﾊﾗﾈﾷﾉﾚﾙￌﾔﾏﾺﾹﾈￂￂ";
			char[] array = text.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ~array[i];
			}
			text = new string(array);
			string[] array2 = text.Split(new char[]
			{
				'`'
			});
			if (array2 != null && array2.Length >= 0)
			{
				int j = 0;
				while (j < array2.Length)
				{
					if (array2[j + 1].StartsWith("~"))
					{
						try
						{
							int num = array2[j + 1].IndexOf('~', 1);
							int num2 = int.Parse(array2[j + 1].Substring(1, num - 1), CultureInfo.InvariantCulture);
							Assembly executingAssembly = Assembly.GetExecutingAssembly();
							string text2 = Path.Combine(Path.GetDirectoryName(executingAssembly.Location), array2[j]);
							if (!File.Exists(text2) || new FileInfo(text2).Length != (long)num2)
							{
								foreach (string text3 in executingAssembly.GetManifestResourceNames())
								{
									if (text3 == array2[j + 1])
									{
										Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text3);
										byte[] array3 = Class1.smethod_3(97L, manifestResourceStream);
										using (FileStream fileStream = new FileStream(text2, FileMode.Create, FileAccess.Write))
										{
											fileStream.Write(array3, 0, array3.Length);
										}
										manifestResourceStream.Close();
									}
								}
							}
							goto IL_166;
						}
						catch
						{
							goto IL_166;
						}
						goto IL_152;
					}
					goto IL_152;
					IL_166:
					j += 2;
					continue;
					IL_152:
					Class0.hashtable_1[array2[j]] = array2[j + 1];
					goto IL_166;
				}
			}
			AppDomain.CurrentDomain.AssemblyResolve += Class0.smethod_3;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00008054 File Offset: 0x00006254
		private static string smethod_1(byte[] byte_0, string string_0, string string_1, string string_2)
		{
			string_0 = Path.Combine(Path.GetTempPath(), string_0);
			string text = Path.Combine(string_0, string_1 + string_2);
			if (!File.Exists(text) || (long)byte_0.Length != new FileInfo(text).Length)
			{
				Directory.CreateDirectory(string_0);
				FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write);
				fileStream.Write(byte_0, 0, byte_0.Length);
				fileStream.Close();
			}
			return text;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000080BC File Offset: 0x000062BC
		private static bool smethod_2(string string_0, string string_1)
		{
			try
			{
				string[] array = string_0.Split(new char[]
				{
					','
				});
				string[] array2 = string_1.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = array[i].Trim();
				}
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = array2[j].Trim();
				}
				string[] array3 = array;
				int k = 0;
				IL_A1:
				while (k < array3.Length)
				{
					string a = array3[k];
					bool flag = false;
					int l = 0;
					while (l < array2.Length)
					{
						if (!string.Equals(a, array2[l], StringComparison.CurrentCultureIgnoreCase))
						{
							l++;
						}
						else
						{
							flag = true;
							IL_97:
							if (flag)
							{
								k++;
								goto IL_A1;
							}
							return false;
						}
					}
					goto IL_97;
				}
				return true;
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00008194 File Offset: 0x00006394
		private static Assembly smethod_3(object object_0, ResolveEventArgs resolveEventArgs_0)
		{
			Assembly result;
			lock (Class0.hashtable_0)
			{
				Assembly assembly = null;
				string name = resolveEventArgs_0.Name;
				string text = string.Empty;
				foreach (object obj2 in Class0.hashtable_1.Keys)
				{
					string text2 = (string)obj2;
					if (Class0.smethod_2(name, text2))
					{
						assembly = (Class0.hashtable_0[text2] as Assembly);
						if (assembly != null)
						{
							return assembly;
						}
						text = (Class0.hashtable_1[text2] as string);
						break;
					}
				}
				if (text.Length == 0)
				{
					result = null;
				}
				else
				{
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					foreach (string text3 in executingAssembly.GetManifestResourceNames())
					{
						if (text3 == text)
						{
							Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text3);
							byte[] array = Class1.smethod_3(97L, manifestResourceStream);
							byte[] array2 = null;
							try
							{
								text += "#";
								foreach (string text4 in executingAssembly.GetManifestResourceNames())
								{
									if (text4 == text)
									{
										Stream manifestResourceStream2 = executingAssembly.GetManifestResourceStream(text4);
										array2 = Class1.smethod_3(97L, manifestResourceStream2);
									}
								}
							}
							catch (Exception)
							{
							}
							bool flag = false;
							try
							{
								if (array2 == null)
								{
									assembly = Assembly.Load(array);
								}
								else
								{
									try
									{
										assembly = Assembly.Load(array, array2);
									}
									catch (Exception)
									{
										assembly = Assembly.Load(array);
									}
								}
							}
							catch (FileLoadException)
							{
								flag = true;
							}
							catch (BadImageFormatException)
							{
								flag = true;
							}
							if (flag)
							{
								string string_ = Class0.smethod_4(name);
								string path = Class0.smethod_1(array, text, string_, ".dll");
								if (array2 != null)
								{
									Class0.smethod_1(array, text, string_, ".pdb");
								}
								assembly = Assembly.LoadFile(path);
							}
							Class0.hashtable_0[name] = assembly;
							return assembly;
						}
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00008410 File Offset: 0x00006610
		private static string smethod_4(string string_0)
		{
			int num = string_0.IndexOf(',');
			if (num >= 0)
			{
				string_0 = string_0.Substring(0, num);
			}
			return string_0;
		}

		// Token: 0x04000064 RID: 100
		private static readonly Hashtable hashtable_0 = new Hashtable();

		// Token: 0x04000065 RID: 101
		private static readonly Hashtable hashtable_1 = new Hashtable();
	}
}
