using System;
using System.Collections.Generic;
using Gemu.JSON;
using MTM101BaldAPI.AssetTools;

namespace Gemu
{
	// Token: 0x02000025 RID: 37
	public class LevelFunctions
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00004778 File Offset: 0x00002978
		public static List<WeightedNPC> ProcessNPCList(JsonWeight[] weights, AssetManager plugin)
		{
			List<WeightedNPC> output = new List<WeightedNPC>();
			foreach (JsonWeight w in weights)
			{
				output.Add(new WeightedNPC
				{
					selection = ObjectFunctions.FindResourceOfName<NPC>(w.value, plugin),
					weight = w.weight
				});
			}
			return output;
		}
	}
}
