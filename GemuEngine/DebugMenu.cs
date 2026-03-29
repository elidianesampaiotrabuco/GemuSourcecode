using System;
using System.Collections.Generic;
using HarmonyLib;
using MTM101BaldAPI.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200001E RID: 30
public class DebugMenu : MonoBehaviour
{
	// Token: 0x0600003A RID: 58 RVA: 0x00002E7C File Offset: 0x0000107C
	public void Setup(EnvironmentController ec, PlayerManager pm)
	{
		this.debugLogs = new List<string>();
		this.ec = ec;
		this.pm = pm;
		this.npcs = Resources.FindObjectsOfTypeAll<NPC>();
		this.items = Resources.FindObjectsOfTypeAll<ItemObject>();
		this.scenes = Resources.FindObjectsOfTypeAll<SceneObject>();
		this.Initalzied = true;
		DebugMenu.instance = this;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002ED4 File Offset: 0x000010D4
	public static void LogEvent(object eventToLog)
	{
		if (DebugMenu.instance != null)
		{
			if (DebugMenu.instance.Initalzied)
			{
				DebugMenu.instance.debugLogs.Add(eventToLog.ToString());
				if (DebugMenu.instance.debugLogs.Count > 20)
				{
					DebugMenu.instance.debugLogs.RemoveAt(0);
				}
				Debug.Log(eventToLog);
			}
		}
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002FF8 File Offset: 0x000011F8
	private void OnGUI()
	{
		if (this.Initalzied)
		{
			if (this.TurnedON)
			{
				this.wtf = GUI.BeginScrollView(new Rect(230f, 20f, 180f, 360f), this.wtf, new Rect(230f, 20f, 180f, 3000f));
				for (int i = 0; i < this.npcs.Length; i++)
				{
					if (GUI.Button(new Rect(230f, 20f + 20f * (float)i, 160f, 15f), "Spawn " + this.npcs[i].name + " here"))
					{
						this.SpawnNpcDebug(this.npcs[i]);
					}
				}
				GUI.EndScrollView();
				this.wtf2 = GUI.BeginScrollView(new Rect(430f, 20f, 180f, 360f), this.wtf2, new Rect(430f, 20f, 180f, 3000f));
				for (int j = 0; j < this.items.Length; j++)
				{
					if (GUI.Button(new Rect(430f, 20f + 20f * (float)j, 160f, 15f), "Spawn " + this.items[j].name + " here"))
					{
						Pickup pickupPre = (Pickup)AccessTools.Field(typeof(EnvironmentController), "pickupPre").GetValue(this.ec);
						Pickup doodoo = UnityEngine.Object.Instantiate<Pickup>(pickupPre, this.ec.transform);
						doodoo.item = this.items[j];
						doodoo.transform.position = this.pm.transform.position;
					}
				}
				GUI.EndScrollView();
				this.wtf3 = GUI.BeginScrollView(new Rect(630f, 20f, 180f, 360f), this.wtf3, new Rect(630f, 20f, 180f, 3000f));
				for (int k = 0; k < this.scenes.Length; k++)
				{
					if (GUI.Button(new Rect(630f, 20f + 20f * (float)k, 160f, 15f), "Load " + this.scenes[k].nameKey + " here"))
					{
						SceneObject scene = this.scenes[k];
						Singleton<BaseGameManager>.Instance.StopAllCoroutines();
						Singleton<BaseGameManager>.Instance.Ec.ResetEvents();
						Time.timeScale = 0f;
						Singleton<CoreGameManager>.Instance.readyToStart = false;
						Singleton<CoreGameManager>.Instance.disablePause = true;
						PropagatedAudioManager.paused = true;
						Singleton<BaseGameManager>.Instance.ReflectionSetVariable("elevatorScreen", UnityEngine.Object.Instantiate<ElevatorScreen>((ElevatorScreen)Singleton<BaseGameManager>.Instance.ReflectionGetVariable("elevatorScreenPre")));
						ElevatorScreen elevatorScreen = Singleton<BaseGameManager>.Instance.ReflectionGetVariable("elevatorScreen") as ElevatorScreen;
						ElevatorScreen elevatorScreen2 = elevatorScreen;
						ElevatorScreen.OnLoadReadyHandler value = delegate()
						{
							Singleton<BaseGameManager>.Instance.StopAllCoroutines();
							Singleton<BaseGameManager>.Instance.Ec.ResetEvents();
							Time.timeScale = 0f;
							Singleton<CoreGameManager>.Instance.readyToStart = false;
							Singleton<CoreGameManager>.Instance.disablePause = true;
							PropagatedAudioManager.paused = true;
							Singleton<CoreGameManager>.Instance.PrepareForReload();
							Singleton<CoreGameManager>.Instance.SetLives(3, true);
							Singleton<CoreGameManager>.Instance.tripPlayed = false;
							Singleton<SubtitleManager>.Instance.DestroyAll();
							Singleton<CoreGameManager>.Instance.sceneObject = scene;
							SceneManager.LoadSceneAsync("Game");
						};
						elevatorScreen2.OnLoadReady += value;
						elevatorScreen.Initialize();
					}
				}
				GUI.EndScrollView();
				if (GUI.Button(new Rect(30f, 20f, 160f, 30f), "Fill map"))
				{
					this.ec.map.CompleteMap();
				}
				for (int k = 0; k < this.debugLogs.Count; k++)
				{
					GUI.Label(new Rect(30f, (float)(Screen.height - 20) - 20f * (float)k, 230f, 20f), this.debugLogs[k]);
				}
				this.infItemsInternal = GUI.Toggle(new Rect(30f, 70f, 160f, 30f), this.infItemsInternal, "Infinite Items");
				DebugMenu.infItems = this.infItemsInternal;
				this.npcOnPlayer = GUI.Toggle(new Rect(30f, 120f, 160f, 30f), this.npcOnPlayer, "NPCs Spawn on Player");
				if (GUI.Button(new Rect(30f, 170f, 160f, 30f), "Clear Player moveMods"))
				{
					this.pm.Am.moveMods.Clear();
				}
				Transform player = Singleton<CoreGameManager>.Instance.GetPlayer(0).transform;
				RaycastHit hit;
				if (Physics.Raycast(player.position, player.forward, out hit, 1000f))
				{
					if (hit.transform != null)
					{
						GUI.Label(new Rect(630f, 20f, 360f, 30f), hit.transform.name + hit.transform.gameObject.layer);
					}
				}
				IntVector2 intVector = IntVector2.GetGridPosition(player.position);
				GUI.Label(new Rect(630f, 40f, 360f, 30f), intVector.x + ", " + intVector.z);
			}
		}
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000035B4 File Offset: 0x000017B4
	private void SpawnNpcDebug(NPC npc)
	{
		if (this.npcOnPlayer)
		{
			this.ec.SpawnNPC(npc, IntVector2.GetGridPosition(this.pm.transform.position));
		}
		else
		{
			this.ec.SpawnNPC(npc, IntVector2.GetGridPosition(this.ec.npcSpawnTile[new System.Random().Next(this.ec.npcSpawnTile.Length)].FloorWorldPosition));
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003630 File Offset: 0x00001830
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F5))
		{
			this.TurnedON = !this.TurnedON;
			this.unlocked = !this.unlocked;
			Cursor.visible = this.unlocked;
			if (this.unlocked)
			{
				Cursor.lockState = CursorLockMode.None;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}

	// Token: 0x04000095 RID: 149
	private EnvironmentController ec;

	// Token: 0x04000096 RID: 150
	private PlayerManager pm;

	// Token: 0x04000097 RID: 151
	private bool Initalzied = false;

	// Token: 0x04000098 RID: 152
	private bool TurnedON = false;

	// Token: 0x04000099 RID: 153
	public List<string> debugLogs;

	// Token: 0x0400009A RID: 154
	public static DebugMenu instance;

	// Token: 0x0400009B RID: 155
	public NPC[] npcs;

	// Token: 0x0400009C RID: 156
	public ItemObject[] items;

	// Token: 0x0400009D RID: 157
	public SceneObject[] scenes;

	// Token: 0x0400009E RID: 158
	public StructureWithParameters[] structures;

	// Token: 0x0400009F RID: 159
	private Vector2 wtf = Vector2.zero;

	// Token: 0x040000A0 RID: 160
	private Vector2 wtf2 = Vector2.zero;

	// Token: 0x040000A1 RID: 161
	private Vector2 wtf3 = Vector2.zero;

	// Token: 0x040000A2 RID: 162
	public static bool infItems;

	// Token: 0x040000A3 RID: 163
	private bool infItemsInternal = false;

	// Token: 0x040000A4 RID: 164
	private bool npcOnPlayer = true;

	// Token: 0x040000A5 RID: 165
	private bool unlocked = false;
}
