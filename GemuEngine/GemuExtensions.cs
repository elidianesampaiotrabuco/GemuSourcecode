using System;
using MTM101BaldAPI.Reflection;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000029 RID: 41
public static class GemuExtensions
{
	// Token: 0x06000072 RID: 114 RVA: 0x000048E0 File Offset: 0x00002AE0
	public static NPC GetNPC(this EnvironmentController ec, string character)
	{
		foreach (NPC npc in ec.Npcs)
		{
			if (npc.Character.ToString().ToLower() == character.ToLower())
			{
				return npc;
			}
		}
		Debug.LogError("No npc exists with that name, try again.");
		return null;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00004974 File Offset: 0x00002B74
	public static Color from255(this Color color, float R, float G, float B)
	{
		return new Color(R / 255f, G / 255f, B / 255f);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x000049A0 File Offset: 0x00002BA0
	public static void Freeze(this NPC npc, MovementModifier moveMod, Color color = default(Color))
	{
		npc.spriteRenderer[0].color = color;
		npc.GetComponent<ActivityModifier>().moveMods.Add(moveMod);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x000049C4 File Offset: 0x00002BC4
	public static void Unfreeze(this NPC npc, MovementModifier moveMod)
	{
		npc.spriteRenderer[0].color = Color.white;
		npc.GetComponent<ActivityModifier>().moveMods.Remove(moveMod);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x000049EC File Offset: 0x00002BEC
	public static void baldi_Enable(this BaldiTV tv, bool enabled)
	{
		Image img = (Image)tv.ReflectionGetVariable("baldiImage");
		if (enabled)
		{
			img.color = Color.white;
		}
		else
		{
			img.color = Color.clear;
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00004A2E File Offset: 0x00002C2E
	public static void SetSubcolor(this AudioManager audioManager, Color color)
	{
		audioManager.ReflectionSetVariable("subtitleColor", color);
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00004A43 File Offset: 0x00002C43
	public static void SetSuboveride(this AudioManager audioManager, bool oride)
	{
		audioManager.ReflectionSetVariable("overrideSubtitleColor", oride);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00004A58 File Offset: 0x00002C58
	public static float Height(this Entity entity, bool useBase = true)
	{
		return entity.get_Height(useBase);
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00004A74 File Offset: 0x00002C74
	public static float get_Height(this Entity entity, bool useBase = true)
	{
		float result;
		if (useBase)
		{
			result = entity.BaseHeight;
		}
		else
		{
			result = entity.InternalHeight;
		}
		return result;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00004AA0 File Offset: 0x00002CA0
	public static void AddAffector<T>(this Entity entity) where T : Affector
	{
		entity.gameObject.AddComponent<T>();
		Affector affector = entity.GetComponent<Affector>();
		affector.affectedEntity = entity;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00004AC8 File Offset: 0x00002CC8
	public static void EnableEffector(this Entity entity, bool val)
	{
		if (entity.GetComponent<Affector>() != null)
		{
			entity.GetComponent<Affector>().Enable(val);
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00004AF8 File Offset: 0x00002CF8
	public static void AddMapBackground(this RoomAsset room, Texture2D texture)
	{
		room.mapMaterial = new Material(room.mapMaterial);
		room.mapMaterial.SetTexture("_MapBackground", texture);
		room.mapMaterial.shaderKeywords = new string[]
		{
			"_KEYMAPSHOWBACKGROUND_ON"
		};
		room.mapMaterial.name = room.name;
	}
}
