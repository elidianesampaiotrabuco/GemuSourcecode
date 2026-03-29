using System;
using System.Collections.Generic;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using UnityEngine;

namespace Gemu
{
	// Token: 0x02000022 RID: 34
	public class ModContents
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00003784 File Offset: 0x00001984
		public ModContents()
		{
			Debug.Log("Mod Setup Done");
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000037E8 File Offset: 0x000019E8
		public PosterObject CreateNPCPoster(string file, string nameKey, string descKey, string path)
		{
			return ObjectCreators.CreateCharacterPoster(AssetLoader.TextureFromFile(path + "/" + file + ".png"), nameKey, descKey);
		}

		// Token: 0x040000AA RID: 170
		public Dictionary<string, WeightedNPC> newNpcs = new Dictionary<string, WeightedNPC>();

		// Token: 0x040000AB RID: 171
		public Dictionary<string, WeightedItemObject> newItems = new Dictionary<string, WeightedItemObject>();

		// Token: 0x040000AC RID: 172
		public Dictionary<string, WeightedRoomAsset> newRooms = new Dictionary<string, WeightedRoomAsset>();

		// Token: 0x040000AD RID: 173
		public Dictionary<string, WeightedRandomEvent> newEvents = new Dictionary<string, WeightedRandomEvent>();

		// Token: 0x040000AE RID: 174
		public Dictionary<string, GameObject> modPrefabs = new Dictionary<string, GameObject>();

		// Token: 0x040000AF RID: 175
		public Dictionary<string, WeightedStructureWithParameters> newStructures = new Dictionary<string, WeightedStructureWithParameters>();

		// Token: 0x040000B0 RID: 176
		public string imagePath;

		// Token: 0x040000B1 RID: 177
		public string audioPath;
	}
}
