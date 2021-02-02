using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using UnityEngine;

// Token: 0x0200000A RID: 10
public static class GlobalSettings
{
	// Token: 0x0600002F RID: 47 RVA: 0x000027B8 File Offset: 0x000009B8
	public static bool FileIsUsed(string fileFullName)
	{
		bool result = false;
		if (!File.Exists(fileFullName))
		{
			result = false;
		}
		else
		{
			FileStream fileStream = null;
			try
			{
				fileStream = File.Open(fileFullName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
			}
			catch (IOException)
			{
				result = true;
			}
			catch (Exception)
			{
				result = true;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}
		return result;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002820 File Offset: 0x00000A20
	public static void ReadXml()
	{
		XmlDocument xmlDocument = new XmlDocument();
		string text = Application.streamingAssetsPath + "/Setting.xml";
		if (!File.Exists(text))
		{
			Log.Error("Common", "没有找到XML文件");
		}
		xmlDocument.Load(text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("CommonTag");
		if (xmlNode != null)
		{
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				if (xmlNode2.Name == "LOG_LEVENL")
				{
					GlobalSettings.LOG_LEVENL = int.Parse(xmlNode2.InnerText);
				}
				else if (xmlNode2.Name == "IsShowDebug")
				{
					if (xmlNode2.InnerText == "false")
					{
						GlobalSettings.IsShowDebug = false;
					}
					else
					{
						GlobalSettings.IsShowDebug = true;
					}
				}
				else if (xmlNode2.Name == "MoveSpeed")
				{
					GlobalSettings.MoveSpeed = float.Parse(xmlNode2.InnerText);
				}
				else if (xmlNode2.Name == "portName")
				{
					GlobalSettings.portName = xmlNode2.InnerText;
					UnityEngine.Debug.Log("portName is " + GlobalSettings.portName);
				}
				else if (xmlNode2.Name == "portLigthName")
				{
					GlobalSettings.LightPortName = xmlNode2.InnerText;
					UnityEngine.Debug.Log("LightPortName is " + GlobalSettings.LightPortName);
				}
				else if (xmlNode2.Name == "StandByTime")
				{
					GlobalSettings.StandByTime = float.Parse(xmlNode2.InnerText);
				}
				else
				{
					if (xmlNode2.Name == "PointCom")
					{
						int num = 0;

                       
                        IEnumerator enumerator2 = xmlNode2.ChildNodes.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            object obj2 = enumerator2.Current;
                            XmlElement xmlElement = (XmlElement)obj2;
                            switch (num)
                            {
                                case 0:
                                    GlobalSettings.Point0Com = xmlElement.InnerText;
                                    if (string.IsNullOrEmpty(GlobalSettings.Point0Com))
                                    {
                                        throw new UnityException("获得的是空的字符串，请查看xml是否正确");
                                    }
                                    GlobalSettings.Point0Pos = int.Parse(GlobalSettings.Point0Com.Replace(" ", "").Substring(12, 16), NumberStyles.AllowHexSpecifier);
                                    Debug.Log(string.Concat(new object[]
                                    {
                                        "str is ",
                                        GlobalSettings.Point0Com,
                                        "  conversionHEX is ",
                                        GlobalSettings.Point0Pos
                                    }));
                                    break;
                                case 1:
                                    GlobalSettings.Point1Com = xmlElement.InnerText;
                                    if (string.IsNullOrEmpty(GlobalSettings.Point1Com))
                                    {
                                        throw new UnityException("获得的是空的字符串，请查看xml是否正确");
                                    }
                                    GlobalSettings.Point1Pos = int.Parse(GlobalSettings.Point1Com.Replace(" ", "").Substring(12, 16), NumberStyles.AllowHexSpecifier);
                                    Debug.Log(string.Concat(new object[]
                                    {
                                        "str is ",
                                        GlobalSettings.Point1Com,
                                        "  conversionHEX is ",
                                        GlobalSettings.Point1Pos
                                    }));
                                    break;
                                case 2:
                                    GlobalSettings.Point2Com = xmlElement.InnerText;
                                    if (string.IsNullOrEmpty(GlobalSettings.Point2Com))
                                    {
                                        throw new UnityException("获得的是空的字符串，请查看xml是否正确");
                                    }
                                    GlobalSettings.Point2Pos = int.Parse(GlobalSettings.Point2Com.Replace(" ", "").Substring(12, 16), NumberStyles.AllowHexSpecifier);
                                    Debug.Log(string.Concat(new object[]
                                    {
                                        "str is ",
                                        GlobalSettings.Point2Com,
                                        "  conversionHEX is ",
                                        GlobalSettings.Point2Pos
                                    }));
                                    break;
                                case 3:
                                    GlobalSettings.Point3Com = xmlElement.InnerText;
                                    if (string.IsNullOrEmpty(GlobalSettings.Point3Com))
                                    {
                                        throw new UnityException("获得的是空的字符串，请查看xml是否正确");
                                    }
                                    GlobalSettings.Point3Pos = int.Parse(GlobalSettings.Point3Com.Replace(" ", "").Substring(12, 16), NumberStyles.AllowHexSpecifier);
                                    Debug.Log(string.Concat(new object[]
                                    {
                                        "str is ",
                                        GlobalSettings.Point3Com,
                                        "  conversionHEX is ",
                                        GlobalSettings.Point3Pos
                                    }));
                                    break;
                                case 4:
                                    GlobalSettings.Point4Com = xmlElement.InnerText;
                                    if (string.IsNullOrEmpty(GlobalSettings.Point4Com))
                                    {
                                        throw new UnityException("获得的是空的字符串，请查看xml是否正确");
                                    }
                                    GlobalSettings.Point4Pos = int.Parse(GlobalSettings.Point4Com.Replace(" ", "").Substring(12, 16), NumberStyles.AllowHexSpecifier);
                                    Debug.Log(string.Concat(new object[]
                                    {
                                        "str is ",
                                        GlobalSettings.Point4Com,
                                        "  conversionHEX is ",
                                        GlobalSettings.Point4Pos
                                    }));
                                    break;
                                case 5:
                                    GlobalSettings.Point5Com = xmlElement.InnerText;
                                    if (string.IsNullOrEmpty(GlobalSettings.Point5Com))
                                    {
                                        throw new UnityException("获得的是空的字符串，请查看xml是否正确");
                                    }
                                    GlobalSettings.Point5Pos = int.Parse(GlobalSettings.Point5Com.Replace(" ", "").Substring(12, 16), NumberStyles.AllowHexSpecifier);
                                    Debug.Log(string.Concat(new object[]
                                    {
                                        "str is ",
                                        GlobalSettings.Point5Com,
                                        "  conversionHEX is ",
                                        GlobalSettings.Point5Pos
                                    }));
                                    break;
                                case 6:
                                    GlobalSettings.Point6Com = xmlElement.InnerText;
                                    if (string.IsNullOrEmpty(GlobalSettings.Point6Com))
                                    {
                                        throw new UnityException("获得的是空的字符串，请查看xml是否正确");
                                    }
                                    GlobalSettings.Point6Pos = int.Parse(GlobalSettings.Point6Com.Replace(" ", "").Substring(12, 16), NumberStyles.AllowHexSpecifier);
                                    Debug.Log(string.Concat(new object[]
                                    {
                                        "str is ",
                                        GlobalSettings.Point6Com,
                                        "  conversionHEX is ",
                                        GlobalSettings.Point6Pos
                                    }));
                                    break;
                            }
                            num++;
                        }
                        continue;
                    }
					if (xmlNode2.Name == "Points")
					{
						int num2 = 1;
						foreach (object obj3 in xmlNode2.ChildNodes)
						{
							XmlElement xmlElement2 = (XmlElement)obj3;
							switch (num2)
							{
								case 1:
									GlobalSettings.Point1 = xmlElement2.InnerText;
									if (string.IsNullOrEmpty(GlobalSettings.Point1))
									{
										throw new UnityException("获得的是空的字符串，请查看xml是否正确");
									}
									Debug.Log("str is " + GlobalSettings.Point1);
									break;
								case 2:
									GlobalSettings.Point2 = xmlElement2.InnerText;
									if (string.IsNullOrEmpty(GlobalSettings.Point2))
									{
										throw new UnityException("获得的是空的字符串，请查看xml是否正确");
									}
									Debug.Log("str is " + GlobalSettings.Point2);
									break;
								case 3:
									GlobalSettings.Point3 = xmlElement2.InnerText;
									if (string.IsNullOrEmpty(GlobalSettings.Point3))
									{
										throw new UnityException("获得的是空的字符串，请查看xml是否正确");
									}
									Debug.Log("str is " + GlobalSettings.Point3);
									break;
								case 4:
									GlobalSettings.Point4 = xmlElement2.InnerText;
									if (string.IsNullOrEmpty(GlobalSettings.Point4))
									{
										throw new UnityException("获得的是空的字符串，请查看xml是否正确");
									}
									Debug.Log("str is " + GlobalSettings.Point4);
									break;
								case 5:
									GlobalSettings.Point5 = xmlElement2.InnerText;
									if (string.IsNullOrEmpty(GlobalSettings.Point5))
									{
										throw new UnityException("获得的是空的字符串，请查看xml是否正确");
									}
									Debug.Log("str is " + GlobalSettings.Point5);
									break;
								case 6:
									GlobalSettings.Point6 = xmlElement2.InnerText;
									if (string.IsNullOrEmpty(GlobalSettings.Point6))
									{
										throw new UnityException("获得的是空的字符串，请查看xml是否正确");
									}
									Debug.Log("str is " + GlobalSettings.Point6);
									break;
								case 7:
									GlobalSettings.Point7 = xmlElement2.InnerText;
									if (string.IsNullOrEmpty(GlobalSettings.Point7))
									{
										throw new UnityException("获得的是空的字符串，请查看xml是否正确");
									}
									Debug.Log("str is " + GlobalSettings.Point7);
									break;
								case 8:
									GlobalSettings.Point8 = xmlElement2.InnerText;
									if (string.IsNullOrEmpty(GlobalSettings.Point8))
									{
										throw new UnityException("获得的是空的字符串，请查看xml是否正确");
									}
									Debug.Log("str is " + GlobalSettings.Point8);
									break;
							}
							num2++;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x0000309C File Offset: 0x0000129C
	public static void SaveXml()
	{
		string filename = Application.streamingAssetsPath + "/test.xml";
		XmlDocument xmlDocument = new XmlDocument();
		XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "");
		xmlDocument.AppendChild(newChild);
		XmlElement xmlElement = xmlDocument.CreateElement("CommonTag");
		XmlElement xmlElement2 = xmlDocument.CreateElement("LOG_LEVENL");
		xmlElement2.InnerText = GlobalSettings.LOG_LEVENL.ToString();
		xmlElement.AppendChild(xmlElement2);
		xmlDocument.AppendChild(xmlElement);
		xmlDocument.Save(filename);
		Debug.Log("创建XML成功！");
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00003124 File Offset: 0x00001324
	public static string CreatUuid()
	{
		string text = null;
		System.Random random = new System.Random();
		for (int i = 1; i <= 8; i++)
		{
			text += GlobalSettings.CharsLetter[random.Next(GlobalSettings.CharsLetter.Length)].ToString();
		}
		return text;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00003169 File Offset: 0x00001369
	public static IEnumerator WaitEndFrame(Action callBack)
	{
		yield return null;
		if (callBack != null)
		{
			callBack();
		}
		yield break;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00003178 File Offset: 0x00001378
	public static void InitArg()
	{
		string @string = PlayerPrefs.GetString("AV");
		string string2 = PlayerPrefs.GetString("TV");
		string string3 = PlayerPrefs.GetString("ISO");
		if (!string.IsNullOrEmpty(@string))
		{
			GlobalSettings.AV = uint.Parse(@string);
		}
		if (!string.IsNullOrEmpty(string2))
		{
			GlobalSettings.TV = uint.Parse(string2);
		}
		if (!string.IsNullOrEmpty(string3))
		{
			GlobalSettings.ISO = uint.Parse(string3);
		}
	}

	// Token: 0x06000035 RID: 53
	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);

	// Token: 0x06000036 RID: 54
	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern IntPtr GetForegroundWindow();

	// Token: 0x0400000C RID: 12
	public const float ZoomMagnification = 1.6f;

	// Token: 0x0400000D RID: 13
	public static int PictureWidth = 2400;

	// Token: 0x0400000E RID: 14
	public static int PictureHeight = 1600;

	// Token: 0x0400000F RID: 15
	public static float WidthScale = 0.5f;

	// Token: 0x04000010 RID: 16
	public static float HeightScale = 1f;

	// Token: 0x04000011 RID: 17
	public static uint AV = 40U;

	// Token: 0x04000012 RID: 18
	public static uint TV = 109U;

	// Token: 0x04000013 RID: 19
	public static uint ISO = 104U;

	// Token: 0x04000014 RID: 20
	private static char[] CharsLetter = new char[]
	{
		'1',
		'2',
		'3',
		'4',
		'5',
		'6',
		'7',
		'8',
		'9',
		'0'
	};

	// Token: 0x04000015 RID: 21
	public static string ServerPictureIp = " http://www.syyj.tglfair.com/Webpage/VoiceOffice/MyScreenshots.aspx";

	// Token: 0x04000016 RID: 22
	public static bool IsShowDebug = true;

	// Token: 0x04000017 RID: 23
	public static float MoveSpeed;

	// Token: 0x04000018 RID: 24
	public static string portName = "COM3";

	// Token: 0x04000019 RID: 25
	public static string LightPortName = "COM4";

	// Token: 0x0400001A RID: 26
	public static string Point0Com = "";

	// Token: 0x0400001B RID: 27
	public static string Point1Com = "";

	// Token: 0x0400001C RID: 28
	public static string Point2Com = "";

	// Token: 0x0400001D RID: 29
	public static string Point3Com = "";

	// Token: 0x0400001E RID: 30
	public static string Point4Com = "";

	// Token: 0x0400001F RID: 31
	public static string Point5Com = "";

	// Token: 0x04000020 RID: 32
	public static string Point6Com = "";

	// Token: 0x04000021 RID: 33
	public static string Point1 = "";

	// Token: 0x04000022 RID: 34
	public static string Point2 = "";

	// Token: 0x04000023 RID: 35
	public static string Point3 = "";

	// Token: 0x04000024 RID: 36
	public static string Point4 = "";

	// Token: 0x04000025 RID: 37
	public static string Point5 = "";

	// Token: 0x04000026 RID: 38
	public static string Point6 = "";

	// Token: 0x04000027 RID: 39
	public static string Point7 = "";

	// Token: 0x04000028 RID: 40
	public static string Point8 = "";

	// Token: 0x04000029 RID: 41
	public static int Point0Pos;

	// Token: 0x0400002A RID: 42
	public static int Point1Pos;

	// Token: 0x0400002B RID: 43
	public static int Point2Pos;

	// Token: 0x0400002C RID: 44
	public static int Point3Pos;

	// Token: 0x0400002D RID: 45
	public static int Point4Pos;

	// Token: 0x0400002E RID: 46
	public static int Point5Pos;

	// Token: 0x0400002F RID: 47
	public static int Point6Pos;

	// Token: 0x04000030 RID: 48
	public static bool IsReceive = false;

	// Token: 0x04000031 RID: 49
	public static float StandByTime = 10f;

	// Token: 0x04000032 RID: 50
	public static string Stie = "阿里巴巴拍照留念";

	// Token: 0x04000033 RID: 51
	public static bool IsOutLog = true;

	// Token: 0x04000034 RID: 52
	public const int REPORT_LEVENL = 2;

	// Token: 0x04000035 RID: 53
	public static int LOG_LEVENL = 3;
}
