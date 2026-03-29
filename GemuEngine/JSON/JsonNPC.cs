using System;

namespace Gemu.JSON
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class JsonNPC
	{
		// Token: 0x04000074 RID: 116
		public string name;

		// Token: 0x04000075 RID: 117
		public bool hasLooker;

		// Token: 0x04000076 RID: 118
		public bool hasTrigger;

		// Token: 0x04000077 RID: 119
		public bool airborne;

		// Token: 0x04000078 RID: 120
		public bool stationary;

		// Token: 0x04000079 RID: 121
		public string poster;

		// Token: 0x0400007A RID: 122
		public string posterName;

		// Token: 0x0400007B RID: 123
		public string posterDesc;

		// Token: 0x0400007C RID: 124
		public string character;

		// Token: 0x0400007D RID: 125
		public JsonSprite[] sprites;

		// Token: 0x0400007E RID: 126
		public JsonAudio[] sounds;
	}
}
