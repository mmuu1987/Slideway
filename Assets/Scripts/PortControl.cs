using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class PortControl : MonoBehaviour
{
	// Token: 0x06000046 RID: 70 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000034E9 File Offset: 0x000016E9
	private void Update()
	{
		if (this.temp != null)
		{
			if (this.ReceiveMsg != null)
			{
				this.ReceiveMsg(this.temp);
			}
			this.temp = null;
		}
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003514 File Offset: 0x00001714
	public void OpenPort()
	{
		if (string.IsNullOrEmpty(GlobalSettings.portName))
		{
			Debug.LogError("端口字符串为null");
			return;
		}
		string text = null;
		if (this.portName == "Point")
		{
			text = GlobalSettings.portName;
		}
		else if (this.portName == "Light")
		{
			text = GlobalSettings.LightPortName;
		}
		Debug.LogError("串口名字是： " + text);
		if (!string.IsNullOrEmpty(text))
		{
			this.sp = new SerialPort(text, this.baudRate, this.parity, this.dataBits, this.stopBits);
			this.sp.ReadTimeout = 400;
			try
			{
				this.sp.Open();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
			}
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000035E4 File Offset: 0x000017E4
	private void OnApplicationQuit()
	{
		this.canRecieveMsg = false;
		this.ClosePort();
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000035F4 File Offset: 0x000017F4
	public void ClosePort()
	{
		try
		{
			this.sp.Close();
			this.dataReceiveThread.Abort();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003638 File Offset: 0x00001838
	internal void StarcConnect()
	{
		this.OpenPort();
		this.dataReceiveThread = new Thread(new ThreadStart(this.PortReceive));
		this.dataReceiveThread.Start();
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003664 File Offset: 0x00001864
	private void PrintData()
	{
		for (int i = 0; i < this.listReceive.Count; i++)
		{
			this.strchar[i] = (char)this.listReceive[i];
			this.str = new string(this.strchar);
		}
		Debug.Log(this.str);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000036B8 File Offset: 0x000018B8
	private void DataReceiveFunction()
	{
		//new byte[1024];
		//for (; ; )
		//{
		//	if (this.sp != null && this.sp.IsOpen)
		//	{
		//		try
		//		{
		//			Debug.LogError(this.sp.ReadLine());
		//		}
		//		catch (Exception ex)
		//		{
		//			ex.GetType() != typeof(ThreadAbortException);
		//		}
		//	}
		//}
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x0600004E RID: 78 RVA: 0x00003720 File Offset: 0x00001920
	// (remove) Token: 0x0600004F RID: 79 RVA: 0x00003758 File Offset: 0x00001958
	public event Action<string> ReceiveMsg;

	// Token: 0x06000050 RID: 80 RVA: 0x00003790 File Offset: 0x00001990
	private void PortReceive()
	{
		try
		{
			while (this.canRecieveMsg)
			{
				if (!this.sp.IsOpen)
				{
					break;
				}
				if (GlobalSettings.IsReceive)
				{
					Thread.Sleep(500);
					int bytesToRead = this.sp.BytesToRead;
					if (bytesToRead != 0)
					{
						int i = 0;
						StringBuilder stringBuilder = new StringBuilder();
						while (i < bytesToRead)
						{
							byte[] array = new byte[1];
							int num = this.sp.Read(array, 0, 1);
							stringBuilder.Append(Encoding.ASCII.GetString(array, 0, num));
							i += num;
						}
						this.temp = stringBuilder.ToString();
						GlobalSettings.IsReceive = false;
					}
				}
			}
		}
		catch
		{
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003848 File Offset: 0x00001A48
	public void WriteData(string dataStr)
	{
		if (this.sp.IsOpen)
		{
			this.sp.Write(dataStr);
		}
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00003864 File Offset: 0x00001A64
	public void SendMsg(string s)
	{
		byte[] data = this.textWork16(s);
		this.SendSerialPortData0(data);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003880 File Offset: 0x00001A80
	private byte[] textWork16(string strText)
	{
		strText = strText.Replace(" ", "");
		byte[] array = new byte[strText.Length / 2];
		for (int i = 0; i < strText.Length / 2; i++)
		{
			array[i] = Convert.ToByte(Convert.ToInt32(strText.Substring(i * 2, 2), 16));
		}
		return array;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000038DA File Offset: 0x00001ADA
	public void SendSerialPortData0(byte[] data)
	{
		if (this.sp.IsOpen)
		{
			this.sp.Write(data, 0, data.Length);
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000038FC File Offset: 0x00001AFC
	public void SendStr(string str)
	{
		byte[] bytes = Encoding.ASCII.GetBytes(str);
		if (this.sp.IsOpen)
		{
			this.sp.Write(bytes, 0, bytes.Length);
		}
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00003934 File Offset: 0x00001B34
	public static string StringToHex16String(string _str)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(_str);
		string text = string.Empty;
		for (int i = 0; i < bytes.Length; i++)
		{
			text += Convert.ToString(bytes[i], 16);
		}
		return text;
	}

	// Token: 0x0400003B RID: 59
	public GUIText gui;

	// Token: 0x0400003C RID: 60
	public string portName = "COM3";

	// Token: 0x0400003D RID: 61
	public int baudRate = 9600;

	// Token: 0x0400003E RID: 62
	public Parity parity;

	// Token: 0x0400003F RID: 63
	public int dataBits = 8;

	// Token: 0x04000040 RID: 64
	public StopBits stopBits = StopBits.One;

	// Token: 0x04000041 RID: 65
	private SerialPort sp;

	// Token: 0x04000042 RID: 66
	private Thread dataReceiveThread;

	// Token: 0x04000043 RID: 67
	private string message = "";

	// Token: 0x04000044 RID: 68
	public List<byte> listReceive = new List<byte>();

	// Token: 0x04000045 RID: 69
	private char[] strchar = new char[100];

	// Token: 0x04000046 RID: 70
	private string str;

	// Token: 0x04000047 RID: 71
	private bool canRecieveMsg = true;

	// Token: 0x04000049 RID: 73
	private string temp;
}
