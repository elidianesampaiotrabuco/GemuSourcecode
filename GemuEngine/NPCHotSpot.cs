using System;
using UnityEngine;

namespace Gemu
{
	// Token: 0x02000028 RID: 40
	public class NPCHotSpot : MonoBehaviour, IClickable<int>
	{
		// Token: 0x0600006C RID: 108 RVA: 0x0000489A File Offset: 0x00002A9A
		public void Clicked(int playerNumber)
		{
			this.action(playerNumber);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000048AA File Offset: 0x00002AAA
		public void ClickableSighted(int player)
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000048AD File Offset: 0x00002AAD
		public void ClickableUnsighted(int player)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000048B0 File Offset: 0x00002AB0
		public bool ClickableHidden()
		{
			return false;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000048C4 File Offset: 0x00002AC4
		public bool ClickableRequiresNormalHeight()
		{
			return true;
		}

		// Token: 0x040000B4 RID: 180
		public Action<int> action;
	}
}
