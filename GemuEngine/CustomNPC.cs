using System;
using System.Collections;
using Gemu;
using Gemu.Animation;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class CustomNPC : NPC
{
	// Token: 0x06000023 RID: 35 RVA: 0x00002850 File Offset: 0x00000A50
	public override void Initialize()
	{
		base.Initialize();
		this.bank = SpriteBanks.getBank(this.npcName);
		this.audioBank = SoundBanks.getBank(this.npcName);
		this.animator = base.GetComponent<GemuAnimator>();
		this.spriteBase = base.transform.Find("SpriteBase").gameObject;
		this.spriteRenderer = new SpriteRenderer[]
		{
			this.spriteBase.transform.GetChild(0).GetComponent<SpriteRenderer>()
		};
		this.audMan = base.GetComponent<PropagatedAudioManager>();
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000028E4 File Offset: 0x00000AE4
	public void UpdateSprite(string name)
	{
		this.bank = SpriteBanks.getBank(this.npcName);
		if (this.bank != null)
		{
			if (this.spriteRenderer.Length > 0)
			{
				if (this.bank.allSprites.ContainsKey(name))
				{
					this.spriteRenderer[0].sprite = this.bank.GetSprite(name);
				}
			}
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x0000295A File Offset: 0x00000B5A
	[Obsolete("Legacy function, use GemuAnimator Instead", false)]
	public void ForceStopAnimation()
	{
		base.StopCoroutine("playLoopedAnim");
	}

	// Token: 0x06000026 RID: 38 RVA: 0x0000296C File Offset: 0x00000B6C
	[Obsolete("Legacy function, use GemuAnimator Instead", false)]
	public void PlayAnimation(string name, float fps, int length, Action onFinish)
	{
		base.StopCoroutine("playLoopedAnim");
		if (this.loopAnim)
		{
			base.StartCoroutine(this.playLoopedAnim(name, fps, length));
		}
		else
		{
			base.StartCoroutine(this.playAnimInternal(name, fps, length, onFinish));
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000029B8 File Offset: 0x00000BB8
	public void PlayAudio(string name)
	{
		if (this.audMan == null)
		{
			this.audMan = base.GetComponent<PropagatedAudioManager>();
		}
		if (this.audioBank.allSounds.ContainsKey(name))
		{
			this.audMan.PlaySingle(this.audioBank.GetSound(name));
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002A19 File Offset: 0x00000C19
	public void PlayRandomAudio(string name, int arraySize)
	{
		this.PlayAudio(name + UnityEngine.Random.Range(0, arraySize));
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002B44 File Offset: 0x00000D44
	private IEnumerator playAnimInternal(string n, float fps, int l, Action fin)
	{
		for (int i = 0; i < l; i++)
		{
			this.UpdateSprite(n + i);
			yield return new WaitForSeconds(1f / fps);
		}
		fin();
		yield break;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002CA0 File Offset: 0x00000EA0
	private IEnumerator playLoopedAnim(string n, float fps, int l)
	{
		int i = 0;
		while (this.loopAnim)
		{
			while (i < l)
			{
				this.UpdateSprite(n + i);
				yield return new WaitForSeconds(1f / fps);
				i++;
			}
			i = 0;
		}
		yield break;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002CD5 File Offset: 0x00000ED5
	[Obsolete("Legacy function, use GemuAnimator Instead", false)]
	public void ForceSprite(string name)
	{
		this.ForceStopAnimation();
		this.UpdateSprite(name);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002CE7 File Offset: 0x00000EE7
	public void StartAnimation(NPCAnimation_Base animation)
	{
		this.animator.ChangeAnimation(animation);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002CF8 File Offset: 0x00000EF8
	public void ResetAnimaton(string name)
	{
		if (this.animator.currentAnim != null)
		{
			this.animator.currentAnim.EndAnim();
		}
		this.UpdateSprite(name);
	}

	// Token: 0x0400008F RID: 143
	public SpriteBank bank;

	// Token: 0x04000090 RID: 144
	public SoundBank audioBank;

	// Token: 0x04000091 RID: 145
	public string npcName;

	// Token: 0x04000092 RID: 146
	public AudioManager audMan;

	// Token: 0x04000093 RID: 147
	public GemuAnimator animator;

	// Token: 0x04000094 RID: 148
	public bool loopAnim = false;
}
