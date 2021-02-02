using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExhibitionItem : MonoBehaviour
{
	// Token: 0x06000029 RID: 41 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
	}

	// Token: 0x0600002A RID: 42 RVA: 0x0000279B File Offset: 0x0000099B
	public void PointaArriveEvent(int obj, bool isRight)
	{
		this.PointArrive(obj, isRight);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000027A5 File Offset: 0x000009A5
	public void KeyCodeEvent(string obj)
	{
		this.Message(obj);
	}

	// Token: 0x0600002C RID: 44
	protected abstract void PointArrive(int obj, bool isRight);

	// Token: 0x0600002D RID: 45
	protected abstract void Message(string obj);

	// Token: 0x0400000B RID: 11
	public int ID;
}
