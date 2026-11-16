using System;
using System.IO;
using System.Xml.Serialization;
using HarmonyLib;
using Newtonsoft.Json;

// Token: 0x02000008 RID: 8
public class SISNoEnterInCombat_Config
{
	// Token: 0x0600000F RID: 15 RVA: 0x000023CC File Offset: 0x000005CC
	public static void Log(string message)
	{
		try
		{
			bool loggingActive = SISNoEnterInCombat_Config.LoggingActive;
			if (loggingActive)
			{
				FileLog.Log("[SISNEIC] " + message);
			}
		}
		catch
		{
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002410 File Offset: 0x00000610
	private static string GetStoryPath()
	{
		foreach (Story story in GameImpl.Instance.CurrentStories)
		{
			string text = story.Path + SISNoEnterInCombat_Config.ModIdentifier;
			string text2 = story.Path + SISNoEnterInCombat_Config.SettingsFilePath;
			bool flag = File.Exists(text) || File.Exists(text2);
			if (flag)
			{
				return story.Path;
			}
		}
		return ".";
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000024B4 File Offset: 0x000006B4
	public static void LoadSettings()
	{
		try
		{
			bool flag = false;
			foreach (Story story in GameImpl.Instance.CurrentStories)
			{
				string text = story.Path + SISNoEnterInCombat_Config.SettingsFilePath;
				bool flag2 = File.Exists(text);
				if (flag2)
				{
					flag = true;
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISNoEnterInCombat_Settings));
					FileStream fileStream = new FileStream(text, FileMode.Open);
					SISNoEnterInCombat_Settings sisnoEnterInCombat_Settings = (SISNoEnterInCombat_Settings)xmlSerializer.Deserialize(fileStream);
					fileStream.Dispose();
					SISNoEnterInCombat_Config.Log(JsonConvert.SerializeObject(sisnoEnterInCombat_Settings));
					SISNoEnterInCombat_Config.FillSettings(sisnoEnterInCombat_Settings);
					SISNoEnterInCombat_Config.Log("Loaded settings from path " + text);
				}
			}
			bool flag3 = !flag;
			if (flag3)
			{
				SISNoEnterInCombat_Settings @default = SISNoEnterInCombat_Settings.GetDefault();
				SISNoEnterInCombat_Config.FillSettings(@default);
				SISNoEnterInCombat_Config.Log("Loaded default settings since no settings file was found");
				SISNoEnterInCombat_Config.Log(JsonConvert.SerializeObject(@default));
				foreach (Story story2 in GameImpl.Instance.CurrentStories)
				{
					string text2 = story2.Path + SISNoEnterInCombat_Config.ModIdentifier;
					bool flag4 = File.Exists(text2);
					if (flag4)
					{
						SISNoEnterInCombat_Config.SaveSettings(SISNoEnterInCombat_Settings.GetDefault(), story2.Path);
						SISNoEnterInCombat_Config.Log("Saved settings file at path " + story2.Path);
					}
				}
			}
		}
		catch (Exception ex)
		{
			SISNoEnterInCombat_Config.Log(ex.ToString());
			SISNoEnterInCombat_Config.FillSettings(SISNoEnterInCombat_Settings.GetDefault());
			SISNoEnterInCombat_Config.Log("Loaded default settings since no settings file was found");
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000026B0 File Offset: 0x000008B0
	private static void SaveSettings(SISNoEnterInCombat_Settings settings, string folderPath)
	{
		string text = folderPath + SISNoEnterInCombat_Config.SettingsFilePath;
		SISNoEnterInCombat_Config.Log("Saving to " + text);
		bool flag = File.Exists(text);
		if (flag)
		{
			File.Delete(text);
		}
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISNoEnterInCombat_Settings));
		FileStream fileStream = File.Create(text);
		xmlSerializer.Serialize(fileStream, settings);
		fileStream.Dispose();
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002718 File Offset: 0x00000918
	private static void FillSettings(SISNoEnterInCombat_Settings myobj)
	{
		FileLog.logPath = SISNoEnterInCombat_Config.GetStoryPath() + SISNoEnterInCombat_Config.LogFilePath;
		bool flag = File.Exists(FileLog.logPath);
		if (flag)
		{
			File.Delete(FileLog.logPath);
		}
		SISNoEnterInCombat_Config.LoggingActive = myobj.LoggingActive;
		SISNoEnterInCombat_Config.EnabledForPlayer = myobj.EnabledForPlayer;
		SISNoEnterInCombat_Config.EnabledForCommunity = myobj.EnabledForCommunity;
		SISNoEnterInCombat_Config.EnabledForNPCs = myobj.EnabledForNPCs;
		SISNoEnterInCombat_Config.CanEnterCommunityBuildingsInCombat = myobj.CanEnterCommunityBuildingsInCombat;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000278C File Offset: 0x0000098C
	public static void ResetSettings()
	{
		SISNoEnterInCombat_Config.FillSettings(SISNoEnterInCombat_Settings.GetDefault());
	}

	// Token: 0x04000007 RID: 7
	private static bool LoggingActive;

	// Token: 0x04000008 RID: 8
	private static string SettingsFilePath = "/Settings_NoEnterInCombat.xml";

	// Token: 0x04000009 RID: 9
	private static string LogFilePath = "/log.txt";

	// Token: 0x0400000A RID: 10
	private static string ModIdentifier = "/SISNOENTERINCOMBAT.txt";

	// Token: 0x0400000B RID: 11
	public const string StatusMsgNoEnterInCombat = "HUD_SISNoEnterInCombat";

	// Token: 0x0400000C RID: 12
	public static bool EnabledForPlayer;

	// Token: 0x0400000D RID: 13
	public static bool EnabledForCommunity;

	// Token: 0x0400000E RID: 14
	public static bool EnabledForNPCs;

	// Token: 0x0400000F RID: 15
	public static bool CanEnterCommunityBuildingsInCombat;
}
