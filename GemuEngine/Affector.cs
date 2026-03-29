using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class Affector : MonoBehaviour
{
	// Token: 0x06000042 RID: 66 RVA: 0x00003716 File Offset: 0x00001916
	public void Enable(bool val)
	{
		this.EnableStat = val;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003720 File Offset: 0x00001920
	public void Update()
	{
		if (this.EnableStat && this.affectedEntity != null)
		{
			this.UpdateStat();
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003756 File Offset: 0x00001956
	public virtual void UpdateStat()
	{
	}

	// Token: 0x040000A6 RID: 166
	public Entity affectedEntity;

	// Token: 0x040000A7 RID: 167
	public bool EnableStat = false;
}
