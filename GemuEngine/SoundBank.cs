using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class SoundBank
{
	// Token: 0x06000020 RID: 32 RVA: 0x0000279D File Offset: 0x0000099D
	public SoundBank(string name)
	{
		this.bankName = name;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000027BA File Offset: 0x000009BA
	public void AddSound(string name, SoundObject sprite)
	{
		this.allSounds.Add(name, sprite);
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000027CC File Offset: 0x000009CC
	public SoundObject GetSound(string name)
	{
		SoundObject result;
		if (this.allSounds.ContainsKey(name))
		{
			result = this.allSounds[name];
		}
		else
		{
			Debug.LogError(string.Concat(new object[]
			{
				"No SOUND called ",
				name,
				" exists in ",
				this.allSounds,
				" Size:",
				this.allSounds.Count
			}));
			result = null;
		}
		return result;
	}

	// Token: 0x0400008D RID: 141
	public string bankName;

	// Token: 0x0400008E RID: 142
	public Dictionary<string, SoundObject> allSounds = new Dictionary<string, SoundObject>();
}
