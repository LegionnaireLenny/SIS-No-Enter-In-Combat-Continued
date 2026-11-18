using HarmonyLib;

public class Main
{
	public static void Load()
	{
        HarmonyInstance = new Harmony("SISNoEnterInCombat");
		HarmonyInstance.PatchAll();
	}

	public static void Unload()
	{
        if (HarmonyInstance != null)
		{
			HarmonyInstance.UnpatchAll(null);
		}
	}

	public static Harmony HarmonyInstance;
}
