using System;

namespace Gemu.JSON
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public class JsonRoomMetadata
	{
		// Token: 0x04000058 RID: 88
		public bool hasActivity;

		// Token: 0x04000059 RID: 89
		public bool keepTextures;

		// Token: 0x0400005A RID: 90
		public JsonVector[] doorPositions;

		// Token: 0x0400005B RID: 91
		public JsonVector[] entityPositions;

		// Token: 0x0400005C RID: 92
		public JsonVector[] eventPositions;

		// Token: 0x0400005D RID: 93
		public JsonVector[] lightPositions;

		// Token: 0x0400005E RID: 94
		public float[] color;

		// Token: 0x0400005F RID: 95
		public float posterChance;
	}
}
