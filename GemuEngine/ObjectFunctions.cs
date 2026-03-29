using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using Gemu.Animation;
using Gemu.JSON;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI.ObjectCreation;
using MTM101BaldAPI.Registers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gemu
{
	// Token: 0x02000023 RID: 35
	public class ObjectFunctions
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00003818 File Offset: 0x00001A18
		public static Sprite CreateSprite(string name, string path, float offset, float ppu)
		{
			Texture2D tex = AssetLoader.TextureFromFile(path + "/" + name + ".png");
			Sprite spr = Sprite.Create(tex, new Rect(0f, 0f, (float)tex.width, (float)tex.height), new Vector2(0.5f, offset), ppu);
			spr.name = tex.name;
			return spr;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003880 File Offset: 0x00001A80
		public static Sprite CreateSprite(string name, string path)
		{
			Texture2D tex = AssetLoader.TextureFromFile(path + "/" + name + ".png");
			Sprite spr = Sprite.Create(tex, new Rect(0f, 0f, (float)tex.width, (float)tex.height), new Vector2(0.5f, 0.5f), 32f);
			spr.name = tex.name;
			return spr;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000038F0 File Offset: 0x00001AF0
		public static SoundObject[] CreateSoundArray(string name, string path, int length, SoundMetadata metadata, string Subtitle, string format = "wav", bool captioned = true)
		{
			string pather = path + "/" + name;
			SoundObject[] output = new SoundObject[length];
			for (int i = 1; i < length + 1; i++)
			{
				int index = i - 1;
				output[index] = ObjectFunctions.CreateSound(name + i, path, metadata, Subtitle + i, format, captioned);
			}
			return output;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000395C File Offset: 0x00001B5C
		public static SoundObject CreateSound(string name, string path, SoundMetadata metadata, string Subtitle, string format = "wav", bool useCaption = true)
		{
			string pather = path + "/" + name;
			AudioClip audioasset = AssetLoader.AudioClipFromFile(pather + "." + format);
			SoundObject fuckMe = ObjectCreators.CreateSoundObject(audioasset, Subtitle, metadata.type, metadata.subcolor, -1f);
			fuckMe.subtitle = useCaption;
			fuckMe.name = name;
			return fuckMe;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000039B8 File Offset: 0x00001BB8
		public static Fog createFog(Color color, float strength, float maxDist, int priortiy, float startDist)
		{
			return new Fog
			{
				color = color,
				strength = strength,
				maxDist = maxDist,
				startDist = startDist,
				priority = priortiy
			};
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000039F8 File Offset: 0x00001BF8
		public static Sprite[] CreateSpriteArray(string name, float units, float offset, int count, string path)
		{
			List<Sprite> listofsprites = new List<Sprite>();
			for (int num = 0; num < count; num++)
			{
				listofsprites.Add(ObjectFunctions.CreateSprite(name + num, path, offset, units));
			}
			return listofsprites.ToArray();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003A44 File Offset: 0x00001C44
		public static T FindResourceOfName<T>(string name, AssetManager plugin = null) where T : UnityEngine.Object
		{
			T[] stuffs = Resources.FindObjectsOfTypeAll<T>();
			foreach (T thingy in stuffs)
			{
				if (thingy.name == name)
				{
					return thingy;
				}
			}
			if (plugin != null)
			{
				try
				{
					return plugin.Get<T>(name);
				}
				catch
				{
					throw new NotImplementedException("YOU DONT HAVE THAT YET");
				}
			}
			return default(T);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003AE4 File Offset: 0x00001CE4
		[Obsolete("USE ENTITYBUILDER THIS IS OBSOLETE AS FUCK MAN", false)]
		public static Entity CreateEntity(GameObject entityObject, float height, bool triggerBool, Collider collider, Collider trigger, ActivityModifier actMod, Transform renderBase)
		{
			Entity entity = entityObject.AddComponent<Entity>();
			entity.SetHeight(height);
			entity.SetTrigger(triggerBool);
			AccessTools.Field(typeof(Entity), "collider").SetValue(entity, collider);
			AccessTools.Field(typeof(Entity), "trigger").SetValue(entity, trigger);
			AccessTools.Field(typeof(Entity), "externalActivity").SetValue(entity, actMod);
			AccessTools.Field(typeof(Entity), "rendererBase").SetValue(entity, renderBase);
			return entity;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003B84 File Offset: 0x00001D84
		public static Entity CreateEntity(string jsonFile)
		{
			JsonEntity dataParsed = JsonConvert.DeserializeObject<JsonEntity>(File.ReadAllText(jsonFile));
			EntityBuilder poopoo = new EntityBuilder().SetName(dataParsed.name).SetBaseRadius(dataParsed.colliderRadius).AddTrigger(dataParsed.triggerRadius);
			Entity entity = poopoo.Build();
			entity.SetTrigger(dataParsed.isTrigger);
			entity.SetHeight(dataParsed.height);
			return entity;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003BEC File Offset: 0x00001DEC
		public static void SwapPositions(Transform a, Transform b)
		{
			Vector3 origPosition = a.position;
			a.position = b.position;
			b.position = origPosition;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003C18 File Offset: 0x00001E18
		public static NPC CreateBasicNPC<T>(string filePath, PluginInfo info, ModContents mod, NPCFlags flag, RoomCategory[] categorys, params WeightedRoomAsset[] roomAssets) where T : NPC
		{
			JsonNPC dataParsed = JsonConvert.DeserializeObject<JsonNPC>(File.ReadAllText(filePath));
			NPCBuilder<T> builder = new NPCBuilder<T>(info).SetName(dataParsed.name).SetEnum(dataParsed.character).SetMinMaxAudioDistance(10f, 250f).AddSpawnableRoomCategories(categorys).AddMetaFlag(flag).SetPoster(mod.CreateNPCPoster(dataParsed.poster, dataParsed.posterName, dataParsed.posterDesc, mod.imagePath)).IgnorePlayerOnSpawn();
			if (dataParsed.airborne)
			{
				builder = builder.SetAirborne();
			}
			if (dataParsed.hasLooker)
			{
				builder = builder.AddLooker();
			}
			if (dataParsed.hasTrigger)
			{
				builder = builder.AddTrigger();
			}
			if (dataParsed.stationary)
			{
				builder = builder.SetStationary();
			}
			if (roomAssets.Length > 0)
			{
				builder = builder.AddPotentialRoomAssets(roomAssets);
			}
			return builder.Build();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003D14 File Offset: 0x00001F14
		public static CustomNPC CreateCustomNPC<T>(string filePath, PluginInfo info, ModContents mod, NPCFlags flag, RoomCategory[] categorys, Action<NPCBuilder<T>> buildAddend, params WeightedRoomAsset[] roomAssets) where T : CustomNPC
		{
			JsonNPC dataParsed = JsonConvert.DeserializeObject<JsonNPC>(File.ReadAllText(filePath));
			NPCBuilder<T> builder = new NPCBuilder<T>(info).SetName(dataParsed.name).SetEnum(dataParsed.character).SetMinMaxAudioDistance(10f, 250f).AddSpawnableRoomCategories(categorys).AddMetaFlag(flag).SetPoster(mod.CreateNPCPoster(dataParsed.poster, dataParsed.posterName, dataParsed.posterDesc, mod.imagePath)).IgnorePlayerOnSpawn();
			if (dataParsed.airborne)
			{
				builder = builder.SetAirborne();
			}
			if (dataParsed.hasLooker)
			{
				builder = builder.AddLooker();
			}
			if (dataParsed.hasTrigger)
			{
				builder = builder.AddTrigger();
			}
			if (dataParsed.stationary)
			{
				builder = builder.SetStationary();
			}
			if (roomAssets.Length > 0)
			{
				builder = builder.AddPotentialRoomAssets(roomAssets);
			}
			buildAddend(builder);
			CustomNPC t = builder.Build();
			SpriteBank bank = new SpriteBank(dataParsed.name);
			Debug.Log("Created bank " + bank.bankName + " now do the funnies");
			if (dataParsed.sprites.Length > 0)
			{
				foreach (JsonSprite sprite in dataParsed.sprites)
				{
					Debug.Log("Registering " + sprite.name + " " + sprite.filename);
					bank.AddSprite(sprite.name, ObjectFunctions.CreateSprite(sprite.filename, mod.imagePath, sprite.center, sprite.pixelsPerUnit));
				}
			}
			SpriteBanks.banks.Add(dataParsed.name, bank);
			SoundBank bank2 = new SoundBank(dataParsed.name);
			Debug.Log("Created sound bank " + bank2.bankName + " now do the funnies");
			if (dataParsed.sounds.Length > 0)
			{
				foreach (JsonAudio audio in dataParsed.sounds)
				{
					Debug.Log("Registering " + audio.name + " " + audio.filename);
					bank2.AddSound(audio.name, ObjectFunctions.CreateSound(audio.filename, mod.audioPath, new SoundMetadata(0, new Color(audio.color[0] / 255f, audio.color[1] / 255f, audio.color[2] / 255f)), audio.caption, audio.format, true));
				}
			}
			SoundBanks.banks.Add(dataParsed.name, bank2);
			t.npcName = dataParsed.name;
			t.UpdateSprite("idle");
			t.gameObject.AddComponent<GemuAnimator>();
			return t;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004028 File Offset: 0x00002228
		public static CustomNPC CreateCustomNPC<T>(string filePath, PluginInfo info, ModContents mod, NPCFlags flag, RoomCategory[] categorys, params WeightedRoomAsset[] roomAssets) where T : CustomNPC
		{
			JsonNPC dataParsed = JsonConvert.DeserializeObject<JsonNPC>(File.ReadAllText(filePath));
			NPCBuilder<T> builder = new NPCBuilder<T>(info).SetName(dataParsed.name).SetEnum(dataParsed.character).SetMinMaxAudioDistance(10f, 250f).AddSpawnableRoomCategories(categorys).AddMetaFlag(flag).SetPoster(mod.CreateNPCPoster(dataParsed.poster, dataParsed.posterName, dataParsed.posterDesc, mod.imagePath)).IgnorePlayerOnSpawn();
			if (dataParsed.airborne)
			{
				builder = builder.SetAirborne();
			}
			if (dataParsed.hasLooker)
			{
				builder = builder.AddLooker();
			}
			if (dataParsed.hasTrigger)
			{
				builder = builder.AddTrigger();
			}
			if (dataParsed.stationary)
			{
				builder = builder.SetStationary();
			}
			if (roomAssets.Length > 0)
			{
				builder = builder.AddPotentialRoomAssets(roomAssets);
			}
			CustomNPC t = builder.Build();
			SpriteBank bank = new SpriteBank(dataParsed.name);
			Debug.Log("Created bank " + bank.bankName + " now do the funnies");
			if (dataParsed.sprites.Length > 0)
			{
				foreach (JsonSprite sprite in dataParsed.sprites)
				{
					Debug.Log("Registering " + sprite.name + " " + sprite.filename);
					bank.AddSprite(sprite.name, ObjectFunctions.CreateSprite(sprite.filename, mod.imagePath, sprite.center, sprite.pixelsPerUnit));
				}
			}
			SpriteBanks.banks.Add(dataParsed.name, bank);
			SoundBank bank2 = new SoundBank(dataParsed.name);
			Debug.Log("Created sound bank " + bank2.bankName + " now do the funnies");
			if (dataParsed.sounds.Length > 0)
			{
				foreach (JsonAudio audio in dataParsed.sounds)
				{
					Debug.Log("Registering " + audio.name + " " + audio.filename);
					bank2.AddSound(audio.name, ObjectFunctions.CreateSound(audio.filename, mod.audioPath, new SoundMetadata(0, new Color(audio.color[0] / 255f, audio.color[1] / 255f, audio.color[2] / 255f)), audio.caption, audio.format, true));
				}
			}
			SoundBanks.banks.Add(dataParsed.name, bank2);
			t.npcName = dataParsed.name;
			t.UpdateSprite("idle");
			t.gameObject.AddComponent<GemuAnimator>();
			return t;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004334 File Offset: 0x00002534
		public static StructureParameters CreateParameters(WeightedGameObject[] prefabs, float[] chances, IntVector2[] ranges)
		{
			return new StructureParameters
			{
				prefab = prefabs,
				chance = chances,
				minMax = ranges
			};
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004364 File Offset: 0x00002564
		public static StructureWithParameters CreateStructureParameter(StructureBuilder builder, StructureParameters parameters)
		{
			return new StructureWithParameters
			{
				prefab = builder,
				parameters = parameters
			};
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000438C File Offset: 0x0000258C
		public static T[] AddToArray<T>(List<T> gaye, T[] og)
		{
			List<T> news = new List<T>();
			for (int i = 0; i < og.Length; i++)
			{
				news.Add(og[i]);
			}
			for (int j = 0; j < gaye.Count; j++)
			{
				news.Add(gaye[j]);
			}
			return news.ToArray();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000043F8 File Offset: 0x000025F8
		public static Transform CreateBasicMesh(PrimitiveType mesh, Vector3 scale, Material material, Vector3 collisionScale)
		{
			Transform cube = GameObject.CreatePrimitive(mesh).transform;
			cube.localScale = scale;
			cube.GetComponent<MeshRenderer>().material = material;
			cube.GetComponent<BoxCollider>().size = collisionScale;
			return cube;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000443C File Offset: 0x0000263C
		public static Sprite[] GenerateAtlas(string name, string path, int sizeX, int sizeZ, float height, float ppu)
		{
			string filePath = path + name + ".png";
			List<Sprite> outputFile = new List<Sprite>();
			Texture2D spriteAtlas = AssetLoader.TextureFromFile(filePath);
			float scaleX = (float)(spriteAtlas.height / sizeX);
			float scaleZ = (float)(spriteAtlas.width / sizeZ);
			for (int i = 0; i < sizeX; i++)
			{
				for (int j = 0; j < sizeZ; j++)
				{
					Rect eRECTion = new Rect((float)j * scaleZ, (float)i * scaleX, scaleZ, scaleX);
					Sprite newSprite = Sprite.Create(spriteAtlas, eRECTion, new Vector2(0.5f, height), ppu);
					outputFile.Add(newSprite);
				}
			}
			return outputFile.ToArray();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000044EC File Offset: 0x000026EC
		public static SpriteRenderer CreateSpriteRender(string name, bool billboarded, Transform baseObject)
		{
			GameObject srobject = new GameObject(name);
			srobject.transform.parent = baseObject;
			srobject.transform.position = baseObject.position;
			srobject.AddComponent<SpriteRenderer>();
			SpriteRenderer rend = srobject.GetComponent<SpriteRenderer>();
			if (billboarded)
			{
				rend.material = ObjectFunctions.FindResourceOfName<Material>("SpriteStandard_Billboard", null);
			}
			else
			{
				rend.material = ObjectFunctions.FindResourceOfName<Material>("SpriteWithFog_Forward_NoBillboard", null);
			}
			return rend;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004568 File Offset: 0x00002768
		public static GameObject findObjectInScene(string name)
		{
			GameObject[] objectz = SceneManager.GetActiveScene().GetRootGameObjects();
			foreach (GameObject x in objectz)
			{
				if (x.name == name)
				{
					return x;
				}
			}
			return null;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000045CC File Offset: 0x000027CC
		public static GameObject CreateFlooredSprite(string name, string spriteFile, float center, float ppu, bool prefab, ModContents mod)
		{
			GameObject objectLol = new GameObject(name);
			SpriteRenderer mainObject = ObjectFunctions.CreateSpriteRender(name + "_Sprite", false, objectLol.transform);
			mainObject.sprite = ObjectFunctions.CreateSprite(spriteFile, mod.imagePath, center, ppu);
			if (prefab)
			{
				objectLol.ConvertToPrefab(true);
			}
			return objectLol;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004628 File Offset: 0x00002828
		public static RoomFunction CreateRoomFunction<T>(string name, bool post) where T : RoomFunction
		{
			GameObject XD = new GameObject(name);
			RoomFunction LMAO = XD.AddComponent<T>();
			if (!post)
			{
				XD.ConvertToPrefab(true);
			}
			return LMAO;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004660 File Offset: 0x00002860
		public static RoomFunctionContainer CreateRoomFunctionContainer(string name, bool post, params RoomFunction[] functions)
		{
			GameObject XD2 = new GameObject(name);
			RoomFunctionContainer container = XD2.AddComponent<RoomFunctionContainer>();
			foreach (RoomFunction function in functions)
			{
				container.AddFunction(function);
			}
			if (!post)
			{
				XD2.ConvertToPrefab(true);
			}
			return container;
		}
	}
}
