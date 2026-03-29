using System;

namespace Gemu.JSON
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class JsonLevelContents
	{
		// Token: 0x04000070 RID: 112
		public JsonVector3 spawnPoint;

		// Token: 0x04000071 RID: 113
		public int spawnDirection;

		// Token: 0x04000072 RID: 114
		public JsonVector levelSize;

		// Token: 0x04000073 RID: 115
		public JsonCell[] tile;
	}
}
