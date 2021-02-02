using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class Exhibition6 : ExhibitionItem
{
	// Token: 0x06000023 RID: 35 RVA: 0x0000260F File Offset: 0x0000080F
	private void Start()
	{
		if (this._rectTransform == null)
		{
			this._rectTransform = base.GetComponent<RectTransform>();
			this._origerVector2 = this._rectTransform.anchoredPosition;
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000022A4 File Offset: 0x000004A4
	protected override void PointArrive(int obj, bool isRight)
	{
		base.gameObject.SetActive(obj == this.ID);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x0000263C File Offset: 0x0000083C
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
			float num;
			if (obj == "down")
			{
				num = this._rectTransform.anchoredPosition.y - 1000f;
				if (num <= 0f)
				{
					num = 0f;
				}
				this._rectTransform.DOAnchorPosY(num, 1f, false);
				return;
			}
			if (!(obj == "up"))
			{
				return;
			}
			num = this._rectTransform.anchoredPosition.y + 1000f;
			if (num >= 3840f)
			{
				num = 3840f;
			}
			this._rectTransform.DOAnchorPosY(num, 1f, false);
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x0000275D File Offset: 0x0000095D
	private void OnEnable()
	{
		if (this._rectTransform == null)
		{
			this._rectTransform = base.GetComponent<RectTransform>();
			this._origerVector2 = this._rectTransform.anchoredPosition;
		}
		this._rectTransform.anchoredPosition = this._origerVector2;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002050 File Offset: 0x00000250
	private void Update()
	{
	}

	// Token: 0x04000009 RID: 9
	private RectTransform _rectTransform;

	// Token: 0x0400000A RID: 10
	private Vector2 _origerVector2;
}
