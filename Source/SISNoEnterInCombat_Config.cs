using System;
using System.IO;
using System.Xml.Serialization;
using HarmonyLib;
using Newtonsoft.Json;

public class SISNoEnterInCombat_Config
{
	public static void Log(string message)
	{
		try
		{
            if (LoggingEnabled)
			{
				FileLog.Log("[SISNEIC] " + message);
			}
		}
		catch { }
	}

	private static string GetStoryPath()
	{
		foreach (Story story in GameImpl.Instance.CurrentStories)
		{
			string text = story.Path + ModIdentifier;
			string text2 = story.Path + SettingsFilePath;
            if (File.Exists(text) || File.Exists(text2))
			{
				return story.Path;
			}
		}
		return ".";
	}

	public static void LoadSettings()
	{
		try
		{
			bool fileExists = false;
			foreach (Story story in GameImpl.Instance.CurrentStories)
			{
				string text = story.Path + SettingsFilePath;
                if (File.Exists(text))
                {
                    fileExists = true;
                    using (FileStream fileStream = new FileStream(text, FileMode.Open))
                    {
	                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISNoEnterInCombat_Settings));
                        SISNoEnterInCombat_Settings sisnoEnterInCombat_Settings = (SISNoEnterInCombat_Settings)xmlSerializer.Deserialize(fileStream);
                        fileStream.Dispose();
                        Log(JsonConvert.SerializeObject(sisnoEnterInCombat_Settings));
                        FillSettings(sisnoEnterInCombat_Settings);
                    }

                    Log("Loaded settings from path " + text);
                }
            }
            if (!fileExists)
			{
				SISNoEnterInCombat_Settings @default = SISNoEnterInCombat_Settings.GetDefault();
				FillSettings(@default);
				Log("Loaded default settings since no settings file was found");
				Log(JsonConvert.SerializeObject(@default));
				foreach (Story story2 in GameImpl.Instance.CurrentStories)
				{
                    if (File.Exists(story2.Path + ModIdentifier))
					{
						SaveSettings(SISNoEnterInCombat_Settings.GetDefault(), story2.Path);
						Log("Saved settings file at path " + story2.Path);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Log(ex.ToString());
			FillSettings(SISNoEnterInCombat_Settings.GetDefault());
			Log("Loaded default settings since no settings file was found");
		}
	}

	private static void SaveSettings(SISNoEnterInCombat_Settings settings, string folderPath)
	{
		string text = folderPath + SettingsFilePath;
		Log("Saving to " + text);
        if (File.Exists(text))
		{
			File.Delete(text);
		}
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISNoEnterInCombat_Settings));
		FileStream fileStream = File.Create(text);
		xmlSerializer.Serialize(fileStream, settings);
		fileStream.Dispose();
	}

	private static void FillSettings(SISNoEnterInCombat_Settings myobj)
	{
		FileLog.logPath = GetStoryPath() + LogFilePath;
        if (File.Exists(FileLog.logPath))
		{
			File.Delete(FileLog.logPath);
		}
		LoggingEnabled = myobj.LoggingEnabled;
		EnabledForPlayer = myobj.EnabledForPlayer;
		EnabledForCommunity = myobj.EnabledForCommunity;
		EnabledForNPCs = myobj.EnabledForNPCs;
		CanEnterCommunityBuildingsInCombat = myobj.CanEnterCommunityBuildingsInCombat;
	}

	public static void ResetSettings()
	{
		FillSettings(SISNoEnterInCombat_Settings.GetDefault());
	}

	private static string SettingsFilePath = "/Settings_NoEnterInCombat.xml";
	private static string LogFilePath = "/Log_SISNoEnterInCombat.txt";
	private static string ModIdentifier = "/SISNOENTERINCOMBAT.txt";
	public const string StatusMsgNoEnterInCombat = "HUD_SISNoEnterInCombat";

	private static bool LoggingEnabled;
	public static bool EnabledForPlayer;
	public static bool EnabledForCommunity;
	public static bool EnabledForNPCs;
	public static bool CanEnterCommunityBuildingsInCombat;
}
