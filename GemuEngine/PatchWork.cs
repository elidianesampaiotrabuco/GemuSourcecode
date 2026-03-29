using System;
using BepInEx;
using HarmonyLib;
using MTM101BaldAPI;
using UnityEngine;

// Token: 0x0200001B RID: 27
[BepInPlugin("dee4.games.baldiplus.gemuengine", "Gemu Mod Engine", "1.0.0")]
[BepInDependency("mtm101.rulerp.bbplus.baldidevapi", BepInDependency.DependencyFlags.HardDependency)]
public class PatchWork : BaseUnityPlugin
{
	// Token: 0x0600002F RID: 47 RVA: 0x00002D44 File Offset: 0x00000F44
	private void Awake()
	{
		Harmony harmony = new Harmony("dee4.games.baldiplus.gemuengine");
		harmony.PatchAllConditionals();
		Debug.Log("Patcher was successful");
	}
}
