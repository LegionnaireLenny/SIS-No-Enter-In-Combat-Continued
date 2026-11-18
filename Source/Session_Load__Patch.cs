using HarmonyLib;

[HarmonyPatch(typeof(Session), "Load")]
public class Session_Load__Patch
{
	private static void Finalizer(Session __instance)
	{
		SISNoEnterInCombat_Config.ResetSettings();
		SISNoEnterInCombat_Config.LoadSettings();
	}
}
