using System;
using UnityEngine;

namespace Gemu.Animation
{
	// Token: 0x0200002B RID: 43
	public class GemuAnimator : MonoBehaviour
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00004C10 File Offset: 0x00002E10
		public void ChangeAnimation(NPCAnimation_Base newAnim)
		{
			if (this.currentAnim != null)
			{
				this.currentAnim.EndAnim();
			}
			this.currentAnim = newAnim;
			this.currentAnim.StartAnim();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004C4C File Offset: 0x00002E4C
		private void Update()
		{
			if (this.currentAnim != null)
			{
				if (this.currentAnim.isPlaying)
				{
					this.currentAnim.Update();
				}
			}
		}

		// Token: 0x040000BC RID: 188
		public NPCAnimation_Base currentAnim;

		// Token: 0x040000BD RID: 189
		public CustomNPC npc;
	}
}
