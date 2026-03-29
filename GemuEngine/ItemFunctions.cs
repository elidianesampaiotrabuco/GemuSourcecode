using System;
using System.Collections.Generic;
using Gemu.JSON;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using Newtonsoft.Json;
using UnityEngine;

namespace Gemu
{
	// Token: 0x02000024 RID: 36
	public class ItemFunctions
	{
		// Token: 0x06000061 RID: 97 RVA: 0x000046C8 File Offset: 0x000028C8
		[Obsolete("USE ItemBuilder.SetItemComponent<T> instead", false)]
		public static GameObject CreateItemObject<T>(string name, bool prefabPost = false, bool setActive = true) where T : Item
		{
			GameObject itemObj = new GameObject(name);
			itemObj.AddComponent<T>();
			if (!prefabPost)
			{
				itemObj.ConvertToPrefab(setActive);
			}
			return itemObj;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000046F8 File Offset: 0x000028F8
		public static List<WeightedItemObject> parseItemTable(string data, AssetManager plugin)
		{
			List<WeightedItemObject> output = new List<WeightedItemObject>();
			JsonItemTable dataParsed = JsonConvert.DeserializeObject<JsonItemTable>(data);
			foreach (JsonItem item in dataParsed.items)
			{
				output.Add(new WeightedItemObject
				{
					selection = ObjectFunctions.FindResourceOfName<ItemObject>(item.name, plugin),
					weight = item.weight
				});
			}
			return output;
		}
	}
}
