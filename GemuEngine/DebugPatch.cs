using System;
using HarmonyLib;

// Token: 0x0200001C RID: 28
[HarmonyPatch(typeof(BaseGameManager))]
[HarmonyPatch("BeginPlay")]
public class DebugPatch
{
	// Token: 0x06000031 RID: 49 RVA: 0x00002D78 File Offset: 0x00000F78
	public static void Prefix(BaseGameManager __instance)
	{
		if (Singleton<PlayerFileManager>.Instance.fileName == "USERDEV")
		{
			__instance.gameObject.AddComponent<DebugMenu>();
			__instance.gameObject.GetComponent<DebugMenu>().Setup(__instance.Ec, Singleton<CoreGameManager>.Instance.GetPlayer(0));
		}
	}
}
