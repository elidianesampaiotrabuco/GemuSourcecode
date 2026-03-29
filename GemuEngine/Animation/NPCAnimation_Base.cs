using System;
using UnityEngine;

namespace Gemu.Animation
{
	// Token: 0x0200002C RID: 44
	public class NPCAnimation_Base
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00004C95 File Offset: 0x00002E95
		public NPCAnimation_Base(string name, CustomNPC myNPC, float fps, int frames)
		{
			this.rootName = name;
			this.myNPC = myNPC;
			this.fps = fps;
			this.frameCount = frames;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004CCF File Offset: 0x00002ECF
		public virtual void StartAnim()
		{
			this.time = 0f;
			this.currentFrame = 0;
			this.isPlaying = true;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004CEB File Offset: 0x00002EEB
		public virtual void Update()
		{
			this.time += Time.deltaTime * this.fps;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004D07 File Offset: 0x00002F07
		public virtual void EndAnim()
		{
			this.isPlaying = false;
		}

		// Token: 0x040000BE RID: 190
		public string rootName;

		// Token: 0x040000BF RID: 191
		public CustomNPC myNPC;

		// Token: 0x040000C0 RID: 192
		protected float fps;

		// Token: 0x040000C1 RID: 193
		protected int frameCount;

		// Token: 0x040000C2 RID: 194
		public bool isPlaying;

		// Token: 0x040000C3 RID: 195
		protected float time = 0f;

		// Token: 0x040000C4 RID: 196
		protected int currentFrame = 0;
	}
}
