using System;

// Token: 0x02000007 RID: 7
public class Exhibition5 : ExhibitionItem
{
	// Token: 0x0600001E RID: 30 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000022A4 File Offset: 0x000004A4
	protected override void PointArrive(int obj, bool isRight)
	{
		base.gameObject.SetActive(obj == this.ID);
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002050 File Offset: 0x00000250
	protected override void Message(string obj)
	{
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002050 File Offset: 0x00000250
	private void Update()
	{
	}
}
