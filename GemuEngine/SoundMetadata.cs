using System;
using UnityEngine;

namespace Gemu
{
	// Token: 0x02000021 RID: 33
	public class SoundMetadata
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003769 File Offset: 0x00001969
		public SoundMetadata(SoundType type, Color subcolor)
		{
			this.type = type;
			this.subcolor = subcolor;
		}

		// Token: 0x040000A8 RID: 168
		public SoundType type;

		// Token: 0x040000A9 RID: 169
		public Color subcolor;
	}
}
