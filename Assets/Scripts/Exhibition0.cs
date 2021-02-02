using System;

// Token: 0x02000002 RID: 2
public class Exhibition0 : ExhibitionItem
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002054 File Offset: 0x00000254
	protected override void PointArrive(int obj, bool isRight)
	{
		
		if (isRight)
		{
			
			if (obj == this.ID + 1 || obj == this.ID)
			{
				base.gameObject.SetActive(true);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			base.gameObject.SetActive(obj == this.ID);
		}
		this._isStay = (obj == this.ID);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
	protected override void Message(string obj)
	{
		
		 if (!this._isStay)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002050 File Offset: 0x00000250
	private void Update()
	{
	}

	// Token: 0x04000001 RID: 1
	private bool _isStay;
}
