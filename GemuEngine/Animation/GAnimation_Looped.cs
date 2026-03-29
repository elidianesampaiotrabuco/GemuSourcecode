using System;
using UnityEngine;

namespace Gemu.Animation
{
	// Token: 0x0200002E RID: 46
	public class GAnimation_Looped : NPCAnimation_Base
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00004D9A File Offset: 0x00002F9A
		public GAnimation_Looped(string name, CustomNPC myNPC, float fps, int frames) : base(name, myNPC, fps, frames)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004DAC File Offset: 0x00002FAC
		public override void Update()
		{
			base.Update();
			this.myNPC.UpdateSprite(string.Format("{0}{1}", this.rootName, this.currentFrame));
			this.currentFrame = Mathf.FloorToInt(this.time) % this.frameCount;
		}
	}
}
