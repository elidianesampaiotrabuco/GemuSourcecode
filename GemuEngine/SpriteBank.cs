using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class SpriteBank
{
	// Token: 0x0600001D RID: 29 RVA: 0x000026ED File Offset: 0x000008ED
	public SpriteBank(string name)
	{
		this.bankName = name;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000270A File Offset: 0x0000090A
	public void AddSprite(string name, Sprite sprite)
	{
		this.allSprites.Add(name, sprite);
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000271C File Offset: 0x0000091C
	public Sprite GetSprite(string name)
	{
		Sprite result;
		if (this.allSprites.ContainsKey(name))
		{
			result = this.allSprites[name];
		}
		else
		{
			Debug.LogError(string.Concat(new object[]
			{
				"No Sprite called ",
				name,
				" exists in ",
				this.allSprites,
				" Size:",
				this.allSprites.Count
			}));
			result = null;
		}
		return result;
	}

	// Token: 0x0400008B RID: 139
	public string bankName;

	// Token: 0x0400008C RID: 140
	public Dictionary<string, Sprite> allSprites = new Dictionary<string, Sprite>();
}
