using System;
using HarmonyLib;

// Token: 0x02000006 RID: 6
[HarmonyPatch(typeof(Session), "Load")]
public class Session_Load__Patch
{
	// Token: 0x0600000B RID: 11 RVA: 0x00002374 File Offset: 0x00000574
	private static void Finalizer(Session __instance)
	{
		SISNoEnterInCombat_Config.ResetSettings();
		SISNoEnterInCombat_Config.LoadSettings();
	}
}
