﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Exhibition2 : ExhibitionItem
{
	// Token: 0x0600000B RID: 11 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000022A4 File Offset: 0x000004A4
	protected override void PointArrive(int obj, bool isRight)
	{
		base.gameObject.SetActive(obj == this.ID);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002050 File Offset: 0x00000250
	protected override void Message(string obj)
	{
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002050 File Offset: 0x00000250
	private void Update()
	{
	}
}
