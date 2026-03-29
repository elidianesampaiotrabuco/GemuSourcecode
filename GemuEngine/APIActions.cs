using System;
using System.Collections.Generic;

// Token: 0x0200002A RID: 42
public class APIActions
{
	// Token: 0x0600007E RID: 126 RVA: 0x00004B58 File Offset: 0x00002D58
	public static void InvokeCustom(string name)
	{
		if (APIActions.customActions.ContainsKey(name))
		{
			APIActions.customActions[name]();
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00004B8C File Offset: 0x00002D8C
	public static void CreateCustom(string name, Action initAction = null)
	{
		if (!APIActions.customActions.ContainsKey(name))
		{
			APIActions.customActions.Add(name, initAction);
		}
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00004BB8 File Offset: 0x00002DB8
	public static void AddToCustom(string name, Action action)
	{
		if (APIActions.customActions.ContainsKey(name))
		{
			Dictionary<string, Action> dictionary;
			(dictionary = APIActions.customActions)[name] = (Action)Delegate.Combine(dictionary[name], action);
		}
	}

	// Token: 0x040000B5 RID: 181
	public static Dictionary<string, Action> customActions = new Dictionary<string, Action>();

	// Token: 0x040000B6 RID: 182
	public static Action onNotebookCollect;

	// Token: 0x040000B7 RID: 183
	public static Action onDetentionGet;

	// Token: 0x040000B8 RID: 184
	public static Action onGameStart;

	// Token: 0x040000B9 RID: 185
	public static Action onNpcSpawns;

	// Token: 0x040000BA RID: 186
	public static Action onItemUse;

	// Token: 0x040000BB RID: 187
	public static Action onItemCollect;
}
