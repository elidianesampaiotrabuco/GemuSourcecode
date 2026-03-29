using System;
using HarmonyLib;

// Token: 0x0200001F RID: 31
[HarmonyPatch(typeof(ItemManager), "RemoveItem")]
[HarmonyPriority(800)]
internal class NoConsumptions
{
	// Token: 0x06000040 RID: 64 RVA: 0x000036F4 File Offset: 0x000018F4
	private static bool Prefix()
	{
		return !DebugMenu.infItems;
	}
}
