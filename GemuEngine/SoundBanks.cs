using System;
using System.Collections.Generic;

namespace Gemu
{
	// Token: 0x02000027 RID: 39
	public class SoundBanks
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00004844 File Offset: 0x00002A44
		public static SoundBank getBank(string name)
		{
			SoundBank result;
			if (SoundBanks.banks.ContainsKey(name))
			{
				result = SoundBanks.banks[name];
			}
			else
			{
				result = SoundBanks.banks["Carmella"];
			}
			return result;
		}

		// Token: 0x040000B3 RID: 179
		public static Dictionary<string, SoundBank> banks = new Dictionary<string, SoundBank>();
	}
}
