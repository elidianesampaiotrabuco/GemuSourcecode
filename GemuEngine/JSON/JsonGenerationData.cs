using System;

namespace Gemu.JSON
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class JsonGenerationData
	{
		// Token: 0x04000011 RID: 17
		public string levelName;

		// Token: 0x04000012 RID: 18
		public string levelID;

		// Token: 0x04000013 RID: 19
		public string baseLevel;

		// Token: 0x04000014 RID: 20
		public int[] mapSizeMin;

		// Token: 0x04000015 RID: 21
		public int[] mapSizeMax;

		// Token: 0x04000016 RID: 22
		public bool fieldTrips;

		// Token: 0x04000017 RID: 23
		public int mapCost;

		// Token: 0x04000018 RID: 24
		public int[] classRange;

		// Token: 0x04000019 RID: 25
		public int[] facultyRange;

		// Token: 0x0400001A RID: 26
		public int[] officeRange;

		// Token: 0x0400001B RID: 27
		public int[] extraRange;

		// Token: 0x0400001C RID: 28
		public int[] specialRange;

		// Token: 0x0400001D RID: 29
		public int[] plotRange;

		// Token: 0x0400001E RID: 30
		public int[] hallRemovalRange;

		// Token: 0x0400001F RID: 31
		public int[] hallReplacementRange;

		// Token: 0x04000020 RID: 32
		public int[] prePhRange;

		// Token: 0x04000021 RID: 33
		public int[] postPhRange;

		// Token: 0x04000022 RID: 34
		public int[] speciaBuilderRange;

		// Token: 0x04000023 RID: 35
		public int[] plotSizeMin;

		// Token: 0x04000024 RID: 36
		public int deadEndBuffer;

		// Token: 0x04000025 RID: 37
		public int edgeBuffer;

		// Token: 0x04000026 RID: 38
		public int hallBuffer;

		// Token: 0x04000027 RID: 39
		public int outerEdgeBuffer;

		// Token: 0x04000028 RID: 40
		public int lightDistance;

		// Token: 0x04000029 RID: 41
		public float posterChance;

		// Token: 0x0400002A RID: 42
		public float prePhChance;

		// Token: 0x0400002B RID: 43
		public float postPhChance;

		// Token: 0x0400002C RID: 44
		public float extraDoorChance;

		// Token: 0x0400002D RID: 45
		public float dijkstraMulti;

		// Token: 0x0400002E RID: 46
		public float dijkstraPower;

		// Token: 0x0400002F RID: 47
		public float doorReqMulti;

		// Token: 0x04000030 RID: 48
		public float hallDamp;

		// Token: 0x04000031 RID: 49
		public float perimeterBase;

		// Token: 0x04000032 RID: 50
		public float centerWeightMulti;

		// Token: 0x04000033 RID: 51
		public float initEventGap;

		// Token: 0x04000034 RID: 52
		public float classStick;

		// Token: 0x04000035 RID: 53
		public float facultyStick;

		// Token: 0x04000036 RID: 54
		public float extraStick;

		// Token: 0x04000037 RID: 55
		public float officeStick;

		// Token: 0x04000038 RID: 56
		public bool specialStick;

		// Token: 0x04000039 RID: 57
		public int npcCount;

		// Token: 0x0400003A RID: 58
		public JsonWeight[] baldis;

		// Token: 0x0400003B RID: 59
		public JsonWeight[] forcedNpcs;

		// Token: 0x0400003C RID: 60
		public JsonWeight[] npcs;

		// Token: 0x0400003D RID: 61
		public JsonWeight[] items;

		// Token: 0x0400003E RID: 62
		public int itemMaxVal;

		// Token: 0x0400003F RID: 63
		public int itemEntranceVal;

		// Token: 0x04000040 RID: 64
		public int itemNoHallVal;

		// Token: 0x04000041 RID: 65
		public int[] eventRange;

		// Token: 0x04000042 RID: 66
		public JsonWeight[] events;

		// Token: 0x04000043 RID: 67
		public int exitCount;

		// Token: 0x04000044 RID: 68
		public JsonWeight[] structures;

		// Token: 0x04000045 RID: 69
		public JsonWeight[] floorSkins;

		// Token: 0x04000046 RID: 70
		public JsonWeight[] wallSkins;

		// Token: 0x04000047 RID: 71
		public JsonWeight[] ceilingSkins;

		// Token: 0x04000048 RID: 72
		public string doorMaterial;
	}
}
