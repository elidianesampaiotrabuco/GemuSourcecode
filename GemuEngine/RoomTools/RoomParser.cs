using System;
using System.Collections.Generic;
using System.IO;
using Gemu.JSON;
using MTM101BaldAPI.AssetTools;
using Newtonsoft.Json;
using UnityEngine;

namespace Gemu.RoomTools
{
	// Token: 0x02000003 RID: 3
	public class RoomParser
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static Transform getObjectFromType(string type, AssetManager plugin)
		{
			RoomObjects objlol = RoomObjects.Custom;
			switch (type)
			{
			case "BigDesk":
				objlol = RoomObjects.BigDesk;
				break;
			case "FilingCabinent":
				objlol = RoomObjects.FilingCabinent;
				break;
			case "SmallCabinent":
				objlol = RoomObjects.SmallCabinent;
				break;
			case "Locker":
				objlol = RoomObjects.Locker;
				break;
			case "Chair":
				objlol = RoomObjects.Chair;
				break;
			case "Banana":
				objlol = RoomObjects.Banana;
				break;
			case "Globe":
				objlol = RoomObjects.Globe;
				break;
			case "Lunch":
				objlol = RoomObjects.Lunch;
				break;
			case "TapePlayer":
				objlol = RoomObjects.TapePlayer;
				break;
			case "Fan":
				objlol = RoomObjects.CeilingFan;
				break;
			case "BSODA":
				objlol = RoomObjects.BSODA;
				break;
			case "Zesty":
				objlol = RoomObjects.Zesty;
				break;
			case "Crazy":
				objlol = RoomObjects.Crazy;
				break;
			}
			Transform result;
			if (objlol != RoomObjects.Custom)
			{
				result = RoomParser.jernk[(int)objlol];
			}
			else
			{
				result = plugin.Get<Transform>(type);
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000021D0 File Offset: 0x000003D0
		public static List<BasicObjectData> parseRoomData(string data, AssetManager plugin)
		{
			List<BasicObjectData> rofl = new List<BasicObjectData>();
			JsonLevelData dataParsed = JsonConvert.DeserializeObject<JsonLevelData>(data);
			JsonObjectsDatas junk = dataParsed.objects;
			foreach (JsonObjectData jsonData in junk.basicObjects)
			{
				BasicObjectData convertedData = new BasicObjectData();
				convertedData.position = new Vector3(jsonData.position[0], jsonData.position[1], jsonData.position[2]);
				convertedData.rotation = new Quaternion(jsonData.rotation[0], jsonData.rotation[1], jsonData.rotation[2], jsonData.rotation[3]);
				string lol = jsonData.type;
				convertedData.prefab = RoomParser.getObjectFromType(lol, plugin);
				rofl.Add(convertedData);
			}
			return rofl;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000022A0 File Offset: 0x000004A0
		public static List<CellData> parseRoomTiles(string data)
		{
			List<CellData> output = new List<CellData>();
			JsonTileData dataParsed = JsonConvert.DeserializeObject<JsonTileData>(data);
			foreach (JsonCell jsonData in dataParsed.cells)
			{
				output.Add(new CellData
				{
					pos = new IntVector2(jsonData.position[0], jsonData.position[1]),
					type = jsonData.type,
					roomId = 0
				});
			}
			return output;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002328 File Offset: 0x00000528
		public static List<PosterData> parsePosterData(string data, AssetManager plugin)
		{
			List<PosterData> output = new List<PosterData>();
			JsonPosterFile dataParsed = JsonConvert.DeserializeObject<JsonPosterFile>(data);
			foreach (JsonPosterData jsonData in dataParsed.posters)
			{
				output.Add(new PosterData
				{
					position = new IntVector2(jsonData.position[0], jsonData.position[1]),
					direction = jsonData.direction,
					poster = plugin.Get<PosterObject>(jsonData.name)
				});
			}
			return output;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023BC File Offset: 0x000005BC
		public static List<IntVector2> convertVectors(JsonVector[] array)
		{
			List<IntVector2> result = new List<IntVector2>();
			foreach (JsonVector vector in array)
			{
				result.Add(new IntVector2(vector.x, vector.z));
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002410 File Offset: 0x00000610
		public static RoomAsset CreateRoomAsset(string path, string tileName, string objectName, string metaName, Transform lightPre, RoomCategory category, Texture2D[] textures, StandardDoorMats doorTex, AssetManager plugin)
		{
			RoomAsset finalRoom = ScriptableObject.CreateInstance<RoomAsset>();
			finalRoom.cells = RoomParser.parseRoomTiles(File.ReadAllText(path + "/" + tileName + ".json"));
			finalRoom.basicObjects = RoomParser.parseRoomData(File.ReadAllText(path + "/" + objectName + ".json"), plugin);
			string metadata = File.ReadAllText(path + "/" + metaName + ".json");
			JsonRoomMetadata dataParsed = JsonConvert.DeserializeObject<JsonRoomMetadata>(metadata);
			finalRoom.hasActivity = dataParsed.hasActivity;
			finalRoom.activity = new ActivityData();
			finalRoom.potentialDoorPositions = RoomParser.convertVectors(dataParsed.doorPositions);
			finalRoom.entitySafeCells = RoomParser.convertVectors(dataParsed.entityPositions);
			finalRoom.eventSafeCells = RoomParser.convertVectors(dataParsed.eventPositions);
			finalRoom.color = new Color(dataParsed.color[0] / 255f, dataParsed.color[1] / 255f, dataParsed.color[2] / 255f);
			finalRoom.keepTextures = dataParsed.keepTextures;
			finalRoom.posterChance = dataParsed.posterChance;
			finalRoom.lightPre = lightPre;
			finalRoom.standardLightCells = RoomParser.convertVectors(dataParsed.lightPositions);
			finalRoom.category = category;
			finalRoom.type = 2;
			finalRoom.florTex = textures[0];
			finalRoom.wallTex = textures[1];
			finalRoom.ceilTex = textures[2];
			finalRoom.doorMats = doorTex;
			return finalRoom;
		}

		// Token: 0x04000010 RID: 16
		public static Transform[] jernk = new Transform[]
		{
			ObjectFunctions.FindResourceOfName<Transform>("BigDesk", null),
			ObjectFunctions.FindResourceOfName<Transform>("FilingCabinet_Tall", null),
			ObjectFunctions.FindResourceOfName<Transform>("FilingCabinet_Short", null),
			ObjectFunctions.FindResourceOfName<Transform>("Locker", null),
			ObjectFunctions.FindResourceOfName<Transform>("Chair_Test", null),
			ObjectFunctions.FindResourceOfName<Transform>("Decor_Banana", null),
			ObjectFunctions.FindResourceOfName<Transform>("Decor_Globe", null),
			ObjectFunctions.FindResourceOfName<Transform>("Decor_Lunch", null),
			ObjectFunctions.FindResourceOfName<Transform>("TapePlayer", null),
			ObjectFunctions.FindResourceOfName<Transform>("CeilingFan", null),
			ObjectFunctions.FindResourceOfName<Transform>("SodaMachine", null),
			ObjectFunctions.FindResourceOfName<Transform>("ZestyMachine", null),
			ObjectFunctions.FindResourceOfName<Transform>("CrazyVendingMachineBSODA", null)
		};
	}
}
