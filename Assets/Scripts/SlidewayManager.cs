using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x0200000F RID: 15
public class SlidewayManager : MonoBehaviour
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000058 RID: 88 RVA: 0x000039D8 File Offset: 0x00001BD8
	// (remove) Token: 0x06000059 RID: 89 RVA: 0x00003A10 File Offset: 0x00001C10
	public event Action<string> KeyCodeEvent;

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x0600005A RID: 90 RVA: 0x00003A48 File Offset: 0x00001C48
	// (remove) Token: 0x0600005B RID: 91 RVA: 0x00003A80 File Offset: 0x00001C80
	public event Action<int, bool> PointaArriveEvent;

	// Token: 0x0600005C RID: 92 RVA: 0x00003AB5 File Offset: 0x00001CB5
	private void Awake()
	{
		if (SlidewayManager.Instance != null)
		{
			throw new UnityException("有多个单例");
		}
		SlidewayManager.Instance = this;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00003AD8 File Offset: 0x00001CD8
	private void Start()
	{
		GlobalSettings.ReadXml();
		
		TCPUDPSocket.Instance.RecevieDataEvent += this.RecevieDataEvent;
		this.PortControl.ReceiveMsg += this.ReceiveMsg;
		foreach (ExhibitionItem @object in this.Contents)
		{
			this.PointaArriveEvent += @object.PointaArriveEvent;
			this.KeyCodeEvent += @object.KeyCodeEvent;
		}
		Screen.SetResolution(2160, 3840, true);
		this.PortControl.StarcConnect();
		this.PortLightControl.StarcConnect();
		this.LoadLoadingImage();
		this.ProcessData("7");
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00003BC8 File Offset: 0x00001DC8
	private void ReceiveMsg(string obj)
	{
		this._curDistance = this.HandleData(obj);
		if (this._curDistance < 0)
		{
			return;
		}
		Debug.LogError(string.Concat(new object[]
		{
			"当前位置信息为：",
			this._curDistance,
			"    当前需要到达的点位信息为：",
			this._curPointPos,
			"   当前点位为：",
			this._curIndex,
			"    行走方向为 ",
			this.isRight.ToString(),
			"\r\n串口发送过来的参数是：",
			obj
		}));
		if (Mathf.Abs(this._curDistance - this._curPointPos) <= this.Distance)
		{
			if (this._coroutine != null)
			{
				base.StopCoroutine(this._coroutine);
			}
			this._coroutine = null;
			this.ShowContent();
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00003CA0 File Offset: 0x00001EA0
	private int HandleData(string data)
	{
		int num = -1;
		if (string.IsNullOrEmpty(data))
		{
			return num;
		}
		try
		{
			num = int.Parse(data.Split(new string[]
			{
				"\n"
			}, StringSplitOptions.None)[0].Split(new string[]
			{
				","
			}, StringSplitOptions.None)[1], NumberStyles.AllowHexSpecifier);
			if (num - this._curDistance > 0)
			{
				this.isRight = true;
			}
			else
			{
				this.isRight = false;
			}
			this._curDistance = num;
			return num;
		}
		catch (Exception)
		{
		}
		return num;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00003D30 File Offset: 0x00001F30
	private void RecevieDataEvent(string obj)
	{
		if (GlobalSettings.IsShowDebug)
		{
			
		}
		this.ProcessData(obj);
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00003D88 File Offset: 0x00001F88
	private void LoadLoadingImage()
	{
		string path = Application.streamingAssetsPath + "/Standby.jpg";
		if (File.Exists(path))
		{
			byte[] data = File.ReadAllBytes(path);
			Texture2D texture2D = new Texture2D(4, 4);
			texture2D.LoadImage(data);
			texture2D.Apply();
			this.Contents[0].GetComponent<RawImage>().texture = texture2D;
			this.Contents[0].transform.Find("Text").gameObject.SetActive(false);
			this.Contents[0].gameObject.SetActive(true);
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00003E08 File Offset: 0x00002008
	private void ShowContent()
	{
		Debug.LogError("到达的点位是： " + this._curIndex);
		if (GlobalSettings.IsShowDebug)
		{
			
		}
		base.StartCoroutine(this.OpenLight(this._curIndex));
		if (this.PointaArriveEvent != null)
		{
			this.PointaArriveEvent(this._curIndex, this.isRight);
		}
		if (this._standByCoroutine != null)
		{
			base.StopCoroutine(this._standByCoroutine);
		}
		this._standByCoroutine = base.StartCoroutine(this.StandBy());
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00003ED4 File Offset: 0x000020D4
	private void ProcessData(string data)
	{
		string text = null;
		int num;
		if (int.TryParse(data, out num))
		{
			Debug.LogError("n is " + num);
			if (this._coroutine != null)
			{
				base.StopCoroutine(this._coroutine);
			}
			this._coroutine = null;
			if (num != 0)
			{
				if (num != this._curIndex)
				{
					base.StartCoroutine(this.CloseLight(this._curIndex));
				}
				switch (num)
				{
					case 1:
						text = GlobalSettings.Point1Com;
						this._curPointPos = GlobalSettings.Point1Pos;
						this._curIndex = 1;
						break;
					case 2:
						text = GlobalSettings.Point2Com;
						this._curPointPos = GlobalSettings.Point2Pos;
						this._curIndex = 2;
						break;
					case 3:
						text = GlobalSettings.Point3Com;
						this._curPointPos = GlobalSettings.Point3Pos;
						this._curIndex = 3;
						break;
					case 4:
						text = GlobalSettings.Point4Com;
						this._curPointPos = GlobalSettings.Point4Pos;
						this._curIndex = 4;
						break;
					case 5:
						text = GlobalSettings.Point5Com;
						this._curPointPos = GlobalSettings.Point5Pos;
						this._curIndex = 5;
						break;
					case 6:
						text = GlobalSettings.Point6Com;
						this._curPointPos = GlobalSettings.Point6Pos;
						this._curIndex = 6;
						break;
					case 7:
					case 0:
						text = GlobalSettings.Point0Com;
						this._curPointPos = GlobalSettings.Point0Pos;
						this._curIndex = 0;
						break;
					case 8:
						text = "AA FF 01 12  01   01    00 00 00 00 00 2A BD 31    00 00 20 D0   00 00 00 01    C7 55";
						this._curPointPos = this.point8;
						this._curIndex = 8;
						break;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.PortControl.SendMsg(text);
				if (this._coroutine != null)
				{
					base.StopCoroutine(this._coroutine);
				}
				this._coroutine = base.StartCoroutine(this.GetInfo());
				return;
			}
		}
		else
		{
			num = -1;
			if (this.KeyCodeEvent != null)
			{
				this.KeyCodeEvent(data);
			}
			if (this._standByCoroutine != null)
			{
				base.StopCoroutine(this._standByCoroutine);
			}
			this._standByCoroutine = base.StartCoroutine(this.StandBy());
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000040BE File Offset: 0x000022BE
	private IEnumerator GetInfo()
	{
		for (; ; )
		{
			if (this._standByCoroutine != null)
			{
				base.StopCoroutine(this._standByCoroutine);
			}
			this._standByCoroutine = null;
			yield return new WaitForSeconds(1.1f);
			GlobalSettings.IsReceive = true;
			this.PortControl.SendMsg("AA FF 0A 01 00 00 55");
		}
		yield break;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000040CD File Offset: 0x000022CD
	private IEnumerator CloseLight(int index)
	{
		yield return new WaitForSeconds(0.1f);
		Debug.LogError("命令当前的点位 " + index + "  关关关灯");
		switch (index)
		{
			case 0:
				this.PortLightControl.SendStr("0");
				break;
			case 1:
				this.PortLightControl.SendStr(GlobalSettings.Point1.ToLower());
				break;
			case 2:
				this.PortLightControl.SendStr(GlobalSettings.Point2.ToLower());
				break;
			case 3:
				this.PortLightControl.SendStr(GlobalSettings.Point3.ToLower());
				break;
			case 4:
				this.PortLightControl.SendStr(GlobalSettings.Point4.ToLower());
				break;
			case 5:
				this.PortLightControl.SendStr(GlobalSettings.Point5.ToLower());
				break;
			case 6:
				this.PortLightControl.SendStr(GlobalSettings.Point6.ToLower());
				break;
		}
		yield break;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000040E3 File Offset: 0x000022E3
	private IEnumerator OpenLight(int index)
	{
		yield return new WaitForSeconds(0.1f);
		Debug.LogError("命令当前的点位 " + index + "  开灯");
		switch (index)
		{
			case 1:
				this.PortLightControl.SendStr(GlobalSettings.Point1);
				break;
			case 2:
				this.PortLightControl.SendStr(GlobalSettings.Point2);
				break;
			case 3:
				this.PortLightControl.SendStr(GlobalSettings.Point3);
				break;
			case 4:
				this.PortLightControl.SendStr(GlobalSettings.Point4);
				break;
			case 5:
				this.PortLightControl.SendStr(GlobalSettings.Point5);
				break;
			case 6:
				this.PortLightControl.SendStr(GlobalSettings.Point6);
				break;
		}
		yield break;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000040F9 File Offset: 0x000022F9
	private IEnumerator StandBy()
	{
		if (this._curIndex == 0)
		{
			yield break;
		}
		yield return new WaitForSeconds(GlobalSettings.StandByTime);
		this.ProcessData("7");
		yield break;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00002050 File Offset: 0x00000250
	private void Update()
	{
	}


    private void OnGUI()
    {


#if UNITY_IOS
// ... iOS项目才会编译
#elif UNITY_ANDROID
// ... apk 或 iOS项目才会编译
#elif UNITY_EDITOR
		// ... UNITY调试时候才编译
		if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "test1"))
		{

			if (this._coroutine != null)
			{
				base.StopCoroutine(this._coroutine);
			}
			this._coroutine = null;
			isRight = true;
			this.ShowContent();


		}

		if (GUI.Button(new Rect(100f, 0f, 100f, 100f), "test2"))
		{

			if (this._coroutine != null)
			{
				base.StopCoroutine(this._coroutine);
			}
			this._coroutine = null;
			isRight = false;
			this.ShowContent();


		}
#endif


	}

    // Token: 0x0400004B RID: 75
    public List<ExhibitionItem> Contents;

	// Token: 0x0400004C RID: 76
	private VideoPlayer _videoPlayer;

	// Token: 0x0400004D RID: 77
	public PortControl PortControl;

	// Token: 0x0400004E RID: 78
	public PortControl PortLightControl;

	// Token: 0x0400004F RID: 79
	private int point1 = 765138;

	// Token: 0x04000050 RID: 80
	private int point2 = 1174301;

	// Token: 0x04000051 RID: 81
	private int point3 = 1576420;

	// Token: 0x04000052 RID: 82
	private int point4 = 1980399;

	// Token: 0x04000053 RID: 83
	private int point5 = 2387179;

	// Token: 0x04000054 RID: 84
	private int point6 = 2796634;

	// Token: 0x04000055 RID: 85
	private int point7;

	// Token: 0x04000056 RID: 86
	private int point8 = 2800945;

	// Token: 0x04000057 RID: 87
	private int _curPointPos;

	// Token: 0x04000058 RID: 88
	public int Distance = 500;

	// Token: 0x04000059 RID: 89
	public int _curIndex;

	// Token: 0x0400005C RID: 92
	public static SlidewayManager Instance;

	// Token: 0x0400005D RID: 93
	private Coroutine _standByCoroutine;

	// Token: 0x0400005E RID: 94
	private int _curDistance;

	// Token: 0x0400005F RID: 95
	private bool isRight;

	// Token: 0x04000060 RID: 96
	private Coroutine _coroutine;
}
