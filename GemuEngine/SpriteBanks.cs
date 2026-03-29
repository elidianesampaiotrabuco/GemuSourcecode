using System;
using System.Collections.Generic;

namespace Gemu
{
	// Token: 0x02000026 RID: 38
	public class SpriteBanks
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000047EC File Offset: 0x000029EC
		public static SpriteBank getBank(string name)
		{
			SpriteBank result;
			if (SpriteBanks.banks.ContainsKey(name))
			{
				result = SpriteBanks.banks[name];
			}
			else
			{
				result = SpriteBanks.banks["Carmella"];
			}
			return result;
		}

		// Token: 0x040000B2 RID: 178
		public static Dictionary<string, SpriteBank> banks = new Dictionary<string, SpriteBank>();
	}
}
