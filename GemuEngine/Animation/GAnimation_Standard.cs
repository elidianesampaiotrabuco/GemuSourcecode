using System;
using UnityEngine;

namespace Gemu.Animation
{
	// Token: 0x0200002D RID: 45
	public class GAnimation_Standard : NPCAnimation_Base
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00004D11 File Offset: 0x00002F11
		public GAnimation_Standard(string name, CustomNPC myNPC, float fps, int frames) : base(name, myNPC, fps, frames)
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Update()
		{
			base.Update();
			while (this.time < (float)this.frameCount)
			{
				this.myNPC.UpdateSprite(string.Format("{0}{1}", this.rootName, this.currentFrame));
				this.currentFrame = Mathf.FloorToInt(Mathf.Min(this.time, (float)this.frameCount - 1f));
			}
		}
	}
}
