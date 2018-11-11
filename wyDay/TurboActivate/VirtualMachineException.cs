using System;

namespace wyDay.TurboActivate
{
	// Token: 0x0200006D RID: 109
	public class VirtualMachineException : TurboActivateException
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000392B File Offset: 0x00001B2B
		public VirtualMachineException() : base("The function failed because this instance of your program is running inside a virtual machine / hypervisor and you've prevented the function from running inside a VM.")
		{
		}
	}
}
