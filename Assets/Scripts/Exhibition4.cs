using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class Exhibition4 : ExhibitionItem
{
	// Token: 0x06000016 RID: 22 RVA: 0x00002439 File Offset: 0x00000639
	private void Start()
	{
		if (this._rectTransform == null)
		{
			this._rectTransform = base.GetComponent<RectTransform>();
			this._origerVector2 = this._rectTransform.anchoredPosition;
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000022A4 File Offset: 0x000004A4
	protected override void PointArrive(int obj, bool isRight)
	{
		base.gameObject.SetActive(obj == this.ID);
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002468 File Offset: 0x00000668
	protected override void Message(string obj)
	{
		if (this._rectTransform == null)
		{
			this._rectTransform = base.GetComponent<RectTransform>();
			this._origerVector2 = this._rectTransform.anchoredPosition;
		}
		if (this._rectTransform == null)
		{
			throw new UnityException("没有找到RectTransform组件");
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (!(obj == "play") && !(obj == "left") && !(obj == "right"))
		{
			if (!(obj == "down"))
			{
				if (!(obj == "up"))
				{
					return;
				}
				float num = this._rectTransform.anchoredPosition.y + 3840f;
				if (num >= 19200f)
				{
					num = 19200f;
				}
				if (this._tween == null)
				{
					this._tween = this._rectTransform.DOAnchorPosY(num, 1f, false).OnComplete(delegate
					{
						this._tween = null;
					});
				}
			}
			else
			{
				float num = this._rectTransform.anchoredPosition.y - 3840f;
				if (num <= 0f)
				{
					num = 0f;
				}
				if (this._tween == null)
				{
					this._tween = this._rectTransform.DOAnchorPosY(num, 1f, false).OnComplete(delegate
					{
						this._tween = null;
					});
					return;
				}
			}
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000025C8 File Offset: 0x000007C8
	private void OnEnable()
	{
		if (this._rectTransform == null)
		{
			this._rectTransform = base.GetComponent<RectTransform>();
			this._origerVector2 = this._rectTransform.anchoredPosition;
		}
		this._rectTransform.anchoredPosition = this._origerVector2;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002050 File Offset: 0x00000250
	private void Update()
	{
	}

	// Token: 0x04000006 RID: 6
	private RectTransform _rectTransform;

	// Token: 0x04000007 RID: 7
	private Vector2 _origerVector2;

	// Token: 0x04000008 RID: 8
	private Tween _tween;
}
