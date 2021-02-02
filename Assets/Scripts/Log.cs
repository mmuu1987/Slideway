using System;
using System.IO;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class Log
{
	// Token: 0x06000038 RID: 56 RVA: 0x00003321 File Offset: 0x00001521
	public static void Debug(string className, string content)
	{
		if (GlobalSettings.LOG_LEVENL >= 3)
		{
			Log.WriteLog("DEBUG", className, content);
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00003337 File Offset: 0x00001537
	public static void Info(string className, string content)
	{
		if (GlobalSettings.LOG_LEVENL >= 2)
		{
			Log.WriteLog("INFO", className, content);
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000334D File Offset: 0x0000154D
	public static void Error(string className, string content)
	{
		if (GlobalSettings.LOG_LEVENL >= 1)
		{
			Log.WriteLog("ERROR", className, content);
		}
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00003364 File Offset: 0x00001564
	protected static void WriteLog(string type, string className, string content)
	{
		if (!Directory.Exists(Log.path))
		{
			Directory.CreateDirectory(Log.path);
		}
		string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
		StreamWriter streamWriter = File.AppendText(Log.path + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
		string value = string.Concat(new string[]
		{
			text,
			" ",
			type,
			" ",
			className,
			": ",
			content
		});
		streamWriter.WriteLine(value);
		streamWriter.Close();
	}

	// Token: 0x04000036 RID: 54
	public static string path = Application.streamingAssetsPath + "/logs";
}
