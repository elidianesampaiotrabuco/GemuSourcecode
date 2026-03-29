using System;
using HarmonyLib;

// Token: 0x0200001D RID: 29
public class ApiPatches
{
	// Token: 0x06000033 RID: 51 RVA: 0x00002DD9 File Offset: 0x00000FD9
	[HarmonyPostfix]
	[HarmonyPatch("Clicked")]
	[HarmonyPatch(typeof(Notebook))]
	public static void NotebookCollect()
	{
		APIActions.onNotebookCollect();
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002DE8 File Offset: 0x00000FE8
	[HarmonyPatch("UseItem")]
	[HarmonyPatch(typeof(ItemManager))]
	[HarmonyPostfix]
	public static void ItemUse(ItemManager __instance)
	{
		if (__instance.items[__instance.selectedItem].itemType != 0)
		{
			APIActions.onItemUse();
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002E1C File Offset: 0x0000101C
	[HarmonyPatch(typeof(Pickup))]
	[HarmonyPatch("Collect")]
	[HarmonyPostfix]
	public static void ItemCollect(Pickup __instance)
	{
		if (__instance.item.itemType != 0)
		{
			APIActions.onItemCollect();
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002E49 File Offset: 0x00001049
	[HarmonyPatch(typeof(Principal))]
	[HarmonyPostfix]
	[HarmonyPatch("SendToDetention")]
	public static void Detention()
	{
		APIActions.onDetentionGet();
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002E57 File Offset: 0x00001057
	[HarmonyPatch("SpawnNPCs")]
	[HarmonyPatch(typeof(EnvironmentController))]
	[HarmonyPostfix]
	public static void Spawned()
	{
		APIActions.onNpcSpawns();
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002E65 File Offset: 0x00001065
	[HarmonyPatch(typeof(BaseGameManager))]
	[HarmonyPatch("BeginPlay")]
	[HarmonyPostfix]
	public static void BeginGame()
	{
		APIActions.onGameStart();
	}
}
