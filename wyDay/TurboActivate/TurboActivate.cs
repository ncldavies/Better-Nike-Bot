using System;
using System.Runtime.InteropServices;
using System.Text;

namespace wyDay.TurboActivate
{
	// Token: 0x02000058 RID: 88
	public static class TurboActivate
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00003833 File Offset: 0x00001A33
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000383A File Offset: 0x00001A3A
		public static string VersionGUID { get; set; }

		// Token: 0x0600030B RID: 779 RVA: 0x0002EC00 File Offset: 0x0002CE00
		public static void Activate()
		{
			int num = TurboActivate.Native.Activate();
			switch (num)
			{
			case 0:
				return;
			case 1:
			case 3:
			case 7:
			case 9:
			case 10:
			case 12:
				break;
			case 2:
				throw new InvalidProductKeyException();
			case 4:
				throw new InternetException();
			case 5:
				throw new PkeyMaxUsedException();
			case 6:
				throw new PkeyRevokedException();
			case 8:
				throw new ProductDetailsException();
			case 11:
				throw new COMException();
			case 13:
				throw new DateTimeException(false);
			default:
				if (num == 17)
				{
					throw new VirtualMachineException();
				}
				if (num == 20)
				{
					throw new TurboFloatKeyException();
				}
				break;
			}
			throw new TurboActivateException("Failed to activate.");
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0002ECA0 File Offset: 0x0002CEA0
		public static void Activate(string extraData)
		{
			TurboActivate.Native.ACTIVATE_OPTIONS activate_OPTIONS = new TurboActivate.Native.ACTIVATE_OPTIONS
			{
				sExtraData = extraData
			};
			activate_OPTIONS.nLength = (uint)Marshal.SizeOf(activate_OPTIONS);
			switch (TurboActivate.Native.ActivateEx(ref activate_OPTIONS))
			{
			case 0:
				return;
			case 2:
				throw new InvalidProductKeyException();
			case 4:
				throw new InternetException();
			case 5:
				throw new PkeyMaxUsedException();
			case 6:
				throw new PkeyRevokedException();
			case 8:
				throw new ProductDetailsException();
			case 11:
				throw new COMException();
			case 13:
				throw new DateTimeException(false);
			case 17:
				throw new VirtualMachineException();
			case 18:
				throw new ExtraDataTooLongException();
			case 19:
				throw new InvalidArgsException();
			case 20:
				throw new TurboFloatKeyException();
			}
			throw new TurboActivateException("Failed to activate.");
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0002ED84 File Offset: 0x0002CF84
		public static void ActivationRequestToFile(string filename)
		{
			int num = TurboActivate.Native.ActivationRequestToFile(filename);
			switch (num)
			{
			case 0:
				return;
			case 1:
				break;
			case 2:
				throw new InvalidProductKeyException();
			default:
				if (num == 8)
				{
					throw new ProductDetailsException();
				}
				if (num == 11)
				{
					throw new COMException();
				}
				break;
			}
			throw new TurboActivateException("Failed to save the activation request file.");
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0002EDD4 File Offset: 0x0002CFD4
		public static void ActivationRequestToFile(string filename, string extraData)
		{
			TurboActivate.Native.ACTIVATE_OPTIONS activate_OPTIONS = new TurboActivate.Native.ACTIVATE_OPTIONS
			{
				sExtraData = extraData
			};
			activate_OPTIONS.nLength = (uint)Marshal.SizeOf(activate_OPTIONS);
			int num = TurboActivate.Native.ActivationRequestToFileEx(filename, ref activate_OPTIONS);
			if (num <= 8)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					break;
				case 2:
					throw new InvalidProductKeyException();
				default:
					if (num == 8)
					{
						throw new ProductDetailsException();
					}
					break;
				}
			}
			else
			{
				if (num == 11)
				{
					throw new COMException();
				}
				switch (num)
				{
				case 18:
					throw new ExtraDataTooLongException();
				case 19:
					throw new InvalidArgsException();
				}
			}
			throw new TurboActivateException("Failed to save the activation request file.");
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0002EE6C File Offset: 0x0002D06C
		public static void ActivateFromFile(string filename)
		{
			int num = TurboActivate.Native.ActivateFromFile(filename);
			if (num <= 8)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					break;
				case 2:
					throw new InvalidProductKeyException();
				default:
					if (num == 8)
					{
						throw new ProductDetailsException();
					}
					break;
				}
			}
			else
			{
				switch (num)
				{
				case 11:
					throw new COMException();
				case 12:
					break;
				case 13:
					throw new DateTimeException(true);
				default:
					if (num == 17)
					{
						throw new VirtualMachineException();
					}
					break;
				}
			}
			throw new TurboActivateException("Failed to activate.");
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00003842 File Offset: 0x00001A42
		public static void BlackListKeys(string[] keys)
		{
			if (TurboActivate.Native.BlackListKeys(keys, (uint)keys.Length) != 0)
			{
				throw new TurboActivateException("Failed to add keys to the blacklist. Make sure the keys string array is not empty or null.");
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0002EEE4 File Offset: 0x0002D0E4
		public static bool CheckAndSavePKey(string productKey, TurboActivate.TA_Flags flags = TurboActivate.TA_Flags.TA_USER)
		{
			int num = TurboActivate.Native.CheckAndSavePKey(productKey, flags);
			if (num == 0)
			{
				return true;
			}
			if (num == 8)
			{
				throw new ProductDetailsException();
			}
			if (num == 15)
			{
				throw new PermissionException();
			}
			if (num != 16)
			{
				return true;
			}
			throw new InvalidFlagsException();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0002EF24 File Offset: 0x0002D124
		public static void Deactivate(bool eraseProductKey = false)
		{
			int num = TurboActivate.Native.Deactivate(eraseProductKey ? 1 : 0);
			switch (num)
			{
			case 0:
				return;
			case 1:
			case 5:
			case 6:
			case 7:
				break;
			case 2:
				throw new InvalidProductKeyException();
			case 3:
				throw new NotActivatedException();
			case 4:
				throw new InternetException();
			case 8:
				throw new ProductDetailsException();
			default:
				if (num == 11)
				{
					throw new COMException();
				}
				if (num == 24)
				{
					throw new NoMoreDeactivationsException();
				}
				break;
			}
			throw new TurboActivateException("Failed to deactivate.");
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0002EFA4 File Offset: 0x0002D1A4
		public static void DeactivationRequestToFile(string filename, bool eraseProductKey)
		{
			int num = TurboActivate.Native.DeactivationRequestToFile(filename, eraseProductKey ? 1 : 0);
			switch (num)
			{
			case 0:
				return;
			case 1:
				break;
			case 2:
				throw new InvalidProductKeyException();
			case 3:
				throw new NotActivatedException();
			default:
				if (num == 8)
				{
					throw new ProductDetailsException();
				}
				if (num == 11)
				{
					throw new COMException();
				}
				break;
			}
			throw new TurboActivateException("Failed to deactivate.");
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0002F004 File Offset: 0x0002D204
		public static string GetExtraData()
		{
			int extraData = TurboActivate.Native.GetExtraData(null, 0);
			StringBuilder stringBuilder = new StringBuilder(extraData);
			int extraData2 = TurboActivate.Native.GetExtraData(stringBuilder, extraData);
			if (extraData2 == 0)
			{
				return stringBuilder.ToString();
			}
			if (extraData2 == 8)
			{
				throw new ProductDetailsException();
			}
			return null;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0002F040 File Offset: 0x0002D240
		public static string GetFeatureValue(string featureName)
		{
			string featureValue = TurboActivate.GetFeatureValue(featureName, null);
			if (featureValue == null)
			{
				throw new TurboActivateException("Failed to get feature value. The feature doesn't exist.");
			}
			return featureValue;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0002F064 File Offset: 0x0002D264
		public static string GetFeatureValue(string featureName, string defaultValue)
		{
			int featureValue = TurboActivate.Native.GetFeatureValue(featureName, null, 0);
			StringBuilder stringBuilder = new StringBuilder(featureValue);
			int featureValue2 = TurboActivate.Native.GetFeatureValue(featureName, stringBuilder, featureValue);
			if (featureValue2 == 0)
			{
				return stringBuilder.ToString();
			}
			if (featureValue2 == 8)
			{
				throw new ProductDetailsException();
			}
			return defaultValue;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0002F0A0 File Offset: 0x0002D2A0
		public static string GetPKey()
		{
			StringBuilder stringBuilder = new StringBuilder(35);
			int pkey = TurboActivate.Native.GetPKey(stringBuilder, 35);
			switch (pkey)
			{
			case 0:
				return stringBuilder.ToString();
			case 1:
				break;
			case 2:
				throw new InvalidProductKeyException();
			default:
				if (pkey == 8)
				{
					throw new ProductDetailsException();
				}
				break;
			}
			throw new TurboActivateException("Failed to get the product key.");
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0002F0F4 File Offset: 0x0002D2F4
		[Obsolete("This function is obsolete and will be removed in TurboActivate 4.0; use the UseTrial(), TrialDaysRemaining(), and ExtendTrial() functions instead.")]
		public static int GracePeriodDaysRemaining()
		{
			uint result = 0u;
			int num = TurboActivate.Native.GracePeriodDaysRemaining(TurboActivate.VersionGUID, ref result);
			if (num == 0)
			{
				return (int)result;
			}
			switch (num)
			{
			case 7:
				throw new GUIDMismatchException();
			case 8:
				throw new ProductDetailsException();
			default:
				throw new TurboActivateException("Failed to get the activation grace period days remaining.");
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0002F13C File Offset: 0x0002D33C
		public static bool IsActivated()
		{
			int num = TurboActivate.Native.IsActivated(TurboActivate.VersionGUID);
			if (num != 0)
			{
				switch (num)
				{
				case 7:
					throw new GUIDMismatchException();
				case 8:
					throw new ProductDetailsException();
				case 9:
				case 10:
					break;
				case 11:
					throw new COMException();
				default:
					if (num == 17)
					{
						throw new VirtualMachineException();
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0002F198 File Offset: 0x0002D398
		public static bool IsDateValid(string date_time, TurboActivate.TA_DateCheckFlags flags)
		{
			int num = TurboActivate.Native.IsDateValid(date_time, flags);
			if (num == 0)
			{
				return true;
			}
			if (num == 8)
			{
				throw new ProductDetailsException();
			}
			if (num != 16)
			{
				return false;
			}
			throw new InvalidFlagsException();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0002F1CC File Offset: 0x0002D3CC
		[Obsolete("This particular override for the IsGenuine() function is obsolete; use the IsGenuine() with no arguments or IsGenuine(uint dayBetweenChecks, uint graceDaysOnInetErr, bool skipOffline = false, bool offlineShowInetErr = false).")]
		public static bool IsGenuine(ref bool needsReactivate)
		{
			int num = TurboActivate.Native.IsGenuine(TurboActivate.VersionGUID);
			if (num <= 13)
			{
				switch (num)
				{
				case 0:
					goto IL_88;
				case 1:
				case 2:
				case 5:
				case 6:
					break;
				case 3:
					throw new NotActivatedException();
				case 4:
					throw new InternetException();
				case 7:
					throw new GUIDMismatchException();
				case 8:
					throw new ProductDetailsException();
				default:
					switch (num)
					{
					case 11:
						throw new COMException();
					case 13:
						throw new DateTimeException(false);
					}
					break;
				}
			}
			else
			{
				if (num == 17)
				{
					throw new VirtualMachineException();
				}
				if (num == 22)
				{
					goto IL_88;
				}
			}
			return false;
			IL_88:
			needsReactivate = false;
			return true;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0002F268 File Offset: 0x0002D468
		public static IsGenuineResult IsGenuine()
		{
			int num = TurboActivate.Native.IsGenuine(TurboActivate.VersionGUID);
			if (num <= 8)
			{
				if (num == 0)
				{
					return IsGenuineResult.Genuine;
				}
				switch (num)
				{
				case 4:
					return IsGenuineResult.InternetError;
				case 7:
					throw new GUIDMismatchException();
				case 8:
					throw new ProductDetailsException();
				}
			}
			else
			{
				switch (num)
				{
				case 11:
					throw new COMException();
				case 12:
					break;
				case 13:
					throw new DateTimeException(false);
				default:
					if (num == 17)
					{
						return IsGenuineResult.NotGenuineInVM;
					}
					if (num == 22)
					{
						return IsGenuineResult.GenuineFeaturesChanged;
					}
					break;
				}
			}
			return IsGenuineResult.NotGenuine;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0002F2EC File Offset: 0x0002D4EC
		public static IsGenuineResult IsGenuine(uint daysBetweenChecks, uint graceDaysOnInetErr, bool skipOffline = false, bool offlineShowInetErr = false)
		{
			TurboActivate.Native.GENUINE_OPTIONS genuine_OPTIONS = new TurboActivate.Native.GENUINE_OPTIONS
			{
				nDaysBetweenChecks = daysBetweenChecks,
				nGraceDaysOnInetErr = graceDaysOnInetErr,
				flags = (TurboActivate.Native.GenuineFlags)0u
			};
			genuine_OPTIONS.nLength = (uint)Marshal.SizeOf(genuine_OPTIONS);
			if (skipOffline)
			{
				genuine_OPTIONS.flags |= TurboActivate.Native.GenuineFlags.TA_SKIP_OFFLINE;
				if (offlineShowInetErr)
				{
					genuine_OPTIONS.flags |= TurboActivate.Native.GenuineFlags.TA_OFFLINE_SHOW_INET_ERR;
				}
			}
			int num = TurboActivate.Native.IsGenuineEx(TurboActivate.VersionGUID, ref genuine_OPTIONS);
			if (num <= 8)
			{
				if (num == 0)
				{
					return IsGenuineResult.Genuine;
				}
				switch (num)
				{
				case 4:
					break;
				case 5:
				case 6:
					return IsGenuineResult.NotGenuine;
				case 7:
					throw new GUIDMismatchException();
				case 8:
					throw new ProductDetailsException();
				default:
					return IsGenuineResult.NotGenuine;
				}
			}
			else
			{
				switch (num)
				{
				case 11:
					throw new COMException();
				case 12:
					return IsGenuineResult.NotGenuine;
				case 13:
					throw new DateTimeException(false);
				default:
					switch (num)
					{
					case 17:
						return IsGenuineResult.NotGenuineInVM;
					case 18:
					case 20:
						return IsGenuineResult.NotGenuine;
					case 19:
						throw new InvalidArgsException();
					case 21:
						break;
					case 22:
						return IsGenuineResult.GenuineFeaturesChanged;
					default:
						return IsGenuineResult.NotGenuine;
					}
					break;
				}
			}
			return IsGenuineResult.InternetError;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0002F3E4 File Offset: 0x0002D5E4
		public static bool IsProductKeyValid()
		{
			int num = TurboActivate.Native.IsProductKeyValid(TurboActivate.VersionGUID);
			if (num == 0)
			{
				return true;
			}
			switch (num)
			{
			case 7:
				throw new GUIDMismatchException();
			case 8:
				throw new ProductDetailsException();
			default:
				return false;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000385A File Offset: 0x00001A5A
		public static void SetCustomProxy(string proxy)
		{
			if (TurboActivate.Native.SetCustomProxy(proxy) != 0)
			{
				throw new TurboActivateException("Failed to set the custom proxy.");
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0002F420 File Offset: 0x0002D620
		public static int TrialDaysRemaining()
		{
			uint result = 0u;
			int num = TurboActivate.Native.TrialDaysRemaining(TurboActivate.VersionGUID, ref result);
			if (num == 0)
			{
				return (int)result;
			}
			switch (num)
			{
			case 7:
				throw new GUIDMismatchException();
			case 8:
				throw new ProductDetailsException();
			default:
				throw new TurboActivateException("Failed to get the trial data.");
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0002F468 File Offset: 0x0002D668
		public static void UseTrial(TurboActivate.TA_Flags flags = TurboActivate.TA_Flags.TA_USER)
		{
			int num = TurboActivate.Native.UseTrial(flags);
			if (num == 0)
			{
				return;
			}
			if (num == 8)
			{
				throw new ProductDetailsException();
			}
			switch (num)
			{
			case 15:
				throw new PermissionException();
			case 16:
				throw new InvalidFlagsException();
			case 17:
				throw new VirtualMachineException();
			default:
				throw new TurboActivateException("Failed to save the trial data.");
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0002F4BC File Offset: 0x0002D6BC
		public static void ExtendTrial(string trialExtension)
		{
			int num = TurboActivate.Native.ExtendTrial(trialExtension);
			if (num <= 4)
			{
				if (num == 0)
				{
					return;
				}
				if (num == 4)
				{
					throw new InternetException();
				}
			}
			else
			{
				if (num == 8)
				{
					throw new ProductDetailsException();
				}
				switch (num)
				{
				case 12:
					throw new TrialExtUsedException();
				case 13:
					throw new TrialExtExpiredException();
				}
			}
			throw new TurboActivateException("Failed to extend trial.");
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0002F518 File Offset: 0x0002D718
		public static void PDetsFromPath(string filename)
		{
			int num = TurboActivate.Native.PDetsFromPath(filename);
			if (num == 0)
			{
				return;
			}
			if (num != 8)
			{
				throw new TurboActivateException("The TurboActivate.dat file has already been loaded. You must call this function only once and before any other TurboActivate function.");
			}
			throw new ProductDetailsException();
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0002F548 File Offset: 0x0002D748
		public static string GetCurrentProduct()
		{
			int currentProduct = TurboActivate.Native.GetCurrentProduct(null, 0);
			StringBuilder stringBuilder = new StringBuilder(currentProduct);
			int currentProduct2 = TurboActivate.Native.GetCurrentProduct(stringBuilder, currentProduct);
			if (currentProduct2 == 0)
			{
				return stringBuilder.ToString();
			}
			throw new TurboActivateException("Failed to get the current product. Make sure you've loaded the product details file using PDetsFromPath().");
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0002F584 File Offset: 0x0002D784
		public static void SetCurrentProduct(string vGuid)
		{
			int num = TurboActivate.Native.SetCurrentProduct(vGuid);
			if (num == 0)
			{
				TurboActivate.VersionGUID = vGuid;
				return;
			}
			throw new TurboActivateException("Failed to set the current product. Make sure you've loaded the product details file using PDetsFromPath().");
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
		public static void SetCustomActDataPath(string directory)
		{
			int num = TurboActivate.Native.SetCustomActDataPath(directory);
			if (num == 0)
			{
				return;
			}
			if (num != 8)
			{
				throw new TurboActivateException("The directory must exist and you must have access to it.");
			}
			throw new ProductDetailsException();
		}

		// Token: 0x04000303 RID: 771
		private const int TA_OK = 0;

		// Token: 0x04000304 RID: 772
		private const int TA_FAIL = 1;

		// Token: 0x04000305 RID: 773
		private const int TA_E_PKEY = 2;

		// Token: 0x04000306 RID: 774
		private const int TA_E_ACTIVATE = 3;

		// Token: 0x04000307 RID: 775
		private const int TA_E_INET = 4;

		// Token: 0x04000308 RID: 776
		private const int TA_E_INUSE = 5;

		// Token: 0x04000309 RID: 777
		private const int TA_E_REVOKED = 6;

		// Token: 0x0400030A RID: 778
		private const int TA_E_GUID = 7;

		// Token: 0x0400030B RID: 779
		private const int TA_E_PDETS = 8;

		// Token: 0x0400030C RID: 780
		private const int TA_E_TRIAL = 9;

		// Token: 0x0400030D RID: 781
		private const int TA_E_REACTIVATE = 10;

		// Token: 0x0400030E RID: 782
		private const int TA_E_COM = 11;

		// Token: 0x0400030F RID: 783
		private const int TA_E_TRIAL_EUSED = 12;

		// Token: 0x04000310 RID: 784
		private const int TA_E_TRIAL_EEXP = 13;

		// Token: 0x04000311 RID: 785
		private const int TA_E_EXPIRED = 13;

		// Token: 0x04000312 RID: 786
		private const int TA_E_INSUFFICIENT_BUFFER = 14;

		// Token: 0x04000313 RID: 787
		private const int TA_E_PERMISSION = 15;

		// Token: 0x04000314 RID: 788
		private const int TA_E_INVALID_FLAGS = 16;

		// Token: 0x04000315 RID: 789
		private const int TA_E_IN_VM = 17;

		// Token: 0x04000316 RID: 790
		private const int TA_E_EDATA_LONG = 18;

		// Token: 0x04000317 RID: 791
		private const int TA_E_INVALID_ARGS = 19;

		// Token: 0x04000318 RID: 792
		private const int TA_E_KEY_FOR_TURBOFLOAT = 20;

		// Token: 0x04000319 RID: 793
		private const int TA_E_INET_DELAYED = 21;

		// Token: 0x0400031A RID: 794
		private const int TA_E_FEATURES_CHANGED = 22;

		// Token: 0x0400031B RID: 795
		private const int TA_E_NO_MORE_DEACTIVATIONS = 24;

		// Token: 0x02000059 RID: 89
		[Flags]
		public enum TA_Flags : uint
		{
			// Token: 0x0400031E RID: 798
			TA_SYSTEM = 1u,
			// Token: 0x0400031F RID: 799
			TA_USER = 2u,
			// Token: 0x04000320 RID: 800
			TA_DISALLOW_VM = 4u
		}

		// Token: 0x0200005A RID: 90
		[Flags]
		public enum TA_DateCheckFlags : uint
		{
			// Token: 0x04000322 RID: 802
			TA_HAS_NOT_EXPIRED = 1u
		}

		// Token: 0x0200005B RID: 91
		private static class Native
		{
			// Token: 0x06000327 RID: 807
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int Activate();

			// Token: 0x06000328 RID: 808
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int ActivateEx(ref TurboActivate.Native.ACTIVATE_OPTIONS options);

			// Token: 0x06000329 RID: 809
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int ActivationRequestToFile(string filename);

			// Token: 0x0600032A RID: 810
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int ActivationRequestToFileEx(string filename, ref TurboActivate.Native.ACTIVATE_OPTIONS options);

			// Token: 0x0600032B RID: 811
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int ActivateFromFile(string filename);

			// Token: 0x0600032C RID: 812
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int BlackListKeys([In] string[] keys, uint numKeys);

			// Token: 0x0600032D RID: 813
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int CheckAndSavePKey(string productKey, TurboActivate.TA_Flags flags);

			// Token: 0x0600032E RID: 814
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int Deactivate(byte erasePkey);

			// Token: 0x0600032F RID: 815
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int DeactivationRequestToFile(string filename, byte erasePkey);

			// Token: 0x06000330 RID: 816
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int GetExtraData(StringBuilder lpValueStr, int cchValue);

			// Token: 0x06000331 RID: 817
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int GetFeatureValue(string featureName, StringBuilder lpValueStr, int cchValue);

			// Token: 0x06000332 RID: 818
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int GetPKey(StringBuilder lpPKeyStr, int cchPKey);

			// Token: 0x06000333 RID: 819
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int IsActivated(string versionGUID);

			// Token: 0x06000334 RID: 820
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int IsDateValid(string date_time, TurboActivate.TA_DateCheckFlags flags);

			// Token: 0x06000335 RID: 821
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int IsGenuine(string versionGUID);

			// Token: 0x06000336 RID: 822
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int IsGenuineEx(string versionGUID, ref TurboActivate.Native.GENUINE_OPTIONS options);

			// Token: 0x06000337 RID: 823
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int IsProductKeyValid(string versionGUID);

			// Token: 0x06000338 RID: 824
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int SetCustomProxy(string proxy);

			// Token: 0x06000339 RID: 825
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TrialDaysRemaining(string versionGUID, ref uint DaysRemaining);

			// Token: 0x0600033A RID: 826
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int UseTrial(TurboActivate.TA_Flags flags);

			// Token: 0x0600033B RID: 827
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int ExtendTrial(string trialExtension);

			// Token: 0x0600033C RID: 828
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int PDetsFromPath(string filename);

			// Token: 0x0600033D RID: 829
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int SetCurrentProduct(string versionGUID);

			// Token: 0x0600033E RID: 830
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int GetCurrentProduct(StringBuilder lpValueStr, int cchValue);

			// Token: 0x0600033F RID: 831
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int SetCustomActDataPath(string directory);

			// Token: 0x06000340 RID: 832
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int GracePeriodDaysRemaining(string versionGUID, ref uint DaysRemaining);

			// Token: 0x0200005C RID: 92
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct ACTIVATE_OPTIONS
			{
				// Token: 0x04000323 RID: 803
				public uint nLength;

				// Token: 0x04000324 RID: 804
				[MarshalAs(UnmanagedType.LPWStr)]
				public string sExtraData;
			}

			// Token: 0x0200005D RID: 93
			[Flags]
			public enum GenuineFlags : uint
			{
				// Token: 0x04000326 RID: 806
				TA_SKIP_OFFLINE = 1u,
				// Token: 0x04000327 RID: 807
				TA_OFFLINE_SHOW_INET_ERR = 2u
			}

			// Token: 0x0200005E RID: 94
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct GENUINE_OPTIONS
			{
				// Token: 0x04000328 RID: 808
				public uint nLength;

				// Token: 0x04000329 RID: 809
				public TurboActivate.Native.GenuineFlags flags;

				// Token: 0x0400032A RID: 810
				public uint nDaysBetweenChecks;

				// Token: 0x0400032B RID: 811
				public uint nGraceDaysOnInetErr;
			}
		}
	}
}
